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

        public static T GetValueOrDefault<T>(this List<T> list, int? index)
        {
            if (index.HasValue && index.Value >= 0 && list.Count() > index.Value)
                return list[index.Value];

            return default(T);
        }


        public static void SetProperty<T>(this T obj, object value, Expression<Func<T, string>> selector)
        {
            object target = obj;

            string[] bits = selector.Body.ToString().Split('.');

            //Get the object that is the child property if needed
            for (int i = 0; i < bits.Length - 1; i++)
            {
                var member = target.GetType().GetMember(bits[i]);

                if (member == null || member.Length == 0)
                    continue;

                if (member[0].MemberType == MemberTypes.Property)
                    target = ((PropertyInfo)member[0]).GetValue(target, null);
                else
                    if (member[0].MemberType == MemberTypes.Field)
                    target = ((FieldInfo)member[0]).GetValue(target);
            }

            var memberToSet = target.GetType().GetMember(bits.Last());

            if (memberToSet == null || memberToSet.Length == 0)
                return;

            if (memberToSet[0].MemberType == MemberTypes.Property)
                ((PropertyInfo)memberToSet[0]).SetValue(target, value);
            else
            if (memberToSet[0].MemberType == MemberTypes.Field)
                ((FieldInfo)memberToSet[0]).SetValue(target, value);

        }

        public static V GetValue<T, V>(this T obj, Expression<Func<T, V>> selector)
        {

            V value = default(V);

            object target = obj;

            string[] bits = selector.Body.ToString().Split('.');

            //Get the object that is the child property if needed
            for (int i = 0; i < bits.Length - 1; i++)
            {
                var member = target.GetType().GetMember(bits[i]);

                if (member == null || member.Length == 0)
                    continue;

                if (member[0].MemberType == MemberTypes.Property)
                    target = ((PropertyInfo)member[0]).GetValue(target, null);
                else
                    if (member[0].MemberType == MemberTypes.Field)
                    target = ((FieldInfo)member[0]).GetValue(target);
            }

            var memberToGet = target.GetType().GetMember(bits.Last());

            if (memberToGet == null || memberToGet.Length == 0)
                return default(V);

            if (memberToGet[0].MemberType == MemberTypes.Property)
                return (V)((PropertyInfo)memberToGet[0]).GetValue(target);
            else if (memberToGet[0].MemberType == MemberTypes.Field)
                return (V)((FieldInfo)memberToGet[0]).GetValue(target);

            return value;

        }

        public static string MyPass()
        {
            return "";
        }

        public static bool TrySplit(this string input, out List<string> output, char delimiter = ',', bool hasQuotedFields = true, List<string> commentTokens = null, bool returnNullForEmptyValues = false)
        {
            try
            {
                output = new List<string>();

                if (string.IsNullOrWhiteSpace(input))
                    return false;

                var fields = input.Split(delimiter);

                if (commentTokens != null && commentTokens.Any(c => fields[0].StartsWith(c)))
                    return false;

                for (int i = 0; i < fields.Length; i++)
                {
                    string field = fields[i];
                    if (string.IsNullOrWhiteSpace(field))
                    {
                        output.Add(returnNullForEmptyValues ? null : string.Empty);
                        continue;
                    }

                    // first char quote?
                    if (!hasQuotedFields || field[0] != '"')
                    {
                        string value = field.Trim();

                        if (returnNullForEmptyValues && value.Length == 0)
                            value = null;

                        output.Add(value);
                    }
                    else
                    {
                        string tempField = field;
                        for (int j = ++i; j < fields.Length; j++)
                        {
                            string field2 = fields[j];
                            //put the comma back
                            tempField += $",{field2}";

                            //Found the end quote or the end of the line
                            if (field2.EndsWith("\"") || j == fields.Length - 1)
                            {
                                string value = tempField.Trim('"', ' ');

                                if (returnNullForEmptyValues && value.Length == 0)
                                    value = null;

                                output.Add(value);

                                // move the iterator to the current position
                                i = j;
                                //leave the j loop
                                break;
                            }
                        }
                    }
                }


                return true;
            }
            catch
            {
                output = null;
                return false;
            }
        }

        public static T GetAt<T>(this IList<T> list, int index)
        {
            if (list.Count() > index)
                return list[index];


            return default(T);
        }
        public static string Find(this string input, int start, int? length = null, bool trim = true, bool returnNullForEmptyValues = true)
        {
            string output = string.Empty;

            if (length.HasValue)
            {
                if (input.Length >= start + length.Value)
                    output = input.Substring(start, length.Value);
            }
            else
            {
                if (input.Length > start)
                    output = input.Substring(start);
            }

            if (trim)
                output = output.Trim();

            if (output.Length == 0 && returnNullForEmptyValues)
                output = null;

            return output;

        }
    }
}