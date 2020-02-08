using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplicationInternetShop.BuisnessLogicLayer.DataTransferObject;
using WebApplicationInternetShop.BuisnessLogicLayer.Infrastructure;
using WebApplicationInternetShop.BuisnessLogicLayer.Interfaces;
using WebApplicationInternetShop.BuisnessLogicLayer.Services;
using WebApplicationInternetShop.DataAccessLayer.Interfaces;

namespace WebApplicationInternetShop.BuisnessLogicLayer.BuisnessModels
{
    public class ShoppingCartStore : IShopCart
    {
        public List<OrderItemDataTransferObject> ShopCart { get; private set; }
        private IUnitOfWork _database;

        public ShoppingCartStore(IUnitOfWork unitOfWork)
        {
            ShopCart = new List<OrderItemDataTransferObject>();
            _database = unitOfWork;
        }

        public OrderItemDataTransferObject AddOrderItemInCart(OrderItemDataTransferObject orderItem)
        {
            var item = _database.Items.GetItem(orderItem.ItemID);
            orderItem.Item = new ItemDataTransferObject
            {
                Id = orderItem.ItemID,
                Name = item.Name,
                Code = item.Code,
                Price = (double)item.Price,
                Category = item.Category
            };
            orderItem.ItemPrice = orderItem.Item.Price;
            ShopCart.Add(orderItem);
            return orderItem;
        }

        public void DeleteOrderItemFromShopCart(int id)
        {
            ShopCart.RemoveAt(id - 1);
        }

        public void ClearShopCart()
        {
            ShopCart.Clear();
        }

        public void ChangeOrderItemCountValue(OrderItemDataTransferObject orderItem, int itemCountNewValue)
        {
            int indexOforderItem = ShopCart.FindIndex(oi => oi.ItemID.Equals(orderItem.ItemID));
            if (indexOforderItem == -1)
                throw new NoFoundOrderItemException("Такого элемента заказа не найдено!");
            ShopCart[indexOforderItem].ItemsCount = itemCountNewValue;
            if (itemCountNewValue == 0)
                DeleteOrderItemFromShopCart(indexOforderItem);
        }

        public double ShopCartSum(Guid userId)
        {
            return (double)ShopCart.Sum(
                oi => oi.ItemPrice * oi.ItemsCount * (1 - _database.Customers.GetItem(userId).Discount));
        }
    }
}
