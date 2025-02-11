﻿using System;
using System.Collections.Generic;
using System.Linq;
using ES.Domain.Interfaces;
using ES.Analysis.Infrastructure;
using ES.Analysis;

namespace ES.Analysis.Indicator
{
    public class MinusDirectionalIndicator<TInput, TOutput> : NumericAnalyzableBase<TInput, (decimal High, decimal Low, decimal Close), TOutput>
    {
        private PlusDirectionalMovementByTuple _pdm;
        private MinusDirectionalMovementByTuple _mdm;
        private readonly GenericMovingAverage _tmdmEma;
        private readonly AverageTrueRangeByTuple _atr;

        public MinusDirectionalIndicator(IEnumerable<TInput> inputs, Func<TInput, (decimal High, decimal Low, decimal Close)> inputMapper, int periodCount) : base(inputs, inputMapper)
        {
            _pdm = new PlusDirectionalMovementByTuple(inputs.Select(i => inputMapper(i).High));
            _mdm = new MinusDirectionalMovementByTuple(inputs.Select(i => inputMapper(i).Low));

            Func<int, decimal?> tmdm = i => _mdm[i] > 0 && _pdm[i] < _mdm[i] ? _mdm[i] : 0;

            _tmdmEma = new GenericMovingAverage(
                periodCount,
                i => Enumerable.Range(i - periodCount + 1, periodCount).Average(tmdm),
                tmdm,
                Smoothing.Mma(periodCount),
                inputs.Count());

            _atr = new AverageTrueRangeByTuple(inputs.Select(inputMapper), periodCount);

            PeriodCount = periodCount;
        }

        public int PeriodCount { get; }

        protected override decimal? ComputeByIndexImpl(IReadOnlyList<(decimal High, decimal Low, decimal Close)> mappedInputs, int index)
        {
            var currentAtr = _atr[index];
            return currentAtr == 0 ? default : _tmdmEma[index] / currentAtr * 100;
        }
    }

    public class MinusDirectionalIndicatorByTuple : MinusDirectionalIndicator<(decimal High, decimal Low, decimal Close), decimal?>
    {
        public MinusDirectionalIndicatorByTuple(IEnumerable<(decimal High, decimal Low, decimal Close)> inputs, int periodCount)
            : base(inputs, i => i, periodCount)
        {
        }
    }

    public class MinusDirectionalIndicator : MinusDirectionalIndicator<IOhlcv, Analysis<decimal?>>
    {
        public MinusDirectionalIndicator(IEnumerable<IOhlcv> inputs, int periodCount)
            : base(inputs, i => (i.High, i.Low, i.Close), periodCount)
        {
        }
    }
}
