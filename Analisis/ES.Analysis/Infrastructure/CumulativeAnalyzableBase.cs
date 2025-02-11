﻿using System;
using System.Collections.Generic;
using ES.Analysis.Extension;

namespace ES.Analysis.Infrastructure
{
    public abstract class CumulativeAnalyzableBase<TInput, TMappedInput, TOutputToMap, TOutput> : AnalyzableBase<TInput, TMappedInput, TOutputToMap, TOutput>
    {
        protected CumulativeAnalyzableBase(IEnumerable<TInput> inputs, Func<TInput, TMappedInput> inputMapper) : base(inputs, inputMapper)
        {
        }

        protected sealed override TOutputToMap ComputeByIndexImpl(IReadOnlyList<TMappedInput> mappedInputs, int index)
        {
            // Init LastCacheIndex when ComputeByIndexImpl triggers
            LastCacheIndex = LastCacheIndex ?? InitialValueIndex;

            var tick = default(TOutputToMap);
            if (index < InitialValueIndex)
            {
                tick = ComputeNullValue(mappedInputs, index);
            }
            else if (index == InitialValueIndex)
            {
                tick = ComputeInitialValue(mappedInputs, index);
            }
            else
            {
                // get start index of calculation to cache
                //int cacheStartIndex = Cache.Keys.DefaultIfEmpty(InitialValueIndex).Where(k => k >= InitialValueIndex).Max();
                for (int i = LastCacheIndex.Value; i < index; i++)
                {
                    var prevTick = Cache.GetOrAdd(i, _i => ComputeByIndexImpl(mappedInputs, _i));
                    tick = ComputeCumulativeValue(mappedInputs, i + 1, prevTick);

                    // The result will be cached in the base class for the return tick
                    if (i < index - 1)
                    {
                        Cache.AddOrUpdate(i + 1, tick, (_i, _t) => tick);
                    }
                }
                LastCacheIndex = index - 1;
            }

            return tick;
        }

        private int? LastCacheIndex { get; set; }

        protected virtual int InitialValueIndex { get; } = 0;

        protected virtual TOutputToMap ComputeNullValue(IReadOnlyList<TMappedInput> mappedInputs, int index) => default;

        protected abstract TOutputToMap ComputeInitialValue(IReadOnlyList<TMappedInput> mappedInputs, int index);

        protected abstract TOutputToMap ComputeCumulativeValue(IReadOnlyList<TMappedInput> mappedInputs, int index, TOutputToMap prevOutputToMap);
    }

    public abstract class CumulativeAnalyzableBase<TInput, TOutput> : CumulativeAnalyzableBase<TInput, TInput, TOutput, TOutput>
    {
        protected CumulativeAnalyzableBase(IEnumerable<TInput> inputs)
            : base(inputs, i => i)
        {
        }
    }
}
