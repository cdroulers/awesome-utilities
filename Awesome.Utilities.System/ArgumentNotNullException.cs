using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
    /// <summary>
    ///     Exception for not null arguments
    /// </summary>
    public class ArgumentNotNullException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNotNullException"/> class.
        /// </summary>
        public ArgumentNotNullException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNotNullException"/> class.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        public ArgumentNotNullException(string paramName)
            : base(null, paramName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNotNullException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected ArgumentNotNullException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNotNullException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ArgumentNotNullException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNotNullException"/> class.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="message">The message.</param>
        public ArgumentNotNullException(string paramName, string message)
            : base(message, paramName)
        {
        }
    }
}
