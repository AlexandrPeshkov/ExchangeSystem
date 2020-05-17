using System;
using System.Net.Http;
using AutoMapper;
using ES.Domain.Configurations;
using ES.Gateway.Requests;
using ES.Gateway.UseCase;
using Microsoft.Extensions.Options;

namespace ES.Gateway.UseCases.AlphaVantage
{
    public abstract class BaseAlphaVantageUseCase<TRequest, TResponse, TView> : BaseGatewayUseCase<TRequest, TResponse, TView>
        where TRequest : BaseAlphaVantageRequest
        where TView : class
    {
        public BaseAlphaVantageUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper) : base(keys, mapper)
        {
        }

        protected override string UriPath => "query";

        protected abstract string Function { get; }

        protected override HttpMethod HttpMethod => new HttpMethod("GET");

        protected override HttpClient AddApiKey(ref HttpClient httpClient, UriBuilder uriBuilder) => httpClient;

        protected override void InitRequest(TRequest request)
        {
            if (request != null)
            {
                request.Function = Function;
            }
        }
    }
}
