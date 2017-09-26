using System.Web.Http;
using CompassReports.Web;
using EduApi.Web;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace EduApi.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var httpConfiguration = new HttpConfiguration();
            WebApiConfig.Register(httpConfiguration);
            appBuilder.UseWebApi(httpConfiguration);

            SimpleInjectorConfig.Initialize(httpConfiguration);
            SwaggerConfig.Register(httpConfiguration);
        }
    }
}