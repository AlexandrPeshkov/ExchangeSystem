using System;

namespace ES.Domain.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class QueryParamAttribute : Attribute
    {
        public string Name { get; set; }

        public QueryParamAttribute(string name)
        {
            Name = name;
        }
    }
}
