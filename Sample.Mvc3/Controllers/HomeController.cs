using System;
using System.Configuration;
using System.Web.Mvc;
using GoCardlessSdk;
using GoCardlessSdk.Connect;
using GoCardlessSdk.Partners;

namespace Sample.Mvc3.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _merchantId = ConfigurationManager.AppSettings["GoCardlessMerchantId"];

        public ActionResult Index()
        {
            GoCardless.Environment = GoCardless.Environments.Sandbox;

            try
            {
                ViewBag.NewSubscriptionUrl = GoCardless.Connect.NewSubscriptionUrl(
                    new SubscriptionRequest(_merchantId, 10m, 1, "month")
                        {
                            Name = "Test subscription",
                            Description = "Testing a new monthly subscription",
                            User = new UserRequest
                                       {
                                           BillingAddress1 = "Flat 1",
                                           BillingAddress2 = "100 Main St",
                                           BillingTown = "Townville",
                                           BillingCounty = "Countyshire",
                                           BillingPostcode = "N1 1AB",
                                           Email = "john.smith@example.com",
                                           FirstName = "John",
                                           LastName = "Smith",
                                       }
                        });

                ViewBag.NewBillUrl = GoCardless.Connect.NewBillUrl(
                    new BillRequest(_merchantId, 10m)
                        {
                            Name = "Test bill",
                            Description = "Testing a new bill",
                            User = new UserRequest
                                       {
                                           BillingAddress1 = "Flat 1",
                                           BillingAddress2 = "100 Main St",
                                           BillingTown = "Townville",
                                           BillingCounty = "Countyshire",
                                           BillingPostcode = "N1 1AB",
                                           Email = "john.smith@example.com",
                                           FirstName = "John",
                                           LastName = "Smith",
                                       }
                        });

                ViewBag.NewPreAuthorizationUrl = GoCardless.Connect.NewPreAuthorizationUrl(
                    new PreAuthorizationRequest(_merchantId, 15m, 1, "month")
                        {
                            ExpiresAt = DateTimeOffset.UtcNow.AddYears(1),
                            Name = "Test preauth",
                            Description = "Testing a new preauthorization",
                            IntervalCount = 12,
                            CalendarIntervals = true,
                            User = new UserRequest
                                       {
                                           BillingAddress1 = "Flat 1",
                                           BillingAddress2 = "100 Main St",
                                           BillingTown = "Townville",
                                           BillingCounty = "Countyshire",
                                           BillingPostcode = "N1 1AB",
                                           Email = "john.smith@example.com",
                                           FirstName = "John",
                                           LastName = "Smith",
                                       }
                        });

                ViewBag.CreateMerchantUrl = GoCardless.Partner.NewMerchantUrl(
                    "http://localhost:12345/GoCardless/CreateMerchantCallback",
                    new Merchant
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
                                           Email = "mike.merchant@example.com",
                                       }
                        }
                    );
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }

        public ActionResult Merchant()
        {
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            ViewData.Model = GoCardless.Api.GetMerchant(_merchantId);
            return View();
        }

        public ActionResult Bills()
        {
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            ViewData.Model = GoCardless.Api.GetMerchantBills(_merchantId);
            return View();
        }

        public ActionResult Subscriptions()
        {
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            ViewData.Model = GoCardless.Api.GetMerchantSubscriptions(_merchantId);
            return View();
        }

        public ActionResult PreAuthorizations()
        {
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            ViewData.Model = GoCardless.Api.GetMerchantPreAuthorizations(_merchantId);
            return View();
        }

        public ActionResult Users()
        {
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            ViewData.Model = GoCardless.Api.GetMerchantUsers(_merchantId);
            return View();
        }

        public ActionResult Cancel()
        {
            return View();
        }

        public ActionResult Success()
        {
            ViewData.Model = TempData["payload"];
            return View();
        }
    }
}
