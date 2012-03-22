using System;
using GoCardlessSdk.Api;
using NUnit.Framework;

namespace GoCardlessSdk.Tests.Api
{
    public class CancelSubscriptionTests
    {
        [Test]
        public void CanFetchAndDeserializeCorrectly()
        {
            var expected = new SubscriptionResponse
            {
                Amount = 44.0m,
                IntervalLength = 1,
                IntervalUnit = "month",
                CreatedAt = DateTimeOffset.Parse("2011-09-12T13:51:30Z"),
                Currency = "GBP",
                Name = "London Gym Membership",
                Description = "Entitles you to use all of the gyms around London",
                ExpiresAt = null,
                Id = "1580",
                MerchantId = "WOQRUJU9OH2HH1",
                Status = "cancelled",
                UserId = "638",
                Uri = "https://gocardless.com/api/v1/subscriptions/1580",
                SubResourceUris =
                    {
                        Bills = "https://gocardless.com/api/v1/merchants/WOQRUJU9OH2HH1/bills?source_id=1580"
                    }
            };

            DeepAssertHelper.AssertDeepEquality(expected, new ApiClient("asdf").CancelSubscription("1580"));
        }
    }
}