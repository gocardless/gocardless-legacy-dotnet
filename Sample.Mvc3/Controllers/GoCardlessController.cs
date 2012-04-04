using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using GoCardlessSdk;
using GoCardlessSdk.Api;
using GoCardlessSdk.Connect;
using GoCardlessSdk.WebHooks;

namespace Sample.Mvc3.Controllers
{
    public class GoCardlessController : Controller
    {
        [HttpPost]
        public ContentResult Index()
        {
            var requestContent = new StreamReader(Request.InputStream).ReadToEnd();
            var payload = WebHooksClient.ParseRequest(requestContent);
            // TODO: store request payload.
            
            // respond with status HTTP/1.1 200 OK within 5 seconds.
            // If the API server does not get a 200 OK response within this time,
            // it will retry up to 10 times at ever-increasing time intervals.
            // If you have time-consuming server-side processes that are triggered by a webhook,
            // e.g. email scripts, please consider processing them asynchronously.
            return Content("");
        }

        [HttpGet]
        public ActionResult ConfirmResource()
        {
            var requestContent = Request.QueryString;
            var resource = ConnectClient.ConfirmResource(requestContent);
            
            // TODO: store request payload.
            TempData["payload"] = resource;
            
            return RedirectToAction("Success", "Home");
        }

        [HttpGet]
        public ActionResult CreateMerchantCallback(string code, string state)
        {
            var merchantResponse = GoCardless.Partner.ParseCreateMerchantResponse(Settings.CreateMerchantRedirectUri, code);

            // TODO: store response
            TempData["payload"] = merchantResponse;
            
            // use ApiClient to make calls on behalf of merchant
            var merchantApiClient = new ApiClient(merchantResponse.AccessToken);

            return RedirectToAction("Success", "Home");
        }
        
    }
}
