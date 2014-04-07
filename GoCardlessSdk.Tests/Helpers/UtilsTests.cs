using System;
using System.Linq;
using GoCardlessSdk.Connect;
using GoCardlessSdk.Helpers;
using GoCardlessSdk.Partners;
using NUnit.Framework;

namespace GoCardlessSdk.Tests.Helpers
{
    public class UtilsTests
    {
        [Test]
        public void NonceTest()
        {
            Assert.AreNotEqual(Utils.GenerateNonce(), Utils.GenerateNonce());
        }

        [Test]
        public void ToUnderscoreCaseTests()
        {
            Assert.AreEqual("name", "name".ToUnderscoreCase());
            Assert.AreEqual("name", "Name".ToUnderscoreCase());
            Assert.AreEqual("name", "NAME".ToUnderscoreCase());
            Assert.AreEqual("first_name", "FirstName".ToUnderscoreCase());
            Assert.AreEqual("date_of_birth", "DateOfBirth".ToUnderscoreCase());
        }

        [Test]
        public void ToHashtableTests()
        {
            var subscription = new SubscriptionRequest("merchant123", 2m, 1, "month")
                                   {
                                       User = new UserRequest
                                                  {
                                                      FirstName = "Tim",
                                                      Email = "blah@timiles.com"
                                                  }
                                   };
            var subscriptionHash = subscription.ToHashParams();
            Assert.AreEqual(2m, subscriptionHash["amount"].Single());
            Assert.AreEqual("merchant123", subscriptionHash["merchant_id"].Single());
            Assert.AreEqual("Tim", subscriptionHash["user[first_name]"].Single());
            Assert.AreEqual("blah@timiles.com", subscriptionHash["user[email]"].Single());

            var manageMerchant = new ManageMerchantRequest
                                   {
                                       Merchant = new Merchant
                                                      {
                                                          Name = "Mike the Merchant",
                                                          BillingAddress1 = "Flat 1",
                                                          BillingAddress2 = "200 High St",
                                                          BillingTown = "Townville",
                                                          BillingCounty = "Countyshire",
                                                          BillingPostcode = "N1 1AB",
                                                          User = new User
                                                                     {
                                                                         FirstName = "Mike",
                                                                         LastName = "Merchant",
                                                                         Email = "mike@merchants.com",
                                                                     }
                                                      }
                                   };
            var manageMerchantHash = manageMerchant.ToHashParams();
            Assert.AreEqual("Mike the Merchant", manageMerchantHash["merchant[name]"].Single());
            Assert.AreEqual("Flat 1", manageMerchantHash["merchant[billing_address1]"].Single());
            Assert.AreEqual("200 High St", manageMerchantHash["merchant[billing_address2]"].Single());
            Assert.AreEqual("Townville", manageMerchantHash["merchant[billing_town]"].Single());
            Assert.AreEqual("Countyshire", manageMerchantHash["merchant[billing_county]"].Single());
            Assert.AreEqual("N1 1AB", manageMerchantHash["merchant[billing_postcode]"].Single());
            Assert.AreEqual("Mike", manageMerchantHash["merchant[user][first_name]"].Single());
            Assert.AreEqual("Merchant", manageMerchantHash["merchant[user][last_name]"].Single());
            Assert.AreEqual("mike@merchants.com", manageMerchantHash["merchant[user][email]"].Single());
        }

        [Test]
        public void PercentEncodeTests()
        {
            Assert.AreEqual("Tim", "Tim".PercentEncode());
            Assert.AreEqual("Tim%20Iles", "Tim Iles".PercentEncode());
            Assert.AreEqual("%21%40%A3%24%25%5E%26%2A%28%29%2B%3D%5B%5D%7B%7D%3A%27%40%22%23%3F%3C%3E%2C%60%20",
                "!@£$%^&*()+=[]{}:'@\"#?<>,` ".PercentEncode());
            const string allValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
            Assert.AreEqual(allValidChars, allValidChars.PercentEncode());
        }

        [Test]
        public void ToQueryStringTests()
        {
            Assert.AreEqual("company_name_toggle=0&name=Bob", new UserRequest {Name = "Bob"}.ToQueryString());

            var subscription = new SubscriptionRequest("merchant123", 2m, 1, "month")
                                   {
                                       StartAt = new DateTimeOffset(new DateTime(2011, 01, 01, 12, 00, 00)),
                                       User = new UserRequest
                                                  {
                                                      Name = "John Smith",
                                                      FirstName = "John",
                                                  }
                                   };
            Assert.AreEqual(
                "amount=2.00&interval_length=1&interval_unit=month&merchant_id=merchant123&start_at=2011-01-01T12%3A00%3A00Z&user%5Bcompany_name_toggle%5D=0&user%5Bfirst_name%5D=John&user%5Bname%5D=John%20Smith",
                subscription.ToQueryString());
        }

        class SignatureTestModel
        {
            public string Foo { get; set; }
            public object[] Example { get; set; }
        }
        [Test]
        public void SignatureTest()
        {
            /* from API docs: (https://gocardless.com/docs/signature_guide#constructing-the-parameter-array)
             * irb(main):002:0> to_query(data)
             *  => "example%5B%5D=1&example%5B%5D=a&foo=bar"
             * irb(main):003:0> secret = '5PUZmVMmukNwiHc7V/TJvFHRQZWZumIpCnfZKrVYGpuAdkCcEfv3LIDSrsJ+xOVH'
             *  => "5PUZmVMmukNwiHc7V/TJvFHRQZWZumIpCnfZKrVYGpuAdkCcEfv3LIDSrsJ+xOVH"
             * irb(main):004:0> signature(data, secret)
             *  => "5a9447aef2ebd0e12d80d80c836858c6f9c13219f615ef5d135da408bcad453d"
             */

            var model = new SignatureTestModel
                            {
                                Foo = "bar",
                                Example = new object[] { 1, 'a' }
                            };
            var hash = model.ToHashParams();
            Assert.AreEqual("example%5B%5D=1&example%5B%5D=a&foo=bar", hash.ToQueryString());

            const string secret = "5PUZmVMmukNwiHc7V/TJvFHRQZWZumIpCnfZKrVYGpuAdkCcEfv3LIDSrsJ+xOVH";
            Assert.AreEqual("5a9447aef2ebd0e12d80d80c836858c6f9c13219f615ef5d135da408bcad453d",
                Utils.GetSignatureForParams(hash, secret));
        }
    }
}
