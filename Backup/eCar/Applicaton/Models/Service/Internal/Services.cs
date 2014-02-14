using eCar.Applicaton.Models.Service.Interfaces;

namespace eCar.Applicaton.Models.Service.Internal
{
    public class Services : IServices
    {
        public Services(
            IAutoService autoService,
            ICategoryService categoryService,
            IDepartmentService departmentService,
            ShoppingCart shoppingCart,
            IAuthenticateService authenticateService,
            IMembershipService membershipService,
            IMessageService messageService,
            IConfigService configService,
            ICloudService cloudService
            )
        {
            Auto = autoService;
            Category = categoryService;
            Department = departmentService;
            ShoppingCart = shoppingCart;
            Authentication = authenticateService;
            Membership = membershipService;
            Message = messageService;
            Config = configService;
            Cloud = cloudService;
        }


        public IAutoService Auto { get; set; }

        public ICategoryService Category { get; set; }

        public IDepartmentService Department { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        public IAuthenticateService Authentication { get; set; }

        public IMembershipService Membership { get; set; }

        public IConfigService Config { get; set; }

        public IMessageService Message { get; set; }

        public ICloudService Cloud { get; set; }
    }
}