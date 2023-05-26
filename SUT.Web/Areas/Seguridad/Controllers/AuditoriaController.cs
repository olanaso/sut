using Sut.Entities;
using Sut.IApplicationServices;
using Sut.Log;
using Sut.Security;
using Sut.Security.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Sut.Web.Areas.Seguridad.Controllers
{
    [Autorizacion]
    public class AuditoriaController : Controller
    {
        private readonly ILogService<AuditoriaController> _log;
        private readonly IEntidadService _entidadService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly IDepartamentoService _departamentoService;
        private readonly IProvinciaService _provinciaService;
        private readonly IRolMenuService _rolMenuService;

        public AuditoriaController(IEntidadService entidadService,
                                IAuditoriaService AuditoriaService,
                                IDepartamentoService departamentoService,
                                IProvinciaService provinciaService,
                                IRolMenuService rolMenuService)
        {
            _log = new LogService<AuditoriaController>();
            _entidadService = entidadService;
            _AuditoriaService = AuditoriaService;
            _departamentoService = departamentoService;
            _provinciaService = provinciaService;
            _rolMenuService = rolMenuService;
        }

        //[AutorizacionRol(Roles = "Administrador,Coordinador")]
        public ActionResult Lista(UsuarioInfo user)
        {
            try
            {
                ViewBag.User = user;
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                _log.Error(ex);
                return View("_Error");
            }
        }

        public ActionResult GetAllLikePagin(Auditoria filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            try
            {

                if (user.Rol == (short)Rol.Coordinador)
                {
                    filtro.EntidadId = user.EntidadId;
                }
                int totalRows = 0;
                List<Auditoria> lista = _AuditoriaService.GetAllLikePaginAudi(filtro, (short)user.Rol, pageIndex, pageSize, ref totalRows);

                return Json(new
                {
                    lista = lista.Select(x => new
                    {
                        AuditoriaEquipoId = x.AuditoriaID,
                        EntidadId = x.EntidadId,
                        Entidad = x.Entidad != null ? x.Entidad.Nombre : "",
                        Usuario = x.Usuario,
                        Actividad = x.Actividad,
                        Pantalla = x.Pantalla,
                        FecCreacion = x.FecCreacion.Value.ToString("dd/MM/yyyy hh:mm:ss tt"),
                    }),
                    totalRows = totalRows
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                _log.Error(ex);
                return PartialView("_Error");
            }
        }


    }
}