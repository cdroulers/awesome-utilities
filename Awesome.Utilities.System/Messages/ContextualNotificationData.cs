using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Messages
{
    /// <summary>
    ///     Data for a contextual notification
    /// </summary>
    public class ContextualNotificationData
    {
        private readonly string data;
        private readonly string type;
        private readonly IDictionary<string, object> store;

        private string Key
        {
            get { return ":Flash." + this.type; }
        }

        internal string Type
        {
            get { return type; }
        }

        internal const string DefaultType = "new";

        private ContextualNotificationData(string data)
        {
            this.data = data;
            this.type = ContextualNotificationData.DefaultType;
        }

        internal ContextualNotificationData(IDictionary<string, object> store, string type)
        {
            this.store = store;
            this.type = type;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="System.Messages.ContextualNotificationData"/>.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator ContextualNotificationData(string s)
        {
            return new ContextualNotificationData(s);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Messages.ContextualNotificationData"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="flash">The flash.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator string(ContextualNotificationData flash)
        {
            if (flash.type == ContextualNotificationData.DefaultType)
            {
                return flash.data;
            }

            return flash.store.ContainsKey(flash.Key) ? flash.store[flash.Key] as string : string.Empty;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this;
        }

        /// <summary>
        /// Adds the specified data to the notification.
        /// </summary>
        /// <param name="newData">The new data.</param>
        /// <param name="withNewLine">if set to <c>true</c> [with new line].</param>
        public void Add(string newData, bool withNewLine = false)
        {
            string value = this;
            if (withNewLine)
            {
                value += Environment.NewLine;
            }

            value += newData;
            this.store[this.Key] = value.Trim();
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this.store[this.Key] = string.Empty;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmpty
        {
            get { return string.IsNullOrWhiteSpace(this); }
        }
    }
}