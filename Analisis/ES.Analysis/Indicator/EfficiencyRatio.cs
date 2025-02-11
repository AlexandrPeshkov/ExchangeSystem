﻿using System;
using System.Collections.Generic;
using System.Linq;
using ES.Domain.Interfaces;
using ES.Analysis.Infrastructure;
using ES.Analysis;

namespace ES.Analysis.Indicator
{
    public class EfficiencyRatio<TInput, TOutput> : NumericAnalyzableBase<TInput, decimal?, TOutput>
    {
        public EfficiencyRatio(IEnumerable<TInput> inputs, Func<TInput, decimal?> inputMapper, int periodCount) : base(inputs, inputMapper)
        {
            PeriodCount = periodCount;
        }

        public int PeriodCount { get; }

        protected override decimal? ComputeByIndexImpl(IReadOnlyList<decimal?> mappedInputs, int index)
        {
            if (index <= 0 || index < PeriodCount)
                return default;

            if (!mappedInputs[index].HasValue || !mappedInputs[index - PeriodCount].HasValue)
                return default;

            var change = Math.Abs(mappedInputs[index].Value - mappedInputs[index - PeriodCount].Value);
            var volatility = Enumerable.Range(index - PeriodCount + 1, PeriodCount).Select(i =>
            {
                if (!mappedInputs[i].HasValue || !mappedInputs[i - 1].HasValue)
                    return default(decimal?);

                return Math.Abs(mappedInputs[i].Value - mappedInputs[i - 1].Value);
            }).Sum();
            return volatility > 0 ? change / volatility : default;
        }
    }

    public class EfficiencyRatioByTuple : EfficiencyRatio<decimal?, decimal?>
    {
        public EfficiencyRatioByTuple(IEnumerable<decimal?> inputs, int periodCount)
            : base(inputs, i => i, periodCount)
        {
        }
    }

    public class EfficiencyRatio : EfficiencyRatio<IOhlcv, Analysis<decimal?>>
    {
        public EfficiencyRatio(IEnumerable<IOhlcv> inputs, int periodCount)
            : base(inputs, i => i.Close, periodCount)
        {
        }
    }
}
