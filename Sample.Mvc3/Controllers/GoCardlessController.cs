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
        static readonly List<ConfirmResource> Responses = new List<ConfirmResource>();

        [HttpGet]
        public ActionResult Show()
        {
            ViewData.Model = Responses;
            return View();
        }

        [HttpPost]
        public ContentResult Index()
        {
            var requestContent = new StreamReader(Request.InputStream).ReadToEnd();
            var payload = WebHooksClient.ParseRequest(requestContent);
            // TODO: store request payload.
            //_responses.Add(payload);
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
            TempData["resource"] = resource;
            
            return RedirectToAction("Success", "Home");
        }

        [HttpGet]
        public void ReturnFromCreateMerchant()
        {
            var requestContent = new StreamReader(Request.InputStream).ReadToEnd();
            var merchantResponse = GoCardless.Partner.HandleCreateMerchantResponse(Request.Url.ToString(), requestContent);

            var merchantId = merchantResponse.MerchantId; //at this point you should save the merchant Id and access token...
            var accessToken = merchantResponse.access_token; //...so that you can make calls on behalf of the merchant later...
            var merchantApiClient = new ApiClient(accessToken); //...by initialising an ApiClient
        }

    }
}
