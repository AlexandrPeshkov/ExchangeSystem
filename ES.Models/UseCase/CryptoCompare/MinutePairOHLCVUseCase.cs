using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ES.Domain.Configurations;
using ES.Domain.Entities;
using ES.Domain.Requests.CryptoCompare;
using ES.Domain.Responses.CryptoCompare;
using Microsoft.Extensions.Options;
using static ES.Domain.Responses.CryptoCompare.MinutePairOHLCVResponse;

namespace ES.Domain.UseCase.CryptoCompare
{
    public class MinutePairOHLCVUseCase : BaseCryptoCompareUseCase<MinutePairOHLCVRequest, MinutePairOHLCVResponseData, List<Candle>>
    {
        protected override string UriPath => "data/v2/histominute";
        public MinutePairOHLCVUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper) : base(keys, mapper)
        {
        }

        protected override List<Candle> MapResult(BaseCryptoCompareResponse<MinutePairOHLCVResponseData> result)
        {
            List<Candle> candles = new List<Candle>();

            if (result?.IsSuccess == true && result.Data?.Data?.Any() == true)
            {
                candles = _mapper.Map<List<Candle>>(result.Data.Data);
            }
            return candles;
        }


    }
}
