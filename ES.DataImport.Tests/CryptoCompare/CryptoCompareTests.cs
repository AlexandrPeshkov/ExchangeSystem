using System.Threading.Tasks;
using ES.DataImport.StockExchangeGateways;
using Microsoft.Extensions.DependencyInjection;
using TestInfrastructure;
using Xunit;

namespace ES.DataImport.Tests.CryptoCompare
{
    public class CryptoCompareTests : BaseTest
    {
        private readonly CryptoCompareGateway _gateway;

        public CryptoCompareTests()
        {
            _gateway = _services.GetService<CryptoCompareGateway>();
        }

        [Fact]
        public async Task ImportAllCoins()
        {
            var currencies = await _gateway.ImportAllCurrencies();

            Assert.NotEmpty(currencies);
            Assert.True(currencies.Exists(x => x.Symbol == "ETH"));
        }

        [Fact]
        public async Task ImportAllExchangeasAndPairs()
        {
            var exchangePairs = await _gateway.ImportAllExchangeAndPairs();

            Assert.NotEmpty(exchangePairs);
            Assert.True(exchangePairs.Exists(e => e.Pairs.Count > 0));
        }
    }
}
