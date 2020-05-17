using System.ComponentModel.DataAnnotations;
using ES.Domain.Interfaces;

namespace ES.Domain.ApiCommands
{
    public class ExchangePairsCommand : IApiCommand
    {
        /// <summary>
        /// Биржа
        /// </summary>
        [Required]
        public string Exchange { get; set; }
    }
}
