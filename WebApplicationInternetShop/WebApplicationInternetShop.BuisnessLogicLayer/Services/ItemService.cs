using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplicationInternetShop.BuisnessLogicLayer.DataTransferObject;
using WebApplicationInternetShop.BuisnessLogicLayer.Infrastructure;
using WebApplicationInternetShop.BuisnessLogicLayer.Interfaces;
using WebApplicationInternetShop.DataAccessLayer.Entities;
using WebApplicationInternetShop.DataAccessLayer.Interfaces;

namespace WebApplicationInternetShop.BuisnessLogicLayer.Services
{
    public class ItemService : IItemsService
    {
        private IUnitOfWork _database { get; set; }

        public ItemService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }

        public Guid CreateItem(ItemDataTransferObject item)
        {
            Guid id = Guid.NewGuid();
            _database.Items.Create(new Item
            {
                Id = id,
                Code = item.Code,
                Name = item.Name,
                Price = item.Price,
                Category = item.Category
            });
            _database.Save();
            return id;
        }

        public void UpdateItem(Guid itemId, string code, string name, double price, string category)
        {
            var itemModified = _database.Items.GetItem(itemId);
            itemModified.Code = code;
            itemModified.Name = name;
            itemModified.Price = price;
            itemModified.Category = category;
            _database.Items.Update(itemModified);
            _database.Save();
        }

        public void DeleteItem(Guid id)
        {
            var item = _database.Items.GetItem(id);
            _database.Items.Delete(item.Id);
            _database.Save();
        }

        public IEnumerable<ItemDataTransferObject> GetAllItems()
        {
            return _database.Items.GetAllItems().Select(i => new ItemDataTransferObject
            {
                Id = i.Id,
                Code = i.Code,
                Name = i.Name,
                Price = (double)i.Price,
                Category = i.Category
            });
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        public ItemDataTransferObject GetItem(Guid id)
        {
            var item = _database.Items.GetItem(id);
            return new ItemDataTransferObject
            {
                Id = item.Id,
                Name = item.Name,
                Code = item.Code,
                Price = (double)item.Price,
                Category = item.Category
            };
        }
    }
}
