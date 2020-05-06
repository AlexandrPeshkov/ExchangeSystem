using System;

namespace ES.Domain.Period
{
    public class Per15Minute : IntradayPeriodBase
    {
        public override uint NumberOfSecond => 15 * 60;

        public override bool IsTimestamp(DateTimeOffset dateTime)
            => dateTime.Minute % 15 == 0 && dateTime.Second == 0 && dateTime.Millisecond == 0;
    }
}