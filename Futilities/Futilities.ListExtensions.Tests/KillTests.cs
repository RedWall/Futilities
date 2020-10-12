using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Futilities.ListExtensions.Tests
{
    [TestClass]
    public class KillTests
    {
        [TestMethod]
        public void KillClearsList()
        {
            var l = new List<int> { 1, 2, 3, 4, 5 };

            l.Kill();

            Assert.AreEqual(l.Count, 0);

            Assert.AreEqual(l.Capacity, 0);
        }
    }
}
