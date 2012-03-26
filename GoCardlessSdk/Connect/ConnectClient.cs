using System;
using System.Collections.Specialized;
using System.Net;
using GoCardlessSdk.Api;
using GoCardlessSdk.Helpers;
using Newtonsoft.Json;
using RestSharp;

namespace GoCardlessSdk.Connect
{
    public class ConnectClient
    {
        /// <summary>
        /// Add a signature to a Hash of parameters. The signature will be generated
        /// from the app secret and the provided parameters, and should be used
        /// whenever signed data needs to be sent to GoCardless (e.g. when creating
        /// a new subscription). The signature will be added to the hash under the
        /// key +:signature+.
        /// </summary>
        /// <param name="params">params the parameters to sign</param>
        /// <returns>the parameters with the new +:signature+ key</returns>
        private static Utils.HashParams SignParams(Utils.HashParams @params)
        {
            var signature = Utils.GetSignatureForParams(@params, GoCardlessSdk.GoCardless.AccountDetails.AppSecret);
            @params.Add("signature", signature);
            return @params;
        }


        /// <summary>
        /// Generate the URL for creating a limit of type +type+, including the
        /// provided params, nonce, timestamp and signature
        /// </summary>
        /// <param name="type">type the limit type (+:subscription+, etc)</param>
        /// <param name="params">params the parameters</param>
        /// <returns>the generated URL</returns>
        private static string NewLimitUrl(string type, object @params,
            string redirectUri = null, string cancelUri = null, string state = null)
        {
            var hash = new Utils.HashParams
                           {
                               {"client_id", GoCardlessSdk.GoCardless.AccountDetails.AppId},
                               {"nonce", GoCardlessSdk.GoCardless.GenerateNonce()},
                               {"timestamp", GoCardlessSdk.GoCardless.GetUtcNow().IsoFormatTime()},
                           };

            hash = @params.ToHashParams(hash, type);


            if (redirectUri != null)
            {
                hash.Add("redirect_uri", redirectUri);
            }
            if (cancelUri != null)
            {
                hash.Add("cancel_uri", cancelUri);
            }
            if (state != null)
            {
                hash.Add("state", state);
            }

            hash = SignParams(hash);

            var url = GoCardlessSdk.GoCardless.BaseUrl + "/connect/" + type + "s/new?" + hash.ToQueryString();
            return url;
        }


        /// <summary>
        /// Generate the URL for creating a new subscription. The parameters passed
        /// in define various attributes of the subscription. Redirecting a user to
        /// the resulting URL will show them a page where they can approve or reject
        /// the subscription described by the parameters. Note that this method
        /// automatically includes the nonce, timestamp and signature. 
        /// </summary>
        /// <param name="params">params the subscription parameters</param>
        /// <returns>the generated URL</returns>
        public static string NewSubscriptionUrl(SubscriptionRequest @params,
            string redirectUri = null, string cancelUri = null, string state = null)
        {
            return NewLimitUrl("subscription", @params, redirectUri, cancelUri, state);
        }

        /// <summary>
        /// Generate the URL for creating a new pre authorization. The parameters
        /// passed in define various attributes of the pre authorization. Redirecting
        /// a user to the resulting URL will show them a page where they can approve
        /// or reject the pre authorization described by the parameters. Note that
        /// this method automatically includes the nonce, timestamp and signature.
        /// </summary>
        /// <param name="params">params the pre authorization parameters</param>
        /// <returns>the generated URL</returns>
        public static string NewPreAuthorizationUrl(PreAuthorizationRequest @params,
            string redirectUri = null, string cancelUri = null, string state = null)
        {
            return NewLimitUrl("pre_authorization", @params, redirectUri, cancelUri, state);
        }

        /// <summary>
        /// Generate the URL for creating a new bill. The parameters passed in define
        /// various attributes of the bill. Redirecting a user to the resulting URL
        /// will show them a page where they can approve or reject the bill described
        /// by the parameters. Note that this method automatically includes the
        /// nonce, timestamp and signature.
        /// </summary>
        /// <param name="params">params the bill parameters</param>
        /// <returns>the generated URL</returns>
        public static string NewBillUrl(BillRequest @params,
            string redirectUri = null, string cancelUri = null, string state = null)
        {
            return NewLimitUrl("bill", @params, redirectUri, cancelUri, state);
        }

        private static void ThrowIfNull(string s)
        {
            if (s == null)
            {
                // TODO: name of parameter
                throw new ArgumentException("Parameter missing ");
            }
        }

        /// <summary>
        /// Confirm a newly-created subscription, pre-authorzation or one-off
        /// payment. This method also checks that the resource response data includes
        /// a valid signature and will throw a {SignatureException} if the signature is
        /// invalid.
        /// </summary>
        /// <param name="params">params the response parameters returned by the API server</param>
        /// <returns>the confirmed resource object</returns>
        public static ConfirmResource ConfirmResource(NameValueCollection requestContent)
        {
            var resource = new ConfirmResource
                               {
                                   ResourceId = requestContent["resource_id"],
                                   ResourceType = requestContent["resource_type"],
                                   ResourceUri = requestContent["resource_uri"],
                                   Signature = requestContent["signature"],
                                   State = requestContent["state"],
                               };

            ThrowIfNull(resource.ResourceId);
            ThrowIfNull(resource.ResourceType);
            ThrowIfNull(resource.ResourceUri);
            ThrowIfNull(resource.Signature);

            var signature = resource.Signature;
            resource.Signature = null;

            if (signature != Utils.GetSignatureForParams(resource.ToHashParams(), GoCardlessSdk.GoCardless.AccountDetails.AppSecret))
            {
                throw new SignatureException("An invalid signature was detected");
            }

            var request = new RestRequest("confirm", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(
                new
                    {
                        resource_id = resource.ResourceId,
                        resource_type = resource.ResourceType
                    });

            var client = new RestClient
            {
                BaseUrl = ApiClient.ApiUrl,
            };
            var serializer = new JsonSerializer
            {
                ContractResolver = new UnderscoreToCamelCasePropertyResolver(),
            };
            client.AddHandler("application/json", new NewtonsoftJsonDeserializer(serializer));
            client.Authenticator = new HttpBasicAuthenticator(GoCardlessSdk.GoCardless.AccountDetails.AppId, GoCardlessSdk.GoCardless.AccountDetails.AppSecret);
            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ApiException("Unexpected response : " + (int) response.StatusCode + " " + response.StatusCode);
            }

            return resource;
        }
    }
}