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
        private readonly ImportMetaDataService _importMetaDataService;

        public ImportDataController(ImportMetaDataService importMetaDataService)
        {
            _importMetaDataService = importMetaDataService;
        }

        [HttpGet(nameof(LoadCurrencies))]
        public async Task<IActionResult> LoadCurrencies()
        {
            await _importMetaDataService.ImportAllCurrencies();
            return Ok();
        }
    }
}
