using System.Collections.Generic;
using ES.Domain.DTO.CryptoCompare;

namespace ES.Domain.Responses.CryptoCompare
{
    public class AllCoinsResponse : BaseCryptoCompareResponse<Dictionary<string, CurrencyDTO>>
    {
    }
}
