using System;
using System.Collections.Generic;
using System.Linq;
using ES.Domain.Interfaces;
using ES.Analysis.Infrastructure;
using ES.Analysis;

namespace ES.Analysis.Indicator
{
    public class TrueRange<TInput, TOutput> : NumericAnalyzableBase<TInput, (decimal High, decimal Low, decimal Close), TOutput>
    {
        public TrueRange(IEnumerable<TInput> inputs, Func<TInput, (decimal High, decimal Low, decimal Close)> inputMapper) : base(inputs, inputMapper)
        {
        }

        protected override decimal? ComputeByIndexImpl(IReadOnlyList<(decimal High, decimal Low, decimal Close)> mappedInputs, int index)
            => index > 0 ? new List<decimal?> {
                mappedInputs[index].High - mappedInputs[index].Low,
                Math.Abs(mappedInputs[index].High - mappedInputs[index - 1].Close),
                Math.Abs(mappedInputs[index].Low - mappedInputs[index - 1].Close) }.Max() : default;
    }

    public class TrueRangeByTuple : TrueRange<(decimal High, decimal Low, decimal Close), decimal?>
    {
        public TrueRangeByTuple(IEnumerable<(decimal High, decimal Low, decimal Close)> inputs)
            : base(inputs, i => i)
        {
        }
    }

    public class TrueRange : TrueRange<IOhlcv, Analysis<decimal?>>
    {
        public TrueRange(IEnumerable<IOhlcv> inputs)
            : base(inputs, i => (i.High, i.Low, i.Close))
        {
        }
    }
}
