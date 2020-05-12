using System.Threading.Tasks;
using ES.Domain.Entities;
using ES.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestInfrastructure;
using Xunit;

namespace ES.Infrastructure.Tests
{
    public class DBImportServiceTests : BaseTest
    {
        private readonly ImportMetaDataService _importMetaDataService;

        public DBImportServiceTests()
        {
            _importMetaDataService = _services.GetService<ImportMetaDataService>();
        }

        [Fact]
        public async Task ImportAllCurrencies()
        {
            await _importMetaDataService.ImportAllCurrencies();
            var context = Context();
            Currency currency = await context.Currencies.FirstOrDefaultAsync(x => x.Symbol == "ETH");
            Assert.NotNull(currency);
            Assert.Equal("ETH", currency.Symbol);
        }
    }
}
