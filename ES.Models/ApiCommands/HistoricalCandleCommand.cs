using System.ComponentModel.DataAnnotations;
using ES.Domain.DTO.AphaVantage.Enums;
using ES.Domain.Interfaces;

namespace ES.Domain.ApiCommands
{
    public class HistoricalCandleCommand : IApiCommand
    {
        /// <summary>
        /// Символ импорта
        /// </summary>
        [Required]
        public string FromSymbol { get; set; }

        /// <summary>
        /// Символ результата
        /// </summary>
        [Required]
        public string ToSymbol { get; set; }

        /// <summary>
        /// Биржа
        /// </summary>
        [Required]
        public string Exchange { get; set; }

        /// <summary>
        /// Верхняя граница времени 
        /// </summary>
        public long? BeforeTimestamp { get; set; } = null;

        /// <summary>
        /// Число записей в ответе (максимум 2000)
        /// </summary>
        public int Limit { get; set; } = 2000;

        /// <summary>
        /// Период
        /// </summary>
        [Required]
        public AlphaVantageHistoricalPeriod Period { get; set; }
    }
}
