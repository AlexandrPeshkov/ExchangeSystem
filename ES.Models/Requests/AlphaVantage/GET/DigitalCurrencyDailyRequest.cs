using ES.Domain.Interfaces.Requests;

namespace ES.Domain.Requests.AlphaVantage.GET
{
    public class DigitalCurrencyDailyRequest : IExchangeRequest
    {
        public string Function { get; set; }
        public string Symbol { get; set; }
        public string Market { get; set; }
        public string Datatype { get; set; }
        public string Outputsize { get; set; }
    }
}
