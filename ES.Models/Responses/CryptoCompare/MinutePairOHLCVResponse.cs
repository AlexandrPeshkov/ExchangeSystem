using System.Collections.Generic;
using ES.Domain.DTO.CryptoCompare;
using static ES.Domain.Responses.CryptoCompare.MinutePairOHLCVResponse;

namespace ES.Domain.Responses.CryptoCompare
{
    public class MinutePairOHLCVResponse : BaseCryptoCompareResponse<MinutePairOHLCVResponseData>
    {
        public class MinutePairOHLCVResponseData
        {
            public long TimeFrom { get; set; }

            public long TimeTo { get; set; }

            public List<CandleDTO> Data { get; set; }
        }
    }
}
