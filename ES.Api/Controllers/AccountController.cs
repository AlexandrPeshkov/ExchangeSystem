using System.Threading.Tasks;
using ES.Domain.ApiResults;
using ES.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace ES.API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Создать аккаунт
        /// </summary>
        /// <param name="email">Почта</param>
        /// <returns></returns>
        [HttpPost(nameof(CreateAccount))]
        public async Task<CommandResult<string>> CreateAccount(string email)
        {
            var commandResult = await _accountService.CreateAccount(email);
            return commandResult;
        }

        /// <summary>
        /// Добавить подписку
        /// </summary>
        /// <param name="email">Почта</param>
        /// <param name="currency">Валюта</param>
        /// <returns></returns>
        [HttpPost(nameof(AddSubscription))]
        public async Task<CommandResult<string>> AddSubscription(string email, string currency)
        {
            var commandResult = await _accountService.AddSubscription(email, currency);
            return commandResult;
        }
    }
}
