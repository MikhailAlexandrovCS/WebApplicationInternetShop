using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebApplicationInternetShop.DataAccessLayer.Entities
{
    public class Order
    {
        [Required]
        [Column("ID")]
        public Guid Id { get; set; }
        [Required]
        [Column("CUSTOMER_ID")]
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
        [Required]
        [Column("ORDER_DATE")]
        public DateTimeOffset OrderDate { get; set; }
        [Column("SHIPMENT_DATE")]
        public DateTimeOffset? ShipmentDate { get; set; }
        [Column("ORDER_NUMBER")]
        public int? OrderNumber { get; set; }
        [Column("STATUS", TypeName = "nvarchar(max)")]
        public OrderStatusType? Status { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
