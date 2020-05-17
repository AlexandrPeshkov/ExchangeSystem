using System.Collections.Generic;
using System.Threading.Tasks;
using ES.Data.UseCases;
using ES.Domain.ApiCommands;
using ES.Domain.ApiResults;
using ES.Domain.DTO.AphaVantage;
using ES.Domain.Interfaces.Gateways;
using ES.Domain.ViewModels;
using ES.Gateway.UseCases.AlphaVantage;
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
        public CurrencyController(AllCurrencyUseCase allCurrencyUseCase, IAlphaVantageGateway alphaVantageGateway)
        {
            _allCurrencyUseCase = allCurrencyUseCase;
            _alphaVantageGateway = alphaVantageGateway;
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
            return result;
        }
    }
}
