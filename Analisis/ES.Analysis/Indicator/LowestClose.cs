using System.Collections.Generic;
using ES.Domain.Interfaces;

namespace ES.Analysis.Indicator
{
    public class LowestClose : Lowest<IOhlcv, Analysis<decimal?>>
    {
        public LowestClose(IEnumerable<IOhlcv> inputs, int periodCount)
            : base(inputs, i => i.Close, periodCount)
        {
        }
    }
}