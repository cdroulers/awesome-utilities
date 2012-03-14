using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Data.Common;
using System.Data;

namespace System.Geolocation.Services.Caching
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
        protected override void CreateCachingTable(IDbConnection connection)
        {
            connection.ExecuteNonQuery("CREATE TABLE AddressCache (Address NVARCHAR(250) NOT NULL, Longitude DOUBLE PRECISION NOT NULL, Latitude DOUBLE PRECISION NOT NULL);");
        }

        /// <summary>
        /// returns whether the table exists.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        protected override bool TableExists(string tableName, IDbConnection connection)
        {
            using (var reader = connection.ExecuteReader("SELECT name FROM sqlite_master WHERE type = 'table' AND name = @TableName", tableName))
            {
                return reader.Read();
            }
        }
    }
}
