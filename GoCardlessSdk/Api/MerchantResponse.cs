using System;
using System.Collections.Generic;

namespace GoCardlessSdk.Api
{
    public class MerchantResponse
    {
        public MerchantResponse()
        {
            SubResourceUris = new SubResourceUrisResponse();
        }
        public DateTimeOffset CreatedAt { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Uri { get; set; }
        public decimal Balance { get; set; }
        public decimal PendingBalance { get; set; }
        public DateTimeOffset NextPayoutDate { get; set; }
        public decimal NextPayoutAmount { get; set; }
        public bool HideVariableAmount { get; set; }
        public SubResourceUrisResponse SubResourceUris { get; set; }

        internal ApiClient ApiClient { get; set; }

        public class SubResourceUrisResponse
        {
            public string Users { get; set; }
            public string Bills { get; set; }
            public string PreAuthorizations { get; set; }
            public string Subscriptions { get; set; }
        }

        public IEnumerable<BillResponse> GetBills(string source_id = null, string subscription_id = null, string pre_authorization_id = null, string user_id = null, DateTimeOffset? before = null, DateTimeOffset? after = null, bool? paid = null)
        {
            if (ApiClient == null) throw new InvalidOperationException("Merchant has no associated ApiClient - cannot make call");
            return ApiClient.GetMerchantBills(this.Id, source_id, subscription_id, pre_authorization_id, user_id, before, after, paid);
        }

        public IEnumerable<PreAuthorizationResponse> GetPreAuthorizations(string merchantId, string user_id = null, DateTimeOffset? before = null, DateTimeOffset? after = null)
        {
            if (ApiClient == null) throw new InvalidOperationException("Merchant has no associated ApiClient - cannot make call");
            return ApiClient.GetMerchantPreAuthorizations(this.Id, user_id, before, after);
        }
        public IEnumerable<SubscriptionResponse> GetSubscriptions(string merchantId, string user_id = null, DateTimeOffset? before = null, DateTimeOffset? after = null)
        {
            if (ApiClient == null) throw new InvalidOperationException("Merchant has no associated ApiClient - cannot make call");
            return ApiClient.GetMerchantSubscriptions(this.Id, user_id, before, after);
        }
        public IEnumerable<UserResponse> GetUsers(string merchantId)
        {
            if (ApiClient == null) throw new InvalidOperationException("Merchant has no associated ApiClient - cannot make call");
            return ApiClient.GetMerchantUsers(this.Id);
        }
    }
}