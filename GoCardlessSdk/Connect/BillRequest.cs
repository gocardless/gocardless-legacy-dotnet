namespace GoCardlessSdk.Connect
{
    /// <summary>
    /// GoCardless - BillRequest
    /// </summary>
    public class BillRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BillRequest"/> class.
        /// </summary>
        /// <param name="merchantId">The merchant id.</param>
        /// <param name="amount">The amount.</param>
        public BillRequest(string merchantId, decimal amount)
        {
            Amount = amount;
            MerchantId = merchantId;
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
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public UserRequest User { get; set; }
    }
}