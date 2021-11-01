using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Futilities.ListExtensions.Tests
{
    [TestClass]
    public class GetValueOrDefaultTests
    {
        [TestMethod]
        public void GetValueOrDefault_ReturnsCorrectElement_WhenIndexExists()
        {
            var l = new List<int> { 1, 2, 3, 4, 5 };

            var result = l.GetValueOrDefault(1);

            Assert.AreEqual(result, 2);
        }

        [TestMethod]
        public void GetValueOrDefault_ReturnsDefaultValue_WhenIndexDoesNotExist()
        {
            var l = new List<int> { 1, 2, 3, 4, 5 };

            var result = l.GetValueOrDefault(6);

            Assert.AreEqual(result, default);
        }

        [TestMethod]
        public void GetValueOrDefault_ReturnsSuppliedDefault_WhenIndexDoesNotExist()
        {
            var l = new List<int> { 1, 2, 3, 4, 5 };

            var result = l.GetValueOrDefault(6, -1);

            Assert.AreEqual(result, -1);
        }
    }
}
