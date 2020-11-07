using System;

namespace OnlineAuction.Core.Extensions
{
    public class DateTimeExtensions
    {
        public static DateTime ThisWeekStartDay()
        {
            DayOfWeek day = DateTime.Now.DayOfWeek;
            DateTime start;

            if (day == (byte)DayOfWeek.Sunday)
                start = DateTime.Now.AddDays(-6);
            else
            {
                int days = day - DayOfWeek.Monday;
                start = DateTime.Now.AddDays(-days);
            }

            return start;
        }

        public static DateTime ThisWeekEndDay()
        {
            DayOfWeek day = DateTime.Now.DayOfWeek;
            DateTime start;

            if (day == (byte)DayOfWeek.Sunday)
                start = DateTime.Now.AddDays(-6);
            else
            {
                int days = day - DayOfWeek.Monday;
                start = DateTime.Now.AddDays(-days);
            }
            DateTime end = start.AddDays(6);

            return end;
        }
    }
}