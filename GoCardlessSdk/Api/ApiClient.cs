using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using GoCardlessSdk.Helpers;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serializers;
using System.Globalization;

namespace GoCardlessSdk.Api {
    public class HttpRequestEventArgs : EventArgs {
        public Uri RequestUri { get; set; }
        public string Method { get; set; }
    }
    public class ApiClient {
        public event EventHandler<HttpRequestEventArgs> HttpRequestSending;

        public static readonly string ApiPath = "/api/v1";
        public static string ApiUrl {
            get { return GoCardless.BaseUrl + ApiPath; }
        }

        private readonly string _accessToken;

        public ApiClient(string accessToken) {
            _accessToken = accessToken;
        }

        public MerchantResponse GetMerchant(string id) {
            var restRequest = GetRestRequest("merchants/" + id, Method.GET);
            var merchant = Execute<MerchantResponse>(restRequest);
            merchant.ApiClient = this;
            return merchant;
        }

        public BillResponse GetBill(string id) {
            var restRequest = GetRestRequest("bills/" + id, Method.GET);
            return Execute<BillResponse>(restRequest);
        }

        public PayoutResponse GetPayout(string id) {
            var restRequest = GetRestRequest("payouts/" + id, Method.GET);
            return Execute<PayoutResponse>(restRequest);
        }

        const int per_page = 100;

        public IEnumerable<BillResponse> GetMerchantBills(string merchantId, string sourceId = null, string subscriptionId = null, string preAuthorizationId = null, string userId = null, DateTimeOffset? before = null, DateTimeOffset? after = null, bool? paid = null) {
            var page = 0;
            while (true) {
                var options = new { source_id = sourceId, subscription_id = subscriptionId, pre_authorization_id = preAuthorizationId, user_id = userId, before, after, paid, per_page, page };
                var restRequest = GetRestRequest("merchants/" + merchantId + "/bills", Method.GET, options);
                var list = Execute<List<BillResponse>>(restRequest).AsReadOnly();
                if (!list.Any()) yield break;
                foreach (var item in list) yield return (item);
                page++;
            }

        }

        public IEnumerable<PreAuthorizationResponse> GetMerchantPreAuthorizations(string merchantId, string userId = null, DateTimeOffset? before = null, DateTimeOffset? after = null) {
            var page = 0;
            while (true) {
                var options = new { user_id = userId, before, after, per_page, page };
                var restRequest = GetRestRequest("merchants/" + merchantId + "/pre_authorizations", Method.GET, options);
                var list = Execute<List<PreAuthorizationResponse>>(restRequest).AsReadOnly();
                if (!list.Any()) yield break;
                foreach (var item in list) yield return (item);
                page++;
            }
        }
        public IEnumerable<SubscriptionResponse> GetMerchantSubscriptions(string merchantId, string userId = null, DateTimeOffset? before = null, DateTimeOffset? after = null) {
            var page = 0;
            while (true) {
                var options = new { user_id = userId, before, after, per_page, page };
                var restRequest = GetRestRequest("merchants/" + merchantId + "/subscriptions", Method.GET, options);
                var list = Execute<List<SubscriptionResponse>>(restRequest).AsReadOnly();
                if (!list.Any()) yield break;
                foreach (var item in list) yield return item;
                page++;
            }
        }

        public IEnumerable<UserResponse> GetMerchantUsers(string merchantId) {
            var page = 0;
            while (true) {
                var options = new { per_page, page };
                var restRequest = GetRestRequest("merchants/" + merchantId + "/users", Method.GET, options);
                var list = Execute<List<UserResponse>>(restRequest).AsReadOnly();
                if (!list.Any()) yield break;
                foreach (var item in list) yield return (item);
                page++;
            }
        }

