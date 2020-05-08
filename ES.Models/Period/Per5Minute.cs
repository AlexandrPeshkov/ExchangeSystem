using System;
namespace ES.Domain.Period
{
    public class Per5Minute : IntradayPeriodBase
    {
        public override uint NumberOfSecond => 5 * 60;

        public override bool IsTimestamp(DateTime dateTime)
            => dateTime.Minute % 5 == 0 && dateTime.Second == 0 && dateTime.Millisecond == 0;
    }
}
