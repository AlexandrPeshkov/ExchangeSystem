using System;
using System.Collections.Generic;
using System.Linq;
using ES.Domain.Interfaces;
using ES.Analysis.Infrastructure;
using ES.Analysis;

namespace ES.Analysis.Indicator
{
    public class RelativeStrengthIndex<TInput, TOutput> : NumericAnalyzableBase<TInput, decimal?, TOutput>
    {
        private readonly RelativeStrengthByTuple _rs;

        public RelativeStrengthIndex(IEnumerable<TInput> inputs, Func<TInput, decimal?> inputMapper, int periodCount) : base(inputs, inputMapper)
        {
            _rs = new RelativeStrengthByTuple(inputs.Select(inputMapper).ToList(), periodCount);

            PeriodCount = periodCount;
        }

        public int PeriodCount { get; }

        protected override decimal? ComputeByIndexImpl(IReadOnlyList<decimal?> mappedInputs, int index) => 100 - (100 / (1 + _rs[index]));
    }

    public class RelativeStrengthIndexByTuple : RelativeStrengthIndex<decimal?, decimal?>
    {
        public RelativeStrengthIndexByTuple(IEnumerable<decimal?> inputs, int periodCount)
            : base(inputs, i => i, periodCount)
        {
        }
    }

    public class RelativeStrengthIndex : RelativeStrengthIndex<IOhlcv, Analysis<decimal?>>
    {
        public RelativeStrengthIndex(IEnumerable<IOhlcv> inputs, int periodCount)
            : base(inputs, i => i.Close, periodCount)
        {
        }
    }
}
