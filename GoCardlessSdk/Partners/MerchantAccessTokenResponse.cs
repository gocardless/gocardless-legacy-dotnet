namespace GoCardlessSdk.Partners
{
    /// <summary>
    /// GoCardless - MerchantAccessTokenResponse
    /// </summary>
    public class MerchantAccessTokenResponse
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the type of the token.
        /// </summary>
        /// <value>
        /// The type of the token.
        /// </value>
        public string TokenType { get; set; }

        /// <summary>
        /// Gets or sets the scope.
        /// </summary>
        /// <value>
        /// The scope.
        /// </value>
        public string Scope { get; set; }

        /// <summary>
        /// Gets the merchant id.
        /// </summary>
        public string MerchantId
        {
            get
            {
                if (Scope != null && Scope.IndexOf("manage_merchant:") > -1 && Scope.Length > "manage_merchant:".Length)
                {
                    return Scope.Substring("manage_merchant:".Length);
                }

                return null;
            }
        }
    }
}