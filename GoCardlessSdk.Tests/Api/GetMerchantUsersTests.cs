using System;
using GoCardlessSdk.Api;
using NUnit.Framework;

namespace GoCardlessSdk.Tests.Api
{
    public class GetMerchantUsersTests
    {
        [Test]
        public void CanFetchAndDeserializeCorrectly()
        {
            var expected = new[]
            {
                new UserResponse
                {
                    CreatedAt = DateTimeOffset.Parse("2011-11-18T17:06:15Z"),
                    Email = "customer40@gocardless.com",
                    Id = "JKH8HGKL9H",
                    FirstName = "Frank",
                    LastName = "Smith"
                },
                new UserResponse
                {
                    CreatedAt = DateTimeOffset.Parse("2011-11-19T14:16:15Z"),
                    Email = "customer41@gocardless.com",
                    Id = "JKH8HGKL9I",
                    FirstName = "James",
                    LastName = "Dean"
                }
            };
            DeepAssertHelper.AssertIEnumerableDeepEquality(expected, new ApiClient("asdf").GetMerchantUsers("WOQRUJU9OH2HH1"));
        }

    }
}