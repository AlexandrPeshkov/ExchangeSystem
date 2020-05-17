namespace ES.Domain.ViewModels
{
    public class ExchangePairView
    {
        public string CurrencyFromSymbol { get; set; }

        public string CurrencyToSymbol { get; set; }

        /// <summary>
        /// Биржа
        /// </summary>
        public string ExchangeName { get; set; }
    }
}
