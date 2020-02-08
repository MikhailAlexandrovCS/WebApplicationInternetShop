using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationInternetShop.DataAccessLayer;

namespace WebApplicationInternetShop.Models
{
    public class OrderGetModel
    {
        public Guid Id { get; set; }
        public CustomerGetModel Customer { get; set; }        //??
        public DateTimeOffset OrderDate { get; set; }
        public DateTimeOffset ShipmentDate { get; set; }
        public int OrderNumber { get; set; }
        public OrderStatusType Status { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
    }
}
