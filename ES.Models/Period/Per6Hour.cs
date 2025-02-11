﻿using System;

namespace ES.Domain.Period
{
    public class Per6Hour : IntradayPeriodBase
    {
        public override uint NumberOfSecond => 6 * 60 * 60;

        public override bool IsTimestamp(DateTime dateTime)
            => dateTime.Hour % 6 == 0 && dateTime.Minute == 0 && dateTime.Second == 0 && dateTime.Millisecond == 0;
    }
}
