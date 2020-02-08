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
    public class ItemRepository : IRepository<Item>
    {
        private InternetShopContext db;

        public ItemRepository(InternetShopContext context)
        {
            db = context;
        }

        public Task<IdentityResult> AddRoleToUserInDb(UserManager<CustomerInfo> userManager, string role, CustomerInfo customerUser)
        {
            throw new NotImplementedException();
        }

        public void Create(Item item)
        {
            db.Items.Add(item);
        }

        public Task<IdentityResult> Create(UserManager<CustomerInfo> userManager, CustomerInfo customerInfo, string password)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            var item = db.Items.Find(id);
            if (item != null)
                db.Items.Remove(item);
        }

        public Task<IdentityRole> FindRole(RoleManager<IdentityRole> roleManager, string role)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerInfo> FindUser(UserManager<CustomerInfo> userManager, string email)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetAllItems()
        {
            return db.Items.ToList();
        }

        public Item GetItem(Guid id)
        {
            return db.Items.Find(id);
        }

        public IEnumerable<Item> GetOrdersByPredicate(Func<Item, bool> predicate)
        {
            return db.Items.Where(predicate).ToList();
        }

        public void Update(Item item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
