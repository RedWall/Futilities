using System;
using System.Text.RegularExpressions;

namespace Futilities.Temporal
{
    public static class TemporalExtensions
    {

        public static bool IsBefore(this DateTime d, DateTime other)
        {
            return d.CompareTo(other) < 0;
        }

        public static bool IsAfter(this DateTime d, DateTime other)
        {
            return d.CompareTo(other) > 0;
        }

        public static bool IsOn(this DateTime d, DateTime other)
        {
            return d.CompareTo(other) == 0;
        }

        public static bool IsOnOrAfter(this DateTime d, DateTime other)
        {
            return d.IsOn(other) || d.IsAfter(other);
        }

        public static bool IsOnOrBefore(this DateTime d, DateTime other)
        {
            return d.IsOn(other) || d.IsBefore(other);
        }

        public static bool IsBetween(this DateTime d, DateTime begin, DateTime end, bool inclusive = false)
        {
            if (inclusive)
                return d.IsOnOrAfter(begin) && d.IsOnOrBefore(end);

            return d.IsAfter(begin) && d.IsBefore(end);
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
