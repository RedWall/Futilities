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

    }
}