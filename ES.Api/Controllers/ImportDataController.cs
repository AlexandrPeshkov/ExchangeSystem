using System.Diagnostics;
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

        /// <summary>
        /// Загрузка свечей текущей пары на бирже
        /// </summary>
        /// <param name="fromSymbol">Символ валюты</param>
        /// <param name="toSymbol">Символ валюты</param>
        /// <param name="exchange">Биржа</param>
        /// <returns></returns>
        [HttpGet(nameof(LoadCandles))]
        public async Task<IActionResult> LoadCandles([FromQuery] string fromSymbol, string toSymbol, string exchange)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            await _importMetaDataService.ImportAllMinutePairCandles(fromSymbol, toSymbol, exchange);
            stopwatch.Stop();
            return Ok($"Total time = {stopwatch.ElapsedMilliseconds / 1000}");
        }
    }
}
