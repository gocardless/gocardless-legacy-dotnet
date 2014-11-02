using System.Globalization;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;

namespace GoCardlessSdk.Helpers
{
    public class NewtonsoftJsonDeserializer : IDeserializer
    {
        private readonly JsonSerializer  _serializer;

        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
        public CultureInfo Culture { get; set; }
 
        public NewtonsoftJsonDeserializer(JsonSerializer serializer)
        {
            this._serializer = serializer;
            Culture = CultureInfo.InvariantCulture;
        }

        public T Deserialize<T>(IRestResponse response)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(response.Content)))
            using (var streamReader = new StreamReader(ms))
            using (var jtr = new JsonTextReader(streamReader))
            {
                return  _serializer.Deserialize<T>(jtr);
            }
        }

    }
}