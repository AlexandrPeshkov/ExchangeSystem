using System;
using System.Collections.Generic;
using ES.Analysis.Infrastructure;
using ES.Analysis;

namespace ES.Analysis.Indicator
{
    public class HistoricalLowest<TInput, TOutput> : CumulativeNumericAnalyzableBase<TInput, decimal, TOutput>
    {
        public HistoricalLowest(IEnumerable<TInput> inputs, Func<TInput, decimal> inputMapper) : base(inputs, inputMapper)
        {
        }

        protected override decimal? ComputeCumulativeValue(IReadOnlyList<decimal> mappedInputs, int index, decimal? prevOutputToMap)
            => mappedInputs[index] < prevOutputToMap ? mappedInputs[index] : prevOutputToMap;

        protected override decimal? ComputeInitialValue(IReadOnlyList<decimal> mappedInputs, int index)
            => mappedInputs[index];
    }

    public class HistoricalLowestByTuple : HistoricalLowest<decimal, decimal?>
    {
        public HistoricalLowestByTuple(IEnumerable<decimal> inputs)
            : base(inputs, i => i)
        {
        }
    }
}
