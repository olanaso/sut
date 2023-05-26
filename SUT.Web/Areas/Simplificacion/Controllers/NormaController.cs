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
    public class NormaController : Controller
    {
        private readonly ILogService<NormaController> _log;
        private readonly INormaService _normaService;
        private readonly IMetaDatoService _metaDatoService;
        private readonly IAuditoriaService _AuditoriaService;
        Auditoria objauditoria = new Auditoria();
        public NormaController(INormaService normaService, IAuditoriaService AuditoriaService,
                                IMetaDatoService metaDatoService)
        {
            _log = new LogService<NormaController>();
            _normaService = normaService;
            _metaDatoService = metaDatoService;
            _AuditoriaService = AuditoriaService;
        }

        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Lista()
        {
            return View();
        }

        public string GetAllLikePagin(Norma filtro, int pageIndex, int pageSize)
        {
            int totalRows = 0;

            List<Norma> lista = _normaService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    NormaId = x.NormaId,
                    TipoNormaId = x.TipoNormaId,
                    TipoNorma = x.TipoNorma.Nombre,
                    Descripcion = x.Descripcion,
                    Numero = x.Numero,
                    FechaPublicacion = x.FechaPublicacion.Value.ToShortDateString(),
                    Sector = x.Sector
                }),
                totalRows = totalRows
            });
        }

        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Editar(long id)
        {
            try
            {
                Norma item;
                if (id == 0)
                {
                    item = new Norma();
                    item.ArchivoAdjuntoId = 0;
                    ViewBag.Title = "Nueva Norma";
                }
                else
                {
                    item = _normaService.GetOne(id);
                    if (item.ArchivoAdjuntoId == null) item.ArchivoAdjuntoId = 0;
                    ViewBag.Title = "Editar Norma";
                }

                List<MetaDato> listMD = _metaDatoService.GetByParent(12);
                ViewBag.TipoNorma = listMD.Select(x => new SelectListItem()
                {
                    Value = x.MetaDatoId.ToString(),
                    Text = x.Nombre
                })
                .ToList();

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
        public ActionResult Guardar(Norma model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.NormaId == 0 ? "La Norma fue registrada satisfactoriamente."
                                                : "La Norma fue modificada satisfactoriamente.";

                    Norma obj;

                    if (model.NormaId == 0)
                    {
                        obj = new Norma();
                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;

                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Norma";
                        objauditoria.Pantalla = "Norma";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }
                    else
                    {
                        obj = _normaService.GetOne(model.NormaId);

                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Norma";
                        objauditoria.Pantalla = "Norma";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }

                    obj.TipoNormaId = model.TipoNormaId;
                    obj.Numero = model.Numero;
                    obj.FechaPublicacion = model.FechaPublicacion;
                    obj.Sector = model.Sector;
                    obj.Descripcion = model.Descripcion;

                    obj.ArchivoAdjuntoId = model.ArchivoAdjuntoId;
                    //obj.ArchivoAdjunto = null;
                    if (model.ArchivoAdjuntoId.Value == 0) obj.ArchivoAdjuntoId = null;

                    obj.UserModificacion = user.NroDocumento;
                    obj.FecModificacion = DateTime.Now;
                    _normaService.Save(obj);

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