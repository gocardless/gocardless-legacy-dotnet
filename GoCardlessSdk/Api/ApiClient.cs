using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using GoCardlessSdk.Api.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace GoCardlessSdk.Api
{
    public class ApiClient
    {
        public static readonly string ApiPath = "/api/v1";
        public static string ApiUrl
        {
            get { return GoCardlessSdk.GoCardless.BaseUrl + ApiPath; }
        }

        private readonly string _accessToken;

        public ApiClient(string accessToken)
        {
            _accessToken = accessToken;
        }

        public MerchantResponse GetMerchant(string id)
        {
            var restRequest = GetRestRequest("merchants/" + id, Method.GET);
            var merchant = Execute<MerchantResponse>(restRequest);
            merchant.ApiClient = this;
            return merchant;
        }

        public BillResponse GetBill(string id)
        {
            var restRequest = GetRestRequest("bills/" + id, Method.GET);
            return Execute<BillResponse>(restRequest);
        }

        public IEnumerable<BillResponse> GetMerchantBills(string merchantId, string source_id = null, string subscription_id = null, string pre_authorization_id = null, string user_id = null, DateTimeOffset? before = null, DateTimeOffset? after = null, bool? paid = null)
        {
            var options = new {source_id, subscription_id, pre_authorization_id, user_id, before, after, paid};
            var restRequest = GetRestRequest("merchants/" + merchantId + "/bills", Method.GET, options);
            return Execute<List<BillResponse>>(restRequest).AsReadOnly();
        }
        
        public IEnumerable<PreAuthorizationResponse> GetMerchantPreAuthorizations(string merchantId, string user_id = null, DateTimeOffset? before = null, DateTimeOffset? after = null)
        {
            var options = new {user_id, before, after};
            var restRequest = GetRestRequest("merchants/" + merchantId + "/pre_authorizations", Method.GET, options);
            return Execute<List<PreAuthorizationResponse>>(restRequest).AsReadOnly();
        }
        public IEnumerable<SubscriptionResponse> GetMerchantSubscriptions(string merchantId, string user_id = null, DateTimeOffset? before = null, DateTimeOffset? after = null)
        {
            var options = new { user_id, before, after };
            var restRequest = GetRestRequest("merchants/" + merchantId + "/subscriptions", Method.GET, options);
            return Execute<List<SubscriptionResponse>>(restRequest).AsReadOnly();
        }
        public IEnumerable<UserResponse> GetMerchantUsers(string merchantId)
        {
            var restRequest = GetRestRequest("merchants/" + merchantId + "/users", Method.GET);
            return Execute<List<UserResponse>>(restRequest).AsReadOnly();
        }
        public SubscriptionResponse GetSubscription(string id)
        {
            var restRequest = GetRestRequest("subscriptions/" + id, Method.GET);
            return Execute<SubscriptionResponse>(restRequest);
        }
        public SubscriptionResponse CancelSubscription(string id)
        {
            var restRequest = GetRestRequest("subscriptions/" + id + "/cancel", Method.PUT);
            return Execute<SubscriptionResponse>(restRequest);
        }
        public PreAuthorizationResponse GetPreAuthorization(string id)
        {
            var restRequest = GetRestRequest("pre_authorizations/" + id, Method.GET);
            return Execute<PreAuthorizationResponse>(restRequest);
        }
        public PreAuthorizationResponse CancelPreAuthorization(string id)
        {
            var restRequest = GetRestRequest("pre_authorizations/" + id + "/cancel", Method.PUT);
            return Execute<PreAuthorizationResponse>(restRequest);
        }


        private static RestRequest GetRestRequest(string resource, Method method, object options = null)
        {
            var request = new RestRequest(resource, method) {RequestFormat = DataFormat.Json};
            if (options != null)
            {
                foreach (var arg in ToHash(options).Where(arg => arg.Value != null))
                {
                    var value = arg.Value is DateTimeOffset ? ((DateTimeOffset)arg.Value).ToString("r") : arg.Value.ToString();
                    request.AddParameter(arg.Key, value, ParameterType.GetOrPost);
                }
            }
            return request;
        }

        private static Dictionary<string, object> ToHash(object obj)
        {
            return obj.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(obj, null));
        }

        public T Execute<T>(RestRequest request, HttpStatusCode expected = HttpStatusCode.OK) where T : new()
        {
            var client = new RestClient
            {
                BaseUrl = ApiUrl,
            };
            var serializer = new Newtonsoft.Json.JsonSerializer
            {
                ContractResolver = new UnderscoreToCamelCasePropertyResolver(),
            };
            client.AddHandler("application/json", new NewtonsoftJsonDeserializer(serializer));
            request.AddHeader("Authorization", "bearer " + _accessToken); // used on every request
            var response = client.Execute<T>(request);
            if (response.StatusCode != expected)
            {
                var ex = new ApiException("Expected response " + (int) expected + " " + expected + " but received " +
                                 (int) response.StatusCode + " " + response.StatusCode +
                                 ". See Content for more details");
                try
                {
                    ex.Content = JObject.Parse(response.Content);
                }catch(Exception)
                {
                }
                ex.RawContent = response.Content;
                throw ex;
            }
            return response.Data;
        }

        public BillResponse PostBill(decimal amount, string preAuthorizationId, string name = null, string description = null)
        {
          
            var restRequest = GetRestRequest("bills", Method.POST);
            restRequest.AddBody(new {bill = new
            {
                amount, pre_authorization_id = preAuthorizationId, name, description
            }});
            return Execute<BillResponse>(restRequest, HttpStatusCode.Created);
        }
    }
}