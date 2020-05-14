using System.Collections.Generic;
using ES.Domain.DTO.CryptoCompare;

namespace ES.Gateway.Responses.CryptoCompare
{
    public class AllCurrenciesResponse : BaseCryptoCompareResponse<Dictionary<string, CurrencyDTO>>
    {
    }
}
