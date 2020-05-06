﻿using System;
using System.Collections.Generic;
using System.Linq;
using ES.Domain.Interfaces;
using ES.Analysis.Infrastructure;
using ES.Analysis;

namespace ES.Analysis.Indicator
{
    public class ChandelierExit<TInput, TOutput> : AnalyzableBase<TInput, (decimal High, decimal Low, decimal Close), (decimal? Long, decimal? Short), TOutput>
    {
        private readonly HighestByTuple _hh;
        private readonly LowestByTuple _ll;
        private readonly AverageTrueRangeByTuple _atr;

        public ChandelierExit(IEnumerable<TInput> inputs, Func<TInput, (decimal High, decimal Low, decimal Close)> inputMapper, int periodCount, decimal atrCount)
            : base(inputs, inputMapper)
        {
            _hh = new HighestByTuple(inputs.Select(i => inputMapper(i).High), periodCount);
            _ll = new LowestByTuple(inputs.Select(i => inputMapper(i).Low), periodCount);
            _atr = new AverageTrueRangeByTuple(inputs.Select(inputMapper), periodCount);

            PeriodCount = periodCount;
            AtrCount = atrCount;
        }

        public int PeriodCount { get; }

        public decimal AtrCount { get; }

        protected override (decimal? Long, decimal? Short) ComputeByIndexImpl(IReadOnlyList<(decimal High, decimal Low, decimal Close)> mappedInputs, int index)
        {
            var atr = _atr[index];
            var @long = _hh[index] - atr * AtrCount;
            var @short = _ll[index] + atr * AtrCount;
            return (@long, @short);
        }
    }

    public class ChandelierExitByTuple : ChandelierExit<(decimal High, decimal Low, decimal Close), (decimal? Long, decimal? Short)>
    {
        public ChandelierExitByTuple(IEnumerable<(decimal High, decimal Low, decimal Close)> inputs, int periodCount, decimal atrCount)
            : base(inputs, i => i, periodCount, atrCount)
        {
        }
    }

    public class ChandelierExit : ChandelierExit<IOhlcv, Analysis<(decimal? Long, decimal? Short)>>
    {
        public ChandelierExit(IEnumerable<IOhlcv> inputs, int periodCount, decimal atrCount)
            : base(inputs, i => (i.High, i.Low, i.Close), periodCount, atrCount)
        {
        }
    }
}