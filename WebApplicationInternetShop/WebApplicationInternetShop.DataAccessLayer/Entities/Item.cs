using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebApplicationInternetShop.DataAccessLayer.Entities
{
    public class Item
    {
        [Required]
        [Column("ID")]
        public Guid Id { get; set; }
        [Required]
        [Column("CODE")]
        public string Code { get; set; }
        [Column("NAME")]
        public string Name { get; set; }
        [Column("PRICE")]
        public double? Price { get; set; }
        [Column("CATEGORY")]
        [MaxLength(30)]
        public string Category { get; set; }
        [NotMapped]
        public OrderItem OrderItem { get; set; }
    }
}
