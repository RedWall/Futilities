using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Futilities.ListExtensions.Tests
{
    [TestClass]
    public class KillTests
    {
        [TestMethod]
        public void Kill_ClearsList_WhenListHasValues()
        {
            var l = new List<int> { 1, 2, 3, 4, 5 };

            l.Kill();

            Assert.AreEqual(l.Count, 0);
        }

        [TestMethod]
        public void Kill_ClearsList_WhenListDoesNotHaveValues()
        {
            var l = new List<int>();

            l.Kill();

            Assert.AreEqual(l.Count, 0);
        }

        [TestMethod]
        public void Kill_ClearsCapacity_WhenListHasValues()
        {
            var l = new List<int> { 1, 2, 3, 4, 5 };

            l.Kill();

            Assert.AreEqual(l.Capacity, 0);
        }

        [TestMethod]
        public void Kill_ClearsCapacity_WhenListDoesNotHaveValues()
        {
            var l = new List<int>();

            l.Kill();

            Assert.AreEqual(l.Capacity, 0);
        }
    }
}
