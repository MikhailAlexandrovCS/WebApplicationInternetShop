using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplicationInternetShop.DataAccessLayer.Entities
{
    public class CustomerInfo : IdentityUser
    {
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
    }
}
