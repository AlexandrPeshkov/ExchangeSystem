using System;
using System.Collections.Generic;
using ES.Analysis.Infrastructure;
using ES.Domain.Interfaces;

namespace ES.Analysis.Backtest
{
    public class BuySellRuleExecutor : RuleExecutorBase<IOhlcv, IIndexedOhlcv, (TransactionType, IIndexedOhlcv)?>
    {
        public BuySellRuleExecutor(
            Func<IIndexedOhlcv, int, (TransactionType, IIndexedOhlcv)?> outputFunc,
            IAnalyzeContext<IOhlcv> context,
            Predicate<IIndexedOhlcv> buyRule,
            Predicate<IIndexedOhlcv> sellRule)
            : base(outputFunc, context, new[] { buyRule, sellRule })
        {
        }

        protected override Func<IEnumerable<IOhlcv>, int, IIndexedOhlcv> IndexedObjectConstructor
            => (l, i) => new IndexedCandle(l, i);
    }
}
