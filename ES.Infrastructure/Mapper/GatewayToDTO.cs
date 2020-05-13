using System;
using AutoMapper;
using ES.Domain.DTO.CryptoCompare;
using ES.Domain.Entities;
using ES.Infrastructure.Extensions;

namespace ES.Infrastructure.Mapper
{
    public class GatewayToDTO : Profile
    {
        public GatewayToDTO()
        {
            CreateMap<CurrencyDTO, Currency>();
            CreateMap<CandleDTO, Candle>()
                .ForMember(d => d.Volume, d => d.MapFrom(s => s.VolumeFrom))
                .ForMember(d => d.Time, d => d.MapFrom(s => s.Time.UnixTimeStampToDateTime()));
            CreateMap<ExchangeDTO, Exchange>()
                .ForMember(d => d.WebSite, d => d.MapFrom(s => s.AffiliateURL));
        }
    }
}
