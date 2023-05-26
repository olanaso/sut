using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Sut.Security
{
    public class UserDataModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {

            if (bindingContext.Model != null)
            {
                throw new InvalidOperationException("Can not update instances");
            }
            if (controllerContext.RequestContext.HttpContext.Request.IsAuthenticated)
            {
                HttpCookie authCookie = controllerContext.RequestContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (authCookie == null) return null;
                if (string.IsNullOrEmpty(authCookie.Value)) return null;

                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                if (!string.IsNullOrEmpty(ticket.UserData))
                {
                    UsuarioInfo user = JsonConvert.DeserializeObject<UsuarioInfo>(ticket.UserData);
                    
                    return user;
                }
            }
            return null;
        }
    }
}
