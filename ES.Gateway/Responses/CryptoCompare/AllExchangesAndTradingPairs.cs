using System.Collections.Generic;
using static ES.Gateway.Responses.CryptoCompare.AllExchangesAndTradingPairs;

namespace ES.Gateway.Responses.CryptoCompare
{
    public class AllExchangesAndTradingPairs : BaseCryptoCompareResponse<ExchangesResponse>
    {
        public class ExchangesResponse
        {
            public class CryptoComparePairs
            {
                public class CryptoComparePair
                {
                    public Dictionary<string, Dictionary<string, string>> TSYMS { get; set; }
                }

                public Dictionary<string, CryptoComparePair> Pairs { get; set; }
            }

            public Dictionary<string, CryptoComparePairs> Exchanges { get; set; }
        }
    }
}
