using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using ES.Domain;
using ES.Domain.ApiCommands;
using ES.Domain.ApiResults;
using ES.Domain.Configurations;
using ES.Domain.Constants;
using ES.Domain.DTO.CryptoCompare;
using ES.Domain.Entities;
using ES.Domain.Interfaces.Gateways;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ES.Infrastructure.Services
{
    public class SignalService
    {
        private readonly EmailService _emailService;

        private readonly ICryptoCompareGateway _cryptoCompareGateway;

        private readonly Timer _timer;

        private readonly string _connectionString;

        private readonly SignalConfiguration _signalConfiguartion;

        public SignalService(EmailService emailService,
            ICryptoCompareGateway cryptoCompareGateway,
            IConfiguration configuration,
            IOptions<SignalConfiguration> signalConfiguration)
        {
            _connectionString = configuration.GetConnectionString(ContextContstants.ConnectionStringCoreDB);
            _emailService = emailService;
            _cryptoCompareGateway = cryptoCompareGateway;
            _signalConfiguartion = signalConfiguration?.Value;

            _timer = new Timer();
            _timer.Elapsed += OnTimerElapsed;
            _timer.Interval = 1000 * _signalConfiguartion.Period;
            //_timer.AutoReset = true;

            _timer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            //_timer.Stop();
            using (var context = new CoreDBContext(new DbContextOptionsBuilder<CoreDBContext>().UseNpgsql(_connectionString).Options))
            {
                List<Account> accounts = context?.Accounts?
                    .Include(a => a.Subscriptions)
                    .ThenInclude(s => s.Currency)
                    .ToList();

                List<SignalsDTO> signalsDTOs = new List<SignalsDTO>();

                Dictionary<string, List<SignalsDTO>> userSignals = new Dictionary<string, List<SignalsDTO>>(accounts?.Count ?? 0);

                List<string> currencies = accounts?.SelectMany(x => x.Subscriptions?.Select(s => s?.Currency?.Name))?.Distinct()?.ToList();

                foreach (var currency in currencies)
                {
                    SignalsDTO signal = CheckSignals(currency).Result;
                    if (signal != null)
                    {
                        signalsDTOs.Add(signal);
                    }
                }

                foreach (var account in accounts)
                {
                    foreach (var subscription in account?.Subscriptions)
                    {
                        if (currencies.Contains(subscription?.Currency?.Name))
                        {
                            if (userSignals.TryGetValue(account?.Email, out var signals) == false)
                            {
                                signals = new List<SignalsDTO>();
                                userSignals.Add(account?.Email, signals);
                            }
                            if (signals.Exists(s => s.Symbol == subscription?.Currency?.Symbol) == false)
                            {
                                SignalsDTO signal = signalsDTOs.FirstOrDefault(s => s.Symbol == subscription?.Currency.Symbol);
                                signals.Add(signal);
                            }
                        }
                    }
                }

                SendSignals(userSignals);
            }
        }

        private async Task<SignalsDTO> CheckSignals(string currency)
        {
            SignalCommand signalCommand = new SignalCommand
            {
                Currency = currency
            };
            CommandResult<SignalsDTO> commandResult = await _cryptoCompareGateway.LatestSignals(signalCommand);
            return commandResult?.Content;
        }

        private void SendSignals(Dictionary<string, List<SignalsDTO>> userSignals)
        {
            foreach (var account in userSignals)
            {
                if (account.Value?.Count > 0)
                {
                    string json = JsonConvert.SerializeObject(account.Value);
                    _emailService.Send(account.Key, json);
                }
            }
        }
    }
}
