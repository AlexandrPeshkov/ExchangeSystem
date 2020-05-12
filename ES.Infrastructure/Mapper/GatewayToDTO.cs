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
            CreateMap<ExchangeDTO, Exchange>()
                .ForMember(d => d.WebSite, d => d.MapFrom(s => s.AffiliateURL));
        }
    }
}
