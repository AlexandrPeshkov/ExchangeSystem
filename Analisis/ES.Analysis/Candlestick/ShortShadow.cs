﻿using System;
using System.Collections.Generic;
using ES.Domain.Interfaces;
using ES.Analysis.Infrastructure;
using ES.Analysis;

namespace ES.Analysis.Candlestick
{
    /// <summary>
    /// Reference: http://stockcharts.com/school/doku.php?id=chart_school:chart_analysis:candlestick_pattern_dictionary
    /// </summary>
    public class ShortShadow<TInput, TOutput> : AnalyzableBase<TInput, (decimal Open, decimal High, decimal Low, decimal Close), bool?, TOutput>
    {
        public ShortShadow(IEnumerable<TInput> inputs, Func<TInput, (decimal Open, decimal High, decimal Low, decimal Close)> inputMapper) : base(inputs, inputMapper)
        {
        }

        protected override bool? ComputeByIndexImpl(IReadOnlyList<(decimal Open, decimal High, decimal Low, decimal Close)> mappedInputs, int index)
        {
            throw new NotImplementedException();
        }
    }

    public class ShortShadowByTuple : ShortShadow<(decimal Open, decimal High, decimal Low, decimal Close), bool?>
    {
        public ShortShadowByTuple(IEnumerable<(decimal Open, decimal High, decimal Low, decimal Close)> inputs)
            : base(inputs, i => i)
        {
        }
    }

    public class ShortShadow : ShortShadow<IOhlcv, Analysis<bool?>>
    {
        public ShortShadow(IEnumerable<IOhlcv> inputs)
            : base(inputs, i => (i.Open, i.High, i.Low, i.Close))
        {
        }
    }
}