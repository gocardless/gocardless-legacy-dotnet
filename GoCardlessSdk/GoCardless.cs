using System;
using System.Collections.Generic;
using System.Reflection;
using GoCardlessSdk.Api;
using GoCardlessSdk.Connect;
using GoCardlessSdk.Helpers;
using GoCardlessSdk.Partners;

namespace GoCardlessSdk
{
    /// <summary>
    /// GoCardless - Main Class
    /// </summary>
    public static class GoCardless
    {
        /// <summary>
        /// Set up BaseUrls
        /// </summary>
        public static readonly Dictionary<Environments, string> BaseUrls =
            new Dictionary<Environments, string>
                {
                    { Environments.Production, "https://gocardless.com" },
                    { Environments.Sandbox, "https://sandbox.gocardless.com" },
                    { Environments.Test, "http://gocardless.com" }
                };

        private static Func<string> _generateNonce;
        private static Func<DateTimeOffset> _getUtcNow;
        private static string _baseUrl;

        /// <summary>
        /// Initializes static members of the <see cref="GoCardless"/> class.
        /// </summary>
        static GoCardless()
        {
            AccountDetails = new AccountDetails();
        }

        /// <summary>
        /// Enumerated type listing Environments
        /// </summary>
        public enum Environments
        {
            /// <summary>
            /// Environment - Production
            /// </summary>
            Production,

            /// <summary>
            /// Environment - Sandbox
            /// </summary>
            Sandbox,

            /// <summary>
            /// Environment - Test
            /// </summary>
            Test
        }

        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        /// <value>
        /// The base URL.
        /// </value>
        public static string BaseUrl
        {
            get { return _baseUrl ?? BaseUrls[Environment ?? Environments.Production]; }
            set { _baseUrl = value.Trim(); }
        }

        /// <summary>
        /// Gets or sets the environment.
        /// </summary>
        /// <value>
        /// The environment.
        /// </value>
        public static Environments? Environment { get; set; }

        /// <summary>
        /// Gets or sets the account details.
        /// </summary>
        /// <value>
        /// The account details.
        /// </value>
        public static AccountDetails AccountDetails { get; set; }

        /// <summary>
        /// Gets the API.
        /// </summary>
        public static ApiClient Api
        {
            get { return new ApiClient(AccountDetails.Token); }
        }

        /// <summary>
        /// Gets the connect.
        /// </summary>
        public static ConnectClient Connect
        {
            get { return new ConnectClient(); }
        }

        /// <summary>
        /// Gets the partner.
        /// </summary>
        public static PartnerClient Partner
        {
            get { return new PartnerClient(); }
        }

        /// <summary>
        /// Gets the user agent.
        /// </summary>
        public static string UserAgent
        {
            get
            {
                return GetUserAgent();
            }
        }

        /// <summary>
        /// Gets or sets the generate nonce.
        /// </summary>
        /// <value>
        /// The generate nonce.
        /// </value>
        internal static Func<string> GenerateNonce
        {
            get { return _generateNonce ?? (_generateNonce = Utils.GenerateNonce); }
            set { _generateNonce = value; }
        }

        /// <summary>
        /// Gets or sets the get UTC now.
        /// </summary>
        /// <value>
        /// The get UTC now.
        /// </value>
        internal static Func<DateTimeOffset> GetUtcNow
        {
            get { return _getUtcNow ?? (_getUtcNow = () => DateTimeOffset.UtcNow); }
            set { _getUtcNow = value; }
        }

        private static string GetUserAgent()
        {
            try
            {
                return "gocardless-dotnet/v" + GetAssemblyFileVersion();
            }
            catch
            {
                return "gocardless-dotnet";
            }
        }

        private static string GetAssemblyFileVersion()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(GoCardless));
            var attributes = assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)
                    as AssemblyFileVersionAttribute[];

            if (attributes != null && attributes.Length == 1)
            {
                return attributes[0].Version;
            }

            return string.Empty;
        }
    }
}