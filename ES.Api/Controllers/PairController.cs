using System.Collections.Generic;
using System.Threading.Tasks;
using ES.Domain.ApiCommands;
using ES.Domain.ApiResults;
using ES.Domain.ViewModels;
using ES.Infrastructure.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace ES.API.Controllers
{
    /// <summary>
    /// Контроллер торговых пар
    /// </summary>
    public class PairController : BaseController
    {
        private readonly AllPairsUseCase _allPairsUseCase;
        public PairController(AllPairsUseCase allPairsUseCase)
        {
            _allPairsUseCase = allPairsUseCase;
        }

        /// <summary>
        /// Список всех доступных пар
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(All))]
        public async Task<CommandResult<List<ExchangePairView>>> All()
        {
            var result = await _allPairsUseCase.Execute(new EmptyCommand());
            return result;
        }
    }
}
