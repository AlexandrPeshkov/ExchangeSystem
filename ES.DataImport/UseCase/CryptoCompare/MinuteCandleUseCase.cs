using System.Collections.Generic;
using AutoMapper;
using ES.DataImport.Requests.CryptoCompare;
using ES.DataImport.Responses.CryptoCompare;
using ES.DataImport.UseCase.CryptoCompare;
using ES.Domain.Configurations;
using ES.Domain.DTO.CryptoCompare;
using Microsoft.Extensions.Options;
using static ES.DataImport.Responses.CryptoCompare.MinuteCandleResponse;

namespace ES.Domain.UseCase.CryptoCompare
{
    public class MinuteCandleUseCase : BaseCryptoCompareUseCase<MinuteCandleRequest, MinuteCandleResponseData, List<CandleDTO>>
    {
        protected override string UriPath => "data/v2/histominute";
        public MinuteCandleUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper) : base(keys, mapper)
        {
        }

        protected override List<CandleDTO> MapResult(BaseCryptoCompareResponse<MinuteCandleResponseData> result)
        {
            return result?.Data?.Data;
        }
    }
}
