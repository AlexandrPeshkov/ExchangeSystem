using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace ES.API.Filters
{
    /// <summary>
    /// Фильтр ошибок
    /// </summary>
    public partial class HttpGlobalExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Перехват ошибки
        /// </summary>
        /// <param name="context">ExceptionContext</param>
        public void OnException(ExceptionContext context)
        {
            Log.Error(context.Exception, context.Exception.Message);
        }
    }
}
