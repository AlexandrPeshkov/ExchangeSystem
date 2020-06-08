using ES.Domain.DTO.CryptoCompare;
using static ES.Gateway.Responses.CryptoCompare.SignalsResponse;

namespace ES.Gateway.Responses.CryptoCompare
{
    public class SignalsResponse : BaseCryptoCompareResponse<TradingSignalsResponse>
    {
        public class TradingSignalsResponse
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
}
