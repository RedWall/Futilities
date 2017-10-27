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

        public static string GetFileCheckSum(this FileInfo File)
        {
            if (!File.Exists)
                throw new FileNotFoundException("Unable to locate file", File.Name);

            using (var stream = new BufferedStream(File.OpenRead(), 1500000))
            {

                using (var md5 = MD5.Create())
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