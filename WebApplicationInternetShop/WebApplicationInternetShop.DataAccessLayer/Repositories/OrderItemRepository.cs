using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationInternetShop.DataAccessLayer.EF;
using WebApplicationInternetShop.DataAccessLayer.Entities;
using WebApplicationInternetShop.DataAccessLayer.Interfaces;

namespace WebApplicationInternetShop.DataAccessLayer.Repositories
{
    public class OrderItemRepository : IRepository<OrderItem>
    {
        private InternetShopContext db;

        public OrderItemRepository(InternetShopContext context)
        {
            db = context;
        }

        public Task<IdentityResult> AddRoleToUserInDb(UserManager<CustomerInfo> userManager, string role, CustomerInfo customerUser)
        {
            throw new NotImplementedException();
        }

        public void Create(OrderItem item)
        {
            db.OrderItems.Add(item);
        }

        public Task<IdentityResult> Create(UserManager<CustomerInfo> userManager, CustomerInfo customerInfo, string password)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            var orderItem = db.OrderItems.Find(id);
            if (orderItem != null)
                db.OrderItems.Remove(orderItem);
        }

        public Task<IdentityRole> FindRole(RoleManager<IdentityRole> roleManager, string role)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerInfo> FindUser(UserManager<CustomerInfo> userManager, string email)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItem> GetAllItems()
        {
            return db.OrderItems.ToList();
        }

        public OrderItem GetItem(Guid id)
        {
            return db.OrderItems.Find(id);
        }

        public IEnumerable<OrderItem> GetOrdersByPredicate(Func<OrderItem, bool> predicate)
        {
            return db.OrderItems.Where(predicate).ToList();
        }

        public void Update(OrderItem item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
