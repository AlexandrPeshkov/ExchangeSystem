using System;

namespace ES.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime UnixTimeStampToDateTime(this long unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static long ToUnixTimeStamp(this DateTime dateTime)
        {
            long timestamp = new DateTimeOffset(dateTime).ToUnixTimeSeconds();
            return timestamp;
        }
    }
}
