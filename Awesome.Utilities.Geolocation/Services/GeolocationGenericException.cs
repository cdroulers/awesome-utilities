using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace System.Geolocation.Services
{
    /// <summary>
    ///     Thrown when the response returned by the provider is an error.
    /// </summary>
    public class GeolocationGenericException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeolocationGenericException"/> class.
        /// </summary>
        public GeolocationGenericException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeolocationGenericException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public GeolocationGenericException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeolocationGenericException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        [SecuritySafeCritical]
        protected GeolocationGenericException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeolocationGenericException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public GeolocationGenericException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
