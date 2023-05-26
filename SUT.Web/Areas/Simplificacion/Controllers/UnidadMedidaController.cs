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
    public class UnidadMedidaController : Controller
    {
        private readonly ILogService<UnidadMedidaController> _log;
        private readonly IUnidadMedidaService _unidadMedidaService;
        private readonly IAuditoriaService _AuditoriaService;
        Auditoria objauditoria = new Auditoria();
        public UnidadMedidaController(IAuditoriaService AuditoriaService, IUnidadMedidaService unidadMedidaService)
        {
            _log = new LogService<UnidadMedidaController>();
            _unidadMedidaService = unidadMedidaService;
            _AuditoriaService = AuditoriaService;
        }

        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Lista()
        {
            ViewBag.TipoRecurso = 1;
            return View();
        }

        public string GetAllLikePagin(UnidadMedida filtro, int pageIndex, int pageSize)
        {
            int totalRows = 0;
            List<UnidadMedida> lista = _unidadMedidaService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    UnidadMedidaId = x.UnidadMedidaId,
                    Abreviatura = x.Abreviatura,
                    Nombre = x.Nombre
                }),
                totalRows = totalRows
            });
        }

        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Editar(long id)
        {
            try
            {
                UnidadMedida item;
                if (id == 0)
                {
                    item = new UnidadMedida();
                    ViewBag.Title = "Nueva Unidad de Medida";
                }
                else
                {
                    item = _unidadMedidaService.GetOne(id);
                    ViewBag.Title = "Editar Unidad de Medida";
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
        public ActionResult Guardar(UnidadMedida model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.UnidadMedidaId == 0 ? "La Unidad de Medida fue registrada satisfactoriamente."
                                                : "La Unidad de Medida fue modificada satisfactoriamente.";

                    UnidadMedida obj;

                    if (model.UnidadMedidaId == 0)
                    {
                        obj = new UnidadMedida();
                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;

                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Unidad de Medida";
                        objauditoria.Pantalla = "Unidad de Medida";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }
                    else
                    {
                        obj = _unidadMedidaService.GetOne(model.UnidadMedidaId);

                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Unidad de Medida";
                        objauditoria.Pantalla = "Unidad de Medida";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }

                    obj.Abreviatura = model.Abreviatura;
                    obj.Nombre = model.Nombre;
                    obj.Activo = true;

                    obj.UserModificacion = user.NroDocumento;
                    obj.FecModificacion = DateTime.Now;
                    _unidadMedidaService.Save(obj);
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
                ModelState.AddModelError("editarUnidadMedida", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }
        //public ActionResult Select(bool multi, string fnAdd)
        //{
        //    try
        //    {
        //        ViewBag.Multi = multi;
        //        ViewBag.FnAdd = fnAdd;

        //        return PartialView("_SelectServicio");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}