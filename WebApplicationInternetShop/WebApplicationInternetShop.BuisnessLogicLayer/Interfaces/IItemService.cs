using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationInternetShop.BuisnessLogicLayer.DataTransferObject;

namespace WebApplicationInternetShop.BuisnessLogicLayer.Interfaces
{
    public interface IItemsService
    {
        Guid CreateItem(ItemDataTransferObject item);
        void UpdateItem(Guid itemId, string code, string name, double price, string category);
        void DeleteItem(Guid id);
        IEnumerable<ItemDataTransferObject> GetAllItems();
        ItemDataTransferObject GetItem(Guid id);
        void Dispose();
    }
}
