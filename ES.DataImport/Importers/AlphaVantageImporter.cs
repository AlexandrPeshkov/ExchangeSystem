using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using ES.Domain.Interfaces;
using ES.Domain.Models;
using ES.Domain.Period;
using ES.Infrastructure.Interfaces;

namespace ES.DataImporter.AlphaVantage
{
    public class AlphaVantageImporter : IImporter
    {
        public AlphaVantageImporter(string apiKey, OutputSize outputSize = OutputSize.compact)
        {
            ApiKey = apiKey;
            OutputSize = outputSize;
        }

        protected string ApiKey { get; set; }
        public OutputSize OutputSize { get; set; }
        protected HttpClient Client { get; } = new HttpClient();

        /// <summary>
        /// Imports the async. endTime stock history inclusive
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="symbol">Symbol.</param>
        /// <param name="startTime">Start time.</param>
        /// <param name="endTime">End time.</param>
        /// <param name="period">Period.</param>
        /// <param name="token">Token.</param>
        public async Task<IReadOnlyList<IOhlcv>> ImportAsync(
            string symbol,
            DateTime? startTime = default,
            DateTime? endTime = default,
            PeriodOption period = PeriodOption.PerSecond,
            CancellationToken token = default(CancellationToken))
        {
            if (period == PeriodOption.PerSecond || period == PeriodOption.Per10Minute || period == PeriodOption.BiHourly)
            {
                throw new ArgumentException($"This importer does not support {period.ToString()}");
            }

            Client.BaseAddress = new Uri("https://www.alphavantage.co");
            var query = string.Empty;
            var function = "TIME_SERIES_DAILY";
            var format = "yyyy-MM-dd";
            switch (period)
            {
                case PeriodOption.PerMinute:
                    format = "yyyy-MM-dd HH:mm:ss";
                    function = "function=TIME_SERIES_INTRADAY&interval=1min";
                    break;
                case PeriodOption.Per5Minute:
                    format = "yyyy-MM-dd HH:mm:ss";
                    function = "function=TIME_SERIES_INTRADAY&interval=5min";
                    break;
                case PeriodOption.Per15Minute:
                    format = "yyyy-MM-dd HH:mm:ss";
                    function = "function=TIME_SERIES_INTRADAY&interval=15min";
                    break;
                case PeriodOption.Per30Minute:
                    format = "yyyy-MM-dd HH:mm:ss";
                    function = "function=TIME_SERIES_INTRADAY&interval=30min";
                    break;
                case PeriodOption.Hourly:
                    format = "yyyy-MM-dd HH:mm:ss";
                    function = "function=TIME_SERIES_INTRADAY&interval=60min";
                    break;
                case PeriodOption.Daily:
                    function = "function=TIME_SERIES_DAILY";
                    break;
                case PeriodOption.Weekly:
                    function = "function=TIME_SERIES_WEEKLY";
                    break;
                case PeriodOption.Monthly:
                    function = "function=TIME_SERIES_MONTHLY";
                    break;
                default:
                    break;
            }
            query = $"/query?{function}&symbol={symbol}&apikey={ApiKey}&outputsize={OutputSize.ToString()}&datatype=csv";
            var csvStream = await Client.GetStreamAsync(query);

            TextReader textReader = new StreamReader(csvStream);
            var culture = "en-US";
            var cultureInfo = new CultureInfo(culture);
            var candles = new List<IOhlcv>();

            using (var csvReader = new CsvReader(textReader, new CsvConfiguration(cultureInfo) { Delimiter = ",", HasHeaderRecord = true }))
            {
                bool isHeaderBypassed = false;
                while (csvReader.Read())
                {
                    // HasHeaderRecord is not working for CsvReader 6.0.2
                    if (!isHeaderBypassed)
                    {
                        isHeaderBypassed = true;
                        continue;
                    }

                    var date = string.IsNullOrWhiteSpace(format) ? csvReader.GetField<DateTime>(0) : DateTime.ParseExact(csvReader.GetField<string>(0), format, cultureInfo);
                    if ((!startTime.HasValue || date >= startTime) && (!endTime.HasValue || date <= endTime))
                    {
                        candles.Add(GetRecord(csvReader, format, cultureInfo));
                    }
                }
            }

            return candles.OrderBy(c => c.DateTime).ToList();
        }

        public IOhlcv GetRecord(CsvReader csv, string format, CultureInfo culture)
        {
            // By using GetField Methodo of the CSV Reader Culture Info set in the configuration is used
            return new Candle(
                string.IsNullOrWhiteSpace(format) ? csv.GetField<DateTime>(0) : DateTime.ParseExact(csv.GetField<string>(0), format, culture),
                csv.GetField<decimal>(1),
                csv.GetField<decimal>(2),
                csv.GetField<decimal>(3),
                csv.GetField<decimal>(4),
                csv.GetField<decimal>(5)
            );
        }


    }

    public enum OutputSize
    {
        compact = 0,
        full = 1
    }
}
