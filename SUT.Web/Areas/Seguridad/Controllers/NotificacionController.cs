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
    public class NotificacionController : Controller
    {
        private readonly ILogService<NotificacionController> _log;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly INotificacionService _NotificacionService;

        public NotificacionController(
                                IAuditoriaService AuditoriaService,
                                INotificacionService NotificacionService)
        {
            _log = new LogService<NotificacionController>();
            _AuditoriaService = AuditoriaService;
            _NotificacionService = NotificacionService;
        }


        public ActionResult Lista(UsuarioInfo user)
        {
            try
            {
                ViewBag.Usuario = user;

                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                _log.Error(ex);
                return View("_Error");
            }
        }


        public ActionResult GetAllLikePagin(Notificacion filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            try
            {

                int totalRows = 0;
                List<Notificacion> lista = _NotificacionService.GetByNotificacion(filtro, pageIndex, pageSize, ref totalRows);

                return Json(new
                {
                    lista = lista.Select(x => new
                    {
                        NotificacionId = x.NotificacionId,
                        IdTipoNotificacion = x.IdTipoNotificacion,
                        CCO = x.CCO,
                        Asunto = x.Asunto,
                        Descripcion = x.Descripcion,
                        FecCreacion = x.FecCreacion.Value.ToString("dd/MM/yyyy"),
                        Estado = x.Estado,
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

        public ActionResult Editar(long id, UsuarioInfo user)
        {
            try
            {
                ViewBag.User = user;

                Notificacion item;
                if (id == 0)
                {
                    item = new Notificacion();
                    ViewBag.Title = "Nueva Notificación";

                }
                else
                {
                    item = _NotificacionService.GetByone(id);
                    ViewBag.Title = "Editar Notificación";
                }
                ViewBag.TipoNotificacion = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "- Seleccionar -", Value = "0" },
                    new SelectListItem() { Text = "Presentar Envio", Value = "1" , Selected = item.IdTipoNotificacion == 1 ? true:false},
                    new SelectListItem() { Text = "Presentar Recibido", Value = "2" , Selected = item.IdTipoNotificacion == 1 ? true:false},
                    new SelectListItem() { Text = "Sustentar Envio", Value = "3" , Selected = item.IdTipoNotificacion == 1 ? true:false},
                    new SelectListItem() { Text = "Sustentar Recibido", Value = "4" , Selected = item.IdTipoNotificacion == 1 ? true:false},
                    new SelectListItem() { Text = "Observar Envio", Value = "5" , Selected = item.IdTipoNotificacion == 1 ? true:false},
                    new SelectListItem() { Text = "Observar Recibido", Value = "6" , Selected = item.IdTipoNotificacion == 1 ? true:false},
                    new SelectListItem() { Text = "Publicar Envio", Value = "7" , Selected = item.IdTipoNotificacion == 1 ? true:false},
                    new SelectListItem() { Text = "Publicar Recibido", Value = "8" , Selected = item.IdTipoNotificacion == 1 ? true:false},
                    new SelectListItem() { Text = "Aprobar Envio", Value = "9" , Selected = item.IdTipoNotificacion == 1 ? true:false},
                    new SelectListItem() { Text = "Aprobar Recibido", Value = "10" , Selected = item.IdTipoNotificacion == 1 ? true:false},
                };


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
        public ActionResult Guardar(Notificacion model, UsuarioInfo user)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    string mensaje = model.NotificacionId == 0 ? "La Notificación fue registrado satisfactoriamente."
                                                : "La Notificación fue modificado satisfactoriamente.";

                    Auditoria objauditoria = new Auditoria();
                    Notificacion obj;

                    if (model.NotificacionId == 0)
                    {
                        obj = new Notificacion();
                        obj.Descripcion = model.Descripcion;

                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;



                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Notificacion";
                        objauditoria.Pantalla = "Notificacion";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/

                    }
                    else
                    {
                        obj = _NotificacionService.GetByone(model.NotificacionId);

                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Notificacion";
                        objauditoria.Pantalla = "Notificacion";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/

                    }
                    obj.CCO = model.CCO;
                    obj.Asunto = model.Asunto;
                    obj.Descripcion = model.Descripcion;
                    obj.Estado = 1;
                    obj.IdTipoNotificacion = model.IdTipoNotificacion;


                    obj.UserModificacion = user.NroDocumento;
                    obj.FecModificacion = DateTime.Now;
                    _NotificacionService.Save(obj);

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