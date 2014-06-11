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
    public class StringArgumentNotNullOrWhiteSpaceException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringArgumentNotNullOrWhiteSpaceException"/> class.
        /// </summary>
        public StringArgumentNotNullOrWhiteSpaceException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringArgumentNotNullOrWhiteSpaceException"/> class.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        public StringArgumentNotNullOrWhiteSpaceException(string paramName)
            : base(null, paramName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringArgumentNotNullOrWhiteSpaceException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected StringArgumentNotNullOrWhiteSpaceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringArgumentNotNullOrWhiteSpaceException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public StringArgumentNotNullOrWhiteSpaceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringArgumentNotNullOrWhiteSpaceException"/> class.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        public StringArgumentNotNullOrWhiteSpaceException(string paramName, string message)
            : base(message, paramName)
        {
        }
    }
}
