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
    public class PlanTrabajoController : Controller
    {
        private readonly ILogService<PlanTrabajoController> _log;
        private readonly IEntidadService _entidadService;
        private readonly IPlanTrabajoService _planTrabajoService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly IRolMenuService _rolMenuService;
        Auditoria objauditoria = new Auditoria();
        public PlanTrabajoController(IPlanTrabajoService planTrabajoService,
                                    IAuditoriaService AuditoriaService,
                                    IEntidadService entidadService,
                                    IRolMenuService rolMenuService)
        {
            _planTrabajoService = planTrabajoService;
            _entidadService = entidadService;
            _AuditoriaService = AuditoriaService;
            _rolMenuService = rolMenuService;
        }

        [AutorizacionRol(Roles = "Coordinador")]
        public ActionResult Lista(UsuarioInfo user)
        {
            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 5);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/
            ViewBag.Usuario = user;
            return View();
        }

        public ActionResult ListaByEntidad(long EntidadId)
        {
            return PartialView("_ListaByEntidad", EntidadId);
        }

        public ActionResult GetAllLikePagin(PlanTrabajo filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            try
            {
                int totalRows = 0;
                filtro.EntidadId = user.EntidadId;
                List<PlanTrabajo> lista = _planTrabajoService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

                return Json(new
                {
                    lista = lista.Select(x => new
                    {
                        x.PlanTrabajoId,
                        x.EntidadId,
                        x.Descripcion,
                        x.ArchivoAdjuntoId,
                        Fecha = x.Fecha.ToShortDateString()
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

        public ActionResult GetAllLikePaginByEntidad(PlanTrabajo filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            try
            {
                int totalRows = 0;
                List<PlanTrabajo> lista = _planTrabajoService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

                return Json(new
                {
                    lista = lista.Select(x => new
                    {
                        x.PlanTrabajoId,
                        x.EntidadId,
                        x.Descripcion,
                        x.ArchivoAdjuntoId,
                        Fecha = x.Fecha.ToShortDateString()
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

        [AutorizacionRol(Roles = "Coordinador")]
        public ActionResult Editar(long id, UsuarioInfo user)
        {
            try
            {
                /*Inicio Cargar roles - Acceso*/
                List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 5);
                user.rolmenu = listarolmenu.ToList();
                /*Fin Cargar roles  - Acceso*/
                ViewBag.User = user;

                PlanTrabajo item;
                if (id == 0)
                {
                    item = new PlanTrabajo();
                    ViewBag.Title = "Nuevo Plan de Trabajo";
                    item.EntidadId = user.EntidadId;
                }
                else
                {
                    item = _planTrabajoService.GetOne(id);
                    ViewBag.Title = "Editar Plan de Trabajo";
                }

                return PartialView("_Editar", item);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        [HttpPost]
        [AutorizacionRol(Roles = "Coordinador")]
        public ActionResult Guardar(PlanTrabajo model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.PlanTrabajoId == 0 ? "El Plan de Trabajo fue registrado satisfactoriamente."
                                                : "El Plan de Trabajo fue modificado satisfactoriamente.";

                    PlanTrabajo obj;

                    if (model.PlanTrabajoId == 0)
                    {

                        PlanTrabajo item = _planTrabajoService.GetOneEntidad(user.EntidadId);

                        if (item != null)
                        {
                            item.Principal = "0";
                            _planTrabajoService.Save(item);
                        }

                        obj = new PlanTrabajo();
                        obj.EntidadId = user.EntidadId;

                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;
                        obj.Fecha = DateTime.Now;
                        obj.Principal = "1";

                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Plan de Trabajo";
                        objauditoria.Pantalla = "Plan de Trabajo";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/

                    }
                    else
                    {
                        obj = _planTrabajoService.GetOne(model.PlanTrabajoId);


                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Plan de Trabajo";
                        objauditoria.Pantalla = "Plan de Trabajo";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }

                    obj.Fecha = model.Fecha;
                    obj.Descripcion = model.Descripcion;

                    if (model.ArchivoAdjuntoId == 0)
                    {
                        obj.ArchivoAdjuntoId = null;
                    }
                    else { obj.ArchivoAdjuntoId = model.ArchivoAdjuntoId; }

                    obj.UserModificacion = user.NroDocumento;
                    obj.FecModificacion = DateTime.Now;
                    obj.Fecha = DateTime.Now;
                    _planTrabajoService.Save(obj);

                    /*auditoria Grabar*/
                    _AuditoriaService.Save(objauditoria);
                    /*auditoria Grabar*/

                    return Json(new
                    {
                        mensaje = mensaje
                    });
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Error");
                }
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