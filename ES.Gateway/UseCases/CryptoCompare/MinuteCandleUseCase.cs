using System.Collections.Generic;
using AutoMapper;
using ES.Gateway.Requests.CryptoCompare;
using ES.Gateway.Responses.CryptoCompare;
using ES.Gateway.UseCase.CryptoCompare;
using ES.Domain.Configurations;
using ES.Domain.DTO.CryptoCompare;
using Microsoft.Extensions.Options;
using static ES.Gateway.Responses.CryptoCompare.MinuteCandleResponse;

namespace ES.Domain.UseCase.CryptoCompare
{
    public class MinuteCandleUseCase : BaseDataCryptoCompareUseCase<
        MinuteCandleRequest,
        BaseCryptoCompareResponse<MinuteCandleResponseData>,
        MinuteCandleResponseData,
        List<CandleDTO>>
    {
        protected override string UriPath => "data/v2/histominute";
        public MinuteCandleUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper) : base(keys, mapper)
        {
        }

        protected override object ExtractData(MinuteCandleResponseData data)
        {
            return data?.Data;
        }
    }
}
