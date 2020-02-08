using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplicationInternetShop.BuisnessLogicLayer.Infrastructure
{
    public class NoFoundOrderItemException : Exception
    {
        public NoFoundOrderItemException(string message) : base(message)
        { }
    }
}
