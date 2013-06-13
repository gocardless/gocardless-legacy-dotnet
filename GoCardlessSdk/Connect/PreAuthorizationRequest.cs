using System;

namespace GoCardlessSdk.Connect
{
    /// <summary>
    /// GoCardless - PreAuthorizationRequest
    /// </summary>
    public class PreAuthorizationRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreAuthorizationRequest"/> class.
        /// </summary>
        /// <param name="merchantId">The merchant id.</param>
        /// <param name="maxAmount">The max amount.</param>
        /// <param name="intervalLength">Length of the interval.</param>
        /// <param name="intervalUnit">The interval unit.</param>
        public PreAuthorizationRequest(string merchantId, decimal maxAmount, int intervalLength, string intervalUnit)
        {
            MaxAmount = maxAmount;
            MerchantId = merchantId;
            IntervalLength = intervalLength;
            IntervalUnit = intervalUnit;
        }

        /// <summary>
        /// Gets or sets the max amount.
        /// </summary>
        /// <value>
        /// The max amount.
        /// </value>
        public decimal MaxAmount { get; set; }

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
        /// Gets or sets the calendar intervals. False if omitted.
        /// </summary>
        /// <value>
        /// The calendar intervals.
        /// </value>
        public bool? CalendarIntervals { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public UserRequest User { get; set; }
    }
}