namespace ES.Domain.DTO.CryptoCompare
{
    public class SignalsDTO
    {
        public int Id { get; set; }
        public int Time { get; set; }
        public string Symbol { get; set; }

        public CryptoCompareSignal InOutVar { get; set; }
        public CryptoCompareSignal LargetxsVar { get; set; }
        public CryptoCompareSignal AddressesNetGrowth { get; set; }
        public CryptoCompareSignal ConcentrationVar { get; set; }
    }
}
