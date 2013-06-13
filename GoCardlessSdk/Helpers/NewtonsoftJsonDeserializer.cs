using System.Globalization;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;

namespace GoCardlessSdk.Helpers
{
    /// <summary>
    /// GoCardless - NewtonsoftJsonDeserializer
    /// </summary>
    public class NewtonsoftJsonDeserializer : IDeserializer
    {
        private readonly JsonSerializer _serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewtonsoftJsonDeserializer"/> class.
        /// </summary>
        /// <param name="serializer">The serializer.</param>
        public NewtonsoftJsonDeserializer(JsonSerializer serializer)
        {
            this._serializer = serializer;
            Culture = CultureInfo.InvariantCulture;
        }

        /// <summary>
        /// Gets or sets the root element.
        /// </summary>
        /// <value>
        /// The root element.
        /// </value>
        public string RootElement { get; set; }

        /// <summary>
        /// Gets or sets the namespace.
        /// </summary>
        /// <value>
        /// The namespace.
        /// </value>
        public string Namespace { get; set; }

        /// <summary>
        /// Gets or sets the date format.
        /// </summary>
        /// <value>
        /// The date format.
        /// </value>
        public string DateFormat { get; set; }

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        /// <value>
        /// The culture.
        /// </value>
        public CultureInfo Culture { get; set; }

        /// <summary>
        /// Deserializes the specified response.
        /// </summary>
        /// <typeparam name="T">Type T</typeparam>
        /// <param name="response">The response.</param>
        /// <returns>Object of Type T</returns>
        public T Deserialize<T>(IRestResponse response)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(response.Content)))
            using (var streamReader = new StreamReader(ms))
            using (var jtr = new JsonTextReader(streamReader))
            {
                return _serializer.Deserialize<T>(jtr);
            }
        }
    }
}