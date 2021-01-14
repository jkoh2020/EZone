using EZone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EZone.Services
{
    public class ShoppingCartService
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        string ShoppingCartId { get; set; }
     
        public const string CartSessionKey = "CartId";
        public static ShoppingCartService GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCartService();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        // Helper method to simplify shopping cart calls
        public static ShoppingCartService GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Product product)
        {
            var cartItem = _db.Carts.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.ProductId == product.ProductId);
            
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
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
                // If item does exist in the cart, then add one to the quantity
                cartItem.Count++;
            }
            // Save changes
            _db.SaveChanges();
        }
        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = _db.Carts.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem !=null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    _db.Carts.Remove(cartItem);
                }
                // Save changes
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
            // Save changes
            _db.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return _db.Carts.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in _db.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            // Multiply product price by count of that product to get the current price for each of those products in the cart
            // Sum all product price totals to get the cart total
            decimal? total = (from cartItems in _db.Carts
                             where cartItems.CartId == ShoppingCartId
                             select (int?)cartItems.Count * cartItems.Product.Price).Sum();
            //return total ?? Math.Round((double)total,2);
            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;
            var cartItems = GetCartItems();
            // Adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    // OrderDetail data     Cart data
                    ProductId = item.ProductId,
                    OrderId = order.OrderId,
                    Price = item.Product.Price,
                    OrderQuantity = item.Count
                };
                // Set the order total of the shopping cart
                orderTotal = Math.Round((decimal)orderTotal + Math.Round(item.Count * (decimal)item.Product.Price));
                _db.OrderDetails.Add(orderDetail);
            }
            // Set the order's total will be the the orderTotal count with tax
            decimal tax = .007M;
            decimal taxAmount = Math.Round((decimal)orderTotal * tax, 2);
            order.Total = orderTotal + taxAmount;
            _db.SaveChanges();
            EmptyCart(); // Empty the shopping cart
            return order.OrderId;
        }
        
        // Using HttpContextBase to allow access to cookies
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid(); // Generate a new random Guid using System.Guid class
                    context.Session[CartSessionKey] = tempCartId.ToString(); // Send tempCartId back to client as a cookie
                }
            }
            
            return context.Session[CartSessionKey].ToString();
        }

        // When a user has logged in, migrate their shopping cart to be associated with their username
        public void MigrateCart(string userName) 
        {
            var shoppingCart = _db.Carts.Where(
                c => c.CartId == ShoppingCartId);
            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            _db.SaveChanges();
        }
       
    }
}
