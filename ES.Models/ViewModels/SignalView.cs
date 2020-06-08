using System;
using ES.Domain.DTO.CryptoCompare;

namespace ES.Domain.ViewModels
{
    public class SignalView
    {
        public DateTime Time { get; set; }
        public string Symbol { get; set; }

        public CryptoCompareSignal InOutVar { get; set; }
        public CryptoCompareSignal LargetxsVar { get; set; }
        public CryptoCompareSignal AddressesNetGrowth { get; set; }
        public CryptoCompareSignal ConcentrationVar { get; set; }
    }
}
