using System;

namespace GoCardlessSdk.Connect
{
    public class PreAuthorizationRequest
    {
        public PreAuthorizationRequest(string merchantId, decimal maxAmount, int intervalLength, string intervalUnit)
        {
            MaxAmount = maxAmount;
            MerchantId = merchantId;
            IntervalLength = intervalLength;
            IntervalUnit = intervalUnit;
        }

        public decimal MaxAmount { get;  set; }
        public string MerchantId { get;  set; }
        public int IntervalLength { get;  set; }
        public string IntervalUnit { get;  set; }

        public DateTimeOffset? ExpiresAt { get;  set; }
        public string Name{ get;  set; }
        public string Description{ get;  set; }
        public int? IntervalCount { get; set; }

        /// <summary>
        /// false if omitted
        /// </summary>
        public bool? CalendarIntervals { get; set; }

        public UserRequest User { get; set; }
    }
}