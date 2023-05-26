using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Sut.Security
{
    public static class ConfigurationSecurity
    {

      
        public static string AccesoDenegadoArea = ConfigurationManager.AppSettings["Sut.Security.AccesoDenegadoArea"];
        public static string AccesoDenegadoController = ConfigurationManager.AppSettings["Sut.Security.AccesoDenegadoController"];
        public static string AccesoDenegadoAction = ConfigurationManager.AppSettings["Sut.Security.AccesoDenegadoAction"];

        public static void Start()
        {
            ModelBinders.Binders.Add(typeof(UsuarioInfo), new UserDataModelBinder());
        }

        public static void EndRequest(HttpApplication mvcApp)
        {
            if (mvcApp.Response.StatusCode == 302 && !mvcApp.Request.IsAuthenticated)
            {
                string redirectLocation = mvcApp.Response.RedirectLocation;
                if (redirectLocation.Contains("ReturnUrl="))
                {
                    string str = HttpUtility.UrlEncode((mvcApp.Request.IsSecureConnection ? "https://" : "http://") + mvcApp.Request.Url.Host + (mvcApp.Request.Url.IsDefaultPort ? "" : (":" + mvcApp.Request.Url.Port)));
                    mvcApp.Response.RedirectLocation = redirectLocation.Replace("ReturnUrl=", "ReturnUrl=" + str);
                }
            }
        }

        public static string LogOut(Controller controller)
        {
            FormsAuthentication.SignOut();
            controller.Session.Abandon();

            return FormsAuthentication.LoginUrl;
        }

        public static RedirectToRouteResult RedirectAccessDenied()
        {
            return new RedirectToRouteResult(new RouteValueDictionary(new
            {
                area = AccesoDenegadoArea,
                controller = AccesoDenegadoController,
                action = AccesoDenegadoAction
            }));
        }
    }
}
