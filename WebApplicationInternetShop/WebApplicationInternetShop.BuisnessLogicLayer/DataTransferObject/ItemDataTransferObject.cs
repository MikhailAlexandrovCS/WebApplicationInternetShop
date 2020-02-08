using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplicationInternetShop.BuisnessLogicLayer.DataTransferObject
{
    public class ItemDataTransferObject
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
    }
}