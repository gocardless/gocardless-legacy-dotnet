using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoCardlessSdk.Api
{
    public class PayoutResponse
    {
        public decimal Amount { get; set; }
        public string BankReference { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Id { get; set; }
        public DateTimeOffset PaidAt { get; set; }
        public decimal TransactionFees { get; set; }
    }
}
