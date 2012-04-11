using System;
using System.Web.Mvc;
using GoCardlessSdk;
using GoCardlessSdk.Connect;
using GoCardlessSdk.Partners;
using Sample.Mvc3.Models;

namespace Sample.Mvc3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            GoCardless.Environment = GoCardless.Environments.Sandbox;

            var model = new LinksModel();
            try
            {
                model.NewSubscriptionUrl = GoCardless.Connect.NewSubscriptionUrl(
                    new SubscriptionRequest(Settings.MyMerchantId, 10m, 1, "month")
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
                                           Email = "john.smith14444@example.com",
                                           FirstName = "John",
                                           LastName = "Smith",
                                       }
                        });

                model.NewBillUrl = GoCardless.Connect.NewBillUrl(
                    new BillRequest(Settings.MyMerchantId, 10m)
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
                                           Email = "john.smith345345345@example.com",
                                           FirstName = "John",
                                           LastName = "Smith",
                                       }
                        });

                model.NewPreAuthorizationUrl = GoCardless.Connect.NewPreAuthorizationUrl(
                    new PreAuthorizationRequest(Settings.MyMerchantId, 15m, 1, "month")
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
                                           Email = "john.smith34534545@example.com",
                                           FirstName = "John",
                                           LastName = "Smith",
                                       }
                        });

                model.CreateMerchantUrl = GoCardless.Partner.NewMerchantUrl(
                    Settings.CreateMerchantRedirectUri,
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
                model.Error = ex.Message;
            }
            ViewData.Model = model;
            return View();
        }

        public ActionResult Merchant()
        {
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            ViewData.Model = GoCardless.Api.GetMerchant(Settings.MyMerchantId);
            return View();
        }

        public ActionResult Bills()
        {
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            ViewData.Model = GoCardless.Api.GetMerchantBills(Settings.MyMerchantId);
            return View();
        }

        public ActionResult Subscriptions()
        {
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            ViewData.Model = GoCardless.Api.GetMerchantSubscriptions(Settings.MyMerchantId);
            return View();
        }

        public ActionResult PreAuthorizations()
        {
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            ViewData.Model = GoCardless.Api.GetMerchantPreAuthorizations(Settings.MyMerchantId);
            return View();
        }

        public ActionResult Users()
        {
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            ViewData.Model = GoCardless.Api.GetMerchantUsers(Settings.MyMerchantId);
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
