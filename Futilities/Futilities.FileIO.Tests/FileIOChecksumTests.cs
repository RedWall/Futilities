using Futilities.Shared;
using Futilities.Testing;
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
    public class FileIOChecksumTests : TestingBase
    {
        private string GetProjectDirectory => Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        [TestMethod]
        public void GetFileChecksum_ThrowsArgumentNullException_WhenFileInfoIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                FileInfo value = null;
                string result = value.GetFileCheckSum();
            });
        }

        [TestMethod]
        [TestProperty("TestFileName", "testfile.txt")]
        public void GetFileChecksum_ThrowsArgumentException_WhenStringFilePathIsNull()
        {
            TryGetTestProperty(nameof(GetFileChecksum_ThrowsArgumentException_WhenStringFilePathIsNull), "TestFileName", out string fileName);

            Assert.ThrowsException<ArgumentException>(() =>
            {
                string filePath = Path.Combine(GetProjectDirectory, fileName);

                FileInfo value = new FileInfo(filePath);
                string result = value.GetFileCheckSum((HashingAlgorithm)100);
            });
        }

        [TestMethod]
        [TestProperty("FakeFileName", "Fake.fake")]
        public void GetFileChecksum_ThrowsFileNotFoundException_WhenFileInfoDoesNotExist()
        {
            TryGetTestProperty(nameof(GetFileChecksum_ThrowsFileNotFoundException_WhenFileInfoDoesNotExist), "FakeFileName", out string fakeFileName);

            Assert.ThrowsException<FileNotFoundException>(() =>
            {
                FileInfo value = new FileInfo(fakeFileName);
                string result = value.GetFileCheckSum();
            });
        }

        [TestMethod]
        [TestProperty("TestFileName", "testfile.txt")]
        [TestProperty("ExpectedChecksum", "89-88-C8-E6-B7-96-F1-D2-22-D5-12-B6-FB-DE-B8-C3")]
        public void GetFileChecksum_ReturnsMD5Checksum_WhenNoAlgorithmProvided()
        {
            TryGetTestProperty(nameof(GetFileChecksum_ReturnsMD5Checksum_WhenNoAlgorithmProvided), "TestFileName", out string fileName);
            TryGetTestProperty(nameof(GetFileChecksum_ReturnsMD5Checksum_WhenNoAlgorithmProvided), "ExpectedChecksum", out string expected);

            string filePath = Path.Combine(GetProjectDirectory, fileName);

            FileInfo value = new FileInfo(filePath);

            string result = value.GetFileCheckSum();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestProperty("TestFileName", "testfile.txt")]
        [TestProperty("ExpectedMD5Checksum", "89-88-C8-E6-B7-96-F1-D2-22-D5-12-B6-FB-DE-B8-C3")]
        public void GetFileChecksum_ReturnsMD5Checksum_WhenAlgorithmIsMD5()
        {
            TryGetTestProperty(nameof(GetFileChecksum_ReturnsMD5Checksum_WhenAlgorithmIsMD5), "TestFileName", out string fileName);
            TryGetTestProperty(nameof(GetFileChecksum_ReturnsMD5Checksum_WhenAlgorithmIsMD5), "ExpectedMD5Checksum", out string expected);

            string filePath = Path.Combine(GetProjectDirectory, fileName);

            FileInfo value = new FileInfo(filePath);

            string result = value.GetFileCheckSum(HashingAlgorithm.MD5);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestProperty("TestFileName", "testfile.txt")]
        [TestProperty("ExpectedSHA1Checksum", "82-77-95-F4-B5-3F-EC-A5-D5-52-C1-38-06-D5-D0-8E-D3-18-66-A2")]
        public void GetFileChecksum_ReturnsSHA1Checksum_WhenAlgorithmIsSHA1()
        {
            TryGetTestProperty(nameof(GetFileChecksum_ReturnsSHA1Checksum_WhenAlgorithmIsSHA1), "TestFileName", out string fileName);
            TryGetTestProperty(nameof(GetFileChecksum_ReturnsSHA1Checksum_WhenAlgorithmIsSHA1), "ExpectedSHA1Checksum", out string expected);

            string filePath = Path.Combine(GetProjectDirectory, fileName);

            FileInfo value = new FileInfo(filePath);

            string result = value.GetFileCheckSum(HashingAlgorithm.SHA1);

            Assert.AreEqual(expected, result);
        }
    }
}
