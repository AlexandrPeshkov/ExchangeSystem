using System;
namespace ES.Domain.Period
{
    public class Per10Minute : IntradayPeriodBase
    {
        public override uint NumberOfSecond => 10 * 60;

        public override bool IsTimestamp(DateTime dateTime)
            => dateTime.Minute % 10 == 0 && dateTime.Second == 0 && dateTime.Millisecond == 0;
    }
}
