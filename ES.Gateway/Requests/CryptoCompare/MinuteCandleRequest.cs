using ES.Domain.Extensions.Attributes;

namespace ES.Gateway.Requests.CryptoCompare
{
    public class MinuteCandleRequest : BaseCryptoComparePairRequest
    {
        /// <summary>
        /// Лимит (max 2000)
        /// </summary>
        [QueryParam("limit")]
        public int Limit { get; set; } = 2000;

        /// <summary>
        /// Timestamp до
        /// </summary>
        [QueryParam("toTs")]
        public long ToTimeStamp { get; set; }

        public MinuteCandleRequest(BaseCryptoCompareRequest baseRequest = null) : base(baseRequest)
        {
        }
    }
}
