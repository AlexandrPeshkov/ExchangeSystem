using AutoMapper;
using ES.Domain.Configurations;
using Microsoft.Extensions.Options;

namespace ES.Gateway.UseCases.AlphaVantage.Analysis
{
    public class EmaUseCase : BaseAnalysisUseCase
    {
        public EmaUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper) : base(keys, mapper)
        {
        }

        protected override string Function => "EMA";
    }
}
