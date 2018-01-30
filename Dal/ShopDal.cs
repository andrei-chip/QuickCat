using QuickCat.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuickCat.Dal
{
    public class ShopDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Shop>().ToTable("Shops");
        }

        public DbSet<Shop> Shops { get; set; }
    }
}