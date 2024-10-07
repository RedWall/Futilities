using Futilities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Futilities.Hashing
{
    public static class Hash
    {
        public static void SetHash<T>(this T obj, Expression<Func<T, string>> selector, HashingAlgorithm algorithm = HashingAlgorithm.MD5) where T : IComputeHash
        {
            var p = (PropertyInfo)((MemberExpression)selector.Body).Member;
            p.SetValue(obj, ComputeHash(obj, algorithm));
        }

        public static string ComputeHash<T>(this T obj, HashingAlgorithm algorithm = HashingAlgorithm.MD5) where T : IComputeHash
        {
            if (obj is null)
                return null;

            var dict = (obj.GetObjectToCompute() as IDictionary<string, object>).ToDictionary(x => x.Key, x => x.Value);
            var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(dict));

            string hash = string.Empty;

            if (algorithm == HashingAlgorithm.MD5)
                using (var md = MD5.Create())
                {
                    var hashBytes = md.ComputeHash(bytes);
                    hash = BitConverter.ToString(hashBytes);
                }

            if (algorithm == HashingAlgorithm.SHA1)
                using (var sh = SHA1.Create())
                {
                    var hashBytes = sh.ComputeHash(bytes);
                    hash = BitConverter.ToString(hashBytes);
                }

            return hash;
        }

        /// <summary>
        /// Hashes a string using <see cref="SHA256"></see>
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>A string hashed using <see cref="SHA256"></see></returns>
        public static string ToSHA256(this string value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            byte[] bytes = Encoding.UTF8.GetBytes(value);

            using var hash = SHA256.Create();

            return string.Concat(hash.ComputeHash(bytes).Select(i => i.ToString("x2")));
        }

        /// <summary>
        /// Hashes a string using <see cref="SHA1"></see>
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>A string hashed using <see cref="SHA1"></see></returns>
        public static string ToSHA1(this string value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var bytes = Encoding.UTF8.GetBytes(value);

            using var hash = SHA1.Create();
            
            return string.Concat(hash.ComputeHash(bytes).Select(b => b.ToString("x2")));
        }
    }
}
