using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace GoCardlessSdk.WebHooks
{
    class SignatureValidator
    {

        public void Validate(string content)
        {
            JObject o = JObject.Parse(content);
            Flatten(o["payload"]);
        }

        public List<StringTuple> Flatten(JToken root)
        {
            List<StringTuple> result = new List<StringTuple>();
            foreach (JProperty token in root)
            {
                if (token.Value is JValue)
                {
                    new StringTuple(token.Name, (string) token.Value);
                }   
            }

            return result;
        }
    }

    class StringTuple
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public StringTuple(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
