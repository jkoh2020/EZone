using EZone.Data;
using EZone.Models;
using EZone.Models.Home;
using EZone.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EZone.WebMVC.Controllers
{
    
    public class HomeController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        public ActionResult Index(string search, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
                                             //  page  page size
            return View(model.CreateModel(search,page, 8));
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult CheckoutDetails()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckoutDetails(OrderCreate model)
        {
            if (ModelState.IsValid)
            {
                Order order = new Order()
                {
                    OrderId = model.OrderId,
                    ProductId = model.ProductId,
                    OrderQuantity = model.OrderQuantity,
                    DateOfOrder = DateTimeOffset.Now
                };
                Product product = _db.Products.Find(order.ProductId);
                product.Quantity -= order.OrderQuantity;
                _db.Orders.Add(order);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult DecreaseQuantity(int productId)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = _db.Products.Find(productId);
                foreach (var item in cart)
                {
                    if (item.Product.ProductId == productId)
                    {
                        int previousQuantity = item.Quantity;
                        if (previousQuantity > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = previousQuantity - 1
                            });
                                
                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Checkout");
        }

        public ActionResult IncreaseQuantity(int productId)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = _db.Products.Find(productId);
                foreach (var item in cart)
                {
                    if (item.Product.ProductId == productId)
                    {
                        int previousQuantity = item.Quantity;
                        if (previousQuantity >= 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = previousQuantity + 1
                            });
                        }
                        break;
                    }
                        
                }
                Session["cart"] = cart;

            }
            return RedirectToAction("Checkout");
        }

        public ActionResult AddToCart(int productId)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = _db.Products.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var count = cart.Count();
                var product = _db.Products.Find(productId);

                for (int i = 0; i < count; i++)
                {
                    if (cart[i].Product.ProductId == productId)
                    {
                        int previousQuantity = cart[i].Quantity;
                        cart.Remove(cart[i]);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = previousQuantity + 1
                        });
                        break;
                    }
                    else
                    {
                        var p = cart.Where(x => x.Product.ProductId == productId).SingleOrDefault();
                        if (p == null)

                        {
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = 1
                            });
                           
                        }
                    }
                }
               
                Session["cart"] = cart;
               
            }

            return RedirectToAction("Index");

        }

        public ActionResult RemoveItemFromCart(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];

            foreach (var item in cart)
            {
                if(item.Product.ProductId == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return RedirectToAction("Index");

        }

        // Get: order list

        public ActionResult OrderIndex()
        {
            return View(CreateOrderService().GetOrdersList());
        }

        // Get: order detail by id
        public ActionResult OrderDetailById(int id)
        {
            var detail = CreateOrderService().GetOrderDetailById(id);
            return View(detail);
        }

        public ActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrder(OrderCreate model)
        {
            if (ModelState.IsValid)
            {
                Order order = new Order()
                {
                    OrderId = model.OrderId,
                    ProductId = model.ProductId,
                    OrderQuantity = model.OrderQuantity,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    City = model.City,
                    State = model.State,
                    ZipCode = model.ZipCode,
                    DateOfOrder = DateTimeOffset.Now
                };
                Product product = _db.Products.Find(order.ProductId);
                product.Quantity -= order.OrderQuantity;
                _db.Orders.Add(order);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

       
        private OrderService CreateOrderService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new OrderService(userId);
            return service;
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View("Index");
        }
    }
}