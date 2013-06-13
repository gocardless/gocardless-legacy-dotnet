using System;
using Newtonsoft.Json.Linq;

namespace GoCardlessSdk
{
    /// <summary>
    /// GoCardless - ApiException
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ApiException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public JObject Content { get; set; }

        /// <summary>
        /// Gets or sets the content of the raw.
        /// </summary>
        /// <value>
        /// The content of the raw.
        /// </value>
        public string RawContent { get; set; }
    }
}
