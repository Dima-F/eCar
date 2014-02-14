using System;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using eCar.Applicaton.Models;
using eCar.Applicaton.Models.Service.Interfaces;
using eCar.Applicaton.Models.Service.Internal;

namespace eCar.Controllers
{
    public class AccountController : LayoutController
    {
        private static OpenIdRelyingParty openid = new OpenIdRelyingParty();
        //
        // GET: /Account/LogOn

        public AccountController(IServices services) : base(services){}
        private void MigrateShoppingCart(string UserName)
        {           
            Services.ShoppingCart.MigrateCart(UserName);
            Session[ShoppingCart.CartSessionKey] = UserName;
        }

        [ValidateInput(false)]
        public ActionResult Authenticate(string returnUrl)
        {
            var response = openid.GetResponse();
            if (response == null)
            {
                //Let us submit the request to OpenID provider
                Identifier id;
                if (Identifier.TryParse(Request.Form["openid_identifier"], out id))
                {
                    try
                    {
                        var request = openid.CreateRequest(
                                             Request.Form["openid_identifier"]);
                        return request.RedirectingResponse.AsActionResult();
                    }
                    catch (ProtocolException ex)
                    {
                        ViewBag.Message = ex.Message;
                        return View("LogOn");
                    }
                }

                ViewBag.Message = "Invalid identifier";
                return View("LogOn");
            }

            //Let us check the response
            switch (response.Status)
            {

                case AuthenticationStatus.Authenticated:
                    LogOnModel lm = new LogOnModel();
                    lm.OpenID = response.ClaimedIdentifier;
                    // check if user exist
                    MembershipUser user = Services.Membership.GetUser(lm.OpenID,true);
                    if (user != null)
                    {
                        MigrateShoppingCart(user.UserName);
                        lm.UserName = user.UserName;
                        Services.Authentication.SignIn(user.UserName, false);
                    }

                    return View("LogOn", lm);

                case AuthenticationStatus.Canceled:
                    ViewBag.Message = "Canceled at provider";
                    return View("LogOn");
                case AuthenticationStatus.Failed:
                    ViewBag.Message = response.Exception.Message;
                    return View("LogOn");
            }

            return new EmptyResult();
        }
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Services.Membership.ValidateUser(model.UserName, model.Password))
                {
                    MigrateShoppingCart(model.UserName);
                    Services.Authentication.SignIn(model.UserName,model.RememberMe);
                    //защита от угрозы "Открытая переадресацыя"
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public ActionResult LogOff()
        {
            Services.Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Register(string OpenID)
        {
            ViewBag.PasswordLength = Services.Membership.MinPasswordLength;
            ViewBag.OpenID = OpenID;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                string error;
                bool result = Services.Membership.RegisterUser(model.UserName, model.Password, model.Email, model.OpenID,out error);
                if (result)
                {
                    MigrateShoppingCart(model.UserName);
                    Services.Authentication.SignIn(model.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("",error);
            }
            // If we got this far, something failed, redisplay form
            ViewBag.PasswordLength = Services.Membership.MinPasswordLength;
            return View(model);
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                bool changePasswordSucceeded;
                try
                {
                    changePasswordSucceeded = Services.Membership.ChangePassword(User.Identity.Name, model.OldPassword,
                                                                                  model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }
                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }
    }
}
