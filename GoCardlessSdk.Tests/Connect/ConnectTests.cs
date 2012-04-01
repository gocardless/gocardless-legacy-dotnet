using System;
using GoCardlessSdk.Connect;
using NUnit.Framework;

namespace GoCardlessSdk.Tests.Connect
{
    public class ConnectTests
    {
        [Test]
        public void NewBillUrl_ExcOptionalParams_GeneratesCorrectUrl()
        {
            var @params = new BillRequest
                                   {
                                       Amount = 15m,
                                       MerchantId = "0190G74E3J",
                                   };
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            GoCardless.AccountDetails.AppId = "test_id";
            GoCardless.AccountDetails.AppSecret = "test_secret";
            GoCardless.GenerateNonce = () => "Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT+HO3ReWMxavlco0Fw8rva+ZcI";
            GoCardless.GetUtcNow = () => new DateTimeOffset(new DateTime(2012, 03, 21, 08, 55, 56));

            var url = ConnectClient.NewBillUrl(@params);
            var expected =
                "https://sandbox.gocardless.com/connect/bills/new?bill%5Bamount%5D=15.00&bill%5Bmerchant_id%5D=0190G74E3J&client_id=test_id&nonce=Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT%2BHO3ReWMxavlco0Fw8rva%2BZcI&signature=bab27ff9111e292286d207e68722a00a3d8a36d1cf69fb4d094b4443998283b1&timestamp=2012-03-21T08%3A55%3A56Z";
            Assert.AreEqual(expected, url);
        }

        [Test]
        public void NewBillUrl_IncOptionalParams_GeneratesCorrectUrl()
        {
            var @params = new BillRequest
            {
                Amount = 15m,
                   MerchantId = "0190G74E3J",

             Name = "Premium Account",
                Description = "Test payment",
                User = new UserRequest
                {
                    Name = "John Smith",
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "john.smith@example.com",
                    BillingAddress1 = "Flat 1",
                    BillingAddress2 = "100 Main Street",
                    BillingTown = "Townville",
                    BillingCounty = "Countyshire",
                    BillingPostcode = "N1 1AB",
                }
            };
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            GoCardless.AccountDetails.AppId = "test_id";
            GoCardless.AccountDetails.AppSecret = "test_secret";
            GoCardless.GenerateNonce = () => "Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT+HO3ReWMxavlco0Fw8rva+ZcI";
            GoCardless.GetUtcNow = () => new DateTimeOffset(new DateTime(2012, 03, 21, 08, 55, 56));

            var url = ConnectClient.NewBillUrl(@params);
            var expected =
                "https://sandbox.gocardless.com/connect/bills/new?bill%5Bamount%5D=15.00&bill%5Bdescription%5D=Test%20payment&bill%5Bmerchant_id%5D=0190G74E3J&bill%5Bname%5D=Premium%20Account&bill%5Buser%5D%5Bbilling_address1%5D=Flat%201&bill%5Buser%5D%5Bbilling_address2%5D=100%20Main%20Street&bill%5Buser%5D%5Bbilling_county%5D=Countyshire&bill%5Buser%5D%5Bbilling_postcode%5D=AB12%203CD&bill%5Buser%5D%5Bbilling_town%5D=Townville&bill%5Buser%5D%5Bemail%5D=john.smith%40example.com&bill%5Buser%5D%5Bfirst_name%5D=John&bill%5Buser%5D%5Blast_name%5D=Smith&bill%5Buser%5D%5Bname%5D=John%20Smith&client_id=test_id&nonce=Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT%2BHO3ReWMxavlco0Fw8rva%2BZcI&signature=8bff4a2989828bed0e1d2e085326d1f3a65ceed85ee8bbb7e3f512a87bba3e6b&timestamp=2012-03-21T08%3A55%3A56Z";
            Assert.AreEqual(expected, url);
        }

