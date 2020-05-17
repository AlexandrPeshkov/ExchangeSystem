using System;
using AutoMapper;
using ES.Domain.Configurations;
using ES.Domain.DTO.AphaVantage;
using ES.Gateway.Requests.AlphaVantage;
using ES.Gateway.Responses.AlphaVantage;
using Microsoft.Extensions.Options;

namespace ES.Gateway.UseCases.AlphaVantage
{
    /// <summary>
    /// This API is powered by Flipside Crypto,
    /// the inventor of the FCAS system and an industry-leading "rating agency" for cryptocurrencies.
    /// For more information, please visit https://flipsidecrypto.com/fcas/.
    /// </summary>
    public class CryptoRatingUseCase : BaseAlphaVantageUseCase<CryptoRatingRequest, CryptoRatingResponse, CryptoRatingDTO>
    {
        public CryptoRatingUseCase(IOptions<StockExchangeKeys> keys, IMapper mapper) : base(keys, mapper)
        {
        }

        protected override string Function => "CRYPTO_RATING";

        protected override CryptoRatingDTO MapResponse(CryptoRatingResponse response)
        {
            CryptoRatingDTO dto = default;
            if (response != null && response.Data != null)
            {
                try
                {
                    dto = _mapper.Map<CryptoRatingDTO>(response.Data);
                    if (dto != null)
                    {
                        OkResult(dto);
                    }
                }
                catch (Exception ex)
                {
                    ErrorResult("Error map types");
                }
            }
            ErrorResult("No data");
            return dto;
        }
    }
}
