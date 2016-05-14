using Landlord.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Landlord.Persisters.Utils
{
    internal static class SqlUtils
    {
        public static string InsertAddress =
            "INSERT INTO Address (Address1, Address2, Address3, City, Postcode, Country, Hidden) " +
            "VALUES " +
            "(@Address1, @Address2, @Address3, @City, @Postcode, @Country, @Hidden);";

        public static string InsertProperty =
            "INSERT INTO Property (AddressId, Hidden) VALUES (@AddressId, @Hidden);";

        public static string LastIdentity =
            "SELECT @@IDENTITY";

        public static string UpdateAddress =
            "UPDATE Address SET " +
            "Address1 = @Address1, " +
            "Address2 = @Address2, " +
            "Address3 = @Address3, " +
            "City = @City, " +
            "Postcode = @Postcode, " +
            "Country = @Country, " +
            "Hidden = @Hidden " +
            "WHERE Id = @Id;";

        public static string UpdateProperty =
            "UPDATE Property SET " +
            "AddressId = @AddressId, " +
            "Hidden = @Hidden " +
            "WHERE Id = @Id;";

        public static Array GetAddressParameters(DbCommand cmd, Address address)
        {
            var parameters = new List<DbParameter>
            {
                CreateDbParameter(cmd, "@Id", DbType.String, address.Id),
                CreateDbParameter(cmd, "@Address1", DbType.String, address.Address1),
                CreateDbParameter(cmd, "@Address2", DbType.String, address.Address2),
                CreateDbParameter(cmd, "@Address3", DbType.String, address.Address3),
                CreateDbParameter(cmd, "@City", DbType.String, address.City),
                CreateDbParameter(cmd, "@Postcode", DbType.String, address.Postcode),
                CreateDbParameter(cmd, "@Country", DbType.String, address.Country),
                CreateDbParameter(cmd, "@Hidden", DbType.Boolean, address.Hidden)
            };
            return parameters.ToArray();
        }

        public static Array GetPropertyParameters(DbCommand cmd, Property property)
        {
            var parameters = new List<DbParameter>
            {
                CreateDbParameter(cmd, "@Id", DbType.Int64, property.Id),
                CreateDbParameter(cmd, "@AddressId", DbType.Int64, property.AddressId),
                CreateDbParameter(cmd, "@Hidden", DbType.Boolean, property.Hidden)
            };
            return parameters.ToArray();
        }

        private static DbParameter CreateDbParameter(DbCommand cmd, string name, DbType type, object value)
        {
            var parameter = cmd.CreateParameter();
            parameter.DbType = type;
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }
    }
}