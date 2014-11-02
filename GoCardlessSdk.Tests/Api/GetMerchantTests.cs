using System;
using GoCardlessSdk.Api;
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

      
    }
}