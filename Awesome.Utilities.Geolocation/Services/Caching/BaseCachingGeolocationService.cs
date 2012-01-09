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
    //TODO The creation of database/table/insert need to be unit tested

    /// <summary>
    ///     A Caching base class. Subclass for each DB provider.
    /// </summary>
    public abstract class BaseCachingGeolocationService : IGeolocationService
    {
        private readonly IGeolocationService decorated;
        private readonly ConnectionStringSettings connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCachingGeolocationService"/> class.
        /// </summary>
        /// <param name="decorated">The decorated.</param>
        /// <param name="connectionString">The connection string.</param>
        protected BaseCachingGeolocationService(IGeolocationService decorated, ConnectionStringSettings connectionString)
        {
            this.decorated = decorated;
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Bases the setup.
        /// </summary>
        protected virtual void BaseSetup()
        {
            var factory = DbProviderFactories.GetFactory(this.connectionString.ProviderName);
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString.ConnectionString;
                connection.Open();
                if (!this.TableExists("AddressCache", connection))
                {
                    this.CreateCashingTable(connection);
                }
            }
        }

        /// <summary>
        /// Create the cashing table.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        protected abstract void CreateCashingTable(IDbConnection connection);

        /// <summary>
        /// returns whether the table exists.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        protected abstract bool TableExists(string tableName, IDbConnection connection);

        /// <summary>
        /// Gets the coordinates of the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public Coordinates GetCoordinates(string address)
        {
            var factory = DbProviderFactories.GetFactory(connectionString.ProviderName);
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString.ConnectionString;
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Longitude, Latitude FROM AddressCache WHERE Address = @Address";
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Address";
                    parameter.Value = address;
                    command.Parameters.Add(parameter);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Coordinates(
                                reader.GetDouble(0),
                                reader.GetDouble(1)
                            );
                        }
                    }
                }

                var coordinates = this.decorated.GetCoordinates(address);
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO AddressCache (Address, Longitude, Latitude) VALUES (@Address, @Longitude, @Latitude)";
                    var parameter1 = command.CreateParameter();
                    parameter1.ParameterName = "@Address";
                    parameter1.Value = address;
                    var parameter2 = command.CreateParameter();
                    parameter2.ParameterName = "@Longitude";
                    parameter2.Value = coordinates.Longitude;
                    var parameter3 = command.CreateParameter();
                    parameter3.ParameterName = "@Latitude";
                    parameter3.Value = coordinates.Latitude;
                    command.Parameters.Add(parameter1);
                    command.Parameters.Add(parameter2);
                    command.Parameters.Add(parameter3);
                    command.ExecuteNonQuery();
                }
                return coordinates;
            }
        }

        /// <summary>
        /// Gets all the address information of an address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public AddressInformation GetAddressInformation(string address)
        {
            return this.decorated.GetAddressInformation(address);
        }

        /// <summary>
        /// Gets all the address information for all results for a specific address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public AddressInformation[] GetAllAddressInformation(string address)
        {
            return this.decorated.GetAllAddressInformation(address);
        }
    }
}
