namespace GoCardlessSdk.Connect
{
    /// <summary>
    /// GoCardless - ConfirmResource
    /// </summary>
    public class ConfirmResource
    {
        /// <summary>
        /// Gets or sets the resource URI.
        /// </summary>
        /// <value>
        /// The resource URI.
        /// </value>
        public string ResourceUri { get; set; }

        /// <summary>
        /// Gets or sets the resource id.
        /// </summary>
        /// <value>
        /// The resource id.
        /// </value>
        public string ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the type of the resource.
        /// </summary>
        /// <value>
        /// The type of the resource.
        /// </value>
        public string ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the signature.
        /// </summary>
        /// <value>
        /// The signature.
        /// </value>
        public string Signature { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; set; }
    }
}
