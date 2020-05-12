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

        [HttpGet(nameof(LoadCurrencies))]
        public async Task<IActionResult> LoadCurrencies()
        {
            await _importMetaDataService.ImportAllCurrencies();
            await _importMetaDataService.ImportAllExchanges();
            await _importMetaDataService.ImportAllExchangePairs();
            return Ok();
        }
    }
}
