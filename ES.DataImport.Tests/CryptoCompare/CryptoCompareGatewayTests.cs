using System.Threading.Tasks;
using ES.DataImport.StockExchangeGateways;
using Microsoft.Extensions.DependencyInjection;
using TestInfrastructure;
using Xunit;

namespace ES.DataImport.Tests.CryptoCompare
{
    public class CryptoCompareGatewayTests : BaseTest
    {
        private readonly CryptoCompareGateway _gateway;

        public CryptoCompareGatewayTests()
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
        public async Task ImportAllExchangesAndPairs()
        {
            var exchangePairs = await _gateway.ImportAllExchangePairs();

            Assert.NotEmpty(exchangePairs);
            Assert.True(exchangePairs.Exists(e => e.Pairs.Count > 0));
        }

        [Fact]
        public async Task ImportAllExchanges()
        {
            var exchanges = await _gateway.ImportAllExchanges();

            Assert.NotEmpty(exchanges);
            Assert.True(exchanges.Exists(e => e.GradePoints > 0));
            Assert.True(exchanges.Exists(e => string.IsNullOrEmpty(e.AffiliateURL) == false));
        }
    }
}
