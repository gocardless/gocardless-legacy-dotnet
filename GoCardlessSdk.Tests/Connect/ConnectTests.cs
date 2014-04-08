using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Threading;
using GoCardlessSdk.Connect;
using NUnit.Framework;

namespace GoCardlessSdk.Tests.Connect
{
    public class ConnectTests
    {

        [Test]
        public void NewBillUrl_GreaterThan1000_GeneratesCorrectUrl()
        {
            var request = new BillRequest("0190G74E3J", 1000m);
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            GoCardless.AccountDetails.AppId = "test_id";
            GoCardless.AccountDetails.AppSecret = "test_secret";
            GoCardless.GenerateNonce = () => "Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT+HO3ReWMxavlco0Fw8rva+ZcI";
            GoCardless.GetUtcNow = () => new DateTimeOffset(new DateTime(2012, 03, 21, 08, 55, 56));

            var url = GoCardless.Connect.NewBillUrl(request);
            var expected =
                "https://sandbox.gocardless.com/connect/bills/new?bill%5Bamount%5D=1000.00&bill%5Bmerchant_id%5D=0190G74E3J&client_id=test_id&nonce=Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT%2BHO3ReWMxavlco0Fw8rva%2BZcI&signature=d65039ed59c227bcfa96e3bc1b3d42562a11806db57086509fba17c0028b3f76&timestamp=2012-03-21T08%3A55%3A56Z";
            Assert.AreEqual(expected, url);
        }

        [Test]
        public void NewBillUrl_UkrainianCulture_GeneratesCorrectUrl()
        {
            // test Ukrainian culture, where numbers are formatted 0,00
            Thread.CurrentThread.CurrentCulture = new CultureInfo("uk-UA");

            var request = new BillRequest("0190G74E3J", 10.45m);
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            GoCardless.AccountDetails.AppId = "test_id";
            GoCardless.AccountDetails.AppSecret = "test_secret";
            GoCardless.GenerateNonce = () => "Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT+HO3ReWMxavlco0Fw8rva+ZcI";
            GoCardless.GetUtcNow = () => new DateTimeOffset(new DateTime(2012, 03, 21, 08, 55, 56));

            var url = GoCardless.Connect.NewBillUrl(request);
            var expected =
                "https://sandbox.gocardless.com/connect/bills/new?bill%5Bamount%5D=10.45&bill%5Bmerchant_id%5D=0190G74E3J&client_id=test_id&nonce=Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT%2BHO3ReWMxavlco0Fw8rva%2BZcI&signature=2b67e51028b69a67f0f08c67282386ec625a9b78ebc1936c786602897c52f268&timestamp=2012-03-21T08%3A55%3A56Z";
            Assert.AreEqual(expected, url);
        }

        [Test]
        public void NewBillUrl_ExcOptionalParams_GeneratesCorrectUrl()
        {
            var request = new BillRequest("0190G74E3J", 15m);
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            GoCardless.AccountDetails.AppId = "test_id";
            GoCardless.AccountDetails.AppSecret = "test_secret";
            GoCardless.GenerateNonce = () => "Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT+HO3ReWMxavlco0Fw8rva+ZcI";
            GoCardless.GetUtcNow = () => new DateTimeOffset(new DateTime(2012, 03, 21, 08, 55, 56));

            var url = GoCardless.Connect.NewBillUrl(request);
            var expected =
                "https://sandbox.gocardless.com/connect/bills/new?bill%5Bamount%5D=15.00&bill%5Bmerchant_id%5D=0190G74E3J&client_id=test_id&nonce=Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT%2BHO3ReWMxavlco0Fw8rva%2BZcI&signature=bab27ff9111e292286d207e68722a00a3d8a36d1cf69fb4d094b4443998283b1&timestamp=2012-03-21T08%3A55%3A56Z";
            Assert.AreEqual(expected, url);
        }

