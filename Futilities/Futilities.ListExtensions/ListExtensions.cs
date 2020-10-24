using System;
using System.Collections.Generic;
using System.Linq;

namespace Futilities.ListExtensions
{
    public static class ListExtensions
    {
        public static void Kill<T>(this List<T> list)
        {

            list.Clear();
            list.TrimExcess();

        }

        public static T GetValueOrDefault<T>(this List<T> list, int index, T defaultValue = default)
        {
            if (list.Count > index)
                return list[index];

            return defaultValue;
        }
    }
}
