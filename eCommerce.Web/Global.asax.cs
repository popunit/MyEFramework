using eCommerce.Core.Data;
using eCommerce.Core.Infrastructure;
using eCommerce.Services.Common;
using eCommerce.Web.Framework.Mvc.DependencyResolvers;
using eCommerce.Web.Framework.Mvc.ModelBinder;
using eCommerce.Web.Framework.Mvc.RouteData;
using eCommerce.Web.Framework.Mvc.View.ViewEngines;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace eCommerce.Web
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

            var routeManager = DependencyResolver.Current.GetService<IRouteManager>();
            routeManager.RegisterAllRoutes(routes);

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default
            //Database.DefaultConnectionFactory = new SqlConnectionFactory("Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            // Set dependencyresolver
            DependencyResolver.SetResolver(new WebDependencyResolver());

            // Binding model
            ModelBinderProviders.BinderProviders.Add(new WebModelBinderProvider());

            //bool dbInstalled = DatabaseSettingHelper.FindDatabaseSettings; // TO-DO: the value has been checked before, should use ioc or cache to reduce execute times
            bool dbInstalled = DependencyResolver.Current.GetService<IDataInfoDataService>().DatabaseIsInstalled();
            if (dbInstalled)
            {
                // Update view engines to new
                ViewEngines.Engines.Clear();
                ViewEngines.Engines.Add(new ThemeableRazorViewEngine());
            }

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            BundleTable.Bundles.RegisterTemplateBundles();
        }
    }
}