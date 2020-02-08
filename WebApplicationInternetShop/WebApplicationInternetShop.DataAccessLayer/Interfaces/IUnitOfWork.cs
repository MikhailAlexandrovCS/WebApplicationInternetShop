using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationInternetShop.DataAccessLayer.Entities;

namespace WebApplicationInternetShop.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Order> Orders { get; }
        IRepository<OrderItem> OrderItems { get; }
        IRepository<Item> Items { get; }
        IRepository<Customer> Customers { get; }
        void Save();
    }
}
