using ES.Domain.ApiCommands;
using ES.Domain.DTO.AphaVantage;
using ES.Domain.DTO.AphaVantage.Enums;
using ES.Domain.Extensions.Attributes;

namespace ES.Gateway.Requests.AlphaVantage.TechnicalAnalysis
{
    public class BaseAnalysisRequest : BaseAlphaVantageRequest
    {
        [QueryParam("symbol")]
        public string Symbol { get; set; }


        [QueryParam("interval")]
        public AlphaVantageHistoricalPeriod Interval { get; set; }


        [QueryParam("time_period")]
        public uint TimePeriod { get; set; }


        [QueryParam("series_type")]
        public CandleSeriesType SeriesType { get; set; }

        public BaseAnalysisRequest(BaseAlphaVantageRequest request)
        {
            ApiKey = request?.ApiKey;
        }
    }
}
