using System.Linq;
using System.Threading.Tasks;
using ES.Domain;
using ES.Domain.Entities;
using ES.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestInfrastructure;
using Xunit;

namespace ES.Infrastructure.Tests
{
    public class ColdImportTests : BaseTest
    {
        private readonly ImportColdDataService _coldImportTests;

        public ColdImportTests()
        {
            _coldImportTests = _services.GetService<ImportColdDataService>();
        }

        [Fact]
        public async Task ImportAllCurrencies()
        {
            await _coldImportTests.ImportAllCurrencies();
            var context = Context();
            Currency currency = await context.Currencies.FirstOrDefaultAsync(x => x.Symbol == "ETH");
            Assert.NotNull(currency);
            Assert.Equal("ETH", currency.Symbol);
        }

        [Fact]
        public async Task ImportAllExchanges()
        {
            await _coldImportTests.ImportAllExchanges();
            var context = Context();
            Exchange exchange = await context.Exchanges.FirstOrDefaultAsync(x => x.Name == "DSX");
            Assert.NotNull(exchange);
            Assert.Equal("DSX", exchange.Name);
        }

        [Fact]
        public async Task ImportAllExchangePairs()
        {
            //using (var cntx = Context())
            //{
            //    Currency currency = new Currency
            //    {
            //        Symbol = "BTC"
            //    };
            //    cntx.Currencies.Add(currency);
            //    cntx.SaveChanges();
            //}

            //using (var cntx = Context())
            //{
            //    Currency currency = new Currency
            //    {
            //        Symbol = "ETH"
            //    };
            //    cntx.Currencies.Add(currency);
            //    cntx.SaveChanges();
            //}

            //using (var cntx = Context())
            //{
            //    Exchange exchange = new Exchange
            //    {
            //        Name = "DSX"
            //    };
            //    cntx.Exchanges.Add(exchange);
            //    cntx.SaveChanges();
            //}

            //using (var cntx = Context())
            //{
            //    Exchange exchange = new Exchange
            //    {
            //        Name = "ABCC"
            //    };
            //    cntx.Exchanges.Add(exchange);
            //    cntx.SaveChanges();
            //}

            //using (var cntx = Context())
            //{
            //    ExchangePair exchangePair = new ExchangePair
            //    {
            //        CurrencyFromId = cntx.Currencies.FirstOrDefault(c => c.Symbol == "BTC").Id,
            //        CurrencyToId = cntx.Currencies.FirstOrDefault(c => c.Symbol == "ETH").Id,
            //        ExchangeId = cntx.Exchanges.FirstOrDefault(c => c.Name == "DSX").Id,
            //    };
            //    cntx.Pairs.Add(exchangePair);
            //    cntx.SaveChanges();
            //}

            //using (var cntx = Context())
            //{
            //    ExchangePair exchangePair = new ExchangePair
            //    {
            //        CurrencyFromId = cntx.Currencies.FirstOrDefault(c => c.Symbol == "BTC").Id,
            //        CurrencyToId = cntx.Currencies.FirstOrDefault(c => c.Symbol == "ETH").Id,
            //        ExchangeId = cntx.Exchanges.FirstOrDefault(c => c.Name == "ABCC").Id,
            //    };
            //    cntx.Pairs.Add(exchangePair);
            //    cntx.SaveChanges();
            //}

            //using (var cntx = Context())
            //{
            //    ExchangePair exchangePair = new ExchangePair
            //    {
            //        CurrencyFromId = cntx.Currencies.FirstOrDefault(c => c.Symbol == "ETH").Id,
            //        CurrencyToId = cntx.Currencies.FirstOrDefault(c => c.Symbol == "BTC").Id,
            //        ExchangeId = cntx.Exchanges.FirstOrDefault(c => c.Name == "ABCC").Id,
            //    };
            //    cntx.Pairs.Add(exchangePair);
            //    cntx.SaveChanges();
            //}

            //using (var cntx = Context())
            //{
            //    var pairs = cntx.Pairs
            //        .Include(x => x.CurrencyFrom)
            //        .Include(x => x.CurrencyTo)
            //        .Include(x => x.Exchange)
            //        .ToList();
            //}

            await ImportAllCurrencies();
            await ImportAllExchanges();

            await _coldImportTests.ImportAllExchangePairs();

            CoreDBContext context = Context();
            ExchangePair pair = await context.Pairs
                    .Include(x => x.CurrencyFrom)
                    .Include(x => x.CurrencyTo)
                    .Include(x => x.Exchange)
                    .FirstOrDefaultAsync(x => x.Exchange.Name == "DSX");

            Assert.NotNull(pair);
            Assert.NotNull(pair.Exchange);
            Assert.NotNull(pair.CurrencyFrom);
            Assert.NotNull(pair.CurrencyTo);
            Assert.Equal("DSX", pair.Exchange.Name);
        }
    }
}
