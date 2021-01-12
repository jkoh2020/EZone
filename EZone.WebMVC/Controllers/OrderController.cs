using EZone.Data;
using EZone.Models;
using EZone.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EZone.WebMVC.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // Get: order list

        //public ActionResult OrderIndex()
        //{
        //    return View(CreateOrderService().GetOrdersList());
        //}

        //// Get: order detail by id
        //public ActionResult OrderDetailById(int id)
        //{
        //    var detail = CreateOrderService().GetOrderDetailById(id);
        //    return View(detail);
        //}

        //public ActionResult CreateOrder()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateOrder(OrderCreate model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Order order = new Order()
        //        {
        //            OrderId = model.OrderId,
        //            ProductId = model.ProductId,
        //            OrderQuantity = model.OrderQuantity,
        //            DateOfOrder = DateTimeOffset.Now
        //        };
        //        Product product = _db.Products.Find(order.ProductId);
        //        product.Quantity -= order.OrderQuantity;
        //        _db.Orders.Add(order);
        //        _db.SaveChanges();
        //        return RedirectToAction("~/Home/Index");
        //    }
        //    return View(model);
        //}

        //private OrderService CreateOrderService()
        //{
        //    var userId = Guid.Parse(User.Identity.GetUserId());
        //    var service = new OrderService(userId);
        //    return service;
        //}
    }
}


