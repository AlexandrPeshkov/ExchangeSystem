using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ES.Domain.Constants;
using ES.Domain.Entities;
using ES.Domain.Requests.CryptoCompare;
using ES.Domain.UseCase.CryptoCompare;
using ES.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ES.Domain.Tests.UseCases.CryptoCompare
{
    public class MinutePairOHLCVUseCaseTests : BaseCryptoCompareUseCaseTest
    {
        private readonly MinutePairOHLCVUseCase _useCase;

        public MinutePairOHLCVUseCaseTests()
        {
            _useCase = _services.GetService<MinutePairOHLCVUseCase>();
        }

        [Theory]
        [InlineData("BTC", "ETH", "DSX")]
        public async Task Load10MinuteDataset(string from, string to, string exchange)
        {
            var request = new MinutePairOHLCVRequest
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
            List<Candle> response = await _useCase.Execute(request, _uriBuilder);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
            Assert.True(response.All(c => c.Time.ToUnixTimeStamp() <= request.ToTs));
        }
    }
}
