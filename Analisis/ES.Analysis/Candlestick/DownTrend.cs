﻿using System;
using System.Collections.Generic;
using ES.Domain.Interfaces;
using ES.Analysis.Infrastructure;
using ES.Analysis;

namespace ES.Analysis.Candlestick
{
    public class DownTrend<TInput, TOutput> : AnalyzableBase<TInput, (decimal High, decimal Low), bool?, TOutput>
    {
        public DownTrend(IEnumerable<TInput> inputs, Func<TInput, (decimal High, decimal Low)> inputMapper, int periodCount = 3) : base(inputs, inputMapper)
        {
            PeriodCount = periodCount;
        }

        public int PeriodCount { get; }

        protected override bool? ComputeByIndexImpl(IReadOnlyList<(decimal High, decimal Low)> mappedInputs, int index)
        {
            if (index <= PeriodCount - 1)
                return default;

            for (var i = 0; i < PeriodCount; i++)
            {
                var isHighDecreasing = mappedInputs[index - i].High < mappedInputs[index - i - 1].High;
                var isLowDecreasing = mappedInputs[index - i].Low < mappedInputs[index - i - 1].Low;
                if (!isHighDecreasing || !isLowDecreasing)
                    return false;
            }

            return true;
        }
    }

    public class DownTrendByTuple : DownTrend<(decimal High, decimal Low), bool?>
    {
        public DownTrendByTuple(IEnumerable<(decimal High, decimal Low)> inputs, int periodCount = 3)
            : base(inputs, i => i, periodCount)
        {
        }
    }

    public class DownTrend : DownTrend<IOhlcv, Analysis<bool?>>
    {
        public DownTrend(IEnumerable<IOhlcv> inputs, int periodCount = 3)
            : base(inputs, i => (i.High, i.Low), periodCount)
        {
        }
    }
}
