using System.Collections.Generic;
using AutoMapper;
using ES.DataImport.Requests.CryptoCompare;
using ES.DataImport.Responses.CryptoCompare;
using ES.Domain.Configurations;
using ES.Domain.DTO.CryptoCompare;
using Microsoft.Extensions.Options;
using static ES.DataImport.Responses.CryptoCompare.AllExchangesAndTradingPairs;

namespace ES.DataImport.UseCase.CryptoCompare
{
    public class AllExchangesAndPairsUseCase : BaseCryptoCompareUseCase<BaseCryptoCompareRequest, ExchangesResponse, List<ExchangePairsDTO>>
    {
        protected override string UriPath => "data/v4/all/exchanges";

        public AllExchangesAndPairsUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper) : base(keys, mapper)
        {
        }

        protected override List<ExchangePairsDTO> MapResult(BaseCryptoCompareResponse<ExchangesResponse> result)
        {
            List<ExchangePairsDTO> exchangePairs = new List<ExchangePairsDTO>();
            if (result?.IsSuccess == true)
            {
                foreach (var exchange in result.Data.Exchanges)
                {
                    ExchangePairsDTO exchangePairsDTO = new ExchangePairsDTO
                    {
                        Name = exchange.Key,
                        Pairs = new List<PairDTO>()
                    };

                    foreach (var pairs in exchange.Value.Pairs)
                    {
                        foreach (var currTo in pairs.Value.TSYMS.Keys)
                        {
                            var pairDTO = new PairDTO
                            {
                                CurrencyFrom = pairs.Key,
                                CurrencyTo = currTo
                            };

                            exchangePairsDTO.Pairs.Add(pairDTO);

                        }
                    }
                    exchangePairs.Add(exchangePairsDTO);
                }
            }
            return exchangePairs;
        }
    }
}
