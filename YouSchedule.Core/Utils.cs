using System;

namespace YouSchedule.Core
{
    public static class Utils
    {
        public static readonly int SecondsPerWeek = 7 * 24 * 60 * 60; // technically not true during daylight savings, but shouldn't affect clustering calculations

        public static readonly int SecondsPerDay = 24 * 60 * 60;

        public static readonly int SecondsPerHour = 60 * 60;

        public static DateTimeOffset GetStartOfWeek(DateTimeOffset dateTime, DayOfWeek startOfWeek = DayOfWeek.Sunday)
        {
            int diff = (7 + (dateTime.DayOfWeek - startOfWeek)) % 7;
            return dateTime.AddDays(-1 * diff).Date;
        }

        public static int FindModulusDistance(int x, int y, int mod)
        {
            if (x > y)
            {
                int tmp = x;
                x = y;
                y = tmp;
            }
            // if the points are ordered then either are nearby or x is on the left and y is on the right
            return Math.Min(Math.Abs(y - x), Math.Abs(y - (x + mod)));
        }
    }
}
