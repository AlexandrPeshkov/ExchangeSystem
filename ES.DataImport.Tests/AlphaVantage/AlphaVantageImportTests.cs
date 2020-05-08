using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ES.DataImport.StockExchangeGateways;
using ES.Domain;
using ES.Domain.Configurations;
using ES.Domain.Interfaces;
using Microsoft.Extensions.Options;
using TestInfrastructure;
using Xunit;

namespace ES.DataImport.Tests
{
    public class AlphaVantageImportTests : BaseTest
    {
        private readonly AlphaVantageGateway _stockExchangeGateway;
        public AlphaVantageImportTests()
        {
            var key = _configurationRoot.GetSection(nameof(StockExchangeKeys)).GetChildren().FirstOrDefault(p => p.Key == "AlphaVantage").Value?.ToString();

            StockExchangeKeys stockExchangeTokens = new StockExchangeKeys
            {
                AlphaVantage = key
            };
            IOptions<StockExchangeKeys> options = Options.Create(stockExchangeTokens);
            _stockExchangeGateway = new AlphaVantageGateway(options, _mapper);
        }

        [Theory]
        [InlineData("BTC", "USD")]
        public async Task ImportTimeSeries(string symbol, string market)
        {
            IReadOnlyList<IOhlcv> timeSeries = await _stockExchangeGateway.ImportTimeSeriesDaily(symbol, market);

            Assert.NotEmpty(timeSeries);
            Assert.True(timeSeries.All(s => s.DateTime != default));
        }
    }
}
