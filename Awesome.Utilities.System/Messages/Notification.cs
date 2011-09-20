using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Messages
{
    public class Notification
    {
        private NotificationData info;
        public NotificationData Info
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
        private NotificationData success;
        public NotificationData Success
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

        private NotificationData warning;
        public NotificationData Warning
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

        private NotificationData error;
        public NotificationData Error
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

        public Notification(IDictionary<string, object> store)
        {
            this.store = store;
            this.info = new NotificationData(this.store, "Info");
            this.success = new NotificationData(this.store, "Notice");
            this.warning = new NotificationData(this.store, "Warning");
            this.error = new NotificationData(this.store, "Error");
        }

        private void Set(ref NotificationData flash, NotificationData newValue, string type)
        {
            if (newValue.Type != NotificationData.DefaultType)
            {
                throw new InvalidCastException("Cannot set a flash that is not new");
            }
            flash.Clear();
            flash.Add((string)newValue);
        }

        public bool Any()
        {
            return !this.Info.IsEmpty || !this.Success.IsEmpty || !this.Warning.IsEmpty || !this.Error.IsEmpty;
        }
    }
}