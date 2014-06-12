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
        private ContextualNotificationData success;
        private ContextualNotificationData warning;
        private ContextualNotificationData error;

        /// <summary>
        /// Gets or sets the info notification.
        /// </summary>
        public ContextualNotificationData Info
        {
            get { return this.info; }
            set { this.Set(ref this.info, value); }
        }

        /// <summary>
        /// Gets or sets the success notification.
        /// </summary>
        public ContextualNotificationData Success
        {
            get { return this.success; }
            set { this.Set(ref this.success, value); }
        }
        
        /// <summary>
        /// Gets or sets the warning notification.
        /// </summary>
        public ContextualNotificationData Warning
        {
            get { return this.warning; }
            set { this.Set(ref this.warning, value); }
        }
        
        /// <summary>
        /// Gets or sets the error notification.
        /// </summary>
        public ContextualNotificationData Error
        {
            get { return this.error; }
            set { this.Set(ref this.error, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextualNotification"/> class.
        /// </summary>
        /// <param name="store">The store.</param>
        public ContextualNotification(IDictionary<string, object> store)
        {
            this.info = new ContextualNotificationData(store, "Info");
            this.success = new ContextualNotificationData(store, "Notice");
            this.warning = new ContextualNotificationData(store, "Warning");
            this.error = new ContextualNotificationData(store, "Error");
        }

        private void Set(ref ContextualNotificationData flash, ContextualNotificationData newValue)
        {
            if (newValue.Type != ContextualNotificationData.DefaultType)
            {
                throw new InvalidCastException("Cannot set a flash that is not new");
            }

            flash.Clear();
            flash.Add(newValue);
        }

        /// <summary>
        /// returns whether any of the notifications have data.
        /// </summary>
        /// <returns>True if there are any notifications.</returns>
        public bool Any()
        {
            return !this.Info.IsEmpty || !this.Success.IsEmpty || !this.Warning.IsEmpty || !this.Error.IsEmpty;
        }
    }
}