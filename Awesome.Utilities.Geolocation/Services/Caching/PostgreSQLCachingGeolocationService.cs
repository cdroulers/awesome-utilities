using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Npgsql;
using System.IO;
using System.Data.Common;
using System.Data;

namespace System.Geolocation.Services.Caching
{
    /// <summary>
    ///     A caching class for PostgreSQL
    /// </summary>
    public class PostgreSQLCachingGeolocationService : BaseCachingGeolocationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteCachingGeolocationService"/> class.
        /// </summary>
        /// <param name="decorated">The decorated.</param>
        /// <param name="connectionString">The connection string.</param>
        public PostgreSQLCachingGeolocationService(IGeolocationService decorated, ConnectionStringSettings connectionString)
            : base(decorated, connectionString)
        {
            var builder = new NpgsqlConnectionStringBuilder(connectionString.ConnectionString);
            string databaseName = ConnectionStringHelper.SafeDataDirectoryReplacement(builder.Database);
            builder.Database = null;
            using (var connection = new NpgsqlConnection(builder.ConnectionString))
            {
                connection.Open();
                var scalar = connection.ExecuteScalar<object>(string.Format("SELECT COUNT(*) FROM pg_catalog.pg_database WHERE datname='{0}';", databaseName));
                var exists = int.Parse(scalar.ToString()) > 0;
                if (!exists)
                {
                    connection.ExecuteNonQuery(string.Format("CREATE DATABASE \"{0}\";", databaseName));
                }
            }
            this.BaseSetup();
        }


        /// <summary>
        /// Create the caching table.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        protected override void CreateCachingTable(IDbConnection connection)
        {
            connection.ExecuteNonQuery("CREATE UNLOGGED TABLE IF NOT EXISTS AddressCache (Address VARCHAR(250) NOT NULL, Longitude DOUBLE PRECISION NOT NULL, Latitude DOUBLE PRECISION NOT NULL);");
        }

        /// <summary>
        /// returns whether the table exists.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        protected override bool TableExists(string tableName, IDbConnection connection)
        {
            // The validation is done in the create command;
            return false;
        }
    }
}
