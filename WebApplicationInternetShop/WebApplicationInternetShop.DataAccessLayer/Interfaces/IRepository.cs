using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplicationInternetShop.DataAccessLayer.Entities;

namespace WebApplicationInternetShop.DataAccessLayer.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IdentityResult> Create(UserManager<CustomerInfo> userManager,
            CustomerInfo customerInfo, string password);
        Task<CustomerInfo> FindUser(UserManager<CustomerInfo> userManager, string email);
        Task<IdentityResult> AddRoleToUserInDb(UserManager<CustomerInfo> userManager, string role,
           CustomerInfo customerUser);
        Task<IdentityRole> FindRole(RoleManager<IdentityRole> roleManager, string role);
        void Create(T item);
        void Update(T item);
        void Delete(Guid id);
        IEnumerable<T> GetAllItems();
        T GetItem(Guid id);
        IEnumerable<T> GetOrdersByPredicate(Func<T, bool> predicate);
    }
}
