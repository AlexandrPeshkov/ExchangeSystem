using System;
using System.Collections.Generic;
using ES.Analysis.Indicator;
using ES.Domain.Interfaces;
using ES.Analysis;

namespace ES.Analysis.Extension
{
    public static class FuncExtension
    {
        public static FuncAnalyzable<IOhlcv, Analysis<decimal?>> AsAnalyzable(this Func<IReadOnlyList<IOhlcv>, int, IReadOnlyList<decimal>, IAnalyzeContext<IOhlcv>, decimal?> func, IEnumerable<IOhlcv> inputs, params decimal[] parameters)
            => new FuncAnalyzable(inputs, parameters).Init(func);

        public static FuncAnalyzable<TInput, decimal?> AsAnalyzable<TInput>(this Func<IReadOnlyList<TInput>, int, IReadOnlyList<decimal>, IAnalyzeContext<TInput>, decimal?> func, IEnumerable<TInput> inputs, params decimal[] parameters)
            => new FuncAnalyzable<TInput, decimal?>(inputs, parameters).Init(func);
    }
}
