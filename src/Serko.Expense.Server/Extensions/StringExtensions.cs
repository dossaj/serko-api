using System;
using System.Globalization;
using System.Threading;

namespace Serko.Expense.Server.Extensions
{
    public static class StringExtensions
    {
        public static DateTime ToDateTime(this string value, params string[] formats)
        {
            if (formats.Length != 0)
            {
                if (DateTime.TryParseExact(value, formats, Thread.CurrentThread.CurrentCulture, DateTimeStyles.None, out var result))
                {
                    return result;
                }
            }
            return DateTime.Parse(value, Thread.CurrentThread.CurrentCulture, DateTimeStyles.None);
        }
    }
}