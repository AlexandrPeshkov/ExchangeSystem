using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ES.Domain.Configurations;
using ES.Domain.Constants;
using ES.Domain.DTO.CryptoCompare;
using ES.Domain.Entities;
using ES.Domain.Requests.CryptoCompare;
using ES.Domain.UseCase.CryptoCompare;
using Microsoft.Extensions.Options;

namespace ES.DataImport.StockExchangeGateways
{
    public class CryptoCompareGateway : BaseExchangeGateway<BaseCryptoCompareRequest>
    {
        private readonly AllCurrenciesUseCase _allCoinsUseCase;

        private readonly AllExchangesAndPairsUseCase _allExchangePairsUseCase;

        private readonly AllExchangesUseCase _allExchangesUseCase;

        protected override RequestLimitConfiguration Limits => new RequestLimitConfiguration
        {
            MonthLimit = 100000
        };

        protected override string HostName => "min-api.cryptocompare.com";

        public CryptoCompareGateway(IMapper mapper,
            IOptions<StockExchangeKeys> keys,
            AllCurrenciesUseCase allCoinsUseCase,
            AllExchangesAndPairsUseCase allExchangesAndPairsUseCase,
            AllExchangesUseCase allExchangesUseCase)
            : base(mapper, keys)
        {
            _allCoinsUseCase = allCoinsUseCase;
            _allExchangePairsUseCase = allExchangesAndPairsUseCase;
            _allExchangesUseCase = allExchangesUseCase;
        }

        protected override BaseCryptoCompareRequest DefaultRequest()
        {
            return new BaseCryptoCompareRequest
            {
                Api_Key = _keys?.CryptoCompare,
                ExtraParams = HttpConstants.CryptoCompareAppName
            };
        }

        public async Task<List<Currency>> ImportAllCurrencies()
        {
            List<Currency> currencies = await _allCoinsUseCase.Execute(_defaultRequest, _uriBuilder);
            return currencies;
        }

        public async Task<List<ExchangePairsDTO>> ImportAllExchangePairs()
        {
            List<ExchangePairsDTO> exchangePairsDTOs = await _allExchangePairsUseCase.Execute(_defaultRequest, _uriBuilder);
            return exchangePairsDTOs;
        }

        public async Task<List<ExchangeDTO>> ImportAllExchanges()
        {
            List<ExchangeDTO> exchanges = await _allExchangesUseCase.Execute(_defaultRequest, _uriBuilder);
            return exchanges;
        }
    }
}
