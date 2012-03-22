using System;
using System.Collections.Generic;
using GoCardlessSdk.Api;
using GoCardlessSdk.Partners;

namespace GoCardlessSdk
{
    public static class GoCardless
    {
        static GoCardless()
        {
            AccountDetails = new AccountDetails();
        }

        public enum Environments
        {
            Production,
            Sandbox,
            Test
        }

        public static readonly Dictionary<Environments, string> BaseUrls =
            new Dictionary<Environments, string>
                {
                    {Environments.Test, "http://gocardless.com"},
                    {Environments.Production, "https://gocardless.com"},
                    {Environments.Sandbox, "https://sandbox.gocardless.com"}
                };

        private static string _baseUrl;

        public static string BaseUrl
        {
            get { return _baseUrl ?? BaseUrls[Environment ?? Environments.Production]; }
            set { _baseUrl = value.Trim(); }
        }

        public static Environments? Environment { get; set; }

        public static AccountDetails AccountDetails { get; set; }

        public static ApiClient Api
        {
            get { return new ApiClient(AccountDetails.Token); }
        }

        public static PartnerClient Partner
        {
            get { return new PartnerClient(AccountDetails.AppId, BaseUrl); }
        }



        private static Func<string> _generateNonce;
        internal static Func<string> GenerateNonce
        {
            get { return _generateNonce ?? (_generateNonce = Utils.GenerateNonce); }
            set { _generateNonce = value; }
        }

        private static Func<DateTimeOffset> _getUtcNow; 
        internal static Func<DateTimeOffset> GetUtcNow
        {
            get { return _getUtcNow ?? (_getUtcNow = () => DateTimeOffset.UtcNow); }
            set { _getUtcNow = value; }
        } 
    }
}