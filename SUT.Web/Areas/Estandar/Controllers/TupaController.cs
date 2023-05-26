using Newtonsoft.Json;
using Sut.Entities;
using Sut.IApplicationServices;
using Sut.Log;
using Sut.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Sut.Web.Areas.Estandar.Controllers
{
    [Authorize]
    public class TupaController : Controller
    {
        private readonly ILogService<TupaController> _log;
        private readonly IEntidadService _entidadService;
        private readonly IAuditoriaService _AuditoriaService;

        public TupaController(IAuditoriaService AuditoriaService, IEntidadService entidadService)
        {
            _log = new LogService<TupaController>();
            _entidadService = entidadService;
            _AuditoriaService = AuditoriaService;
        }

        ////[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Lista()
        {
            return View();
        }
        public string GetAllLikePagin(Entidad filtro, int pageIndex, int pageSize)
        {
            int totalRows = 0;

            List<Entidad> lista = _entidadService.EstandarGetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    EntidadId = x.EntidadId,
                    Codigo = x.Codigo,
                    Nombre = x.Nombre,
                    FecCreacion = x.FecCreacion.Value.ToString("dd/MM/yyyy") + ' ' + x.FecCreacion.Value.ToLongTimeString(),
                    FecModificacion = x.FecModificacion.Value.ToString("dd/MM/yyyy") + ' ' + x.FecModificacion.Value.ToLongTimeString(),

                }),
                totalRows = totalRows
            });
        }

        ////[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Editar(long id)
        {
            try
            {
                Entidad item;
                if (id == 0)
                {
                    item = new Entidad();
                    ViewBag.Title = "Nuevo TUPA Estándar";
                }
                else
                {
                    item = _entidadService.GetOne(id);
                    ViewBag.Title = "Editar TUPA Estándar";
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
        ////[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Guardar(Entidad model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.EntidadId == 0 ? "El TUPA Estándar fue registrado satisfactoriamente."
                                                : "El TUPA Estándar fue modificado satisfactoriamente.";

                    Auditoria objauditoria = new Auditoria();
                    Entidad obj;

                    if (model.EntidadId == 0)
                    {
                        obj = new Entidad();
                        obj.TipoTupa = TipoTupa.Estandar;
                        obj.NivelGobiernoId = 44;
                        obj.SectorId = 48;
                        obj.FecCreacion = DateTime.Now;
                        obj.FecModificacion = DateTime.Now;
                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Tupa Estándar";
                        objauditoria.Pantalla = "Tupa Estándar";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }
                    else
                    {
                        obj = _entidadService.GetOne(model.EntidadId);
                        obj.FecModificacion = DateTime.Now;

                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Tupa Estándar";
                        objauditoria.Pantalla = "Tupa Estándar";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }
                    obj.Nombre = model.Nombre;

                    _entidadService.Save(obj);

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
                ModelState.AddModelError("editarNorma", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }

        ////[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Abrir(long id, UsuarioInfo user)
        {
            try
            {
                //HelperSession.Usuario.Entidad = _entidadService.GetOne(id);
                Entidad entidad = _entidadService.GetOne(id);
                user.EntidadId = entidad.EntidadId;
                user.NomEntidad = entidad.Nombre;
                user.TipoTupa = (short)entidad.TipoTupa;

                string userData = JsonConvert.SerializeObject(user);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "ticket", DateTime.Now, DateTime.Now.AddDays(1), false, userData, FormsAuthentication.FormsCookiePath);
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                authCookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(authCookie);

                return RedirectToAction("Lista", "Expediente", new { area = "Simplificacion" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}