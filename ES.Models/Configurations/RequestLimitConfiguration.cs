namespace ES.Domain.Configurations
{
    /// <summary>
    /// Параметры ограничений запросов к хосту
    /// </summary>
    public class RequestLimitConfiguration
    {
        public int SecondLimit { get; set; }

        public int MinuteLimit { get; set; }

        public int DayLimit { get; set; }

        public int MonthLimit { get; set; }

        public int TotalLimit { get; set; }
    }
}
