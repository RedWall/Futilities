using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Futilities.StringParsing.Tests
{
    [TestClass]
    public class SafeSubstringTests
    {
        string testString = "abcdefghij";
        [TestMethod]
        public void SafeSubstring_Returns_Null_For_Null_String()
        {
            string s = null;

            string result = s.SafeSubstring(0,1);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void SafeSubstring_Returns_Full_String_When_Requested_Length_Is_Greater_Than_String_Length()
        {
            string result = testString.SafeSubstring(0,12);

            Assert.AreEqual(result, testString);
        }

        [TestMethod]
        public void SafeSubstring_Returns_String_When_Requested_Length_Is_Less_Than_String_Length()
        {

            string result = testString.SafeSubstring(2, 6);

            Assert.AreEqual(result.Length, 6);

            Assert.AreEqual(result[0], testString[2]);
        }

        [TestMethod]
        public void SafeSubstring_Returns_String_When_Requested_No_Lenght_Is_Given()
        {

            string result = testString.SafeSubstring(1);

            Assert.AreEqual(result.Length, 9);

            Assert.AreEqual(result[0], testString[1]);
        }

    }
}
