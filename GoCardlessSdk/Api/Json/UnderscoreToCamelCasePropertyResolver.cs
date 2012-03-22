using Newtonsoft.Json.Serialization;

namespace GoCardlessSdk.Api.Json
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