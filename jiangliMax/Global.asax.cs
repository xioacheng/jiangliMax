using jiangliMax.App_Start;
using System.IO;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace jiangliMax
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("Log4Net.config")));
        }
    }
}
