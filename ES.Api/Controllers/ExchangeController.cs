using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ES.Domain;
using ES.Domain.ApiCommands;
using ES.Domain.ApiRequests;
using ES.Domain.ApiResults;
using ES.Domain.Entities;
using ES.Domain.Interfaces.Gateways;
using ES.Domain.ViewModels;
using ES.Gateway.StockExchangeGateways;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ES.API.Controllers
{
    /// <summary>
    /// Биржа
    /// </summary>
    public class ExchangeController : BaseController
    {
        private readonly CoreDBContext _coreDBContext;

        private readonly ICryptoCompareGateway _cryptoCompareGateway;

        private readonly IMapper _mapper;
        public ExchangeController(CoreDBContext coreDBContext, IMapper mapper, ICryptoCompareGateway cryptoCompareGateway)
        {
            _coreDBContext = coreDBContext;
            _mapper = mapper;
            _cryptoCompareGateway = cryptoCompareGateway;
        }

        /// <summary>
        /// Список всех доступных бирж
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(All))]
        public async Task<IActionResult> All()
        {
            List<ExchangeView> all = new List<ExchangeView>();
            var allEntity = await _coreDBContext.Exchanges.ToListAsync();
            if (allEntity?.Any() == true)
            {
                all = _mapper.Map<List<ExchangeView>>(allEntity);
            }
            return Ok(all);
        }

        /// <summary>
        /// Список торгуемых пар на бирже
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet(nameof(Pairs))]
        public async Task<IActionResult> Pairs([FromQuery] ExchangePairsCommand command)
        {
            List<ExchangePairView> pairViews = new List<ExchangePairView>();
            Exchange exchangeEntity = await _coreDBContext.Exchanges
                .Include(exch => exch.Pairs)
                .ThenInclude(pair => pair.CurrencyFrom)
                .Include(exch => exch.Pairs)
                .ThenInclude(pair => pair.CurrencyTo)
                .FirstOrDefaultAsync(x => x.Name == command.Exchange);

            if (exchangeEntity?.Pairs != null)
            {
                pairViews = _mapper.Map<List<ExchangePairView>>(exchangeEntity.Pairs);
                return Ok(pairViews);
            }
            else
            {
                return NotFound(command.Exchange);
            }
        }

        /// <summary>
        /// Текущий курс пары
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet(nameof(CurrencyPrice))]
        public async Task<CommandResult<Dictionary<string, decimal>>> CurrencyPrice([FromQuery]CurrencyPriceCommand command)
        {
            var result = await _cryptoCompareGateway.CurrencyPrice(command);
            return result;
        }
    }
}
