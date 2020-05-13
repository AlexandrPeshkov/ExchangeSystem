using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ES.Domain.Configurations;
using ES.Domain.DTO.CryptoCompare;
using ES.Domain.Requests.CryptoCompare;
using ES.Domain.Responses.CryptoCompare;
using Microsoft.Extensions.Options;

namespace ES.Domain.UseCase.CryptoCompare
{
    public class AllExchangesUseCase : BaseCryptoCompareUseCase<BaseCryptoCompareRequest, Dictionary<string, ExchangeDTO>, List<ExchangeDTO>>
    {
        protected override string UriPath => "data/exchanges/general";

        public AllExchangesUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper) : base(keys, mapper)
        {
        }

        protected override List<ExchangeDTO> MapResult(BaseCryptoCompareResponse<Dictionary<string, ExchangeDTO>> result)
        {
            List<ExchangeDTO> exchanges = result.Data.Values.ToList();
            return exchanges;
        }
    }
}
