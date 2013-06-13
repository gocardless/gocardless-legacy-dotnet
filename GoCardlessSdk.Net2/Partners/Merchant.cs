namespace GoCardlessSdk.Partners
{
    /// <summary>
    /// GoCardless - Merchant
    /// </summary>
    public class Merchant
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the billing address1.
        /// </summary>
        /// <value>
        /// The billing address1.
        /// </value>
        public string BillingAddress1 { get; set; }

        /// <summary>
        /// Gets or sets the billing address2.
        /// </summary>
        /// <value>
        /// The billing address2.
        /// </value>
        public string BillingAddress2 { get; set; }

        /// <summary>
        /// Gets or sets the billing town.
        /// </summary>
        /// <value>
        /// The billing town.
        /// </value>
        public string BillingTown { get; set; }

        /// <summary>
        /// Gets or sets the billing county.
        /// </summary>
        /// <value>
        /// The billing county.
        /// </value>
        public string BillingCounty { get; set; }

        /// <summary>
        /// Gets or sets the billing postcode.
        /// </summary>
        /// <value>
        /// The billing postcode.
        /// </value>
        public string BillingPostcode { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User { get; set; }
    }
}
