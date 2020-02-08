using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplicationInternetShop.BuisnessLogicLayer.DataTransferObject;
using WebApplicationInternetShop.BuisnessLogicLayer.Infrastructure;
using WebApplicationInternetShop.BuisnessLogicLayer.Interfaces;
using WebApplicationInternetShop.DataAccessLayer;
using WebApplicationInternetShop.DataAccessLayer.Entities;
using WebApplicationInternetShop.DataAccessLayer.Interfaces;

namespace WebApplicationInternetShop.BuisnessLogicLayer.Services
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork _database { get; set; }

        public OrderService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }
        
        public OrderDataTransferObject CreateOrder(OrderDataTransferObject order)
        {
            Guid id = Guid.NewGuid();
            DateTimeOffset currentTime = DateTimeOffset.Now;
            Order newOrder = new Order
            {
                Id = id,
                Customer = _database.Customers.GetItem(order.CustomerId),
                OrderDate = currentTime,
                OrderNumber = order.OrderNumber,
                Status = OrderStatusType.New,
                OrderItems = order.OrderItems.Select(orderItem => new OrderItem
                {
                    Id = Guid.NewGuid(),
                    Item = _database.Items.GetItem(orderItem.ItemID),
                    ItemsCount = orderItem.ItemsCount,
                    ItemPrice = orderItem.ItemPrice
                }).ToList()
            };
            _database.Orders.Create(newOrder);
            _database.Save();
            return new OrderDataTransferObject
            {
                Id = newOrder.Id,
                Customer = new CustomerDataTransferObject
                {
                    Id = newOrder.Customer.Id,
                    Name = newOrder.Customer.Name,
                    Code = newOrder.Customer.Code,
                    Adress = newOrder.Customer.Adress,
                    Discount = newOrder.Customer.Discount
                },
                OrderDate = newOrder.OrderDate,
                ShipmentDate = (DateTimeOffset)(newOrder.ShipmentDate == null ? default(DateTimeOffset) : newOrder.ShipmentDate),
                Status = (OrderStatusType)(newOrder.Status == null ? OrderStatusType.New : newOrder.Status),
                OrderNumber = (int)newOrder.OrderNumber,
                OrderItems = newOrder.OrderItems.Select(oi => new OrderItemDataTransferObject
                {
                    Id = oi.Id,
                    Item = new ItemDataTransferObject
                    {
                        Id = oi.Item.Id,
                        Name = oi.Item.Name,
                        Code = oi.Item.Code,
                        Price = (double)oi.Item.Price,
                        Category = oi.Item.Category
                    },
                    ItemsCount = oi.ItemsCount,
                    ItemPrice = oi.ItemPrice
                }).ToList()
            };
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        public void AcceptOrder(OrderDataTransferObject orderDataTransferObject)
        {
            Order order = _database.Orders.GetOrdersByPredicate(o => o.Id == orderDataTransferObject.Id).FirstOrDefault();
            if (order.Status == OrderStatusType.New)
            {
                order.Status = OrderStatusType.IsExecuted;
                order.ShipmentDate = orderDataTransferObject.ShipmentDate;
                _database.Orders.Update(order);
                _database.Save();
            }
        }

        public void CloseOrder(Guid id)
        {
            Order order = _database.Orders.GetOrdersByPredicate(item => item.Id == id).FirstOrDefault();
            order.Status = OrderStatusType.Done;
            _database.Orders.Update(order);
            _database.Save();
        }
        
        public IEnumerable<OrderDataTransferObject> GetOrdersByStatus(OrderStatusType status,
            Guid id)
        {
            var orders = _database.Orders.GetOrdersByPredicate(o => o.Status == status)
                .Where(o => o.CustomerId == id).ToList();
            foreach (var order in orders)
            {
                _database.OrderItems.GetOrdersByPredicate(oi => oi.OrderId == order.Id);
                _database.Customers.GetOrdersByPredicate(c => c.Id == order.CustomerId);
                foreach (var orderItem in order.OrderItems)
                    _database.Items.GetOrdersByPredicate(i => i.Id == orderItem.Item.Id);
            }
            var customer = _database.Customers.GetItem(id);
            return orders.Select(o => new OrderDataTransferObject
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                ShipmentDate = (DateTimeOffset)(o.ShipmentDate == null ? default(DateTimeOffset) : o.ShipmentDate),
                OrderNumber = (int)(o.OrderNumber == null ? default(int) : o.OrderNumber),
                Status = (OrderStatusType)(o.Status == null ? OrderStatusType.New : o.Status),
                Customer = new CustomerDataTransferObject
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Code = customer.Code,
                    Adress = customer.Adress,
                    Discount = customer.Discount
                },
                OrderItems = o.OrderItems.Select(oi => new OrderItemDataTransferObject
                {
                    Id = oi.Id,
                    Item = new ItemDataTransferObject
                    {
                        Id = _database.Items.GetItem(oi.ItemId).Id,
                        Code = _database.Items.GetItem(oi.ItemId).Code,
                        Name = _database.Items.GetItem(oi.ItemId).Name,
                        Price = (double)_database.Items.GetItem(oi.ItemId).Price,
                        Category = _database.Items.GetItem(oi.ItemId).Category
                    },
                    ItemsCount = oi.ItemsCount,
                    ItemPrice = oi.ItemPrice
                }).ToList()
            }).ToList();
        }

        public void DeleteOrder(Guid id)
        {
            var order = _database.Orders.GetAllItems()
                .Where(orderItem => orderItem.Id == id).FirstOrDefault();
            _database.Orders.Delete(id);
            _database.Save();
        }
        
        public IEnumerable<OrderDataTransferObject> GetAllOrdersByCustomer(Guid id)
        {
            var orders = _database.Orders.GetOrdersByPredicate(o => o.CustomerId == id);
            foreach (var order in orders)
            {
                _database.Customers.GetOrdersByPredicate(c => c.Id == order.CustomerId);
                _database.OrderItems.GetOrdersByPredicate(oi => oi.Order.Id == order.Id);
                foreach (var orderItem in order.OrderItems)
                    _database.Items.GetOrdersByPredicate(i => i.Id == orderItem.Item.Id);
            }
            return orders.Select(o => new OrderDataTransferObject
            {
                Id = o.Id,
                Customer = new CustomerDataTransferObject
                {
                    Id = o.Customer.Id,
                    Name = o.Customer.Name,
                    Code = o.Customer.Code,
                    Adress = o.Customer.Adress,
                    Discount = o.Customer.Discount
                },
                OrderDate = o.OrderDate,
                ShipmentDate = (DateTimeOffset)(o.ShipmentDate == null ? default(DateTimeOffset) : o.ShipmentDate),
                Status = (OrderStatusType)o.Status,
                OrderNumber = (int)o.OrderNumber,
                OrderItems = o.OrderItems.Select(oi => new OrderItemDataTransferObject
                {
                    Id = oi.Id,
                    Item = new ItemDataTransferObject
                    {
                        Id = oi.Item.Id,
                        Code = oi.Item.Code,
                        Name = oi.Item.Name,
                        Price = (double)oi.Item.Price,
                        Category = oi.Item.Category
                    },
                    ItemsCount = oi.ItemsCount,
                    ItemPrice = oi.ItemPrice
                }).ToList()
            });
        }
        
        public IEnumerable<OrderDataTransferObject> GetAllOrders()
        {
            var orders = _database.Orders.GetAllItems();
            foreach (var order in orders)
            {
                _database.OrderItems.GetOrdersByPredicate(oi => oi.Order.Id == order.Id);
                _database.Customers.GetOrdersByPredicate(c => c.Id == order.CustomerId);
                foreach (var orderItem in order.OrderItems)
                    _database.Items.GetOrdersByPredicate(i => i.Id == orderItem.Item.Id);
            }
            return orders.Select(o => new OrderDataTransferObject
            {
                Id = o.Id,
                Customer = new CustomerDataTransferObject
                {
                    Id = o.Customer.Id,
                    Name = o.Customer.Name,
                    Code = o.Customer.Code,
                    Adress = o.Customer.Adress,
                    Discount = o.Customer.Discount
                },
                OrderDate = o.OrderDate,
                ShipmentDate = (DateTimeOffset)(o.ShipmentDate == null ? default(DateTimeOffset) : o.ShipmentDate),
                Status = (OrderStatusType)o.Status,
                OrderNumber = (int)o.OrderNumber,
                OrderItems = o.OrderItems.Select(oi => new OrderItemDataTransferObject
                {
                    Id = oi.Id,
                    Item = new ItemDataTransferObject
                    {
                        Id = oi.Item.Id,
                        Code = oi.Item.Code,
                        Name = oi.Item.Name,
                        Price = (double)oi.Item.Price,
                        Category = oi.Item.Category
                    },
                    ItemsCount = oi.ItemsCount,
                    ItemPrice = oi.ItemPrice
                }).ToList()
            });
        }
        
        public OrderDataTransferObject GetOrder(Guid id)
        {
            var order = _database.Orders.GetItem(id);
            _database.OrderItems.GetOrdersByPredicate(oi => oi.OrderId == order.Id);
            _database.Customers.GetOrdersByPredicate(c => c.Id == order.CustomerId);
            foreach (var orderItem in order.OrderItems)
                _database.Items.GetOrdersByPredicate(i => i.Id == orderItem.Item.Id);
            return new OrderDataTransferObject
            {
                Id = order.Id,
                Customer = new CustomerDataTransferObject
                {
                    Id = order.Customer.Id,
                    Name = order.Customer.Name,
                    Code = order.Customer.Code,
                    Adress = order.Customer.Adress,
                    Discount = order.Customer.Discount
                },
                OrderDate = order.OrderDate,
                ShipmentDate = (DateTimeOffset)(order.ShipmentDate == null ? default(DateTimeOffset) : order.ShipmentDate),
                Status = (OrderStatusType)order.Status,
                OrderNumber = (int)order.OrderNumber,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDataTransferObject
                {
                    Id = oi.Id,
                    Item = new ItemDataTransferObject
                    {
                        Id = oi.Item.Id,
                        Name = oi.Item.Name,
                        Code = oi.Item.Code,
                        Price = (double)oi.Item.Price,
                        Category = oi.Item.Category
                    },
                    ItemPrice = oi.ItemPrice,
                    ItemsCount = oi.ItemsCount
                }).ToList()
            };
        }
    }
}

