using System;

namespace GoCardlessSdk.Connect
{
    /// <summary>
    /// GoCardless - SubscriptionRequest
    /// </summary>
    public class SubscriptionRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionRequest"/> class.
        /// </summary>
        /// <param name="merchantId">The merchant id.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="intervalLength">Length of the interval.</param>
        /// <param name="intervalUnit">The interval unit.</param>
        public SubscriptionRequest(string merchantId, decimal amount, int intervalLength, string intervalUnit)
        {
            Amount = amount;
            MerchantId = merchantId;
            IntervalLength = intervalLength;
            IntervalUnit = intervalUnit;
        }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the merchant id.
        /// </summary>
        /// <value>
        /// The merchant id.
        /// </value>
        public string MerchantId { get; set; }

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
        /// Gets or sets the start at.
        /// </summary>
        /// <value>
        /// The start at.
        /// </value>
        public DateTimeOffset? StartAt { get; set; }

        /// <summary>
        /// Gets or sets the expires at.
        /// </summary>
        /// <value>
        /// The expires at.
        /// </value>
        public DateTimeOffset? ExpiresAt { get; set; }

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
        /// Gets or sets the interval count.
        /// </summary>
        /// <value>
        /// The interval count.
        /// </value>
        public int? IntervalCount { get; set; }

        /// <summary>
        /// Gets or sets the setup fee.
        /// </summary>
        /// <value>
        /// The setup fee.
        /// </value>
        public decimal? SetupFee { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public UserRequest User { get; set; }
    }
}