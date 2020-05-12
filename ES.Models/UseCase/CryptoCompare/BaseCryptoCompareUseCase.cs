using System;
using System.Net.Http;
using System.Threading.Tasks;
using ES.Domain.Commands;
using ES.Domain.Configurations;
using Microsoft.Extensions.Options;
using ES.Domain.Extensions;
using ES.Domain.Interfaces.Requests;
using Newtonsoft.Json;
using ES.Domain.Responses.CryptoCompare;
using AutoMapper;

namespace ES.Domain.UseCase.CryptoCompare
{
    public abstract class BaseCryptoCompareUseCase<TRequest, TData, TView> : BaseUseCase<TRequest, BaseCryptoCompareResponse<TData>, TView>
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

        protected override HttpClient AddApiKey(ref HttpClient httpClient, UriBuilder uriBuilder)
        {
            uriBuilder.Query = $"apikey={_keys.CryptoCompare}";
            return httpClient;
        }
    }
}
