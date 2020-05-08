using System;
using ES.Domain.Interfaces;

namespace ES.Analysis
{
    public class Analysis<T> : IAnalysis<T>
    {
        public DateTime DateTime { get; }

        public T Tick { get; }

        object IAnalysis.Tick => Tick;

        public Analysis(DateTime dateTime, T tick)
        {
            Tick = tick;
            DateTime = dateTime;
        }
    }
}
