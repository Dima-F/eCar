using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCar.Applicaton.Models.Service.Entities;
using eCar.Applicaton.Models.Service.Interfaces;

namespace eCar.Applicaton.Models.Service.Internal
{
    public class ShoppingCartService : IShoppingCartService
    {
        public ShoppingCartService(ICartService cart, IOrderService order, IOrderDetailService orderDetail)
        {
            CartService = cart;
            OrderService = order;
            OrderDetailService = orderDetail;
        }

        public ICartService CartService { get; private set; }

        public IOrderService OrderService { get; private set; }

        public IOrderDetailService OrderDetailService { get; private set; }
    }
    //так как мы вряд ли будем изменять клас ShoppingCart при смене модели доступа к данным, можно реализовать его без интрерфейсности.
    public class ShoppingCart
    {
        private string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        public IShoppingCartService ShoppingCartService { get; private set; }
        //пришлось передать в конструктор контекст, ибо другого выхода пока не нашел
        //интересно, но судя по всему MVC сам подставляет необходимый контекст в конструктор, даже не нужно использовать NinjectKernel для указания создания типа.
        public ShoppingCart(IShoppingCartService shoppingCartService, HttpContextBase context)
        {
            ShoppingCartService = shoppingCartService;

            ShoppingCartId = GetCartId(context);
        }               
        public void AddToCart(Auto auto)
        {
            // Get the matching cart and album instances 
            var cartItem = ShoppingCartService.CartService.SingleOrDefault(
                     c => c.CartId == ShoppingCartId
                     && c.AutoId == auto.AutoID);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists 
                cartItem = new Cart
                {
                    AutoId = auto.AutoID,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                ShoppingCartService.CartService.Insert(cartItem);
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity 
                cartItem.Count++;
                ShoppingCartService.CartService.Update(cartItem);
            }
        } 
        public int RemoveFromCart(int id) 
        { 
             // Get the cart 
            var cartItem = ShoppingCartService.CartService.SingleOrDefault( 
                      cart => cart.CartId == ShoppingCartId 
                      && cart.RecordId == id);
             int itemCount = 0;

             if (cartItem != null)
             {
                 if (cartItem.Count > 1)
                 {
                     cartItem.Count--;
                     itemCount = cartItem.Count;
                     ShoppingCartService.CartService.Update(cartItem);
                 }
                 else
                 {
                     ShoppingCartService.CartService.Delete(cartItem);
                 }
             }

             return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = ShoppingCartService.CartService.Query(cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                ShoppingCartService.CartService.Delete(cartItem);
            }
        }
        public List<Cart> GetCartItems()
        {
            return ShoppingCartService.CartService.Query(cart => cart.CartId == ShoppingCartId).ToList();
        }
        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up 
            int? count = (from cartItems in ShoppingCartService.CartService.Query()
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();

            // Return 0 if all entries are null 
            return count ?? 0;
        }
        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart 
            // sum all album price totals to get the cart total 
            decimal? total = (from cartItems in ShoppingCartService.CartService.Query()
                              where cartItems.CartId == ShoppingCartId
                              select (int?) cartItems.Count*cartItems.Auto.Price).Sum();
            return total ?? decimal.Zero;
        }
        public int CreateOrderDetails(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            // Iterate over the items in the cart, adding the order details for each 
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    AutoId = item.AutoId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Auto.Price,
                    Quantity = item.Count
                };

                // Set the order total of the shopping cart 
                orderTotal += (item.Count * item.Auto.Price);

                ShoppingCartService.OrderDetailService.Insert(orderDetail);

            }

            // Set the order's total to the orderTotal count 
            order.Total = orderTotal;

            // Empty the shopping cart 
            EmptyCart();

            // Return the OrderId as the confirmation number 
            return order.OrderId;
        } 
        // We're using HttpContextBase to allow access to cookies. 
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
                       // Generate a new random GUID using System.Guid class
                      Guid tempCartId = Guid.NewGuid();
                      // Send tempCartId back to client as a cookie 
                      context.Session[CartSessionKey] = tempCartId.ToString();
                  }
             }
             return context.Session[CartSessionKey].ToString();
        }
        // When a user has logged in, migrate their shopping cart to 
        // be associated with their username 
        public void MigrateCart(string userName)
        {
            var shoppingCart = ShoppingCartService.CartService.Query(c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
                ShoppingCartService.CartService.Update(item);
            }
        } 
    }
}