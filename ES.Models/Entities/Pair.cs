using System;
using ES.Domain.Models;

namespace ES.Domain.Entities
{
    /// <summary>
    /// Торговая пара
    /// </summary>
    public class Pair : BaseEntity
    {
        public Guid CurrencyFromId { get; set; }

        public Guid CurrencyToId { get; set; }

        public virtual Currency CurrencyFrom { get; set; }
        public virtual Currency CurrencyTo { get; set; }
    }
}
