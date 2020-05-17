using System;
using System.Net.Http;
using AutoMapper;
using ES.Domain.Configurations;
using ES.Gateway.Interfaces.Requests;
using ES.Gateway.Responses.CryptoCompare;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;


namespace ES.Gateway.UseCase.CryptoCompare
{
    public abstract class BaseCryptoCompareUseCase<TRequest, TResponse, TView> : BaseGatewayUseCase<TRequest, TResponse, TView>
        where TRequest : IExchangeRequest
        where TView : class
    {
        public BaseCryptoCompareUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper) : base(keys, mapper) { }

        protected override HttpMethod HttpMethod => new HttpMethod("GET");

        protected override HttpClient AddApiKey(ref HttpClient httpClient, UriBuilder uriBuilder) => httpClient;

        protected override TResponse DeserializeResponse(string json)
        {
            TResponse response = base.DeserializeResponse(json);
            if (response == null || _commandResult.IsSuccess == false)
            {
                try
                {
                    var errorResponse = JsonConvert.DeserializeObject<BaseCryptoCompareResponse<object>>(json);
                    ErrorResult(errorResponse?.Message);
                }
                catch (Exception ex) { ErrorResult(); }
            }
            return response;
        }

        protected override TView MapResponse(TResponse response)
        {
            TView view = default;
            if (response != null && _commandResult.IsSuccess)
            {
                try
                {
                    view = _mapper.Map<TView>(response);
                    if (view != null)
                    {
                        OkResult(view);
                    }
                }
                catch (Exception ex)
                {
                    ErrorResult();
                }
            }
            return view;
        }
    }
}
