using System;
using System.Collections.Generic;
using ES.Domain.Interfaces;
using ES.Analysis.Infrastructure;
using ES.Analysis;

namespace ES.Analysis
{
    public class SimpleRuleExecutor : RuleExecutorBase<IOhlcv, IIndexedOhlcv, IIndexedOhlcv>
    {
        public SimpleRuleExecutor(IAnalyzeContext<IOhlcv> context, Predicate<IIndexedOhlcv> rule)
            : base((l, i) => l, context, new[] { rule })
        {
        }

        protected override Func<IEnumerable<IOhlcv>, int, IIndexedOhlcv> IndexedObjectConstructor
            => (l, i) => new IndexedCandle(l, i);
    }
}
