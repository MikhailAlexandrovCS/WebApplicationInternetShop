using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplicationInternetShop.BuisnessLogicLayer.DataTransferObject
{
    public class CustomerDataTransferObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Adress { get; set; }
        public double? Discount { get; set; }
    }
}