using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using ES.Domain.Extensions.Attributes;
using ES.Gateway.Interfaces.Requests;

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
                object val = prop.GetValue(request, null);
                if (val != null)
                {
                    string name = prop.GetCustomAttribute<QueryParamAttribute>()?.Name ?? prop.Name;
                    string value = val.ToString();

                    if (prop.PropertyType == typeof(bool))
                    {
                        value = value.ToLower();
                    }

                    if (prop.PropertyType.GetInterface(nameof(IEnumerable)) != null && prop.PropertyType != typeof(string)
                        && prop.PropertyType.IsGenericType && val is IEnumerable collection)
                    {
                        List<string> arrayValues = new List<string>();
                        foreach (var elem in collection)
                        {
                            arrayValues.Add($"{name}={elem}");
                        }
                        var valueCollection = string.Join("&", arrayValues);
                        args.Add(valueCollection);
                        value = string.Empty;
                    }

                    value = HttpUtility.UrlEncode(value);
                    if (string.IsNullOrEmpty(name) == false && string.IsNullOrEmpty(value) == false)
                    {
                        args.Add($"{name}={value}");
                    }
                }
            }

            var uri = string.Join("&", args);
            return uri;
        }
    }
}
