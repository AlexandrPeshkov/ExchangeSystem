using System;
using System.Globalization;
using AutoMapper;
using ES.Domain.Configurations;
using ES.Domain.Requests;

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

        protected readonly EmptyRequest _emptyRequest;

        public BaseExchangeGateway(IMapper mapper)
        {
            _uriBuilder = new UriBuilder()
            {
                Scheme = Scheme,
                Host = HostName
            };

            _cultureInfo = CultureInfo.GetCultureInfo("en-US");
            _mapper = mapper;

            _emptyRequest = new EmptyRequest();
        }
    }
}
