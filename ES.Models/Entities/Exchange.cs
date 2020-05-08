using System.Collections.Generic;
using System.Text.Json.Serialization;
using ES.Domain.Models;

namespace ES.Domain.Entities
{
    /// <summary>
    /// Биржа | Crypto API
    /// </summary>
    public class Exchange : BaseEntity
    {
        public string Name { get; set; }

        [JsonPropertyName("AffiliateURL")]
        public string WebSite { get; set; }
        public string Country { get; set; }
        public string Grade { get; set; }

        public virtual ICollection<Pair> Pairs { get; set; }
    }
}
