using System;
using System.Text;
using GoCardlessSdk.Api;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace GoCardlessSdk.Tests.Api
{
    public class PostBillTests
    {
        [Test]
        public void CanFetchAndDeserializeCorrectly()
        {
            var expected = new BillResponse
            {
                Amount = 10.00m,
                GocardlessFees = 0.10m,
                PartnerFees = 0m,
                Currency = "GBP",
                CreatedAt = DateTime.Parse("2011-11-22T11: 59: 12Z"),
                Description = null,
                Id = "PWSDXRYSCOKA7Z",
                Name = null,
                Status = "pending",
                MerchantId = "6UFY9IJWGYBTAP",
                UserId = "BWJ2GP659OXPAU",
                PaidAt = null,
                SourceType = "pre_authorization",
                SourceId = "FAZ6FGSMTCOZUG",
                Uri = "https://gocardless.com/api/v1/bills/PWSDXRYSCOKA7Z"
            };

            DeepAssertHelper.AssertDeepEquality(expected, new ApiClient("asdf").PostBill(44, "AJKH638A99"));
        }

        [Test]
        public void PostsDataCorrectly()
        {

            var body = null as string;
            Fiddler.SessionStateHandler inspect = s =>
            {
                body = Encoding.UTF8.GetString(s.requestBodyBytes);
            };
            Fiddler.FiddlerApplication.BeforeRequest += inspect;
            new ApiClient("asdf").PostBill(44, "AJKH638A99", "some name", "some description");
            Fiddler.FiddlerApplication.BeforeRequest -= inspect;
            var expected = JObject.Parse("{bill: { amount: '44', pre_authorization_id: 'AJKH638A99', charge_customer_at: null, name: 'some name', description: 'some description' }}");

            Assert.AreEqual(expected.ToString(), JObject.Parse(body).ToString());
        }

        [Test]
        public void PostsRetryCorrectly()
        {
            var url = null as string;
            Fiddler.SessionStateHandler inspect = s =>
            {
                url = s.url;
            };

            Fiddler.FiddlerApplication.BeforeRequest += inspect;
            new ApiClient("asdf").RetryBill("AJKH338A19");
            Fiddler.FiddlerApplication.BeforeRequest -= inspect;
            Assert.AreEqual("gocardless.com/api/v1/bills/AJKH338A19/retry", url);
        }
    }
}
