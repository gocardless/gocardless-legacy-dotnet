using System;
using System.Web.Mvc;
using GoCardlessSdk;
using GoCardlessSdk.Connect;
using Sample.Mvc3.Models;

namespace Sample.Mvc3.Controllers
{
    public class HomeController : Controller
    {
        private const string MyMerchantId = "0190G74E3J";

        public ActionResult Index()
        {
            GoCardless.Environment = GoCardless.Environments.Sandbox;

            var model = new LinksModel();
            try
            {
                model.NewSubscriptionUrl = ConnectClient.NewSubscriptionUrl(
                    new SubscriptionRequest
                        {
                            Amount = 10m,
                            Description = "Testing a new monthly subscription",
                            IntervalLength = 1,
                            IntervalUnit = "month",
                            MerchantId = MyMerchantId,
                            User = new UserRequest
                                       {
                                           BillingAddress1 = "Flat 1",
                                           BillingAddress2 = "100 Main St",
                                           BillingTown = "Townville",
                                           BillingCounty = "Countyshire",
                                           BillingPostcode = "AB12 3CD",
                                           Email = "john.smith@example.com",
                                           FirstName = "John",
                                           LastName = "Smith",
                                       }
                        });

                model.NewBillUrl = ConnectClient.NewBillUrl(
                    new BillRequest
                        {
                            Amount = 10m,
                            Description = "Testing a new bill",
                            MerchantId = MyMerchantId,
                            Name = "Test bill",
                            User = new UserRequest
                            {
                                BillingAddress1 = "Flat 1",
                                BillingAddress2 = "100 Main St",
                                BillingTown = "Townville",
                                BillingCounty = "Countyshire",
                                BillingPostcode = "AB12 3CD",
                                Email = "john.smith@example.com",
                                FirstName = "John",
                                LastName = "Smith",
                            }
                        });

                model.NewPreAuthorizationUrl = ConnectClient.NewPreAuthorizationUrl(
                    new PreAuthorizationRequest
                        {
                            MaxAmount = 15m,
                            MerchantId = MyMerchantId,
                            IntervalLength = 1,
                            IntervalUnit = "month",

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
                                BillingPostcode = "AB12 3CD",
                                Email = "john.smith@example.com",
                                FirstName = "John",
                                LastName = "Smith",
                            }
                        });
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
            ViewData.Model = GoCardless.Api.GetMerchant(MyMerchantId);
            return View();
        }

        public ActionResult Bills()
        {
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            ViewData.Model = GoCardless.Api.GetMerchantBills(MyMerchantId);
            return View();
        }

        public ActionResult Subscriptions()
        {
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            ViewData.Model = GoCardless.Api.GetMerchantSubscriptions(MyMerchantId);
            return View();
        }

        public ActionResult PreAuthorizations()
        {
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            ViewData.Model = GoCardless.Api.GetMerchantPreAuthorizations(MyMerchantId);
            return View();
        }

        public ActionResult Users()
        {
            GoCardless.Environment = GoCardless.Environments.Sandbox;
            ViewData.Model = GoCardless.Api.GetMerchantUsers(MyMerchantId);
            return View();
        }

        public ActionResult Cancel()
        {
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}
