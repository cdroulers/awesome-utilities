using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
    /// <summary>
    ///     Exception for whitespace or null string arguments
    /// </summary>
    public class StringArgumentNullOrWhiteSpaceException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringArgumentNullOrWhiteSpaceException"/> class.
        /// </summary>
        public StringArgumentNullOrWhiteSpaceException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringArgumentNullOrWhiteSpaceException"/> class.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        public StringArgumentNullOrWhiteSpaceException(string paramName)
            : base(null, paramName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringArgumentNullOrWhiteSpaceException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected StringArgumentNullOrWhiteSpaceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringArgumentNullOrWhiteSpaceException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public StringArgumentNullOrWhiteSpaceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringArgumentNullOrWhiteSpaceException"/> class.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="message">The message.</param>
        public StringArgumentNullOrWhiteSpaceException(string paramName, string message)
            : base(message, paramName)
        {
        }
    }
}
