using eCar.Applicaton.Models.Service.Internal;

namespace eCar.Applicaton.Models.Service.Interfaces
{
    ///BLL interface
    public interface IServices
    {
        IAutoService Auto { get; }
        ICategoryService Category { get; }
        IDepartmentService Department { get; }
        ShoppingCart ShoppingCart { get; }
        IAuthenticateService Authentication { get; }
        IMembershipService Membership { get; set; }
        //IUserService User { get; }
        IConfigService Config { get; }
        IMessageService Message { get; }
        ICloudService Cloud { get; }
    }
}