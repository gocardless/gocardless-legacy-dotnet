using System;
using System.Collections.Generic;

namespace GoCardlessSdk.Api
{
    /// <summary>
    /// GoCardless - MerchantResponse
    /// </summary>
    public class MerchantResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MerchantResponse"/> class.
        /// </summary>
        public MerchantResponse()
        {
            SubResourceUris = new SubResourceUrisResponse();
        }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
        public string Uri { get; set; }

        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        /// <value>
        /// The balance.
        /// </value>
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets the pending balance.
        /// </summary>
        /// <value>
        /// The pending balance.
        /// </value>
        public decimal PendingBalance { get; set; }

        /// <summary>
        /// Gets or sets the next payout date.
        /// </summary>
        /// <value>
        /// The next payout date.
        /// </value>
        public DateTimeOffset NextPayoutDate { get; set; }

        /// <summary>
        /// Gets or sets the next payout amount.
        /// </summary>
        /// <value>
        /// The next payout amount.
        /// </value>
        public decimal NextPayoutAmount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [hide variable amount].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [hide variable amount]; otherwise, <c>false</c>.
        /// </value>
        public bool HideVariableAmount { get; set; }

        /// <summary>
        /// Gets or sets the sub resource uris.
        /// </summary>
        /// <value>
        /// The sub resource uris.
        /// </value>
        public SubResourceUrisResponse SubResourceUris { get; set; }

        /// <summary>
        /// Gets or sets the API client.
        /// </summary>
        /// <value>
        /// The API client.
        /// </value>
        internal ApiClient ApiClient { get; set; }

        /// <summary>
        /// Gets the bills.
        /// </summary>
        /// <param name="source_id">The source_id.</param>
        /// <param name="subscription_id">The subscription_id.</param>
        /// <param name="pre_authorization_id">The pre_authorization_id.</param>
        /// <param name="user_id">The user_id.</param>
        /// <param name="before">The before.</param>
        /// <param name="after">The after.</param>
        /// <param name="paid">The paid.</param>
        /// <returns>IEnumerable over BillResponse</returns>
        public IEnumerable<BillResponse> GetBills(string source_id = null, string subscription_id = null, string pre_authorization_id = null, string user_id = null, DateTimeOffset? before = null, DateTimeOffset? after = null, bool? paid = null)
        {
            if (ApiClient == null)
            {
                throw new InvalidOperationException("Merchant has no associated ApiClient - cannot make call");
            }

            return ApiClient.GetMerchantBills(this.Id, source_id, subscription_id, pre_authorization_id, user_id, before, after, paid);
        }

        /// <summary>
        /// Gets the pre authorizations.
        /// </summary>
        /// <param name="merchantId">The merchant id.</param>
        /// <param name="user_id">The user_id.</param>
        /// <param name="before">The before.</param>
        /// <param name="after">The after.</param>
        /// <returns>IEnumerable over PreAuthorizationResponse</returns>
        public IEnumerable<PreAuthorizationResponse> GetPreAuthorizations(string merchantId, string user_id = null, DateTimeOffset? before = null, DateTimeOffset? after = null)
        {
            if (ApiClient == null)
            {
                throw new InvalidOperationException("Merchant has no associated ApiClient - cannot make call");
            }

            return ApiClient.GetMerchantPreAuthorizations(this.Id, user_id, before, after);
        }

        /// <summary>
        /// Gets the subscriptions.
        /// </summary>
        /// <param name="merchantId">The merchant id.</param>
        /// <param name="user_id">The user_id.</param>
        /// <param name="before">The before.</param>
        /// <param name="after">The after.</param>
        /// <returns>IEnumerable over SubscriptionResponse</returns>
        public IEnumerable<SubscriptionResponse> GetSubscriptions(string merchantId, string user_id = null, DateTimeOffset? before = null, DateTimeOffset? after = null)
        {
            if (ApiClient == null)
            {
                throw new InvalidOperationException("Merchant has no associated ApiClient - cannot make call");
            }

            return ApiClient.GetMerchantSubscriptions(this.Id, user_id, before, after);
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="merchantId">The merchant id.</param>
        /// <returns>IEnumerable over UserResponse</returns>
        public IEnumerable<UserResponse> GetUsers(string merchantId)
        {
            if (ApiClient == null)
            {
                throw new InvalidOperationException("Merchant has no associated ApiClient - cannot make call");
            }

            return ApiClient.GetMerchantUsers(this.Id);
        }

        /// <summary>
        /// GoCardless - SubResourceUrisResponse
        /// </summary>
        public class SubResourceUrisResponse
        {
            /// <summary>
            /// Gets or sets the users.
            /// </summary>
            /// <value>
            /// The users.
            /// </value>
            public string Users { get; set; }

            /// <summary>
            /// Gets or sets the bills.
            /// </summary>
            /// <value>
            /// The bills.
            /// </value>
            public string Bills { get; set; }

            /// <summary>
            /// Gets or sets the pre authorizations.
            /// </summary>
            /// <value>
            /// The pre authorizations.
            /// </value>
            public string PreAuthorizations { get; set; }

            /// <summary>
            /// Gets or sets the subscriptions.
            /// </summary>
            /// <value>
            /// The subscriptions.
            /// </value>
            public string Subscriptions { get; set; }
        }
    }
}