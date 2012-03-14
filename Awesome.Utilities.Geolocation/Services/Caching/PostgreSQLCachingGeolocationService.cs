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
        protected override void VerifyCachingTable(IDbConnection connection)
        {
            bool tableExists = false;
            using (var reader = connection.ExecuteReader("SELECT relname FROM pg_class WHERE relname = {0}", BaseCachingGeolocationService.TableName))
            {
                tableExists = reader.Read();
            }
            if (!tableExists)
            {
                connection.ExecuteNonQuery(
                    string.Format(@"CREATE UNLOGGED TABLE {0} (
    address VARCHAR(250) NOT NULL,
    formatted_address VARCHAR(250) NOT NULL,
    longitude DOUBLE PRECISION NOT NULL,
    latitude DOUBLE PRECISION NOT NULL,
    type VARCHAR(100) NOT NULL,
    components TEXT NOT NULL,
    updated_on TIMESTAMP NOT NULL,
    CONSTRAINT address_cache_pk PRIMARY KEY (address, formatted_address));",
                        BaseCachingGeolocationService.TableName));

                connection.ExecuteNonQuery(@"CREATE INDEX ""address_cache_address_asc"" ON address_cache (LOWER(address));");
            }
        }
    }
}
