using System;
using ES.Domain.Models;

namespace ES.Domain.Entities
{
    public class Subscription : BaseEntity
    {
        /// <summary>
        /// Интеерсующая валюта
        /// </summary>
        public virtual Currency Currency { get; set; }

        public Guid CurrencyId { get; set; }

        /// <summary>
        /// Условие вызова
        /// </summary>
        //public Func<Currency, bool> Predicate { get; set; }
        public string Predicate { get; set; }
    }
}
