using Newtonsoft.Json;
using RestSharp.Serializers;

namespace GoCardlessSdk.Helpers
{
    /// <summary>
    /// GoCardless - NewtonsoftJsonSerializer
    /// </summary>
    public class NewtonsoftJsonSerializer : ISerializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewtonsoftJsonSerializer"/> class.
        /// </summary>
        public NewtonsoftJsonSerializer()
        {
            ContentType = "application/json";
        }

        /// <summary>
        /// Gets or sets the date format.
        /// </summary>
        /// <value>
        /// The date format.
        /// </value>
        public string DateFormat { get; set; }

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
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; set; }

        /// <summary>
        /// Serializes the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>string (serialized data)</returns>
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}