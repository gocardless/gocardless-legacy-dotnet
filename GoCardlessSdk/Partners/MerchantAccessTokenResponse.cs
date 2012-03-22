using System;

namespace GoCardlessSdk.Partners
{
    public class MerchantAccessTokenResponse
    {
        public string access_token {get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }

        public string MerchantId
        {
            get
            {
                if (scope != null && scope.IndexOf("manage_merchant:", StringComparison.InvariantCulture) > -1 && scope.Length > "manage_merchant:".Length)
                    return scope.Substring("manage_merchant:".Length);
                return null;
            }
        }
    }
}