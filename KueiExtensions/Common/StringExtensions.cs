using System.Collections.Generic;

namespace KueiExtensions.Common
{
    public static class StringExtensions
    {
        public static string Join(this IEnumerable<string> strs, string delimieter)
        {
            return string.Join(delimieter, strs);
        }

        public static decimal? ToNullableDecimal(this string str)
        {
            if (decimal.TryParse(str, out var result))
            {
                return result;
            }

            return null;
        }

        public static decimal ToDecimal(this string str)
        {
            if (decimal.TryParse(str, out var result))
            {
                return result;
            }

            return 0;
        }
    }
}
