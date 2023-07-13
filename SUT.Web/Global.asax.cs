using Sut.Security;
using Sut.Web.App_Start;
using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Sut.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            MvcHandler.DisableMvcResponseHeader = true;
            DependencyInjectionConfig.Register();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ConfigurationSecurity.Start();
        }

        protected void Application_AcquireRequestState(Object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            var languageSession = "es";
            if (context != null && context.Session != null)
            {
                languageSession = context.Session["lang"] != null ? context.Session["lang"].ToString() : "es";
            }
            //ADD ESEO
            CultureInfo ci = new CultureInfo(languageSession);
            ci.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = ci;
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            ConfigurationSecurity.EndRequest(this);
        }

    }
}
