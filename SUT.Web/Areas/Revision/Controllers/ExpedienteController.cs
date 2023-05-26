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

namespace Sut.Web.Areas.Revision.Controllers
{
    [Authorize]
    public class ExpedienteController : Controller
    {
        private readonly ILogService<ExpedienteController> _log;
        private readonly IExpedienteService _expedienteService;
        private readonly ISeguridadService _seguridadService;
        private readonly IAuditoriaService _AuditoriaService;
        Auditoria objauditoria = new Auditoria();
        public ExpedienteController(IExpedienteService expedienteService,
                                    IAuditoriaService AuditoriaService,
                                    ISeguridadService seguridadService)
        {
            _log = new LogService<ExpedienteController>();
            _expedienteService = expedienteService;
            _seguridadService = seguridadService;
            _AuditoriaService = AuditoriaService;
        }

        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult PendienteRevision()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("_Error");
                throw ex;
            }
        }

        public string GetAllLikePagin(Expediente filtro, int pageIndex, int pageSize)
        {
            int totalRows = 0;

            List<Expediente> lista = _expedienteService.PresentadosGetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    ExpedienteId = x.ExpedienteId,
                    Codigo = x.Codigo,
                    Entidad = x.Entidad.Nombre,
                    FechaPresentacion = x.FecPresentacion == null ? "" : x.FecPresentacion.Value.ToShortDateString()
                }),
                totalRows = totalRows
            });
        }

        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Aprobar(long id, UsuarioInfo user)
        {
            try
            {
                _expedienteService.CambiarEstado(id, EstadoExpediente.Aprobado);

                /*auditoria actualizar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Aprobar Expediente";
                objauditoria.Pantalla = "Expediente";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/


                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/

                return Json(new { valid = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Observar(long id, UsuarioInfo user)
        {
            try
            {
                _expedienteService.CambiarEstado(id, EstadoExpediente.Observado);
                /*auditoria actualizar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Observar Expediente";
                objauditoria.Pantalla = "Expediente";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/


                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/


                return Json(new { valid = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Administrar(UsuarioInfo user)
        {
            try
            {
                //HelperSession.Usuario = _seguridadService.GetUsuario("DNI", HelperSession.Usuario.NroDocumento);

                user.EntidadId = 0;
                user.NomEntidad = string.Empty;
                user.TipoTupa = (short)TipoTupa.Normal;

                string userData = JsonConvert.SerializeObject(user);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "ticket", DateTime.Now, DateTime.Now.AddDays(1), false, userData, FormsAuthentication.FormsCookiePath);
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                authCookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(authCookie);

                return RedirectToAction("Lista", "Estandar/Tupa", new { @area = "" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
