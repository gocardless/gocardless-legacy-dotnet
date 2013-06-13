using System;

namespace GoCardlessSdk
{
    /// <summary>
    /// GoCardless - SignatureException
    /// </summary>
    public class SignatureException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignatureException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public SignatureException(string message)
            : base(message)
        {
        }
    }
}
