using System;

namespace GoCardlessSdk.Connect
{
    public class SubscriptionRequest
    {
        public decimal Amount { get; set; }
        public string MerchantId { get; set; }
        public int IntervalLength { get; set; }
        public string IntervalUnit { get; set; }

        public DateTimeOffset? StartAt { get; set; }
        public DateTimeOffset? ExpiresAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? IntervalCount { get; set; }

        public UserRequest User { get; set; }
    }
}