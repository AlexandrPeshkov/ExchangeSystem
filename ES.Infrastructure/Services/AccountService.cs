using System.Collections.Generic;
using System.Threading.Tasks;
using ES.Domain;
using ES.Domain.ApiResults;
using ES.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ES.Infrastructure.Services
{
    public class AccountService
    {
        private readonly CoreDBContext _coreDBContext;
        public AccountService(CoreDBContext coreDBContext)
        {
            _coreDBContext = coreDBContext;
        }

        public async Task<CommandResult<string>> CreateAccount(string email)
        {
            CommandResult<string> commandResult = new CommandResult<string>();
            Account account = new Account
            {
                Email = email
            };

            if ((await _coreDBContext.Accounts.FirstOrDefaultAsync(a => a.Email == email)) != null)
            {
                commandResult.IsSuccess = false;
                commandResult.Messages.Add("Пользователь с таким Email уже зарегестрирован");
            }
            else
            {
                var result = await _coreDBContext.Accounts.AddAsync(account);
                await _coreDBContext.SaveChangesAsync();
                commandResult.IsSuccess = true;
                commandResult.Content = "Пользователь успешно создан";
            }
            return commandResult;
        }

        public async Task<CommandResult<string>> AddSubscription(string email, string currencyName)
        {
            CommandResult<string> commandResult = new CommandResult<string>();
            Account account = await _coreDBContext.Accounts
                //.Include(a => a.Subscriptions)
                .FirstOrDefaultAsync(a => a.Email == email);

            if (account == null)
            {
                commandResult.IsSuccess = false;
                commandResult.Messages.Add($"Пользователь с emaail {email} не найден");
            }
            else
            {
                Currency currency = await _coreDBContext.Currencies.FirstOrDefaultAsync(c => c.Symbol == currencyName);
                if (currency == null)
                {
                    commandResult.IsSuccess = false;
                    commandResult.Messages.Add($"Не найдена валюта {currencyName}");
                }
                else
                {
                    account.Subscriptions ??= new List<Subscription>();
                    Subscription subscription = new Subscription
                    {
                        Currency = currency,
                        Predicate = "()=>EMA(4) > 0"
                    };
                    account.Subscriptions.Add(subscription);
                    await _coreDBContext.SaveChangesAsync();

                    commandResult.Messages.Add($"Подписка {subscription.Predicate} на {subscription.Currency} успешно добавлена");
                }
            }

            return commandResult;
        }
    }
}
