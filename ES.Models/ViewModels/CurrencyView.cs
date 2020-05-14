namespace ES.Domain.ViewModels
{
    public class CurrencyView
    {
        public string Name { get; set; }

        public string Symbol { get; set; }

        public string Fullname { get; set; }

        public string Algorithm { get; set; }

        public decimal? TotalCoinsMined { get; set; }

        public long? BlockNumber { get; set; }

        public decimal? NetHashesPerSecond { get; set; }

        public decimal? BlockReward { get; set; }

        public string SmartContractAddress { get; set; }
    }
}
