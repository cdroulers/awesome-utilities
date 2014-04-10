using System;
using System.Collections.Generic;
using System.Geolocation.Caching.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Geolocation.Services;
using Moq;
using System.Geolocation;
using System.Configuration;
using System.Data.SQLite;

namespace Awesome.Utilities.Test.Integration.Geolocation.Services.Caching
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenSQLiteCachingGeolocationService
    {
        private Mock<IGeolocationService> geoMock;
        private SQLiteCachingGeolocationService sqliteGeo;
        private ConnectionStringSettings settings;

        private AddressInformation[] results;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            FlexibleConfiguration.LoadProviderFactories(@"..\..\..\Awesome.Utilities.Test\Configuration\External.config");
        }

        [SetUp]
        public void SetUp()
        {
            SQLiteConnection.ClearAllPools();
            if (File.Exists("SQLiteCachingTest.s3db"))
            {
                File.Delete("SQLiteCachingTest.s3db");
            }
            this.settings = new ConnectionStringSettings("Test", "Data Source=SQLiteCachingTest.s3db;Synchronous=Off;Version=3;New=True;Pooling=True;Max Pool Size=1;", "System.Data.SQLite");

            results = new AddressInformation[]
            {
                new AddressInformation(new AddressInformationComponent[] { new AddressInformationComponent("LONG", "SHORT", new string[] { "type1", "type2" }) },
                        new Coordinates(15, 15),
                        "address",
                        "type")
            };

            geoMock = new Mock<IGeolocationService>();
            geoMock.Setup(x => x.GetAllAddressInformation(It.IsAny<string>())).Returns(results);
            this.sqliteGeo = null;
        }

        [Test]
        public void When_getting_all_info_Then_caches()
        {
            this.sqliteGeo = new SQLiteCachingGeolocationService(geoMock.Object, settings);
            var actual = this.sqliteGeo.GetAllAddressInformation("OH GOD WHY");

            var second = this.sqliteGeo.GetAllAddressInformation("OH GOD WHY");

            Assert.That(actual, Is.EqualTo(second));
            geoMock.Verify(x => x.GetAllAddressInformation(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void When_getting_coordinates_Then_caches()
        {
            this.sqliteGeo = new SQLiteCachingGeolocationService(geoMock.Object, settings);
            var actual = this.sqliteGeo.GetCoordinates("OH GOD WHY");

            var second = this.sqliteGeo.GetCoordinates("OH GOD WHY");

            Assert.That(actual, Is.EqualTo(second));
            geoMock.Verify(x => x.GetAllAddressInformation(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void When_expires_Then_clears()
        {
            this.sqliteGeo = new SQLiteCachingGeolocationService(geoMock.Object, settings);
            var actual = this.sqliteGeo.GetAllAddressInformation("OH GOD WHY");

            var second = this.sqliteGeo.GetAllAddressInformation("OH GOD WHY");

            using (Clock.Pause(Clock.UtcNow.AddYears(1)))
            {
                var third = this.sqliteGeo.GetAllAddressInformation("OH GOD WHY");

                Assert.That(actual, Is.EqualTo(third));

                geoMock.Verify(x => x.GetAllAddressInformation(It.IsAny<string>()), Times.Exactly(2));
            }
        }
    }
}
