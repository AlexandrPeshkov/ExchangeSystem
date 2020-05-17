using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ES.Domain.Interfaces;

namespace ES.Domain.ApiRequests
{
    /// <summary>
    /// Запрос курса валюты в еденицах указанных валют на бирже
    /// </summary>
    public class CurrencyPriceCommand : IApiCommand
    {
        /// <summary>
        /// Валюта
        /// </summary>
        [Required]
        public string FromSymbol { get; set; }

        /// <summary>
        /// Список валют для конвертации
        /// </summary>
        [Required]
        public List<string> ToSymbols { get; set; }

        /// <summary>
        /// Биржа
        /// </summary>
        [Required]
        public string Exchange { get; set; }
    }
}
