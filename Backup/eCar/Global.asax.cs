using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using eCar.Applicaton.Infrastructure;
using eCar.Applicaton.Job;
using eCar.Applicaton.Models;
using eCar.Applicaton.Models.Repository;
using eCar.Applicaton.Models.Service.Interfaces;
using eCar.Applicaton.Models.Service.Internal;

namespace eCar
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ElmahHandleErrorAttribute());
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
            //заполняем базу данных из файла SampleData.cs
            Database.SetInitializer(new SampleData());

            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //регистрация внедрения зависимости Autofac на уровне всего приложения.
            var builder = new ContainerBuilder();
            //регистрируем ActionInvoker, цель которого выяснить, какой же метод класса контроллера нужно выполнить в ответ на пользовательский запрос
            builder.RegisterType<ExtensibleActionInvoker>().As<IActionInvoker>().InstancePerHttpRequest();
            //регистрируем существующие контроллеры в текущей сборке
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).InjectActionInvoker().InstancePerDependency();
            //регистрируем обекти-связиватели моделей, которые существуют в текущей сборке.
            //builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            //для того, чтобы зарегистрировать абстрактные Http класы (HttpContextBase,HttpRequestBase,HttpResponseBase,HttpServerUtilityBase ) к Autofac
            builder.RegisterModule(new AutofacWebTypesModule());
            //регистрация контекстов:
            builder.RegisterType<Services>().As<IServices>().InstancePerLifetimeScope();
            builder.RegisterType<AutoService>().As<IAutoService>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<DepartmentService>().As<IDepartmentService>().InstancePerLifetimeScope();
            builder.RegisterType<EFRepository>().As<IRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ECarContext>().As<DbContext>().InstancePerLifetimeScope();
            builder.RegisterType<ConfigService>().As<IConfigService>().InstancePerLifetimeScope();
            builder.RegisterType<ShoppingCart>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ShoppingCartService>().As<IShoppingCartService>().InstancePerLifetimeScope();
            builder.RegisterType<CartService>().As<ICartService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderDetailService>().As<IOrderDetailService>().InstancePerLifetimeScope();
            builder.RegisterType<FormsAuthenticationService>().As<IAuthenticateService>().InstancePerLifetimeScope();
            builder.RegisterType<AccountMembershipService>().As<IMembershipService>().InstancePerLifetimeScope();
            builder.RegisterType<MessageService>().As<IMessageService>().InstancePerLifetimeScope();
            builder.RegisterType<CloudService>().As<ICloudService>().InstancePerLifetimeScope();
            builder.RegisterType<Beckup>().As<IJob>().InstancePerLifetimeScope();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            ConfigureQuartzJobs(container);
        }
        public static void ConfigureQuartzJobs( IContainer container)
        {
            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            //sched.JobFactory = new AutofacJobFactory(container);----покане понятен толкот этой фабрики??????????????????????????
            sched.Start();
            
            // construct job info
            IJobDetail jobDetail = new JobDetailImpl("myJob", null, typeof(Beckup));
            //IJobDetail jobDetail = JobBuilder.Create<IJob>().Build();
            //триггер будет срабатывать каждые 20 сек.
            ITrigger trigger = new CronTriggerImpl("myTrigger", null, "0/20 * * * * ?");
            sched.ScheduleJob(jobDetail, trigger);
        }
    }
}