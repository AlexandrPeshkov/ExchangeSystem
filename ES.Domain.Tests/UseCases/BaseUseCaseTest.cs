using System;
using ES.Domain.Configurations;
using ES.Domain.Requests;
using Microsoft.Extensions.Options;
using TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ES.Domain.Tests.UseCases
{
    public abstract class BaseUseCaseTest : BaseTest
    {
        protected virtual bool SSL { get; } = true;

        private string Scheme => SSL ? "HTTPS" : "HTTPS";

        protected abstract string HostName { get; }

        protected readonly UriBuilder _uriBuilder;

        protected readonly EmptyRequest _emptyRequest;

        protected readonly StockExchangeKeys _keys;

        public BaseUseCaseTest()
        {
            _uriBuilder = new UriBuilder()
            {
                Scheme = Scheme,
                Host = HostName
            };

            _emptyRequest = new EmptyRequest();

            _keys = _services.GetService<IOptions<StockExchangeKeys>>()?.Value;
        }
    }
}
