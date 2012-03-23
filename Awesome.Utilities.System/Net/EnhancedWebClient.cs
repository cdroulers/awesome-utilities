using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Net
{
    /// <summary>
    ///     A web client with more options.
    /// </summary>
    public class EnhancedWebClient : WebClient
    {
        /// <summary>
        /// Gets or sets the timeout, in milliseconds, for requests made from this web client.
        /// </summary>
        public int? Timeout { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnhancedWebClient"/> class.
        /// </summary>
        public EnhancedWebClient()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnhancedWebClient"/> class.
        /// </summary>
        /// <param name="timeout">The timeout in milliseconds</param>
        public EnhancedWebClient(int timeout)
        {
            this.Timeout = timeout;
        }

        /// <summary>
        /// Returns a <see cref="T:System.Net.WebRequest"/> object for the specified resource.
        /// </summary>
        /// <param name="address">A <see cref="T:System.Uri"/> that identifies the resource to request.</param>
        /// <returns>
        /// A new <see cref="T:System.Net.WebRequest"/> object for the specified resource.
        /// </returns>
        protected override WebRequest GetWebRequest(Uri address)
        {
            var result = base.GetWebRequest(address);
            if (this.Timeout.HasValue)
            {
                result.Timeout = this.Timeout.Value;
            }
            return result;
        }
    }
}
