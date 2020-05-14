using System.Collections.Generic;
using AutoMapper;
using ES.Gateway.Requests.CryptoCompare;
using ES.Domain.Configurations;
using Microsoft.Extensions.Options;

namespace ES.Gateway.UseCase.CryptoCompare
{
    public class PairPriceUseCase : BaseCryptoCompareUseCase<CurrencyPriceRequest, Dictionary<string, decimal>, Dictionary<string, decimal>>
    {
        protected override string UriPath => "data/price";
        public PairPriceUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper) : base(keys, mapper)
        {
        }
    }
}
