using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using GoCardlessSdk.Helpers;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace GoCardlessSdk.Api
{
    /// <summary>
    /// GoCardless - ApiClient
    /// </summary>
    public class ApiClient
    {
        /// <summary>
        /// API Path
        /// </summary>
        public static readonly string ApiPath = "/api/v1";

        private readonly string _accessToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient"/> class.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        public ApiClient(string accessToken)
        {
            _accessToken = accessToken;
        }

        /// <summary>
        /// Gets the API URL.
        /// </summary>
        public static string ApiUrl
        {
            get { return GoCardless.BaseUrl + ApiPath; }
        }

        /// <summary>
        /// Gets the merchant.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>MerchantResponse object</returns>
        public MerchantResponse GetMerchant(string id)
        {
            var restRequest = GetRestRequest("merchants/" + id, Method.GET);
            var merchant = Execute<MerchantResponse>(restRequest);
            merchant.ApiClient = this;
            return merchant;
        }

        /// <summary>
        /// Gets the bill.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>BillResponse object</returns>
        public BillResponse GetBill(string id)
        {
            var restRequest = GetRestRequest("bills/" + id, Method.GET);
            return Execute<BillResponse>(restRequest);
        }

        /// <summary>
        /// Gets the payout.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>PayoutResponse object</returns>
        public PayoutResponse GetPayout(string id)
        {
            var restRequest = GetRestRequest("payouts/" + id, Method.GET);
            return Execute<PayoutResponse>(restRequest);
        }

        /// <summary>
        /// Gets the merchant bills.
        /// </summary>
        /// <param name="merchantId">The merchant id.</param>
        /// <param name="sourceId">The source id.</param>
        /// <param name="subscriptionId">The subscription id.</param>
        /// <param name="preAuthorizationId">The pre authorization id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="before">The before.</param>
        /// <param name="after">The after.</param>
        /// <param name="paid">The paid.</param>
        /// <returns>IEnumerable over BillResponse</returns>
        public IEnumerable<BillResponse> GetMerchantBills(string merchantId, string sourceId = null, string subscriptionId = null, string preAuthorizationId = null, string userId = null, DateTimeOffset? before = null, DateTimeOffset? after = null, bool? paid = null)
        {
            var options = new { source_id = sourceId, subscription_id = subscriptionId, pre_authorization_id = preAuthorizationId, user_id = userId, before, after, paid };
            var restRequest = GetRestRequest("merchants/" + merchantId + "/bills", Method.GET, options);
            return Execute<List<BillResponse>>(restRequest).AsReadOnly();
        }

        /// <summary>
        /// Gets the merchant pre authorizations.
        /// </summary>
        /// <param name="merchantId">The merchant id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="before">The before.</param>
        /// <param name="after">The after.</param>
        /// <returns>IEnumerable over PreAuthorizationResponse</returns>
        public IEnumerable<PreAuthorizationResponse> GetMerchantPreAuthorizations(string merchantId, string userId = null, DateTimeOffset? before = null, DateTimeOffset? after = null)
        {
            var options = new {user_id = userId, before, after};
            var restRequest = GetRestRequest("merchants/" + merchantId + "/pre_authorizations", Method.GET, options);
            return Execute<List<PreAuthorizationResponse>>(restRequest).AsReadOnly();
        }

        /// <summary>
        /// Gets the merchant subscriptions.
        /// </summary>
        /// <param name="merchantId">The merchant id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="before">The before.</param>
        /// <param name="after">The after.</param>
        /// <returns>IEnumerable over SubscriptionResponse</returns>
        public IEnumerable<SubscriptionResponse> GetMerchantSubscriptions(string merchantId, string userId = null, DateTimeOffset? before = null, DateTimeOffset? after = null)
        {
            var options = new { user_id = userId, before, after };
            var restRequest = GetRestRequest("merchants/" + merchantId + "/subscriptions", Method.GET, options);
            return Execute<List<SubscriptionResponse>>(restRequest).AsReadOnly();
        }

        /// <summary>
        /// Gets the merchant users.
        /// </summary>
        /// <param name="merchantId">The merchant id.</param>
        /// <returns>IEnumerable over UserResponse</returns>
        public IEnumerable<UserResponse> GetMerchantUsers(string merchantId)
        {
            var restRequest = GetRestRequest("merchants/" + merchantId + "/users", Method.GET);
            return Execute<List<UserResponse>>(restRequest).AsReadOnly();
        }

        /// <summary>
        /// Gets the merchant payouts.
        /// </summary>
        /// <param name="merchantId">The merchant id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>IEnumerable over PayoutResponse</returns>
        public IEnumerable<PayoutResponse> GetMerchantPayouts(string merchantId, string userId = null)
        {
            var restRequest = GetRestRequest("merchants/" + merchantId + "/payouts", Method.GET);
            return Execute<List<PayoutResponse>>(restRequest).AsReadOnly();
        }

        /// <summary>
        /// Gets the subscription.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>SubscriptionResponse object</returns>
        public SubscriptionResponse GetSubscription(string id)
        {
            var restRequest = GetRestRequest("subscriptions/" + id, Method.GET);
            return Execute<SubscriptionResponse>(restRequest);
        }

        /// <summary>
        /// Cancels the subscription.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>SubscriptionResponse object</returns>
        public SubscriptionResponse CancelSubscription(string id)
        {
            var restRequest = GetRestRequest("subscriptions/" + id + "/cancel", Method.PUT);
            return Execute<SubscriptionResponse>(restRequest);
        }

        /// <summary>
        /// Gets the pre authorization.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>PreAuthorizationResponse object</returns>
        public PreAuthorizationResponse GetPreAuthorization(string id)
        {
            var restRequest = GetRestRequest("pre_authorizations/" + id, Method.GET);
            return Execute<PreAuthorizationResponse>(restRequest);
        }

        /// <summary>
        /// Cancels the pre authorization.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>PreAuthorizationResponse object</returns>
        public PreAuthorizationResponse CancelPreAuthorization(string id)
        {
            var restRequest = GetRestRequest("pre_authorizations/" + id + "/cancel", Method.PUT);
            return Execute<PreAuthorizationResponse>(restRequest);
        }

        /// <summary>
        /// Retries the bill.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>BillResponse object</returns>
        public BillResponse RetryBill(string id)
        {
            var restRequest = GetRestRequest(string.Format("bills/{0}/retry", id), Method.POST);
            return Execute<BillResponse>(restRequest, HttpStatusCode.Created);
        }

        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <typeparam name="T">Type T</typeparam>
        /// <param name="request">The request.</param>
        /// <param name="expected">The expected.</param>
        /// <returns>
        /// object of Type T
        /// </returns>
        public T Execute<T>(RestRequest request, HttpStatusCode expected = HttpStatusCode.OK) where T : new()
        {
            var client = new RestClient
                             {
                                 BaseUrl = ApiUrl,
                                 UserAgent = GoCardless.UserAgent
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
                var ex = new ApiException("Expected response " + (int)expected + " " + expected + " but received " +
                                 (int)response.StatusCode + " " + response.StatusCode +
                                 ". See Content for more details");
                try
                {
                    ex.Content = JObject.Parse(response.Content);
                }
                catch (Exception)
                {
                }

                ex.RawContent = response.Content;
                throw ex;
            }

            return response.Data;
        }

        /// <summary>
        /// Posts the bill.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="preAuthorizationId">The pre authorization id.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <returns>BillResponse object</returns>
        public BillResponse PostBill(decimal amount, string preAuthorizationId, string name = null, string description = null)
        {
            var restRequest = GetRestRequest("bills", Method.POST);
            restRequest.AddBody(new
            {
                bill = new
                    {
                        amount = amount.ToString(CultureInfo.InvariantCulture),
                        pre_authorization_id = preAuthorizationId,
                        name,
                        description
                    }
            });
            return Execute<BillResponse>(restRequest, HttpStatusCode.Created);
        }

        private static RestRequest GetRestRequest(string resource, Method method, object options = null)
        {
            var request = new RestRequest(resource, method)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new NewtonsoftJsonSerializer()
            };

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
    }
}