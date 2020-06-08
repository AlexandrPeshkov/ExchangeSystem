using System.ComponentModel.DataAnnotations;

namespace ES.Domain.ApiCommands
{
    public class SignalCommand
    {
        /// <summary>
        /// Валюта
        /// </summary>
        [Required]
        public string Currency { get; set; }
    }
}
