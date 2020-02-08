using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationInternetShop.BuisnessLogicLayer.DataTransferObject;
using WebApplicationInternetShop.BuisnessLogicLayer.Interfaces;
using WebApplicationInternetShop.DataAccessLayer;
using WebApplicationInternetShop.Models;

namespace WebApplicationInternetShop.Controllers
{
    [Route("/api/orders")]
    [ApiController]
    public class OrderController : Controller
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orders = _orderService.GetAllOrders();
            if (orders == null)
                return NotFound();
            return Ok(orders.Select(o => new OrderGetModel
            {
                Id = o.Id,
                Customer = new CustomerGetModel
                {
                    Id = o.Customer.Id,
                    Name = o.Customer.Name,
                    Code = o.Customer.Code,
                    Adress = o.Customer.Adress,
                    Discount = o.Customer.Discount
                },
                OrderDate = o.OrderDate,
                ShipmentDate = o.ShipmentDate,
                Status = o.Status,
                OrderNumber = o.OrderNumber,
                OrderItems = o.OrderItems.Select(oi => new OrderItemModel
                {
                    Id = oi.Id,
                    Item = new ItemGetModel
                    {
                        Id = oi.Item.Id,
                        Name = oi.Item.Name,
                        Code = oi.Item.Code,
                        Price = oi.Item.Price,
                        Category = oi.Item.Category
                    }
                }).ToList()
            }));
        }

        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult GetOrder(Guid id)
        {
            var order = _orderService.GetOrder(id);
            if (order == null)
                return NotFound();
            return Ok(new OrderGetModel
            {
                Id = order.Id,
                Customer = new CustomerGetModel
                {
                    Id = order.Customer.Id,
                    Name = order.Customer.Name,
                    Code = order.Customer.Code,
                    Adress = order.Customer.Adress,
                    Discount = order.Customer.Discount
                },
                OrderDate = order.OrderDate,
                ShipmentDate = order.ShipmentDate == null ? default(DateTimeOffset) : order.ShipmentDate,
                Status = order.Status,
                OrderNumber = order.OrderNumber,
                OrderItems = order.OrderItems.Select(oi => new OrderItemModel
                {
                    Id = oi.Id,
                    Item = new ItemGetModel
                    {
                        Id = oi.Item.Id,
                        Name = oi.Item.Name,
                        Code = oi.Item.Code,
                        Price = oi.Item.Price,
                        Category = oi.Item.Category
                    },
                    ItemsCount = oi.ItemsCount,
                    ItemPrice = oi.ItemPrice
                }).ToList()
            });
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderCreateModel order)
        {
            if (order == null || !ModelState.IsValid)
                return BadRequest(ModelState);
            var newOrder = new OrderDataTransferObject
            {
                CustomerId = order.CustomerId,
                OrderNumber = order.OrderNumber,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDataTransferObject
                {
                    ItemID = oi.Item.Id,
                    ItemPrice = oi.ItemPrice,
                    ItemsCount = oi.ItemsCount
                }).ToList()
            };
            var newOrderDataTransferObject = _orderService.CreateOrder(newOrder);
            return CreatedAtRoute("GetOrder", new { id = newOrderDataTransferObject.Id }, new OrderGetModel
            {
                Id = newOrderDataTransferObject.Id,
                Customer = new CustomerGetModel
                {
                    Id = newOrderDataTransferObject.Customer.Id,
                    Name = newOrderDataTransferObject.Customer.Name,
                    Code = newOrderDataTransferObject.Customer.Code,
                    Adress = newOrderDataTransferObject.Customer.Adress,
                    Discount = newOrderDataTransferObject.Customer.Discount
                },
                OrderDate = newOrderDataTransferObject.OrderDate,
                ShipmentDate = newOrderDataTransferObject.ShipmentDate,
                OrderNumber = newOrderDataTransferObject.OrderNumber,
                Status = newOrderDataTransferObject.Status,
                OrderItems = newOrderDataTransferObject.OrderItems.Select(oi => new OrderItemModel
                {
                    Id = oi.Id,
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
                }).ToList()
            });
        }

        [HttpGet("{id}|{status}")]
        public IActionResult GetOrdersByStatus(Guid id, OrderStatusType status)
        {
            var orders = _orderService.GetOrdersByStatus(status, id);
            if (orders == null)
                return BadRequest();
            return Ok(orders.Select(o => new OrderGetModel
            {
                Id = o.Id,
                Customer = new CustomerGetModel
                {
                    Id = o.Customer.Id,
                    Name = o.Customer.Name,
                    Code = o.Customer.Code,
                    Adress = o.Customer.Adress,
                    Discount = o.Customer.Discount
                },
                OrderDate = o.OrderDate,
                ShipmentDate = o.ShipmentDate,
                Status = o.Status,
                OrderNumber = o.OrderNumber,
                OrderItems = o.OrderItems.Select(oi => new OrderItemModel
                {
                    Id = oi.Id,
                    Item = new ItemGetModel
                    {
                        Id = oi.Item.Id,
                        Name = oi.Item.Name,
                        Code = oi.Item.Code,
                        Price = oi.Item.Price,
                        Category = oi.Item.Category
                    },
                    ItemsCount = oi.ItemsCount,
                    ItemPrice = oi.ItemPrice
                }).ToList()
            }));
        }

        [HttpGet("customer-{id}")]
        public IActionResult GetAllCustomerOrders(Guid id)
        {
            var orders = _orderService.GetAllOrdersByCustomer(id);
            if (orders == null)
                return BadRequest();
            return Ok(orders.Select(o => new OrderGetModel
            {
                Id = o.Id,
                Customer = new CustomerGetModel
                {
                    Id = o.Customer.Id,
                    Name = o.Customer.Name,
                    Code = o.Customer.Code,
                    Adress = o.Customer.Adress,
                    Discount = o.Customer.Discount
                },
                OrderDate = o.OrderDate,
                ShipmentDate = o.ShipmentDate,
                Status = o.Status,
                OrderNumber = o.OrderNumber,
                OrderItems = o.OrderItems.Select(oi => new OrderItemModel
                {
                    Id = oi.Id,
                    Item = new ItemGetModel
                    {
                        Id = oi.Item.Id,
                        Name = oi.Item.Name,
                        Code = oi.Item.Code,
                        Price = oi.Item.Price,
                        Category = oi.Item.Category
                    },
                    ItemsCount = oi.ItemsCount,
                    ItemPrice = oi.ItemPrice
                }).ToList()
            }));
        }

        [HttpPatch]
        public IActionResult AcceptOrder([FromBody] OrderUpdateAcceptDateModel order)
        {
            if (order == null || !ModelState.IsValid)
                return BadRequest(ModelState);
            _orderService.AcceptOrder(new OrderDataTransferObject
            {
                Id = order.Id,
                ShipmentDate = order.ShipmentDate
            });
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult ClodeOrder(Guid id)
        {
            _orderService.CloseOrder(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(Guid id)
        {
            _orderService.DeleteOrder(id);
            return NoContent();
        }
    }
}
