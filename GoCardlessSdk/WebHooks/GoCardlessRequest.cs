using System;

namespace GoCardlessSdk.WebHooks
{
    public class GoCardlessRequest
    {
        public Payload Payload { get; set; }
    }
    public class Payload
    {
        public string ResourceType { get; set; }
        public string Action { get; set; }
        public Bill[] Bills { get; set; }
        public PreAuthorization[] PreAuthorizations { get; set; }
        public Subscription[] Subscriptions { get; set; }
        public string Signature { get; set; }

        public class Bill
        {
            public string Id { get; set; }
            public string Status { get; set; }
            public string SourceType { get; set; }
            public string SourceId { get; set; }
            public DateTimeOffset? PaidAt { get; set; }
            public string Uri { get; set; }
			public string Amount { get; set; }
			public string AmountMinusFees { get; set; }
            public string MerchantId { get; set; }
            public string UserId { get; set; }
        }

        public class PreAuthorization
        {
            public string Id { get; set; }
            public string Status { get; set; }
            public string Uri { get; set; }
        }

        public class Subscription
        {
            public string Id { get; set; }
            public string Status { get; set; }
            public string Uri { get; set; }
        }
    }
}
