using System;
using System.Collections.Generic;
using System.Data;
using System.Geolocation.Caching.PostgreSQL;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Geolocation.Services;
using Moq;
using System.Geolocation;
using System.Configuration;
using Npgsql;

namespace Awesome.Utilities.Test.Integration.Geolocation.Services.Caching
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenPostgreSQLCachingGeolocationService
    {
        private Mock<IGeolocationService> geoMock;
        private PostgreSQLCachingGeolocationService postgresGeo;
        private ConnectionStringSettings settings;

        private AddressInformation[] results;

        private bool canTest = true;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            FlexibleConfiguration.LoadProviderFactories(@"..\..\..\Awesome.Utilities.Test\Configuration\External.config");
        }

        [SetUp]
        public void SetUp()
        {
            settings = FlexibleConfiguration.Manager.ConnectionStrings["PostgreSQLCachingTest"];
            if (settings == null)
            {
                canTest = false;
                return;
            }
            using (var connection = new NpgsqlConnection(settings.ConnectionString))
            {
                connection.Open();
                var scalar = connection.ExecuteScalar<object>(string.Format("SELECT COUNT(*) FROM pg_catalog.pg_database WHERE datname='{0}';", "Geolocation"));
                var exists = int.Parse(scalar.ToString()) > 0;
                if (exists)
                {
                    connection.ExecuteNonQuery(string.Format("select pg_terminate_backend(procpid) from pg_stat_activity where datname='{0}';", "Geolocation"));
                    connection.ExecuteNonQuery(string.Format("DROP DATABASE \"{0}\";", "Geolocation"));
                }
            }

            results = new AddressInformation[]
            {
                new AddressInformation(new AddressInformationComponent[] { new AddressInformationComponent("LONG", "SHORT", new string[] { "type1", "type2" }) },
                        new Coordinates(15, 15),
                        "address",
                        "type")
            };

            geoMock = new Mock<IGeolocationService>();
            geoMock.Setup(x => x.GetAllAddressInformation(It.IsAny<string>())).Returns(results);
            settings = new ConnectionStringSettings(settings.Name, settings.ConnectionString + "Database=Geolocation;", settings.ProviderName);
            this.postgresGeo = null;
        }

        [Test]
        public void When_getting_all_info_Then_caches()
        {
            if (!this.canTest)
            {
                return;
            }
            this.postgresGeo = new PostgreSQLCachingGeolocationService(geoMock.Object, this.settings);

            var actual = this.postgresGeo.GetAllAddressInformation("OH GOD WHY");

            var second = this.postgresGeo.GetAllAddressInformation("OH GOD WHY");

            Assert.That(actual, Is.EqualTo(second));
            geoMock.Verify(x => x.GetAllAddressInformation(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void When_getting_all_info_Then_caches_in_right_order()
        {
            if (!this.canTest)
            {
                return;
            }
            var multipleResults = new AddressInformation[]
            {
                new AddressInformation(new AddressInformationComponent[] { new AddressInformationComponent("LONG", "SHORT", new string[] { "type1", "type2" }) },
                        new Coordinates(15, 15),
                        "address",
                        "type"),
                new AddressInformation(new AddressInformationComponent[] { new AddressInformationComponent("ALONG", "ASHORT", new string[] { "type1", "type2" }) },
                        new Coordinates(30, 30),
                        "aaddress",
                        "type")
            };
            geoMock.Setup(x => x.GetAllAddressInformation("Boston, Massachusetts")).Returns(multipleResults);
            this.postgresGeo = new PostgreSQLCachingGeolocationService(geoMock.Object, this.settings);

            var actual = this.postgresGeo.GetAllAddressInformation("Boston, Massachusetts");

            var second = this.postgresGeo.GetAllAddressInformation("Boston, Massachusetts");

            Assert.That(second, Is.EqualTo(actual));
            geoMock.Verify(x => x.GetAllAddressInformation(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void When_getting_coordinates_Then_caches()
        {
            if (!this.canTest)
            {
                return;
            }
            this.postgresGeo = new PostgreSQLCachingGeolocationService(geoMock.Object, this.settings);

            var actual = this.postgresGeo.GetCoordinates("OH GOD WHY");

            var second = this.postgresGeo.GetCoordinates("OH GOD WHY");

            Assert.That(actual, Is.EqualTo(second));
            geoMock.Verify(x => x.GetAllAddressInformation(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void When_expires_Then_clears()
        {
            if (!this.canTest)
            {
                return;
            }
            this.postgresGeo = new PostgreSQLCachingGeolocationService(geoMock.Object, this.settings);

            var actual = this.postgresGeo.GetAllAddressInformation("OH GOD WHY");

            this.postgresGeo.GetAllAddressInformation("OH GOD WHY");

            using (Clock.Pause(Clock.UtcNow.AddYears(1)))
            {
                var third = this.postgresGeo.GetAllAddressInformation("OH GOD WHY");

                Assert.That(actual, Is.EqualTo(third));

                geoMock.Verify(x => x.GetAllAddressInformation(It.IsAny<string>()), Times.Exactly(2));
            }
        }
    }
}
