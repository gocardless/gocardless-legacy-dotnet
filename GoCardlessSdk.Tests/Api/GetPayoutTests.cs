using System;
using System.Linq;
using GoCardlessSdk.Api;
using NUnit.Framework;

namespace GoCardlessSdk.Tests.Api
{
    public class GetPayoutTests
    {

        private PayoutResponse SamplePayout = new PayoutResponse
        {
            Amount = 12.37m,
            BankReference = "JOHNSMITH-Z5DRM",
            CreatedAt = DateTimeOffset.Parse("2013-05-10T16:34:34Z"),
            Id = "0BKR1AZNJF",
            PaidAt = DateTimeOffset.Parse("2013-05-10T17:00:26Z"),
            TransactionFees = 0.13m
        };

        [Test]
        public void CanFetchAndDeserializeCorrectly()
        {
            DeepAssertHelper.AssertDeepEquality(SamplePayout, new ApiClient("asdf").GetPayout("0BKR1AZNJF"));
        }

        [Test]
        public void CanFetchMerchantPayouts()
        {
            var payouts = new ApiClient("asdf").GetMerchantPayouts("WOQRUJU9OH2HH1").ToArray();
            Assert.AreEqual(2, payouts.Length);
        }
    }
}