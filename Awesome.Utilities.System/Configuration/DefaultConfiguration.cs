using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace System.Configuration
{
    /// <summary>
    ///     Default implementation. Returns everything directly from ConfigurationManager static methods.
    /// </summary>
    public class DefaultConfiguration : IFlexibleConfiguration
    {
        /// <summary>
        /// Gets the connection strings.
        /// </summary>
        public virtual ConnectionStringSettingsCollection ConnectionStrings
        {
            get { return ConfigurationManager.ConnectionStrings; }
        }

        /// <summary>
        /// Gets the app settings.
        /// </summary>
        public virtual NameValueCollection AppSettings
        {
            get { return ConfigurationManager.AppSettings; }
        }

        /// <summary>
        /// Gets the section. See the doc of ConfigurationManager to know exactly what it does.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns>
        /// A configuration section.
        /// </returns>
        public virtual object GetSection(string sectionName)
        {
            return ConfigurationManager.GetSection(sectionName);
        }

        /// <summary>
        /// Opens the exe configuration. See the doc of ConfigurationManager to know exactly what it does.
        /// </summary>
        /// <param name="userLevel">The user level.</param>
        /// <returns>
        /// A configuration object
        /// </returns>
        public virtual Configuration OpenExeConfiguration(ConfigurationUserLevel userLevel)
        {
            return ConfigurationManager.OpenExeConfiguration(userLevel);
        }

        /// <summary>
        /// Opens the exe configuration. See the doc of ConfigurationManager to know exactly what it does.
        /// </summary>
        /// <param name="exePath">The exe path.</param>
        /// <returns>
        /// A configuration object
        /// </returns>
        public virtual Configuration OpenExeConfiguration(string exePath)
        {
            return ConfigurationManager.OpenExeConfiguration(exePath);
        }

        /// <summary>
        /// Opens the machine configuration. See the doc of ConfigurationManager to know exactly what it does.
        /// </summary>
        /// <returns>
        /// A configuration object
        /// </returns>
        public virtual Configuration OpenMachineConfiguration()
        {
            return ConfigurationManager.OpenMachineConfiguration();
        }

        /// <summary>
        /// Opens the mapped exe configuration. See the doc of ConfigurationManager to know exactly what it does.
        /// </summary>
        /// <param name="fileMap">The file map.</param>
        /// <param name="userLevel">The user level.</param>
        /// <returns>
        /// A configuration object
        /// </returns>
        public virtual Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel)
        {
            return ConfigurationManager.OpenMappedExeConfiguration(fileMap, userLevel);
        }

        /// <summary>
        /// Opens the mapped machine configuration. See the doc of ConfigurationManager to know exactly what it does.
        /// </summary>
        /// <param name="fileMap">The file map.</param>
        /// <returns>
        /// A configuration object
        /// </returns>
        public virtual Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap)
        {
            return ConfigurationManager.OpenMappedMachineConfiguration(fileMap);
        }

        /// <summary>
        /// Refreshes the section. See the doc of ConfigurationManager to know exactly what it does.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        public virtual void RefreshSection(string sectionName)
        {
            ConfigurationManager.RefreshSection(sectionName);
        }
    }
}
