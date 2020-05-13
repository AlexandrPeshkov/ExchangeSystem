using ES.Domain.Extensions.Attributes;

namespace ES.DataImport.Requests.CryptoCompare
{
    public class MinuteCandleRequest : BaseCryptoCompareRequest
    {
        /// <summary>
        /// Преобразование 
        /// </summary>
        [QueryParam("tryConversion")]
        public bool TryConversion { get; set; } = false;

        /// <summary>
        /// Лимит (max 2000)
        /// </summary>
        [QueryParam("limit")]
        public int Limit { get; set; } = 2000;

        /// <summary>
        /// From символ
        /// </summary>
        [QueryParam("fsym")]
        public string FromSymbol { get; set; }

        /// <summary>
        /// To символ
        /// </summary>
        [QueryParam("tsym")]
        public string ToSymbol { get; set; }

        /// <summary>
        /// Биржа
        /// </summary>
        [QueryParam("e")]
        public string Exchange { get; set; }

        /// <summary>
        /// Timestamp до
        /// </summary>
        [QueryParam("toTs")]
        public long ToTimeStamp { get; set; }

        public MinuteCandleRequest(BaseCryptoCompareRequest baseRequest = null)
        {
            ApiKey = baseRequest?.ApiKey;
            ExtraParams = baseRequest?.ExtraParams;
        }
    }
}
