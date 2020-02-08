using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationInternetShop.DataAccessLayer;

namespace WebApplicationInternetShop.BuisnessLogicLayer.DataTransferObject
{
    public class OrderDataTransferObject
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerDataTransferObject Customer { get; set; }        //??
        public DateTimeOffset OrderDate { get; set; }
        public DateTimeOffset ShipmentDate { get; set; }
        public int OrderNumber { get; set; }
        public OrderStatusType Status { get; set; }
        public List<OrderItemDataTransferObject> OrderItems { get; set; }
    }
}