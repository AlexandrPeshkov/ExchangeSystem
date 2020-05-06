using System;
using ES.Domain.Interfaces;

namespace ES.Analysis
{
    public class Analysis<T> : IAnalysis<T>
    {
        public DateTimeOffset? DateTime { get; }

        public T Tick { get; }

        object IAnalysis.Tick => Tick;

        DateTimeOffset ITick.DateTime => DateTime.GetValueOrDefault();

        public Analysis(DateTimeOffset? dateTime, T tick)
        {
            Tick = tick;
            DateTime = dateTime;
        }
    }
}
