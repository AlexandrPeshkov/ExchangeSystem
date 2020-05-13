using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ES.Domain;
using ES.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ES.Infrastructure.Services.DataHolders
{
    public class BaseDataHolder<TEntity> where TEntity : BaseEntity
    {
        public List<TEntity> Entities { get; private set; }

        protected virtual Expression<Func<TEntity, object>>[] Includes { get; }

        public BaseDataHolder(CoreDBContext context)
        {
            Load(context, Includes);
        }

        private void Load(CoreDBContext context, params Expression<Func<TEntity, object>>[] includes)
        {
            var set = context.Set<TEntity>();

            if (includes?.Any() == true)
            {
                foreach (var include in includes)
                {
                    set.Include(include);
                }
            }
            Entities = set.ToList();
        }
    }
}
