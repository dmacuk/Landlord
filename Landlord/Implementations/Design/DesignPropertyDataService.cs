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
            throw new NotImplementedException();
        }

        public void GetProperties(Action action)
        {
            //
        }

        public void Save(Property property)
        {
            //
        }
    }
}