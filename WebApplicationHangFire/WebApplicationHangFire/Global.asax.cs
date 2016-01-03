using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Http;

namespace WebApplicationHangFire
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.MapHttpRoute(
                name: "Api Controller",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional});
        }

        public override void Init()
        {
            base.Init();
        }
    }
}