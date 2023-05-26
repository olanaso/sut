using System.Web.Mvc;

namespace Sut.Web.Controllers
{
    public class NotFoundController : Controller
    {
        // GET: NotFound
        public ActionResult Index()
        {
            return View();
        }
    }
}