﻿using System;

namespace ES.Domain.Period
{
    public class Monthly : InterdayPeriodBase
    {
        public Monthly() : base()
        {
        }

        public override uint OrderOfTransformation => 30;

        public override bool IsTimestamp(DateTime dateTime)
            => dateTime.Day == 1 && dateTime.TimeOfDay == new TimeSpan(0, 0, 0);

        protected override DateTime ComputeTimestampByCorrectedPeriodCount(DateTime dateTime, int correctedPeriodCount)
            => new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(correctedPeriodCount);

        protected override DateTime FloorByDay(DateTime dateTime, bool isPositivePeriodCount)
            => dateTime.AddDays(1);
    }
}
