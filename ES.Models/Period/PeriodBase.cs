﻿using System;

namespace ES.Domain.Period
{
    public abstract class PeriodBase : IPeriod
    {
        public abstract bool IsTimestamp(DateTime dateTime);

        public DateTime NextTimestamp(DateTime dateTime) => TimestampAt(dateTime, 1);

        public DateTime PrevTimestamp(DateTime dateTime) => TimestampAt(dateTime, -1);

        public DateTime TimestampAt(DateTime dateTime, int periodCount)
        {
            if (periodCount == 0)
                throw new ArgumentException("Timestamp at 0 is undefined, you should use non-zero periodCount");

            // periodCount-1 if periodCount is negative & not a timestamp, since there is truncation in internal implementation.
            var correctedPeriodCount = periodCount + ((periodCount < 0 && !IsTimestamp(dateTime)) ? 1 : 0);
            return ComputeTimestampByCorrectedPeriodCount(dateTime, correctedPeriodCount);
        }

        protected abstract DateTime ComputeTimestampByCorrectedPeriodCount(DateTime dateTime, int correctedPeriodCount);
    }
}
