using System;
using ES.Domain.Interfaces;
using ES.Domain.Models;

namespace ES.Domain.Entities
{
    public class Candle : BaseEntity, IOhlcv
    {
        public DateTime Time { get; set; }

        public decimal Open { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Close { get; set; }

        public decimal Volume { get; set; }
    }
}
