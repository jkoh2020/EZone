using EZone.Data;
using EZone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZone.Services
{
   public class OrderService
    {
        private readonly Guid _userId;
        public OrderService(Guid userId)
        {
            _userId = userId;
        }

        // Get all order list
        public List<OrderListItem> GetOrdersList()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Orders.Select(o => new OrderListItem
                {
                    ProductId = o.ProductId,
                    OrderQuantity = o.OrderQuantity,
                    OrderTotal = o.OrderTotal,
                    DateOfOrder = o.DateOfOrder,
                    DateOfShipping = o.DateOfOrder
                });
                return query.OrderBy(o => o.DateOfOrder).ToList();
            }
        }

        // Get order detali by id
        public OrderDetail GetOrderDetailById(int id)
        {
            using (var ctx = new ApplicationDbContext()) // Use using statement to connect to the database
            {
                var order =
                    ctx
                        .Orders
                        .Single(o => o.OrderId == id);
                var product =
                    ctx
                        .Products
                        .Single(p => p.ProductId == order.OrderId);
                var customer =
                    ctx
                        .Customers
                        .Single(c => c.CustomerId == order.OrderId);
                return
                    new OrderDetail
                    {
                        OrderId = order.OrderId,
                        OrderDate = order.DateOfOrder,
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        OrderQuantity = order.OrderQuantity,
                        OrderPrice = product.Price,
                        OrderTotal = order.OrderTotal,
                        CustomerId = customer.CustomerId,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Address = customer.Address,
                        //IsShipped = order.IsShipped,
                        DateOfShipped = order.DateOfOrder

                    };
            }
        }
        
    }
}







