using Landlord.Interface;
using Landlord.Model;
using System;
using System.Collections.Generic;

namespace Landlord.Implementations.Design
{
    public class DesignPropertyDataService : IPropertyDataService
    {
        public void GetProperties(Action<List<Property>, Exception> action)
        {
            var results = new List<Property>
            {
                new Property
                {
                    Address = new Address
                    {
                        Address1 = "Address1",
                        Address2 = "Address2",
                        Address3 = "Address3",
                        City = "City",
                        Postcode = "Postcode",
                        Country = "Country"
                    },
                    Hidden = false
                }
            };

            action(results, null);
        }

        public void Save(Property property)
        {
            //
        }
    }
}