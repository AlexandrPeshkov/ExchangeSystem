using System.Threading.Tasks;
using ES.Domain.ApiResults;
using Microsoft.AspNetCore.Mvc;

namespace ES.API.Controllers
{
    /// <summary>
    /// Контроллер валюты
    /// </summary>
    public class CurrencyController : BaseController
    {
        public CurrencyController()
        {

        }

        /// <summary>
        /// Список всех доступных валют
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(All))]
        public async Task<CommandResult<object>> All()
        {
            return default;
        }
    }
}
