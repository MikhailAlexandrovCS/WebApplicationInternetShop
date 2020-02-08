using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplicationInternetShop.BuisnessLogicLayer.DataTransferObject
{
    public class CustomerInfoDataTransferObject : IdentityUser
    {
        public Guid CustomerId { get; set; }
    }
}
