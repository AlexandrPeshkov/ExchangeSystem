using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ES.DataImport.Requests.CryptoCompare;
using ES.DataImport.UseCase.CryptoCompare;
using ES.Domain.Configurations;
using ES.Domain.Constants;
using ES.Domain.DTO.CryptoCompare;
using ES.Domain.Entities;
using ES.Domain.Extensions;
using ES.Domain.UseCase.CryptoCompare;
using Microsoft.Extensions.Options;

namespace ES.DataImport.StockExchangeGateways
{
    public class CryptoCompareGateway : BaseExchangeGateway<BaseCryptoCompareRequest>
    {
        private readonly AllCurrenciesUseCase _allCoinsUseCase;

        private readonly AllExchangesAndPairsUseCase _allExchangePairsUseCase;

        private readonly AllExchangesUseCase _allExchangesUseCase;

        private readonly MinuteCandleUseCase _minuteCandleUseCase;

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
            MinuteCandleUseCase minuteCandleUseCase)
            : base(mapper, keys)
        {
            _allCoinsUseCase = allCoinsUseCase;
            _allExchangePairsUseCase = allExchangesAndPairsUseCase;
            _allExchangesUseCase = allExchangesUseCase;
            _minuteCandleUseCase = minuteCandleUseCase;
        }

        protected override BaseCryptoCompareRequest DefaultRequest()
        {
            return new BaseCryptoCompareRequest
            {
                ApiKey = _keys?.CryptoCompare,
                ExtraParams = HttpConstants.CryptoCompareAppName
            };
        }

        public async Task<List<Currency>> ImportAllCurrencies()
        {
            var response = await _allCoinsUseCase.Execute(_defaultRequest, _uriBuilder);
            return response;
        }

        public async Task<List<ExchangePairsDTO>> ImportAllExchangePairs()
        {
            var response = await _allExchangePairsUseCase.Execute(_defaultRequest, _uriBuilder);
            return response;
        }

        public async Task<List<ExchangeDTO>> ImportAllExchanges()
        {
            var response = await _allExchangesUseCase.Execute(_defaultRequest, _uriBuilder);
            return response;
        }

        public async Task<List<CandleDTO>> MinuteCandle(string fromSymbol, string toSymbol, string exchange, long? beforeTimestamp = null, int limit = 2000)
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
    }
}
