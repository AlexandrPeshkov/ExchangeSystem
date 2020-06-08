using System.Collections.Generic;
using ES.Domain.Models;

namespace ES.Domain.Entities
{
    public class Account : BaseEntity
    {
        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Подписки
        /// </summary>
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
