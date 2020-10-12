using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Futilities.StringParsing.Tests
{
    [TestClass]
    public class RigthTests
    {
        string testString = "abcdefghij";
        [TestMethod]
        public void Right_Returns_Null_For_Null_String()
        {
            string s = null;

            string result = s.Right(1);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Right_Returns_Full_String_When_Requested_Length_Is_Greater_Than_String_Length()
        {


            string result = testString.Right(11);

            Assert.AreEqual(result, testString);
        }

        [TestMethod]
        public void Right_Returns_String_When_Requested_Length_Is_Less_Than_String_Length()
        {

            string result = testString.Right(9);

            Assert.AreEqual(result[0], testString[1]);
        }

    }
}
