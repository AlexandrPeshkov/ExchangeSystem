using System.Collections.Generic;
using System.Threading.Tasks;
using ES.Domain.ApiRequests;
using ES.Domain.ApiResults;
using ES.Domain.DTO.CryptoCompare;
using ES.Domain.Entities;

namespace ES.Domain.Interfaces.Gateways
{
    /// <summary>
    /// Шлюз CryptoCompare
    /// </summary>
    public interface ICryptoCompareGateway
    {
        /// <summary>
        /// Импорт всех валют
        /// </summary>
        /// <returns></returns>
        Task<CommandResult<List<Currency>>> AllCurrencies();

        /// <summary>
        /// Импорт всех пар бирж
        /// </summary>
        /// <returns></returns>

        Task<CommandResult<List<ExchangePairsDTO>>> AllExchangePairs();

        /// <summary>
        /// Импорт всех бирж
        /// </summary>
        /// <returns></returns>
        Task<CommandResult<List<ExchangeDTO>>> AllExchanges();

        /// <summary>
        /// Импорт свечей
        /// </summary>
        /// <param name="fromSymbol">валюта</param>
        /// <param name="toSymbol">валюта конвертации</param>
        /// <param name="exchange">биржа</param>
        /// <param name="beforeTimestamp">граница выборки</param>
        /// <param name="limit">максимальное число записей(макс.2000)</param>
        /// <returns></returns>
        Task<CommandResult<List<CandleDTO>>> MinuteCandle(string fromSymbol, string toSymbol, string exchange, long? beforeTimestamp = null, int limit = 2000);

        /// <summary>
        /// Курс валюты в еденицах указанных на бирже
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<CommandResult<Dictionary<string, decimal>>> CurrencyPrice(CurrencyPriceCommand command);

    }
}
