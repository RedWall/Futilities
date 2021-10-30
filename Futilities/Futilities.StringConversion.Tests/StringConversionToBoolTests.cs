using Microsoft.VisualStudio.TestTools.UnitTesting;
using Futilities.StringConversion;
using System;

namespace Futilities.StringConversion.Tests
{
    [TestClass]
    public class StringConversionToBoolTests
    {

        [TestMethod]
        public void GenericToBool_ReturnsTrue_ForTrueString()
        {
            string value = bool.TrueString;

            bool result = value.To<bool>();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ToBool_ReturnsTrue_ForTrueString()
        {
            string value = bool.TrueString;

            bool result = value.ToBool();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ToBool_ReturnsDefaultValue_WhenConversionFails()
        {
            string value = "Q";
            bool defaultValue = true;

            bool result = value.ToBool(defaultValue);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GenericToBool_ReturnsDefaultValue_WhenConversionFails()
        {
            string value = "Q";
            bool defaultValue = true;

            bool result = value.To(defaultValue);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ToBool_ReturnsTrue_WhenAlsoTrueContainsValue()
        {
            string value = "y";
            string[] alsoTrue = { "Y", "X" };

            bool result = value.ToBool(alsoTrue: alsoTrue);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ToBool_ReturnsTrue_WhenAlsoTrueContainsValueAndIgnoreCaseIsFalse()
        {
            string value = "Y";
            string[] alsoTrue = { "Y", "X" };

            bool result = value.ToBool(alsoTrue: alsoTrue, ignoreCase: false);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ToBool_ReturnsFalse_WhenAlsoTrueDoesNotContainValue()
        {
            string value = "Y";
            string[] alsoTrue = { "Q", "X" };

            bool result = value.ToBool(alsoTrue: alsoTrue);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ToBool_ReturnsFalse_WhenAlsoTrueDoesNotContainValueAndIgnoreCaseIsFalse()
        {
            string value = "Y";
            string[] alsoTrue = { "y", "X" };

            bool result = value.ToBool(alsoTrue: alsoTrue, ignoreCase: false);

            Assert.IsFalse(result);
        }
    }
}
