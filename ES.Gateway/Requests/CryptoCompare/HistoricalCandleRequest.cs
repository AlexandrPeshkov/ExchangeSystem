using ES.Domain.Extensions.Attributes;

namespace ES.Gateway.Requests.CryptoCompare
{
    /// <summary>
    /// Модель запроса исторических данных
    /// </summary>
    public class HistoricalCandleRequest : BaseCryptoComparePairRequest
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

        public HistoricalCandleRequest(BaseCryptoCompareRequest baseRequest = null) : base(baseRequest)
        {
        }
    }
}
