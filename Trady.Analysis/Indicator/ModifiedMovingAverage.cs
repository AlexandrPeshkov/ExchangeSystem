using System;
using System.Collections.Generic;
using System.Linq;
using ES.Domain.Interfaces;
using ES.Analysis.Infrastructure;
using ES.Analysis;

namespace ES.Analysis.Indicator
{
    public class ModifiedMovingAverage<TInput, TOutput> : NumericAnalyzableBase<TInput, decimal?, TOutput>
    {
        private readonly GenericMovingAverage _gma;

        public ModifiedMovingAverage(IEnumerable<TInput> inputs, Func<TInput, decimal?> inputMapper, int periodCount) : base(inputs, inputMapper)
        {
            _gma = new GenericMovingAverage(
                i => inputs.Select(inputMapper).ElementAt(i),
                Smoothing.Mma(periodCount),
                inputs.Count());

            PeriodCount = periodCount;
        }

        public int PeriodCount { get; }

        protected override decimal? ComputeByIndexImpl(IReadOnlyList<decimal?> mappedInputs, int index) => _gma[index];
    }

    public class ModifiedMovingAverageByTuple : ModifiedMovingAverage<decimal?, decimal?>
    {
        public ModifiedMovingAverageByTuple(IEnumerable<decimal?> inputs, int periodCount)
            : base(inputs, i => i, periodCount)
        {
        }

        public ModifiedMovingAverageByTuple(IEnumerable<decimal> inputs, int periodCount)
            : this(inputs.Cast<decimal?>(), periodCount)
        {
        }
    }

    public class ModifiedMovingAverage : ModifiedMovingAverage<IOhlcv, Analysis<decimal?>>
    {
        public ModifiedMovingAverage(IEnumerable<IOhlcv> inputs, int periodCount)
            : base(inputs, i => i.Close, periodCount)
        {
        }
    }
}