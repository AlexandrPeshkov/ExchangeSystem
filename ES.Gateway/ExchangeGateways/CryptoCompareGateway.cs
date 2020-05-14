using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ES.Gateway.Requests.CryptoCompare;
using ES.Gateway.UseCase.CryptoCompare;
using ES.Domain.ApiRequests;
using ES.Domain.ApiResults;
using ES.Domain.Configurations;
using ES.Domain.Constants;
using ES.Domain.DTO.CryptoCompare;
using ES.Domain.Entities;
using ES.Domain.Extensions;
using ES.Domain.UseCase.CryptoCompare;
using Microsoft.Extensions.Options;
using ES.Domain.Interfaces.Gateways;

namespace ES.Gateway.StockExchangeGateways
{
    public class CryptoCompareGateway : BaseExchangeGateway<BaseCryptoCompareRequest>, ICryptoCompareGateway
    {
        private readonly AllCurrenciesUseCase _allCoinsUseCase;

        private readonly AllExchangesAndPairsUseCase _allExchangePairsUseCase;

        private readonly AllExchangesUseCase _allExchangesUseCase;

        private readonly MinuteCandleUseCase _minuteCandleUseCase;

        private readonly PairPriceUseCase _pairPriceUseCase;

        protected override RequestLimitConfiguration Limits => new RequestLimitConfiguration
        {
            MonthLimit = 100000
        };

        protected override string HostName => "min-api.cryptocompare.com";

        public CryptoCompareGateway(IMapper mapper,
            IOptions<StockExchangeKeys> keys,
            AllCurrenciesUseCase allCoinsUseCase,
            AllExchangesAndPairsUseCase allExchangesAndPairsUseCase,
            AllExchangesUseCase allExchangesUseCase,
            MinuteCandleUseCase minuteCandleUseCase,
            PairPriceUseCase pairPriceUseCase)
            : base(mapper, keys)
        {
            _allCoinsUseCase = allCoinsUseCase;
            _allExchangePairsUseCase = allExchangesAndPairsUseCase;
            _allExchangesUseCase = allExchangesUseCase;
            _minuteCandleUseCase = minuteCandleUseCase;
            _pairPriceUseCase = pairPriceUseCase;
        }

        protected override BaseCryptoCompareRequest DefaultRequest()
        {
            return new BaseCryptoCompareRequest
            {
                ApiKey = _keys?.CryptoCompare,
                ExtraParams = HttpConstants.CryptoCompareAppName
            };
        }

        public async Task<CommandResult<List<Currency>>> AllCurrencies()
        {
            var response = await _allCoinsUseCase.Execute(_defaultRequest, _uriBuilder);
            return response;
        }

        public async Task<CommandResult<List<ExchangePairsDTO>>> AllExchangePairs()
        {
            var response = await _allExchangePairsUseCase.Execute(_defaultRequest, _uriBuilder);
            return response;
        }

        public async Task<CommandResult<List<ExchangeDTO>>> AllExchanges()
        {
            var response = await _allExchangesUseCase.Execute(_defaultRequest, _uriBuilder);
            return response;
        }

        public async Task<CommandResult<List<CandleDTO>>> MinuteCandle(string fromSymbol, string toSymbol, string exchange, long? beforeTimestamp = null, int limit = 2000)
        {
            MinuteCandleRequest request = new MinuteCandleRequest(_defaultRequest)
            {
                FromSymbol = fromSymbol,
                ToSymbol = toSymbol,
                Exchange = exchange,
                ToTimeStamp = beforeTimestamp ?? DateTime.Now.ToUnixTimeStamp(),
                Limit = limit
            };
            var response = await _minuteCandleUseCase.Execute(request, _uriBuilder);
            return response;
        }

        public async Task<CommandResult<Dictionary<string, decimal>>> CurrencyPrice(CurrencyPriceCommand command)
        {
            CurrencyPriceRequest request = new CurrencyPriceRequest(_defaultRequest)
            {
                FromSymbol = command.FromSymbol,
                ToSymbols = command.ToSymbols,
                Exchange = command.Exchange,
            };
            var response = await _pairPriceUseCase.Execute(request, _uriBuilder);
            return response;
        }
    }
}
