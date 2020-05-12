using System;
using System.Linq;
using System.Threading.Tasks;
using ES.DataImport.StockExchangeGateways;
using ES.Domain;
using ES.Domain.Configurations;
using ES.Domain.Entities;
using ES.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TestInfrastructure;
using Xunit;

namespace ES.Infrastructure.Tests
{
    public class DBImportServiceTests : BaseTest
    {
        private readonly ImportMetaDataService _importMetaDataService;

        public DBImportServiceTests()
        {
            var section = _configurationRoot?.GetSection(nameof(StockExchangeKeys));
            var key = section?.GetChildren()?.FirstOrDefault(p => p?.Key == "CryptoCompare")?.Value?.ToString();

            StockExchangeKeys stockExchangeTokens = new StockExchangeKeys
            {
                CryptoCompare = key
            };
            IOptions<StockExchangeKeys> options = Options.Create(stockExchangeTokens);
            CoreDBContext context = Context();
            CryptoCompareGateway gateway = new CryptoCompareGateway(options, _mapper);

            _importMetaDataService = new ImportMetaDataService(gateway, context);
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
