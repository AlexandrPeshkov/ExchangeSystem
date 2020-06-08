using AutoMapper;
using ES.Domain.Configurations;
using ES.Domain.DTO.AphaVantage;
using ES.Gateway.Requests.AlphaVantage.TechnicalAnalysis;
using ES.Gateway.Responses.AlphaVantage;
using Microsoft.Extensions.Options;

namespace ES.Gateway.UseCases.AlphaVantage.Analysis
{
    public abstract class BaseAnalysisUseCase : BaseAlphaVantageUseCase<BaseAnalysisRequest, BaseAnalysisResponse, AnalysisDTO>
    {
        public BaseAnalysisUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper) : base(keys, mapper)
        {
        }

        protected override BaseAnalysisResponse DeserializeResponse(string json)
        {
            return new BaseAnalysisResponse
            {
                Data = json
            };
        }

        protected override AnalysisDTO MapResponse(BaseAnalysisResponse response)
        {
            return new AnalysisDTO
            {
                Data = response?.Data
            };
        }
    }
}
