using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data
{
    /// <summary>
    ///     Extension methods for IDbConnection
    /// </summary>
    public static class DbConnectionExtensions
    {
        private static void PrepareCommand(IDbCommand command, string text, object[] parameters)
        {
            if (parameters.Any())
            {
                var parameterNames = new List<string>();
                for (int i = 0; i < parameters.Length; i++)
                {
                    parameterNames.Add("@p" + i);

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@p" + i;
                    parameter.Value = parameters[i];
                    command.Parameters.Add(parameter);
                }
                text = string.Format(text, parameterNames.ToArray());
            }
            command.CommandText = text;
        }

        /// <summary>
        /// Executes the non query with the specific parameters
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="text">The text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(this IDbConnection connection, string text, params object[] parameters)
        {
            using (var command = connection.CreateCommand())
            {
                PrepareCommand(command, text, parameters);
                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Executes the query and returns a reader
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="text">The text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(this IDbConnection connection, string text, params object[] parameters)
        {
            using (var command = connection.CreateCommand())
            {
                PrepareCommand(command, text, parameters);
                return command.ExecuteReader();
            }
        }

        /// <summary>
        /// Executes the query and returns a Scalar
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="text">The text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static T ExecuteScalar<T>(this IDbConnection connection, string text, params object[] parameters)
        {
            using (var command = connection.CreateCommand())
            {
                PrepareCommand(command, text, parameters);
                return (T)Convert.ChangeType(command.ExecuteScalar(), typeof(T));
            }
        }
    }
}
