﻿using System;
using System.Collections.Generic;
using System.Linq;
using ES.Domain.Interfaces;
using ES.Analysis.Extension;
using ES.Analysis.Infrastructure;
using ES.Analysis;

namespace ES.Analysis.Indicator
{
    public class Aroon<TInput, TOutput> : AnalyzableBase<TInput, (decimal High, decimal Low), (decimal? Up, decimal? Down), TOutput>
    {
        private readonly HighestByTuple _hh;
        private readonly LowestByTuple _ll;

        protected Aroon(IEnumerable<TInput> inputs, Func<TInput, (decimal High, decimal Low)> inputMapper, int periodCount)
            : base(inputs, inputMapper)
        {
            _hh = new HighestByTuple(inputs.Select(i => inputMapper(i).High), periodCount);
            _ll = new LowestByTuple(inputs.Select(i => inputMapper(i).Low), periodCount);
            PeriodCount = periodCount;
        }

        public int PeriodCount { get; }

        protected override (decimal? Up, decimal? Down) ComputeByIndexImpl(IReadOnlyList<(decimal High, decimal Low)> mappedInputs, int index)
        {
            if (index < PeriodCount - 1)
                return (default, default);

            var nearestIndexToHighestHigh = index - PeriodCount + 1 + mappedInputs
                .Skip(index - PeriodCount + 1)
                .Take(PeriodCount)
                .FindLastIndexOrDefault(i => i.High == _hh[index]);

            var nearestIndexToLowestLow = index - PeriodCount + 1 + mappedInputs
                .Skip(index - PeriodCount + 1)
                .Take(PeriodCount)
                .FindLastIndexOrDefault(i => i.Low == _ll[index]);

            var up = 100.0m * (PeriodCount - (index - nearestIndexToHighestHigh)) / PeriodCount;
            var down = 100.0m * (PeriodCount - (index - nearestIndexToLowestLow)) / PeriodCount;

            return (up, down);
        }
    }

    public class AroonByTuple : Aroon<(decimal High, decimal Low), (decimal? Up, decimal? Down)>
    {
        public AroonByTuple(IEnumerable<(decimal High, decimal Low)> inputs, int periodCount)
            : base(inputs, i => i, periodCount)
        {
        }
    }

    public class Aroon : Aroon<IOhlcv, Analysis<(decimal? Up, decimal? Down)>>
    {
        public Aroon(IEnumerable<IOhlcv> inputs, int periodCount)
            : base(inputs, i => (i.High, i.Low), periodCount)
        {
        }
    }
}
