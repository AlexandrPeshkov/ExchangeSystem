using System.Linq;
using System.Web;
using ES.Domain.Interfaces.Requests;

namespace ES.DataImport.Extensions
{
    public static class RequestExtensions
    {
        /// <summary>
        /// Превратить модель запроса в URI строку параметров
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string ToQuery(this IExchangeRequest request)
        {
            var properties = from p in request.GetType().GetProperties()
                             where p.GetValue(request, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(request, null).ToString());

            var url = string.Join("&", properties.ToArray());
            return url;
        }
    }
}
