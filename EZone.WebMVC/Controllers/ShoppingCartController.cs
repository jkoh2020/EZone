using EZone.Data;
using EZone.Models.ViewModels;
using EZone.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EZone.WebMVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: ShoppingCart
        public ActionResult CartIndex()
        {
            var cart = ShoppingCartService.GetCart(this.HttpContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(viewModel);
        }

        public ActionResult CheckoutDetails()
        {
            return View();
        }

        public ActionResult AddToCart(int id)
        {
            // Retrieve the product from the database
            var addedProduct = _db.Products
                .Single(product => product.ProductId == id);

            // Add it to the shopping cart
            var cart = ShoppingCartService.GetCart(this.HttpContext);
            cart.AddToCart(addedProduct);
            return RedirectToAction("CartIndex");
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
    }
}