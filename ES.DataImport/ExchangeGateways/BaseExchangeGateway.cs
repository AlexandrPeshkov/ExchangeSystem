using System;
using System.Globalization;
using System.Net.Http;
using AutoMapper;
using ES.Domain.Configurations;
using Microsoft.Extensions.Options;

namespace ES.DataImport.StockExchangeGateways
{
    public abstract class BaseExchangeGateway
    {
        private string Scheme => SSL ? "HTTPS" : "HTTPS";

        protected abstract string HostName { get; }

        protected readonly IMapper _mapper;

        protected readonly StockExchangeKeys _tokens;

        protected readonly CultureInfo _cultureInfo;

        protected readonly UriBuilder _uriBuilder;

        protected abstract RequestLimitConfiguration Limits { get; }

        protected virtual bool SSL { get; } = true;

        protected HttpClient _httpClient;

        public BaseExchangeGateway(IOptions<StockExchangeKeys> tokens, IMapper mapper)
        {
            _uriBuilder = new UriBuilder()
            {
                Scheme = Scheme,
                Host = HostName
            };

            _tokens = tokens?.Value;
            _cultureInfo = CultureInfo.GetCultureInfo("en-US");
            _mapper = mapper;
        }

        protected abstract HttpClient AddApiKey(ref HttpClient httpClient);

        protected virtual HttpClient CreateHttpClient()
        {
            return new HttpClient()
            {
                BaseAddress = _uriBuilder.Uri
            };
        }

        protected virtual HttpClient HttpClient()
        {
            _httpClient = _httpClient ?? CreateHttpClient();
            return _httpClient;
        }

    }
}
