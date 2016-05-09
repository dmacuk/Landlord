using Landlord.Interface;
using Landlord.Model;
using Landlord.VOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Landlord.Implementations
{
    public class PropertyDataService : IPropertyDataService
    {
        public async void GetProperties(Action<List<PropertyVo>, Exception> action)
        {
            using (var ctx = new LandlordEntities())
            {
                var properties = await ctx.Properties.ToListAsync();
                var vos = new List<PropertyVo>();
                foreach (var property in properties)
                {
                    var address = property.Addresses.FirstOrDefault();
                    //                    if (property.Addresses.Count > 0)
                    //                    {
                    //                        var addresses = property.Addresses.ToList();
                    //                        if (addresses.Count > 0)
                    //                        {
                    //                            address = addresses[0];
                    //                        }
                    //                    }
                    vos.Add(new PropertyVo(property, address));
                }
                action(vos, null);
                ;
            }
        }
    }
}