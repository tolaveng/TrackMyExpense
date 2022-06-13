using Core.Domain.Constants;
using System.Globalization;

namespace Core.Application.Utils
{
    public static class DateTimeUtil
    {
        public const string DefaultTimeZoneId = "Australia/Sydney";

        public static string GetDateTimeFormatFromCultureInfo(string cultureInfoName)
        {
            if (string.IsNullOrWhiteSpace(cultureInfoName)) return DefaultConstants.DefaultDateTimeFormat;

            var cultureInfo = new CultureInfo(cultureInfoName);
            return new string(cultureInfo.DateTimeFormat.ShortDatePattern.Where(z => char.IsAscii(z)).ToArray());
        }

        public static string FormatDateTimeByCultureInfo(DateTime dateTime, string cultureInfoName)
        {
            if (string.IsNullOrWhiteSpace(cultureInfoName))
            {
                return dateTime.ToString(DefaultConstants.DefaultDateTimeFormat);
            }

            var cultureInfo = new CultureInfo(cultureInfoName);
            var shortDate = new string(cultureInfo.DateTimeFormat.ShortDatePattern.Where(z => char.IsAscii(z)).ToArray());
            return dateTime.ToString(shortDate);
        }

        public static TimeZoneInfo GetTimeZoneInfoOrDefault(string timeZoneId)
        {
            try
            {
                return TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            }
            catch (Exception)
            {
                return TimeZoneInfo.FindSystemTimeZoneById(DefaultTimeZoneId);
            }
        }

        public static DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }

        public static DateTime TimeZoneNow(string timeZoneId)
        {
            var utc = DateTime.UtcNow;
            return TimeZoneInfo.ConvertTimeFromUtc(utc, GetTimeZoneInfoOrDefault(timeZoneId));
        }

        public static DateTime ToTimeZoneDateTime(DateTime utcDateTime, string timeZoneId)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, GetTimeZoneInfoOrDefault(timeZoneId));
        }

        public static DateTime ToUtcDateTime(DateTime timeZoneDateTime, string timeZoneId)
        {
            return TimeZoneInfo.ConvertTimeToUtc(timeZoneDateTime, GetTimeZoneInfoOrDefault(timeZoneId));
        }

        public static DateTime StartOfDayUtc(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, DateTimeKind.Utc);
        }

        public static DateTime EndOfDayUtc(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, DateTimeKind.Utc);
        }
    }
}
