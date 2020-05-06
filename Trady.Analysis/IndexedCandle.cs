using System.Collections.Generic;
using ES.Domain.Interfaces;

namespace ES.Analysis
{
    public class IndexedCandle : IndexedCandleBase
    {
        public IndexedCandle(IEnumerable<IOhlcv> candles, int index)
            : base(candles, index)
        {
        }

        protected override IIndexedOhlcv IndexedCandleConstructor(int index)
            => new IndexedCandle(BackingList, index) { Context = Context };
    }
}
