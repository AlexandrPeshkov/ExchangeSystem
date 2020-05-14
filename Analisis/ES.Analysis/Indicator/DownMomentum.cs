using System.Collections.Generic;
using ES.Domain.Interfaces;

namespace ES.Analysis.Indicator
{
    public class DownMomentum : NegativeDifference<IOhlcv, Analysis<decimal?>>
    {
        public DownMomentum(IEnumerable<IOhlcv> inputs, int periodCount = 1)
            : base(inputs, i => i.Close, periodCount)
        {
        }
    }
}
