using System.Collections.Generic;

namespace ES.Domain.DTO.CryptoCompare
{
    public class ExchangePairsDTO
    {
        public string Name { get; set; }

        public List<PairDTO> Pairs { get; set; }
    }
}
