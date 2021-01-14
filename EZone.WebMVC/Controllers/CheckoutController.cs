using EZone.Data;
using EZone.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EZone.WebMVC.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        
        private ApplicationDbContext _db = new ApplicationDbContext();
        const string PromoCode = "FREE";

        // GET: /Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    order.UserName = User.Identity.Name;
                    order.OrderDate = DateTimeOffset.Now;

                    //Save Order
                    _db.Orders.Add(order);
                    _db.SaveChanges();
                    //Process the order
                    var cart = ShoppingCartService.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new { id = order.OrderId });
                }
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }
        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            bool isValid = _db.Orders.Any(
                o => o.OrderId == id &&
                o.UserName == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }

    }
}