using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using ES.Domain.Configurations;
using ES.Domain.Interfaces.Requests;
using ES.Domain.Interfaces.UseCases;
using Microsoft.Extensions.Options;

namespace ES.Domain.Commands
{
    public abstract class BaseUseCase<TRequest, TResponse, TView> : IUseCase<TRequest, TResponse, TView>
        where TResponse : class
        where TRequest : IExchangeRequest
    {
        protected HttpClient _httpClient;

        protected readonly StockExchangeKeys _keys;

        protected IMapper _mapper;

        protected abstract string UriPath { get; }

        public BaseUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper)
        {
            _keys = keys?.Value;
            _mapper = mapper;
        }

        public virtual Task<TView> Execute(TRequest request, UriBuilder uriBuilder)
        {
            TView response = default;
            _httpClient = new HttpClient
            {
                BaseAddress = uriBuilder.Uri
            };
            AddApiKey(ref _httpClient, uriBuilder);
            AddPath(UriPath, uriBuilder);
            return Task.FromResult(response);
        }

        protected abstract HttpClient AddApiKey(ref HttpClient httpClient, UriBuilder uriBuilder);

        protected virtual void AddPath(string path, UriBuilder uriBuilder)
        {
            uriBuilder.Path = path;
        }

        protected virtual TView MapResult(TResponse response)
        {
            TView view = default;
            if (response != null)
            {
                view = _mapper.Map<TView>(response);
            }
            return view;
        }

        #region Utils

        protected async Task<string> ReadJsonBackup(string gatewayName, string requestName)
        {
            var a = AppContext.BaseDirectory;
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
