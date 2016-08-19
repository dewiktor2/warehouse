using Hurtownia.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Hurtownia.DAL
{
    public class HurtowniaContext : DbContext
    {
        public HurtowniaContext()
            :base("DefaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Statistic> Statistics { get; set;}
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderShipping> Orders { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<Order> OrderItems { get; set; }
     //   public DbSet<Cart> Cards { get; set; }
 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<Hurtownia.Models.Report> Reports { get; set; }
    }
}