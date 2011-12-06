using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    ///     An exception for object that already exist.
    /// </summary>
    public class AlreadyExistsException : ApplicationException
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Type { get; private set; }
        /// <summary>
        /// Gets the key.
        /// </summary>
        public object Key { get; private set; }
        /// <summary>
        /// Gets the name of the key.
        /// </summary>
        /// <value>
        /// The name of the key.
        /// </value>
        public string KeyName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlreadyExistsException"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="message">The message.</param>
        public AlreadyExistsException(Type type, object key, string keyName, string message)
            : base(message)
        {
            this.Type = type;
            this.Key = key;
            this.KeyName = keyName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlreadyExistsException"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public AlreadyExistsException(Type type, object key, string keyName, string message, Exception innerException)
            : base(message, innerException)
        {
            this.Type = type;
            this.Key = key;
            this.KeyName = keyName;
        }
    }
}
