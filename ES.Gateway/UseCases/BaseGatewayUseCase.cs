using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using ES.Domain.ApiResults;
using ES.Domain.Configurations;
using ES.Domain.Extensions;
using ES.Gateway.Interfaces.Requests;
using ES.Gateway.Interfaces.UseCases;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ES.Gateway.UseCase
{
    public abstract class BaseGatewayUseCase<TRequest, TResponse, TView> : IGatewayUseCase<TRequest, TResponse, TView>
        where TRequest : IExchangeRequest
        where TView : class
    {
        protected HttpClient _httpClient;

        protected readonly StockExchangeKeys _keys;

        protected IMapper _mapper;

        protected readonly CommandResult<TView> _commandResult;

        protected abstract string UriPath { get; }

        protected abstract HttpMethod HttpMethod { get; }

        public BaseGatewayUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper)
        {
            _keys = keys?.Value;
            _mapper = mapper;
            _commandResult = new CommandResult<TView>();
        }

        public async Task<CommandResult<TView>> Execute(TRequest request, UriBuilder uriBuilder)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = uriBuilder.Uri
            };

            InitRequest(request);

            AddApiKey(ref _httpClient, uriBuilder);
            SetPath(UriPath, uriBuilder);

            uriBuilder.Query = request?.ToQuery();

            TResponse response = await SendRequest(uriBuilder);
            TView view = MapResponse(response);
            _commandResult.Content = view;
            return _commandResult;
        }

        protected virtual void InitRequest(TRequest request) { }

        protected async Task<TResponse> SendRequest(UriBuilder uriBuilder)
        {
            TResponse response = default;
            var message = new HttpRequestMessage(HttpMethod, uriBuilder.Uri);
            try
            {
                var httpResponse = await _httpClient.SendAsync(message);

                if (httpResponse?.IsSuccessStatusCode == true)
                {
                    var json = await httpResponse.Content.ReadAsStringAsync();
                    //var json = await ReadJsonBackup("CryptoCompare", "ExchangeAndPairs");
                    response = DeserializeResponse(json);
                }
                else
                {
                    ErrorResult("Failed to get data from the remote server");
                }
            }
            catch (Exception ex)
            {
                ErrorResult("Error connecting to remote server");
            }
            return response;
        }

        protected virtual TResponse DeserializeResponse(string json)
        {
            TResponse response = default;
            if (string.IsNullOrEmpty(json) == false)
            {
                try
                {
                    response = JsonConvert.DeserializeObject<TResponse>(json);
                }
                catch (JsonReaderException ex)
                {
                    ErrorResult("Incorrect response json mapping");
                }
                catch (Exception ex)
                {
                    ErrorResult();
                }
            }
            return response;
        }

        protected virtual TView MapResponse(TResponse response)
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
                    ErrorResult("Error map respose to view");
                }
                catch (AutoMapperMappingException ex)
                {
                    ErrorResult("Error mapping type");
                }
                catch (Exception ex)
                {
                    ErrorResult();
                }
            }
            return view;
        }

        protected abstract HttpClient AddApiKey(ref HttpClient httpClient, UriBuilder uriBuilder);

        protected void SetPath(string path, UriBuilder uriBuilder)
        {
            uriBuilder.Path = path;
        }

        protected CommandResult<TView> OkResult(TView content, string message = null)
        {
            _commandResult.Content = content;
            if (string.IsNullOrEmpty(message) == false)
            {
                _commandResult.Messages.Add(message);
            }
            _commandResult.IsSuccess = true;
            return _commandResult;
        }

        protected CommandResult<TView> ErrorResult(string message = null)
        {
            if (string.IsNullOrEmpty(message) == false)
            {
                _commandResult.Messages.Add(message);
            }
            _commandResult.IsSuccess = false;
            return _commandResult;
        }

        #region Utils

        protected async Task<string> ReadJsonBackup(string gatewayName, string requestName)
        {
            var jsonBackupPath = $"../../../../SourceData/{gatewayName}/{requestName}.json";
            var json = string.Empty;
            if (File.Exists(jsonBackupPath))
            {
                json = await File.ReadAllTextAsync(jsonBackupPath);
            }
            return json;
        }

        #endregion Utils
    }
}
