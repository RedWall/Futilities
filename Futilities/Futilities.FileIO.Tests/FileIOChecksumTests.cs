using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Futilities.FileIO.Tests
{

    [TestClass]
    public class FileIOChecksumTests
    {
        private string GetProjectDirectory => Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;


        [TestMethod]
        public void GetFileIOChecksum_Throws_ArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                FileInfo value = null;
                string result = value.GetFileCheckSum();
            });
        }

        [TestMethod]
        [TestProperty("FakeFileName", "Fake.fake")]
        public void GetFileIOChecksum_Throws_FileNotFoundException()
        {
            TryGetTestProperty(nameof(GetFileIOChecksum_Throws_FileNotFoundException), "FakeFileName", out string fakeFileName);

            Assert.ThrowsException<FileNotFoundException>(() =>
            {
                FileInfo value = new FileInfo(fakeFileName);
                string result = value.GetFileCheckSum();
            });
        }

        [TestMethod]
        [TestProperty("TestFileName", "testfile.txt")]
        [TestProperty("ExpectedChecksum", "89-88-C8-E6-B7-96-F1-D2-22-D5-12-B6-FB-DE-B8-C3")]
        public void GetFileIOChecksum_Returns_Checksum()
        {
            TryGetTestProperty(nameof(GetFileIOChecksum_Returns_Checksum), "TestFileName", out string fileName);
            TryGetTestProperty(nameof(GetFileIOChecksum_Returns_Checksum), "ExpectedChecksum", out string expected);

            string filePath = Path.Combine(GetProjectDirectory, fileName);

            FileInfo value = new FileInfo(filePath);

            string result = value.GetFileCheckSum();

            Assert.AreEqual(expected, result);
        }

        [ExcludeFromCodeCoverage]
        private bool TryGetTestProperty<T>(string methodName, string propertyName, out T propertyValue)
        {
            try
            {
                Type type = GetType();

                MethodInfo methodInfo = type.GetMethod(methodName);

                IEnumerable<TestPropertyAttribute> attribute = methodInfo.GetCustomAttributes<TestPropertyAttribute>();

                var value = attribute.FirstOrDefault(a => a.Name == propertyName)?.Value ?? null;

                propertyValue = (T)Convert.ChangeType(value, typeof(T));

                if (value is null)
                    return false;

                return true;
            }
            catch (Exception)
            {
                propertyValue = default;
                return false;
            }
        }
    }
}
