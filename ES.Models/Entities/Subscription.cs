using System;
using System.Collections.Generic;
using ES.Domain.Models;

namespace ES.Domain.Entities
{
    public class Subscription : BaseEntity
    {
        /// <summary>
        /// Интеерсующие валюты
        /// </summary>
        public virtual ICollection<Currency> Currencies { get; set; }

        /// <summary>
        /// Условие вызова
        /// </summary>
        public Func<Currency, bool> Predicate { get; set; }
    }
}
