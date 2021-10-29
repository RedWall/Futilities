using Futilities.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Futilities.ObjectExtensions.Tests
{
    [TestClass]
    public class ObjectExtensionsTests : TestingBase
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

    }
}