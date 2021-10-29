using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Futilities.Testing
{

    public class TestingBase
    {
        public bool TryGetTestProperty<T>(string methodName, string propertyName, out T propertyValue) => this.TryGetTestPropertyValue(methodName, propertyName, out propertyValue);
    }

    public static class TestingExtensions
    {
        [ExcludeFromCodeCoverage]
        public static bool TryGetTestPropertyValue<T>(this object obj, string methodName, string propertyName, out T propertyValue)
        {
            try
            {
                Type type = obj.GetType();

                MethodInfo methodInfo = type.GetMethod(methodName);

                var attribute = methodInfo.GetCustomAttributes<TestPropertyAttribute>();

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
