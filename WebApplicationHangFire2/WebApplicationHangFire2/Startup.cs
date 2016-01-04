using Owin; 
using System.Web.Http;
using Hangfire;

namespace WebApplicationHangFire2
{
    class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.

        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);

            // Start hangfire
            Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage("Server=.\\SQLExpress;Database=hangfire;Trusted_Connection=True;");

            appBuilder.UseHangfireDashboard();
            appBuilder.UseHangfireServer();
        }
    }
}

