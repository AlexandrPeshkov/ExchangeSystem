using ES.Domain.Extensions.Attributes;

namespace ES.Gateway.Requests.CryptoCompare
{
    public class BaseCryptoComparePairRequest : BaseCryptoCompareRequest
    {
        /// <summary>
        /// Преобразование 
        /// </summary>
        [QueryParam("tryConversion")]
        public bool TryConversion { get; set; } = false;

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

        public BaseCryptoComparePairRequest(BaseCryptoCompareRequest baseRequest = null)
        {
            ApiKey = baseRequest?.ApiKey;
            ExtraParams = baseRequest?.ExtraParams;
        }
    }
}
