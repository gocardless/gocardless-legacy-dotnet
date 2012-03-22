using System;
using Newtonsoft.Json.Linq;

namespace GoCardlessSdk
{
    public class ApiException : Exception
    {
        public ApiException(string message) : base(message)
        {
        }
        public JObject Content { get; set; }

        public string RawContent { get; set; }
    }
    public class ClientException : Exception
    {
        public ClientException(string message)
            : base(message)
        {

        }
    }
    public class SignatureException : Exception
    {
        public SignatureException(string message)
            : base(message)
        {
        }
    }
}