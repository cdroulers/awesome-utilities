using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
    /// <summary>
    ///     Exception for empty or null string arguments
    /// </summary>
    public class StringArgumentNullOrEmptyException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringArgumentNullOrEmptyException"/> class.
        /// </summary>
        public StringArgumentNullOrEmptyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringArgumentNullOrEmptyException"/> class.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        public StringArgumentNullOrEmptyException(string paramName)
            : base(null, paramName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringArgumentNullOrEmptyException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected StringArgumentNullOrEmptyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringArgumentNullOrEmptyException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public StringArgumentNullOrEmptyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringArgumentNullOrEmptyException"/> class.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        public StringArgumentNullOrEmptyException(string paramName, string message)
            : base(message, paramName)
        {
        }
    }
}
