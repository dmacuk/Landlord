using Landlord.Interface;
using Landlord.Model;
using Landlord.Persisters;
using System;
using System.Collections.Generic;

namespace Landlord.Implementations
{
    public class PropertyDataService : IPropertyDataService
    {
        private readonly PropertyPersister _persister = new PropertyPersister();

        public async void GetProperties(Action<List<Property>, Exception> action)
        {
            var results = await _persister.GetProperties();
            action(results, null);
        }

        public async void Save(Property property)
        {
            await _persister.Save(property);
        }
    }
}