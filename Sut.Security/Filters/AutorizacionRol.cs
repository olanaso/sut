using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Sut.Security.Filters
{
    public class AutorizacionRol : ActionFilterAttribute, IActionFilter
    {
        public string Roles { get; set; }

        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.OnActionExecuted(filterContext);
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null) throw new ArgumentNullException("filterContext");
            if (OutputCacheAttribute.IsChildActionCacheActive(filterContext)) throw new InvalidOperationException("AutorizacionAttribute cannot use within child Action Cache");

            if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) &&
                !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                HttpCookie authCookie = filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie == null)
                {
                    filterContext.Result = ConfigurationSecurity.RedirectAccessDenied();
                }
                else if (string.IsNullOrEmpty(authCookie.Value))
                {
                    filterContext.Result = ConfigurationSecurity.RedirectAccessDenied();
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
                        else if (!string.IsNullOrEmpty(this.Roles))
                        {
                            string[] roles = this.Roles.Split(new char[] { ',' }, StringSplitOptions.None);
                            if (!roles.Any(x => x == user.NomRol))
                            {
                                filterContext.Result = ConfigurationSecurity.RedirectAccessDenied();
                            }
                        }
                        else
                        {
                            filterContext.Result = ConfigurationSecurity.RedirectAccessDenied();
                        }
                    }
                }
            }

            this.OnActionExecuting(filterContext);
        }
    }
}
