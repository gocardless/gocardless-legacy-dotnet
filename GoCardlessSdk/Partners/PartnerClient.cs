using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace GoCardlessSdk.Partners
{
    public class PartnerClient
    {
        private string _appId;
        private string baseUrl;

        public PartnerClient(string appId, string baseUrl)
        {
            _appId = appId;
            this.baseUrl = baseUrl;
        }

        public MerchantAccessTokenResponse HandleCreateMerchantResponse(string redirectUri, string body)
        {
            // deserialize request content. (ensure content type is set to JSON in GoCardless setup)
            var authResponse = JsonConvert.DeserializeObject<AuthorizeResponse>(body);

            var client = new RestSharp.RestClient(baseUrl);
            var tokenUrl = "oauth/access_token?client_id=" + _appId + "&code=" + authResponse.code + "&redirect_uri=" + redirectUri + "&grant_type=authorization_code";
            var restRequest = new RestRequest(tokenUrl);
            restRequest.RequestFormat = DataFormat.Json;
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