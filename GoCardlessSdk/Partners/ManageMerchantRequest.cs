namespace GoCardlessSdk.Partners
{
    public class ManageMerchantRequest
    {
        public string ClientId { get; set; }
        public string RedirectUri { get; set; }
        public string ResponseType { get; set; }
        public string Scope { get; set; }

        public Merchant Merchant { get; set; }
        public string State { get; set; }
    }
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
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
