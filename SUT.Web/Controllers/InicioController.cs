using Sut.Entities;
using Sut.IApplicationServices;
using Sut.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sut.Web.Controllers
{
    [Authorize]
    public class InicioController : Controller
    {

        private readonly IMenuService _menuService;
        private readonly IRolMenuService _rolMenuService;
        public InicioController(IMenuService menuService,
                                  IRolMenuService rolMenuService)
        {

            _menuService = menuService;
            _rolMenuService = rolMenuService;
        }

        public ActionResult Index(UsuarioInfo user)
        {
            //if (user.Rol == (short)Entities.Rol.Administrador)
            //    return RedirectToAction("Lista", "Tupa", new { area = "Estandar" });
            //else
            //    return RedirectToAction("Lista", "Expediente", new { area = "Simplificacion" });


            return View();
        }

        //private Pagina pageActual()
        //{
        //    var fullUrl = Request.Url.ToString();
        //    var questionMarkIndex = fullUrl.IndexOf('?');
        //    string queryString = null;
        //    string url = fullUrl;
        //    if (questionMarkIndex != -1)
        //    {
        //        url = fullUrl.Substring(0, questionMarkIndex);
        //        queryString = fullUrl.Substring(questionMarkIndex + 1);
        //    }

        //    var request = new HttpRequest(null, url, queryString);
        //    var response = new HttpResponse(new StringWriter());
        //    var httpContext = new HttpContext(request, response);

        //    var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

        //    var values = routeData.Values;

        //    return new Pagina()
        //    {
        //        Area = routeData.DataTokens.ContainsKey("area") ? routeData.DataTokens["area"].ToString() : "",
        //        Controller = values["controller"].ToString(),
        //        Action = values["action"].ToString()
        //    };
        //}

        public ActionResult Breadcrumbs()
        {
            //Pagina page = pageActual();

            //List<Pagina> listRuta = new List<Pagina>();
            //if (page.Area != "") listRuta.Add(new Pagina() { Nombre = page.Area, Action = "#" });
            //listRuta.Add(new Pagina() { Nombre = page.Controller, Action = String.Format("{0}{1}/{2}/Index", (page.Area != "" ? "/" : ""), page.Area, page.Controller) });
            //if (page.Action != "Index") listRuta.Add(new Pagina() { Nombre = page.Action, Action = "#" });

            //return PartialView("_BreadcrumbsPartial", listRuta);
            return PartialView("_BreadcrumbsPartial");
        }

        public ActionResult Menu()
        {
            //Pagina page = pageActual();
            //string rutaActual = string.Format("/{0}/{1}/{2}", page.Area, page.Controller, page.Action);

            //List<Menu> menus = MenuRepositorio.GetByRol(user.Perfiles.First().IdPerfil);

            //Menu root = new Menu();
            //root.Menu1 = menus.Where(x => x.MenuPadreId == null)
            //    .Select(x => new Menu()
            //    {
            //        MenuId = x.MenuId,
            //        Nombre = x.Nombre,
            //        Ruta = x.Ruta,
            //        Icono = x.Icono,
            //        Activo = false,
            //        Menu1 = new List<Menu>()
            //    }).ToList();
            //root.Menu1.ToList().ForEach(x => x.Menu1 = menus.Where(m => m.MenuPadreId == x.MenuId).ToList());
            //root.Menu1.ToList().ForEach(x => x.Menu1.ToList().ForEach(m => m.Activo = m.Ruta == rutaActual));

            //ViewBag.Name = user.Name;
            //return PartialView("_MenuPartial", root);

            return PartialView("_MenuPartial");
        }

        public ActionResult TopMenu(UsuarioInfo user)
        {
            /*Lógica para enviar el area/action/controller activo*/
            var fullUrl = Request.Url.ToString();
            var questionMarkIndex = fullUrl.IndexOf('?');
            string queryString = null;
            string url = fullUrl;
            if (questionMarkIndex != -1)
            {
                url = fullUrl.Substring(0, questionMarkIndex);
                queryString = fullUrl.Substring(questionMarkIndex + 1);
            }

            var request = new HttpRequest(null, url, queryString);
            var response = new HttpResponse(new StringWriter());
            var httpContext = new HttpContext(request, response);

            var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

            List<Menu> lista = _menuService.GetByMenu();

            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenu(Convert.ToInt32(user.Rol));

            for (int i = 0; i < listarolmenu.Count(); i++)
            {
                if (lista[i].MenuId == listarolmenu[i].MenuId)
                {
                    lista[i].Ver = listarolmenu[i].Ver;
                }
            }

            user.menu = lista.Where(x => x.Ver == true && x.Estado == 1).ToList();

            var values = routeData.Values;
            var controllerName = values["controller"];
            var actionName = values["action"];
            var areaName = routeData.DataTokens["area"];

            ViewBag.ControllerActive = areaName == null ? controllerName : string.Format("{0}/{1}", areaName, controllerName);
            ViewBag.ActionActive = actionName;

            return PartialView("_TopMenuPartial", user);
        }

        public ActionResult Footer()
        {
            return PartialView("_FooterPartial");
        }
    }
}