using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
    public class StringArgumentNullOrEmptyException : ArgumentException
    {
        public StringArgumentNullOrEmptyException()
        {
        }

        public StringArgumentNullOrEmptyException(string paramName)
            : base(null, paramName)
        {
        }

        protected StringArgumentNullOrEmptyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public StringArgumentNullOrEmptyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public StringArgumentNullOrEmptyException(string paramName, string message)
            : base(message, paramName)
        {
        }
    }
}
