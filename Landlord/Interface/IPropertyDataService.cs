using Landlord.Model;
using System;
using System.Collections.Generic;

namespace Landlord.Interface
{
    public interface IPropertyDataService
    {
        void GetProperties(Action<List<Property>, Exception> action);

        void Save(Property propertyVo);
    }
}