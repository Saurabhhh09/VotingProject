using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowModels
{
    public static class Time
    {
        public static string ConvertUtcToLocalTime(this DateTime utcDateTime)
        {
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
            DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, localTimeZone);
            return localDateTime.ToString("MMMM-dd-yyyy");
        }
    }
}
