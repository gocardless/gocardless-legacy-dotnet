using System;

namespace GoCardlessSdk.Api
{
    public class PreAuthorizationResponse
    {
        public PreAuthorizationResponse()
        {
            this.SubResourceUris = new SubResourceUrisResponse();
        }

        public class SubResourceUrisResponse
        {
            public string Bills { get; set; }
        }

        public DateTimeOffset CreatedAt { get; set; }

        public string Currency { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? ExpiresAt { get; set; }

        public string Id { get; set; }

        public int IntervalLength { get; set; }


        public string IntervalUnit { get; set; }

        public string MerchantId { get; set; }

        public decimal RemainingAmount { get; set; }

        public string Status { get; set; }

        public DateTimeOffset NextIntervalStart { get; set; }

        public string UserId { get; set; }

        public decimal MaxAmount { get; set; }

        public string Uri { get; set; }

        public SubResourceUrisResponse SubResourceUris { get; set; }
    }
}