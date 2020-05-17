using System;

namespace ES.Domain.DTO.AphaVantage
{
    public class CryptoRatingDTO
    {
        public string Symbol { get; set; }

        public string Name { get; set; }

        public string FcasRating { get; set; }

        public decimal FcasScore { get; set; }

        public decimal DeveloperScore { get; set; }

        public decimal MarketMaturityScore { get; set; }

        public decimal UtilityScore { get; set; }

        public DateTime LastRefreshed { get; set; }

        public string Timezone { get; set; }
    }
}
