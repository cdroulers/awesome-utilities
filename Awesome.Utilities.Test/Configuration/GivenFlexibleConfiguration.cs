using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Configuration;
using System.Data.Common;

namespace Awesome.Utilities.Test.Configuration
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenFlexibleConfiguration
    {
        [Test]
        public void When_loading_just_one_file_with_no_override_Then_works()
        {
            var result = FlexibleConfiguration.Load(false, "../../Configuration/External.config");

            Assert.That(result.AppSettings, Has.Count.EqualTo(2));
            Assert.That(result.AppSettings["Setting1"], Is.EqualTo("Setting1Value"));
            Assert.That(result.AppSettings["Setting2"], Is.EqualTo("Setting2Value"));

            Assert.That(result.ConnectionStrings, Has.Count.EqualTo(1));
            Assert.That(result.ConnectionStrings["String1"], Is.EqualTo(new ConnectionStringSettings("String1", "Data Source=String1.s3db;", "System.Data.SQLite")));
        }

        [Test]
        public void When_loading_two_files_Then_works()
        {
            var result = FlexibleConfiguration.Load(false, "../../Configuration/External.config", "../../Configuration/External2.config");

            Assert.That(result.AppSettings, Has.Count.EqualTo(3));
            Assert.That(result.AppSettings["Setting1"], Is.EqualTo("Setting1Value"));
            Assert.That(result.AppSettings["Setting2"], Is.EqualTo("Setting2ValueExternal2"));
            Assert.That(result.AppSettings["Setting3"], Is.EqualTo("Setting3Value"));

            Assert.That(result.ConnectionStrings, Has.Count.EqualTo(2));
            Assert.That(result.ConnectionStrings["String1"], Is.EqualTo(new ConnectionStringSettings("String1", "Data Source=String1External2.s3db;", "System.Data.SQLite")));
            Assert.That(result.ConnectionStrings["String2"], Is.EqualTo(new ConnectionStringSettings("String2", "Data Source=String2.s3db;", "System.Data.SQLite")));
        }

        [Test]
        public void When_loading_two_files_and_allow_local_Then_works()
        {
            var result = FlexibleConfiguration.Load(true, "../../Configuration/External.config", "../../Configuration/External2.config");

            Assert.That(result.AppSettings, Has.Count.EqualTo(4));
            Assert.That(result.AppSettings["Setting1"], Is.EqualTo("Setting1Value"));
            Assert.That(result.AppSettings["Setting2"], Is.EqualTo("Setting2ValueExternal2"));
            Assert.That(result.AppSettings["Setting3"], Is.EqualTo("Setting3Local"));
            Assert.That(result.AppSettings["Setting4"], Is.EqualTo("Setting4Value"));

            Assert.That(result.ConnectionStrings, Has.Count.EqualTo(3));
            Assert.That(result.ConnectionStrings["String1"], Is.EqualTo(new ConnectionStringSettings("String1", "Data Source=String1External2.s3db;", "System.Data.SQLite")));
            Assert.That(result.ConnectionStrings["String2"], Is.EqualTo(new ConnectionStringSettings("String2", "Data Source=String2Local.s3db;", "System.Data.SQLite")));
            Assert.That(result.ConnectionStrings["String3"], Is.EqualTo(new ConnectionStringSettings("String3", "Data Source=String3.s3db;", "System.Data.SQLite")));
        }

        [Test]
        public void When_loading_provider_factories_Then_works()
        {
            FlexibleConfiguration.LoadProviderFactories("../../Configuration/External.config", "../../Configuration/External2.config");

            Assert.That(DbProviderFactories.GetFactory("System.Data.SQLite"), Is.Not.Null);
            Assert.That(DbProviderFactories.GetFactory("System.Data.PostgreSQL"), Is.Not.Null);
        }
    }
}
