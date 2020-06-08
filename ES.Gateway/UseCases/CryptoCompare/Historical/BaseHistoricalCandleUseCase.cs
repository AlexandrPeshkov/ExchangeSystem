using System.Collections.Generic;
using AutoMapper;
using ES.Domain.Configurations;
using ES.Domain.DTO.CryptoCompare;
using ES.Gateway.Requests.CryptoCompare;
using ES.Gateway.Responses.CryptoCompare;
using ES.Gateway.UseCase.CryptoCompare;
using Microsoft.Extensions.Options;
using static ES.Gateway.Responses.CryptoCompare.MinuteCandleResponse;

namespace ES.Gateway.UseCases.CryptoCompare.Historical
{
    public abstract class BaseHistoricalCandleUseCase : BaseDataCryptoCompareUseCase<
        HistoricalCandleRequest,
        BaseCryptoCompareResponse<HistoricalCandleResponseData>,
        HistoricalCandleResponseData,
        List<CandleDTO>>
    {
        protected override string UriPath => $"data/v2/{Period}";

        protected virtual string Period { get; }

        public BaseHistoricalCandleUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper) : base(keys, mapper)
        {
        }

        protected override object ExtractData(HistoricalCandleResponseData data)
        {
            return data?.Data;
        }
    }
}
