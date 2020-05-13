using System;
using ES.Domain.Models;

namespace ES.Domain.Entities
{
    public class Candle : BaseEntity
    {
        public long TimeOpen { get; set; }

        public long TimeClose { get; set; }
        
        public long Interval { get; set; }

        public decimal Open { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Close { get; set; }

        public decimal VolumeFrom { get; set; }

        public decimal VolumeTo { get; set; }

        public virtual ExchangePair Pair { get; set; }

        public Guid PairId { get; set; }
    }
}
