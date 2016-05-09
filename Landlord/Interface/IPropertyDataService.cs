using Landlord.VOs;
using System;
using System.Collections.Generic;

namespace Landlord.Interface
{
    public interface IPropertyDataService
    {
        void GetProperties(Action<List<PropertyVo>, Exception> action);
    }
}