using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationInternetShop.BuisnessLogicLayer.DataTransferObject;
using WebApplicationInternetShop.DataAccessLayer;

namespace WebApplicationInternetShop.BuisnessLogicLayer.Interfaces
{
    public interface IOrderService
    {
        OrderDataTransferObject CreateOrder(OrderDataTransferObject order);
        void AcceptOrder(OrderDataTransferObject orderDataTransferObject);
        void CloseOrder(Guid id);
        IEnumerable<OrderDataTransferObject> GetOrdersByStatus(
            OrderStatusType orderStatus, Guid id);
        IEnumerable<OrderDataTransferObject> GetAllOrdersByCustomer(Guid id);
        IEnumerable<OrderDataTransferObject> GetAllOrders();
        OrderDataTransferObject GetOrder(Guid id);
        void DeleteOrder(Guid id);
        void Dispose();
    }
}
