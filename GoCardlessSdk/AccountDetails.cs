using System;
using System.Configuration;

namespace GoCardlessSdk
{
    /// <summary>
    /// GoCardless - AccountDetails
    /// </summary>
    public class AccountDetails
    {
        private string _appId;
        private string _appSecret;

        // can be null
        private string _token;

        /// <summary>
        /// Gets or sets the app id.
        /// </summary>
        /// <value>
        /// The app id.
        /// </value>
        public string AppId
        {
            get
            {
                var appId = _appId ?? ConfigurationManager.AppSettings["GoCardlessAppId"];

                if (appId == null)
                {
                    throw new ArgumentException("Please supply your appId");
                }

                return appId;
            }

            set
            {
                _appId = value;
            }
        }

        /// <summary>
        /// Gets or sets the app secret.
        /// </summary>
        /// <value>
        /// The app secret.
        /// </value>
        public string AppSecret
        {
            get
            {
                var appSecret = _appSecret ?? ConfigurationManager.AppSettings["GoCardlessAppSecret"];

                if (appSecret == null)
                {
                    throw new ArgumentException("Please supply your appSecret");
                }

                return appSecret;
            }

            set
            {
                _appSecret = value;
            }
        }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token
        {
            get
            {
                var accessToken = _token ?? ConfigurationManager.AppSettings["GoCardlessAccessToken"];
                if (accessToken == null)
                {
                    throw new ArgumentException("Please supply your access token. You can also find this in the developer panel");
                }

                return accessToken;
            }

            set
            {
                _token = value;
            }
        }
    }
}