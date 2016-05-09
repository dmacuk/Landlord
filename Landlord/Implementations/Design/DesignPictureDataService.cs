using Landlord.Interface;
using Landlord.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Landlord.Implementations.Design
{
    public class DesignPictureDataService : IPictureDataService
    {
        public void GetProperties(Action<List<Property>, Exception> action)
        {
            throw new NotImplementedException();
        }
    }
}