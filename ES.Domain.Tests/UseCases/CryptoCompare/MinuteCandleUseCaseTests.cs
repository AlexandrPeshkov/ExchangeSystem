using System;
using System.Linq;
using System.Threading.Tasks;
using ES.DataImport.Requests.CryptoCompare;
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
            var request = new MinuteCandleRequest
            {
                Api_Key = _keys.CryptoCompare,
                ExtraParams = HttpConstants.CryptoCompareAppName,
                TryConversion = false,
                fsym = from,
                tsym = to,
                E = exchange,
                //Aggregate = 1,
                Limit = 2000,
                ToTs = DateTime.Now.ToUnixTimeStamp(),
            };
            var response = await _useCase.Execute(request, _uriBuilder);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
            Assert.True(response.All(c => c.Time <= request.ToTs));
        }
    }
}
