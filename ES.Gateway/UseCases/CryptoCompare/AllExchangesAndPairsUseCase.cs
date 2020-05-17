using System.Collections.Generic;
using AutoMapper;
using ES.Gateway.Requests.CryptoCompare;
using ES.Gateway.Responses.CryptoCompare;
using ES.Domain.Configurations;
using ES.Domain.DTO.CryptoCompare;
using Microsoft.Extensions.Options;
using static ES.Gateway.Responses.CryptoCompare.AllExchangesAndTradingPairs;

namespace ES.Gateway.UseCase.CryptoCompare
{
    public class AllExchangesAndPairsUseCase : BaseDataCryptoCompareUseCase<
        BaseCryptoCompareRequest,
        BaseCryptoCompareResponse<ExchangesResponse>,
        ExchangesResponse,
        List<ExchangePairsDTO>>
    {
        protected override string UriPath => "data/v4/all/exchanges";

        public AllExchangesAndPairsUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper) : base(keys, mapper)
        {
        }

        protected override List<ExchangePairsDTO> MapResponse(BaseCryptoCompareResponse<ExchangesResponse> response)
        {
            List<ExchangePairsDTO> exchangePairs = new List<ExchangePairsDTO>();
            if (response?.IsSuccess == true && response.Data?.Exchanges?.Count > 0)
            {
                foreach (var exchange in response.Data.Exchanges)
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
            else
            {
                ErrorResult(response?.Message);
            }
            return exchangePairs;
        }
    }
}
