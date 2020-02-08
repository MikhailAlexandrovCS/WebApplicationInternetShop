using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using WebApplicationInternetShop.BuisnessLogicLayer.DataTransferObject;
using WebApplicationInternetShop.BuisnessLogicLayer.Infrastructure;
using WebApplicationInternetShop.BuisnessLogicLayer.Interfaces;
using WebApplicationInternetShop.DataAccessLayer.Entities;
using WebApplicationInternetShop.DataAccessLayer.Interfaces;

namespace WebApplicationInternetShop.BuisnessLogicLayer.Services
{
    public class CustomerService : ICustomerService
    {
        private IUnitOfWork _database { get; set; }

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }

        public Guid CreateCustomer(CustomerDataTransferObject customer)
        {
            Guid Id = Guid.NewGuid();
            _database.Customers.Create(new Customer
            {
                Id = Id,
                Name = customer.Name,
                Code = customer.Code,
                Adress = customer.Adress,
                Discount = customer.Discount
            });
            _database.Save();
            return Id;
        }

        public void UpdateCustomer(Guid customerId, string name, string code, string adress,
            double? discount)
        {
            var customerModified = _database.Customers.GetItem(customerId);
            if (customerModified.Name != name)
                customerModified.Name = name;
            if (customerModified.Code != code)
                customerModified.Code = code;
            if (customerModified.Adress != adress)
                customerModified.Adress = adress;
            if (customerModified.Discount != discount)
                customerModified.Discount = discount;
            _database.Customers.Update(customerModified);
            _database.Save();
        }

        public void DeleteCustomer(Guid id)
        {
            var customer = _database.Customers.GetItem(id);
            _database.Customers.Delete(customer.Id);
            _database.Save();
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        public CustomerDataTransferObject GetCustomer(Guid id)
        {
            var customer = _database.Customers.GetItem(id);
            return new CustomerDataTransferObject
            {
                Id = customer.Id,
                Name = customer.Name,
                Code = customer.Code,
                Adress = customer.Adress,
                Discount = customer.Discount
            };
        }

        public void UpdateCustomerAfterJsonPatchDocument(CustomerDataTransferObject customerModified)
        {
            _database.Customers.Update(new Customer
            {
                Name = customerModified.Name,
                Code = customerModified.Code,
                Adress = customerModified.Adress,
                Discount = customerModified.Discount
            });
            _database.Save();
        }

        public IEnumerable<CustomerDataTransferObject> GetAllCustomers()
        {
            var customers = _database.Customers.GetAllItems().ToList();
            return customers.Select(c => new CustomerDataTransferObject
            {
                Id = c.Id,
                Name = c.Name,
                Code = c.Code,
                Adress = c.Adress,
                Discount = c.Discount
            });
        }

        public async Task<IdentityResult> AddCustomerUser(UserManager<CustomerInfo> userManager,
            CustomerInfoDataTransferObject customerUserDataTransferObject,
            string password)
        {
            var customerUser = ConvertCustomerUserDataTransferObjectToCustomerUser(customerUserDataTransferObject);
            return await _database.Customers.Create(userManager, customerUser, password);
        }

        public CustomerInfoDataTransferObject FindUserByUsingUserNameOrEmail(string email,
            UserManager<CustomerInfo> userManager)
        {
            var customerUserTask = _database.Customers.FindUser(userManager, email);
            var customerUserDataTransferObject = new CustomerInfoDataTransferObject
            {
                Id = customerUserTask.Result.Id,
                UserName = customerUserTask.Result.UserName,
                PasswordHash = customerUserTask.Result.PasswordHash,
                AccessFailedCount = customerUserTask.Result.AccessFailedCount,
                ConcurrencyStamp = customerUserTask.Result.ConcurrencyStamp,
                Email = customerUserTask.Result.Email,
                EmailConfirmed = customerUserTask.Result.EmailConfirmed,
                LockoutEnabled = customerUserTask.Result.LockoutEnabled,
                LockoutEnd = customerUserTask.Result.LockoutEnd,
                NormalizedEmail = customerUserTask.Result.NormalizedEmail,
                PhoneNumber = customerUserTask.Result.PhoneNumber,
                NormalizedUserName = customerUserTask.Result.NormalizedUserName,
                PhoneNumberConfirmed = customerUserTask.Result.PhoneNumberConfirmed,
                SecurityStamp = customerUserTask.Result.SecurityStamp,
                TwoFactorEnabled = customerUserTask.Result.TwoFactorEnabled,
                CustomerId = customerUserTask.Result.CustomerId
            };
            return customerUserDataTransferObject;
        }

        public async Task<IdentityResult> AddRoleToUser(CustomerInfoDataTransferObject customerUserDataTransferObject,
            string role, UserManager<CustomerInfo> userManager)
        {
            var user = ConvertCustomerUserDataTransferObjectToCustomerUser(customerUserDataTransferObject);
            return await _database.Customers.AddRoleToUserInDb(userManager, role, user);
        }

        public async Task<IdentityRole> FindRoleByName(RoleManager<IdentityRole> roleManager, string role)
        {
            return await _database.Customers.FindRole(roleManager, role);
        }

        private CustomerInfo ConvertCustomerUserDataTransferObjectToCustomerUser(
            CustomerInfoDataTransferObject customerUserDataTransferObject)
        {
            return new CustomerInfo
            {
                Id = customerUserDataTransferObject.Id,
                UserName = customerUserDataTransferObject.UserName,
                PasswordHash = customerUserDataTransferObject.PasswordHash,
                AccessFailedCount = customerUserDataTransferObject.AccessFailedCount,
                ConcurrencyStamp = customerUserDataTransferObject.ConcurrencyStamp,
                Email = customerUserDataTransferObject.Email,
                EmailConfirmed = customerUserDataTransferObject.EmailConfirmed,
                LockoutEnabled = customerUserDataTransferObject.LockoutEnabled,
                LockoutEnd = customerUserDataTransferObject.LockoutEnd,
                NormalizedEmail = customerUserDataTransferObject.NormalizedEmail,
                PhoneNumber = customerUserDataTransferObject.PhoneNumber,
                NormalizedUserName = customerUserDataTransferObject.NormalizedUserName,
                PhoneNumberConfirmed = customerUserDataTransferObject.PhoneNumberConfirmed,
                SecurityStamp = customerUserDataTransferObject.SecurityStamp,
                TwoFactorEnabled = customerUserDataTransferObject.TwoFactorEnabled,
                CustomerId = customerUserDataTransferObject.CustomerId
            };
        }


        public Task<bool> CheckIsUserInRole(UserManager<CustomerInfo> userManager,
            CustomerInfoDataTransferObject customerUserDataTransferObject,
            string role)
        {
            var user = ConvertCustomerUserDataTransferObjectToCustomerUser(customerUserDataTransferObject);
            return userManager.IsInRoleAsync(user, role);
        }
    }
}

