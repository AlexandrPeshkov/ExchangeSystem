using System;

namespace ES.Domain.DTO.AphaVantage
{
    /// <summary>
    /// Запись о курсе криптовалюты в ценах символа и USD
    /// </summary>
    public class DigitalCurrency
    {
        public DateTime? DateTime { get; set; }
        public decimal? OpenSymbol { get; set; }
        public decimal? OpenUSD { get; set; }
        public decimal? HightSymbol { get; set; }
        public decimal? HighUSD { get; set; }
        public decimal? LowSymbol { get; set; }
        public decimal? LowUSD { get; set; }
        public decimal? CloseSymbol { get; set; }
        public decimal? CloseUSD { get; set; }
        public decimal? Volume { get; set; }
        public decimal? MarketCapUSD { get; set; }
    }
}
