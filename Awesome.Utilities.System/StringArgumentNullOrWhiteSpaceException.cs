using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
    public class StringArgumentNullOrWhiteSpaceException : ArgumentException
    {
        public StringArgumentNullOrWhiteSpaceException()
        {
        }

        public StringArgumentNullOrWhiteSpaceException(string paramName)
            : base(null, paramName)
        {
        }

        protected StringArgumentNullOrWhiteSpaceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public StringArgumentNullOrWhiteSpaceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public StringArgumentNullOrWhiteSpaceException(string paramName, string message)
            : base(message, paramName)
        {
        }
    }
}
