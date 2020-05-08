using System;

namespace ES.Domain.Period
{
    public abstract class InterdayPeriodBase : PeriodBase, IInterdayPeriod
    {
        protected InterdayPeriodBase() : base()
        {
        }

        public abstract uint OrderOfTransformation { get; }

        protected abstract DateTime FloorByDay(DateTime dateTime, bool isPositivePeriodCount);
    }
}
