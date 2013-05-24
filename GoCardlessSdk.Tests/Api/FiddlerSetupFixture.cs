using System;
using System.IO;
using System.Linq;
using System.Net;
using Fiddler;
using NUnit.Framework;

namespace GoCardlessSdk.Tests.Api
{
    [SetUpFixture]
    public class FiddlerSetupFixture
    {
        [SetUp]
        public void StartFiddlerProxy()
        {
            GoCardless.Environment = GoCardless.Environments.Test;

            FiddlerApplication.Startup(7883, FiddlerCoreStartupFlags.None | FiddlerCoreStartupFlags.DecryptSSL);
            WebRequest.DefaultWebProxy = new WebProxy("localhost", 7883);

            FiddlerApplication.BeforeRequest += BeforeRequest;
        }

        public static void BeforeRequest(Session oS)
        {
            var file = oS.url.Replace('/', '_').Split('?').First();
            var method = oS.HTTPMethodIs("GET") ? "GET"
                             : oS.HTTPMethodIs("POST") ? "POST"
                                   : oS.HTTPMethodIs("PUT") ? "PUT" : null;
            oS.utilCreateResponseAndBypassServer();
            var filePath = "./Api/Data/" + method + " " + file + ".txt";

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            var lines = File.ReadAllLines(filePath);
            oS.oResponse.headers = Parser.ParseResponse(lines.First());
            oS.oResponse.headers.Add("Content-Type", "application/json");

            oS.utilSetResponseBody(String.Join(Environment.NewLine, lines.Skip(2).ToArray()));
        }

        [TearDown]
        public void StopFiddlerProxy()
        {
            FiddlerApplication.BeforeRequest -= BeforeRequest;
            FiddlerApplication.oProxy.Detach();
        }
    }
}