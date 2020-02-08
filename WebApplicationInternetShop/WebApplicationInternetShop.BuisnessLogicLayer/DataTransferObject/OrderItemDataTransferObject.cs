using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplicationInternetShop.BuisnessLogicLayer.DataTransferObject
{
    public class OrderItemDataTransferObject
    {
        public Guid Id { get; set; }
        public Guid ItemID { get; set; }
        public ItemDataTransferObject Item { get; set; }
        public int ItemsCount { get; set; }
        public double ItemPrice { get; set; }
    }
}