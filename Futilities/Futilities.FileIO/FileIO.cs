using System;
using System.IO;
using System.Security.Cryptography;

namespace Futilities.FileIO
{
    public static class FileIO
    {
        public static string GetFileCheckSum(this FileInfo File)
        {
            if (File is null)
                throw new ArgumentNullException(nameof(File));

            if (!File.Exists)
                throw new FileNotFoundException("Unable to locate file", File.Name);

            using (var stream = new BufferedStream(File.OpenRead(), 1500000))
            {

                using (var md5 = MD5.Create())
                    return BitConverter.ToString(md5.ComputeHash(stream));

            }
        }
    }
}
