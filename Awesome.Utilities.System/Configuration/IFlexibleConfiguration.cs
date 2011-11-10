using System.Collections.Specialized;

namespace System.Configuration
{
    public interface IFlexibleConfiguration
    {
        ConnectionStringSettingsCollection ConnectionStrings { get; }
        NameValueCollection AppSettings { get; }
    }
}