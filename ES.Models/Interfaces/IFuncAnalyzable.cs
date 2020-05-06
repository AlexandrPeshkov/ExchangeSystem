using System;

namespace ES.Domain.Interfaces
{
    // Marker interface for FuncAnalyzables
    public interface IFuncAnalyzable : IAnalyzable, IDisposable
    {
        object Func { get; }
    }

    public interface IFuncAnalyzable<TOutput> : IAnalyzable<TOutput>, IFuncAnalyzable
    {
    }
}
