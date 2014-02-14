using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Syntax;
using eCar.Applicaton.Models;
using eCar.Applicaton.Models.Repository;
using eCar.Applicaton.Models.Service.Interfaces;
using eCar.Applicaton.Models.Service.Internal;

namespace eCar.Applicaton.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public IBindingToSyntax<T> Bind<T>()
        {
            return kernel.Bind<T>();
        }

        public IKernel Kernel
        {
            get { return kernel; }
        }

        private void AddBindings()
        {
            // put additional bindings here 
            kernel.Bind<IServices>().To<Services>();
            kernel.Bind<IAutoService>().To<AutoService>();
            kernel.Bind<ICategoryService>().To<CategoryService>();
            kernel.Bind<IDepartmentService>().To<DepartmentService>();
            kernel.Bind<IRepository>().To<EFRepository>();
            kernel.Bind<DbContext>().To<ECarContext>();
            kernel.Bind<IConfigService>().To<ConfigService>();
            kernel.Bind<IShoppingCartService>().To<ShoppingCartService>();
            kernel.Bind<ICartService>().To<CartService>();
            kernel.Bind<IOrderService>().To<OrderService>();
            kernel.Bind<IOrderDetailService>().To<OrderDetailService>();
            kernel.Bind<IAuthenticateService>().To<FormsAuthenticationService>();
            kernel.Bind<IMembershipService>().To<AccountMembershipService>();
            kernel.Bind<IMessageService>().To<MessageService>();
            kernel.Bind<ICloudService>().To<CloudService>();
            //kernel.Bind<HttpContextBase>().To<HttpContext>();
        }
    }

}