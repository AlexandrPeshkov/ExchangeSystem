using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ES.Domain.Configurations;
using ES.Domain.DTO.CryptoCompare;
using ES.Domain.Entities;
using ES.Domain.UseCase.CryptoCompare;

namespace ES.DataImport.StockExchangeGateways
{
    public class CryptoCompareGateway : BaseExchangeGateway
    {
        private readonly AllCurrenciesUseCase _allCoinsUseCase;

        private readonly AllExchangesAndPairsUseCase _allExchangesAndPairsUseCase;

        protected override string HostName => "min-api.cryptocompare.com";

        public CryptoCompareGateway(IMapper mapper,
            AllCurrenciesUseCase allCoinsUseCase,
            AllExchangesAndPairsUseCase allExchangesAndPairsUseCase) : base(mapper)
        {
            _allCoinsUseCase = allCoinsUseCase;
            _allExchangesAndPairsUseCase = allExchangesAndPairsUseCase;
        }

        protected override RequestLimitConfiguration Limits => new RequestLimitConfiguration
        {
            MonthLimit = 100000
        };

        public async Task<List<Currency>> ImportAllCurrencies()
        {
            List<Currency> currencies = await _allCoinsUseCase.Execute(_emptyRequest, _uriBuilder);
            return currencies;
        }

        public async Task<List<ExchangePairsDTO>> ImportAllExchangeAndPairs()
        {
            List<ExchangePairsDTO> exchangePairsDTOs = await _allExchangesAndPairsUseCase.Execute(_emptyRequest, _uriBuilder);
            return exchangePairsDTOs;
        }
    }
}
