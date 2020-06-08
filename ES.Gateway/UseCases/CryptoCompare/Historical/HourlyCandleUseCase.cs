using AutoMapper;
using ES.Domain.Configurations;
using Microsoft.Extensions.Options;

namespace ES.Gateway.UseCases.CryptoCompare.Historical
{
    public class HourlyCandleUseCase : BaseHistoricalCandleUseCase
    {
        protected override string Period => "histohour";
        public HourlyCandleUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper) : base(keys, mapper)
        {
        }
    }
}

