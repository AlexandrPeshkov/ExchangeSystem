using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ES.Gateway.Responses.AlphaVantage
{
    public class CryptoRatingResponse : BaseAlphaVantageResponse
    {
        [JsonProperty("Crypto Rating (FCAS)")]
        public CryptoRatingData Data { get; set; }

        public class CryptoRatingData
        {
            [JsonProperty("1. symbol")]
            public string Symbol { get; set; }

            [JsonProperty("2. name")]
            public string Name { get; set; }

            [JsonProperty("3. fcas rating")]
            public string FcasRating { get; set; }

            [JsonProperty("4. fcas score")]
            public decimal? FcasScore { get; set; }

            [JsonProperty("5. developer score")]
            public decimal? DeveloperScore { get; set; }

            [JsonProperty("6. market maturity score")]
            public decimal? MarketMaturityScore { get; set; }

            [JsonProperty("7. utility score")]
            public decimal? UtilityScore { get; set; }

            [JsonProperty("8. last refreshed")]
            public DateTime? LastRefreshed { get; set; }

            [JsonProperty("9. timezone")]
            public string Timezone { get; set; }
        }
    }
}
