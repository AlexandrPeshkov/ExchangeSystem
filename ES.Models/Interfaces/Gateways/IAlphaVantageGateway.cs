using System.Threading.Tasks;
using ES.Domain.ApiResults;
using ES.Domain.DTO.AphaVantage;

namespace ES.Domain.Interfaces.Gateways
{
    /// <summary>
    /// Шлюз AlphaVantage
    /// </summary>
    public interface IAlphaVantageGateway
    {
        Task<CommandResult<CryptoRatingDTO>> CryptoRating(string symbol);
    }
}