        [Test]
        public void NewBillUrl_IncOptionalParams_GeneratesCorrectUrl()
        {
            var request = new BillRequest("0190G74E3J", 15m)
                              {
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

            var url = GoCardless.Connect.NewBillUrl(request);
            var expected =
                "https://sandbox.gocardless.com/connect/bills/new?bill%5Bamount%5D=15.00&bill%5Bdescription%5D=Test%20payment&bill%5Bmerchant_id%5D=0190G74E3J&bill%5Bname%5D=Premium%20Account&bill%5Buser%5D%5Bbilling_address1%5D=Flat%201&bill%5Buser%5D%5Bbilling_address2%5D=100%20Main%20Street&bill%5Buser%5D%5Bbilling_county%5D=Countyshire&bill%5Buser%5D%5Bbilling_postcode%5D=N1%201AB&bill%5Buser%5D%5Bbilling_town%5D=Townville&bill%5Buser%5D%5Bcompany_name_toggle%5D=0&bill%5Buser%5D%5Bemail%5D=john.smith%40example.com&bill%5Buser%5D%5Bfirst_name%5D=John&bill%5Buser%5D%5Blast_name%5D=Smith&bill%5Buser%5D%5Bname%5D=John%20Smith&client_id=test_id&nonce=Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT%2BHO3ReWMxavlco0Fw8rva%2BZcI&signature=21752ad202c27818509355342f45a774ffca945f87e7f76d23a27708b7aed1f5&timestamp=2012-03-21T08%3A55%3A56Z";
            Assert.AreEqual(expected, url);
        }

        [Test]
        public void NewPreAuthorizationUrl_ExcOptionalParams_GeneratesCorrectUrl()
        {
            var request = new PreAuthorizationRequest("0190G74E3J", 15m, 1, "month");
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            GoCardless.AccountDetails.AppId = "test_id";
            GoCardless.AccountDetails.AppSecret = "test_secret";
            GoCardless.GenerateNonce = () => "Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT+HO3ReWMxavlco0Fw8rva+ZcI";
            GoCardless.GetUtcNow = () => new DateTimeOffset(new DateTime(2012, 03, 21, 08, 55, 56));

            var url = GoCardless.Connect.NewPreAuthorizationUrl(request);
            var expected =
                "https://sandbox.gocardless.com/connect/pre_authorizations/new?client_id=test_id&nonce=Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT%2BHO3ReWMxavlco0Fw8rva%2BZcI&pre_authorization%5Binterval_length%5D=1&pre_authorization%5Binterval_unit%5D=month&pre_authorization%5Bmax_amount%5D=15.00&pre_authorization%5Bmerchant_id%5D=0190G74E3J&signature=40792b67ff99a474c2db08b870842bd5b3b82003206e4ab003a69860bbb0a30e&timestamp=2012-03-21T08%3A55%3A56Z";
            Assert.AreEqual(expected, url);
        }

        [Test]
        public void NewPreAuthorizationUrl_IncOptionalParams_GeneratesCorrectUrl()
        {
            var request = new PreAuthorizationRequest("0190G74E3J", 15m, 1, "month")
                              {
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

            var url = GoCardless.Connect.NewPreAuthorizationUrl(request);
            var expected =
                "https://sandbox.gocardless.com/connect/pre_authorizations/new?client_id=test_id&nonce=Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT%2BHO3ReWMxavlco0Fw8rva%2BZcI&pre_authorization%5Bcalendar_intervals%5D=1&pre_authorization%5Bdescription%5D=Test%20preauthorization&pre_authorization%5Bexpires_at%5D=2013-03-24T19%3A32%3A22Z&pre_authorization%5Binterval_count%5D=12&pre_authorization%5Binterval_length%5D=1&pre_authorization%5Binterval_unit%5D=month&pre_authorization%5Bmax_amount%5D=15.00&pre_authorization%5Bmerchant_id%5D=0190G74E3J&pre_authorization%5Bname%5D=Premium%20Account&pre_authorization%5Buser%5D%5Bbilling_address1%5D=Flat%201&pre_authorization%5Buser%5D%5Bbilling_address2%5D=100%20Main%20Street&pre_authorization%5Buser%5D%5Bbilling_county%5D=Countyshire&pre_authorization%5Buser%5D%5Bbilling_postcode%5D=N1%201AB&pre_authorization%5Buser%5D%5Bbilling_town%5D=Townville&pre_authorization%5Buser%5D%5Bcompany_name_toggle%5D=0&pre_authorization%5Buser%5D%5Bemail%5D=john.smith%40example.com&pre_authorization%5Buser%5D%5Bfirst_name%5D=John&pre_authorization%5Buser%5D%5Blast_name%5D=Smith&pre_authorization%5Buser%5D%5Bname%5D=John%20Smith&signature=e1746ec91c368f6238038914670e2301e55993b80be0251015b24de06928e88e&timestamp=2012-03-21T08%3A55%3A56Z";
            Assert.AreEqual(expected, url);
        }

        [Test]
        public void NewSubscriptionUrl_ExcOptionalParams_GeneratesCorrectUrl()
        {
            var request = new SubscriptionRequest("0190G74E3J", 15m, 1, "month");
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            GoCardless.AccountDetails.AppId = "test_id";
            GoCardless.AccountDetails.AppSecret = "test_secret";
            GoCardless.GenerateNonce = () => "Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT+HO3ReWMxavlco0Fw8rva+ZcI";
            GoCardless.GetUtcNow = () => new DateTimeOffset(new DateTime(2012, 03, 21, 08, 55, 56));

            var url = GoCardless.Connect.NewSubscriptionUrl(request);
            var expected =
                "https://sandbox.gocardless.com/connect/subscriptions/new?client_id=test_id&nonce=Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT%2BHO3ReWMxavlco0Fw8rva%2BZcI&signature=5e17c04a0f7f211f55bba6ba633cb06121eb79c3284a5a1d9ac0ba5d0bce0c80&subscription%5Bamount%5D=15.00&subscription%5Binterval_length%5D=1&subscription%5Binterval_unit%5D=month&subscription%5Bmerchant_id%5D=0190G74E3J&timestamp=2012-03-21T08%3A55%3A56Z";
            Assert.AreEqual(expected, url);
        }

        [Test]
        public void NewSubscriptionUrl_IncOptionalParams_GeneratesCorrectUrl()
        {
            var request = new SubscriptionRequest("0190G74E3J", 15m, 1, "month")
                              {
                                  StartAt = new DateTimeOffset(new DateTime(2012, 03, 24, 19, 32, 22)),
                                  ExpiresAt = new DateTimeOffset(new DateTime(2013, 03, 24, 19, 32, 22)),
                                  Name = "Premium Account",
                                  Description = "test subscription",
                                  IntervalCount = 12,
                                  SetupFee = 10,
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
            GoCardless.GetUtcNow = () => new DateTimeOffset(new DateTime(2012, 03, 21, 13, 08, 56));

            var url = GoCardless.Connect.NewSubscriptionUrl(request);
			Console.WriteLine(url);
			var expected = "https://sandbox.gocardless.com/connect/subscriptions/new?client_id=test_id&nonce=Q9gMPVBZixfRiQ9VnRdDyrrMiskqT0ox8IT%2BHO3ReWMxavlco0Fw8rva%2BZcI&signature=ba1c79bbc899f0a6d93b5470f56d886297c18eb3aa63af9fdf5be21e9518e545&subscription%5Bamount%5D=15.00&subscription%5Bdescription%5D=test%20subscription&subscription%5Bexpires_at%5D=2013-03-24T19%3A32%3A22Z&subscription%5Binterval_count%5D=12&subscription%5Binterval_length%5D=1&subscription%5Binterval_unit%5D=month&subscription%5Bmerchant_id%5D=0190G74E3J&subscription%5Bname%5D=Premium%20Account&subscription%5Bsetup_fee%5D=10.00&subscription%5Bstart_at%5D=2012-03-24T19%3A32%3A22Z&subscription%5Buser%5D%5Bbilling_address1%5D=Flat%201&subscription%5Buser%5D%5Bbilling_address2%5D=100%20Main%20Street&subscription%5Buser%5D%5Bbilling_county%5D=Countyshire&subscription%5Buser%5D%5Bbilling_postcode%5D=N1%201AB&subscription%5Buser%5D%5Bbilling_town%5D=Townville&subscription%5Buser%5D%5Bcompany_name_toggle%5D=0&subscription%5Buser%5D%5Bemail%5D=john.smith%40example.com&subscription%5Buser%5D%5Bfirst_name%5D=John&subscription%5Buser%5D%5Blast_name%5D=Smith&subscription%5Buser%5D%5Bname%5D=John%20Smith&timestamp=2012-03-21T13%3A08%3A56Z";
		    Assert.AreEqual(expected, url);
        }

        [Test]
        public void ConfirmResourceWithEncodedUri()
        {
            var request = new NameValueCollection();

            GoCardless.AccountDetails.AppSecret = "WJZ4CHRCWTMR3ZGV7ZT7KB6PG7KG5J6FV97V1TP4PES1GNC5HQ7BQT27R7AQZYW9";
            GoCardless.AccountDetails.AppId = "Y12SN2C4F4ES1NEGRSBN9X5MZWT1TKNX5E18TK8BN6G74432Y35P4ZG7VS2GBPVV";

            request["resource_id"] = "0D2VPBEFXN";
            request["resource_type"] = "bill";
            request["resource_uri"] = "https%3A%2F%2Fsandbox.gocardless.com%2Fapi%2Fv1%2Fbills%2F0D2VPBEFXN";
            request["state"] = "7473";
            request["signature"] = "61ae90cb17130db50056062b215635405d156e5a0014f95d30f046b691fdff20";

            GoCardless.Connect.DeserializeAndValidateRequestSignature(request);
        }

        [Test]
        public void ConfirmResourceWithNonEncodedUri()
        {
            var request = new NameValueCollection();

            GoCardless.AccountDetails.AppSecret = "WJZ4CHRCWTMR3ZGV7ZT7KB6PG7KG5J6FV97V1TP4PES1GNC5HQ7BQT27R7AQZYW9";
            GoCardless.AccountDetails.AppId = "Y12SN2C4F4ES1NEGRSBN9X5MZWT1TKNX5E18TK8BN6G74432Y35P4ZG7VS2GBPVV";

            request["resource_id"] = "0D2VPBEFXN";
            request["resource_type"] = "bill";
            request["resource_uri"] = "https://sandbox.gocardless.com/api/v1/bills/0D2VPBEFXN";
            request["state"] = "7473";
            request["signature"] = "61ae90cb17130db50056062b215635405d156e5a0014f95d30f046b691fdff20";

            GoCardless.Connect.DeserializeAndValidateRequestSignature(request);
        }
    }
}
