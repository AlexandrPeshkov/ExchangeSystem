using System.Collections.Generic;
using AutoMapper;
using ES.Domain.Configurations;
using ES.Domain.DTO.CryptoCompare;
using ES.Domain.Requests;
using ES.Domain.Responses.CryptoCompare;
using Microsoft.Extensions.Options;
using static ES.Domain.Responses.CryptoCompare.AllExchangesAndTradingPairs;

namespace ES.Domain.UseCase.CryptoCompare
{
    public class AllExchangesAndPairsUseCase : BaseCryptoCompareUseCase<EmptyRequest, ExchangesResponse, List<ExchangePairsDTO>>
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
