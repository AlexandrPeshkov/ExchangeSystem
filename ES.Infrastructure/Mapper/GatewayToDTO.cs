using AutoMapper;
using ES.Domain.DTO.AphaVantage;
using ES.Domain.DTO.CryptoCompare;
using ES.Domain.Entities;
using ES.Gateway.Responses.AlphaVantage;
using static ES.Gateway.Responses.AlphaVantage.CryptoRatingResponse;
using static ES.Gateway.Responses.CryptoCompare.SignalsResponse;

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

            CreateMap<CryptoRatingData, CryptoRatingDTO>();
            CreateMap<CryptoRatingResponse, CryptoRatingDTO>();

            CreateMap<TradingSignalsResponse, SignalsDTO>();
        }
    }
}
