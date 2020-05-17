using System.Globalization;
using AutoMapper;
using CsvHelper.Configuration;
using ES.Gateway.Requests;
using ES.Domain.Configurations;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using ES.Gateway.UseCases.AlphaVantage;
using ES.Gateway.Requests.AlphaVantage;
using ES.Domain.ApiResults;
using ES.Gateway.Responses.AlphaVantage;
using ES.Domain.DTO.AphaVantage;
using ES.Domain.Interfaces.Gateways;

namespace ES.Gateway.StockExchangeGateways
{
    /// <summary>
    /// https://www.alphavantage.co
    /// </summary>
    public class AlphaVantageGateway : BaseExchangeGateway<BaseAlphaVantageRequest>, IAlphaVantageGateway
    {
        private const string _datatype = "csv"; //json

        private const string _outputsize = "full"; //compact

        protected override string HostName => "www.alphavantage.co";

        protected override RequestLimitConfiguration Limits => new RequestLimitConfiguration
        {
            MinuteLimit = 5,
            DayLimit = 500,
        };

        private readonly CsvConfiguration _csvConfiguration;

        private readonly CryptoRatingUseCase _cryptoRatingUseCase;

        public AlphaVantageGateway(IMapper mapper, IOptions<StockExchangeKeys> keys,
            CryptoRatingUseCase cryptoRatingUseCase)
            : base(mapper, keys)
        {
            _csvConfiguration = new CsvConfiguration(CultureInfo.GetCultureInfo("en-US"))
            {
                HasHeaderRecord = true,
                Delimiter = ","
            };

            _cryptoRatingUseCase = cryptoRatingUseCase;
        }

        protected override BaseAlphaVantageRequest DefaultRequest()
        {
            return new BaseAlphaVantageRequest
            {
                ApiKey = _keys.AlphaVantage
            };
        }

        public async Task<CommandResult<CryptoRatingDTO>> CryptoRating(string symbol)
        {
            var request = new CryptoRatingRequest(_defaultRequest)
            {
                Symbol = symbol
            };
            var result = await _cryptoRatingUseCase.Execute(request, _uriBuilder);
            return result;
        }
    }
}
