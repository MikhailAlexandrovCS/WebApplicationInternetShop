using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebApplicationInternetShop.DataAccessLayer.Entities
{
    public class OrderItem
    {
        [Required]
        [Column("ID")]
        public Guid Id { get; set; }
        [Required]
        [Column("ITEM_ID")]
        public Item Item { get; set; }
        public Guid ItemId { get; set; }
        [Required]
        [Column("ITEMS_COUNT")]
        public int ItemsCount { get; set; }
        [Required]
        [Column("ITEM_PRICE")]
        public double ItemPrice { get; set; }
        [Required]
        public Order Order { get; set; }
        public Guid OrderId { get; set; }
    }
}
