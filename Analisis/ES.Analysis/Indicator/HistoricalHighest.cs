﻿using System;
using System.Collections.Generic;
using ES.Analysis.Infrastructure;
using ES.Analysis;

namespace ES.Analysis.Indicator
{
    public class HistoricalHighest<TInput, TOutput> : CumulativeNumericAnalyzableBase<TInput, decimal, TOutput>
    {
        public HistoricalHighest(IEnumerable<TInput> inputs, Func<TInput, decimal> inputMapper) : base(inputs, inputMapper)
        {
        }

        //protected override decimal? ComputeByIndexImpl(IReadOnlyList<decimal> mappedInputs, int index)
        //    => mappedInputs.Take(index + 1).Max();

        protected override decimal? ComputeCumulativeValue(IReadOnlyList<decimal> mappedInputs, int index, decimal? prevOutputToMap)
            => mappedInputs[index] > prevOutputToMap ? mappedInputs[index] : prevOutputToMap;

        protected override decimal? ComputeInitialValue(IReadOnlyList<decimal> mappedInputs, int index)
            => mappedInputs[index];
    }

    public class HistoricalHighestByTuple : HistoricalHighest<decimal, decimal?>
    {
        public HistoricalHighestByTuple(IEnumerable<decimal> inputs)
            : base(inputs, i => i)
        {
        }
    }
}
