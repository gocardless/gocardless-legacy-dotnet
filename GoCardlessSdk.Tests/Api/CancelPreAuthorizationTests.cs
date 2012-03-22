using System;
using GoCardlessSdk.Api;
using NUnit.Framework;

namespace GoCardlessSdk.Tests.Api
{
    public class CancelPreAuthorizationTests
    {
        [Test]
        public void CanFetchAndDeserializeCorrectly()
        {
            var expected = new PreAuthorizationResponse
            {
                CreatedAt = DateTime.Parse("2011-02-18T15:25:58Z"),
                Currency = "GBP",
                Name = "Variable Payments For Tennis Court Rental",
                Description = "You will be charged according to your monthly usage of the tennis courts",
                ExpiresAt = null,
                Id = "1609",
                IntervalLength = 1,
                IntervalUnit = "month",
                MerchantId = "WOQRUJU9OH2HH1",
                Status = "cancelled",
                RemainingAmount = 65.0m,
                UserId = "604",
                MaxAmount = 70.0m,
                Uri = "https://gocardless.com/api/v1/pre_authorizations/1609",
                SubResourceUris =
                {
                    Bills = "https://gocardless.com/api/v1/merchants/WOQRUJU9OH2HH1/bills?source_id=1609"
                }
            };

            DeepAssertHelper.AssertDeepEquality(expected, new ApiClient("asdf").CancelPreAuthorization("1580"));
        }
    }
}