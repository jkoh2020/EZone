using EZone.Data;
using EZone.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EZone.Models
{
   public class ShoppingCart
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        //public static ShoppingCart GetCart(HttpContextBase context)
        //{
        //    var cart = new ShoppingCart();
        //    cart.ShoppingCartId = cart.GetCartId(context);
        //    return cart;
        //}
        // Helper method to simplify shopping cart calls
        //public static ShoppingCart GetCart(Controller controller)
        //{
        //    return GetCart(controller.HttpContext);
        //}

        public void AddToCart(Product product)
        {
            var cartItem = _db.Carts.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.ProductId == product.ProductId);

            if(cartItem == null)
            {
                cartItem = new Cart
                {
                    ProductId = product.ProductId,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTimeOffset.Now

                };
                _db.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Count++; // If item does exist in the cart, then add one to the quantity
            }
            _db.SaveChanges(); // Save changes
                
        }
        public int RemoveFromCart(int id)
        {
            var cartItem = _db.Carts.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.RecordId == id);
            int itemCount = 0;
            if(cartItem != null)
            {
                if(cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    _db.Carts.Remove(cartItem);
                }
                _db.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = _db.Carts.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                _db.Carts.Remove(cartItem);
            }
            _db.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return _db.Carts.Where(
               cart => cart.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            int? count = (from cartItems in _db.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();

            return count ?? 0; // Return 0 if all entries are empty
        }

        public double GetTotal()
        {
            double? total = (from cartItems in _db.Carts
                             where cartItems.CartId == ShoppingCartId
                             select (int?)cartItems.Count * cartItems.Product.Price).Sum();

            return total ?? 0;
        }

        public int CreateOrder(Order order)
        {
            double orderTotal = 0;
            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    OrderId = order.OrderId,
                    Price = item.Product.Price,
                    OrderQuantity = item.Count
                };
                orderTotal += (item.Count * item.Product.Price);
                _db.Orders.Add(order);
            }
            order.OrderTotal = orderTotal;
            _db.SaveChanges();
            EmptyCart();
            return order.OrderId;
        }

    }
}
