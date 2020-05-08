using System;

namespace ES.Domain.Interfaces
{
    public interface IAnalysis : ITick
    {
        object Tick { get; }
    }

    public interface IAnalysis<T> : IAnalysis
    {
        new T Tick { get; }
    }
}
