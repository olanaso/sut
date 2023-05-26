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
    public class DistritoController : Controller
    {
        private readonly ILogService<DistritoController> _log;
        private readonly IEntidadService _entidadService;
        private readonly IDistritoService _DistritoService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly IRolMenuService _rolMenuService;
        private readonly IDepartamentoService _departamentoService;
        private readonly IProvinciaService _provinciaService;
        Auditoria objauditoria = new Auditoria();
        public DistritoController(IDistritoService DistritoService,
                                    IAuditoriaService AuditoriaService,
                                    IEntidadService entidadService,
                                    IRolMenuService rolMenuService,
                                IDepartamentoService departamentoService,
                                IProvinciaService provinciaService)
        {
            _DistritoService = DistritoService;
            _entidadService = entidadService;
            _AuditoriaService = AuditoriaService;
            _departamentoService = departamentoService;
            _provinciaService = provinciaService;
            _rolMenuService = rolMenuService;
        }

        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Lista(UsuarioInfo user)
        {
            ViewBag.Usuario = user;


            List<Departamento> listaDepartamento = _departamentoService.GetAll();
            listaDepartamento = listaDepartamento.OrderBy(x => x.Nombre).ToList();
            listaDepartamento.Insert(0, new Departamento() { DepartamentoId = 0, Nombre = " - SELECCIONAR - " });
            ViewBag.ListaDepartamento = listaDepartamento.Select(x => new SelectListItem() { Value = x.DepartamentoId.ToString(), Text = x.Nombre }).ToList();

            ViewBag.ListaDepartamento.RemoveAt(1);

            List<Provincia> listaProvincia = new List<Provincia>();
            listaProvincia = listaProvincia.OrderBy(x => x.Nombre).ToList();
            listaProvincia.Insert(0, new Provincia() { ProvinciaId = 0, Nombre = " - TODOS - " });

            ViewBag.ListaProvincia = listaProvincia.Select(x => new SelectListItem()
            {
                Value = x.ProvinciaId.ToString(),
                Text = x.Nombre
            })
           .ToList();

            return View();
        }


        public ActionResult GetAllLikePagin(Distrito filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            try
            {



                int totalRows = 0;
                List<Distrito> lista = _DistritoService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

                return Json(new
                {
                    lista = lista.Select(x => new
                    {
                        x.DistritoId,
                        Departamento = x.Provincia.Departamento.Nombre.ToString(),
                        Provincia = x.Provincia.Nombre.ToString(),
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

                List<Departamento> listaDepartamento = _departamentoService.GetAll();
                listaDepartamento = listaDepartamento.OrderBy(x => x.Nombre).ToList();
                listaDepartamento.Insert(0, new Departamento() { DepartamentoId = 0, Nombre = " - SELECCIONAR - " });
                ViewBag.ListaDepartamento = listaDepartamento.Select(x => new SelectListItem() { Value = x.DepartamentoId.ToString(), Text = x.Nombre }).ToList();

                ViewBag.ListaDepartamento.RemoveAt(1);

                List<Provincia> listaProvincia = new List<Provincia>();


                Distrito item;
                if (id == 0)
                {
                    item = new Distrito();
                    ViewBag.Title = "Nuevo Distrito";
                }
                else
                {
                    item = _DistritoService.GetOne(id);
                    ViewBag.Title = "Editar Distrito";

                    if (item.Provincia != null)
                    {
                        item.Provincia.DepartamentoId = item.Provincia.DepartamentoId;

                        listaProvincia = _provinciaService.GetByDepartamento(item.Provincia.DepartamentoId);
                        listaProvincia = listaProvincia.OrderBy(x => x.Nombre).ToList();
                    }
                }


                listaProvincia.Insert(0, new Provincia() { ProvinciaId = 0, Nombre = " - SELECCIONAR - " });
                ViewBag.ListaProvincia = listaProvincia.Select(x => new SelectListItem() { Value = x.ProvinciaId.ToString(), Text = x.Nombre }).ToList();
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
        public ActionResult Guardar(Distrito model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.DistritoId == 0 ? "El Distrito fue registrado satisfactoriamente."
                                                : "El Distrito fue modificado satisfactoriamente.";

                    Distrito obj;

                    if (model.DistritoId == 0)
                    {

                        Distrito item = _DistritoService.GetOne(model.DistritoId);


                        obj = new Distrito();
                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;

                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Distrito";
                        objauditoria.Pantalla = "Distrito";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/

                    }
                    else
                    {
                        obj = _DistritoService.GetOne(model.DistritoId);


                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Distrito";
                        objauditoria.Pantalla = "Distrito";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }


                    obj.Nombre = model.Nombre;
                    obj.ProvinciaId = model.ProvinciaId;

                    obj.UserModificacion = user.NroDocumento;
                    obj.FecModificacion = DateTime.Now;
                    _DistritoService.Save(obj);

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