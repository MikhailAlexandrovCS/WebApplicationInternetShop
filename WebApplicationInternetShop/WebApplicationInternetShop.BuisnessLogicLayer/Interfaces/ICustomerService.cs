using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplicationInternetShop.BuisnessLogicLayer.DataTransferObject;
using WebApplicationInternetShop.DataAccessLayer.Entities;

namespace WebApplicationInternetShop.BuisnessLogicLayer.Interfaces
{
    public interface ICustomerService
    {
        Guid CreateCustomer(CustomerDataTransferObject customer);
        void UpdateCustomer(Guid customerId, string name, string code, string adress,
            double? discount);
        void UpdateCustomerAfterJsonPatchDocument(CustomerDataTransferObject customerModified);
        void DeleteCustomer(Guid id);
        CustomerDataTransferObject GetCustomer(Guid id);
        IEnumerable<CustomerDataTransferObject> GetAllCustomers();
        void Dispose();
        Task<IdentityResult> AddCustomerUser(UserManager<CustomerInfo> userManager,
            CustomerInfoDataTransferObject customerUserDataTransferObject,
            string password);
        CustomerInfoDataTransferObject FindUserByUsingUserNameOrEmail(string email,
            UserManager<CustomerInfo> userManager);
        Task<IdentityResult> AddRoleToUser(CustomerInfoDataTransferObject customerUserDataTransferObject,
            string role, UserManager<CustomerInfo> userManager);
        Task<IdentityRole> FindRoleByName(RoleManager<IdentityRole> roleManager, string role);
        Task<bool> CheckIsUserInRole(UserManager<CustomerInfo> userManager,
            CustomerInfoDataTransferObject customerUserDataTransferObject,
            string role);
    }
}