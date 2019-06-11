using System;
using Hangfire;
using Hangfire.Annotations;
using TimeZoneConverter;

namespace AppCheckInNotifier.Application.Hangfire
{
    /// <summary>
    /// Implementation of the Hangfire <see cref="ITimeZoneResolver"/> using the <see cref="TZConvert"/> to
    /// get TimeZoneInfo giving a Windows or IANA time zone
    /// identifier, regardless of which platform the application is running on.
    /// </summary>
    public class TimeZoneResolver : ITimeZoneResolver
    {
        public TimeZoneInfo GetTimeZoneById([NotNull] string timeZoneId)
        {
            return TZConvert.GetTimeZoneInfo(timeZoneId);
        }
    }
}
