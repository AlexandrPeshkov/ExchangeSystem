using System.Collections.Generic;
using System.Threading.Tasks;
using ES.Domain.Interfaces;

namespace ES.Infrastructure.Interfaces
{
    /// <summary>
    /// Шлюз данных биржи
    /// </summary>
    public interface IStockExchangeGateway
    {
        ///// <summary>
        ///// Импорт исторических данных за 20 лет. 1 запись - 1 день
        ///// </summary>
        ///// <param name="symbol">Валютный символ</param>
        ///// <param name="market">Денежный символ</param>
        ///// <returns></returns>
        //Task<IReadOnlyList<IOhlcv>> ImportTimeSeriesDaily(string symbol, string market);
    }
}
