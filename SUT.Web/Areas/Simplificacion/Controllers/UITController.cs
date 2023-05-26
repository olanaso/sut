using Newtonsoft.Json;
using Sut.Entities;
using Sut.IApplicationServices;
using Sut.Log;
using Sut.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Sut.Web.Areas.Simplificacion.Controllers
{
    [Authorize]
    public class UITController : Controller
    {
        private readonly ILogService<UITController> _log;
        private readonly IUITService _UITService;
        private readonly IAuditoriaService _AuditoriaService;
        Auditoria objauditoria = new Auditoria();
        public UITController(IAuditoriaService AuditoriaService, IUITService UITService)
        {
            _log = new LogService<UITController>();
            _UITService = UITService;
            _AuditoriaService = AuditoriaService;
        }

        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Lista()
        {
            ViewBag.TipoRecurso = 4;
            return View();
        }

        public string GetAllLikePagin(UIT filtro, int pageIndex, int pageSize)
        {
            int totalRows = 0;
            List<UIT> lista = _UITService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    UITId = x.UITID,
                    Descripcion = x.Descripcion,
                    Monto = x.Monto,
                    Fecha = x.Fecha.ToString("yyyy"),
                    estado = x.Estado

                }),
                totalRows = totalRows
            });
        }

        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Editar(long id)
        {
            try
            {
                UIT item;
                if (id == 0)
                {
                    item = new UIT();
                    ViewBag.Title = "Nueva Unidad Impositiva Tributaria";
                }
                else
                {
                    item = _UITService.GetOne(id);
                    ViewBag.Title = "Editar Unidad Impositiva Tributaria";
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
        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Guardar(UIT model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.UITID == 0 ? "La Unidad Impositiva Tributaria fue registrada satisfactoriamente."
                                                : "La Unidad Impositiva Tributaria fue modificada satisfactoriamente.";

                    UIT obj;

                    if (model.UITID == 0)
                    {
                        obj = new UIT();
                        UIT prueba = _UITService.LsitaGetOne();
                        if (prueba != null)
                        {
                            prueba.Estado = "0";
                            _UITService.Save(prueba);
                        }
                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;

                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Unidad Impositiva Tributaria ";
                        objauditoria.Pantalla = "Unidad Impositiva Tributaria ";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }
                    else
                    {
                        obj = _UITService.GetOne(model.UITID);

                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Unidad Impositiva Tributaria ";
                        objauditoria.Pantalla = "Unidad Impositiva Tributaria ";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }

                    obj.Descripcion = model.Descripcion;
                    obj.Monto = model.Monto;
                    obj.Fecha = model.Fecha;
                    obj.Estado = "1";

                    obj.UserModificacion = user.NroDocumento;
                    obj.FecModificacion = DateTime.Now;
                    _UITService.Save(obj);
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
                ModelState.AddModelError("editarUIT", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }

    }
}