using System;
using YouSchedule.Core;
using YouSchedule.Predictor.Models;

namespace YouSchedule.Predictor.Algorithm
{
    public static class Mappers
    {
        public static Tuple<uint, uint> MapVideoToOffsetDuration(Video v)
        {
            DateTimeOffset startOfWeek = Utils.GetStartOfWeek(v.DatePublished);
            TimeSpan tzOffset = TimeZoneInfo.Local.GetUtcOffset(startOfWeek.DateTime);
            TimeSpan timeSinceWeekStarted = v.DatePublished.UtcDateTime.Subtract(startOfWeek.UtcDateTime);
            timeSinceWeekStarted = timeSinceWeekStarted.Subtract(tzOffset);
            Console.WriteLine(timeSinceWeekStarted.TotalSeconds + " seconds since "
                + startOfWeek.UtcDateTime + " -> " + v.DatePublished.UtcDateTime);
            uint x = (uint)Math.Floor(timeSinceWeekStarted.TotalSeconds), y = v.DurationSeconds;
            return new Tuple<uint, uint>(x, y);
        }
    }
}
