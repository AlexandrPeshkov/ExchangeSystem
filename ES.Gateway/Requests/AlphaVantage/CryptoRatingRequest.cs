using ES.Domain.Extensions.Attributes;

namespace ES.Gateway.Requests.AlphaVantage
{
    public class CryptoRatingRequest : BaseAlphaVantageRequest
    {
        [QueryParam("symbol")]
        public string Symbol { get; set; }

        public CryptoRatingRequest(BaseAlphaVantageRequest baseRequest)
        {
            ApiKey = baseRequest?.ApiKey;
        }
    }
}
