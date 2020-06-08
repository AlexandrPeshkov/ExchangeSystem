using ES.Domain.Extensions.Attributes;

namespace ES.Domain.DTO.AphaVantage.Enums
{
    public enum AlphaVantageHistoricalPeriod
    {
        [EnumName("1min")]
        OneMinute,
        [EnumName("5min")]
        FiveMinutes,
        [EnumName("15min")]
        FifteenMinutes,
        [EnumName("30min")]
        ThitryMinute,
        [EnumName("60min")]
        Hour,
        [EnumName("daily")]
        Day,
        [EnumName("weekly")]
        Week,
        [EnumName("monthly")]
        Month
    }
}
