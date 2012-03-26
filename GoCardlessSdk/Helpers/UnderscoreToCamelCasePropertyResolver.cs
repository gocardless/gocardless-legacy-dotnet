using Newtonsoft.Json.Serialization;

namespace GoCardlessSdk.Helpers
{
    public class UnderscoreToCamelCasePropertyResolver : DefaultContractResolver
    {
        public UnderscoreToCamelCasePropertyResolver()
            : base(true)
        {
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToUnderscoreCase();
        }
    }
}