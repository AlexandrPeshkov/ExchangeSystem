using System;

namespace ES.Domain.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumNameAttribute : Attribute
    {
        public string Name { get; set; }

        public EnumNameAttribute(string name)
        {
            Name = name;
        }
    }
}
