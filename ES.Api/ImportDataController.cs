using System;
using System.Threading.Tasks;
using ES.API.Controllers;
using ES.DataImport.StockExchangeGateways;
using Microsoft.AspNetCore.Mvc;

namespace ES.API
{
    /// <summary>
    /// Управление импортом данных
    /// </summary>
    public class ImportDataController : BaseController
    {
        private readonly CryptoCompareGateway _cryptoCompareGateway;

        public ImportDataController(CryptoCompareGateway cryptoCompareGateway)
        {
            _cryptoCompareGateway = cryptoCompareGateway;
        }

        [HttpGet(nameof(LoadCurrencies))]
        public async Task<IActionResult> LoadCurrencies()
        {
            //var all = await _cryptoCompareGateway.ImportAllCurrencies();
            throw new Exception("Test");
            return Ok();
        }
    }
}
