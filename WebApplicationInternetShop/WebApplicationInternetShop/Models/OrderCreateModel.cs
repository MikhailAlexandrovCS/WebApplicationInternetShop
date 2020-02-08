using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationInternetShop.DataAccessLayer;

namespace WebApplicationInternetShop.Models
{
    public class OrderCreateModel
    {
        public Guid CustomerId { get; set; }        //??
        public int OrderNumber { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
    }
}
