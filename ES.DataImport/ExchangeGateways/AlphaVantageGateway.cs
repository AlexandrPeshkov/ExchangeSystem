using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using ES.Domain.Configurations;
using ES.Domain.DTO.AphaVantage;
using ES.Domain.Entities;
using ES.Domain.Extensions;
using ES.Domain.Interfaces;
using ES.Domain.Interfaces.Requests;
using ES.Domain.Requests.AlphaVantage.GET;

namespace ES.DataImport.StockExchangeGateways
{
    /// <summary>
    /// https://www.alphavantage.co
    /// </summary>
    public class AlphaVantageGateway : BaseExchangeGateway
    {
        private const string _datatype = "csv"; //json

        private const string _outputsize = "full"; //compact

        protected override string HostName => "www.alphavantage.co/query?";

        protected override RequestLimitConfiguration Limits => new RequestLimitConfiguration
        {
            MinuteLimit = 5,
            DayLimit = 500,
        };

        private readonly CsvConfiguration _csvConfiguration;

        public AlphaVantageGateway(IMapper mapper) : base(mapper)
        {
            _csvConfiguration = new CsvConfiguration(CultureInfo.GetCultureInfo("en-US"))
            {
                HasHeaderRecord = true,
                Delimiter = ","
            };
        }

        //protected override HttpClient AddApiKey(ref HttpClient httpClient)
        //{
        //    _uriBuilder.Query = $"apikey={_tokens.AlphaVantage}";
        //    return httpClient;
        //}

        /// <summary>
        /// Импорт исторических данных
        /// </summary>
        /// <param name="symbol">Криптовалюта</param>
        /// <param name="market">Валюта</param>
        /// <param name="function">Периодичность</param>
        /// <returns></returns>
        //private async Task<IReadOnlyList<DigitalCurrency>> ImportTimeSeries(IExchangeRequest request)
        //{
        //    List<DigitalCurrency> digitalCurrencies = new List<DigitalCurrency>();

        //    string query = request?.ToQuery();
        //    _uriBuilder.Query = query;

        //    HttpClient httpClient = CreateHttpClient();
        //    Stream stream = await httpClient.GetStreamAsync(_uriBuilder.Uri);

        //    if (stream != null)
        //    {
        //        using (TextReader textReader = new StreamReader(stream))
        //        {
        //            using (var csvReader = new CsvReader(textReader, _csvConfiguration))
        //            {
        //                try
        //                {
        //                    csvReader?.Read();
        //                    csvReader?.ReadHeader();

        //                    while (csvReader?.Read() == true)
        //                    {
        //                        DigitalCurrency digitalCurrency = ReadCurrency(csvReader, _cultureInfo);
        //                        if (digitalCurrencies != null)
        //                        {
        //                            digitalCurrencies.Add(digitalCurrency);
        //                        }
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                }
        //            }
        //        }
        //    }

        //    return digitalCurrencies;
        //}

        ///// <summary>
        ///// Импорт исторических данных за 20 лет. 1 запись - 1 день
        ///// </summary>
        ///// <param name="symbol">Валютный символ</param>
        ///// <param name="market">Денежный символ</param>
        ///// <returns></returns>
        //public async Task<IReadOnlyList<IOhlcv>> ImportTimeSeriesDaily(string market, string symbol)
        //{
        //    var request = new DigitalCurrencyDailyRequest
        //    {
        //        Function = "DIGITAL_CURRENCY_DAILY",
        //        Datatype = _datatype,
        //        Outputsize = _outputsize,
        //        Market = market,
        //        Symbol = symbol
        //    };
        //    IReadOnlyList<DigitalCurrency> digitalCurrencies = await ImportTimeSeries(request);
        //    IReadOnlyList<Candle> candles = _mapper.Map<List<Candle>>(digitalCurrencies);
        //    return candles;
        //}

        /// <summary>
        /// Считать строку курса пары
        /// </summary>
        /// <returns></returns>
        private DigitalCurrency ReadCurrency(CsvReader csv, CultureInfo culture)
        {
            if (!csv.TryGetField(0, out string dateTimeStr)) return null;
            if (!DateTime.TryParse(dateTimeStr, culture.DateTimeFormat, DateTimeStyles.None, out DateTime dateTime)) return null;
            if (dateTime == default) return null;

            if (!csv.TryGetField(1, out decimal? openSymbol)) return null;
            if (!csv.TryGetField(2, out decimal? openUSD)) return null;
            if (!csv.TryGetField(3, out decimal? hightSymbol)) return null;
            if (!csv.TryGetField(4, out decimal? highUSD)) return null;
            if (!csv.TryGetField(5, out decimal? lowSymbol)) return null;
            if (!csv.TryGetField(6, out decimal? lowUSD)) return null;
            if (!csv.TryGetField(7, out decimal? closeSymbol)) return null;
            if (!csv.TryGetField(8, out decimal? closeUSD)) return null;
            if (!csv.TryGetField(9, out decimal? volume)) return null;
            if (!csv.TryGetField(10, out decimal? marketCapUSD)) return null;

            return new DigitalCurrency
            {
                DateTime = dateTime,
                OpenSymbol = openSymbol,
                OpenUSD = openUSD,
                HightSymbol = hightSymbol,
                HighUSD = highUSD,
                LowSymbol = lowSymbol,
                LowUSD = lowUSD,
                CloseSymbol = closeSymbol,
                CloseUSD = closeUSD,
                Volume = volume,
                MarketCapUSD = marketCapUSD
            };
        }
    }
}
