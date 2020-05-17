using ES.Domain.Extensions.Attributes;
using ES.Gateway.Interfaces.Requests;

namespace ES.Gateway.Requests
{
    public class BaseAlphaVantageRequest : IExchangeRequest
    {
        [QueryParam("apikey")]
        public string ApiKey { get; set; }

        [QueryParam("function")]
        public string Function { get; set; }
    }
}
