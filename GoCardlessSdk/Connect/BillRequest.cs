namespace GoCardlessSdk.Connect
{
    public class BillRequest
    {
        public BillRequest(string merchantId, decimal amount)
        {
            Amount = amount;
            MerchantId = merchantId;
        }

        public decimal Amount { get; set; }
        public string MerchantId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public UserRequest User { get; set; }
    }
}