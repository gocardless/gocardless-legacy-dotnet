using System;

namespace GoCardlessSdk.Api
{
    /// <summary>
    /// GoCardless - PreAUthorizationResponse
    /// </summary>
    public class PreAuthorizationResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreAuthorizationResponse"/> class.
        /// </summary>
        public PreAuthorizationResponse()
        {
            this.SubResourceUris = new SubResourceUrisResponse();
        }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the expires at.
        /// </summary>
        /// <value>
        /// The expires at.
        /// </value>
        public DateTimeOffset? ExpiresAt { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the length of the interval.
        /// </summary>
        /// <value>
        /// The length of the interval.
        /// </value>
        public int IntervalLength { get; set; }

        /// <summary>
        /// Gets or sets the interval unit.
        /// </summary>
        /// <value>
        /// The interval unit.
        /// </value>
        public string IntervalUnit { get; set; }

        /// <summary>
        /// Gets or sets the merchant id.
        /// </summary>
        /// <value>
        /// The merchant id.
        /// </value>
        public string MerchantId { get; set; }

        /// <summary>
        /// Gets or sets the remaining amount.
        /// </summary>
        /// <value>
        /// The remaining amount.
        /// </value>
        public decimal RemainingAmount { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the next interval start.
        /// </summary>
        /// <value>
        /// The next interval start.
        /// </value>
        public DateTimeOffset NextIntervalStart { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the max amount.
        /// </summary>
        /// <value>
        /// The max amount.
        /// </value>
        public decimal MaxAmount { get; set; }

        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
        public string Uri { get; set; }

        /// <summary>
        /// Gets or sets the sub resource uris.
        /// </summary>
        /// <value>
        /// The sub resource uris.
        /// </value>
        public SubResourceUrisResponse SubResourceUris { get; set; }

        /// <summary>
        /// GoCardless - SubResourceUrisResponse
        /// </summary>
        public class SubResourceUrisResponse
        {
            /// <summary>
            /// Gets or sets the bills.
            /// </summary>
            /// <value>
            /// The bills.
            /// </value>
            public string Bills { get; set; }
        }
    }
}