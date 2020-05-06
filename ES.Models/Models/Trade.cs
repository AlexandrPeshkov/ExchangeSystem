using System;
using ES.Domain.Interfaces;

namespace ES.Domain.Models
{
    public class Trade : ITickTrade
    {
        public Trade(DateTimeOffset date, decimal price, decimal volume)
        {
            DateTime = date;
            Price = price;
            Volume = volume;
        }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public DateTimeOffset DateTime { get; set; }

    }
}
