using System.Globalization;
using System.Threading.Tasks;
using AutoMapper;
using CsvHelper.Configuration;
using ES.Domain.ApiCommands.TechnicalAnalysisCommands;
using ES.Domain.ApiResults;
using ES.Domain.Configurations;
using ES.Domain.DTO.AphaVantage;
using ES.Domain.Interfaces.Gateways;
using ES.Gateway.Requests;
using ES.Gateway.Requests.AlphaVantage;
using ES.Gateway.Requests.AlphaVantage.TechnicalAnalysis;
using ES.Gateway.UseCases.AlphaVantage;
using ES.Gateway.UseCases.AlphaVantage.Analysis;
using Microsoft.Extensions.Options;

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

        private readonly BollingerBandsUseCase _bollingerBandsUseCase;

        private readonly EmaUseCase _emaUseCase;

        private readonly SmaUseCase _smaUseCase;

        public AlphaVantageGateway(IMapper mapper, IOptions<StockExchangeKeys> keys,
            CryptoRatingUseCase cryptoRatingUseCase,
            BollingerBandsUseCase bollingerBandsUseCase,
            EmaUseCase emaUseCase,
            SmaUseCase smaUseCase
            )
            : base(mapper, keys)
        {
            _csvConfiguration = new CsvConfiguration(CultureInfo.GetCultureInfo("en-US"))
            {
                HasHeaderRecord = true,
                Delimiter = ","
            };

            _cryptoRatingUseCase = cryptoRatingUseCase;
            _bollingerBandsUseCase = bollingerBandsUseCase;
            _emaUseCase = emaUseCase;
            _smaUseCase = smaUseCase;
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

        private BaseAnalysisRequest AnalysisRequest(BaseAnalysisCommand command)
        {
            return new BaseAnalysisRequest(_defaultRequest)
            {
                Interval = command.Interval,
                SeriesType = command.SeriesType,
                Symbol = command.Symbol,
                TimePeriod = command.Period
            };
        }

        public async Task<CommandResult<AnalysisDTO>> BollingerBands(BaseAnalysisCommand command)
        {
            var request = AnalysisRequest(command);
            var result = await _bollingerBandsUseCase.Execute(request, _uriBuilder);
            return result;
        }

        public async Task<CommandResult<AnalysisDTO>> EMA(BaseAnalysisCommand command)
        {
            var request = AnalysisRequest(command);
            var result = await _emaUseCase.Execute(request, _uriBuilder);
            return result;
        }

        public async Task<CommandResult<AnalysisDTO>> SMA(BaseAnalysisCommand command)
        {
            var request = AnalysisRequest(command);
            var result = await _smaUseCase.Execute(request, _uriBuilder);
            return result;
        }
    }
}
