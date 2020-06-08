using AutoMapper;
using ES.Domain.Configurations;
using ES.Gateway.UseCases.CryptoCompare.Historical;
using Microsoft.Extensions.Options;

namespace ES.Domain.UseCase.CryptoCompare.Historical
{
    public class MinuteCandleUseCase : BaseHistoricalCandleUseCase
    {
        protected override string Period => "histominute";
        public MinuteCandleUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper) : base(keys, mapper)
        {
        }
    }
}
