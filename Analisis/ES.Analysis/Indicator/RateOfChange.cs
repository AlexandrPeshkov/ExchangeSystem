using System.Collections.Generic;
using ES.Domain.Interfaces;

namespace ES.Analysis.Indicator
{
    public class RateOfChange : PercentageDifference<IOhlcv, Analysis<decimal?>>
    {
        public RateOfChange(IEnumerable<IOhlcv> inputs, int periodCount = 1)
            : base(inputs, i => i.Close, periodCount)
        {
        }
    }
}