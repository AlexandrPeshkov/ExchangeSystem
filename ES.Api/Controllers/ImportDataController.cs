using System.Threading.Tasks;
using ES.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace ES.API.Controllers
{
    /// <summary>
    /// Управление импортом данных
    /// </summary>
    public class ImportDataController : BaseController
    {
        private readonly ImportColdDataService _importMetaDataService;

        public ImportDataController(ImportColdDataService importMetaDataService)
        {
            _importMetaDataService = importMetaDataService;
        }

        /// <summary>
        /// Загрузка списка бирж, валют и активных пар
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(LoadColdData))]
        public async Task<IActionResult> LoadColdData()
        {
            await _importMetaDataService.ImportAllCurrencies();
            await _importMetaDataService.ImportAllExchanges();
            await _importMetaDataService.ImportAllExchangePairs();
            return Ok();
        }
    }
}
