using System;
using System.IO;
using GoCardlessSdk.WebHooks;
using NUnit.Framework;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

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
        public void testFlatteningArray()
        {
            var tuples = flatten("{ cars: ['BMW', 'Fiat', 'VW'] }");
            Assert.AreEqual("cars[]", tuples[0].Key);
            Assert.AreEqual("BMW", tuples[0].Value);
        }

        [Test]
        public void testRootHashElementsAreNotEnclosed()
        {
            var tuples = flatten("{foo: 'bar', bar: 'foo'}");
            Assert.AreEqual("foo", tuples[0].Key);
            Assert.AreEqual("bar", tuples[1].Key);
        }

        [Test]
        public void testFlatteningNestedDictionary()
        {
            var tuples = flatten("{ user: { name: 'Fred', age: 30 } }");
            Assert.AreEqual("user[name]", tuples[0].Key);
            Assert.AreEqual("Fred", tuples[0].Value);
            Assert.AreEqual("user[age]", tuples[1].Key);
            Assert.AreEqual("30", tuples[1].Value);
        }

        [Test]
        public void testCombination()
        {
            var tuples = flatten("{ user: { name: 'Fred', cars: ['BMW', 'Fiat'] } }");
            Assert.AreEqual("user[name]", tuples[0].Key);
            Assert.AreEqual("Fred", tuples[0].Value);
            Assert.AreEqual("user[cars][]", tuples[1].Key);
            Assert.AreEqual("BMW", tuples[1].Value);
        }

        [Test]
        public void testFlatteningHashWithinArray()
        {
            var tuples = flatten("{ bills: [ {id: 'AKJ398H8KA'} ] }");
            Assert.AreEqual(1, tuples.Count);
            Assert.AreEqual("bills[][id]", tuples[0].Key);
            Assert.AreEqual("AKJ398H8KA", tuples[0].Value);
        }

        [Test]
        public void testFlatteningArrayWithinHash()
        {
            var tuples = flatten("{ bills: { id: ['AKJ398H8KA'] } }");
            Assert.AreEqual(1, tuples.Count);
            Assert.AreEqual("bills[id][]", tuples[0].Key);
            Assert.AreEqual("AKJ398H8KA", tuples[0].Value);        
        }

        [Test]
        public void testDatetimesFormattedToZuluTime()
        {
            var tuples = flatten("{ time: '2011-12-01T12:01:23Z'}");
            Assert.AreEqual("2011-12-01T12:01:23Z", tuples[0].Value);
        }

        [Test]
        public void testDecimalFormattingPreserved()
        {
            var tuples = flatten("{ amount: '80.0'}");
            Assert.AreEqual("80.0", tuples[0].Value);
        }

        [Test]
        public void testEncoding()
        {
            var tuple = new StringTuple("user[email]", "fred@example.com");
            var result = new SignatureValidator().PercentEncode(tuple);
            Assert.AreEqual("user%5Bemail%5D", result.Key);
            Assert.AreEqual("fred%40example.com", result.Value);
        }

        private List<StringTuple> flatten(string json)
        {
            return new SignatureValidator().Flatten(null, JToken.Parse(json));
        }

        [Test]
        public void Bill_PayloadDeserializesOk()
        {
            var request = File.ReadAllText("./WebHooks/Data/Bill.txt");
            GoCardless.AccountDetails.AppSecret = "E9XTYW9EJV2P4FRTDTRCTREH1PSADHBWFSM7D6W6PJ3W2S8FF6G9K25PEPXXRF9N";

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
            Assert.AreEqual("040NF42M1F", payload.Bills[0].MerchantId);
            Assert.AreEqual("0AR5NV7TYC", payload.Bills[0].UserId);

            Assert.AreEqual("AKJ398H8KB", payload.Bills[1].Id);
            Assert.AreEqual("paid", payload.Bills[1].Status);
            Assert.AreEqual("subscription", payload.Bills[1].SourceType);
            Assert.AreEqual("8AKJ398H78", payload.Bills[1].SourceId);
            Assert.AreEqual(new DateTimeOffset(new DateTime(2011, 12, 09, 23, 04, 56)), payload.Bills[1].PaidAt);
            Assert.AreEqual("https://sandbox.gocardless.com/api/v1/bills/AKJ398H8KB", payload.Bills[1].Uri);
            Assert.AreEqual("040NF42M1F", payload.Bills[1].MerchantId);
            Assert.AreEqual("0AR5NV7TYC", payload.Bills[1].UserId);

        }

		/// <summary>
		/// Exposes a bug where webhooks without a 'PaidAt' attribute were failing validation.
		/// </summary>
		[Test]
		public void Bill_WithPaidAtValidates ()
		{
			var request = File.ReadAllText("./WebHooks/Data/BillCreated.txt");
			GoCardless.AccountDetails.AppSecret = "E9XTYW9EJV2P4FRTDTRCTREH1PSADHBWFSM7D6W6PJ3W2S8FF6G9K25PEPXXRF9N";

			var payload = WebHooksClient.ParseRequest(request);
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
