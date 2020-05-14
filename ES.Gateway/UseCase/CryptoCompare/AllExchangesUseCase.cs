using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ES.Gateway.Requests.CryptoCompare;
using ES.Gateway.Responses.CryptoCompare;
using ES.Domain.Configurations;
using ES.Domain.DTO.CryptoCompare;
using Microsoft.Extensions.Options;

namespace ES.Gateway.UseCase.CryptoCompare
{
    public class AllExchangesUseCase : BaseDataCryptoCompareUseCase<
        BaseCryptoCompareRequest,
        BaseCryptoCompareResponse<Dictionary<string, ExchangeDTO>>,
        Dictionary<string, ExchangeDTO>,
        List<ExchangeDTO>>
    {
        protected override string UriPath => "data/exchanges/general";

        public AllExchangesUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper) : base(keys, mapper) { }

        protected override object ExtractData(Dictionary<string, ExchangeDTO> data)
        {
            return data?.Values?.ToList();
        }
    }
}
