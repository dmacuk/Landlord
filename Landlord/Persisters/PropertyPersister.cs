using Landlord.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

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

        public void Save(Property property)
        {
            if (property.Id == 0)
            {
                AddProperty(property);
            }
            else
            {
                UpdateProperty(property);
            }
        }

        private void AddProperty(Property property)
        {
        }

        private DbConnection GetConnection()
        {
            var providerFactory = DbProviderFactories.GetFactory("System.Data.VistaDB5");
            var connection = providerFactory.CreateConnection();
            if (connection == null) throw new Exception("No provider for System.Data.VistaDB5");
            connection.ConnectionString = @"Data Source=data\Landlord.vdb5";
            connection.Open();
            return connection;
        }

        private void UpdateProperty(Property property)
        {
        }
    }
}