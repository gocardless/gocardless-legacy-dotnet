using System;
using GoCardlessSdk.Connect;
using NUnit.Framework;

namespace GoCardlessSdk.Tests.Connect
{
    public class ConnectTests
    {
        [Test]
        public void NewSubscriptionUrl_GeneratesCorrectSignature()
        {
            var subscription = new SubscriptionRequest
                                   {
                                       Amount = 15m,
                                       Name="Premium Account",
                                       IntervalUnit = "month",
                                       IntervalLength = 1,
                                       MerchantId = "0190G74E3J",
                                   };
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            GoCardless.AccountDetails.AppId = "test_id";
            GoCardless.AccountDetails.AppSecret = "test_secret";
            GoCardless.GenerateNonce = () => "Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT+HO3ReWMxavlco0Fw8rva+ZcI";
            GoCardless.GetUtcNow = () => new DateTimeOffset(new DateTime(2012, 03, 21, 08, 55, 56));

            var url = ConnectClient.NewSubscriptionUrl(subscription);
            var expected =
                "https://sandbox.gocardless.com/connect/subscriptions/new?client_id=test_id&nonce=Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT%2BHO3ReWMxavlco0Fw8rva%2BZcI&signature=87be2d342d4b3ad1bbd846c4dddd587e363d8c4c787a6851750b1c606bb72017&subscription%5Bamount%5D=15.00&subscription%5Binterval_length%5D=1&subscription%5Binterval_unit%5D=month&subscription%5Bmerchant_id%5D=0190G74E3J&subscription%5Bname%5D=Premium%20Account&timestamp=2012-03-21T08%3A55%3A56Z";
            Assert.AreEqual(expected, url);
        }

        // TODO: other endpoints
    }
}
