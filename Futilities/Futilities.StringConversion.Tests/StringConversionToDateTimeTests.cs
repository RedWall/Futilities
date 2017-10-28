using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Futilities.StringConversion.Tests
{
    [TestClass]
    public class StringConversionToDateTimeTests
    {
        [TestMethod]
        public void ToDateTime_Returns_DateTime_With_Known_Format()
        {
            string value = "3/25/1977";
            DateTime expected = new DateTime(1977, 3, 25);

            DateTime result = value.ToDateTime();


            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void ToDateTime_Returns_DateTime_With_Specified_Format()
        {
            string value = "03|25|1977";
            DateTime expected = new DateTime(1977, 3, 25);

            string dateFormat = "MM|dd|yyyy";

            DateTime result = value.ToDateTime(dateFormat);

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void ToDateTime_Returns_defaultValue_When_Conversion_Fails()
        {
            string value = "not a valid date";
            DateTime defaultValue = new DateTime(1977, 3, 25);
            
            DateTime result = value.ToDateTime(defaultValue: defaultValue);

            Assert.AreEqual(result, defaultValue);
        }
    }
}
