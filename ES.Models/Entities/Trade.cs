using System;
using ES.Domain.Interfaces;

namespace ES.Domain.Models
{
    public class Trade : ITickTrade
    {
        public Trade(DateTime date, decimal price, decimal volume)
        {
            Time = date;
            Price = price;
            Volume = volume;
        }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public DateTime Time { get; set; }

    }
}
