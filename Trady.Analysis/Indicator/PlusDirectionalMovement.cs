using System;
using System.Collections.Generic;
using ES.Domain.Interfaces;
using ES.Analysis.Infrastructure;
using ES.Analysis;

namespace ES.Analysis.Indicator
{
    public class PlusDirectionalMovement<TInput, TOutput> : NumericAnalyzableBase<TInput, decimal, TOutput>
    {
        public PlusDirectionalMovement(IEnumerable<TInput> inputs, Func<TInput, decimal> inputMapper) : base(inputs, inputMapper)
        {
        }

        protected override decimal? ComputeByIndexImpl(IReadOnlyList<decimal> mappedInputs, int index)
            => index > 0 ? (decimal?)mappedInputs[index] - mappedInputs[index - 1] : default;
    }

    public class PlusDirectionalMovementByTuple : PlusDirectionalMovement<decimal, decimal?>
    {
        public PlusDirectionalMovementByTuple(IEnumerable<decimal> inputs)
            : base(inputs, i => i)
        {
        }
    }

    public class PlusDirectionalMovement : PlusDirectionalMovement<IOhlcv, Analysis<decimal?>>
    {
        public PlusDirectionalMovement(IEnumerable<IOhlcv> inputs)
            : base(inputs, i => i.High)
        {
        }
    }
}
