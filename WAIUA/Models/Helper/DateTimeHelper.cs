using System;
using System.Collections.Generic;
using System.Linq;

namespace WAIUA.Models.Helper
{
    public abstract class DateTimeHelper
    {
        public static DateTime GetDateTimeFromMilliseconds(ulong milliseconds) => (new DateTime(1970, 1, 1)).AddMilliseconds(milliseconds);
        public static DateTime GetDateTimeWithAddedMilliseconds(DateTime originalDateTime, ulong millisecondsToAdd) => originalDateTime.AddMilliseconds(millisecondsToAdd);
        public static DateTime GetDateTimeFromAddedMilliseconds(IEnumerable<ulong> milliseconds) {
            ulong sum = 0;
            foreach (ulong millisecondsToAdd in milliseconds) { 
                sum += millisecondsToAdd;
            }
            return GetDateTimeFromMilliseconds(sum);
        }
    }
}
