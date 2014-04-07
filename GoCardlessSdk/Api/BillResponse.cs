using System;

namespace GoCardlessSdk.Api
{
    public class BillResponse
    {
        public decimal Amount { get; set; }
        public decimal GocardlessFees { get; set; } // "0.44",
        public decimal PartnerFees { get; set; } //"0",
        public string Currency { get; set; } //"GBP",
        public DateTimeOffset CreatedAt { get; set; } // "2011-11-04T21: 41: 25Z",
        public string Description { get; set; } // "Month 2 payment",
        public string Id { get; set; } // "VZUG2SC3PRT5EM",
        public string Name { get; set; } // "Bill 2 for Subscription description",
        public DateTimeOffset? PaidAt { get; set; } //  "2011-11-07T15: 00: 00Z",
        public string Status { get; set; } // "paid",
        public string MerchantId { get; set; } // "WOQRUJU9OH2HH1",
        public string UserId { get; set; } // "FIVWCCVEST6S4D",
        public string SourceType { get; set; } // "subscription",
        public string SourceId { get; set; } // "YH1VEVQHYVB1UT",
        public string Uri { get; set; } // "https://gocardless.com/api/v1/bills/VZUG2SC3PRT5EM"

        public DateTimeOffset? ChargeCustomerAt { get; set; }
    }
}