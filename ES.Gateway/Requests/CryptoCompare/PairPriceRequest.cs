using System.Collections.Generic;
using ES.Domain.Extensions.Attributes;

namespace ES.Gateway.Requests.CryptoCompare
{
    public class CurrencyPriceRequest : BaseCryptoCompareRequest
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
        [QueryParam("tsyms")]
        public List<string> ToSymbols { get; set; }

        /// <summary>
        /// Биржа
        /// </summary>
        [QueryParam("e")]
        public string Exchange { get; set; }

        /// <summary>
        /// Игнорировать ошибку отсутствия торгуемой пары на бирже
        /// </summary>
        [QueryParam("relaxedValidation")]
        public bool RelaxedValidation { get; set; } = false;
        public CurrencyPriceRequest(BaseCryptoCompareRequest request)
        {
            ApiKey = request?.ApiKey;
            ExtraParams = request?.ExtraParams;
        }
    }
}
