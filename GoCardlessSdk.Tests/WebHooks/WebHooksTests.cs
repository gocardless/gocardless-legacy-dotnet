using System.IO;
using GoCardlessSdk.WebHooks;
using NUnit.Framework;

namespace GoCardlessSdk.Tests.WebHooks
{
    public class WebHooksTests
    {
        [Test]
        public void TestInvalidSignature()
        {
            var request = File.ReadAllText("./Webhooks/Data/test invalid signature.txt");
            GoCardless.AccountDetails.AppSecret = "8ifu76Qi4HMJC1zSNf93WntQzJKpSmce0SwBNTA5HEqQY61aBTH7Nsx4w_HG1vUL";
            Assert.Throws<SignatureException>(() => WebHooksClient.ParseRequest(request));
        }

        [Test]
        public void TestValidSignature()
        {
            var request = File.ReadAllText("./Webhooks/Data/test valid signature.txt");
            GoCardless.AccountDetails.AppSecret = "8ifu76Qi4HMJC1zSNf93WntQzJKpSmce0SwBNTA5HEqQY61aBTH7Nsx4w_HG1vUL";
            Assert.DoesNotThrow(() => WebHooksClient.ParseRequest(request));
        }

    }
}
