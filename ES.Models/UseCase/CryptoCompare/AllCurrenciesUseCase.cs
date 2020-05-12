using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ES.Domain.Configurations;
using ES.Domain.DTO.CryptoCompare;
using ES.Domain.Entities;
using ES.Domain.Requests;
using ES.Domain.Responses.CryptoCompare;
using Microsoft.Extensions.Options;

namespace ES.Domain.UseCase.CryptoCompare
{
    public class AllCurrenciesUseCase : BaseCryptoCompareUseCase<EmptyRequest, Dictionary<string, CurrencyDTO>, List<Currency>>
    {
        protected override string UriPath => "data/all/coinlist";

        public AllCurrenciesUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper) : base(keys, mapper)
        {
        }

        protected override List<Currency> MapResult(BaseCryptoCompareResponse<Dictionary<string, CurrencyDTO>> result)
        {
            List<Currency> currencies = new List<Currency>();
            if (result != null && result.IsSuccess && result.Data != null)
            {
                var data = result.Data.Values.Where(x => x != null && !string.IsNullOrEmpty(x.Name)).ToList();
                currencies = _mapper.Map<List<Currency>>(data);
            }
            return currencies;
        }
    }
}
