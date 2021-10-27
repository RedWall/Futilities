using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Futilities.ObjectExtensions.Tests
{
    [TestClass]
    public class ObjectExtensionsTests
    {
        [TestMethod]
        [TestProperty("FooPropertyValue", "bar")]
        public void SetProperty_Extension_Sets_Property()
        {
            TryGetTestProperty(nameof(SetProperty_Extension_Sets_Property), "FooPropertyValue", out string fooPropertyValue);

            var testObject = new TestClass();

            testObject.SetProperty(fooPropertyValue, o => o.Foo);

            Assert.AreEqual(fooPropertyValue, testObject.Foo);
        }

        [TestMethod]
        [TestProperty("FooPropertyValue", "bar")]
        public void GetValue_Extension_Returns_Value()
        {
            TryGetTestProperty(nameof(GetValue_Extension_Returns_Value), "FooPropertyValue", out string fooPropertyValue);

            var testObject = new TestClass { Foo = fooPropertyValue };

            var testPropertyValue = testObject.GetValue(o => o.Foo);

            Assert.AreEqual(testObject.Foo, testPropertyValue);
        }

        [ExcludeFromCodeCoverage]
        private bool TryGetTestProperty<T>(string methodName, string propertyName, out T propertyValue)
        {
            try
            {
                Type type = GetType();

                MethodInfo methodInfo = type.GetMethod(methodName);

                IEnumerable<TestPropertyAttribute> attribute = methodInfo.GetCustomAttributes<TestPropertyAttribute>();

                var value = attribute.FirstOrDefault(a => a.Name == propertyName)?.Value ?? null;

                propertyValue = (T)Convert.ChangeType(value, typeof(T));

                if (value is null)
                    return false;

                return true;
            }
            catch (Exception)
            {
                propertyValue = default;
                return false;
            }
        }
    }
}