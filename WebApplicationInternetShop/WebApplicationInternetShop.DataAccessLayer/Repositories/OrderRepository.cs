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
    public class OrderRepository : IRepository<Order>
    {
        private InternetShopContext db;

        public OrderRepository(InternetShopContext context)
        {
            db = context;
        }

        public Task<IdentityResult> AddRoleToUserInDb(UserManager<CustomerInfo> userManager, string role, CustomerInfo customerUser)
        {
            throw new NotImplementedException();
        }

        public void Create(Order item)
        {
            db.Orders.Add(item);
        }

        public Task<IdentityResult> Create(UserManager<CustomerInfo> userManager, CustomerInfo customerInfo, string password)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            var order = db.Orders.Find(id);
            if (order != null)
                db.Orders.Remove(order);
        }

        public Task<IdentityRole> FindRole(RoleManager<IdentityRole> roleManager, string role)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerInfo> FindUser(UserManager<CustomerInfo> userManager, string email)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAllItems()
        {
            return db.Orders.ToList();
        }

        public Order GetItem(Guid id)
        {
            return db.Orders.Find(id);
        }

        public IEnumerable<Order> GetOrdersByPredicate(Func<Order, bool> predicate)
        {
            return db.Orders.Where(predicate).ToList();
        }

        public void Update(Order item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
