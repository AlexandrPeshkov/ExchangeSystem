﻿using System;

namespace ES.Domain.Period
{
    public class PerSecond : IntradayPeriodBase
    {
        public override uint NumberOfSecond => 1;

        public override bool IsTimestamp(DateTime dateTime)
            => dateTime.Millisecond == 0;
    }
}
