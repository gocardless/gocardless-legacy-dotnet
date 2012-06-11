using System.Net;
using GoCardlessSdk.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace GoCardlessSdk.Partners
{
    public class PartnerClient
    {
        public string NewMerchantUrl(string redirectUri, Merchant merchant = null, string state = null)
        {
            var request = new ManageMerchantRequest
                           {
                               ClientId = GoCardless.AccountDetails.AppId,
                               RedirectUri = redirectUri,
                               ResponseType = "code",
                               Scope = "manage_merchant",
                               Merchant = merchant,
                               State = state
                           };

            return GoCardless.BaseUrl + "/oauth/authorize?" + request.ToQueryString();
        }

        public MerchantAccessTokenResponse ParseCreateMerchantResponse(string redirectUri, string code)
        {
            var hash = new Utils.HashParams
                           {
                               {"client_id", GoCardless.AccountDetails.AppId},
                               {"redirect_uri", redirectUri},
                               {"code", code},
                               {"grant_type", "authorization_code"},
                           };
            var tokenUrl = "oauth/access_token?" + hash.ToQueryString();

            var client = new RestClient
                             {
                                 BaseUrl = GoCardless.BaseUrl,
                                 UserAgent = GoCardless.UserAgent
                             };
            client.Authenticator = new HttpBasicAuthenticator(GoCardless.AccountDetails.AppId, GoCardless.AccountDetails.AppSecret);

            var restRequest = new RestRequest(tokenUrl);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.Method = Method.POST;

            var response = client.Execute<MerchantAccessTokenResponse>(restRequest);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                throw new ApiException("Invalid response getting access token from " + tokenUrl)
                {
                    Content = JObject.Parse(response.Content)
                };
            }
        }
    }
}