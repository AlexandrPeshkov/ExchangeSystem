using System.Threading.Tasks;
using ES.Domain.ApiCommands.TechnicalAnalysisCommands;
using ES.Domain.ApiResults;
using ES.Domain.DTO.AphaVantage;
using ES.Domain.Interfaces.Gateways;
using Microsoft.AspNetCore.Mvc;

namespace ES.API.Controllers
{
    /// <summary>
    /// Технический анализ
    /// </summary>
    public class TechnicalAnalysisController : BaseController
    {
        private readonly IAlphaVantageGateway _alphaVantageGateway;
        public TechnicalAnalysisController(IAlphaVantageGateway alphaVantageGateway)
        {
            _alphaVantageGateway = alphaVantageGateway;
        }

        /// <summary>
        /// Полосы Боллинжера
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet(nameof(BollingerBands))]
        public async Task<IActionResult> BollingerBands([FromQuery] BaseAnalysisCommand command)
        {
            var result = await _alphaVantageGateway.BollingerBands(command);
            return Ok(result?.Content?.Data);
        }


        /// <summary>
        /// EMA
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet(nameof(EMA))]
        public async Task<IActionResult> EMA([FromQuery] BaseAnalysisCommand command)
        {
            var result = await _alphaVantageGateway.EMA(command);
            return Ok(result?.Content?.Data);
        }

        /// <summary>
        /// SMA
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet(nameof(SMA))]
        public async Task<IActionResult> SMA([FromQuery] BaseAnalysisCommand command)
        {
            var result = await _alphaVantageGateway.SMA(command);
            return Ok(result?.Content?.Data);
        }

        /// <summary>
        /// VWAP
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet(nameof(VWAP))]
        public async Task<IActionResult> VWAP([FromQuery] BaseAnalysisCommand command)
        {
            var result = await _alphaVantageGateway.VWAP(command);
            return Ok(result?.Content?.Data);
        }

        /// <summary>
        /// MACD
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet(nameof(MACD))]
        public async Task<IActionResult> MACD([FromQuery] BaseAnalysisCommand command)
        {
            var result = await _alphaVantageGateway.MACD(command);
            return Ok(result?.Content?.Data);
        }

        /// <summary>
        /// STOCH
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet(nameof(STOCH))]
        public async Task<IActionResult> STOCH([FromQuery] BaseAnalysisCommand command)
        {
            var result = await _alphaVantageGateway.STOCH(command);
            return Ok(result?.Content?.Data);
        }
    }
}
