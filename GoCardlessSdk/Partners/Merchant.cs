namespace GoCardlessSdk.Partners
{
    public class Merchant
    {
        public string Name { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingTown { get; set; }
        public string BillingCounty { get; set; }
        public string BillingPostcode { get; set; }
        public User User { get; set; }
    }
}