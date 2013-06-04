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
        /// key signature.
        /// </summary>
        /// <param name="params">the parameters to sign</param>
        /// <returns>the parameters with the new signature key</returns>
        private static Utils.HashParams SignParams(Utils.HashParams @params)
        {
            var signature = Utils.GetSignatureForParams(@params, GoCardless.AccountDetails.AppSecret);
            @params.Add("signature", signature);
            return @params;
        }


        /// <summary>
        /// Generate the URL for creating a limit, including the
        /// provided params, nonce, timestamp and signature
        /// </summary>
        /// <param name="type">the limit type (subscription, etc)</param>
        /// <param name="requestResource">the request values</param>
        /// <param name="redirectUri">optional override URI on success</param>
        /// <param name="cancelUri">optional override URI on cancel</param>
        /// <param name="state">optional state, gets passed back with the successful payload</param>
        /// <returns>the generated URL</returns>
        private static string GenerateNewLimitUrl(string type, object requestResource,
            string redirectUri = null, string cancelUri = null, string state = null)
        {
            var hash = new Utils.HashParams
                           {
                               {"client_id", GoCardless.AccountDetails.AppId},
                               {"nonce", GoCardless.GenerateNonce()},
                               {"timestamp", GoCardless.GetUtcNow().IsoFormatTime()},
                           };

            hash = requestResource.ToHashParams(hash, type);


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

            var url = GoCardless.BaseUrl + "/connect/" + type + "s/new?" + hash.ToQueryString();
            return url;
        }


        /// <summary>
        /// Generate the URL for creating a new subscription.
        /// </summary>
        /// <remarks>
        /// The parameters passed in define various attributes of the subscription. 
        /// Redirecting a user to the resulting URL will show them a page where they 
        /// can approve or reject the subscription described by the parameters. 
        /// Note that this method automatically includes the nonce, timestamp and signature. 
        /// </remarks>
        /// <param name="requestResource">the request values</param>
        /// <param name="redirectUri">optional override URI on success</param>
        /// <param name="cancelUri">optional override URI on cancel</param>
        /// <param name="state">optional state, gets passed back with the successful payload</param>
        /// <returns>the generated URL</returns>
        public string NewSubscriptionUrl(SubscriptionRequest requestResource,
            string redirectUri = null, string cancelUri = null, string state = null)
        {
            return GenerateNewLimitUrl("subscription", requestResource, redirectUri, cancelUri, state);
        }

        /// <summary>
        /// Generate the URL for creating a new pre authorization. The parameters
        /// passed in define various attributes of the pre authorization. Redirecting
        /// a user to the resulting URL will show them a page where they can approve
        /// or reject the pre authorization described by the parameters. Note that
        /// this method automatically includes the nonce, timestamp and signature.
        /// </summary>
        /// <param name="requestResource">the request values</param>
        /// <param name="redirectUri">optional override URI on success</param>
        /// <param name="cancelUri">optional override URI on cancel</param>
        /// <param name="state">optional state, gets passed back with the successful payload</param>
        /// <returns>the generated URL</returns>
        public string NewPreAuthorizationUrl(PreAuthorizationRequest requestResource,
            string redirectUri = null, string cancelUri = null, string state = null)
        {
            return GenerateNewLimitUrl("pre_authorization", requestResource, redirectUri, cancelUri, state);
        }

        /// <summary>
        /// Generate the URL for creating a new bill. The parameters passed in define
        /// various attributes of the bill. Redirecting a user to the resulting URL
        /// will show them a page where they can approve or reject the bill described
        /// by the parameters. Note that this method automatically includes the
        /// nonce, timestamp and signature.
        /// </summary>
        /// <param name="requestResource">the request values</param>
        /// <param name="redirectUri">optional override URI on success</param>
        /// <param name="cancelUri">optional override URI on cancel</param>
        /// <param name="state">optional state, gets passed back with the successful payload</param>
        /// <returns>the generated URL</returns>
        public string NewBillUrl(BillRequest requestResource,
            string redirectUri = null, string cancelUri = null, string state = null)
        {
            return GenerateNewLimitUrl("bill", requestResource, redirectUri, cancelUri, state);
        }

        /// <summary>
        /// Confirm a newly-created subscription, pre-authorzation or one-off
        /// payment. This method also checks that the resource response data includes
        /// a valid signature and will throw a {SignatureException} if the signature is
        /// invalid.
        /// </summary>
        /// <param name="requestContent">the response parameters returned by the API server</param>
        /// <returns>the confirmed resource object</returns>
        public ConfirmResource ConfirmResource(NameValueCollection requestContent)
        {
            var resource = DeserializeAndValidateRequestSignature(requestContent);

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
                                 UserAgent = GoCardless.UserAgent
                             };
            var serializer = new JsonSerializer
            {
                ContractResolver = new UnderscoreToCamelCasePropertyResolver(),
            };
            client.AddHandler("application/json", new NewtonsoftJsonDeserializer(serializer));
            client.Authenticator = new HttpBasicAuthenticator(GoCardless.AccountDetails.AppId, GoCardless.AccountDetails.AppSecret);
            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ApiException("Unexpected response : " + (int)response.StatusCode + " " + response.StatusCode);
            }

            return resource;
        }

        /// <summary>
        /// Validate the signature of a specified <c>ConfirmResource</c>.
        /// This method is visible for testing and should not be called by client applications.
        /// </summary>
        internal ConfirmResource DeserializeAndValidateRequestSignature(NameValueCollection requestContent)
        {
            var resource = new ConfirmResource
            {
                ResourceId = requestContent["resource_id"],
                ResourceType = requestContent["resource_type"],
                ResourceUri = Uri.UnescapeDataString(requestContent["resource_uri"]),
                Signature = requestContent["signature"],
                State = requestContent["state"],
            };

            if (resource.ResourceId == null) throw new ArgumentNullException("ResourceId");
            if (resource.ResourceType == null) throw new ArgumentNullException("ResourceType");
            if (resource.ResourceUri == null) throw new ArgumentNullException("ResourceUri");
            if (resource.Signature == null) throw new ArgumentNullException("Signature");

            var signature = resource.Signature;
            resource.Signature = null;

            if (signature != Utils.GetSignatureForParams(resource.ToHashParams(), GoCardless.AccountDetails.AppSecret))
            {
                throw new SignatureException("An invalid signature was detected");
            }

            return resource;
        }
    }
}