using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using ExperimProj.Controllers;

namespace ExperimProj
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            //регистрация внедрения зависимости Autofac на уровне всего приложения.
            var builder = new ContainerBuilder();
            //регистрируем ActionInvoker, цель которого выяснить, какой же метод класса контроллера нужно выполнить в ответ на пользовательский запрос
            builder.RegisterType<ExtensibleActionInvoker>().As<IActionInvoker>().InstancePerHttpRequest();
            //регистрируем существующие контроллеры в текущей сборке
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).InjectActionInvoker().InstancePerHttpRequest();
            //регистрируем обекти-связиватели моделей, которые существуют в текущей сборке.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterType<MyCalculator>().As<ICalculator>().InstancePerLifetimeScope();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            
        }
    }
}