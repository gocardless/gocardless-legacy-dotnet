using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Web;
using GoCardlessSdk.Helpers;
using System.Security.Cryptography;

namespace GoCardlessSdk.WebHooks
{
    class SignatureValidator
    {

        public void Validate(string key, JObject content)
        {
            var payload = content["payload"];
            var tuples = Flatten(null, payload);
            tuples.Sort();

            StringBuilder result = new StringBuilder();
            var values = new List<string>();
            foreach (var tuple in tuples)
            {
                var encoded = tuple;
                //var encoded = PercentEncode(tuple);
                values.Add(string.Format("{0}={1}", encoded.Key, encoded.Value));
            }

            string digest = String.Join("&", values.ToArray());

            Byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            Byte[] paramsBytes = Encoding.UTF8.GetBytes(digest);
            Byte[] hashedBytes = new HMACSHA256(keyBytes).ComputeHash(paramsBytes);
            string sig = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

            var signature = payload["signature"];
            Console.WriteLine(sig);
            Console.WriteLine(signature);
        }

        public StringTuple PercentEncode(StringTuple tuple)
        {
            return new StringTuple(Utils.PercentEncode(tuple.Key), Utils.PercentEncode(tuple.Value));
        }

        public List<StringTuple> Flatten(string parentKey, JToken root)
        {
            List<StringTuple> result = new List<StringTuple>();

            if (root is JValue)
            {
                result.Add(new StringTuple(parentKey, root.ToString()));
            }
            else if (root is JArray)
            {
                result.AddRange(flattenArray(parentKey, (JArray)root));
            }
            else
            {
                foreach (JProperty token in root)
                {
                    if (token.Value is JValue)
                    {
                        result.Add(new StringTuple(parentKey + token.Name, (string)token.Value));
                    }
                    else if (token.Value is JArray)
                    {
                        var key = parentKey == null ? token.Name : parentKey + "[]" + token.Name;
                        result.AddRange(flattenArray(key, (JArray)token.Value));
                    }
                    else if (token.Value is JContainer)
                    {
                        var key = parentKey == null ? token.Name : parentKey + "[" + token.Name + "]";
                        result.AddRange(flattenHash(key, (JContainer)token.Value));
                    }
                }
            }

            return result;
        }

        private List<StringTuple> flattenArray(string key, JArray array)
        {
            key += "[]";
            var result = new List<StringTuple>();
            foreach (var token in array)
            {
                result.AddRange(Flatten(key, token));
            }

            return result;
        }

        private List<StringTuple> flattenHash(string parentKey, JContainer hash)
        {
            var result = new List<StringTuple>();
            foreach (JProperty entry in hash)
            {
                string key = string.Format("{0}[{1}]", parentKey, entry.Name);
                result.AddRange(Flatten(key, entry.Value));
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
