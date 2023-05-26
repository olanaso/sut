using System.Web.Mvc;

namespace Sut.Web.Areas.Formulacion.Controllers
{
    public class SustentoController : Controller
    {
        // GET: Formulacion/Sustento
        public ActionResult DatosGenerales()
        {
            return View();
        }

        public ActionResult Tecnico()
        {
            return View();
        }

        public ActionResult Legal()
        {
            return View();
        }
    }
}