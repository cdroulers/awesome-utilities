using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Messages
{
    public class NotificationData
    {
        private readonly string data;
        private readonly string type;
        private readonly IDictionary<string, object> store;

        private string Key { get { return ":Flash." + this.type; } }
        internal string Type { get { return type; } }
        internal const string DefaultType = "new";

        private NotificationData(string data)
        {
            this.data = data;
            this.type = NotificationData.DefaultType;
        }

        internal NotificationData(IDictionary<string, object> store, string type)
        {
            this.store = store;
            this.type = type;
        }

        public static implicit operator NotificationData(string s)
        {
            return new NotificationData(s);
        }

        public static implicit operator string(NotificationData flash)
        {
            if (flash.type == NotificationData.DefaultType)
            {
                return flash.data;
            }
            return flash.store.ContainsKey(flash.Key) ? flash.store[flash.Key] as string : string.Empty;
        }

        public override string ToString()
        {
            return this;
        }

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

        public void Clear()
        {
            this.store[this.Key] = string.Empty;
        }

        public bool IsEmpty { get { return string.IsNullOrWhiteSpace(this); } }
    }
}