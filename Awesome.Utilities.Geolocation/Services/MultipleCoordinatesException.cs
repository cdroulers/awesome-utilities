using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace System.Geolocation.Services
{
    /// <summary>
    ///     Thrown when Multiple coordinates are returned for one location, and the method does not support it.
    /// </summary>
    public class MultipleCoordinatesException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleCoordinatesException"/> class.
        /// </summary>
        public MultipleCoordinatesException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleCoordinatesException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public MultipleCoordinatesException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleCoordinatesException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        [SecuritySafeCritical]
        protected MultipleCoordinatesException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleCoordinatesException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public MultipleCoordinatesException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
