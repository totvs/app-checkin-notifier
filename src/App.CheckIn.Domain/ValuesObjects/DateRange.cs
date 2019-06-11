using System;

namespace App.CheckIn.Domain.ValuesObjects
{
    /// <summary>
    /// Represents range of dates
    /// </summary>
    public class DateRange
    {
        public DateRange(DateTimeOffset start, DateTimeOffset end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// start of the range
        /// </summary>
        public DateTimeOffset Start { get; }

        /// <summary>
        /// end of the range
        /// </summary>
        public DateTimeOffset End { get; }
    }
}
