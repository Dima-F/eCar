using System.Web.Security;

namespace eCar.Applicaton.Models.Service.Interfaces
{
    public interface IMembershipService
    {
        int MinPasswordLength { get; }
        bool ValidateUser(string userName, string password);
        bool RegisterUser(string userName, string password, string email, string OpenID, out string error);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        MembershipUser GetUser(string OpenID, bool isUserOnline);
    }
}
