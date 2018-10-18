using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace Futilities.Enums
{
    public static class EnumExtensions
    {
        public static List<T> EnumToList<T>() => Enum.GetValues(typeof(T)).Cast<T>().ToList();

        public static bool TryGetAttribute<T>(this Enum value, out T attribute) where T : Attribute
        {
            attribute = value.GetAttribute<T>();

            return attribute != null;
        }

        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);

            if (!attributes.Any())
                return null;

            return (T)attributes[0];
        }

        public static string DescriptionOrName(this Enum value)
        {
            string name = value.ToString();

            if (value.TryGetAttribute(out DescriptionAttribute desc))
            {
                name = desc.Description;
            }
            else if (value.TryGetAttribute(out DisplayNameAttribute dn))
            {
                name = dn.DisplayName;
            }
            else if (value.TryGetAttribute(out DisplayAttribute da))
            {
                if (!string.IsNullOrEmpty(da.Name))
                    name = da.Name;
                else if (!string.IsNullOrEmpty(da.Description))
                    name = da.Description;
            }

            return name;
        }

        public static Dictionary<T, string> EnumToDictionary<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return Enum.GetValues(typeof(T)).Cast<T>().ToDictionary(t => t, v => (v as Enum).DescriptionOrName());
        }


        public static Nullable<T> GetEnumValueOrNull<T>(this int? value) where T : struct, IConvertible
        {
            if (!value.HasValue)
                return null;

            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            if (Enum.IsDefined(typeof(T), value))
            {
                return Enum.GetValues(typeof(T)).Cast<T>().First(t => t.ToInt32(CultureInfo.CurrentCulture) == value);
            }
            else
                return null;
        }

        public static Nullable<T> GetEnumValueOrNull<T>(this int value) where T : struct, IConvertible
        {
            return GetEnumValueOrNull<T>((int?)value);
        }

    }


}
