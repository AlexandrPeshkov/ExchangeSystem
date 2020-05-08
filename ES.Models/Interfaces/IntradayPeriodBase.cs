using System;

namespace ES.Domain.Period
{
    public abstract class IntradayPeriodBase : PeriodBase, IIntradayPeriod
    {
        public abstract uint NumberOfSecond { get; }

        protected override DateTime ComputeTimestampByCorrectedPeriodCount(DateTime dateTime, int correctedPeriodCount)
            => dateTime.Truncate(TimeSpan.FromSeconds(NumberOfSecond)).AddSeconds(correctedPeriodCount * NumberOfSecond);
    }
}
