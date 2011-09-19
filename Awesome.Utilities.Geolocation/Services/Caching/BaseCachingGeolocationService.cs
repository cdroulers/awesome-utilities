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
    public abstract class BaseCachingGeolocationService : IGeolocationService
    {
        private readonly IGeolocationService decorated;
        private readonly ConnectionStringSettings connectionString;

        protected BaseCachingGeolocationService(IGeolocationService decorated, ConnectionStringSettings connectionString)
        {
            this.decorated = decorated;
            this.connectionString = connectionString;
        }

        protected virtual void BaseSetup()
        {
            var factory = DbProviderFactories.GetFactory(this.connectionString.ProviderName);
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString.ConnectionString;
                connection.Open();
                if (!this.TableExists("AddressCache", connection))
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "CREATE TABLE AddressCache (Address NVARCHAR(250) NOT NULL, Longitude DOUBLE PRECISION NOT NULL, Latitude DOUBLE PRECISION NOT NULL);";
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        protected abstract bool TableExists(string tableName, IDbConnection connection);

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
    }
}
