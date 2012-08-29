using System;

namespace GoCardlessSdk.Connect
{
    public class SubscriptionRequest
    {
        public SubscriptionRequest(string merchantId, decimal amount, int intervalLength, string intervalUnit)
        {
            Amount = amount;
            MerchantId = merchantId;
            IntervalLength = intervalLength;
            IntervalUnit = intervalUnit;
        }

        public decimal Amount { get; set; }
        public string MerchantId { get; set; }
        public int IntervalLength { get; set; }
        public string IntervalUnit { get; set; }

        public DateTimeOffset? StartAt { get; set; }
        public DateTimeOffset? ExpiresAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? IntervalCount { get; set; }
        public decimal? SetupFee { get; set; }

        public UserRequest User { get; set; }
    }
}