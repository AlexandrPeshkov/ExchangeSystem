using System;

namespace ES.Domain.Interfaces
{
    public interface IAnalysis : ITick
    {
        new DateTimeOffset? DateTime { get; }

        object Tick { get; }
    }

    public interface IAnalysis<T> : IAnalysis
    {
        new T Tick { get; }
    }
}
