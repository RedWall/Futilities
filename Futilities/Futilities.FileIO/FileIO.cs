using System;
using System.IO;
using System.Security.Cryptography;

namespace Futilities.FileIO
{
    public static class FileIO
    {
        public static string GetFileCheckSum(this FileInfo File, HashingAlgorithm hashingAlgorithm = HashingAlgorithm.MD5)
        {
            if (File is null)
                throw new ArgumentNullException(nameof(File));

            if (!File.Exists)
                throw new FileNotFoundException("Unable to locate file", File.Name);

            using var stream = new BufferedStream(File.OpenRead(), 1500000);

            switch (hashingAlgorithm)
            {
                case HashingAlgorithm.MD5:
                    using (var md5 = MD5.Create())
                    {
                        return BitConverter.ToString(md5.ComputeHash(stream));
                    }

                case HashingAlgorithm.SHA1:
                    using (var sha1 = SHA1.Create())
                    {
                        return BitConverter.ToString(sha1.ComputeHash(stream));
                    }
                default:
                    throw new ArgumentException("Invalid hashing algorithm selected.", nameof(hashingAlgorithm));
            }
        }
    }
}
