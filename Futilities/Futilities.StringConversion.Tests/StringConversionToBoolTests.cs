using Microsoft.VisualStudio.TestTools.UnitTesting;
using Futilities.StringConversion;
using System;

namespace Futilities.StringConversion.Tests
{
    [TestClass]
    public class StringConversionToBoolTests
    {

        [TestMethod]
        public void Generic_To_Bool_Returns_True_For_TrueString()
        {
            string value = bool.TrueString;

            bool result = value.To<bool>();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ToBool_Returns_True_For_TrueString()
        {
            string value = bool.TrueString;

            bool result = value.ToBool();

            Assert.IsTrue(result);
        }
        
        [TestMethod]
        public void ToBool_Returns_defaultValue_When_Coversion_Fails()
        {
            string value = "Q";
            bool defaultValue = true;

            bool result = value.ToBool(defaultValue);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Generic_To_Bool_Returns_defaultValue_When_Coversion_Fails()
        {
            string value = "Q";
            bool defaultValue = true;

            bool result = value.To<bool>(defaultValue);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ToBool_Returns_True_When_alsoTrue_Contains_Value()
        {
            string value = "y";
            string[] alsoTrue = { "Y", "X" };

            bool result = value.ToBool(alsoTrue: alsoTrue);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ToBool_Returns_True_When_alsoTrue_Contains_Value_ignoreCase_False()
        {
            string value = "Y";
            string[] alsoTrue = { "Y", "X" };

            bool result = value.ToBool(alsoTrue: alsoTrue, ignoreCase: false);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ToBool_Returns_False_When_alsoTrue_Does_Not_Contain_Value()
        {
            string value = "Y";
            string[] alsoTrue = { "Q", "X" };

            bool result = value.ToBool(alsoTrue: alsoTrue);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ToBool_Returns_False_When_alsoTrue_Does_Not_Contain_Value_ignoreCase_False()
        {
            string value = "Y";
            string[] alsoTrue = { "y", "X" };

            bool result = value.ToBool(alsoTrue: alsoTrue, ignoreCase: false);

            Assert.IsFalse(result);
        }
    }
}
