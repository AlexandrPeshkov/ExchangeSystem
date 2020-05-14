using System.Collections.Generic;
using ES.Domain.Interfaces;

namespace ES.Analysis.Indicator
{
    public class LowestLow : Lowest<IOhlcv, Analysis<decimal?>>
    {
        public LowestLow(IEnumerable<IOhlcv> inputs, int periodCount)
            : base(inputs, i => i.Low, periodCount)
        {
        }
    }
}