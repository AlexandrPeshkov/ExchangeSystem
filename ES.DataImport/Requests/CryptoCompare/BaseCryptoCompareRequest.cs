using ES.Domain.Interfaces.Requests;

namespace ES.DataImport.Requests.CryptoCompare
{
    public class BaseCryptoCompareRequest : IExchangeRequest
    {
        public string Api_Key { get; set; }
        public string ExtraParams { get; set; }
    }
}
