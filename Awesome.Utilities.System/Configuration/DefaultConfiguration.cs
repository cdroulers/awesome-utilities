using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Configuration
{
    public class DefaultConfiguration : IFlexibleConfiguration
    {
        public ConnectionStringSettingsCollection ConnectionStrings
        {
            get { return ConfigurationManager.ConnectionStrings; }
        }

        public Collections.Specialized.NameValueCollection AppSettings
        {
            get { return ConfigurationManager.AppSettings; }
        }
    }
}
