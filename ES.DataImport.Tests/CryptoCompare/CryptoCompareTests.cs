using System.Linq;
using System.Threading.Tasks;
using ES.DataImport.StockExchangeGateways;
using ES.Domain.Configurations;
using Microsoft.Extensions.Options;
using TestInfrastructure;
using Xunit;

namespace ES.DataImport.Tests.CryptoCompare
{
    public class CryptoCompareTests : BaseTest
    {
        private readonly CryptoCompareGateway _gateway;

        public CryptoCompareTests()
        {
            var section = _configurationRoot?.GetSection(nameof(StockExchangeKeys));
            var key = section?.GetChildren()?.FirstOrDefault(p => p?.Key == "CryptoCompare")?.Value?.ToString();

            StockExchangeKeys stockExchangeTokens = new StockExchangeKeys
            {
                CryptoCompare = key
            };
            IOptions<StockExchangeKeys> options = Options.Create(stockExchangeTokens);
            _gateway = new CryptoCompareGateway(options, _mapper);
        }

        [Fact]
        public async Task ImportAllCoins()
        {
            var currencies = await _gateway.ImportAllCurrencies();

            Assert.NotEmpty(currencies);
            Assert.True(currencies.Exists(x => x.Symbol == "ETH"));
        }
    }
}
