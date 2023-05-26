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

namespace Sut.Web.Areas.General.Controllers
{
    [Authorize]
    public class DatoController : Controller
    {
        private readonly ILogService<DatoController> _log;
        private readonly IMetaDatoService _metaDatoService;
        private readonly IAuditoriaService _AuditoriaService;

        public DatoController(IAuditoriaService AuditoriaService, IMetaDatoService metaDatoService)
        {
            _log = new LogService<DatoController>();
            _metaDatoService = metaDatoService;
            _AuditoriaService = AuditoriaService;
        }

        ////[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Lista(long id)
        {
            if (id == 12)
            {
                ViewBag.TipoRecurso = 2;
            }
            else if (id == 15)
            {
                ViewBag.TipoRecurso = 3;
            }


            MetaDato padre = _metaDatoService.GetOne(id);
            return View(padre);
        }

        public string GetAllLikePagin(long id)
        {
            List<MetaDato> lista = _metaDatoService.GetByParent(id);

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    MetaDatoId = x.MetaDatoId,
                    Codigo = x.Codigo,
                    Nombre = x.Nombre
                }),
                totalRows = lista.Count()
            });
        }

        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Editar(long id, long PadreId)
        {
            try
            {
                MetaDato item;
                if (id == 0)
                {
                    item = new MetaDato();
                    item.PadreId = PadreId;
                    ViewBag.Title = "Nuevo Dato";
                }
                else
                {
                    item = _metaDatoService.GetOne(id);
                    ViewBag.Title = "Editar Dato";
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

        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Eliminar(long id, UsuarioInfo user)
        {
            try
            {
                Auditoria objauditoria = new Auditoria();
                /*auditoria actualizar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Eliminar Dato";
                objauditoria.Pantalla = "Dato";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/

                _metaDatoService.Eliminar(id);


                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
                return Json(new
                {
                    mensaje = "El tipo de normal se elimino satisfactoriamente."
                });

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                string msj = "El tipo de norma se encuentra en uso.";
                ModelState.AddModelError("", msj.ToString());
                _log.Error(ex);
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
                    string mensaje = model.MetaDatoId == 0 ? "El Dato fue registrado satisfactoriamente."
                                                : "El Dato fue modificado satisfactoriamente.";
                    Auditoria objauditoria = new Auditoria();
                    MetaDato obj;

                    if (model.MetaDatoId == 0)
                    {
                        obj = new MetaDato();
                        obj.PadreId = model.PadreId;
                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;


                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Dato";
                        objauditoria.Pantalla = "Dato";
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
                        objauditoria.Actividad = "Modificar Dato";
                        objauditoria.Pantalla = "Dato";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }

                    obj.Nombre = model.Nombre;
                    obj.Valor01 = model.Valor01;
                    obj.Valor02 = model.Valor02;

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
                ModelState.AddModelError("editarMetaDato", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }

        public ActionResult Select(bool multi, string fnAdd)
        {
            try
            {
                ViewBag.Multi = multi;
                ViewBag.FnAdd = fnAdd;

                return PartialView("_Select");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}