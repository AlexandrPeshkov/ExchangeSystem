using System.Threading.Tasks;
using ES.Domain.Interfaces.Gateways;
using Microsoft.Extensions.DependencyInjection;
using TestInfrastructure;
using Xunit;

namespace ES.Gateway.Tests.AlphaVantage
{
    public class AlphaVantageGatewayTests : BaseTest
    {
        private readonly IAlphaVantageGateway _alphaVantageGateway;
        public AlphaVantageGatewayTests()
        {
            _alphaVantageGateway = _services.GetService<IAlphaVantageGateway>();
        }

        [Theory]
        [InlineData("BTC")]
        [InlineData("ETH")]
        public async Task GetCryptoRating(string symbol)
        {
            var result = await _alphaVantageGateway.CryptoRating(symbol);

            Assert.NotNull(result);
            Assert.NotNull(result.Content);
            Assert.Equal(symbol, result.Content.Symbol);
        }

        [Theory]
        [InlineData("ABGTT3")]
        [InlineData(null)]
        public async Task ErrorCryptoRating(string symbol)
        {
            var result = await _alphaVantageGateway.CryptoRating(symbol);

            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.True(result.Messages.Exists(x => x == "No data"));
        }
    }
}
