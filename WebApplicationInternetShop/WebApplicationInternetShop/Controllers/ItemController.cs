using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationInternetShop.BuisnessLogicLayer.DataTransferObject;
using WebApplicationInternetShop.BuisnessLogicLayer.Interfaces;
using WebApplicationInternetShop.Models;

namespace WebApplicationInternetShop.Controllers
{
    [Route("/api/items")]
    [ApiController]
    public class ItemController : Controller
    {
        private IItemsService _itemService;

        public ItemController(IItemsService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public IActionResult GetItems()
        {
            var customers = _itemService.GetAllItems()
                .Select(i => new ItemGetModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Code = i.Code,
                    Price = i.Price,
                    Category = i.Category
                });
            return Ok(customers);
        }

        [HttpGet("{id}", Name = "GetItem")]
        public IActionResult GetItem(Guid id)
        {
            var item = _itemService.GetItem(id);
            if (item == null)
                return NotFound();
            return Ok(new ItemGetModel
            {
                Id = item.Id,
                Name = item.Name,
                Code = item.Code,
                Price = item.Price,
                Category = item.Category
            });
        }

        [HttpPost]
        public IActionResult CreateItem([FromBody] ItemCreateModel item)
        {
            if (item == null || !ModelState.IsValid)
                return BadRequest(ModelState);
            var newItem = new ItemDataTransferObject
            {
                Name = item.Name,
                Code = item.Code,
                Price = item.Price,
                Category = item.Category
            };

            var newItemId = _itemService.CreateItem(newItem);

            return CreatedAtRoute("GetItem", new { id = newItemId },
                new ItemGetModel
                {
                    Id = newItemId,
                    Name = newItem.Name,
                    Code = newItem.Code,
                    Price = newItem.Price,
                    Category = newItem.Category
                });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(Guid id)
        {
            try
            {
                _itemService.DeleteItem(id);
            }
            catch (NullReferenceException e)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateItem(Guid id,
            [FromBody] ItemUpdateModel itemUpdateModel)
        {
            if (itemUpdateModel == null || !ModelState.IsValid)
                return BadRequest(ModelState);
            _itemService.UpdateItem(id, itemUpdateModel.Name, itemUpdateModel.Code,
                itemUpdateModel.Price, itemUpdateModel.Category);
            return NoContent();
        }
    }
}
