using System;

namespace namespace GoCardlessSdk
{
    /// <summary>
    /// GoCardless - ClientException
    /// </summary>
    public class ClientException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ClientException(string message)
            : base(message)
        {
        }
    }
}
