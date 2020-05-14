using AutoMapper;
using ES.Domain.Entities;
using ES.Domain.ViewModels;

namespace ES.Infrastructure.Mapper
{
    public class EntityToView : Profile
    {
        public EntityToView()
        {
            CreateMap<Exchange, ExchangeView>();
            CreateMap<Currency, CurrencyView>();
            CreateMap<ExchangePair, ExchangePairView>();
        }
    }
}
