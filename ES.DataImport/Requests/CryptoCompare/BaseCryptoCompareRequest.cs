using ES.Domain.Extensions.Attributes;
using ES.Domain.Interfaces.Requests;

namespace ES.DataImport.Requests.CryptoCompare
{
    public class BaseCryptoCompareRequest : IExchangeRequest
    {
        [QueryParam("api_key")]
        public string ApiKey { get; set; }

        [QueryParam("extraParams")]
        public string ExtraParams { get; set; }
    }
}
