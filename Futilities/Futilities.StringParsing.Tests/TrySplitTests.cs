using Microsoft.VisualStudio.TestTools.UnitTesting;
using Futilities.StringParsing;


namespace Futilities.StringParsing.Tests
{
    [TestClass]
    public class TrySplitTests
    {
        const string simpleDelimitedString = "A,B,C,D";
        const string delimitedStringWithQuotes = "A,\"B\",C,D";
        const string delimitedWithQuotesAndEmbeddedDelimiter = "A,\"B,C\",D,E";
        const string delimitedWithMismatchedQuotes = "A,\"B,C,D,E";

        [TestMethod]
        public void TrySplit_Returns_False_For_Empty_String()
        {
            string s = string.Empty;

            bool result = s.TrySplit(out _);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TrySplit_Returns_False_For_Null_String()
        {
            string s = null;

            bool result = s.TrySplit(out _);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TrySplit_Returns_True_For_NonEmpty_String()
        {
            string s = "a";

            bool result = s.TrySplit(out _);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TrySplit_Result_Correct_With_String_Without_Embedded_Quotes()
        {
            string s = simpleDelimitedString;

            bool result = s.TrySplit(out var results);

            Assert.IsTrue(result);

            Assert.AreEqual(results.Count, 4);
        }

        [TestMethod]
        public void TrySplit_Result_Correct_With_String_With_Embedded_Quotes()
        {
            string s = delimitedStringWithQuotes;

            bool result = s.TrySplit(out var results);

            Assert.IsTrue(result);

            Assert.AreEqual(results.Count, 4);
        }

        [TestMethod]
        public void TrySplit_Result_Correct_With_String_With_Embedded_Quotes_And_Embedded_Delimiter()
        {
            string s = delimitedWithQuotesAndEmbeddedDelimiter;

            bool result = s.TrySplit(out var results);

            Assert.IsTrue(result);

            Assert.AreEqual(results.Count, 4);

            Assert.AreEqual(results[1], "B,C");
        }

        [TestMethod]
        public void TrySplit_Result_Correct_With_String_With_Mismatched_Quotes()
        {
            string s = delimitedWithMismatchedQuotes;

            bool result = s.TrySplit(out var results);

            Assert.IsTrue(result);

            Assert.AreEqual(results.Count, 2);
        }

    }
}
