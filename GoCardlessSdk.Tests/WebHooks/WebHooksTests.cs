using System;
using System.IO;
using GoCardlessSdk.WebHooks;
using NUnit.Framework;

namespace GoCardlessSdk.Tests.WebHooks
{
    public class WebHooksTests
    {
        [Test]
        public void Bill_InvalidSignature_ThrowsException()
        {
            var request = File.ReadAllText("./WebHooks/Data/Bill invalid signature.txt");
            GoCardless.AccountDetails.AppSecret = "test_secret";
            Assert.Throws<SignatureException>(() => WebHooksClient.ParseRequest(request));
        }

        [Test]
        public void Bill_PayloadDeserializesOk()
        {
            var request = File.ReadAllText("./WebHooks/Data/Bill.txt");
            GoCardless.AccountDetails.AppSecret = "test_secret";

            var payload = WebHooksClient.ParseRequest(request);

            Assert.AreEqual("bill", payload.ResourceType);
            Assert.AreEqual("paid", payload.Action);

            Assert.AreEqual(2, payload.Bills.Length);
            Assert.IsNull(payload.PreAuthorizations);
            Assert.IsNull(payload.Subscriptions);

            Assert.AreEqual("AKJ398H8KA", payload.Bills[0].Id);
            Assert.AreEqual("paid", payload.Bills[0].Status);
            Assert.AreEqual("subscription", payload.Bills[0].SourceType);
            Assert.AreEqual("KKJ398H8K8", payload.Bills[0].SourceId);
            Assert.AreEqual(new DateTimeOffset(new DateTime(2011, 12, 01, 12, 01, 23)), payload.Bills[0].PaidAt);
            Assert.AreEqual("https://sandbox.gocardless.com/api/v1/bills/AKJ398H8KA", payload.Bills[0].Uri);

            Assert.AreEqual("AKJ398H8KB", payload.Bills[1].Id);
            Assert.AreEqual("paid", payload.Bills[1].Status);
            Assert.AreEqual("subscription", payload.Bills[1].SourceType);
            Assert.AreEqual("8AKJ398H78", payload.Bills[1].SourceId);
            Assert.AreEqual(new DateTimeOffset(new DateTime(2011, 12, 09, 23, 04, 56)), payload.Bills[1].PaidAt);
            Assert.AreEqual("https://sandbox.gocardless.com/api/v1/bills/AKJ398H8KB", payload.Bills[1].Uri); 
        }

        [Test]
        public void PreAuthorization_PayloadDeserializesOk()
        {
            var request = File.ReadAllText("./WebHooks/Data/PreAuthorization.txt");
            GoCardless.AccountDetails.AppSecret = "test_secret";
            
            var payload = WebHooksClient.ParseRequest(request);

            Assert.AreEqual("pre_authorization", payload.ResourceType);
            Assert.AreEqual("cancelled", payload.Action);

            Assert.IsNull(payload.Bills);
            Assert.AreEqual(2, payload.PreAuthorizations.Length);
            Assert.IsNull(payload.Subscriptions);

            Assert.AreEqual("AKJ398H8KBOOO3", payload.PreAuthorizations[0].Id);
            Assert.AreEqual("cancelled", payload.PreAuthorizations[0].Status);
            Assert.AreEqual("https://sandbox.gocardless.com/api/v1/pre_authorizations/AKJ398H8KBOOO3", payload.PreAuthorizations[0].Uri);

            Assert.AreEqual("AKJ398H8KBOOOA", payload.PreAuthorizations[1].Id);
            Assert.AreEqual("cancelled", payload.PreAuthorizations[1].Status);
            Assert.AreEqual("https://sandbox.gocardless.com/api/v1/pre_authorizations/AKJ398H8KBOOOA", payload.PreAuthorizations[1].Uri);
        }

        [Test]
        public void Subscription_PayloadDeserializesOk()
        {
            var request = File.ReadAllText("./WebHooks/Data/Subscription.txt");
            GoCardless.AccountDetails.AppSecret = "test_secret";

            var payload = WebHooksClient.ParseRequest(request);

            Assert.AreEqual("subscription", payload.ResourceType);
            Assert.AreEqual("cancelled", payload.Action);

            Assert.IsNull(payload.Bills);
            Assert.IsNull(payload.PreAuthorizations);
            Assert.AreEqual(2, payload.Subscriptions.Length);

            Assert.AreEqual("AKJ398H8KBO122A", payload.Subscriptions[0].Id);
            Assert.AreEqual("cancelled", payload.Subscriptions[0].Status);
            Assert.AreEqual("https://sandbox.gocardless.com/api/v1/subscriptions/AKJ398H8KBO122A", payload.Subscriptions[0].Uri);

            Assert.AreEqual("BBJ398H8KBO122A", payload.Subscriptions[1].Id);
            Assert.AreEqual("cancelled", payload.Subscriptions[1].Status);
            Assert.AreEqual("https://sandbox.gocardless.com/api/v1/subscriptions/BBJ398H8KBO122A", payload.Subscriptions[1].Uri);
        }
    }
}
