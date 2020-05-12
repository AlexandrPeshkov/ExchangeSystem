using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ES.DataImport.StockExchangeGateways;
using ES.Domain;
using ES.Domain.DTO.CryptoCompare;
using ES.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ES.Infrastructure.Services
{
    public class ImportColdDataService
    {
        private readonly IMapper _mapper;

        private readonly CryptoCompareGateway _cryptoCompareGateway;

        private readonly CoreDBContext _coreDBContext;

        public ImportColdDataService(CryptoCompareGateway cryptoCompareGateway, CoreDBContext coreDBContext, IMapper mapper)
        {
            _cryptoCompareGateway = cryptoCompareGateway;
            _coreDBContext = coreDBContext;
            _mapper = mapper;
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

                                    //Exchange = exchange,
                                    //CurrencyFrom = currencyFrom,
                                    //CurrencyTo = currencyTo
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
    }
}
