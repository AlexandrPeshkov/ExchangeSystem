using System;
using System.Linq;
using System.Threading.Tasks;
using ES.Gateway.Requests.CryptoCompare;
using ES.Domain.Constants;
using ES.Domain.Extensions;
using ES.Domain.UseCase.CryptoCompare;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ES.Domain.Tests.UseCases.CryptoCompare
{
    public class MinuteCandleUseCaseTests : BaseCryptoCompareUseCaseTest
    {
        private readonly MinuteCandleUseCase _useCase;

        public MinuteCandleUseCaseTests()
        {
            _useCase = _services.GetService<MinuteCandleUseCase>();
        }

        [Theory]
        [InlineData("BTC", "ETH", "DSX")]
        public async Task LoadMinuteDataset(string from, string to, string exchange)
        {
            var request = new HistoricalCandleRequest
            {
                ApiKey = _keys.CryptoCompare,
                ExtraParams = HttpConstants.CryptoCompareAppName,
                TryConversion = false,
                FromSymbol = from,
                ToSymbol = to,
                Exchange = exchange,
                //Aggregate = 1,
                Limit = 2000,
                ToTimeStamp = DateTime.Now.ToUnixTimeStamp(),
            };
            var response = await _useCase.Execute(request, _uriBuilder);

            Assert.NotNull(response);
//            Assert.NotEmpty(response.Content);
 //           Assert.True(response.Content.All(c => c.Time <= request.ToTimeStamp));
        }
    }
}
