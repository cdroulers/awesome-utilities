using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;

namespace System.Geolocation.Services.Caching
{
    /// <summary>
    ///     A Caching base class. Subclass for each DB provider.
    /// </summary>
    public abstract class BaseCachingGeolocationService : IGeolocationService
    {
        private readonly IGeolocationService decorated;
        private readonly ConnectionStringSettings connectionString;
        private readonly TimeSpan MaximumLifeTime;

        protected const string TableName = "address_cache";

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCachingGeolocationService"/> class.
        /// </summary>
        /// <param name="decorated">The decorated.</param>
        /// <param name="connectionString">The connection string.</param>
        protected BaseCachingGeolocationService(IGeolocationService decorated, ConnectionStringSettings connectionString, TimeSpan? maximumLifeTime = null)
        {
            this.decorated = decorated;
            this.connectionString = connectionString;
            this.MaximumLifeTime = maximumLifeTime.GetValueOrDefault(TimeSpan.FromDays(30));
        }

        protected IDbConnection GetConnection()
        {
            var factory = DbProviderFactories.GetFactory(this.connectionString.ProviderName);
            var connection = factory.CreateConnection();
            connection.ConnectionString = this.connectionString.ConnectionString;
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Bases the setup.
        /// </summary>
        protected virtual void BaseSetup()
        {
            using (var connection = this.GetConnection())
            {
                this.VerifyCachingTable(connection);
            }
        }

        /// <summary>
        /// Create the caching table.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        protected abstract void VerifyCachingTable(IDbConnection connection);

        /// <summary>
        /// Gets the coordinates of the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public Coordinates GetCoordinates(string address)
        {
            return this.GetAddressInformation(address).Coordinates;
        }

        private static AddressInformationComponent[] GetComponents(string components)
        {
            string[] parts = components.Split('\n');
            var results = new List<AddressInformationComponent>();
            foreach (string part in parts)
            {
                var subParts = part.Split('&');
                results.Add(new AddressInformationComponent(subParts[0], subParts[1], subParts[2].Split(';')));
            }
            return results.ToArray();
        }

        private static string GetComponents(AddressInformationComponent[] components)
        {
            return string.Join("\n", components.Select(c => c.LongName + "&" + c.ShortName + "&" + string.Join(";", c.Types)));
        }

        /// <summary>
        /// Gets all the address information of an address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public AddressInformation GetAddressInformation(string address)
        {
            var addresses = this.GetAllAddressInformation(address);
            addresses = GeolocationServiceBase.CheckMultipleResults(address, addresses, a => a);
            return addresses.First();
        }

        /// <summary>
        /// Gets all the address information for all results for a specific address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public AddressInformation[] GetAllAddressInformation(string address)
        {
            using (var connection = this.GetConnection())
            {
                bool deleteValues = false;
                using (var reader = connection.ExecuteReader("SELECT formatted_address, longitude, latitude, type, components, updated_on FROM address_cache WHERE LOWER(address) = {0}", address.ToLowerInvariant()))
                {
                    var addresses = new List<AddressInformation>();
                    while (reader.Read())
                    {
                        var expires = reader.GetDateTime(5) + this.MaximumLifeTime;

                        var info = new AddressInformation(
                            GetComponents(reader.GetString(4)),
                            new Coordinates(
                                reader.GetDouble(1),
                                reader.GetDouble(2)
                            ),
                            reader.GetString(0),
                            reader.GetString(3)
                        );
                        addresses.Add(info);

                        if (Clock.UtcNow > expires)
                        {
                            deleteValues = true;
                        }
                    }

                    if (addresses.Any() && !deleteValues)
                    {
                        return addresses.ToArray();
                    }

                    if (deleteValues)
                    {
                        foreach (var info in addresses)
                        {
                            connection.ExecuteNonQuery("DELETE FROM address_cache WHERE address = {0} AND formatted_address = {1}", address, info.FormattedAddress);
                        }
                    }
                }

                var results = this.decorated.GetAllAddressInformation(address);

                foreach (var result in results)
                {
                    connection.ExecuteNonQuery("INSERT INTO address_cache (address, formatted_address, longitude, latitude, type, components, updated_on) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6})",
                        address,
                        result.FormattedAddress,
                        result.Coordinates.Longitude,
                        result.Coordinates.Latitude,
                        result.Type,
                        GetComponents(result.Components),
                        Clock.UtcNow);
                }
                return results;
            }
        }
    }
}
