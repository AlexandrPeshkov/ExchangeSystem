using System;
using AutoMapper;
using ES.Domain.Configurations;
using ES.Gateway.Interfaces.Requests;
using ES.Gateway.Responses.CryptoCompare;
using Microsoft.Extensions.Options;

namespace ES.Gateway.UseCase.CryptoCompare
{
    public abstract class BaseDataCryptoCompareUseCase<TRequest, TResponse, TData, TView> : BaseCryptoCompareUseCase<TRequest, TResponse, TView>
        where TRequest : IExchangeRequest
        where TResponse : BaseCryptoCompareResponse<TData>
        where TView : class
    {
        public BaseDataCryptoCompareUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper) : base(keys, mapper) { }

        protected override TView MapResponse(TResponse response)
        {
            TView view = default;
            if (response != null && response.IsSuccess && response.Data != null)
            {
                object data = ExtractData(response.Data);
                try
                {
                    view = _mapper.Map<TView>(data);
                    if (view != null)
                    {
                        OkResult(view);
                    }
                    else
                    {
                        ErrorResult(response?.Message);
                    }
                }
                catch (Exception ex)
                {
                    ErrorResult(response?.Message);
                }
            }
            return view;
        }

        protected virtual object ExtractData(TData data)
        {
            return data;
        }
    }
}
