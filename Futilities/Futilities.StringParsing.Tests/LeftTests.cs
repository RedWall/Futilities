using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Futilities.StringParsing.Tests
{
    [TestClass]
    public class LeftTests
    {

        string testString = "abcdefghij";

        [TestMethod]
        public void Left_ReturnsNull_WhenStringIsNull()
        {
            string s = null;

            string result = s.Left(1);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Left_ReturnsFullString_WhenRequestedLengthIsGreaterThanStringLength()
        {
            string result = testString.Left(11);

            Assert.AreEqual(result, testString);
        }

        [TestMethod]
        public void Left_ReturnsString_WhenRequestedLengthIsLessThanStringLength()
        {

            string result = testString.Left(9);

            Assert.AreEqual(result[^1], testString[^2]);
        }

    }
}
