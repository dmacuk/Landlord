using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Landlord.Model;
using Landlord.Persisters.Utils;

namespace Landlord.Persisters
{
    public class PropertyPersister
    {
        public async Task<List<Property>> GetProperties()
        {
            var results = new List<Property>();
            using (var connection = GetConnection())
            {
                using (var propertyCmd = connection.CreateCommand())
                {
                    propertyCmd.CommandText = "SELECT * FROM property";
                    using (var propertyRdr = await propertyCmd.ExecuteReaderAsync())
                    {
                        while (propertyRdr.Read())
                        {
                            var addressId = propertyRdr["AddressId"];
                            using (var addressCmd = connection.CreateCommand())
                            {
                                addressCmd.CommandText = "SELECT * FROM Address WHERE id = " + addressId;
                                using (var addressRdr = await addressCmd.ExecuteReaderAsync())
                                {
                                    while (addressRdr.Read())
                                    {
                                        results.Add(new Property(propertyRdr, new Address(addressRdr)));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return results;
        }

        public async Task Save(Property property)
        {
            if (property.Id == 0)
            {
                await AddProperty(property);
            }
            else
            {
                await UpdateProperty(property);
            }
        }

        private static async Task AddProperty(Property property)
        {
            using (var conn = GetConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = SqlUtils.InsertAddress;
                    var parameters = SqlUtils.GetAddressParameters(cmd, property.Address);
                    cmd.Parameters.AddRange(parameters);
                    await cmd.ExecuteNonQueryAsync();
                    cmd.CommandText = SqlUtils.LastIdentity;
                    var addressId = await cmd.ExecuteScalarAsync();

                    // @Address1, @Address2, @Address3, @City, @Postcode, @Country
                    property.AddressId = (long)addressId;
                    cmd.CommandText = SqlUtils.InsertProperty;
                    cmd.Parameters.Clear();
                    parameters = SqlUtils.GetPropertyParameters(cmd, property);
                    cmd.Parameters.AddRange(parameters);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private static DbConnection GetConnection()
        {
            var providerFactory = DbProviderFactories.GetFactory("System.Data.VistaDB5");
            var connection = providerFactory.CreateConnection();
            if (connection == null) throw new Exception("No provider for System.Data.VistaDB5");
            connection.ConnectionString = @"Data Source=data\Landlord.vdb5";
            connection.Open();
            return connection;
        }

        private async Task UpdateProperty(Property property)
        {
            using (var conn = GetConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = SqlUtils.UpdateProperty;
                    var parameters = SqlUtils.GetPropertyParameters(cmd, property);
                    cmd.Parameters.AddRange(parameters);
                    await cmd.ExecuteNonQueryAsync();

                    cmd.CommandText = SqlUtils.UpdateAddress;
                    cmd.Parameters.Clear();
                    parameters = SqlUtils.GetAddressParameters(cmd, property.Address);
                    cmd.Parameters.AddRange(parameters);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}