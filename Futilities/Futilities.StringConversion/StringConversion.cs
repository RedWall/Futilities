using System;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Futilities.StringConversion
{
    public static class StringConversion
    {

        public static bool ToBool(this string o, bool defaultValue = false, bool ignoreCase = true, params string[] alsoTrue)
        {

            if (string.IsNullOrWhiteSpace(o))
                return defaultValue;

            if (bool.TryParse(o, out bool b))
                return b;

            if (alsoTrue.Any(t => string.Compare(t, o, ignoreCase) == 0))
                return true;

            return defaultValue;
        }

        public static int ToInt(this string s) => s.To<int>();

        public static short ToShort(this string s) => s.To<short>();

        public static decimal ToDecimal(this string s) => s.To<decimal>();

        public static double ToDouble(this string s) => s.To<double>();

        public static DateTime ToSqlDateTime(this string s, string format = null)
        {
            return ToDateTime(s, format, SqlDateTime.MinValue.Value);
        }

        public static DateTime ToDateTime(this string s, string Format = null, DateTime defaultValue = default)
        {
            if (string.IsNullOrWhiteSpace(s))
                return defaultValue;

            if (!string.IsNullOrWhiteSpace(Format) && DateTime.TryParseExact(s, Format, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out DateTime dEx))
                return dEx;

            if (DateTime.TryParse(s, out DateTime d))
                return d;

            return defaultValue;
        }

        public static T To<T>(this string s, T defaultValue = default, bool throwExceptionOnFailedConversion = false) where T : IConvertible
        {
            Type tType = typeof(T);

            switch (defaultValue)
            {
                case bool def:
                    var b = s.ToBool((bool)Convert.ChangeType(defaultValue, typeof(bool)));

                    return (T)Convert.ChangeType(b, tType);
                case DateTime def:
                    var d = s.ToDateTime(defaultValue: (DateTime)Convert.ChangeType(defaultValue, typeof(DateTime)));
                    return (T)Convert.ChangeType(d, tType);
            }

            var conv = TypeDescriptor.GetConverter(tType);

            try
            {
                return (T)conv.ConvertFromString(s);
            }
            catch
            {
                if (throwExceptionOnFailedConversion)
                    throw new InvalidCastException(string.Format("Cannot convert provided System.string to {0}", TypeDescriptor.GetClassName(tType)));

                return default;
            }
        }

        public static bool TryParseTime(this string s, out string time)
        {
            var timerx = new Regex(@"^(?<hour>\d{1,2}):(?<minute>\d{2}):?(?<second>\d{2})?(?<mill>:?[\d\W]*)?(?<period>[AaPp][Mm])?$");
            time = string.Empty;

            if (string.IsNullOrWhiteSpace(s))
                return false;

            var rx = timerx.Match(s.Trim());

            if (!rx.Success)
                return false;

            var hour = rx.Groups["hour"];
            string hh = string.Empty;
            var min = rx.Groups["minute"];
            string mm = string.Empty;
            var sec = rx.Groups["second"];
            string ss = "00";
            var pr = rx.Groups["period"];

            int h = -1;

            if (hour.Length == 0 || !int.TryParse(hour.Value, out h) || h < 0 || h > 24)
                return false;

            if (h == 0)
                hh = "00";
            else if (h >= 12)
                hh = h.ToString();
            else
            {
                if (pr.Length == 2 && string.Compare(pr.Value, "pm", true) == 0)
                    h += 12;

                hh = h.ToString().PadLeft(2, '0');
            }

            int m = -1;
            if (min.Length != 2 || !int.TryParse(min.Value, out m) || m < 0 || m > 59)
                return false;

            mm = m.ToString().PadLeft(2, '0');

            if (sec.Length == 2)
                ss = sec.Value;

            time = $"{hh}:{mm}:{ss}";

            return true;
        }
    }
}
