using System;
using System.Collections.Generic;
using System.Linq;

namespace Futilities.ListExtensions
{
    public static class ListExtensions
    {
        public static List<T> EnumToList<T>() => Enum.GetValues(typeof(T)).Cast<T>().ToList();

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
    }
}
