using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Web;
using GoCardlessSdk.Helpers;
using System.Security.Cryptography;
using System.Linq;

namespace GoCardlessSdk.WebHooks
{
    class SignatureValidator
    {

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
            
            string digest = String.Join("&", values.ToArray());

            Byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            Byte[] paramsBytes = Encoding.UTF8.GetBytes(digest);
            Byte[] hashedBytes = new HMACSHA256(keyBytes).ComputeHash(paramsBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        public StringTuple PercentEncode(StringTuple tuple)
        {
            return new StringTuple(Utils.PercentEncode(tuple.Key), Utils.PercentEncode(tuple.Value));
        }

        public List<StringTuple> Flatten(string keyFormatString, JToken token)
        {
            keyFormatString = keyFormatString == null ? "" : keyFormatString;
            List<StringTuple> result = new List<StringTuple>();

            if (token is JProperty)
            {
                JProperty property = token as JProperty;
                keyFormatString = string.Format(keyFormatString, property.Name);
            }
            else if (token is JArray)
            {
                keyFormatString += "[]";
            }
            else if (token is JContainer)
            {
                keyFormatString += keyFormatString == string.Empty ? "{0}" : "[{0}]";
            }
            else
            {
                JValue value = token as JValue;

                // TODO - we should remove the signature before calling this method,
                // But LINQ to JSON structures appear immutable.
                if (keyFormatString != "signature")
                {
                    string val;
                    // TODO - it would be nice if we could just turn off JSON.net's type inference and work with strings.
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
            }

            foreach (var childToken in token)
            {
                result.AddRange(Flatten(keyFormatString, childToken));
            }
            
            return result;
        }
    }

    class StringTuple : IComparable<StringTuple>
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public StringTuple(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public int CompareTo(StringTuple other)
        {
            var delta = this.Key.CompareTo(other.Key);
            return delta == 0 ? this.Value.CompareTo(other.Value) : delta;
        }
    }
}
