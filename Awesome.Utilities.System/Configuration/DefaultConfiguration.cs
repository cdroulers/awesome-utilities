using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace System.Configuration
{
    public class DefaultConfiguration : IFlexibleConfiguration
    {
        public virtual ConnectionStringSettingsCollection ConnectionStrings
        {
            get { return ConfigurationManager.ConnectionStrings; }
        }

        public virtual NameValueCollection AppSettings
        {
            get { return ConfigurationManager.AppSettings; }
        }


        public virtual object GetSection(string sectionName)
        {
            return ConfigurationManager.GetSection(sectionName);
        }

        public virtual Configuration OpenExeConfiguration(ConfigurationUserLevel userLevel)
        {
            return ConfigurationManager.OpenExeConfiguration(userLevel);
        }

        public virtual Configuration OpenExeConfiguration(string exePath)
        {
            return ConfigurationManager.OpenExeConfiguration(exePath);
        }

        public virtual Configuration OpenMachineConfiguration()
        {
            return ConfigurationManager.OpenMachineConfiguration();
        }

        public virtual Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel)
        {
            return ConfigurationManager.OpenMappedExeConfiguration(fileMap, userLevel);
        }

        public virtual Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap)
        {
            return ConfigurationManager.OpenMappedMachineConfiguration(fileMap);
        }

        public virtual void RefreshSection(string sectionName)
        {
            ConfigurationManager.RefreshSection(sectionName);
        }
    }
}
