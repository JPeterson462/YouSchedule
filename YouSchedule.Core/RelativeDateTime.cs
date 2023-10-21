using System;

namespace YouSchedule.Core
{
    public class RelativeDateTime
    {
        public string DayOfWeekAsString { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }

        public int SecondsSinceSunday { get; set; }

        public RelativeDateTime(int secondsSinceSunday)
        {
            SecondsSinceSunday = secondsSinceSunday;

            int dayOfWeekAsInteger = secondsSinceSunday / Utils.SecondsPerDay;
            DayOfWeek = (DayOfWeek)dayOfWeekAsInteger;
            int secondsInDay = secondsSinceSunday - (dayOfWeekAsInteger * Utils.SecondsPerDay);
            Hour = secondsInDay / Utils.SecondsPerHour;
            secondsInDay -= Hour * Utils.SecondsPerHour;
            Minute = secondsInDay / 60;
            Second = secondsInDay - Minute * 60;

            DayOfWeekAsString = Enum.GetName(typeof(DayOfWeek), DayOfWeek);
        }
    }
}
