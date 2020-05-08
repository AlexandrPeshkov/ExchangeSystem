using System.Net.Http;
using AutoMapper;
using ES.Domain;
using ES.Domain.Configurations;
using ES.Domain.Constants;
using Microsoft.Extensions.Options;

namespace ES.DataImport.StockExchangeGateways
{
    public class CryptoApiGateway : BaseExchangeGateway
    {
        public CryptoApiGateway(IOptions<StockExchangeKeys> tokens, IMapper mapper, CoreDBContext context) : base(tokens, mapper, context)
        {
        }

        protected override string HostName => "api.cryptoapis.io/v1/";

        protected override RequestLimitConfiguration Limits => new RequestLimitConfiguration
        {
            SecondLimit = 3,
            DayLimit = 500,
        };

        protected override HttpClient CreateHttpClient()
        {
            var httpClient = base.CreateHttpClient();
            httpClient.DefaultRequestHeaders.Add(HttpConstants.ContentType, "application/json");
            return httpClient;
        }

        protected override HttpClient AddApiKey(ref HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add(HttpConstants.CryptoAPIKeyHeader, _tokens.CryptoAPI);
            return httpClient;
        }
    }
}
