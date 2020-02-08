using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebApplicationInternetShop.DataAccessLayer.Entities
{
    public class Customer
    {
        [Required]
        [Column("ID")]
        public Guid Id { get; set; }
        [Required]
        [Column("NAME")]
        public string Name { get; set; }
        [Required]
        [Column("CODE")]
        public string Code { get; set; }
        [Column("ADRESS")]
        public string Adress { get; set; }
        [Column("DISCOUNT")]
        public double? Discount { get; set; }
        [NotMapped]
        public Order CustomerOrder { get; set; }
        
    }
}