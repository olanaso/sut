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

namespace Sut.Web.Areas.Seguridad.Controllers
{
    [Autorizacion]
    public class AyudaController : Controller
    {

        private readonly ILogService<AyudaController> _log;
        private readonly IPreguntasFrecuentesService _PreguntasFrecuentesService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly IUsuarioService _usuarioService;
        Auditoria objauditoria = new Auditoria();
        public AyudaController(IAuditoriaService AuditoriaService, IPreguntasFrecuentesService PreguntasFrecuentesService,
                              IUsuarioService usuarioService)
        {
            _log = new LogService<AyudaController>();
            _PreguntasFrecuentesService = PreguntasFrecuentesService;
            _AuditoriaService = AuditoriaService;
            _usuarioService = usuarioService;
        }

        ////////[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Lista(UsuarioInfo user)
        {


            ViewBag.Usuario = user;
            return View();
        }

        public string GetAllLikePagin(PreguntasFrecuentes filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;
            int cont = 0;

            if (user.Rol == 1)
            {
                List<PreguntasFrecuentes> lista = _PreguntasFrecuentesService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

                return JsonConvert.SerializeObject(new
                {
                    lista = lista.Select(x => new
                    {
                        PreguntasID = x.PreguntasID,
                        nombreCompleto = x.NombreCompleto,
                        telefono = x.Telefono,
                        anexo = x.Anexo,
                        descripcion = x.Descripcion,
                        estado = x.Estado,
                        estadoPublicar = x.EstadoPublicar,
                        titulo = x.Titulo,
                        contenido = x.Contenido,
                        FecCreacion = x.FecCreacion.Value.ToShortDateString(),
                        FecModificacion = x.FecModificacion.Value.ToShortDateString(),

                    }),
                    cont = 1,
                    totalRows = totalRows
                });
            }
            else
            {
                filtro.EntidadId = user.EntidadId;
                List<PreguntasFrecuentes> lista = _PreguntasFrecuentesService.GetAllLikePaginEntidad(filtro, pageIndex, pageSize, ref totalRows);

                return JsonConvert.SerializeObject(new
                {
                    lista = lista.Select(x => new
                    {
                        PreguntasID = x.PreguntasID,
                        nombreCompleto = x.NombreCompleto,
                        telefono = x.Telefono,
                        anexo = x.Anexo,
                        descripcion = x.Descripcion,
                        estado = x.Estado,
                        estadoPublicar = x.EstadoPublicar,
                        titulo = x.Titulo,
                        contenido = x.Contenido

                    }),
                    cont = 2,
                    totalRows = totalRows
                });
            }
        }

        public string GetAllLikePaginAcordion(PreguntasFrecuentes filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;

            filtro.EstadoPublicar = Respuesta.Si;

            List<PreguntasFrecuentes> lista = _PreguntasFrecuentesService.GetAllLikePaginAcordion(filtro, pageIndex, pageSize, ref totalRows);

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    PreguntasID = x.PreguntasID,
                    nombreCompleto = x.NombreCompleto,
                    telefono = x.Telefono,
                    anexo = x.Anexo,
                    descripcion = x.Descripcion,
                    estado = x.Estado,
                    estadoPublicar = x.EstadoPublicar,
                    titulo = x.Titulo,
                    contenido = x.Contenido

                }),
                totalRows = totalRows
            });

        }

        public ActionResult Editar(long id, UsuarioInfo user)
        {
            try
            {
                ViewBag.Usuario = user;
                PreguntasFrecuentes item;
                if (id == 0)
                {
                    item = new PreguntasFrecuentes();
                    ViewBag.Title = "Nueva Pregunta";
                }
                else
                {
                    item = _PreguntasFrecuentesService.GetOne(id);
                    ViewBag.Title = "Editar Pregunta";
                }
                ViewBag.respuesta = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Si", Value = "1" , Selected = item.RespuestaTelefono == Respuesta.Si ? true:false},
                    new SelectListItem() { Text = "No", Value = "0",  Selected = item.RespuestaTelefono == Respuesta.No ? true:false },
                };

                ViewBag.publicar = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Si", Value = "1" , Selected = item.EstadoPublicar == Respuesta.Si ? true:false},
                    new SelectListItem() { Text = "No", Value = "0",  Selected = item.EstadoPublicar == Respuesta.No ? true:false },
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

        public ActionResult ActvarAyuda(string documento, int activar, UsuarioInfo user)
        {
            try
            {

                Usuario rep = _usuarioService.GetOne(documento);

                string mensaje = "ok";
                user.Ayuda = activar;
                rep.Ayuda = activar;
                _usuarioService.Save(rep);

                return Json(new
                {
                    mensaje = mensaje
                });

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
        public ActionResult Guardar(PreguntasFrecuentes model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.PreguntasID == 0 ? "La Pregunta fue registrada satisfactoriamente."
                                                : "La Pregunta fue modificada satisfactoriamente.";

                    PreguntasFrecuentes obj;

                    if (model.PreguntasID == 0)
                    {
                        obj = new PreguntasFrecuentes();
                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;
                        obj.Estado = "1";
                        if (user.Rol == 1)
                        {
                            obj.EntidadId = user.EntidadId;
                            obj.Telefono = "admin";
                            obj.Anexo = "admin";
                            obj.NombreCompleto = user.NombreCompleto;
                            obj.Descripcion = "admin";
                        }
                        else
                        {
                            obj.EntidadId = user.EntidadId;
                            obj.Telefono = model.Telefono;
                            obj.Anexo = model.Anexo;
                            obj.NombreCompleto = user.NombreCompleto;
                            obj.Descripcion = model.Descripcion;
                        }

                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Ayuda";
                        objauditoria.Pantalla = "Ayuda";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/

                    }
                    else
                    {
                        obj = _PreguntasFrecuentesService.GetOne(model.PreguntasID);
                        obj.Estado = "2";

                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Ayuda";
                        objauditoria.Pantalla = "Ayuda";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }


                    obj.Titulo = model.Titulo;
                    obj.Contenido = model.Contenido;
                    obj.EstadoPublicar = model.EstadoPublicar;
                    obj.Correo = model.Correo;

                    obj.UserModificacion = user.NroDocumento;
                    obj.FecModificacion = DateTime.Now;
                    _PreguntasFrecuentesService.Save(obj);

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
                ModelState.AddModelError("editarPreguntasFrecuentes", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }

    }
}