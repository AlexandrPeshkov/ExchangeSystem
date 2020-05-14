using System.ComponentModel.DataAnnotations;

namespace ES.Domain.ApiCommands
{
    public class ExchangePairsCommand
    {
        /// <summary>
        /// Биржа
        /// </summary>
        [Required]
        public string Exchange { get; set; }
    }
}
