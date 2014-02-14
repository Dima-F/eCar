using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using eCar.Applicaton.Models.Service.Interfaces;

namespace eCar.Applicaton.Models.Service.Internal
{
    public class FormsAuthenticationService:IAuthenticateService
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("User can't be empty", "userName");
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}