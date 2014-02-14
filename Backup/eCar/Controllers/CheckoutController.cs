using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using eCar.Applicaton.Html;
using eCar.Applicaton.Models;
using eCar.Applicaton.Models.Service.Entities;
using eCar.Applicaton.Models.Service.Interfaces;
using eCar.Applicaton.Models.Service.Internal;

namespace eCar.Controllers
{
    [Authorize]
    public class CheckoutController : LayoutController
    {
        const string PromoCode = "FREE";
        public CheckoutController(IServices services) : base(services) {}
        public ActionResult AddressAndPayment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);
            try
            {
                if (string.Equals(values["PromoCode"], PromoCode, StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                order.Username = User.Identity.Name;
                order.OrderDate = DateTime.Now;
                order.Status = (int)StatusCode.WaitingForPayment;
                //Save Order6969
                Services.ShoppingCart.ShoppingCartService.OrderService.Insert(order);
                //Process the order
                Services.ShoppingCart.CreateOrderDetails(order);
                return Redirect(GetPayPalPaymentUrl(order));
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }
        public ActionResult Complete(int id)
        {
            //Validate customer owns this order
            var order = Services.ShoppingCart.ShoppingCartService.OrderService.SingleOrDefault(o => o.OrderId == id && o.Username == User.Identity.Name);
            var isValid = false;
            if(order!=null)
            {
                isValid = true;
                order.Status = (int) StatusCode.Confirmed;
                Services.ShoppingCart.ShoppingCartService.OrderService.Update(order);
            }
            return isValid ? View(id) : View("Error");
        }
        public ActionResult Cancel()
        {
            return View();
        }
        //Позже можно както переделать этот МД с использованием Ajax...
        public ActionResult Notify()
        {
            if (IsVerifiedNotification())
            {
                int orderID = Convert.ToInt32(Request.Params["custom"]);
                string status = Request.Params["payment_status"];
                decimal amount = Convert.ToDecimal(Request.Params["mc_gross"], CultureInfo.CreateSpecificCulture("en-US"));

                // get the Order object corresponding to the input orderID, 
                // and check that its total matches the input total
                Order order = Services.ShoppingCart.ShoppingCartService.OrderService.SingleOrDefault(o=>o.OrderId==orderID);
                decimal origAmount = (order.Total);
                if (amount >= origAmount)
                {
                    order.Status = (int)StatusCode.Verified;
                    Services.ShoppingCart.ShoppingCartService.OrderService.Update(order);
                    return View();
                }
            }         

            return View("Error");
        }
        private  string GetPayPalPaymentUrl(Order order)
        {
            string serverUrl = (Services.Config.Current.PayPal.SandboxMode ?
               "https://www.sandbox.paypal.com/us/cgi-bin/webscr" :
               "https://www.paypal.com/us/cgi-bin/webscr");
            string amount = order.Total.ToString("N2").Replace(',', '.');
            string baseUrl = HttpContext.Request.Url.AbsoluteUri.Replace(
               HttpContext.Request.Url.PathAndQuery, "") + HttpContext.Request.ApplicationPath;
            if (!baseUrl.EndsWith("/"))
            baseUrl += "/";
            string notifyUrl = HttpUtility.UrlEncode(baseUrl + "Checkout/Notify");
            string returnUrl = HttpUtility.UrlEncode(baseUrl + "Checkout/Complete?id=" + order.OrderId);
            string cancelUrl = HttpUtility.UrlEncode(baseUrl + "Checkout/Cancel");
            string business = HttpUtility.UrlEncode(Services.Config.Current.PayPal.Business);
            string itemName = HttpUtility.UrlEncode("Order #" + order.OrderId);
            var url = new StringBuilder();
            url.AppendFormat(
               "{0}?cmd=_xclick&upload=1&rm=2&no_shipping=1&no_note=1&currency_code={1}&business={2}&item_number={3}&custom={3}&item_name={4}&amount={5}&&notify_url={6}&return={7}&cancel_return={8}",
               serverUrl, Services.Config.Current.PayPal.CurrencyCode, business, order.OrderId, itemName,
               amount, notifyUrl, returnUrl, cancelUrl);

            return url.ToString();
        }
        private bool IsVerifiedNotification()
        {
            string response = "";
            string post = Request.Form.ToString() + "&cmd=_notify-validate";
            string serverUrl = (Services.Config.Current.PayPal.SandboxMode ?
               "https://www.sandbox.paypal.com/us/cgi-bin/webscr" :
               "https://www.paypal.com/us/cgi-bin/webscr");

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(serverUrl);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = post.Length;

            StreamWriter writer = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            writer.Write(post);
            writer.Close();

            StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream());
            response = reader.ReadToEnd();
            reader.Close();
            return (response == "VERIFIED");
        }
    }
}