        public IEnumerable<PayoutResponse> GetMerchantPayouts(string merchantId) {
            var page = 0;
            while (true) {
                var options = new { per_page, page };
                var restRequest = GetRestRequest("merchants/" + merchantId + "/payouts", Method.GET, options);
                var list = Execute<List<PayoutResponse>>(restRequest).AsReadOnly();
                if (!list.Any()) yield break;
                foreach (var item in list) yield return item;
                page++;
            }
        }

        public SubscriptionResponse GetSubscription(string id) {
            var restRequest = GetRestRequest("subscriptions/" + id, Method.GET);
            return Execute<SubscriptionResponse>(restRequest);
        }
        public SubscriptionResponse CancelSubscription(string id) {
            var restRequest = GetRestRequest("subscriptions/" + id + "/cancel", Method.PUT);
            return Execute<SubscriptionResponse>(restRequest);
        }
        public PreAuthorizationResponse GetPreAuthorization(string id) {
            var restRequest = GetRestRequest("pre_authorizations/" + id, Method.GET);
            return Execute<PreAuthorizationResponse>(restRequest);
        }
        public PreAuthorizationResponse CancelPreAuthorization(string id) {
            var restRequest = GetRestRequest("pre_authorizations/" + id + "/cancel", Method.PUT);
            return Execute<PreAuthorizationResponse>(restRequest);
        }

        public BillResponse RetryBill(string id) {
            var restRequest = GetRestRequest(string.Format("bills/{0}/retry", id), Method.POST);
            return Execute<BillResponse>(restRequest, HttpStatusCode.Created);
        }

        private static RestRequest GetRestRequest(string resource, Method method, object options = null) {
            var request = new RestRequest(resource, method) {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new NewtonsoftJsonSerializer()
            };
            if (options != null) {
                foreach (var arg in ToHash(options).Where(arg => arg.Value != null)) {
                    var value = arg.Value is DateTimeOffset ? ((DateTimeOffset)arg.Value).ToString("r") : arg.Value.ToString();
                    request.AddParameter(arg.Key, value, ParameterType.GetOrPost);
                }
            }
            return request;
        }

        private static Dictionary<string, object> ToHash(object obj) {
            return obj.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(obj, null));
        }

        public virtual T Execute<T>(RestRequest request, HttpStatusCode expected = HttpStatusCode.OK) where T : new() {
            var client = new RestClient {
                BaseUrl = ApiUrl,
                UserAgent = GoCardless.UserAgent
            };
            var serializer = new Newtonsoft.Json.JsonSerializer {
                ContractResolver = new UnderscoreToCamelCasePropertyResolver(),
            };
            client.AddHandler("application/json", new NewtonsoftJsonDeserializer(serializer));
            request.AddHeader("Authorization", "bearer " + _accessToken); // used on every request
            var handle = HttpRequestSending;
            if (handle != null) {
                var args = new HttpRequestEventArgs() { Method = request.Method.ToString(), RequestUri = client.BuildUri(request) };
                handle(this, args);
            }
            var response = client.Execute<T>(request);
            if (response.StatusCode != expected) {
                var ex = new ApiException("Expected response " + (int)expected + " " + expected + " but received " +
                                 (int)response.StatusCode + " " + response.StatusCode +
                                 ". See Content for more details");
                try {
                    ex.Content = JObject.Parse(response.Content);
                } catch (Exception) {
                }
                ex.RawContent = response.Content;
                throw ex;
            }
            return response.Data;
        }

        public BillResponse PostBill(decimal amount, string preAuthorizationId, string name = null, string description = null, DateTime? chargeCustomerAt = null) {

            var restRequest = GetRestRequest("bills", Method.POST);
            restRequest.AddBody(new {
                bill = new {
                    amount = amount.ToString(CultureInfo.InvariantCulture),
                    pre_authorization_id = preAuthorizationId,
                    charge_customer_at = (chargeCustomerAt.HasValue) ? string.Format("{0:yyyy-MM-dd}", chargeCustomerAt) : null,
                    name,
                    description
                }
            });
            return Execute<BillResponse>(restRequest, HttpStatusCode.Created);
        }
    }
}