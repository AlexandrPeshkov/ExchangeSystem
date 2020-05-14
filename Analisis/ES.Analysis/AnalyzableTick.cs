using System;
using ES.Domain.Interfaces;

namespace ES.Analysis
{
    public class Analysis<T> : IAnalysis<T>
    {
        public DateTime Time { get; }

        public T Tick { get; }

        object IAnalysis.Tick => Tick;

        public Analysis(DateTime dateTime, T tick)
        {
            Tick = tick;
            Time = dateTime;
        }
    }
}
