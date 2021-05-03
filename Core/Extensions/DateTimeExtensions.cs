using System;

namespace Core.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        ///     String To Date
        ///     is formats
        ///     1. MM.dd.yyyy
        ///     2. MM/dd/yyyy
        ///     3. MM-dd-yyyy
        /// </summary>
        /// <param name="source"></param>
        /// <param name="throwExceptionIfFailed"></param>
        /// <returns></returns>
        public static DateTime ToDate(this string source, bool throwExceptionIfFailed = false)
        {
            source ??= "";
            var valid = DateTime.TryParse(source, out var result);
            if (valid) return result;
            if (throwExceptionIfFailed)
                //throw new Exception($"{HelperMessages.CannotBeConvertedDateTime}");
                throw new Exception($"Cannot Be Converted DateTime");
            return result;
        }

        /// <summary>
        ///     String To Time
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(this string source)
        {
            return !TimeSpan.TryParse(source.Trim(), out var time) ? TimeSpan.Zero : time;
        }

        /// <summary>
        ///     Between Date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <returns></returns>
        public static bool Between(this DateTime date, DateTime first, DateTime last)
        {
            return date.Ticks >= first.Ticks && date.Ticks <= last.Ticks;
        }

        /// <summary>
        ///     Calculate
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int CalculateAge(this DateTime dateTime)
        {
            var age = DateTime.Now.Year - dateTime.Year;
            if (DateTime.Now < dateTime.AddYears(age))
                age--;
            return age;
        }

        /// <summary>
        ///     Readable Time
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToReadableTime(this DateTime value)
        {
            var ts = new TimeSpan(DateTime.Now.Ticks - value.Ticks);
            var delta = ts.TotalSeconds;
            var agoLater = value.Ticks < DateTime.Now.Ticks ? "ago" : "later";

            if (delta < 0)
            {
                ts = new TimeSpan(value.Ticks - DateTime.Now.Ticks);
                delta = ts.TotalSeconds;
            }

            switch (delta)
            {
                case < 60:
                    return ts.Seconds == 1 ? "one second " + agoLater : $"{ts.Seconds} seconds " + agoLater;
                case < 120:
                    return "a minute " + agoLater;
                // 45 * 60
                case < 2700:
                    return $"{ts.Minutes} minutes " + agoLater;
                // 90 * 60
                case < 5400:
                    return "an hour " + agoLater;
                // 24 * 60 * 60
                case < 86400:
                    return $"{ts.Hours} hours " + agoLater;
                // 48 * 60 * 60
                case < 172800:
                    return "yesterday";
                // 30 * 24 * 60 * 60
                case < 2592000:
                    return $"{ts.Days} days " + agoLater;
                // 12 * 30 * 24 * 60 * 60
                case < 31104000:
                {
                    var months = Convert.ToInt32(Math.Floor((double) ts.Days / 30));
                    return months <= 1 ? "one month " + agoLater : $"{months} months " + agoLater;
                }
                default:
                {
                    var years = Convert.ToInt32(Math.Floor((double) ts.Days / 365));
                    return years <= 1 ? "one year " + agoLater : $"{years} years " + agoLater;
                }
            }
        }

        /// <summary>
        ///     Readable Time
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToReadableTime(this DateTime? value)
        {
            return value == null ? "" : value.Value.ToReadableTime();
        }

        /// <summary>
        ///     To Day
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToDay(this DateTime value)
        {
            var ts = new TimeSpan(DateTime.Now.Ticks - value.Ticks);
            return (int) ts.TotalSeconds;
        }

        /// <summary>
        ///     Working Day
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool WorkingDay(this DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }

        /// <summary>
        ///     Is Weekend
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        ///     Next Workday
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime NextWorkday(this DateTime date)
        {
            var nextDay = date.AddDays(1);
            while (!nextDay.WorkingDay()) nextDay = nextDay.AddDays(1);

            return nextDay;
        }
    }
}