using System.Collections.Generic;
using ES.Domain.DTO.CryptoCompare;
using static ES.Gateway.Responses.CryptoCompare.MinuteCandleResponse;

namespace ES.Gateway.Responses.CryptoCompare
{
    public class MinuteCandleResponse : BaseCryptoCompareResponse<HistoricalCandleResponseData>
    {
        public class HistoricalCandleResponseData
        {
            public long TimeFrom { get; set; }

            public long TimeTo { get; set; }

            public List<CandleDTO> Data { get; set; }
        }
    }
}
