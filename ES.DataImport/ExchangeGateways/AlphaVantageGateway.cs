using System.Globalization;
using AutoMapper;
using CsvHelper.Configuration;
using ES.DataImport.Requests;
using ES.Domain.Configurations;
using Microsoft.Extensions.Options;

namespace ES.DataImport.StockExchangeGateways
{
    /// <summary>
    /// https://www.alphavantage.co
    /// </summary>
    public class AlphaVantageGateway : BaseExchangeGateway<EmptyRequest>
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

        public AlphaVantageGateway(IMapper mapper, IOptions<StockExchangeKeys> keys) : base(mapper, keys)
        {
            _csvConfiguration = new CsvConfiguration(CultureInfo.GetCultureInfo("en-US"))
            {
                HasHeaderRecord = true,
                Delimiter = ","
            };
        }
    }
}
