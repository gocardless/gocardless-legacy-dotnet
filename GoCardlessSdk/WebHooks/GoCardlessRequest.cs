namespace GoCardlessSdk.WebHooks
{
    public class GoCardlessRequest
    {
        public Payload payload { get; set; }

        public class Payload
        {
            public string resource_type { get; set; }
            public string action { get; set; }
            public Bill[] bills { get; set; }
            public PreAuthorization[] pre_authorizations { get; set; }
            public Subscription[] subscriptions { get; set; }
            public string signature { get; set; }

            public class Bill
            {
                public string id { get; set; }
                public string status { get; set; }
                public string source_type { get; set; }
                public string source_id { get; set; }
                public string paid_at { get; set; }
                public string uri { get; set; }
            }

            public class PreAuthorization
            {
                public string id { get; set; }
                public string status { get; set; }
                public string uri { get; set; }
            }

            public class Subscription
            {
                public string id { get; set; }
                public string status { get; set; }
                public string uri { get; set; }
            }
        }
    }
}
