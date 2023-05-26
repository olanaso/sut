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
    public class MantenimientoController : Controller
    {
        private readonly ILogService<MantenimientoController> _log;
        private readonly IEntidadService _entidadService;
        private readonly IMetaDatoService _metaDatoService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly IRolMenuService _rolMenuService;
        Auditoria objauditoria = new Auditoria();
        public MantenimientoController(IMetaDatoService metaDatoService,
                                    IAuditoriaService AuditoriaService,
                                    IEntidadService entidadService,
                                    IRolMenuService rolMenuService)
        {
            _metaDatoService = metaDatoService;
            _entidadService = entidadService;
            _AuditoriaService = AuditoriaService;
            _rolMenuService = rolMenuService;
        }

        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Lista(UsuarioInfo user)
        {
            ViewBag.Usuario = user;

            return View();
        }


        public ActionResult GetAllLikePagin(MetaDato filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            try
            {
                int totalRows = 0;
                List<MetaDato> lista = _metaDatoService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

                return Json(new
                {
                    lista = lista.Select(x => new
                    {
                        x.MetaDatoId,
                        x.PadreId,
                        x.TipoRegla,
                        x.Nombre,
                        Fecha = x.FecCreacion == null ? "" : x.FecCreacion.Value.ToString("dd/MM/yyyy"),
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


        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Editar(long id, UsuarioInfo user)
        {
            try
            {
                ViewBag.User = user;

                MetaDato item;
                if (id == 0)
                {
                    item = new MetaDato();
                    ViewBag.Title = "Nuevo Sector";
                }
                else
                {
                    item = _metaDatoService.GetOne(id);
                    ViewBag.Title = "Editar Sector";
                }

                ViewBag.tiporegla = new List<SelectListItem>()
                {

                    new SelectListItem() { Text = "Evaluador", Value = "1",  Selected = item.TipoRegla == TipoRegla.Evaluador ? true:false },
                    new SelectListItem() { Text = "Ratificador", Value = "2" , Selected = item.TipoRegla == TipoRegla.Ratificador ? true:false},
                    new SelectListItem() { Text = "Ninguno", Value = "3" , Selected = item.TipoRegla == TipoRegla.Ninguno ? true:false},
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
        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Guardar(MetaDato model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.MetaDatoId == 0 ? "El Sector fue registrado satisfactoriamente."
                                                : "El Sector fue modificado satisfactoriamente.";

                    MetaDato obj;

                    if (model.MetaDatoId == 0)
                    {

                        MetaDato item = _metaDatoService.GetOne(model.MetaDatoId);


                        obj = new MetaDato();
                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;
                        obj.PadreId = 47;
                        int cantidad = _metaDatoService.GetByParent(47).Count() + 1;
                        obj.Codigo = Convert.ToString(cantidad);

                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Sector";
                        objauditoria.Pantalla = "Mantenimiento";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/

                    }
                    else
                    {
                        obj = _metaDatoService.GetOne(model.MetaDatoId);


                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Sector";
                        objauditoria.Pantalla = "Mantenimiento";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }


                    obj.Nombre = model.Nombre;
                    obj.TipoRegla = model.TipoRegla;

                    obj.UserModificacion = user.NroDocumento;
                    obj.FecModificacion = DateTime.Now;
                    _metaDatoService.Save(obj);

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