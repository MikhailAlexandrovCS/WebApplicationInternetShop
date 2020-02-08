using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApplicationInternetShop.BuisnessLogicLayer.DataTransferObject;
using WebApplicationInternetShop.BuisnessLogicLayer.Interfaces;
using WebApplicationInternetShop.DataAccessLayer.Entities;
using WebApplicationInternetShop.Models;

namespace WebApplicationInternetShop.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : Controller
    {
        private ICustomerService _customerService;
        private SignInManager<CustomerInfo> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<CustomerInfo> _userManager;

        public CustomerController(ICustomerService customerService,
            UserManager<CustomerInfo> userManager,
            SignInManager<CustomerInfo> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _customerService = customerService;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _customerService.GetAllCustomers()
                .Select(c => new CustomerGetModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Code = c.Code,
                    Adress = c.Adress,
                    Discount = c.Discount
                });
            return Ok(customers);
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult GetCustomer(Guid id)
        {
            var customer = _customerService.GetCustomer(id);
            if (customer == null)
                return NotFound();
            return Ok(new CustomerGetModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Code = customer.Code,
                Adress = customer.Adress,
                Discount = customer.Discount
            });
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] CustomerCreateModel customer)
        {
            if (customer == null || !ModelState.IsValid)
                return BadRequest(ModelState);
            var newCustomer = new CustomerDataTransferObject
            {
                Name = customer.Name,
                Adress = customer.Adress,
                Code = customer.Code,
                Discount = customer.Discount
            };

            var newCustomerId = _customerService.CreateCustomer(newCustomer);

            var customerUser = new CustomerInfoDataTransferObject
            {
                UserName = customer.Email,
                Email = customer.Email,
                CustomerId = newCustomerId
            };

            _customerService.AddCustomerUser(_userManager, customerUser, customer.Password).Wait();

            return CreatedAtRoute("GetCustomer", new { id = newCustomerId },
                new CustomerGetModel
                {
                    Id = newCustomerId,
                    Name = newCustomer.Name,
                    Code = newCustomer.Code,
                    Adress = newCustomer.Adress,
                    Discount = newCustomer.Discount
                });
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult LoginUser([FromBody] LoginUserModel loginUserModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = _signInManager.SignOutAsync();
            if (result.IsCompleted)
            {
                var user = _customerService.FindUserByUsingUserNameOrEmail(loginUserModel.Email, _userManager);
                var signResult = _signInManager.PasswordSignInAsync(
                    user.Email, loginUserModel.Password, false, false);
                if (signResult.Result.Succeeded)
                    return Ok();
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Logout")]
        public IActionResult LogoutUser()
        {
            var resultSignung = _signInManager.SignOutAsync();
            if (resultSignung.IsCompletedSuccessfully)
                return Ok(resultSignung.Status);
            return BadRequest(resultSignung.Exception);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(Guid id)
        {
            try
            {
                _customerService.DeleteCustomer(id);
            }
            catch(NullReferenceException e)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateCustomer(Guid id, 
            [FromBody] CustomerUpdateModel customerUpdateModel)
        {
            if (customerUpdateModel == null || !ModelState.IsValid)
                return BadRequest(ModelState);
            _customerService.UpdateCustomer(id, customerUpdateModel.Name, customerUpdateModel.Code,
                customerUpdateModel.Adress, customerUpdateModel.Discount);
            return NoContent();
        }
    }
}
