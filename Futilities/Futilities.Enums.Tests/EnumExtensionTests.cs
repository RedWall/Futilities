using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Futilities.Enums.Tests
{
    [TestClass]
    public class EnumExtensionTests
    {
        [TestMethod]
        public void EnumToList_Returns_List()
        {
            List<ExampleEnum> result = EnumExtensions.EnumToList<ExampleEnum>();
            Assert.IsTrue(result.Count == 3);
        }

        [TestMethod]
        public void EnumToList_Throws_ArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                List<object> result = EnumExtensions.EnumToList<object>();
            });
        }

        [TestMethod]
        public void TryGetAttribute_ReturnsTrue_WhenAttributeExists()
        {
            ExampleEnum value = ExampleEnum.HasDisplayAttribute;
            DisplayAttribute expected = new DisplayAttribute { Name = TestConstants.DISPLAY_NAME };

            bool result = value.TryGetAttribute(out DisplayAttribute attribute);

            Assert.IsTrue(result);
            Assert.AreEqual(expected.Name, attribute.Name);
        }

        [TestMethod]
        public void TryGetAttribute_ReturnsFalse_WhenAttributeDoesNotExist()
        {
            ExampleEnum value = ExampleEnum.DoesNotHaveAttribute;

            bool result = value.TryGetAttribute(out DisplayAttribute attribute);

            Assert.IsFalse(result);
            Assert.IsNull(attribute);
        }

        [TestMethod]
        public void GetAttribute_ReturnsAttribute_WhenAttributeExists()
        {
            ExampleEnum value = ExampleEnum.HasDisplayAttribute;

            Attribute result = value.GetAttribute<DisplayAttribute>();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAttribute_ReturnsNull_WhenAttributeDoesNotExist()
        {
            ExampleEnum value = ExampleEnum.DoesNotHaveAttribute;

            Attribute result = value.GetAttribute<DisplayAttribute>();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void DescriptionOrName_ReturnsDisplayDescription_WhenDisplayAttributeExists()
        {
            ExampleEnum value = ExampleEnum.HasDisplayDescriptionAttribute;

            string result = value.DescriptionOrName();

            Assert.AreEqual(result, TestConstants.DISPLAY_DESCRIPTION);
        }

        [TestMethod]
        public void DescriptionOrName_ReturnsDisplayName_WhenDisplayAttributeExists()
        {
            ExampleEnum value = ExampleEnum.HasDisplayAttribute;

            string result = value.DescriptionOrName();

            Assert.AreEqual(result, TestConstants.DISPLAY_NAME);
        }

        [TestMethod]
        public void DescriptionOrName_ReturnsToStringResult_WhenDescriptionOrNameAttributesDoNotExist()
        {
            ExampleEnum value = ExampleEnum.DoesNotHaveAttribute;

            string result = value.DescriptionOrName();

            Assert.AreEqual(result, value.ToString());
        }

        [TestMethod]
        public void EnumToDictionary_Returns_Dictionary()
        {
            Dictionary<ExampleEnum, string> result = EnumExtensions.EnumToDictionary<ExampleEnum>();
            Assert.AreEqual(ExampleEnum.DoesNotHaveAttribute.ToString(), result[ExampleEnum.DoesNotHaveAttribute]);
        }

        [TestMethod]
        public void EnumToDictionary_Throws_ArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                Dictionary<int, string> result = EnumExtensions.EnumToDictionary<int>();
            });
        }

        [TestMethod]
        public void GetEnumValueOrNull_ReturnsValue_WhenValueExists()
        {
            ExampleEnum? result = EnumExtensions.GetEnumValueOrNull<ExampleEnum>((int)ExampleEnum.DoesNotHaveAttribute);

            Assert.AreEqual(ExampleEnum.DoesNotHaveAttribute, result);
        }

        [TestMethod]
        public void GetEnumValueOrNull_ReturnsNull_WhenValueDoesNotExist()
        {
            ExampleEnum? result = EnumExtensions.GetEnumValueOrNull<ExampleEnum>(-1);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetEnumValueOrNull_ReturnsNull_WhenValueIsNull()
        {
            ExampleEnum? result = EnumExtensions.GetEnumValueOrNull<ExampleEnum>(null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetEnumValueOrNull_ThrowsArgumentException_WhenTypeIsNotAnEnum()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                int? result = EnumExtensions.GetEnumValueOrNull<int>(-1);
            });
        }

        [TestMethod]
        public void IntegerGetEnumValueOrNull_ReturnsEnumValue_WhenValueExists()
        {
            int integer = 2;

            ExampleEnum? result = integer.GetEnumValueOrNull<ExampleEnum>();

            Assert.IsNotNull(result);
            Assert.AreEqual(ExampleEnum.DoesNotHaveAttribute, result.Value);
        }

        [TestMethod]
        public void IntegerGetEnumValueOrNull_ReturnsNull_WhenValueDoesNotExist()
        {
            int integer = -1;
            ExampleEnum? result = integer.GetEnumValueOrNull<ExampleEnum>();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void IntegerGetEnumValueOrNull_ReturnsNull_WhenIntegerIsNull()
        {
            int? integer = null;
            ExampleEnum? result = integer.GetEnumValueOrNull<ExampleEnum>();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void IntegerGetEnumValueOrNull_ThrowsArgumentException_WhenTypeIsNotAnEnum()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                int integer = -1;
                int? result = integer.GetEnumValueOrNull<int>();
            });
        }
    }
}
