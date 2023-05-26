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
    public class ResolucionEquipoController : Controller
    {
        private readonly ILogService<ResolucionEquipoController> _log;
        private readonly IEntidadService _entidadService;
        private readonly IResolucionEquipoService _resolucionEquipoService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly IRolMenuService _rolMenuService;
        Auditoria objauditoria = new Auditoria();
        public ResolucionEquipoController(IResolucionEquipoService resolucionEquipoService, IAuditoriaService AuditoriaService,
                                    IEntidadService entidadService, IRolMenuService rolMenuService)
        {
            _resolucionEquipoService = resolucionEquipoService;
            _entidadService = entidadService;
            _AuditoriaService = AuditoriaService;
            _rolMenuService = rolMenuService;
        }

        [AutorizacionRol(Roles = "Coordinador")]
        public ActionResult Lista(UsuarioInfo user)
        {
            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 4);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/

            ResolucionEquipo listaEntidad = _resolucionEquipoService.GetOneEntidad(user.EntidadId);
            if (listaEntidad != null)
            {
                ViewBag.ResolucionEquipoId = listaEntidad.ResolucionEquipoId;
            }
            else
            {
                ViewBag.ResolucionEquipoId = 0;
            }

            ViewBag.Usuario = user;
            return View();
        }

        public ActionResult ListaByEntidad(long EntidadId, UsuarioInfo user)
        {
            ResolucionEquipo listaEntidad = _resolucionEquipoService.GetOneEntidad(user.EntidadId);
            if (listaEntidad != null)
            {
                ViewBag.ResolucionEquipoId = listaEntidad.ArchivoAdjuntoId;
            }
            else
            {
                ViewBag.ResolucionEquipoId = 0;
            }
            return PartialView("_ListaByEntidad", EntidadId);
        }

        public ActionResult GetAllLikePagin(ResolucionEquipo filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            try
            {
                int totalRows = 0;
                filtro.EntidadId = user.EntidadId;
                List<ResolucionEquipo> lista = _resolucionEquipoService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

                return Json(new
                {
                    lista = lista.Select(x => new
                    {
                        x.ResolucionEquipoId,
                        x.EntidadId,
                        x.Descripcion,
                        x.ArchivoAdjuntoId,
                        Fecha = x.Fecha.ToShortDateString(),
                        x.Principal
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

        public ActionResult GetAllLikePaginByEntidad(ResolucionEquipo filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            try
            {

                int totalRows = 0;
                List<ResolucionEquipo> lista = _resolucionEquipoService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

                return Json(new
                {
                    lista = lista.Select(x => new
                    {
                        x.ResolucionEquipoId,
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
                List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 4);
                user.rolmenu = listarolmenu.ToList();
                /*Fin Cargar roles  - Acceso*/

                ViewBag.User = user;

                ResolucionEquipo item;
                if (id == 0)
                {
                    item = new ResolucionEquipo();
                    ViewBag.Title = "Nueva Documento de Equipo";
                    item.EntidadId = user.EntidadId;
                }
                else
                {
                    item = _resolucionEquipoService.GetOne(id);
                    ViewBag.Title = "Editar Documento de Equipo";
                }

                ViewBag.EstadoGeneral = 0;
                return PartialView("_Editar", item);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }


        [AutorizacionRol(Roles = "Coordinador")]
        public ActionResult EditarGeneral(long id, UsuarioInfo user)
        {
            try
            {
                ViewBag.User = user;

                ResolucionEquipo item;
                if (id == 0)
                {
                    item = new ResolucionEquipo();
                    ViewBag.Title = "Nueva Documento de Equipo";
                    item.EntidadId = user.EntidadId;
                }
                else
                {
                    item = _resolucionEquipoService.GetOne(id);
                    ViewBag.Title = "Editar Documento de Equipo";
                }
                ViewBag.EstadoGeneral = 1;

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
        public ActionResult Guardar(ResolucionEquipo model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.ResolucionEquipoId == 0 ? "El Documento de Equipo fue registrada satisfactoriamente."
                                                : "El Documento de Equipo fue modificada satisfactoriamente.";

                    ResolucionEquipo obj;

                    if (model.ResolucionEquipoId == 0)
                    {
                        ResolucionEquipo item = _resolucionEquipoService.GetOneEntidad(user.EntidadId);
                        if (item != null)
                        {
                            item.Principal = "0";
                            _resolucionEquipoService.Save(item);
                        }
                        obj = new ResolucionEquipo();
                        obj.EntidadId = user.EntidadId;
                        obj.Principal = "1";
                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;

                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Documento de Equipo";
                        objauditoria.Pantalla = "Documento de Equipo";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }
                    else
                    {
                        obj = _resolucionEquipoService.GetOne(model.ResolucionEquipoId);

                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Documento de Equipo";
                        objauditoria.Pantalla = "Documento de Equipo";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }

                    obj.Fecha = DateTime.Now;
                    obj.Descripcion = model.Descripcion;

                    obj.ArchivoAdjuntoId = model.ArchivoAdjuntoId;

                    obj.UserModificacion = user.NroDocumento;
                    obj.FecModificacion = DateTime.Now;
                    _resolucionEquipoService.Save(obj);

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

        [HttpPost]
        [AutorizacionRol(Roles = "Coordinador")]
        public ActionResult GuardarGeneral(ResolucionEquipo model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.ResolucionEquipoId == 0 ? "El Documento de Equipo fue registrada satisfactoriamente."
                                                : "El Documento de Equipo fue modificada satisfactoriamente.";

                    ResolucionEquipo obj;

                    if (model.ResolucionEquipoId == 0)
                    {
                        obj = new ResolucionEquipo();
                        obj.EntidadId = user.EntidadId;

                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;

                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Documento de Equipo";
                        objauditoria.Pantalla = "Documento de Equipo";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }
                    else
                    {
                        obj = _resolucionEquipoService.GetOne(model.ResolucionEquipoId);

                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Documento de Equipo";
                        objauditoria.Pantalla = "Documento de Equipo";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }

                    obj.Fecha = DateTime.Now;
                    obj.Descripcion = model.Descripcion;
                    obj.ArchivoAdjuntoId = model.ArchivoAdjuntoId;

                    obj.UserModificacion = user.NroDocumento;
                    obj.FecModificacion = DateTime.Now;
                    obj.Principal = "1";
                    _resolucionEquipoService.Save(obj);


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