using System;
using System.Collections.Generic;
using ES.Domain.Interfaces;
using ES.Analysis.Indicator;

namespace ES.Analysis.Infrastructure
{
    internal static class FuncAnalyzableFactory
    {
        public static IFuncAnalyzable<TOutput> CreateAnalyzable<TInput, TOutput>(string name, IEnumerable<TInput> inputs, params decimal[] parameters)
        {
            var func = (Func<IReadOnlyList<TInput>, int, IReadOnlyList<decimal>, IAnalyzeContext<TInput>, decimal?>)FuncRegistry.Get(name);
            return new FuncAnalyzable<TInput, TOutput>(inputs, parameters).Init(func);
        }

        public static IFuncAnalyzable<IAnalysis<decimal?>> CreateAnalyzable(string name, IEnumerable<IOhlcv> candles, params decimal[] parameters)
            => CreateAnalyzable<IOhlcv, IAnalysis<decimal?>>(name, candles, parameters);
    }
}
