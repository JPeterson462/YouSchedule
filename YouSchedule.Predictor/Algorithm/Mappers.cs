using System;
using YouSchedule.Predictor.Models;

namespace YouSchedule.Predictor.Algorithm
{
    public static class Mappers
    {
        public static Tuple<int, int> MapVideoToOffsetDuration(Video v)
        {
            TimeSpan timeSinceWeekStarted = v.DatePublished.Subtract(Utils.GetStartOfWeek(v.DatePublished));
            int x = (int)timeSinceWeekStarted.TotalSeconds, y = v.DurationSeconds;
            return new Tuple<int, int>(x, y);
        }
    }
}
