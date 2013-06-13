using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using GoCardlessSdk.Helpers;
using Newtonsoft.Json.Linq;

namespace GoCardlessSdk.WebHooks
{
    /// <summary>
    /// GoCardless - SignatureValidator
    /// </summary>
    internal class SignatureValidator
    {
        /// <summary>
        /// Gets the signature.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="content">The content.</param>
        /// <returns>The Signature</returns>
        public string GetSignature(string key, JObject content)
        {
            var payload = content["payload"];

            var tuples = Flatten(null, payload);
            tuples.Sort();

            StringBuilder result = new StringBuilder();
            var values = new List<string>();
            foreach (var tuple in tuples)
            {
                var encoded = PercentEncode(tuple);
                values.Add(string.Format("{0}={1}", encoded.Key, encoded.Value));
            }

            string digest = string.Join("&", values.ToArray());

            byte[] keybytes = Encoding.UTF8.GetBytes(key);
            byte[] paramsbytes = Encoding.UTF8.GetBytes(digest);
            byte[] hashedbytes = new HMACSHA256(keybytes).ComputeHash(paramsbytes);
            return BitConverter.ToString(hashedbytes).Replace("-", string.Empty).ToLower();
        }

        /// <summary>
        /// Percents the encode.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>StringTuple - percent encoded</returns>
        public StringTuple PercentEncode(StringTuple tuple)
        {
            return new StringTuple(Utils.PercentEncode(tuple.Key), Utils.PercentEncode(tuple.Value));
        }

        /// <summary>
        /// Flattens the specified key format string.
        /// </summary>
        /// <param name="keyFormatString">The key format string.</param>
        /// <param name="token">The token.</param>
        /// <returns>List of StringTuple</returns>
        public List<StringTuple> Flatten(string keyFormatString, JToken token)
        {
            keyFormatString = keyFormatString == null ? string.Empty : keyFormatString;
            List<StringTuple> result = new List<StringTuple>();

            switch (token.Type)
            {
                case JTokenType.Property:
                    JProperty property = token as JProperty;
                    keyFormatString = string.Format(keyFormatString, property.Name);
                    break;
                case JTokenType.Array:
                    keyFormatString += "[]";
                    break;
                case JTokenType.Object:
                    keyFormatString += keyFormatString == string.Empty ? "{0}" : "[{0}]";
                    break;
                default:
                    JValue value = token as JValue;

                    // TODO - we should remove the signature before calling this method,
                    // But LINQ to JSON structures appear immutable.
                    if (keyFormatString != "signature")
                    {
                        string val = string.Empty;

                        //// TODO - it would be nice if we could just turn off JSON.net's type conversion and work with strings.
                        if (value.Type == JTokenType.Date)
                        {
                            val = value.ToString("yyyy-MM-ddTHH:mm:ssZ");
                        }
                        else
                        {
                            val = value.ToString();
                        }

                        result.Add(new StringTuple(keyFormatString, val));
                    }

                    break;
            }

            foreach (var childToken in token)
            {
                result.AddRange(Flatten(keyFormatString, childToken));
            }

            return result;
        }
    }
}
