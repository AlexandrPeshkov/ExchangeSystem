using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ES.Domain.ApiRequests;
using ES.Domain.Interfaces.Gateways;
using Microsoft.Extensions.DependencyInjection;
using TestInfrastructure;
using Xunit;

namespace ES.Gateway.Tests.CryptoCompare
{
    public class CryptoCompareGatewayTests : BaseTest
    {
        private readonly ICryptoCompareGateway _gateway;

        public class TestDataGenerator
        {
            public static IEnumerable<object[]> GetCurrencyPriceCommands()
            {
                yield return new object[]
                {
                    new CurrencyPriceCommand
                    {
                        Exchange = "DSX",
                        FromSymbol = "BTC",
                        ToSymbols = new List<string>{"USD", "EUR"}
                    }
                };
            }
        }

        public CryptoCompareGatewayTests()
        {
            _gateway = _services.GetService<ICryptoCompareGateway>();
        }

        [Fact]
        public async Task GetAllCoins()
        {
            var result = await _gateway.AllCurrencies();

            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result?.Content);
            Assert.True(result.Content.Exists(x => x.Symbol == "ETH"));
        }

        [Fact]
        public async Task GetAllExchangesAndPairs()
        {
            var result = await _gateway.AllExchangePairs();

            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Content);
            Assert.True(result.Content.Exists(e => e.Pairs.Count > 0));
        }

        [Fact]
        public async Task GetAllExchanges()
        {
            var result = await _gateway.AllExchanges();

            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Content);
            Assert.True(result.Content.Exists(e => e.GradePoints > 0));
            Assert.True(result.Content.Exists(e => string.IsNullOrEmpty(e.AffiliateURL) == false));
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetCurrencyPriceCommands), MemberType = typeof(TestDataGenerator))]
        public async Task GetCurrencyPriceTest(CurrencyPriceCommand command)
        {
            var result = await _gateway.CurrencyPrice(command);

            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Content);
            Assert.True(result.Content.Keys.All(k => command.ToSymbols.Contains(k)));
        }
    }
}
