using ES.Domain;
using ES.Domain.Entities;

namespace ES.Infrastructure.Services.DataHolders
{
    public class CurrencyHolder : BaseDataHolder<Currency>
    {
        public CurrencyHolder(CoreDBContext context) : base(context)
        {
        }
    }
}
