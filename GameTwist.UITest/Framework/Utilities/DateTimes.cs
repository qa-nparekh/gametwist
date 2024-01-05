using System;

namespace GTAutomation.Framework.Utilities
{
    public class DateTimes
    {
        public static long CurrentTimestamp()
        {
            return (long)(DateTime.UtcNow.Subtract(new DateTime(1970 , 1 , 1)).TotalSeconds * 1000);
        }
    }
}
