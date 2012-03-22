using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;

namespace GoCardlessSdk.Tests.Api
{
    public class DeepAssertHelper
    {
        public static void AssertIEnumerableDeepEquality(IEnumerable expected, IEnumerable actual, string prefix = "")
        {
            var expectedArray = expected.Cast<object>().ToArray();
            var actualArray = actual.Cast<object>().ToArray();
            Assert.AreEqual(expectedArray.Length, actualArray.Length, "IEnumerables should be the same length");
            for (var i = 0; i < expectedArray.Length; i++)
            {
                AssertDeepEquality(expectedArray[i], actualArray[i], prefix + "Item[" + i + "].");
            }
        }

        public static void AssertDeepEquality(object expected, object actual, string prefix = null)
        {
            if (expected == null && actual != null) Assert.Fail("Actual " + prefix + " was not null");
            if (expected != null && actual == null) Assert.Fail("Actual " + prefix + " was null");
            if (expected == null) return;
            foreach (var property in expected.GetType().GetProperties())
            {
                var type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                var expectedValue = property.GetValue(expected, null);
                var actualValue = property.GetValue(actual, null);

                if (type.IsValueType || type == typeof(string))
                {
                    Assert.AreEqual(expectedValue, actualValue, prefix + property.Name);
                }
                else
                {
                    AssertDeepEquality(expectedValue, actualValue, prefix + property.Name);
                }
            }

        }
    }
}
