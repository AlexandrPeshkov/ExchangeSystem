using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using ES.Domain.Extensions.Attributes;
using ES.Domain.Interfaces.Requests;

namespace ES.Domain.Extensions
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
            var props = request.GetType().GetProperties();

            List<string> args = new List<string>(props.Length);
            foreach (var prop in props)
            {
                var val = prop.GetValue(request, null);
                if (val != null)
                {
                    string value = val.ToString();

                    if (prop.PropertyType == typeof(bool))
                    {
                        value = value.ToLower();
                    }
                    value = HttpUtility.UrlEncode(value);
                    string name = prop.GetCustomAttribute<QueryParamAttribute>()?.Name ?? prop.Name;
                    args.Add($"{name}={value}");
                }
            }

            var uri = string.Join("&", args);

            //var properties = from p in request.GetType().GetProperties()
            //                 where p.GetValue(request, null) != null
            //                 select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(request, null).ToString());

            //var url = string.Join("&", properties.ToArray()); //.ToLower();
            return uri;
        }
    }
}
