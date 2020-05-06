using System.Collections.Generic;
using ES.Domain.Interfaces;

namespace ES.Analysis.Indicator
{
    public class HistoricalLowestClose : HistoricalLowest<IOhlcv, Analysis<decimal?>>
    {
        public HistoricalLowestClose(IEnumerable<IOhlcv> inputs)
            : base(inputs, i => i.Close)
        {
        }
    }
}