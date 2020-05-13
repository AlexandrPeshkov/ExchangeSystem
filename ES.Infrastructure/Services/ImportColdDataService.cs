using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ES.DataImport.StockExchangeGateways;
using ES.Domain;
using ES.Domain.DTO.CryptoCompare;
using ES.Domain.Entities;
using ES.Domain.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ES.Infrastructure.Services
{
    /// <summary>
    /// Импортирование данных в хранилище
    /// </summary>
    public class ImportColdDataService
    {
        private readonly IMapper _mapper;

        private readonly CryptoCompareGateway _cryptoCompareGateway;

        private readonly CoreDBContext _coreDBContext;

        private readonly Serilog.ILogger _logger;


        public ImportColdDataService(CryptoCompareGateway cryptoCompareGateway,
            CoreDBContext coreDBContext,
            IMapper mapper,
            Serilog.ILogger logger
            )
        {
            _cryptoCompareGateway = cryptoCompareGateway;
            _coreDBContext = coreDBContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task ImportAllCurrencies()
        {
            List<Currency> currencies = await _cryptoCompareGateway.ImportAllCurrencies();
            if (currencies?.Any() == true)
            {
                await _coreDBContext.AddRangeAsync(currencies);
                await _coreDBContext.SaveChangesAsync();
            }
        }

        public async Task ImportAllExchanges()
        {
            List<ExchangeDTO> exchangeDTOs = await _cryptoCompareGateway.ImportAllExchanges();
            if (exchangeDTOs?.Any() == true)
            {
                List<Exchange> exchanges = _mapper.Map<List<Exchange>>(exchangeDTOs);
                if (exchanges?.Any() == true)
                {
                    await _coreDBContext.AddRangeAsync(exchanges);
                    await _coreDBContext.SaveChangesAsync();
                }
            }
        }

        public async Task ImportAllExchangePairs()
        {
            List<ExchangePairsDTO> exchangePairsDTOs = await _cryptoCompareGateway.ImportAllExchangePairs();
            List<Exchange> exchanges = _coreDBContext.Exchanges.AsEnumerable().Where(e => exchangePairsDTOs?.Exists(d => d.Name == e.Name) == true).ToList();
            List<Currency> currencies = await _coreDBContext.Currencies.ToListAsync();

            List<ExchangePair> exchangePairs = new List<ExchangePair>();
            if (exchangePairsDTOs?.Any() == true)
            {
                foreach (var exchangeDto in exchangePairsDTOs)
                {
                    Exchange exchange = exchanges?.FirstOrDefault(e => e.Name == exchangeDto.Name);
                    if (exchange != null)
                    {
                        foreach (var pair in exchangeDto.Pairs)
                        {
                            Currency currencyFrom = currencies?.FirstOrDefault(c => c.Symbol == pair.CurrencyFrom);
                            Currency currencyTo = currencies?.FirstOrDefault(c => c.Symbol == pair.CurrencyTo);
                            if (currencyFrom != null && currencyTo != null)
                            {
                                var exchangePair = new ExchangePair
                                {
                                    ExchangeId = exchange.Id,
                                    CurrencyFromId = currencyFrom.Id,
                                    CurrencyToId = currencyTo.Id,
                                };
                                exchangePairs.Add(exchangePair);
                            }
                        }
                    }
                }
            }

            if (exchangePairs.Any())
            {
                await _coreDBContext.AddRangeAsync(exchangePairs);
                await _coreDBContext.SaveChangesAsync();
            }
        }

        public async Task ImportAllMinutePairCandles(string fromSymbol, string toSymbol, string exchange)
        {
            if (string.IsNullOrEmpty(fromSymbol) == false && string.IsNullOrEmpty(toSymbol) == false && string.IsNullOrEmpty(exchange) == false)
            {
                ExchangePair pair = await _coreDBContext.Pairs.FirstOrDefaultAsync(x =>
                x.CurrencyTo.Symbol == toSymbol
                && x.CurrencyFrom.Symbol == fromSymbol
                && x.Exchange.Name == exchange);

                long currentTimestamp = DateTime.Now.ToUnixTimeStamp();
                bool isActive = true;
                long interval = 60;
                if (pair != null)
                {
                    while (isActive)
                    {
                        List<CandleDTO> candleDTOs = await _cryptoCompareGateway.MinuteCandle(fromSymbol, toSymbol, exchange, currentTimestamp);

                        if (candleDTOs.Count > 0)
                        {
                            List<Candle> candles = new List<Candle>(candleDTOs.Count);
                            foreach (var candleDTO in candleDTOs)
                            {
                                Candle candle = _mapper.Map<Candle>(candleDTO);
                                candle.PairId = pair.Id;
                                candles.Add(candle);
                                candle.TimeClose = candle.TimeOpen + interval;
                                candle.Interval = interval;
                            }

                            currentTimestamp = candleDTOs.First().Time;

                            await _coreDBContext.AddRangeAsync(candles);
                            await _coreDBContext.SaveChangesAsync();
                            _logger.Information($"Load {candles.Count} candles for {fromSymbol}-{toSymbol} from {exchange}");
                        }
                        else
                        {
                            isActive = false;
                        }
                    }
                }
                else
                {
                    //пара отсутствует на бирже
                }
            }
        }
    }
}
