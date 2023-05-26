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
    public class RecursoController : Controller
    {
        private readonly ILogService<RecursoController> _log;
        private readonly IRecursoService _recursoService;
        private readonly IUnidadMedidaService _unidadMedidaService;
        private readonly IMetaDatoService _metaDatoService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly IDetalleMaestroService _DetalleMaestroService;
        private readonly IRolMenuService _rolMenuService;
        Auditoria objauditoria = new Auditoria();
        public RecursoController(IRecursoService recursoService,
                                IUnidadMedidaService unidadMedidaService, IAuditoriaService AuditoriaService,
                                IMetaDatoService metaDatoService, IDetalleMaestroService detalleMaestroService, IRolMenuService rolMenuService)
        {
            _log = new LogService<RecursoController>();
            _recursoService = recursoService;
            _unidadMedidaService = unidadMedidaService;
            _metaDatoService = metaDatoService;
            _AuditoriaService = AuditoriaService;
            _DetalleMaestroService = detalleMaestroService;
            _rolMenuService = rolMenuService;
        }

        string[] tipos = { "",
            "PERSONAL DIRECTO",
            "MATERIAL FUNGIBLE",
            "SERVICIO IDENTIFICABLE",
            "MATERIAL NO FUNGIBLE",
            "SERVICIO NO IDENTIFICABLE",
            "DEPRECIACIÓN",
            "COSTO FIJO"
        };

        public ActionResult ListaPersonal(UsuarioInfo user)
        {
            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 10);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/

            ViewBag.TipoRecurso = 1;
            ViewBag.Title = "Personal";
            ViewBag.Usuario = user;
            ViewBag.codigomenu = 8;
            return View("Lista");
        }
        public ActionResult ListaMaterialFungible(UsuarioInfo user)
        {
            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 11);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/
            ViewBag.TipoRecurso = 2;
            ViewBag.Title = "Materiales Fungibles";
            ViewBag.Usuario = user;
            ViewBag.codigomenu = 9;
            return View("Lista");
        }

        public ActionResult ListaServicioIdentificable(UsuarioInfo user)
        {
            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 12);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/

            ViewBag.TipoRecurso = 3;
            ViewBag.Title = "Servicios Identificables";
            ViewBag.Usuario = user;
            ViewBag.codigomenu = 10;
            return View("Lista");
        }

        public ActionResult ListaMaterialNoFungible(UsuarioInfo user)
        {
            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 13);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/

            ViewBag.TipoRecurso = 4;
            ViewBag.Title = "Materiales No Fungibles";
            ViewBag.Usuario = user;
            ViewBag.codigomenu = 11;
            return View("Lista");
        }

        public ActionResult ListaServicioTercero(UsuarioInfo user)
        {
            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 14);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/

            ViewBag.TipoRecurso = 5;
            ViewBag.Title = "Servicios no Identificables";
            ViewBag.Usuario = user;
            ViewBag.codigomenu = 12;
            return View("Lista");
        }

        public ActionResult ListaDepreciacion(UsuarioInfo user)
        {

            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 15);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/

            ViewBag.TipoRecurso = 6;
            ViewBag.Title = "Depreciación y Amortización";
            ViewBag.Usuario = user;
            ViewBag.codigomenu = 13;
            return View("Lista");
        }

        public ActionResult ListaCostoFijo(UsuarioInfo user)
        {
            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 16);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/

            ViewBag.TipoRecurso = 7;
            ViewBag.Title = "Costos Fijos";
            ViewBag.Usuario = user;
            ViewBag.codigomenu = 14;
            return View("Lista");
        }

        public string GetAllLikePagin(Recurso filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;
            filtro.EntidadId = user.EntidadId;

            List<Recurso> lista = _recursoService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

            System.Web.HttpContext.Current.Session["ListRecurso"] = lista;


            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    RecursoId = x.RecursoId,
                    Nombre = x.Nombre,
                    Tipo = this.tipos[(int)x.TipoRecurso],
                    TipoId = (int)x.TipoRecurso
                }),
                totalRows = totalRows
            });
        }
        public string GetAllLikePaginSinTipo(Recurso filtro, int tipo, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;
            filtro.EntidadId = user.EntidadId;

            List<Recurso> lista = _recursoService.GetAllLikePagin(filtro, tipo, pageIndex, pageSize, ref totalRows);

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    RecursoId = x.RecursoId,
                    Nombre = x.Nombre,
                    Tipo = this.tipos[(int)x.TipoRecurso],
                    TipoId = (int)x.TipoRecurso
                }),
                totalRows = totalRows
            });
        }

        public string sessionlistarecursos(long TablaAsmeId, List<ListaRecurso> lstTablaAsmeS, Security.UsuarioInfo user)
        {
            long totalRows = TablaAsmeId;

            List<ListaRecurso> lstTablaAsme = new List<ListaRecurso>();

            lstTablaAsme = lstTablaAsmeS.OrderBy(x => x.numero).ToList();

            System.Web.HttpContext.Current.Session["ListRecursoNumero"] = (List<ListaRecurso>)lstTablaAsme;

            return JsonConvert.SerializeObject(new
            {

                totalRows = totalRows
            });
        }

        public ActionResult Editar(long id, TipoRecurso tipo, UsuarioInfo user)
        {
            try
            {
                List<RolMenu> listarolmenu = new List<RolMenu>();
                /*Inicio Cargar roles - Acceso*/
                if (tipo == TipoRecurso.Personal)
                {
                    listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 10);
                }
                else if (tipo == TipoRecurso.MaterialFungible)
                {
                    listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 11);
                }
                else if (tipo == TipoRecurso.ServicioIdentificable)
                {
                    listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 12);
                }
                else if (tipo == TipoRecurso.MaterialNoFungible)
                {
                    listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 13);
                }
                else if (tipo == TipoRecurso.ServicioTerceros)
                {
                    listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 14);
                }
                else if (tipo == TipoRecurso.Depreciacion)
                {
                    listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 15);
                }
                else if (tipo == TipoRecurso.Fijos)
                {
                    listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 16);
                }
                /*Fin Cargar roles  - Acceso*/
                user.rolmenu = listarolmenu.ToList();
                ViewBag.User = user;


                Recurso item;
                if (id == 0)
                {
                    item = new Recurso();
                    item.EntidadId = user.EntidadId;
                    item.TipoRecurso = tipo;
                    ViewBag.Title = "Nuevo " + tipos[(int)tipo];
                }
                else
                {
                    item = _recursoService.GetOne(id);
                    ViewBag.Title = "Editar " + tipos[(int)tipo];
                }

                List<UnidadMedida> listUM = _unidadMedidaService.GetAll();
                foreach (var um in listUM)
                    um.Nombre = string.Format("{0} - {1}", um.Abreviatura, um.Nombre);
                listUM.Insert(0, new UnidadMedida() { UnidadMedidaId = 0, Nombre = "- SELECCIONAR -" });

                ViewBag.UnidadMedida = listUM.Select(x => new SelectListItem()
                {
                    Value = x.UnidadMedidaId.ToString(),
                    Text = x.Nombre
                })
                                            .ToList();

                //if (tipo == TipoRecurso.Depreciacion)
                //{
                List<MetaDato> listMD = _metaDatoService.GetByParent(1);
                listMD.Insert(0, new MetaDato() { MetaDatoId = 0, Nombre = "- SELECCIONAR -" });
                ViewBag.TipoDepreciacion = listMD.Select(x => new SelectListItem()
                {
                    Value = x.MetaDatoId.ToString(),
                    Text = x.Nombre
                })
                                            .ToList();
                //}
                ViewBag.Id = id;


                //Combo para cargar la data maestra de inicio 
                ViewBag.ListRecurso = System.Web.HttpContext.Current.Session["ListRecurso"];

                TipoDetalleMaestro tipodetalle = (TipoDetalleMaestro)tipo;

                List<DetalleMaestro> listDM = _DetalleMaestroService.GetAllXTIPO(tipodetalle);
                List<Recurso> lista = ViewBag.ListRecurso;


                //validar el contenido de información
                var excludedIDs = new HashSet<string>(lista.Select(p => p.Nombre.Trim()));
                var result = listDM.Where(p => !excludedIDs.Contains(p.Nombre.Trim()));
                //fin



                ViewBag.DetalleMaestro = result.Select(x => new DetalleMaestro()
                {
                    DetalleMaestroId = x.DetalleMaestroId,
                    Nombre = x.Nombre.Trim(),
                    UnidadMedidaId = x.UnidadMedidaId,
                    TipoDepreciacionId = x.TipoDepreciacionId,

                }).ToList();



                //fin




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
        public ActionResult Guardar(Recurso model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.RecursoId == 0 ? "El Recurso fue registrado satisfactoriamente."
                                                : "El Recurso fue modificado satisfactoriamente.";

                    Recurso obj;

                    if (model.RecursoId == 0)
                    {
                        if (model.chkbuscarUndOrg != null)
                        {
                            if (model.chkbuscarUndOrg.Count() != 0)
                            {
                                foreach (var item in model.chkbuscarUndOrg)
                                {

                                    obj = new Recurso();
                                    obj.EntidadId = model.EntidadId;
                                    obj.TipoRecurso = model.TipoRecurso;
                                    obj.UserCreacion = user.NroDocumento;
                                    obj.FecCreacion = DateTime.Now;

                                    //obj.Codigo = model.Codigo;
                                    string valor = item.ToString();
                                    obj.Nombre = model.listaNombreCargado[Convert.ToInt32(valor)];
                                    if (model.TipoRecurso != TipoRecurso.Personal)
                                    {
                                        obj.UnidadMedidaId = model.listaUnidadMedidaIdCargado[Convert.ToInt32(valor)];
                                    }

                                    if (model.TipoRecurso == TipoRecurso.Depreciacion)
                                    {
                                        obj.TipoDepreciacionId = model.listaTipoDepreciacionIdCargado[Convert.ToInt32(valor)];
                                    }
                                    /*auditoria agregar*/
                                    objauditoria.EntidadId = user.EntidadId;
                                    objauditoria.SectorId = user.Sector;
                                    objauditoria.ProvinciaId = user.Provincia;
                                    objauditoria.Usuario = user.NombreCompleto;
                                    objauditoria.Actividad = "Agregar Recursos";
                                    objauditoria.Pantalla = "Recursos";
                                    objauditoria.UserCreacion = user.NroDocumento;
                                    objauditoria.FecCreacion = DateTime.Now;
                                    /*auditoria*/


                                    obj.Activo = true;

                                    obj.UserModificacion = user.NroDocumento;
                                    obj.FecModificacion = DateTime.Now;

                                    obj.RecursoCosto = null;
                                    _recursoService.Save(obj);
                                    /*auditoria Grabar*/
                                    _AuditoriaService.Save(objauditoria);
                                    /*auditoria Grabar*/

                                }
                            }
                        }


                        for (int i = 0; i < model.listaNombre.Count(); i++)
                        {
                            if (model.listaNombre[i] != "")
                            {


                                var resultado = _AuditoriaService.ValidarRecursoId(0, user.EntidadId, model.listaNombre[i].Trim());

                                if (resultado > 0)
                                {

                                    return Json(new
                                    {
                                        mensaje = "El Recurso " + model.listaNombre[i] + " ya fue registrado ",
                                        Estado = "2"
                                    });
                                }




                                obj = new Recurso();
                                obj.EntidadId = model.EntidadId;
                                obj.TipoRecurso = model.TipoRecurso;
                                obj.UserCreacion = user.NroDocumento;
                                obj.FecCreacion = DateTime.Now;

                                //obj.Codigo = model.Codigo;
                                obj.Nombre = model.listaNombre[i];
                                if (model.TipoRecurso != TipoRecurso.Personal)
                                {
                                    obj.UnidadMedidaId = model.listaUnidadMedidaId[i];
                                }

                                if (model.TipoRecurso == TipoRecurso.Depreciacion)
                                {
                                    obj.TipoDepreciacionId = model.listaTipoDepreciacionId[i];
                                }
                                /*auditoria agregar*/
                                objauditoria.EntidadId = user.EntidadId;
                                objauditoria.SectorId = user.Sector;
                                objauditoria.ProvinciaId = user.Provincia;
                                objauditoria.Usuario = user.NombreCompleto;
                                objauditoria.Actividad = "Agregar Recursos";
                                objauditoria.Pantalla = "Recursos";
                                objauditoria.UserCreacion = user.NroDocumento;
                                objauditoria.FecCreacion = DateTime.Now;
                                /*auditoria*/


                                obj.Activo = true;

                                obj.UserModificacion = user.NroDocumento;
                                obj.FecModificacion = DateTime.Now;

                                obj.RecursoCosto = null;
                                _recursoService.Save(obj);
                                /*auditoria Grabar*/
                                _AuditoriaService.Save(objauditoria);
                                /*auditoria Grabar*/

                            }
                        }


                    }
                    else
                    {

                        var resultado = _AuditoriaService.ValidarUnidadOrganicaId(model.RecursoId, user.EntidadId, model.Nombre);

                        if (resultado > 0)
                        {

                            return Json(new
                            {
                                mensaje = "El Recurso " + model.Nombre + " ya fue registrado ",
                                Estado = "2"
                            });
                        }


                        obj = _recursoService.GetOne(model.RecursoId);

                        //obj.Codigo = model.Codigo;
                        obj.Nombre = model.Nombre;
                        obj.UnidadMedidaId = model.UnidadMedidaId;
                        obj.TipoDepreciacionId = model.TipoDepreciacionId;


                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Recursos";
                        objauditoria.Pantalla = "Recursos";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/


                        obj.Activo = true;

                        obj.UserModificacion = user.NroDocumento;
                        obj.FecModificacion = DateTime.Now;

                        obj.RecursoCosto = null;
                        _recursoService.Save(obj);
                        /*auditoria Grabar*/
                        _AuditoriaService.Save(objauditoria);
                        /*auditoria Grabar*/

                    }


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
                ModelState.AddModelError("editarRecurso", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }
        public ActionResult Select(bool multi, string fnAdd, string Nro, string Actividad, string UnidadOrganizacion, string Duracion)
        {
            try
            {
                ViewBag.Multi = multi;
                ViewBag.FnAdd = fnAdd;

                ViewBag.Nro = Nro;
                ViewBag.Actividad = Actividad;
                ViewBag.UnidadOrganizacion = UnidadOrganizacion;
                ViewBag.Duracion = Duracion;

                ViewBag.ListaTipo = this.tipos
                                    .Where(x => !string.IsNullOrEmpty(x))
                                    .Select((x, index) => new SelectListItem()
                                    {
                                        Text = x,
                                        Value = index.ToString()
                                    })
                                    .ToList();

                return PartialView("_Select");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult SelectPlantilla(bool multi, string fnAdd, long tablaAsmeId)
        {
            try
            {
                ViewBag.Multi = multi;
                ViewBag.FnAdd = fnAdd;
                ViewBag.tablaAsmeId = tablaAsmeId;

                return PartialView("_SelectPlantilla");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string Eliminar(long id, UsuarioInfo user)
        {

            var res = false;
            /*auditoria agregar*/
            objauditoria.EntidadId = user.EntidadId;
            objauditoria.SectorId = user.Sector;
            objauditoria.ProvinciaId = user.Provincia;
            objauditoria.Usuario = user.NombreCompleto;
            objauditoria.Actividad = "Eliminar Recurso";
            objauditoria.Pantalla = "Recurso";
            objauditoria.UserCreacion = user.NroDocumento;
            objauditoria.FecCreacion = DateTime.Now;
            /*auditoria*/

            //_recursoService.Eliminar(id);
            ///*auditoria Grabar*/
            //_AuditoriaService.Save(objauditoria);
            ///*auditoria Grabar*/
            //return Json(new
            //{
            //    mensaje = "El registro fue eliminado satisfactoriamente."
            //});

            ////return PartialView("_Select");
            ///
            var VerificarEliminar = _AuditoriaService.VerificarEliminarRecursoId(id);
            _AuditoriaService.Save(objauditoria);

            return JsonConvert.SerializeObject(new StatusResponse()
            {
                Value = res,
                Success = true,
                Message = VerificarEliminar
            });

        }

        [HttpPost]
        public string EliminarEditar(long id, UsuarioInfo user)
        {

            var res = false;
            /*auditoria agregar*/
            objauditoria.EntidadId = user.EntidadId;
            objauditoria.SectorId = user.Sector;
            objauditoria.ProvinciaId = user.Provincia;
            objauditoria.Usuario = user.NombreCompleto;
            objauditoria.Actividad = "Eliminar Recurso";
            objauditoria.Pantalla = "Recurso";
            objauditoria.UserCreacion = user.NroDocumento;
            objauditoria.FecCreacion = DateTime.Now;
            /*auditoria*/

            ///
            var VerificarEliminar = _AuditoriaService.VerificarEliminarRecursoIdEditar(id);
            _AuditoriaService.Save(objauditoria);

            return JsonConvert.SerializeObject(new StatusResponse()
            {
                Value = res,
                Success = true,
                Message = VerificarEliminar
            });

        }
    }
    public class ListaRecurso
    {
        public string RecursoId { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string TipoId { get; set; }
        public int numero { get; set; }
    }
}