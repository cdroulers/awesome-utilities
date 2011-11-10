﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Xml.Linq;
using System.IO;

namespace System.Configuration
{
    /// <summary>
    ///     Flexible configuration. By default uses ConfigurationManager methods, but can be overriden with .Configure!
    /// </summary>
    public class FlexibleConfiguration : DefaultConfiguration, IFlexibleConfiguration
    {
        private static IFlexibleConfiguration instance = new DefaultConfiguration();

        /// <summary>
        /// Gets the current flexible configuration manager.
        /// </summary>
        public static IFlexibleConfiguration Manager { get { return FlexibleConfiguration.instance; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlexibleConfiguration"/> class.
        /// </summary>
        public FlexibleConfiguration()
        {
            this.connectionStrings = new ConnectionStringSettingsCollection();
            this.appSettings = new NameValueCollection();
        }

        private readonly ConnectionStringSettingsCollection connectionStrings;
        /// <summary>
        /// Gets the connection strings.
        /// </summary>
        public override ConnectionStringSettingsCollection ConnectionStrings
        {
            get { return this.connectionStrings; }
        }

        private readonly NameValueCollection appSettings;
        /// <summary>
        /// Gets the app settings.
        /// </summary>
        public override NameValueCollection AppSettings
        {
            get { return this.appSettings; }
        }

        private static Configuration OpenFile(string fileName)
        {
            var map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = fileName;
            return ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
        }

        /// <summary>
        /// Loads a manager with the specified parameters.
        /// </summary>
        /// <param name="fileNames">The file names.</param>
        public static IFlexibleConfiguration Load(params string[] fileNames)
        {
            return FlexibleConfiguration.Load(true, fileNames);
        }

        /// <summary>
        /// Loads a manager with the specified parameters.
        /// </summary>
        /// <param name="allowLocalOverrides">if set to <c>true</c> [allow local overrides].</param>
        /// <param name="fileNames">The file names.</param>
        public static IFlexibleConfiguration Load(bool allowLocalOverrides, params string[] fileNames)
        {
            var config = new FlexibleConfiguration();

            foreach (var fileName in fileNames)
            {
                var configuration = OpenFile(fileName);

                var csDataSet = XDocument.Parse(configuration.ConnectionStrings.SectionInformation.GetRawXml());
                foreach (var cs in csDataSet.Descendants("add"))
                {
                    string name = cs.Attribute("name").Value;
                    string value = cs.Attribute("connectionString").Value;
                    string provider = cs.Attribute("providerName").Value;
                    var csSettings = new ConnectionStringSettings(name, value, provider);
                    int index = config.connectionStrings.IndexOf(csSettings);
                    if (index >= 0)
                    {
                        config.connectionStrings[index] = csSettings;
                    }
                    else
                    {
                        config.ConnectionStrings.Add(csSettings);
                    }
                }

                var asDataSet = XDocument.Parse(configuration.AppSettings.SectionInformation.GetRawXml());
                foreach (var cs in asDataSet.Descendants("add"))
                {
                    string key = cs.Attribute("key").Value;
                    string value = cs.Attribute("value").Value;
                    config.AppSettings[key] = value;
                }
            }

            if (allowLocalOverrides)
            {
                foreach (ConnectionStringSettings cs in ConfigurationManager.ConnectionStrings)
                {
                    int index = config.connectionStrings.IndexOf(cs);
                    if (index >= 0)
                    {
                        config.connectionStrings[index] = cs;
                    }
                    else
                    {
                        config.ConnectionStrings.Add(cs);
                    }
                }
                foreach (string key in ConfigurationManager.AppSettings)
                {
                    config.appSettings[key] = ConfigurationManager.AppSettings[key];
                }
            }

            return config;
        }

        /// <summary>
        /// Configures the manager with the specified parameters and sets the instance to the resulting configuration manager.
        /// </summary>
        /// <param name="fileNames">The file names.</param>
        public static void Configure(params string[] fileNames)
        {
            FlexibleConfiguration.Configure(true, fileNames);
        }

        /// <summary>
        /// Configures the manager with the specified parameters and sets the instance to the resulting configuration manager.
        /// </summary>
        /// <param name="allowLocalOverrides">if set to <c>true</c> [allow local overrides].</param>
        /// <param name="fileNames">The file names.</param>
        public static void Configure(bool allowLocalOverrides, params string[] fileNames)
        {
            FlexibleConfiguration.Configure(FlexibleConfiguration.Load(allowLocalOverrides, fileNames));
        }

        /// <summary>
        /// Loads the provider factories from all the files. No overrides possible, first to be loaded remains!
        /// </summary>
        /// <param name="fileNames">The file names.</param>
        public static void LoadProviderFactories(params string[] fileNames)
        {
            foreach (string fileName in fileNames)
            {
                var configuration = OpenFile(fileName);
                var currentDataSet = ConfigurationManager.GetSection("system.data") as DataSet;

                string xml = configuration.GetSection("system.data").SectionInformation.GetRawXml();
                var mainDataSet = XDocument.Parse(xml);

                foreach (var provider in mainDataSet.Descendants("add"))
                {
                    if (currentDataSet.Tables[0].Rows.Find(provider.Attribute("invariant").Value) == null) // Add each invariant at most once. In case it was registered on the computer already.
                    {
                        currentDataSet.Tables[0].Rows.Add(provider.Attribute("name").Value,
                                                          provider.Attribute("description").Value,
                                                          provider.Attribute("invariant").Value,
                                                          provider.Attribute("type").Value);
                    }
                }
            }
        }

        /// <summary>
        /// Configures the specified flexible configuration to use a specific manager.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public static void Configure(IFlexibleConfiguration manager)
        {
            Validate.Is.NotNull(manager, "manager");
            FlexibleConfiguration.instance = manager;
        }
    }
}
