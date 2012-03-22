using System;
using System.Configuration;

namespace GoCardlessSdk
{
    public class AccountDetails
    {
        private string _appId;
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
            set { _appId = value; }
        }

        private string _appSecret;
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
            set { _appSecret = value; }
        }

        // can be null
        private string _token;
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
            set { _token = value; }
        }
    }
}