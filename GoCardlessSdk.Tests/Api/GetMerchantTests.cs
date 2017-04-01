using System;
using System.IO;
using GoCardlessSdk.Api;
using GoCardlessSdk.Helpers;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GoCardlessSdk.Tests.Api
{
    public class GetMerchantTests
    {
        [Test]
        public void CanFetchAndDeserializeCorrectly()
        {
            var expected = new MerchantResponse
            {
                CreatedAt = DateTimeOffset.Parse("2011-11-18T17:07:09Z"),
                Description = null,
                Id = "WOQRUJU9OH2HH1",
                Name = "Tom's Delicious Chicken Shop",
                FirstName = "Tom",
                LastName = "Blomfield",
                Email = "tom@gocardless.com",
                Uri = "https://gocardless.com/api/v1/merchants/WOQRUJU9OH2HH1",
                Balance = 12.00m,
                PendingBalance = 0.00m,
                NextPayoutDate = DateTimeOffset.Parse("2011-11-25T17: 07: 09Z"),
                NextPayoutAmount = 12.00m,
                SubResourceUris =
                    {
                        Users = "https://gocardless.com/api/v1/merchants/WOQRUJU9OH2HH1/users",
                        Bills = "https://gocardless.com/api/v1/merchants/WOQRUJU9OH2HH1/bills",
                        PreAuthorizations = "https://gocardless.com/api/v1/merchants/WOQRUJU9OH2HH1/pre_authorizations",
                        Subscriptions = "https://gocardless.com/api/v1/merchants/WOQRUJU9OH2HH1/subscriptions",
                    }
            };
            DeepAssertHelper.AssertDeepEquality(expected, new ApiClient("asdf").GetMerchant("WOQRUJU9OH2HH1"));
        }

        private MerchantResponse ParseMerchantJson() {
            const string JSON_DATA = "{ \"id\": \"MERCHANT01\", \"name\": \"Test Merchant\", \"description\": \"Test merchant for reproducing serialization bug in GoCardless SDK\", \"created_at\": \"2014-12-19T12:46:36Z\", \"first_name\": \"Test\", \"last_name\": \"Merchant\", \"email\": \"help@gocardless.com\", \"uri\": \"https://gocardless.com/api/v1/merchants/MERCHANT01\", \"balance\": \"0.0\", \"pending_balance\": \"0.0\", \"next_payout_date\": null, \"next_payout_amount\": null, \"hide_variable_amount\": false, \"sub_resource_uris\": { \"users\": \"https://gocardless.com/api/v1/merchants/MERCHANT01/users\", \"bills\": \"https://gocardless.com/api/v1/merchants/MERCHANT01/bills\", \"pre_authorizations\": \"https://gocardless.com/api/v1/merchants/MERCHANT01/pre_authorizations\", \"subscriptions\": \"https://gocardless.com/api/v1/merchants/MERCHANT01/subscriptions\", \"payouts\": \"https://gocardless.com/api/v1/merchants/MERCHANT01/payouts\" } }";
            var serializer = new JsonSerializer { ContractResolver = new UnderscoreToCamelCasePropertyResolver() };
            var reader = new JsonTextReader(new StringReader(JSON_DATA));
            return(serializer.Deserialize<MerchantResponse>(reader));
        }

        [Test]
        public void CanDeserializeMerchantResponseWithNullPayoutAmount() {
            var response = ParseMerchantJson();
            Assert.IsNull(response.NextPayoutAmount);
        }

        [Test]
        public void CanDeserializeMerchantResponseWithNullPayoutDate() {
            var response = ParseMerchantJson();
            Assert.IsNull(response.NextPayoutDate);
        }
    }
}