using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Futilities.StringParsing.Tests
{
    [TestClass]
    public class LeftTests
    {

        string testString = "abcdefghij";

        [TestMethod]
        public void Left_Returns_Null_For_Null_String()
        {
            string s = null;

            string result = s.Left(1);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Left_Returns_Full_String_When_Requested_Length_Is_Greater_Than_String_Length()
        {
            

            string result = testString.Left(11);

            Assert.AreEqual(result, testString);
        }

        [TestMethod]
        public void Left_Returns_String_When_Requested_Length_Is_Less_Than_String_Length()
        {

            string result = testString.Left(9);

            Assert.AreEqual(result[^1], testString[^2]);
        }

    }
}
