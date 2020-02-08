using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationInternetShop.DataAccessLayer;

namespace WebApplicationInternetShop.Models
{
    public class OrderUpdateAcceptDateModel
    {
        public Guid Id { get; set; }
        public DateTimeOffset ShipmentDate { get; set; }
    }
}
