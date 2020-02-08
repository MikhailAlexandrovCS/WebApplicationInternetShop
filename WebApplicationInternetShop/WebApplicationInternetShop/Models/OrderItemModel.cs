using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationInternetShop.Models
{
    public class OrderItemModel
    {
        public Guid Id { get; set; }
        public ItemGetModel Item { get; set; }
        //public Guid ItemId { get; set; }
        public int ItemsCount { get; set; }
        public double ItemPrice { get; set; }
    }
}
