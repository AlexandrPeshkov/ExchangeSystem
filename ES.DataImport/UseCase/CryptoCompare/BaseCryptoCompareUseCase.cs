using System;
using System.Threading.Tasks;
using AutoMapper;
using ES.DataImport.Responses.CryptoCompare;
using ES.Domain.Configurations;
using ES.Domain.Extensions;
using ES.Domain.Interfaces.Requests;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ES.DataImport.UseCase.CryptoCompare
{
    public abstract class BaseCryptoCompareUseCase<TRequest, TData, TView> : BaseGatewayUseCase<TRequest, BaseCryptoCompareResponse<TData>, TView>
        where TRequest : IExchangeRequest
    {
        public BaseCryptoCompareUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper) : base(keys, mapper)
        {

        }

        public override async Task<TView> Execute(TRequest request, UriBuilder uriBuilder)
        {
            TView view = await base.Execute(request, uriBuilder);

            uriBuilder.Query = request?.ToQuery();

            var response = await _httpClient.GetAsync(uriBuilder.Uri);

            if (response?.IsSuccessStatusCode == true)
            {
                var json = await response.Content.ReadAsStringAsync();
                //var json = await ReadJsonBackup("CryptoCompare", "ExchangeAndPairs");

                if (string.IsNullOrEmpty(json) == false)
                {
                    var result = JsonConvert.DeserializeObject<BaseCryptoCompareResponse<TData>>(json);
                    if (result != null && result.IsSuccess)
                    {
                        view = MapResult(result);
                    }
                }
            }

            return view;
        }

        protected override TView MapResult(BaseCryptoCompareResponse<TData> result)
        {
            TView view = default;
            if (result != null && result.IsSuccess && result.Data != null)
            {
                view = _mapper.Map<TView>(result.Data);
            }
            return view;
        }
    }
}
