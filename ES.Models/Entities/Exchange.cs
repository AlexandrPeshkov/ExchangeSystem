﻿using System.Collections.Generic;
using ES.Domain.Models;

namespace ES.Domain.Entities
{
    /// <summary>
    /// Биржа | Crypto API
    /// </summary>
    public class Exchange : BaseEntity
    {
        public string Name { get; set; }
        public string WebSite { get; set; }
        public string Country { get; set; }
        public string Grade { get; set; }
        public decimal GradePoints { get; set; }
        public string CentralizationType { get; set; }
        public bool Trades { get; set; }
        public bool OrderBook { get; set; }

        public virtual ICollection<ExchangePair> Pairs { get; set; }
    }
}