        [Test]
        public void NewPreAuthorizationUrl_ExcOptionalParams_GeneratesCorrectUrl()
        {
            var @params = new PreAuthorizationRequest
            {
                MaxAmount = 15m,
                MerchantId = "0190G74E3J",
                IntervalLength = 1,
                IntervalUnit = "month",
            };
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            GoCardless.AccountDetails.AppId = "test_id";
            GoCardless.AccountDetails.AppSecret = "test_secret";
            GoCardless.GenerateNonce = () => "Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT+HO3ReWMxavlco0Fw8rva+ZcI";
            GoCardless.GetUtcNow = () => new DateTimeOffset(new DateTime(2012, 03, 21, 08, 55, 56));

            var url = ConnectClient.NewPreAuthorizationUrl(@params);
            var expected =
                "https://sandbox.gocardless.com/connect/pre_authorizations/new?client_id=test_id&nonce=Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT%2BHO3ReWMxavlco0Fw8rva%2BZcI&pre_authorization%5Binterval_length%5D=1&pre_authorization%5Binterval_unit%5D=month&pre_authorization%5Bmax_amount%5D=15.00&pre_authorization%5Bmerchant_id%5D=0190G74E3J&signature=40792b67ff99a474c2db08b870842bd5b3b82003206e4ab003a69860bbb0a30e&timestamp=2012-03-21T08%3A55%3A56Z";
            Assert.AreEqual(expected, url);
        }

        [Test]
        public void NewPreAuthorizationUrl_IncOptionalParams_GeneratesCorrectUrl()
        {
            var @params = new PreAuthorizationRequest
                                   {
                                       MaxAmount = 15m,
                                       MerchantId = "0190G74E3J",
                                       IntervalLength = 1,
                                       IntervalUnit = "month",

                                       ExpiresAt = new DateTimeOffset(new DateTime(2013, 03, 24, 19, 32, 22)),
                                       Name = "Premium Account",
                                       Description = "Test preauthorization",
                                       IntervalCount = 12,
                                       CalendarIntervals = true,
                                       User = new UserRequest
                                       {
                                           Name = "John Smith",
                                           FirstName = "John",
                                           LastName = "Smith",
                                           Email = "john.smith@example.com",
                                           BillingAddress1 = "Flat 1",
                                           BillingAddress2 = "100 Main Street",
                                           BillingTown = "Townville",
                                           BillingCounty = "Countyshire",
                                           BillingPostcode = "N1 1AB",
                                       }
                                   };
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            GoCardless.AccountDetails.AppId = "test_id";
            GoCardless.AccountDetails.AppSecret = "test_secret";
            GoCardless.GenerateNonce = () => "Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT+HO3ReWMxavlco0Fw8rva+ZcI";
            GoCardless.GetUtcNow = () => new DateTimeOffset(new DateTime(2012, 03, 21, 08, 55, 56));

            var url = ConnectClient.NewPreAuthorizationUrl(@params);
            var expected =
                "https://sandbox.gocardless.com/connect/pre_authorizations/new?client_id=test_id&nonce=Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT%2BHO3ReWMxavlco0Fw8rva%2BZcI&pre_authorization%5Bcalendar_intervals%5D=True&pre_authorization%5Bdescription%5D=Test%20preauthorization&pre_authorization%5Bexpires_at%5D=2013-03-24T19%3A32%3A22Z&pre_authorization%5Binterval_count%5D=12&pre_authorization%5Binterval_length%5D=1&pre_authorization%5Binterval_unit%5D=month&pre_authorization%5Bmax_amount%5D=15.00&pre_authorization%5Bmerchant_id%5D=0190G74E3J&pre_authorization%5Bname%5D=Premium%20Account&pre_authorization%5Buser%5D%5Bbilling_address1%5D=Flat%201&pre_authorization%5Buser%5D%5Bbilling_address2%5D=100%20Main%20Street&pre_authorization%5Buser%5D%5Bbilling_county%5D=Countyshire&pre_authorization%5Buser%5D%5Bbilling_postcode%5D=AB12%203CD&pre_authorization%5Buser%5D%5Bbilling_town%5D=Townville&pre_authorization%5Buser%5D%5Bemail%5D=john.smith%40example.com&pre_authorization%5Buser%5D%5Bfirst_name%5D=John&pre_authorization%5Buser%5D%5Blast_name%5D=Smith&pre_authorization%5Buser%5D%5Bname%5D=John%20Smith&signature=ce364a27e77455e32c1c1246bd4306055f8afee41fe28e1bf7b90e9e8f9f88a8&timestamp=2012-03-21T08%3A55%3A56Z";
            Assert.AreEqual(expected, url);
        }

