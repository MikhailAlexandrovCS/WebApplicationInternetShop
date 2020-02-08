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
    public class CustomerRepository : IRepository<Customer>
    {
        private InternetShopContext db;

        public CustomerRepository(InternetShopContext context)
        {
            db = context;
        }

        public async Task<IdentityResult> Create( 
            UserManager<CustomerInfo> userManager, 
            CustomerInfo customerInfo, string password)
        {
            return await userManager.CreateAsync(customerInfo, password);
        }

        public async Task<CustomerInfo> FindUser(UserManager<CustomerInfo> userManager, string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public void Delete(Guid id)
        {
            var customer = db.Customers.Find(id);
            if (customer != null)
                db.Customers.Remove(customer);
        }

        public async Task<IdentityResult> AddRoleToUserInDb(UserManager<CustomerInfo> userManager, string role,
           CustomerInfo customerUser)
        {
            var res = await userManager.AddToRoleAsync(customerUser, role);
            return res;
        }

        public async Task<IdentityRole> FindRole(RoleManager<IdentityRole> roleManager, string role)
        {
            return await roleManager.FindByNameAsync(role);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public IEnumerable<Customer> GetAllItems()
        {
            return db.Customers.ToList();
        }

        public Customer GetItem(Guid id)
        {
            return db.Customers.Find(id);
        }

        public IEnumerable<Customer> GetOrdersByPredicate(Func<Customer, bool> predicate)
        {
            return db.Customers.Where(predicate).ToList();
        }

        public void Update(Customer item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Create(Customer item)
        {
            db.Add(item);
        }
    }
}
