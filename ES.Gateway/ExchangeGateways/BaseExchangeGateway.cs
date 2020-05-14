using System;
using System.Globalization;
using AutoMapper;
using ES.Domain.Configurations;
using ES.Gateway.Interfaces.Requests;
using Microsoft.Extensions.Options;

namespace ES.Gateway.StockExchangeGateways
{
    public abstract class BaseExchangeGateway<TDefaultRequest>
        where TDefaultRequest : class, IExchangeRequest, new()
    {
        private string Scheme => SSL ? "HTTPS" : "HTTPS";

        protected abstract string HostName { get; }

        protected readonly IMapper _mapper;

        protected readonly StockExchangeKeys _keys;

        protected readonly CultureInfo _cultureInfo;

        protected readonly UriBuilder _uriBuilder;

        protected abstract RequestLimitConfiguration Limits { get; }

        protected virtual bool SSL { get; } = true;

        protected TDefaultRequest _defaultRequest;

        public BaseExchangeGateway(IMapper mapper, IOptions<StockExchangeKeys> keys)
        {
            _uriBuilder = new UriBuilder()
            {
                Scheme = Scheme,
                Host = HostName
            };

            _cultureInfo = CultureInfo.GetCultureInfo("en-US");
            _mapper = mapper;
            _keys = keys?.Value;
            _defaultRequest = DefaultRequest();
        }

        protected virtual TDefaultRequest DefaultRequest()
        {
            return new TDefaultRequest();
        }
    }
}
