using System;

namespace GoCardlessSdk.Api
{
    public class SubscriptionResponse
    {

        public SubscriptionResponse()
        {
            this.SubResourceUris = new SubResourceUrisResponse();
        }

        public decimal Amount { get; set; }
        public int IntervalLength { get; set; }
        public string IntervalUnit { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Currency { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? ExpiresAt { get; set; }
        public DateTimeOffset NextIntervalStart { get; set; }
        public string Id { get; set; }
        public string MerchantId { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public string Uri { get; set; }
        public SubResourceUrisResponse SubResourceUris { get; set; }

        public class SubResourceUrisResponse
        {
            public string Bills { get; set; }
        }
    }
}