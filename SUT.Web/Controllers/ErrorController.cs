using Sut.Security;
using System.Web;
using System.Web.Mvc;

namespace Sut.Web.Controllers
{

    public class ErrorController : Controller
    {

        public ActionResult Index(UsuarioInfo user)
        {
            return View();
        }
        public ActionResult Error_400_BadRequest()
        {
            return View();
        }

        public ActionResult Error_400()
        {
            throw new HttpException(504, "No tienes permisos para acceder a este recurso");

            // Código para el recurso protegido
            return View();
        }

        public ActionResult Error_401_Unauthorized()
        {
            return View();
        }
        public ActionResult Error_403_Forbidden()
        {
            return View();
        }
        public ActionResult Error_404_NotFound()
        {
            return View();
        }
        public ActionResult Error_500_InternalServer()
        {
            return View();
        }
        public ActionResult Error_504_GatewayTimeout()
        {
            return View();
        }





    }
}