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
        public MultipleCoordinatesException()
            : base()
        {
        }

        public MultipleCoordinatesException(string message)
            : base(message)
        {
        }

        [SecuritySafeCritical]
        protected MultipleCoordinatesException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public MultipleCoordinatesException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
