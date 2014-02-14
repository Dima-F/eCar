using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCar.Applicaton.Models.Service.Interfaces
{
    public interface IAuthenticateService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }
}
