﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WMS.BusinessLogic.DataModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WarehouseEntities : DbContext
    {
        public WarehouseEntities()
            : base("name=WarehouseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Cell> Cells { get; set; }
        public DbSet<Consignment> Consignments { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<GoodsInCell> GoodsInCells { get; set; }
        public DbSet<LoginedUser> LoginedUsers { get; set; }
        public DbSet<UserCart> UserCarts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
    }
}
