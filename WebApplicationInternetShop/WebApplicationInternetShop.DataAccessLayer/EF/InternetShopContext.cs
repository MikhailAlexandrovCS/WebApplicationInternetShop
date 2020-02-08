using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationInternetShop.DataAccessLayer.Entities;

namespace WebApplicationInternetShop.DataAccessLayer.EF
{
    public class InternetShopContext : IdentityDbContext<CustomerInfo>
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Item> Items { get; set; }


        public InternetShopContext(DbContextOptions db) : base(db)
        {

        }
    }
}
