using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using System.Geolocation.Services;
using System.Geolocation.Services.Caching;
using System.Configuration;
using System.IO;
using System.Geolocation;
using System.Data.SQLite;

namespace Awesome.Utilities.Test.Geolocation.Services.Caching
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenSQLiteCachingGeolocationService
    {
        private Mock<IGeolocationService> geo;
        private ConnectionStringSettings settings;

        [SetUp]
        public void SetUp()
        {
            SQLiteConnection.ClearAllPools();
            if (File.Exists("Test.s3db"))
            {
                File.Delete("Test.s3db");
            }

            this.geo = new Mock<IGeolocationService>();
            this.settings = new ConnectionStringSettings("Wat", "Data Source=Test.s3db;Synchronous=Off;Version=3;New=True;Pooling=True;Max Pool Size=1;", "System.Data.SQLite");
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void When_setting_up_caching_Then_creates_file_and_table()
        {
            var result = new SQLiteCachingGeolocationService(this.geo.Object, this.settings);

            Assert.That(File.Exists("Test.s3db"), Is.True);
        }

        [Test]
        public void When_getting_coordinates_Then_caches()
        {
            this.geo.Setup(x => x.GetCoordinates(It.IsAny<string>())).Returns(new Coordinates(15, 15));
            var result = new SQLiteCachingGeolocationService(this.geo.Object, this.settings);

            var result1 = result.GetCoordinates("Boston, MA, USA");
            var result2 = result.GetCoordinates("Boston, MA, USA");

            Assert.That(result1, Is.EqualTo(result2));

            this.geo.Verify(x => x.GetCoordinates(It.IsAny<string>()), Times.Once());
        }
    }
}
