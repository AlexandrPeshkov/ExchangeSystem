﻿using System;

namespace ES.Domain.Period
{
    public class PerMinute : IntradayPeriodBase
    {
        public override uint NumberOfSecond => 60;

        public override bool IsTimestamp(DateTime dateTime)
            => dateTime.Millisecond == 0 && dateTime.Second == 0;
    }
}
