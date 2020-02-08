using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationInternetShop.DataAccessLayer.EF;
using WebApplicationInternetShop.DataAccessLayer.Entities;
using WebApplicationInternetShop.DataAccessLayer.Interfaces;

namespace WebApplicationInternetShop.DataAccessLayer.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private InternetShopContext _db;
        private CustomerRepository _customerRepository;
        private ItemRepository _itemRepository;
        private OrderRepository _orderRepository;
        private OrderItemRepository _orderItemsRepository;

        private bool disposed = false;

        public EFUnitOfWork(InternetShopContext internetShopContext)
        {
            _db = internetShopContext;
        }

        public IRepository<Customer> Customers
        {
            get
            {
                if (_customerRepository == null)
                    _customerRepository = new CustomerRepository(_db);
                return _customerRepository;
            }
        }

        public IRepository<Item> Items
        {
            get
            {
                if (_itemRepository == null)
                    _itemRepository = new ItemRepository(_db);
                return _itemRepository;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new OrderRepository(_db);
                return _orderRepository;
            }
        }

        public IRepository<OrderItem> OrderItems
        {
            get
            {
                if (_orderItemsRepository == null)
                    _orderItemsRepository = new OrderItemRepository(_db);
                return _orderItemsRepository;
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}