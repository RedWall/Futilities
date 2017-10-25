using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Xml.Linq;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq.Expressions;
using System.Reflection;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;

namespace Futilities
{
    public static class Futilities
    {
        /// <summary>
        /// Attempts to parse to a bool. If unable to parse because null or whitespace or "U", returns default value.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool ToBool(this string o, bool defaultValue = false)
        {
            bool b = defaultValue;

            if (string.IsNullOrWhiteSpace(o))
                return b;

            if (!bool.TryParse(o, out b))
            {
                b = string.Compare(o, "YES", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(o, "Y", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(o, "U", StringComparison.OrdinalIgnoreCase) == 0;
            }

            return b;
        }

        public static List<T> EnumToList<T>() => Enum.GetValues(typeof(T)).Cast<T>().ToList();

        public static string Right(this string s, int Length)
        {
            if (s == null)
                return s;

            if (s.Length > Length)
            {
                return s.Substring(s.Length - Length);
            }
            else
                return s;
        }
        public static string Left(this string s, int Length)
        {
            if (s == null)
                return s;

            if (s.Length > Length)
            {
                return s.Substring(0, Length);
            }
            else
                return s;
        }

        public static IEqualityComparer<string> IgnoreCaseComparer => new IgnoreCaseComparer();

        public static void Kill<T>(this List<T> list)
        {

            list.Clear();
            list.TrimExcess();

        }

        public static string GetValue(this XElement element, string defaultValue = null) => element != null ? element.Value : defaultValue;

        public static string GetValue(this XAttribute attribute, string defaultValue = null) => attribute != null ? attribute.Value : defaultValue;

        public static T GetValue<T>(this XElement element, Func<XElement, T> func, T defaultValue = default(T)) => element != null ? func(element) : defaultValue;

        public static T GetValue<T>(this XAttribute attribute, Func<XAttribute, T> func, T defaultValue = default(T)) => attribute != null ? func(attribute) : defaultValue;

        public static int ToInt(this string s) => s.To<int>();

        public static short ToShort(this string s) => s.To<short>();

        public static decimal ToDecimal(this string s) => s.To<decimal>();

        public static double ToDouble(this string s) => s.To<double>();

        public static DateTime ToDateTime(this string s, string Format = null)
        {
            DateTime d = SqlDateTime.MinValue.Value;

            try
            {
                return s.To<DateTime>(true);
            }
            catch
            {

                if (s != null && !string.IsNullOrWhiteSpace(Format))
                    DateTime.TryParseExact(s, Format, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out d);

            }
            if (d == DateTime.MinValue)
                d = SqlDateTime.MinValue.Value;
            return d;
        }

        public static T To<T>(this string s) where T : IConvertible => s.To<T>(false);

        public static T To<T>(this string s, bool ExceptionOnFailedConversion) where T : IConvertible
        {

            var conv = TypeDescriptor.GetConverter(typeof(T));

            try
            {
                return (T)conv.ConvertFromString(s);
            }
            catch
            {
                if (ExceptionOnFailedConversion)
                    throw new InvalidCastException(string.Format("Cannot convert provided System.string to {0}", TypeDescriptor.GetClassName(typeof(T))));

                return default(T);
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

        public static string GetFileCheckSum(this FileInfo File)
        {
            if (!File.Exists)
                throw new FileNotFoundException("Unable to locate file", File.Name);

            using (var stream = new BufferedStream(File.OpenRead(), 1500000))
            {

                var md5 = MD5.Create();
                return BitConverter.ToString(md5.ComputeHash(stream));

            }
        }

        public static void SetHash<T>(this T obj, Expression<Func<T, string>> selector) where T : IComputeHash
        {
            var p = (PropertyInfo)((MemberExpression)selector.Body).Member;
            p.SetValue(obj, ComputeHash<T>(obj));
        }

        public static string ComputeHash<T>(this T obj) where T : IComputeHash
        {
            using (var ms = new MemoryStream())
            {

                var formatter = new BinaryFormatter();
                var dict = (obj.GetObjectToCompute() as IDictionary<string, object>).ToDictionary(x => x.Key, x => x.Value);

                formatter.Serialize(ms, dict);
                var md = MD5.Create().ComputeHash(ms.ToArray());
                return BitConverter.ToString(md);
            }
        }

    }
}