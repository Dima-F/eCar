using System.Web.Mvc;
using eCar.Applicaton.Models;
using eCar.Applicaton.Models.Service.Interfaces;

namespace eCar.Controllers
{
    public partial class ShoppingCartController : LayoutController
    {
        // 
        // GET: /ShoppingCart/ 

        public ShoppingCartController(IServices services) : base(services)
        {
        }

        public ActionResult Index()
        {
            var cart = Services.ShoppingCart;

            // Set up our ViewModel 
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            // Return the view 
            return View(viewModel);
        }

        // 
        // GET: /Store/AddToCart/5 

        public ActionResult AddToCart(int id)
        {

            // Retrieve the album from the database 
            var addedAuto = Services.Auto
                  .SingleOrDefault(a => a.AutoID == id);

            // Add it to the shopping cart 
            var cart = Services.ShoppingCart;

            cart.AddToCart(addedAuto);

            // Go back to the main store page for more shopping 
            return RedirectToAction("Index");
        }

        // 
        // AJAX: /ShoppingCart/RemoveFromCart/5 
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart 
            var cart = Services.ShoppingCart;
            string autoName = Services.Auto
                 .SingleOrDefault(a => a.AutoID == id).Name;
            // Remove from cart 
            if (string.IsNullOrEmpty(autoName))
                return null;
            int itemCount = cart.RemoveFromCart(id);
            // Display the confirmation message 
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(autoName) +
                     " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        // 
        // GET: /ShoppingCart/CartSummary 

        //[ChildActionOnly]
        [HttpGet]
        public ActionResult CartSummary()
        {
            var cart = Services.ShoppingCart;
            ViewData["Count"] = cart.GetCount();

            return PartialView("CartSummary");
        } 
    }
}
