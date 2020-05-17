using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ES.Domain;
using ES.Domain.ApiCommands;
using ES.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ES.Infrastructure.UseCases
{
    public class AllPairsUseCase : BaseContextUseCase<EmptyCommand, List<ExchangePairView>>
    {
        public AllPairsUseCase(CoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override async Task<List<ExchangePairView>> CommandExecute(EmptyCommand command)
        {
            List<ExchangePairView> views = null;
            var entities = await _context.Pairs
                .Include(x => x.CurrencyFrom)
                .Include(x => x.CurrencyTo)
                .Include(x => x.Exchange)
                .ToListAsync();
            if (entities != null)
            {
                views = _mapper.Map<List<ExchangePairView>>(entities);
            }
            return views;
        }
    }
}
