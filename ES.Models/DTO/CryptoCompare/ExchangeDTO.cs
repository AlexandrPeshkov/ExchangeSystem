using System.Text.Json.Serialization;

namespace ES.Domain.DTO.CryptoCompare
{
    public class ExchangeDTO
    {
        public string Name { get; set; }
        public string AffiliateURL { get; set; }
        public string Country { get; set; }
        public string Grade { get; set; }
        public decimal GradePoints { get; set; }
        public string CentralizationType { get; set; }
        public bool Trades { get; set; }
        public bool OrderBook { get; set; }
    }
}
