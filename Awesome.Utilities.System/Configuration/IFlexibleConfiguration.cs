using System.Collections.Specialized;

namespace System.Configuration
{
    /// <summary>
    ///     An interface to wrap all configuration stuff
    /// </summary>
    public interface IFlexibleConfiguration
    {
        /// <summary>
        /// Gets the connection strings.
        /// </summary>
        ConnectionStringSettingsCollection ConnectionStrings { get; }

        /// <summary>
        /// Gets the app settings.
        /// </summary>
        NameValueCollection AppSettings { get; }

        /// <summary>
        /// Gets the section. See the doc of ConfigurationManager to know exactly what it does.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns>A configuration section.</returns>
        object GetSection(string sectionName);

        /// <summary>
        /// Opens the exe configuration. See the doc of ConfigurationManager to know exactly what it does.
        /// </summary>
        /// <param name="userLevel">The user level.</param>
        /// <returns>A configuration object</returns>
        Configuration OpenExeConfiguration(ConfigurationUserLevel userLevel);

        /// <summary>
        /// Opens the exe configuration. See the doc of ConfigurationManager to know exactly what it does.
        /// </summary>
        /// <param name="exePath">The exe path.</param>
        /// <returns>A configuration object</returns>
        Configuration OpenExeConfiguration(string exePath);

        /// <summary>
        /// Opens the machine configuration. See the doc of ConfigurationManager to know exactly what it does.
        /// </summary>
        /// <returns>A configuration object</returns>
        Configuration OpenMachineConfiguration();

        /// <summary>
        /// Opens the mapped exe configuration. See the doc of ConfigurationManager to know exactly what it does.
        /// </summary>
        /// <param name="fileMap">The file map.</param>
        /// <param name="userLevel">The user level.</param>
        /// <returns>A configuration object</returns>
        Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel);

        /// <summary>
        /// Opens the mapped machine configuration. See the doc of ConfigurationManager to know exactly what it does.
        /// </summary>
        /// <param name="fileMap">The file map.</param>
        /// <returns>A configuration object</returns>
        Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap);

        /// <summary>
        /// Refreshes the section. See the doc of ConfigurationManager to know exactly what it does.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        void RefreshSection(string sectionName);
    }
}