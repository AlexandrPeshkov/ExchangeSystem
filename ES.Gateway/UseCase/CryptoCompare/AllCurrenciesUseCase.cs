using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ES.Gateway.Requests.CryptoCompare;
using ES.Gateway.Responses.CryptoCompare;
using ES.Domain.Configurations;
using ES.Domain.DTO.CryptoCompare;
using ES.Domain.Entities;
using Microsoft.Extensions.Options;

namespace ES.Gateway.UseCase.CryptoCompare
{
    public class AllCurrenciesUseCase : BaseDataCryptoCompareUseCase<
        BaseCryptoCompareRequest,
        BaseCryptoCompareResponse<Dictionary<string, CurrencyDTO>>,
        Dictionary<string, CurrencyDTO>,
        List<Currency>>
    {
        protected override string UriPath => "data/all/coinlist";

        public AllCurrenciesUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper) : base(keys, mapper) { }

        protected override object ExtractData(Dictionary<string, CurrencyDTO> data)
        {
            return data?.Values?.Where(x => x != null && !string.IsNullOrEmpty(x.Name))?.ToList();
        }
    }
}
