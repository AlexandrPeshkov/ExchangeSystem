﻿using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ES.Gateway.AlphaVantage;
using ES.Gateway.Csv;
using ES.Gateway.Quandl;
using ES.Gateway.Stooq;
using ES.Gateway.Yahoo;
using ES.Domain.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StooqApi;
//using Trady.Importer.Google;
//using Trady.Importer.Quandl;

namespace Trady.Test
{
    [TestClass]
    public class ImporterTest
    {
        public ImporterTest()
        {
            // Test culture info
            CultureInfo.CurrentCulture = new CultureInfo("nl-nl");
        }

        // TODO: test later
        //[TestMethod]
        //public void ImportByGoogleFinance()
        //{
        //    var importer = new GoogleFinanceImporter();
        //    var candle = importer.ImportAsync("NASDAQ/AAPL", new DateTime(2017, 1, 3), new DateTime(2017, 1, 3)).Result.First();
        //    Assert.AreEqual(candle.Open, 115.8m);
        //    Assert.AreEqual(candle.High, 116.33m);
        //    Assert.AreEqual(candle.Low, 114.76m);
        //    Assert.AreEqual(candle.Close, 116.15m);
        //    Assert.AreEqual(candle.Volume, 28_781_865);
        //}

        // TODO: test later
        [TestMethod]
        public void ImportByQuandlYahoo()
        {
            //// Test account api key
            //const string ApiKey = "QP46TOX16WBTQ59Y";

            //var importer = new QuandlWikiImporter(ApiKey);
            //var candle = importer.ImportAsync("AAPL", new DateTime(2017, 1, 3), new DateTime(2017, 1, 3)).Result.First();
            //Assert.AreEqual(candle.Open, 115.8m);
            //Assert.AreEqual(candle.High, 116.33m);
            //Assert.AreEqual(candle.Low, 114.76m);
            //Assert.AreEqual(candle.Close, 116.15m);
            //Assert.AreEqual(candle.Volume, 28_781_865);
        }

        [TestMethod]
        public async Task ImportByAlphaVantage()
        {
            //// Test account api key
            //const string ApiKey = "QP46TOX16WBTQ59Y";
            //string symbol = "AAPL";
            //DateTime start = new DateTime(2020, 01, 01, 00, 00, 00);
            //DateTime end = DateTime.Now;
            //PeriodOption period = PeriodOption.Daily;

            //var importer = new AlphaVantageImporter(ApiKey, OutputSize.full);
            ////var candles = importer.ImportAsync("AAPL", new DateTime(2016, 9, 15), new DateTime(2018, 9, 15)).Result;

            //var candles = await importer.ImportAsync(symbol, start, end, period);

            //var candle = candles.FirstOrDefault(c => c.Time == new DateTime(2017, 1, 3));
            ////Assert.AreEqual(candle.Open, 115.8m);
            ////Assert.AreEqual(candle.High, 116.33m);
            ////Assert.AreEqual(candle.Low, 114.76m);
            ////Assert.AreEqual(candle.Close, 116.15m);
            ////Assert.AreEqual(candle.Volume, 28_781_865);
        }

        [TestMethod]
        public void ImportByAlphaVantage_Hourly()
        {
            //// Test account api key
            //const string ApiKey = "QP46TOX16WBTQ59Y";

            //var importer = new AlphaVantageImporter(ApiKey, OutputSize.full);
            //var candles = importer.ImportAsync("WBA", period: PeriodOption.Hourly).Result;
            //var candle = candles.FirstOrDefault();
            //Assert.IsNotNull(candle);
        }

        [TestMethod]
        public void ImportByYahoo()
        {
            //var importer = new YahooFinanceImporter();
            //var candle = importer.ImportAsync("^GSPC", new DateTime(2017, 1, 3), new DateTime(2017, 1, 4)).Result.First();  // Endtime stock history exclusive
            //Assert.AreEqual(candle.Open, 2251.570068m);
            //Assert.AreEqual(candle.High, 2263.879883m);
            //Assert.AreEqual(candle.Low, 2245.129883m);
            //Assert.AreEqual(candle.Close, 2257.830078m);
            //Assert.AreEqual(candle.Volume, 3_770_530_000);
        }

        [TestMethod]
        public void ImportByStooq()
        {
            //var importer = new StooqImporter();
            //var candle = importer.ImportAsync("^SPX", new DateTime(2017, 1, 3), new DateTime(2017, 1, 3)).Result.First();   // Endtime stock history inclusive
            //Assert.AreEqual(candle.Open, 2251.57m);
            //Assert.AreEqual(candle.High, 2263.88m);
            //Assert.AreEqual(candle.Low, 2245.13m);
            //Assert.AreEqual(candle.Close, 2257.83m);
            //Assert.AreEqual(candle.Volume, 644_640_832);
        }

        [TestMethod]
        public void ImportFromCsv()
        {
            var importer = new CsvImporter("fb.csv", CultureInfo.GetCultureInfo("en-US"));
            var candles = importer.ImportAsync("FB").Result;
            Assert.AreEqual(candles.Count, 1342);
            var firstIOhlcvData = candles.First();
            Assert.AreEqual(firstIOhlcvData.Time, new DateTime(2012, 5, 18));
        }

        [TestMethod]
        public void ImportFromCsvWithTime()
        {
            var config = new CsvImportConfiguration()
            {
                Culture = "en-US",
                Delimiter = ";",
                DateFormat = "yyyyMMdd HHmmss",
                HasHeaderRecord = false
            };
            var importer = new CsvImporter("EURUSD.csv", config);
            var candles = importer.ImportAsync("EURUSD").Result;
            Assert.AreEqual(744, candles.Count);
            var firstIOhlcvData = candles.First();
            Assert.AreEqual(new DateTime(2000, 5, 30, 17, 27, 00), firstIOhlcvData.Time);
        }
    }
}
