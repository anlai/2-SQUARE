using System;
using System.Web.Mvc;
using System.Web.Routing;
using _2SQUARE.Controllers;
using _2SQUARE.Core.Domain;
using Microsoft.Practices.ServiceLocation;
using MvcContrib.Castle;
using Castle.Windsor;
using MvcContrib.Routing;
using NHibernate;
using UCDArch.Data.NHibernate;
using UCDArch.Web.IoC;
using UCDArch.Web.ModelBinder;
using UCDArch.Web.Validator;

namespace _2SQUARE
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    routes.Clear();

        //    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        //    routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

        //    MvcRoute.MappUrl("{controller}/{action}/{id}")
        //        .WithDefaults(new { controller = "Home", action = "Index", id = "" })
        //        .AddWithName("Default", routes);

        //}

        protected void Application_Start()
        {
#if DEBUG
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
#endif

            xVal.ActiveRuleProviders.Providers.Add(new ValidatorRulesProvider());

            new RouteConfigurator().RegisterRoutes();

            ModelBinders.Binders.DefaultBinder = new UCDArchModelBinder();

            IWindsorContainer container = InitializeServiceLocator();

            NHibernateSessionConfiguration.Mappings.UseFluentMappings(typeof(Project).Assembly);

            //NHibernateSessionManager.Instance.RegisterInterceptor(container.Resolve<IInterceptor>());
        }

        private static IWindsorContainer InitializeServiceLocator()
        {
            IWindsorContainer container = new WindsorContainer();
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));

            container.RegisterControllers(typeof(HomeController).Assembly);
            ComponentRegistrar.AddComponentsTo(container);

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));

            return container;
        }
    }
}