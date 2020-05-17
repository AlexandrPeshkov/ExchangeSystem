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
    }
}
