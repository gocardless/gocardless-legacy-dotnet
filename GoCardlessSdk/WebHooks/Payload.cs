using System;

namespace GoCardlessSdk.WebHooks
{
    /// <summary>
    /// GoCardless - Payload
    /// </summary>
    public class Payload
    {
        /// <summary>
        /// Gets or sets the type of the resource.
        /// </summary>
        /// <value>
        /// The type of the resource.
        /// </value>
        public string ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets the bills.
        /// </summary>
        /// <value>
        /// The bills.
        /// </value>
        public Bill[] Bills { get; set; }

        /// <summary>
        /// Gets or sets the pre authorizations.
        /// </summary>
        /// <value>
        /// The pre authorizations.
        /// </value>
        public PreAuthorization[] PreAuthorizations { get; set; }

        /// <summary>
        /// Gets or sets the subscriptions.
        /// </summary>
        /// <value>
        /// The subscriptions.
        /// </value>
        public Subscription[] Subscriptions { get; set; }

        /// <summary>
        /// Gets or sets the signature.
        /// </summary>
        /// <value>
        /// The signature.
        /// </value>
        public string Signature { get; set; }

        /// <summary>
        /// GoCardless - Bill
        /// </summary>
        public class Bill
        {
            /// <summary>
            /// Gets or sets the id.
            /// </summary>
            /// <value>
            /// The id.
            /// </value>
            public string Id { get; set; }

            /// <summary>
            /// Gets or sets the status.
            /// </summary>
            /// <value>
            /// The status.
            /// </value>
            public string Status { get; set; }

            /// <summary>
            /// Gets or sets the type of the source.
            /// </summary>
            /// <value>
            /// The type of the source.
            /// </value>
            public string SourceType { get; set; }

            /// <summary>
            /// Gets or sets the source id.
            /// </summary>
            /// <value>
            /// The source id.
            /// </value>
            public string SourceId { get; set; }

            /// <summary>
            /// Gets or sets the paid at.
            /// </summary>
            /// <value>
            /// The paid at.
            /// </value>
            public DateTimeOffset? PaidAt { get; set; }

            /// <summary>
            /// Gets or sets the URI.
            /// </summary>
            /// <value>
            /// The URI.
            /// </value>
            public string Uri { get; set; }

            /// <summary>
            /// Gets or sets the amount.
            /// </summary>
            /// <value>
            /// The amount.
            /// </value>
            public string Amount { get; set; }

            /// <summary>
            /// Gets or sets the amount minus fees.
            /// </summary>
            /// <value>
            /// The amount minus fees.
            /// </value>
            public string AmountMinusFees { get; set; }

            /// <summary>
            /// Gets or sets the merchant id.
            /// </summary>
            /// <value>
            /// The merchant id.
            /// </value>
            public string MerchantId { get; set; }

            /// <summary>
            /// Gets or sets the user id.
            /// </summary>
            /// <value>
            /// The user id.
            /// </value>
            public string UserId { get; set; }
        }

        /// <summary>
        /// GoCardless - PreAuthorization
        /// </summary>
        public class PreAuthorization
        {
            /// <summary>
            /// Gets or sets the id.
            /// </summary>
            /// <value>
            /// The id.
            /// </value>
            public string Id { get; set; }

            /// <summary>
            /// Gets or sets the status.
            /// </summary>
            /// <value>
            /// The status.
            /// </value>
            public string Status { get; set; }

            /// <summary>
            /// Gets or sets the URI.
            /// </summary>
            /// <value>
            /// The URI.
            /// </value>
            public string Uri { get; set; }
        }

        /// <summary>
        /// GoCardless - Subscription
        /// </summary>
        public class Subscription
        {
            /// <summary>
            /// Gets or sets the id.
            /// </summary>
            /// <value>
            /// The id.
            /// </value>
            public string Id { get; set; }

            /// <summary>
            /// Gets or sets the status.
            /// </summary>
            /// <value>
            /// The status.
            /// </value>
            public string Status { get; set; }

            /// <summary>
            /// Gets or sets the URI.
            /// </summary>
            /// <value>
            /// The URI.
            /// </value>
            public string Uri { get; set; }
        }
    }
}
