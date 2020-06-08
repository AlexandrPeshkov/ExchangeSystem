using ES.Domain.Extensions.Attributes;

namespace ES.Gateway.Requests.CryptoCompare
{
    public class SignalsRequest : BaseCryptoCompareRequest
    {
        [QueryParam("fsym")]
        public string Currecny { get; set; }

        public SignalsRequest(BaseCryptoCompareRequest request)
        {
            ApiKey = request?.ApiKey;
            ExtraParams = request?.ExtraParams;
        }
    }
}