        [Test]
        public void NewSubscriptionUrl_ExcOptionalParams_GeneratesCorrectUrl()
        {
            var @params = new SubscriptionRequest
                                   {
                                       Amount = 15m,
                                       MerchantId = "0190G74E3J",
                                       IntervalLength = 1,
                                       IntervalUnit = "month",
                                   };
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            GoCardless.AccountDetails.AppId = "test_id";
            GoCardless.AccountDetails.AppSecret = "test_secret";
            GoCardless.GenerateNonce = () => "Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT+HO3ReWMxavlco0Fw8rva+ZcI";
            GoCardless.GetUtcNow = () => new DateTimeOffset(new DateTime(2012, 03, 21, 08, 55, 56));

            var url = ConnectClient.NewSubscriptionUrl(@params);
            var expected =
                "https://sandbox.gocardless.com/connect/subscriptions/new?client_id=test_id&nonce=Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT%2BHO3ReWMxavlco0Fw8rva%2BZcI&signature=5e17c04a0f7f211f55bba6ba633cb06121eb79c3284a5a1d9ac0ba5d0bce0c80&subscription%5Bamount%5D=15.00&subscription%5Binterval_length%5D=1&subscription%5Binterval_unit%5D=month&subscription%5Bmerchant_id%5D=0190G74E3J&timestamp=2012-03-21T08%3A55%3A56Z";
            Assert.AreEqual(expected, url);
        }

        [Test]
        public void NewSubscriptionUrl_IncOptionalParams_GeneratesCorrectUrl()
        {
            var @params = new SubscriptionRequest
                                   {
                                       Amount = 15m,
                                       MerchantId = "0190G74E3J",
                                       IntervalLength = 1,
                                       IntervalUnit = "month",

                                       StartAt = new DateTimeOffset(new DateTime(2012, 03, 24, 19, 32, 22)),
                                       ExpiresAt = new DateTimeOffset(new DateTime(2013, 03, 24, 19, 32, 22)),
                                       Name = "Premium Account",
                                       Description = "test subscription",
                                       IntervalCount = 12,
                                       User = new UserRequest
                                                  {
                                                      Name = "John Smith",
                                                      FirstName = "John",
                                                      LastName = "Smith",
                                                      Email = "john.smith@example.com",
                                                      BillingAddress1 = "Flat 1",
                                                      BillingAddress2 = "100 Main Street",
                                                      BillingTown = "Townville",
                                                      BillingCounty = "Countyshire",
                                                      BillingPostcode = "N1 1AB",
                                                  }
                                   };
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            GoCardless.AccountDetails.AppId = "test_id";
            GoCardless.AccountDetails.AppSecret = "test_secret";
            GoCardless.GenerateNonce = () => "Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT+HO3ReWMxavlco0Fw8rva+ZcI";
            GoCardless.GetUtcNow = () => new DateTimeOffset(new DateTime(2012, 03, 21, 08, 55, 56));

            var url = ConnectClient.NewSubscriptionUrl(@params);
            var expected =
                "https://sandbox.gocardless.com/connect/subscriptions/new?client_id=test_id&nonce=Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT%2BHO3ReWMxavlco0Fw8rva%2BZcI&signature=ee2e43caabcec4f3369460a889ff010cc411eda60f9ea9974b5d195175cc14c0&subscription%5Bamount%5D=15.00&subscription%5Bdescription%5D=test%20subscription&subscription%5Bexpires_at%5D=2013-03-24T19%3A32%3A22Z&subscription%5Binterval_count%5D=12&subscription%5Binterval_length%5D=1&subscription%5Binterval_unit%5D=month&subscription%5Bmerchant_id%5D=0190G74E3J&subscription%5Bname%5D=Premium%20Account&subscription%5Bstart_at%5D=2012-03-24T19%3A32%3A22Z&subscription%5Buser%5D%5Bbilling_address1%5D=Flat%201&subscription%5Buser%5D%5Bbilling_address2%5D=100%20Main%20Street&subscription%5Buser%5D%5Bbilling_county%5D=Countyshire&subscription%5Buser%5D%5Bbilling_postcode%5D=AB12%203CD&subscription%5Buser%5D%5Bbilling_town%5D=Townville&subscription%5Buser%5D%5Bemail%5D=john.smith%40example.com&subscription%5Buser%5D%5Bfirst_name%5D=John&subscription%5Buser%5D%5Blast_name%5D=Smith&subscription%5Buser%5D%5Bname%5D=John%20Smith&timestamp=2012-03-21T08%3A55%3A56Z";
            Assert.AreEqual(expected, url);
        }
    }
}
