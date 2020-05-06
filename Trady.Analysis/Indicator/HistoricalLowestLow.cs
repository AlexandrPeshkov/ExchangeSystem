using System.Collections.Generic;
using ES.Domain.Interfaces;

namespace ES.Analysis.Indicator
{
    public class HistoricalLowestLow : HistoricalLowest<IOhlcv, Analysis<decimal?>>
    {
        public HistoricalLowestLow(IEnumerable<IOhlcv> inputs)
            : base(inputs, i => i.Low)
        {
        }
    }
}