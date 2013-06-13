using System;

namespace GoCardlessSdk.Api
{
    /// <summary>
    /// GoCardless - PayoutResponse
    /// </summary>
    public class PayoutResponse
    {
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the bank reference.
        /// </summary>
        /// <value>
        /// The bank reference.
        /// </value>
        public string BankReference { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the paid at.
        /// </summary>
        /// <value>
        /// The paid at.
        /// </value>
        public DateTimeOffset PaidAt { get; set; }

        /// <summary>
        /// Gets or sets the transaction fees.
        /// </summary>
        /// <value>
        /// The transaction fees.
        /// </value>
        public decimal TransactionFees { get; set; }
    }
}
