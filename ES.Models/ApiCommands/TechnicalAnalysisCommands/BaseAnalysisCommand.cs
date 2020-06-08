using System.ComponentModel.DataAnnotations;
using ES.Domain.DTO.AphaVantage;
using ES.Domain.DTO.AphaVantage.Enums;
using ES.Domain.Interfaces;

namespace ES.Domain.ApiCommands.TechnicalAnalysisCommands
{
    /// <summary>
    /// Команда технического анализа
    /// </summary>
    public class BaseAnalysisCommand : IApiCommand
    {
        /// <summary>
        /// Символ валюты
        /// </summary>
        [Required]
        public string Symbol { get; set; }

        /// <summary>
        /// Период вычисления средних(положительное целое число)
        /// </summary>
        [Required, Range(1, int.MaxValue)]
        public uint Period { get; set; }

        /// <summary>
        /// Интервал между двумя точками ряда
        /// </summary>
        [Required]
        public AlphaVantageHistoricalPeriod Interval { get; set; }

        /// <summary>
        /// Поле значений ряда свечей
        /// </summary>
        [Required]
        public CandleSeriesType SeriesType { get; set; }
    }
}
