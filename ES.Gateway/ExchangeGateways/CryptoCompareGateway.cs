using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ES.Domain.ApiCommands;
using ES.Domain.ApiRequests;
using ES.Domain.ApiResults;
using ES.Domain.Configurations;
using ES.Domain.Constants;
using ES.Domain.DTO.AphaVantage.Enums;
using ES.Domain.DTO.CryptoCompare;
using ES.Domain.Entities;
using ES.Domain.Extensions;
using ES.Domain.Interfaces.Gateways;
using ES.Domain.UseCase.CryptoCompare.Historical;
using ES.Gateway.Requests.CryptoCompare;
using ES.Gateway.UseCase.CryptoCompare;
using ES.Gateway.UseCases.CryptoCompare;
using ES.Gateway.UseCases.CryptoCompare.Historical;
using Microsoft.Extensions.Options;
using static ES.Gateway.Responses.CryptoCompare.SignalsResponse;

namespace ES.Gateway.StockExchangeGateways
{
    public class CryptoCompareGateway : BaseExchangeGateway<BaseCryptoCompareRequest>, ICryptoCompareGateway
    {
        private readonly AllCurrenciesUseCase _allCoinsUseCase;

        private readonly AllExchangesAndPairsUseCase _allExchangePairsUseCase;

        private readonly AllExchangesUseCase _allExchangesUseCase;

        private readonly MinuteCandleUseCase _minuteCandleUseCase;

        private readonly HourlyCandleUseCase _hourlyCandleUseCase;

        private readonly DailyCandleUseCase _dailyCandleUseCase;

        private readonly PairPriceUseCase _pairPriceUseCase;

        private readonly TradingSignalsUseCase _tradingSignalsUseCase;

        protected override RequestLimitConfiguration Limits => new RequestLimitConfiguration
        {
            MonthLimit = 100000
        };

        protected override string HostName => "min-api.cryptocompare.com";

        public CryptoCompareGateway(IMapper mapper,
            IOptions<StockExchangeKeys> keys,
            AllCurrenciesUseCase allCoinsUseCase,
            AllExchangesAndPairsUseCase allExchangesAndPairsUseCase,
            AllExchangesUseCase allExchangesUseCase,
            PairPriceUseCase pairPriceUseCase,
            MinuteCandleUseCase minuteCandleUseCase,
            HourlyCandleUseCase hourlyCandleUseCase,
            DailyCandleUseCase dailyCandleUseCase,
            TradingSignalsUseCase tradingSignalsUseCase)
            : base(mapper, keys)
        {
            _allCoinsUseCase = allCoinsUseCase;
            _allExchangePairsUseCase = allExchangesAndPairsUseCase;
            _allExchangesUseCase = allExchangesUseCase;
            _pairPriceUseCase = pairPriceUseCase;
            _minuteCandleUseCase = minuteCandleUseCase;
            _hourlyCandleUseCase = hourlyCandleUseCase;
            _dailyCandleUseCase = dailyCandleUseCase;
            _tradingSignalsUseCase = tradingSignalsUseCase;
        }

        protected override BaseCryptoCompareRequest DefaultRequest()
        {
            return new BaseCryptoCompareRequest
            {
                ApiKey = _keys?.CryptoCompare,
                ExtraParams = HttpConstants.CryptoCompareAppName
            };
        }

        public async Task<CommandResult<List<Currency>>> AllCurrencies()
        {
            var response = await _allCoinsUseCase.Execute(_defaultRequest, _uriBuilder);
            return response;
        }

        public async Task<CommandResult<List<ExchangePairsDTO>>> AllExchangePairs()
        {
            var response = await _allExchangePairsUseCase.Execute(_defaultRequest, _uriBuilder);
            return response;
        }

        public async Task<CommandResult<List<ExchangeDTO>>> AllExchanges()
        {
            var response = await _allExchangesUseCase.Execute(_defaultRequest, _uriBuilder);
            return response;
        }

        public async Task<CommandResult<Dictionary<string, decimal>>> CurrencyPrice(CurrencyPriceCommand command)
        {
            CurrencyPriceRequest request = new CurrencyPriceRequest(_defaultRequest)
            {
                FromSymbol = command.FromSymbol,
                ToSymbols = command.ToSymbols,
                Exchange = command.Exchange,
            };
            var response = await _pairPriceUseCase.Execute(request, _uriBuilder);
            return response;
        }


        public async Task<CommandResult<List<CandleDTO>>> HistoricalCandle(HistoricalCandleCommand command)
        {
            var request = new HistoricalCandleRequest(_defaultRequest)
            {
                FromSymbol = command.FromSymbol,
                ToSymbol = command.ToSymbol,
                Exchange = command.Exchange,
                ToTimeStamp = command.BeforeTimestamp ?? DateTime.Now.ToUnixTimeStamp(),
                Limit = command.Limit
            };

            CommandResult<List<CandleDTO>> response = null;

            switch (command.Period)
            {
                case AlphaVantageHistoricalPeriod.OneMinute:
                    {
                        response = await _minuteCandleUseCase.Execute(request, _uriBuilder);
                        break;
                    }
                case AlphaVantageHistoricalPeriod.Hour:
                    {
                        response = await _hourlyCandleUseCase.Execute(request, _uriBuilder);
                        break;
                    }
                case AlphaVantageHistoricalPeriod.Day:
                    {
                        response = await _dailyCandleUseCase.Execute(request, _uriBuilder);
                        break;
                    }
            }
           
            return response;
        }

        public async Task<CommandResult<SignalsDTO>> LatestSignals(SignalCommand command)
        {
            var request = new SignalsRequest(_defaultRequest)
            {
                Currecny = command?.Currency
            };

            var response = await _tradingSignalsUseCase.Execute(request, _uriBuilder);
            return response;
        }
    }
}
