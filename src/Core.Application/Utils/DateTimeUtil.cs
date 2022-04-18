namespace Core.Application.Utils
{
    public static class DateTimeUtil
    {
        public const string DefaultTimeZoneId = "Australia/Sydney";

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

        public static DateTime ToUtc(DateTime timeZoneDateTime)
        {
            return timeZoneDateTime.ToUniversalTime();
        }
    }
}
