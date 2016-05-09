using Landlord.Interface;
using Landlord.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Landlord.Implementations
{
    public class PropertyDataService : IPropertyDataService
    {
        public async void GetProperties(Action<List<Property>, Exception> action)
        {
            using (var ctx = new LandlordEntities())
            {
                var properties = await ctx.Properties.ToListAsync();
                var vos = new List<Property>();
                foreach (var property in properties)
                {
                    //                    var address = property.Addresses.FirstOrDefault();
                    //                    if (property.Addresses.Count > 0)
                    //                    {
                    //                        var addresses = property.Addresses.ToList();
                    //                        if (addresses.Count > 0)
                    //                        {
                    //                            address = addresses[0];
                    //                        }
                    //                    }
                    vos.Add(property);
                }
                action(vos, null);
            }
        }

        public void Save(Property propertyVo)
        {
            //            using (var ctx = new LandlordEntities())
            //            {
            //                var property = propertyVo.Property;
            //                if (property.Id == 0)
            //                {
            //                    ctx.Properties.Add(property);
            //                }
            //
            //                await ctx.SaveChangesAsync();
            //
            //                var address = propertyVo.Address;
            //                address.Property = property;
            //
            //                var addresses = ctx.Addresses;
            //                if (address.Id == 0)
            //                {
            //                    addresses.Add(address);
            //                }
            //                else
            //                {
            //                    addresses.Attach(address);
            //                }
            //
            //                await ctx.SaveChangesAsync();
        }
    }
}