using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Geolocation.Services;
using System.Geolocation.Services.Caching;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace System.Geolocation.Caching.SQLite
{
    /// <summary>
    ///     A caching class for SQLite
    /// </summary>
    public class SQLiteCachingGeolocationService : BaseCachingGeolocationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteCachingGeolocationService"/> class.
        /// </summary>
        /// <param name="decorated">The decorated.</param>
        /// <param name="connectionString">The connection string.</param>
        public SQLiteCachingGeolocationService(IGeolocationService decorated, ConnectionStringSettings connectionString)
            : base(decorated, connectionString)
        {
            var builder = new SQLiteConnectionStringBuilder(connectionString.ConnectionString);
            string dataSource = ConnectionStringHelper.SafeDataDirectoryReplacement(builder.DataSource);
            if (!File.Exists(dataSource))
            {
                SQLiteConnection.CreateFile(dataSource);
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
            using (var reader = connection.ExecuteReader("SELECT name FROM sqlite_master WHERE type = 'table' AND name = {0}", BaseCachingGeolocationService.TableName))
            {
                tableExists = reader.Read();
            }
            if (!tableExists)
            {
                connection.ExecuteNonQuery(
                    string.Format(@"CREATE TABLE {0} (
    address NVARCHAR(250) NOT NULL,
    formatted_address NVARCHAR(250) NOT NULL,
    longitude DOUBLE PRECISION NOT NULL,
    latitude DOUBLE PRECISION NOT NULL,
    type NVARCHAR(100) NOT NULL,
    components TEXT NOT NULL,
    updated_on TIMESTAMP NOT NULL,
    CONSTRAINT address_cache_pk PRIMARY KEY (address, formatted_address));",
                        BaseCachingGeolocationService.TableName));
            }
        }
    }
}
