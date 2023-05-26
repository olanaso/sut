using Newtonsoft.Json;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Sut.Security.Filters
{
    public class Autorizacion : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.OnActionExecuted(filterContext);
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpCookie authCookie = filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie == null)
            {
                var info = new HttpRequestWrapper(HttpContext.Current.Request);
                if (info.IsAjaxRequest()) filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.NonAuthoritativeInformation);
                else filterContext.Result = new HttpUnauthorizedResult();
            }
            else if (string.IsNullOrEmpty(authCookie.Value))
            {
                var info = new HttpRequestWrapper(HttpContext.Current.Request);
                if (info.IsAjaxRequest()) filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.NonAuthoritativeInformation);
                else filterContext.Result = new HttpUnauthorizedResult();
            }
            else
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                if (string.IsNullOrEmpty(ticket.UserData))
                {
                    filterContext.Result = ConfigurationSecurity.RedirectAccessDenied();
                }
                else
                {
                    UsuarioInfo user = JsonConvert.DeserializeObject<UsuarioInfo>(ticket.UserData);

                    if (user == null)
                    {
                        filterContext.Result = ConfigurationSecurity.RedirectAccessDenied();
                    }
                }
            }
            this.OnActionExecuting(filterContext);
        }
    }
}
