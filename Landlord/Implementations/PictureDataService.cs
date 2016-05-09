using Landlord.Interface;
using Landlord.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Landlord.Implementations
{
    public class PictureDataService : IPictureDataService
    {
        public async void GetProperties(Action<List<Property>, Exception> action)
        {
            using (var ctx = new LandlordEntities())
            {
                var properties = await ctx.Properties.ToListAsync();
                action(properties, null);
                ;
            }
        }
    }
}