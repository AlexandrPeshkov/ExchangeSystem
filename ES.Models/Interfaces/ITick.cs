using System;

namespace ES.Domain.Interfaces
{
    public interface ITick
    {
        DateTimeOffset DateTime { get; }
    }
}