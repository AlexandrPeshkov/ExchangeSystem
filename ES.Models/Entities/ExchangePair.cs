using System;
using ES.Domain.Models;

namespace ES.Domain.Entities
{
    /// <summary>
    /// Торговая пара
    /// </summary>
    public class ExchangePair : BaseEntity
    {
        public Guid CurrencyFromId { get; set; }

        public Guid CurrencyToId { get; set; }

        public Guid ExchangeId { get; set; }

        public virtual Currency CurrencyFrom { get; set; }

        public virtual Currency CurrencyTo { get; set; }

        public virtual Exchange Exchange { get; set; }
    }
}
