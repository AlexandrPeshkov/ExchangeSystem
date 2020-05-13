using System.Collections.Generic;
using ES.Domain.DTO.CryptoCompare;
using static ES.DataImport.Responses.CryptoCompare.MinuteCandleResponse;

namespace ES.DataImport.Responses.CryptoCompare
{
    public class MinuteCandleResponse : BaseCryptoCompareResponse<MinuteCandleResponseData>
    {
        public class MinuteCandleResponseData
        {
            public long TimeFrom { get; set; }

            public long TimeTo { get; set; }

            public List<CandleDTO> Data { get; set; }
        }
    }
}
