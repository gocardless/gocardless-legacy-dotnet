using GoCardlessSdk.Api;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace GoCardlessSdk.Tests.Api
{
    public class GetErrorTests
    {
        private static readonly JObject Expected = JObject.FromObject(new { amount = new[] { "is not a number" } });
        [Test]
        public void CanFetchAndDeserializeCorrectly()
        {
            try
            {
                new ApiClient("asdf").GetBill("213"); //the document returns a 400
                Assert.Fail("Should have thrown an error instead of reaching this point");
            }
            catch (ApiException ex)
            {
                Assert.AreEqual(Expected.ToString(), ex.Content.ToString());
            }
        }
    }
}