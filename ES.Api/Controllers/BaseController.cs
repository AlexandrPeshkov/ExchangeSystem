using System.Linq;
using ES.Domain.ApiResults;
using Microsoft.AspNetCore.Mvc;

namespace ES.API.Controllers
{
    /// <summary>
    /// Базовый контроллер
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        public CommandResult<TContent> OkResult<TContent>(TContent content, params string[] messages)
        {
            return new CommandResult<TContent>(content, messages?.ToList(), true);
        }

        public CommandResult<TContent> ErrorResult<TContent>(params string[] messages)
        {
            return new CommandResult<TContent>(messages: messages?.ToList(), isSuccess: false);
        }
    }
}
