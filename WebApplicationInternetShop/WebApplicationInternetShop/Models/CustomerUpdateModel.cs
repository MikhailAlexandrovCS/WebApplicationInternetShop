using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationInternetShop.Models
{
    public class CustomerUpdateModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Adress { get; set; }
        public double? Discount { get; set; }
    }
}
