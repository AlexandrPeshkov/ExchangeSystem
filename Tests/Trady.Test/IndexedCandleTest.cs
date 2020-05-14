using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ES.Analysis;
using ES.Analysis.Extension;
using ES.Domain.Interfaces;
using ES.Gateway.Csv;

namespace Trady.Test
{
    [TestClass]
    public class IndexedCandleTest
    {
        protected async Task<IEnumerable<IOhlcv>> ImportCandlesAsync()
        {
            var csvImporter = new CsvImporter("fb.csv", CultureInfo.GetCultureInfo("en-US"));
            return await csvImporter.ImportAsync("fb");
        }

        [TestMethod]
        public async Task TestIsBullish()
        {
            var candles = await ImportCandlesAsync();
            var indexedCandle = new IndexedCandle(candles, 1);
            var result = indexedCandle.IsBullish();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task TestIsBearish()
        {
            var candles = await ImportCandlesAsync();
            var indexedCandle = new IndexedCandle(candles, 1);
            var result = indexedCandle.IsBearish();
            Assert.IsTrue(result);
        }
    }
}
