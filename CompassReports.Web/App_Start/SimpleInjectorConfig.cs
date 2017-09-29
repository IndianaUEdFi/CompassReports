using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using CompassReports.Data;
using CompassReports.Data.Context;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace CompassReports.Web
{
    public static class SimpleInjectorConfig
    {
        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize(HttpConfiguration configuration)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            
            InitializeContainer(container);
            InitalizeTypes(container, "Service");

            container.RegisterWebApiControllers(configuration);
       
            container.Verify();

            configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<DatabaseContext, DatabaseContext>(Lifestyle.Scoped);

            container.Register(typeof(IRepository<>), typeof(Repository<>), Lifestyle.Scoped);
        }

        public static void InitalizeTypes(Container container, string endsWith)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies.Where(x => x.FullName.Contains("CompassReports")))
            {
                var types = assembly.GetTypes();
                
                var typesToRegister = (
                   from serviceType in types.Where(t => t.Name.StartsWith("I") && t.Name.EndsWith(endsWith))
                   from implementationType in
                   types.Where(t => t.Name == serviceType.Name.Substring(1) && t.Namespace == serviceType.Namespace)
                   select new
                   {
                       ServiceType = serviceType,
                       ImplementationType = implementationType
                   }
               );

                foreach (var pair in typesToRegister)
                {
                    container.Register(pair.ServiceType, pair.ImplementationType, Lifestyle.Scoped);
                }
            }
        }
    }
}