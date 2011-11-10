using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace System.Geolocation.Services
{
    /// <summary>
    ///     Thrown when no results exist for the specified address.
    /// </summary>
    public class AddressNotFoundException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressNotFoundException"/> class.
        /// </summary>
        public AddressNotFoundException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public AddressNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressNotFoundException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        [SecuritySafeCritical]
        protected AddressNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public AddressNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
