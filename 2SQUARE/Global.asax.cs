using System;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using Devtalk.EF.CodeFirst;
using _2SQUARE.Controllers;
using Microsoft.Practices.ServiceLocation;
using MvcContrib.Castle;
using Castle.Windsor;
using MvcContrib.Routing;

namespace _2SQUARE
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            new RouteConfigurator().RegisterRoutes();

            IWindsorContainer container = InitializeServiceLocator();



#if DEBUG
            //Database.SetInitializer(new SquareInitializer());
            Database.SetInitializer(new SquareInitializerDropCreate());
#else
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SquareContext>());
            Database.SetInitializer(new DontDropDbJustCreateTablesIfModelChanged<SquareContext>());
#endif


        }

        private static IWindsorContainer InitializeServiceLocator()
        {
            IWindsorContainer container = new WindsorContainer();
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));

            container.RegisterControllers(typeof(HomeController).Assembly);
            ComponentRegistrar.AddComponentsTo(container);

            //ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));

            return container;
        }
    }
}