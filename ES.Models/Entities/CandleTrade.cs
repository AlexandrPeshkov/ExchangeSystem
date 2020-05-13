using System;
using ES.Domain.Interfaces;

namespace ES.Domain.Models
{
    public class CandleTrade : IOhlcv
    {
        public CandleTrade(DateTime dateTime, decimal open, decimal high, decimal low, decimal close, decimal volume)
        {
            Time = dateTime;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }

        public DateTime Time { get; set; }

        public decimal Open { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Close { get; set; }

        public decimal Volume { get; set; }
        public decimal VolumeTo { get; set; }
    }
}
