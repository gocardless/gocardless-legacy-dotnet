namespace GoCardlessSdk.Connect
{
    public class UserRequest
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingTown { get; set; }
        public string BillingCounty { get; set; }
        public string BillingPostcode { get; set; }

        public string CompanyName { get; set; }
        public bool CompanyNameToggle { get; set; }
    }
}
