using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ES.Domain;
using ES.Domain.ApiCommands;
using ES.Domain.ViewModels;
using ES.Infrastructure.UseCases;
using Microsoft.EntityFrameworkCore;

namespace ES.Data.UseCases
{
    public class AllCurrencyUseCase : BaseContextUseCase<EmptyCommand, List<CurrencyView>>
    {
        public AllCurrencyUseCase(CoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override async Task<List<CurrencyView>> CommandExecute(EmptyCommand command)
        {
            List<CurrencyView> views = null;
            var currencies = await _context.Currencies.ToListAsync();
            if (currencies != null)
            {
                views = _mapper.Map<List<CurrencyView>>(currencies);
            }
            return views;
        }
    }
}
