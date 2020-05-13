using AutoMapper;
using ES.Domain.DTO.CryptoCompare;
using ES.Domain.Entities;

namespace ES.Infrastructure.Mapper
{
    public class GatewayToDTO : Profile
    {
        public GatewayToDTO()
        {
            CreateMap<CurrencyDTO, Currency>();
            CreateMap<CandleDTO, Candle>()
                .ForMember(d => d.TimeOpen, d => d.MapFrom(s => s.Time));

            CreateMap<ExchangeDTO, Exchange>()
                .ForMember(d => d.WebSite, d => d.MapFrom(s => s.AffiliateURL));
        }
    }
}
