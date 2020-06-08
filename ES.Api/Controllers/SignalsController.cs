using System.Threading.Tasks;
using ES.Domain.ApiCommands;
using ES.Domain.ApiResults;
using ES.Domain.DTO.CryptoCompare;
using ES.Domain.Interfaces.Gateways;
using Microsoft.AspNetCore.Mvc;

namespace ES.API.Controllers
{
    public class SignalsController : BaseController
    {
        private readonly ICryptoCompareGateway _cryptoCompareGateway;
        public SignalsController(ICryptoCompareGateway cryptoCompareGateway)
        {
            _cryptoCompareGateway = cryptoCompareGateway;
        }

        /// <summary>
        /// Последние торговые сигналы валюты
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(LatestSignals))]
        public async Task<CommandResult<SignalsDTO>> LatestSignals([FromQuery]SignalCommand command)
        {
            var result = await _cryptoCompareGateway.LatestSignals(command);
            return result;
        }
    }
}
