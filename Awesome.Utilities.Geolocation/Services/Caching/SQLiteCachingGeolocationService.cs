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
    public class SQLiteCachingGeolocationService : BaseCachingGeolocationService
    {
        public SQLiteCachingGeolocationService(IGeolocationService decorated, ConnectionStringSettings connectionString)
            : base(decorated, connectionString)
        {
            var builder = new SQLiteConnectionStringBuilder(connectionString.ConnectionString);
            if (!File.Exists(builder.DataSource))
            {
                SQLiteConnection.CreateFile(builder.DataSource);
            }
            this.BaseSetup();
        }

        protected override bool TableExists(string tableName, IDbConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name=@TableName";
                var parameter = command.CreateParameter();
                parameter.ParameterName = "@TableName";
                parameter.Value = tableName;
                command.Parameters.Add(parameter);
                using (var reader = command.ExecuteReader())
                {
                    return reader.Read();
                }
            }
        }
    }
}
