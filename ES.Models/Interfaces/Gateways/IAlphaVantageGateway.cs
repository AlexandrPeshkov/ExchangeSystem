using System.Threading.Tasks;
using ES.Domain.ApiCommands.TechnicalAnalysisCommands;
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

        public Task<CommandResult<AnalysisDTO>> BollingerBands(BaseAnalysisCommand command);

        public Task<CommandResult<AnalysisDTO>> EMA(BaseAnalysisCommand command);

        public Task<CommandResult<AnalysisDTO>> SMA(BaseAnalysisCommand command);

    }
}
