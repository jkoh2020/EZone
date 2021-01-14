using EZone.Data;
using EZone.Models;
using EZone.Models.Home;
using EZone.Models.ViewModels;
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
            return View(model.CreateModel(search, page, 8));

            ////// Use this code when not using IpagedList
            //HomeIndexViewModel model = new HomeIndexViewModel();
            //return View(model.CreateModel());

        }

        public ActionResult UserProductDetail(int id)
        {
            var detail = CreateProductService().GetUserProductDetailById(id);
            return View(detail);
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult CheckoutDetails()
        {
            return View();
        }

        public ActionResult PlaceOrder(string total)
        {
            ViewBag.t = total;
            return View();
        }

        public ActionResult CompleteOrder(int id)
        {
            bool isValid = _db.Orders.Any(
                o => o.OrderId == id &&
                o.LastName == User.Identity.Name);
           if (isValid)
            {
                return View(id);
            }
           else
            {
                return View("Error");
            }


        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CompletedOrder(Order order, string total)
        //{
        //    Order obj = new Order();
        //    obj.DateOfOrder = DateTimeOffset.Now;
        //    obj.FirstName = order.FirstName;
        //    obj.LastName = order.LastName;
        //    obj.PhoneNumber = order.PhoneNumber;
        //    obj.Address = order.Address;
        //    obj.City = order.City;
        //    obj.State = order.State;
        //    obj.ZipCode = order.ZipCode;
        //    obj.OrderTotal = double.Parse(total);
        //    _db.Orders.Add(obj);
        //    _db.SaveChanges();
        //    //max order id for order details
        //    int orderId = _db.Orders.Select(a => a.OrderId).DefaultIfEmpty(0).Max();

            

        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CheckoutDetails(OrderCreate model)
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
        //        return RedirectToAction("Index");
        //    }
        //    return View(model);
        //}

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

        //public ActionResult AddToCart(int productId)
        //{
        //    if (Session["cart"] == null)
        //    {
        //        List<Item> cart = new List<Item>();
        //        var product = _db.Products.Find(productId);
        //        cart.Add(new Item()
        //        {
        //            Product = product,
        //            Quantity = 1
        //        });
        //        Session["cart"] = cart;
        //    }
        //    else
        //    {
        //        List<Item> cart = (List<Item>)Session["cart"];
        //        var count = cart.Count();
        //        var product = _db.Products.Find(productId);

        //        for (int i = 0; i < count; i++)
        //        {
        //            if (cart[i].Product.ProductId == productId)
        //            {
        //                int previousQuantity = cart[i].Quantity;
        //                cart.Remove(cart[i]);
        //                cart.Add(new Item()
        //                {
        //                    Product = product,
        //                    Quantity = previousQuantity + 1
        //                });
        //                break;
        //            }
        //            else
        //            {
        //                var p = cart.Where(x => x.Product.ProductId == productId).SingleOrDefault();
        //                if (p == null)

        //                {
        //                    cart.Add(new Item()
        //                    {
        //                        Product = product,
        //                        Quantity = 1
        //                    });

        //                }
        //            }
        //        }

        //        Session["cart"] = cart;

        //    }

        //    return RedirectToAction("Index");

        //}

        public ActionResult AddToCart(int id)
        {
            // Retrieve the product from the database
            var addedProduct = _db.Products
                .Single(product => product.ProductId == id);

            // Add it to the shopping cart
            var cart = ShoppingCartService.GetCart(this.HttpContext);
            cart.AddToCart(addedProduct);
            return RedirectToAction("Index");
        }

        // AJAX: 
        public ActionResult RemoveFromCart(int id)
        {

            var cart = ShoppingCartService.GetCart(this.HttpContext); // Remove the item from the cart
            string productName = _db.Carts
                .Single(item => item.RecordId == id).Product.ProductName;
            int itemCount = cart.RemoveFromCart(id);
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(productName) + "It has been removed from your shopping cart.",
                // ShoppingCartService
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);

        }

        // Get: ShoppingCart summary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCartService.GetCart(this.HttpContext);
            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
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

        public ActionResult RemoveItemFromCheckout(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];

            foreach (var item in cart)
            {
                if (item.Product.ProductId == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return RedirectToAction("Checkout");

        }

       

       

        private ProductService CreateProductService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ProductService(userId);
            return service;
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View("Index");
        }
    }
}

