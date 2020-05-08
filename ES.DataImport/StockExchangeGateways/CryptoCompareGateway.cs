using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using ES.Domain;
using ES.Domain.Configurations;
using ES.Domain.Entities;
using ES.Domain.Responses.CryptoCompare;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ES.DataImport.StockExchangeGateways
{
    public class CryptoCompareGateway : BaseExchangeGateway
    {
        public CryptoCompareGateway(IOptions<StockExchangeKeys> tokens, IMapper mapper, CoreDBContext context) : base(tokens, mapper, context)
        {
        }

        protected override string HostName => "min-api.cryptocompare.com";

        protected override RequestLimitConfiguration Limits => new RequestLimitConfiguration
        {
            MonthLimit = 100000
        };

        protected override HttpClient AddApiKey(ref HttpClient httpClient)
        {
            _uriBuilder.Query = $"apikey={_tokens.CryptoCompare}";
            return httpClient;
        }

        public async Task<List<Currency>> ImportAllCurrencies()
        {
            var currencies = new List<Currency>();
            var httpClient = HttpClient();
          
            _uriBuilder.Path = "data/all/coinlist";
            httpClient = AddApiKey(ref httpClient);
            try
            {
                var response = await httpClient.GetAsync(_uriBuilder.Uri);
                if (response?.IsSuccessStatusCode == true)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(json) == false)
                    {
                        var allCoinsResponse = JsonConvert.DeserializeObject<AllCoinsResponse>(json);
                        if (allCoinsResponse != null && allCoinsResponse.IsSuccess && allCoinsResponse.Data?.Any() == true)
                        {
                            var allDTO = allCoinsResponse.Data.Values.Where(x => x != null && !string.IsNullOrEmpty(x.Name)).ToList();
                            currencies = _mapper.Map<List<Currency>>(allDTO);

                            if (currencies?.Any() == true)
                            {
                                await _context.AddRangeAsync(currencies);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return currencies;
        }
    }
}
