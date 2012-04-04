using System;

namespace GoCardlessSdk.Partners
{
    public class MerchantAccessTokenResponse
    {
        public string AccessToken {get; set; }
        public string TokenType { get; set; }
        public string Scope { get; set; }

        public string MerchantId
        {
            get
            {
                if (Scope != null && Scope.IndexOf("manage_merchant:") > -1 && Scope.Length > "manage_merchant:".Length)
                    return Scope.Substring("manage_merchant:".Length);
                return null;
            }
        }
    }
}