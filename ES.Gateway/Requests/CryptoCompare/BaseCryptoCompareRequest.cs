using ES.Domain.Extensions.Attributes;
using ES.Gateway.Interfaces.Requests;

namespace ES.Gateway.Requests.CryptoCompare
{
    public class BaseCryptoCompareRequest : IExchangeRequest
    {
        [QueryParam("api_key")]
        public string ApiKey { get; set; }

        [QueryParam("extraParams")]
        public string ExtraParams { get; set; }
    }
}
