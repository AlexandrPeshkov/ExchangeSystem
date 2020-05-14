using System.Collections.Generic;
using ES.Domain.Interfaces;

namespace ES.Analysis.Indicator
{
    public class HistoricalHighestHigh : HistoricalHighest<IOhlcv, Analysis<decimal?>>
    {
        public HistoricalHighestHigh(IEnumerable<IOhlcv> inputs)
            : base(inputs, i => i.High)
        {
        }
    }
}