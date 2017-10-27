using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace Futilities.Hashing
{
    public static class Hash
    {
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

                using (var md = MD5.Create())
                {
                    var bytes = md.ComputeHash(ms.ToArray());

                    return BitConverter.ToString(bytes);
                }
            }
        }
    }
}
