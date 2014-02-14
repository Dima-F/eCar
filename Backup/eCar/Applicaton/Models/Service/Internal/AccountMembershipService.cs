using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using eCar.Applicaton.Models.Service.Interfaces;

namespace eCar.Applicaton.Models.Service.Internal
{
    public class AccountMembershipService:IMembershipService
    {
        private readonly MembershipProvider _provider;

        public AccountMembershipService() : this(null){}

        private Guid StringToGUID(string value)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            return new Guid(data);
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("", "password");

            return _provider.ValidateUser(userName, password);
        }

        private MembershipCreateStatus CreateUser(string userName, string password, string email, string OpenID)
        {
            if (String.IsNullOrEmpty(userName)) throw
            new ArgumentException("Значение не может быть пустым.", "userName");
            if (String.IsNullOrEmpty(password)) throw
            new ArgumentException("Значение не может быть пустым.", "password");
            if (String.IsNullOrEmpty(email)) throw
            new ArgumentException("Значение не может быть пустым.", "email");
            MembershipCreateStatus status;
            //пришлось добавить условие, потому что метод StringToGUID(OpenID) не работает с нулевым 
            //OpenID идентификатором(это случается при обычной регистрации) 
            if (!String.IsNullOrEmpty(OpenID))
            {
                _provider.CreateUser(userName, password, email, null, null, true, StringToGUID(OpenID), out status);
            }
            else
            {
                _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            }
            return status;
        }
        //метод введен мною для того,чтобы в МД Register не оперировать с такими обектами, как MembershipCreateStatus
        public bool RegisterUser(string userName, string password, string email, string OpenID, out string error)
        {
            var createStatus = CreateUser(userName, password, email, OpenID);
            if(createStatus==MembershipCreateStatus.Success)
            {
                error = "";
                return true;
            }
            error = ErrorCodeToString(createStatus);
            return false;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            try
            {
                if (String.IsNullOrEmpty(userName)) throw new ArgumentException("", "userName");
                if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("", "oldPassword");
                if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("", "newPassword");

                MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
                return currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public MembershipUser GetUser(string OpenID,bool isUserOnline)
        {
            return _provider.GetUser(StringToGUID(OpenID), isUserOnline);
        }
        #region Status Codes

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        #endregion
    }
}