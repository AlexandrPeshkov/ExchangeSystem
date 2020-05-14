using System;
using System.Collections.Generic;
using System.Linq;
using ES.Domain.Interfaces;
using ES.Analysis.Infrastructure;
using ES.Analysis;

namespace ES.Analysis.Indicator
{
    public class StochasticsMomentum<TInput, TOutput> : NumericAnalyzableBase<TInput, (decimal High, decimal Low, decimal Close), TOutput>
    {
        private readonly HighestByTuple _hh;
        private readonly LowestByTuple _ll;

        public int PeriodCount { get; }

        public StochasticsMomentum(IEnumerable<TInput> inputs, Func<TInput, (decimal High, decimal Low, decimal Close)> inputMapper, int periodCount) : base(inputs, inputMapper)
        {
            _hh = new HighestByTuple(inputs.Select(inputMapper).Select(i => i.High), periodCount);
            _ll = new LowestByTuple(inputs.Select(inputMapper).Select(i => i.Low), periodCount);

            PeriodCount = periodCount;
        }

        protected override decimal? ComputeByIndexImpl(IReadOnlyList<(decimal High, decimal Low, decimal Close)> mappedInputs, int index)
            => mappedInputs[index].Close - 0.5m * (_hh[index] + _ll[index]);
    }

    public class StochasticsMomentumByTuple : StochasticsMomentum<(decimal High, decimal Low, decimal Close), decimal?>
    {
        public StochasticsMomentumByTuple(IEnumerable<(decimal High, decimal Low, decimal Close)> inputs, int periodCount)
            : base(inputs, i => i, periodCount)
        {
        }
    }

    public class StochasticsMomentum : StochasticsMomentum<IOhlcv, Analysis<decimal?>>
    {
        public StochasticsMomentum(IEnumerable<IOhlcv> inputs, int periodCount)
            : base(inputs, i => (i.High, i.Low, i.Close), periodCount)
        {
        }
    }
}
