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
        /// Gets the addresses that were returned by the provider.
        /// </summary>
        public AddressInformation[] Addresses { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleCoordinatesException"/> class.
        /// </summary>
        /// <param name="addresses">The addresses.</param>
        public MultipleCoordinatesException(AddressInformation[] addresses)
            : base()
        {
            this.Addresses = addresses;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleCoordinatesException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="addresses">The addresses.</param>
        public MultipleCoordinatesException(string message, AddressInformation[] addresses)
            : base(message)
        {
            this.Addresses = addresses;
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
        /// <param name="addresses">The addresses.</param>
        public MultipleCoordinatesException(string message, Exception innerException, AddressInformation[] addresses)
            : base(message, innerException)
        {
            this.Addresses = addresses;
        }
    }
}
