using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Futilities.StringParsing.Tests
{
    [TestClass]
    public class RigthTests
    {
        string testString = "abcdefghij";
        [TestMethod]
        public void Right_ReturnsNull_WhenStringIsNull()
        {
            string s = null;

            string result = s.Right(1);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Right_ReturnsFullString_WhenRequestedLengthIsGreaterThanStringLength()
        {
            string result = testString.Right(11);

            Assert.AreEqual(result, testString);
        }

        [TestMethod]
        public void Right_ReturnsString_WhenRequestedLengthIsLessThanStringLength()
        {
            string result = testString.Right(9);

            Assert.AreEqual(result[0], testString[1]);
        }

    }
}
