using Newtonsoft.Json;
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

namespace Sut.Web.Areas.Simplificacion.Controllers
{
    [Autorizacion]
    public class MapaProcedimientoController : Controller
    {
        private readonly ILogService<MapaProcedimientoController> _log;
        private readonly IMapaProcedimientoService _mapaProcedimientoService;
        private readonly IAuditoriaService _AuditoriaService;
        Auditoria objauditoria = new Auditoria();
        public MapaProcedimientoController(IAuditoriaService AuditoriaService, IMapaProcedimientoService mapaProcedimientoService)
        {
            _log = new LogService<MapaProcedimientoController>();
            _mapaProcedimientoService = mapaProcedimientoService;
            _AuditoriaService = AuditoriaService;
        }

        public ActionResult Lista()
        {
            List<SelectListItem> lstTipo = new List<SelectListItem>();
            lstTipo.Add(new SelectListItem() { Value = "0", Text = " - TODOS - " });
            lstTipo.Add(new SelectListItem() { Value = "1", Text = "Categoría" });
            lstTipo.Add(new SelectListItem() { Value = "2", Text = "Temática" });
            lstTipo.Add(new SelectListItem() { Value = "3", Text = "Tipo" });
            lstTipo.Add(new SelectListItem() { Value = "4", Text = "SubTipo" });
            lstTipo.Add(new SelectListItem() { Value = "5", Text = "Específica" });
            ViewBag.ListaTipo = JsonConvert.SerializeObject(lstTipo);

            return View();
        }

        public string GetAllLikePagin(MapaProcedimiento filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;
            List<MapaProcedimiento> lista = _mapaProcedimientoService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

            return JsonConvert.SerializeObject(new
            {
                data = lista.Select(x => new
                {
                    x.MapaProcedimientoId,
                    x.PadreId,
                    x.Codigo,
                    x.Nombre,
                    Tipo = (short)x.Tipo
                }),
                total = totalRows
            });
        }
        public ActionResult GetByParent(long PadreId)
        {
            try
            {
                List<MapaProcedimiento> lista = _mapaProcedimientoService.GetByParent(PadreId);
                return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        public ActionResult Editar(long id, TipoMapaProcedimiento tipo, UsuarioInfo user)
        {
            try
            {
                ViewBag.Tipo = (short)tipo;

                MapaProcedimiento item;
                if (id == 0)
                {
                    item = new MapaProcedimiento();
                    ViewBag.Title = "Nueva Unidad Orgánica";
                }
                else
                {
                    item = _mapaProcedimientoService.GetOne(id);
                    ViewBag.Title = "Editar Unidad Orgánica";
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
        public ActionResult Guardar(MapaProcedimiento model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.MapaProcedimientoId == 0 ? "La Unidad Orgánica fue registrada satisfactoriamente."
                                                : "La Unidad Orgánica fue modificada satisfactoriamente.";

                    MapaProcedimiento obj;

                    if (model.MapaProcedimientoId == 0)
                    {
                        obj = new MapaProcedimiento();
                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;

                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Unidad Orgánica";
                        objauditoria.Pantalla = "Unidad Orgánica";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }
                    else
                    {
                        obj = _mapaProcedimientoService.GetOne(model.MapaProcedimientoId);
                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Unidad Orgánica";
                        objauditoria.Pantalla = "Unidad Orgánica";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }

                    //obj.Nombre = model.Nombre;
                    //obj.Activo = true;

                    obj.UserModificacion = user.NroDocumento;
                    obj.FecModificacion = DateTime.Now;
                    _mapaProcedimientoService.Save(obj);

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