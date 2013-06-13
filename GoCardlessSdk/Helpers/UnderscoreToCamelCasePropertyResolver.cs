using Newtonsoft.Json.Serialization;

namespace GoCardlessSdk.Helpers
{
    /// <summary>
    /// GoCardless - UnderscoreToCamelCasePropertyResolver
    /// </summary>
    public class UnderscoreToCamelCasePropertyResolver : DefaultContractResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnderscoreToCamelCasePropertyResolver"/> class.
        /// </summary>
        public UnderscoreToCamelCasePropertyResolver()
            : base(true)
        {
        }

        /// <summary>
        /// Resolves the name of the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>string resolved name</returns>
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToUnderscoreCase();
        }
    }
}