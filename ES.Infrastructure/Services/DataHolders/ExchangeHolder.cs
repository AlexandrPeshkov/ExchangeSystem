using System;
using System.Linq.Expressions;
using ES.Domain;
using ES.Domain.Entities;

namespace ES.Infrastructure.Services.DataHolders
{
    public class ExchangeHolder : BaseDataHolder<Exchange>
    {
        protected override Expression<Func<Exchange, object>>[] Includes => new Expression<Func<Exchange, object>>[]
        {
            x => x.Pairs
        };
        public ExchangeHolder(CoreDBContext context) : base(context)
        {
        }
    }
}
