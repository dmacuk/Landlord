﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Landlord.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LandlordEntities : DbContext
    {
        public LandlordEntities()
            : base("name=LandlordEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Furniture> Furnitures { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Tennant> Tennants { get; set; }
    }
}
