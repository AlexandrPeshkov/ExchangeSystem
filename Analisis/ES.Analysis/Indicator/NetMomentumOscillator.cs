using System;
using System.Collections.Generic;
using System.Linq;
using ES.Domain.Interfaces;
using ES.Analysis.Infrastructure;
using ES.Analysis;

namespace ES.Analysis.Indicator
{
    public class NetMomentumOscillator<TInput, TOutput> : NumericAnalyzableBase<TInput, decimal?, TOutput>
    {
        private RelativeStrengthIndexByTuple _rsi;

        public int PeriodCount { get; }

        public NetMomentumOscillator(IEnumerable<TInput> inputs, Func<TInput, decimal?> inputMapper, int periodCount) : base(inputs, inputMapper)
        {
            _rsi = new RelativeStrengthIndexByTuple(inputs.Select(inputMapper), periodCount);

            PeriodCount = periodCount;
        }

        protected override decimal? ComputeByIndexImpl(IReadOnlyList<decimal?> mappedInputs, int index)
            => 2 * _rsi[index] - 100;
    }

    public class NetMomentumOscillatorByTuple : NetMomentumOscillator<decimal?, decimal?>
    {
        public NetMomentumOscillatorByTuple(IEnumerable<decimal?> inputs, int periodCount)
            : base(inputs, i => i, periodCount)
        {
        }
    }

    public class NetMomentumOscillator : NetMomentumOscillator<IOhlcv, Analysis<decimal?>>
    {
        public NetMomentumOscillator(IEnumerable<IOhlcv> inputs, int periodCount)
            : base(inputs, i => i.Close, periodCount)
        {
        }
    }
}
