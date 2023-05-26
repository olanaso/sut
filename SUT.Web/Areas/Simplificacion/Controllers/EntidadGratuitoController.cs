using Newtonsoft.Json;
using Sut.Entities;
using Sut.IApplicationServices;
using Sut.Log;
using Sut.Security.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Sut.Web.Areas.Simplificacion.Controllers
{
    [Autorizacion]
    public class EntidadGratuitoController : Controller
    {
        private readonly ILogService<EntidadGratuitoController> _log;
        private readonly IEntidadService _entidadService;
        private readonly IMetaDatoService _metaDatoService;
        private readonly IDepartamentoService _departamentoService;
        private readonly IProvinciaService _provinciaService;
        private readonly IAuditoriaService _AuditoriaService;
        Auditoria objauditoria = new Auditoria();
        public EntidadGratuitoController(IEntidadService entidadService,
                                IMetaDatoService metaDatoService,
                                IDepartamentoService departamentoService,
                                IAuditoriaService AuditoriaService,
                                IProvinciaService provinciaService)
        {
            _log = new LogService<EntidadGratuitoController>();
            _entidadService = entidadService;
            _metaDatoService = metaDatoService;
            _departamentoService = departamentoService;
            _provinciaService = provinciaService;
            _AuditoriaService = AuditoriaService;
        }

        public ActionResult Lista()
        {
            List<MetaDato> listaGobierno = _metaDatoService.GetByParent(43);
            listaGobierno.Insert(0, new MetaDato() { MetaDatoId = 0, Nombre = " - TODOS - " });
            ViewBag.ListaGobierno = JsonConvert.SerializeObject(listaGobierno.Select(x => new SelectListItem() { Value = x.MetaDatoId.ToString(), Text = x.Nombre }).ToList());

            List<MetaDato> listaSector = _metaDatoService.GetByParent(47);
            listaSector = listaSector.OrderBy(x => x.Nombre).ToList();
            listaSector.Insert(0, new MetaDato() { MetaDatoId = 0, Nombre = " - TODOS - " });
            ViewBag.ListaSector = JsonConvert.SerializeObject(listaSector.Select(x => new SelectListItem() { Value = x.MetaDatoId.ToString(), Text = x.Nombre }).ToList());

            List<Departamento> listaDepartamento = _departamentoService.GetAll();
            listaDepartamento = listaDepartamento.OrderBy(x => x.Nombre).ToList();
            listaDepartamento.Insert(0, new Departamento() { DepartamentoId = 0, Nombre = " - TODOS - " });
            ViewBag.ListaDepartamento = JsonConvert.SerializeObject(listaDepartamento.Select(x => new SelectListItem() { Value = x.DepartamentoId.ToString(), Text = x.Nombre }).ToList());


            List<Provincia> listaProvincia = new List<Provincia>();
            listaProvincia = listaProvincia.OrderBy(x => x.Nombre).ToList();
            listaProvincia.Insert(0, new Provincia() { ProvinciaId = 0, Nombre = " - TODOS - " });
            ViewBag.ListaProvincia = JsonConvert.SerializeObject(listaProvincia.Select(x => new SelectListItem() { Value = x.ProvinciaId.ToString(), Text = x.Nombre }).ToList());



            return View();
        }

        public string GetAllLikePagin(Entidad filtro, int pageIndex, int pageSize)
        {

            int totalRows = 0;
            List<Entidad> lista = _entidadService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

            return JsonConvert.SerializeObject(new
            {
                data = lista.Select(x => new
                {
                    EntidadId = x.EntidadId,
                    Nombre = x.Nombre + '-' + x.Acronimo,
                    Codigo = x.Codigo,
                    Acronimo = x.Acronimo,
                    NivelGobierno = x.NivelGobierno.Nombre,
                    Sector = x.Sector.Nombre,
                    Provincia = x.Provincia == null ? "" : x.Provincia.Nombre,
                    Departamento = x.Provincia == null ? "" : x.Provincia.Departamento.Nombre,
                    Estado = x.ProcesoGratuito == 1 ? "Activo" : "Inactivo",
                }),
                total = totalRows,
            });
        }


        public ActionResult GetProvincias(long id)
        {
            try
            {
                List<Provincia> listaProvincia = new List<Provincia>();
                listaProvincia = _provinciaService.GetByDepartamento(id);
                listaProvincia = listaProvincia.OrderBy(x => x.Nombre).ToList();
                listaProvincia.Insert(0, new Provincia() { ProvinciaId = 0, Nombre = " - SELECCIONAR - " });

                return Json(new { data = listaProvincia.Select(x => new { ProvinciaId = x.ProvinciaId, Nombre = x.Nombre }).ToList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                _log.Error(ex);
                return PartialView("_Error");
            }
        }

        public ActionResult GetProvinciasGrid(long id)
        {
            try
            {
                List<Provincia> listaProvincia = new List<Provincia>();
                listaProvincia = _provinciaService.GetByDepartamento(id);
                listaProvincia = listaProvincia.OrderBy(x => x.Nombre).ToList();
                listaProvincia.Insert(0, new Provincia() { ProvinciaId = 0, Nombre = " - TODOS - " });

                return Json(new { data = listaProvincia.Select(x => new { ProvinciaId = x.ProvinciaId, Nombre = x.Nombre }).ToList() }, JsonRequestBehavior.AllowGet);
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
        public ActionResult Guardar(long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = "OK";

                    Entidad obj = _entidadService.GetOne(id);

                    if (obj.ProcesoGratuito == 1)
                    {
                        obj.ProcesoGratuito = 0;
                    }
                    else
                    {
                        obj.ProcesoGratuito = 1;
                    }


                    _entidadService.Save(obj);


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