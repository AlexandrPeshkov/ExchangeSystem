using AutoMapper;
using ES.Domain.Configurations;
using Microsoft.Extensions.Options;

namespace ES.Gateway.UseCases.CryptoCompare.Historical
{
    public class DailyCandleUseCase : BaseHistoricalCandleUseCase
    {
        protected override string Period => "histoday";

        public DailyCandleUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper) : base(keys, mapper)
        {
        }
    }
}
