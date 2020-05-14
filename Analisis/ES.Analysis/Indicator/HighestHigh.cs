using System.Collections.Generic;
using ES.Domain.Interfaces;

namespace ES.Analysis.Indicator
{
    public class HighestHigh : Highest<IOhlcv, Analysis<decimal?>>
    {
        public HighestHigh(IEnumerable<IOhlcv> inputs, int periodCount)
            : base(inputs, i => i.High, periodCount)
        {
        }
    }
}