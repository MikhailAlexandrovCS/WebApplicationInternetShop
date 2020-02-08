using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationInternetShop.BuisnessLogicLayer.DataTransferObject;

namespace WebApplicationInternetShop.BuisnessLogicLayer.Interfaces
{
    public interface IShopCart
    {
        List<OrderItemDataTransferObject> ShopCart { get; }
        OrderItemDataTransferObject AddOrderItemInCart(OrderItemDataTransferObject orderItem);
        double ShopCartSum(Guid userId);
        void DeleteOrderItemFromShopCart(int id);
        void ClearShopCart();
        void ChangeOrderItemCountValue(OrderItemDataTransferObject orderItem, int itemCountNewValue);
    }
}
