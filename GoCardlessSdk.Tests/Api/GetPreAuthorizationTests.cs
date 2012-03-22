using System;
using GoCardlessSdk.Api;
using NUnit.Framework;

namespace GoCardlessSdk.Tests.Api
{
    public class GetPreAuthorizationTests
    {
        [Test]
        public void CanFetchAndDeserializeCorrectly()
        {
            var expected = new PreAuthorizationResponse
                {
                    CreatedAt = DateTimeOffset.Parse("2011-02-18T15:25:58Z"),
                    Currency = "GBP",
                    Name = "Variable Payments For Tennis Court Rental",
                    Description = "You will be charged according to your monthly usage of the tennis courts",
                    ExpiresAt = null,
                    Id = "1234JKH8KLJ",
                    IntervalLength = 1,
                    IntervalUnit = "month",
                    MerchantId = "WOQRUJU9OH2HH1",
                    Status = "active",
                    RemainingAmount = 65.0m,
                    NextIntervalStart = DateTimeOffset.Parse("2012-02-20T00:00:00Z"),
                    UserId = "834JUH8KLJ",
                    MaxAmount = 70.0m,
                    Uri = "https://gocardless.com/api/v1/pre_authorizations/1610",
                    SubResourceUris =
                    {
                        Bills = "https://gocardless.com/api/v1/merchants/WOQRUJU9OH2HH1/bills?source_id=1610"
                    }
                }
            ;
            DeepAssertHelper.AssertDeepEquality(expected, new ApiClient("asdf").GetPreAuthorization("1234JKH8KLJ"));
        }
    }
}