using System.Threading.Tasks;
using ES.Domain;
using ES.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ES.API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly CoreDBContext _dbContext;
        public AccountController(CoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Создать аккаунт
        /// </summary>
        /// <param name="email">Почта</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAccount(string email)
        {
            Account account = new Account
            {
                Email = email
            };
            var result = await _dbContext.Accounts.AddAsync(account);
            return Created(email, result);
        }
    }
}
