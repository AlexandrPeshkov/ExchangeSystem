using AutoMapper;
using ES.Domain.Configurations;
using ES.Domain.DTO.CryptoCompare;
using ES.Gateway.Requests.CryptoCompare;
using ES.Gateway.Responses.CryptoCompare;
using ES.Gateway.UseCase.CryptoCompare;
using Microsoft.Extensions.Options;
using static ES.Gateway.Responses.CryptoCompare.SignalsResponse;

namespace ES.Gateway.UseCases.CryptoCompare
{
    public class TradingSignalsUseCase : BaseDataCryptoCompareUseCase<SignalsRequest, SignalsResponse, TradingSignalsResponse, SignalsDTO>
    {
        public TradingSignalsUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper) : base(keys, mapper)
        {
        }

        protected override string UriPath => "data/tradingsignals/intotheblock/latest";
    }
}
