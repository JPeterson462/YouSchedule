using System;
namespace YouSchedule.Predictor.Algorithm
{
    public static class Utils
    {
        public static DateTimeOffset GetStartOfWeek(DateTimeOffset dateTime, DayOfWeek startOfWeek = DayOfWeek.Sunday)
        {
            int diff = (7 + (dateTime.DayOfWeek - startOfWeek)) % 7;
            return dateTime.AddDays(-1 * diff).Date;
        }
    }
}
