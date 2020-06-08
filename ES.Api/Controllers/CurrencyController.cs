using System.Collections.Generic;
using System.Threading.Tasks;
using ES.Data.UseCases;
using ES.Domain.ApiCommands;
using ES.Domain.ApiResults;
using ES.Domain.DTO.AphaVantage;
using ES.Domain.DTO.CryptoCompare;
using ES.Domain.Interfaces.Gateways;
using ES.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ES.API.Controllers
{
    /// <summary>
    /// Контроллер валюты
    /// </summary>
    public class CurrencyController : BaseController
    {
        private readonly AllCurrencyUseCase _allCurrencyUseCase;

        private readonly IAlphaVantageGateway _alphaVantageGateway;

        private readonly ICryptoCompareGateway _cryptoCompareGateway;
        public CurrencyController(AllCurrencyUseCase allCurrencyUseCase, IAlphaVantageGateway alphaVantageGateway, ICryptoCompareGateway cryptoCompareGateway)
        {
            _allCurrencyUseCase = allCurrencyUseCase;
            _alphaVantageGateway = alphaVantageGateway;
            _cryptoCompareGateway = cryptoCompareGateway;
        }

        /// <summary>
        /// Список всех доступных валют
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(All))]
        public async Task<CommandResult<List<CurrencyView>>> All()
        {
            var result = await _allCurrencyUseCase.Execute(new EmptyCommand());
            return result;
        }


        /// <summary>
        /// Рейтинг криптовалюты
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(Rating))]
        public async Task<CommandResult<CryptoRatingDTO>> Rating(string symbol)
        {
            var result = await _alphaVantageGateway.CryptoRating(symbol);
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Испорт исторических данных
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet(nameof(HistoricalData))]
        public async Task<CommandResult<List<CandleDTO>>> HistoricalData([FromQuery]HistoricalCandleCommand command)
        {
            var result = await _cryptoCompareGateway.HistoricalCandle(command);
            return result;
        }
    }
}
