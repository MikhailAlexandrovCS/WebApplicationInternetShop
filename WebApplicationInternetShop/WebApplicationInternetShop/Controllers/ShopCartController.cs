using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationInternetShop.BuisnessLogicLayer.DataTransferObject;
using WebApplicationInternetShop.BuisnessLogicLayer.Interfaces;
using WebApplicationInternetShop.DataAccessLayer.Interfaces;
using WebApplicationInternetShop.Models;

namespace WebApplicationInternetShop.Controllers
{
    [Route("/api/cart")]
    [ApiController]
    public class ShopCartController : Controller
    {
        private IShopCart _shopCart;

        public ShopCartController(IShopCart shopCart)
        {
            _shopCart = shopCart;
        }

        [HttpGet]
        public IActionResult GetShopCart()
        {
            return Ok(_shopCart.ShopCart.Select(oi => new OrderItemModel
            {
                Item = new ItemGetModel
                {
                    Id = oi.Item.Id,
                    Name = oi.Item.Name,
                    Code = oi.Item.Code,
                    Price = oi.Item.Price,
                    Category = oi.Item.Category
                },
                ItemPrice = oi.ItemPrice,
                ItemsCount = oi.ItemsCount
            }));
        }

        [HttpPost]
        public IActionResult AddOrderItemInCart([FromBody] OrderItemModel orderItem)
        {
            if (orderItem == null || !ModelState.IsValid)
                return BadRequest(ModelState);
            var orderItemDataTransferObject = _shopCart.AddOrderItemInCart(new OrderItemDataTransferObject
            {
                ItemID = orderItem.Item.Id,
                ItemsCount = orderItem.ItemsCount
            });
            return CreatedAtRoute("GetShopCartItem", new { id = _shopCart.ShopCart.Count - 1 },
                new OrderItemModel
                {
                    Item = new ItemGetModel
                    {
                        Id = orderItemDataTransferObject.Item.Id,
                        Name = orderItemDataTransferObject.Item.Name,
                        Code = orderItemDataTransferObject.Item.Code,
                        Price = orderItemDataTransferObject.Item.Price,
                        Category = orderItemDataTransferObject.Item.Category
                    },
                    ItemPrice = orderItemDataTransferObject.ItemPrice,
                    ItemsCount = orderItemDataTransferObject.ItemsCount
                });
        }

        [HttpGet("{id}", Name = "GetShopCartItem")]
        public IActionResult GetCartItemByCartId(int id)
        {
            if (id <= 0)
                return BadRequest();
            var cartOrderItem = _shopCart.ShopCart[id - 1];
            if (cartOrderItem == null)
                return BadRequest();
            return Ok(new OrderItemModel
            {
                Item = new ItemGetModel
                {
                    Id = cartOrderItem.Item.Id,
                    Name = cartOrderItem.Item.Name,
                    Code = cartOrderItem.Item.Code,
                    Price = cartOrderItem.Item.Price,
                    Category = cartOrderItem.Item.Category
                },
                ItemPrice = cartOrderItem.ItemPrice,
                ItemsCount = cartOrderItem.ItemsCount
            });
        }

        [HttpDelete]
        public IActionResult ClearShopCart()
        {
            _shopCart.ClearShopCart();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrderItemFromShopCart(int id)
        {
            if (id <= 0)
                return BadRequest();
            _shopCart.DeleteOrderItemFromShopCart(id);
            return NoContent();
        }

        [HttpPatch("updateOrderItem/{newItemsCountValue}")]
        public IActionResult ChangeOrderItemItemsCountFromShopCart([FromBody] OrderItemModel order,
            int newItemsCountValue)
        {
            var findOrderItem = new OrderItemDataTransferObject
            {
                ItemID = order.Item.Id,
                Item = new ItemDataTransferObject
                {
                    Id = order.Item.Id,
                    Name = order.Item.Name,
                    Code = order.Item.Code,
                    Price = order.Item.Price,
                    Category = order.Item.Category
                },
                ItemPrice = order.ItemPrice,
                ItemsCount = order.ItemsCount
            };
            findOrderItem.ItemPrice = findOrderItem.Item.Price;
            _shopCart.ChangeOrderItemCountValue(findOrderItem, newItemsCountValue);
            return NoContent();
        }

        [HttpGet("shopCartSum/{idUser}")]
        public IActionResult GetShopCartSum(Guid idUser)
        {
            if (_shopCart.ShopCart.Count == 0)
                return Ok(0);
            return Ok(_shopCart.ShopCartSum(idUser));
        }
    }
}
