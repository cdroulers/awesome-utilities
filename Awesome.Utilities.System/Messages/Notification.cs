using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Messages
{
    /// <summary>
    ///     Contextual notifications for anything!
    /// </summary>
    public class ContextualNotification
    {
        private ContextualNotificationData info;
        /// <summary>
        /// Gets or sets the info notification.
        /// </summary>
        public ContextualNotificationData Info
        {
            get
            {
                return info;
            }
            set
            {
                this.Set(ref info, value, "Info");
            }
        }
        private ContextualNotificationData success;
        /// <summary>
        /// Gets or sets the success notification.
        /// </summary>
        public ContextualNotificationData Success
        {
            get
            {
                return success;
            }
            set
            {
                this.Set(ref success, value, "Notice");
            }
        }

        private ContextualNotificationData warning;
        /// <summary>
        /// Gets or sets the warning notification.
        /// </summary>
        public ContextualNotificationData Warning
        {
            get
            {
                return warning;
            }
            set
            {
                this.Set(ref warning, value, "Warning");
            }
        }

        private ContextualNotificationData error;
        /// <summary>
        /// Gets or sets the error notification.
        /// </summary>
        public ContextualNotificationData Error
        {
            get
            {
                return error;
            }
            set
            {
                this.Set(ref error, value, "Error");
            }
        }

        private readonly IDictionary<string, object> store;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextualNotification"/> class.
        /// </summary>
        /// <param name="store">The store.</param>
        public ContextualNotification(IDictionary<string, object> store)
        {
            this.store = store;
            this.info = new ContextualNotificationData(this.store, "Info");
            this.success = new ContextualNotificationData(this.store, "Notice");
            this.warning = new ContextualNotificationData(this.store, "Warning");
            this.error = new ContextualNotificationData(this.store, "Error");
        }

        private void Set(ref ContextualNotificationData flash, ContextualNotificationData newValue, string type)
        {
            if (newValue.Type != ContextualNotificationData.DefaultType)
            {
                throw new InvalidCastException("Cannot set a flash that is not new");
            }
            flash.Clear();
            flash.Add((string)newValue);
        }

        /// <summary>
        /// returns whether any of the notifications have data.
        /// </summary>
        /// <returns></returns>
        public bool Any()
        {
            return !this.Info.IsEmpty || !this.Success.IsEmpty || !this.Warning.IsEmpty || !this.Error.IsEmpty;
        }
    }
}