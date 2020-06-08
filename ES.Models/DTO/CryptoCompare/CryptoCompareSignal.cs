namespace ES.Domain.DTO.CryptoCompare
{
    public class CryptoCompareSignal
    {
        public string Categoty { get; set; }
        public string Sentiment { get; set; }
        public double Value { get; set; }
        public double Score { get; set; }
        public double Score_threshold_bearish { get; set; }
        public double Score_threshold_bullish { get; set; }
    }
}
