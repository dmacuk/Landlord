using Landlord.Model;
using Landlord.Persisters.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Landlord.Persisters
{
    public class PropertyPersister
    {
        public async Task<List<Property>> GetProperties()
        {
            var results = new List<Property>();
            using (var connection = SqlUtils.GetConnection())
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
            using (var conn = SqlUtils.GetConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = PropertySqlUtils.InsertAddress;
                    var parameters = PropertySqlUtils.GetAddressParameters(cmd, property.Address);
                    cmd.Parameters.AddRange(parameters);
                    await cmd.ExecuteNonQueryAsync();
                    cmd.CommandText = PropertySqlUtils.LastIdentity;
                    var addressId = await cmd.ExecuteScalarAsync();

                    // @Address1, @Address2, @Address3, @City, @Postcode, @Country
                    property.AddressId = (long)addressId;
                    cmd.CommandText = PropertySqlUtils.InsertProperty;
                    cmd.Parameters.Clear();
                    parameters = PropertySqlUtils.GetPropertyParameters(cmd, property);
                    cmd.Parameters.AddRange(parameters);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task UpdateProperty(Property property)
        {
            using (var conn = SqlUtils.GetConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = PropertySqlUtils.UpdateProperty;
                    var parameters = PropertySqlUtils.GetPropertyParameters(cmd, property);
                    cmd.Parameters.AddRange(parameters);
                    await cmd.ExecuteNonQueryAsync();

                    cmd.CommandText = PropertySqlUtils.UpdateAddress;
                    cmd.Parameters.Clear();
                    parameters = PropertySqlUtils.GetAddressParameters(cmd, property.Address);
                    cmd.Parameters.AddRange(parameters);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}