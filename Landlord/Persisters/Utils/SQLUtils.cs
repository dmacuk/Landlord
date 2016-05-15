using System;
using System.Data;
using System.Data.Common;

namespace Landlord.Persisters.Utils
{
    internal class SqlUtils
    {
        public static DbConnection GetConnection()
        {
            var providerFactory = DbProviderFactories.GetFactory("System.Data.VistaDB5");
            var connection = providerFactory.CreateConnection();
            if (connection == null) throw new Exception("No provider for System.Data.VistaDB5");
            connection.ConnectionString = @"Data Source=data\Landlord.vdb5";
            connection.Open();
            return connection;
        }

        internal static DbParameter CreateDbParameter(DbCommand cmd, string name, DbType type, object value)
        {
            var parameter = cmd.CreateParameter();
            parameter.DbType = type;
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }
    }
}