using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Futilities.StringParsing.Tests
{
    [TestClass]
    public class SafeSubstringTests
    {
        string testString = "abcdefghij";
        [TestMethod]
        public void SafeSubstring_ReturnsNull_WhenStringIsNull()
        {
            string s = null;

            string result = s.SafeSubstring(0,1);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void SafeSubstring_ReturnsFullString_WhenRequestedLengthIsGreaterThanStringLength()
        {
            string result = testString.SafeSubstring(0,12);

            Assert.AreEqual(result, testString);
        }

        [TestMethod]
        public void SafeSubstring_ReturnsString_WhenRequestedLengthIsLessThanStringLength()
        {
            string result = testString.SafeSubstring(2, 6);

            Assert.AreEqual(result.Length, 6);

            Assert.AreEqual(result[0], testString[2]);
        }

        [TestMethod]
        public void SafeSubstring_ReturnsString_WhenLengthIsNotProvided()
        {
            string result = testString.SafeSubstring(1);

            Assert.AreEqual(result.Length, 9);

            Assert.AreEqual(result[0], testString[1]);
        }
    }
}
