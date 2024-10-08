﻿using Futilities.Hashing;
using Futilities.Testing;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Dynamic;

namespace Futilities.Hash.Tests
{
    [TestClass]
    public class HashTests : TestingBase
    {
        [TestMethod]
        [TestProperty("Expected-MD5-Hash", "92-2C-16-B0-30-3D-C8-64-0D-E8-57-C5-31-ED-D7-2A")]
        [TestProperty("Prop1-Value", "abcdefghijklmnopqrstuvwxyz")]
        [TestProperty("Prop2-Value", "9999")]
        [TestProperty("Prop3-Value", "1/1/2001")]
        public void ComputeHash_ReturnsExpectedMD5Hash()
        {
            TryGetTestProperty(nameof(ComputeHash_ReturnsExpectedMD5Hash), "Prop1-Value", out string p1);
            TryGetTestProperty(nameof(ComputeHash_ReturnsExpectedMD5Hash), "Prop2-Value", out double p2);
            TryGetTestProperty(nameof(ComputeHash_ReturnsExpectedMD5Hash), "Prop3-Value", out DateTime p3);

            var obj = new TestObject() { Prop1 = p1, Prop2 = p2, Prop3 = p3 };

            var hash = obj.ComputeHash();

            TryGetTestProperty(nameof(ComputeHash_ReturnsExpectedMD5Hash), "Expected-MD5-Hash", out string expected);

            Assert.AreEqual(expected, hash);
        }

        [TestMethod]
        [TestProperty("Expected-SHA1-Hash", "C1-88-3B-4D-17-16-1D-E6-3C-23-47-75-F2-22-56-29-62-36-21-5C")]
        [TestProperty("Prop1-Value", "abcdefghijklmnopqrstuvwxyz")]
        [TestProperty("Prop2-Value", "9999")]
        [TestProperty("Prop3-Value", "1/1/2001")]
        public void ComputeHash_ReturnsExpectedSHA1Hash()
        {
            TryGetTestProperty(nameof(ComputeHash_ReturnsExpectedSHA1Hash), "Prop1-Value", out string p1);
            TryGetTestProperty(nameof(ComputeHash_ReturnsExpectedSHA1Hash), "Prop2-Value", out double p2);
            TryGetTestProperty(nameof(ComputeHash_ReturnsExpectedSHA1Hash), "Prop3-Value", out DateTime p3);

            var obj = new TestObject() { Prop1 = p1, Prop2 = p2, Prop3 = p3 };

            var hash = obj.ComputeHash(Shared.HashingAlgorithm.SHA1);

            TryGetTestProperty(nameof(ComputeHash_ReturnsExpectedSHA1Hash), "Expected-SHA1-Hash", out string expected);

            Assert.AreEqual(expected, hash);
        }

        [TestMethod]
        [TestProperty("Prop1-Value", "abcdefghijklmnopqrstuvwxyz")]
        [TestProperty("Prop2-Value", "9999")]
        [TestProperty("Prop3-Value", "1/1/2001")]
        public void ComputeHash_ReturnsSameHash_WhenTwoObjectsContainSamePropertyValues()
        {
            TryGetTestProperty(nameof(ComputeHash_ReturnsExpectedMD5Hash), "Prop1-Value", out string p1);
            TryGetTestProperty(nameof(ComputeHash_ReturnsExpectedMD5Hash), "Prop2-Value", out double p2);
            TryGetTestProperty(nameof(ComputeHash_ReturnsExpectedMD5Hash), "Prop3-Value", out DateTime p3);

            var obj = new TestObject() { Prop1 = p1, Prop2 = p2, Prop3 = p3 };
            var obj2 = new TestObject() { Prop1 = p1, Prop2 = p2, Prop3 = p3 };

            var hash = obj.ComputeHash();
            var hash2 = obj2.ComputeHash();

            Assert.AreEqual(hash, hash2);
        }

        [TestMethod]
        public void ComputeHash_ReturnsNull_WhenObjectIsNull()
        {
            TestObject obj = null;

            var hash = obj.ComputeHash();

            Assert.IsNull(hash);
        }

        [TestMethod]
        public void ToSHA256_ThrowsException_WhenValueIsNull()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(() =>
            {
                string nullValue = null;

                var actual = nullValue.ToSHA256();
            });
        }

        [TestMethod]
        public void ToSHA1_ThrowsException_WhenValueIsNull()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(() =>
            {
                string nullValue = null;

                var actual = nullValue.ToSHA1();
            });
        }
    }

    public class TestObject : IComputeHash
    {
        public string Prop1 { get; set; }
        public double Prop2 { get; set; }
        public DateTime Prop3 { get; set; }
        public dynamic GetObjectToCompute()
        {
            dynamic exp = new ExpandoObject();

            exp.Prop1 = Prop1;
            exp.Prop2 = Prop2;
            exp.Prop3 = Prop3;

            return exp;
        }
    }
}
