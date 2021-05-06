using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace KueiExtensions
{
    public static class StringExtensions
    {
        public static string Join(this IEnumerable<string> strs, string delimieter = "")
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

        public static string Utf8Encode(this string s)
        {
            var charsInutf8 = s.Select(c =>
                                       {
                                           var charInBytes = UTF8Encoding.Unicode.GetBytes(c.ToString());

                                           //charInBytes.Dump();   // 60 00

                                           // \u0100 結構 \u 01 00
                                           // 01 的位數高 - 十六進位
                                           // 00 的位數低 - 十六進位

                                           var lowHex  = charInBytes[0].ToString("X2");
                                           var highHex = charInBytes[1].ToString("X2");

                                           var charInUtf8 = @$"\u{highHex}{lowHex}";
                                           return charInUtf8;
                                       })
                               .Join();

            return charsInutf8;
        }

        public static string Utf8Decode(this string s)
        {
            // 以 \u 拆分成各個字
            var bytes = s.Split(@"\u")
                         .Where(p => !string.IsNullOrWhiteSpace(p))
                         .SelectMany(word =>
                                     {
                                         //word.Dump();

                                         // 將十六進位的字串拆成二組，每組二個字串
                                         var highHex = word.Substring(0, 2);
                                         var lowHex  = word.Substring(2, 2);

                                         // 將每組十六進位字串，轉成 Byte
                                         var highByte = Byte.Parse(highHex, System.Globalization.NumberStyles.HexNumber);
                                         var lowByte  = Byte.Parse(lowHex,  System.Globalization.NumberStyles.HexNumber);

                                         // 還原成原本的 ByteArray
                                         var bytes = new[] { lowByte, highByte };

                                         return bytes;
                                     })
                         .ToArray();

            string result = Encoding.Unicode.GetString(bytes);

            return result;
        }

        public static Guid? ToNullableGuid(this string str)
        {
            if (Guid.TryParse(str, out var result))
            {
                return result;
            }

            return null;
        }

        public static DateTime? ToNullableDateTime(this string      str,
                                                   string           format,
                                                   IFormatProvider? formatProvider = null,
                                                   DateTimeStyles   dateTimeStyles = DateTimeStyles.None)
        {
            if (DateTime.TryParseExact(str, format: format, formatProvider, dateTimeStyles, out var result))
            {
                return result;
            }

            return null;
        }

        public static DateTime? ToNullableDateTime(this string      str,
                                                   IFormatProvider? formatProvider = null,
                                                   DateTimeStyles   dateTimeStyles = DateTimeStyles.None)
        {
            if (DateTime.TryParse(str, formatProvider, dateTimeStyles, out var result))
            {
                return result;
            }

            return null;
        }

        public static TimeSpan? ToNullableTimeSpan(this string      str,
                                                   string           format,
                                                   IFormatProvider? formatProvider = null,
                                                   TimeSpanStyles   dateTimeStyles = TimeSpanStyles.None)
        {
            if (TimeSpan.TryParseExact(str, format: format, formatProvider, dateTimeStyles, out var result))
            {
                return result;
            }

            return null;
        }

        public static TimeSpan? ToNullableTimeSpan(this string      str,
                                                   IFormatProvider? formatProvider = null)
        {
            if (TimeSpan.TryParse(str, formatProvider, out var result))
            {
                return result;
            }

            return null;
        }

        public static long? ToNullableLong(this string str)
        {
            if (long.TryParse(str, out var result))
            {
                return result;
            }

            return null;
        }

        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }
    }
}
