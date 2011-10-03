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
        public AddressNotFoundException()
            : base()
        {
        }

        public AddressNotFoundException(string message)
            : base(message)
        {
        }

        [SecuritySafeCritical]
        protected AddressNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public AddressNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
