using System;
using GoCardlessSdk.Api;
using NUnit.Framework;

namespace GoCardlessSdk.Tests.Api
{
    public class GetBillTests
    {
        [Test]
        public void CanFetchAndDeserializeCorrectly()
        {
            var expected = new BillResponse
            {
                Amount = 44.0m,
                GocardlessFees = 0.44m,
                PartnerFees = 0m,
                Currency = "GBP",
                CreatedAt = DateTimeOffset.Parse("2011-11-04T21: 41: 25Z"),
                Description = "Month 2 payment",
                Id = "VZUG2SC3PRT5EM",
                Name = "Bill 2 for Subscription description",
                PaidAt = DateTimeOffset.Parse("2011-11-07T15: 00: 00Z"),
                Status = "paid",
                MerchantId = "WOQRUJU9OH2HH1",
                UserId = "FIVWCCVEST6S4D",
                SourceType = "subscription",
                SourceId = "YH1VEVQHYVB1UT",
                Uri = "https://gocardless.com/api/v1/bills/VZUG2SC3PRT5EM"
            };
            DeepAssertHelper.AssertDeepEquality(expected, new ApiClient("asdf").GetBill("VZUG2SC3PRT5EM"));
        }

    }
}