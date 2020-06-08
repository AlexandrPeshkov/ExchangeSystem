
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using ES.Domain;
using ES.Domain.ApiCommands;
using ES.Domain.ApiResults;
using ES.Domain.DTO.CryptoCompare;
using ES.Domain.Entities;
using ES.Domain.Interfaces.Gateways;
using Newtonsoft.Json;

namespace ES.Infrastructure.Services
{
    public class SignalService
    {
        private readonly EmailService _emailService;

        private readonly ICryptoCompareGateway _cryptoCompareGateway;

        private readonly Timer _timer;

        private readonly CoreDBContext _dBContext;

        public SignalService(EmailService emailService, ICryptoCompareGateway cryptoCompareGateway, CoreDBContext dBContext)
        {
            _emailService = emailService;
            _cryptoCompareGateway = cryptoCompareGateway;
            _dBContext = dBContext;

            _timer = new Timer();
            _timer.Elapsed += OnTimerElapsed;
            _timer.Interval = 1000 * 10;
            _timer.AutoReset = true;
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            List<Account> accounts = _dBContext.Accounts.ToList();

            List<SignalsDTO> signalsDTOs = new List<SignalsDTO>();

            Dictionary<string, List<SignalsDTO>> userSignals = new Dictionary<string, List<SignalsDTO>>(accounts.Count);

            List<string> currencies = accounts.SelectMany(x => x.Subscriptions?.SelectMany(s => s.Currencies?.Select(c => c?.Name))).Distinct().ToList();

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
                    foreach (var currency in subscription?.Currencies)
                    {
                        if (currencies.Contains(currency?.Name))
                        {
                            if (userSignals.TryGetValue(account?.Email, out var signals) == false)
                            {
                                signals = new List<SignalsDTO>();
                                userSignals.Add(account?.Email, signals);
                            }
                            if (signals.Exists(s => s.Symbol == currency?.Symbol) == false)
                            {
                                SignalsDTO signal = signalsDTOs.FirstOrDefault(s => s.Symbol == currency.Symbol);
                                signals.Add(signal);
                            }
                        }
                    }
                }
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
