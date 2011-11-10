using System.Collections.Specialized;

namespace System.Configuration
{
    public interface IFlexibleConfiguration
    {
        ConnectionStringSettingsCollection ConnectionStrings { get; }
        NameValueCollection AppSettings { get; }

        object GetSection(string sectionName);
        Configuration OpenExeConfiguration(ConfigurationUserLevel userLevel);
        Configuration OpenExeConfiguration(string exePath);
        Configuration OpenMachineConfiguration();
        Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel);
        Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap);
        void RefreshSection(string sectionName);
    }
}