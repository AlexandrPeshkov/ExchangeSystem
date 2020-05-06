using System.Collections.Generic;
using ES.Domain.Interfaces;

namespace ES.Analysis.Indicator
{
    public class HistoricalHighestClose : HistoricalHighest<IOhlcv, Analysis<decimal?>>
    {
        public HistoricalHighestClose(IEnumerable<IOhlcv> inputs)
            : base(inputs, i => i.Close)
        {
        }
    }
}