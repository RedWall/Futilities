using Futilities.Hashing;
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
        [TestProperty("Expected-MD5-Hash", "D3-6B-69-53-6B-9C-C1-47-03-95-E2-C3-8A-14-1F-5A")]
        [TestProperty("Prop1-Value", "abcdefghijklmnopqrstuvwxyz")]
        [TestProperty("Prop2-Value", "9999")]
        [TestProperty("Prop3-Value", "1/1/2001")]
        public void ReturnsExpectedMD5Hash()
        {
            TryGetTestProperty(nameof(ReturnsExpectedMD5Hash), "Prop1-Value", out string p1);
            TryGetTestProperty(nameof(ReturnsExpectedMD5Hash), "Prop2-Value", out double p2);
            TryGetTestProperty(nameof(ReturnsExpectedMD5Hash), "Prop3-Value", out DateTime p3);

            var obj = new TestObject() { Prop1 = p1, Prop2 = p2, Prop3 = p3 };

            var hash = obj.ComputeHash();

            TryGetTestProperty(nameof(ReturnsExpectedMD5Hash), "Expected-MD5-Hash", out string expected);

            Assert.AreEqual(expected, hash);
        }

        [TestMethod]
        [TestProperty("Expected-SHA1-Hash", "AD-4C-EE-67-96-68-21-E3-52-A4-BE-31-18-92-2C-8D-6F-A6-0F-0D")]
        [TestProperty("Prop1-Value", "abcdefghijklmnopqrstuvwxyz")]
        [TestProperty("Prop2-Value", "9999")]
        [TestProperty("Prop3-Value", "1/1/2001")]
        public void ReturnsExpectedSHA1Hash()
        {
            TryGetTestProperty(nameof(ReturnsExpectedSHA1Hash), "Prop1-Value", out string p1);
            TryGetTestProperty(nameof(ReturnsExpectedSHA1Hash), "Prop2-Value", out double p2);
            TryGetTestProperty(nameof(ReturnsExpectedSHA1Hash), "Prop3-Value", out DateTime p3);

            var obj = new TestObject() { Prop1 = p1, Prop2 = p2, Prop3 = p3 };

            var hash = obj.ComputeHash(Shared.HashingAlgorithm.SHA1);

            TryGetTestProperty(nameof(ReturnsExpectedSHA1Hash), "Expected-SHA1-Hash", out string expected);

            Assert.AreEqual(expected, hash);
        }

        [TestMethod]
        [TestProperty("Expected-MD5-Hash", "D3-6B-69-53-6B-9C-C1-47-03-95-E2-C3-8A-14-1F-5A")]
        [TestProperty("Prop1-Value", "abcdefghijklmnopqrstuvwxyz")]
        [TestProperty("Prop2-Value", "9999")]
        [TestProperty("Prop3-Value", "1/1/2001")]
        public void TwoObjectsWithSamePropertyValuesReturnSameHash()
        {
            TryGetTestProperty(nameof(ReturnsExpectedMD5Hash), "Prop1-Value", out string p1);
            TryGetTestProperty(nameof(ReturnsExpectedMD5Hash), "Prop2-Value", out double p2);
            TryGetTestProperty(nameof(ReturnsExpectedMD5Hash), "Prop3-Value", out DateTime p3);

            var obj = new TestObject() { Prop1 = p1, Prop2 = p2, Prop3 = p3 };
            var obj2 = new TestObject() { Prop1 = p1, Prop2 = p2, Prop3 = p3 };

            var hash = obj.ComputeHash();
            var hash2 = obj2.ComputeHash();

            Assert.AreEqual(hash, hash2);
        }

        [TestMethod]
        public void ReturnsNullForNullObject()
        {

            TestObject obj = null;

            var hash = obj.ComputeHash();

            Assert.IsNull(hash);
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
