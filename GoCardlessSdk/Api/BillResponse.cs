using System;

namespace GoCardlessSdk.Api
{
    /// <summary>
    /// GoCardless - BillResponse
    /// </summary>
    public class BillResponse
    {
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the gocardless fees.
        /// </summary>
        /// <value>
        /// The gocardless fees.
        /// </value>
        public decimal GocardlessFees { get; set; } // "0.44",

        /// <summary>
        /// Gets or sets the partner fees.
        /// </summary>
        /// <value>
        /// The partner fees.
        /// </value>
        public decimal PartnerFees { get; set; } // "0",

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; } // "GBP",

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTimeOffset CreatedAt { get; set; } // "2011-11-04T21: 41: 25Z",

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; } // "Month 2 payment",

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public string Id { get; set; } // "VZUG2SC3PRT5EM",

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; } // "Bill 2 for Subscription description",

        /// <summary>
        /// Gets or sets the paid at.
        /// </summary>
        /// <value>
        /// The paid at.
        /// </value>
        public DateTimeOffset? PaidAt { get; set; } // "2011-11-07T15: 00: 00Z",

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; } // "paid",

        /// <summary>
        /// Gets or sets the merchant id.
        /// </summary>
        /// <value>
        /// The merchant id.
        /// </value>
        public string MerchantId { get; set; } // "WOQRUJU9OH2HH1",

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        public string UserId { get; set; } // "FIVWCCVEST6S4D",

        /// <summary>
        /// Gets or sets the type of the source.
        /// </summary>
        /// <value>
        /// The type of the source.
        /// </value>
        public string SourceType { get; set; } // "subscription",

        /// <summary>
        /// Gets or sets the source id.
        /// </summary>
        /// <value>
        /// The source id.
        /// </value>
        public string SourceId { get; set; } // "YH1VEVQHYVB1UT",

        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
        public string Uri { get; set; } // "https://gocardless.com/api/v1/bills/VZUG2SC3PRT5EM"
    }
}