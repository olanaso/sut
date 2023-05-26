using LinqToExcel;
using LinqToExcel.Query;
using Newtonsoft.Json;
using Sut.Entities;
using Sut.IApplicationServices;
using Sut.Log;
using Sut.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;





namespace Sut.Web.Areas.Simplificacion.Controllers
{
    [Authorize]
    public class TablaAsmeController : Controller
    {
        private readonly ILogService<TablaAsmeController> _log;
        private readonly ITablaAsmeService _tablaAsmeService;
        private readonly IActividadService _actividadService;
        private readonly IUnidadOrganicaService _unidadOrganicaService;
        private readonly IEntidadService _entidadService;
        private readonly IObservacionService _observacionService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly IRolMenuService _rolMenuService;
        private readonly IProcedimientoUndOrgResponsableService _ProcedimientoUndOrgResponsableService;

        Auditoria objauditoria = new Auditoria();
        public TablaAsmeController(ITablaAsmeService tablaAsmeService,
                                    IActividadService actividadService,
                                    IUnidadOrganicaService unidadOrganicaService,
                                    IEntidadService entidadService, IAuditoriaService AuditoriaService,
                                        IObservacionService observacionService,
                                        IRolMenuService rolMenuService,
                                        IProcedimientoUndOrgResponsableService procedimientoUndOrgResponsableService)
        {
            _log = new LogService<TablaAsmeController>();
            _tablaAsmeService = tablaAsmeService;
            _actividadService = actividadService;
            _unidadOrganicaService = unidadOrganicaService;
            _entidadService = entidadService;
            _observacionService = observacionService;
            _AuditoriaService = AuditoriaService;
            _rolMenuService = rolMenuService;
            _ProcedimientoUndOrgResponsableService = procedimientoUndOrgResponsableService;

        }
        public bool AplicarFiltro(Filtros filtros)
        {
            Filtros model = new Filtros();
            try
            {
                model.Clasificacion = filtros.Clasificacion;
                model.Prestacionesanuales = filtros.Prestacionesanuales;
                model.PrestacionesCosto = filtros.PrestacionesCosto;
                model.Duracion = filtros.Duracion;
                model.PlazoAtencion = filtros.PlazoAtencion;
                model.Requisitos = filtros.Requisitos;
                model.estado = "1";
                System.Web.HttpContext.Current.Session["modelfiltro"] = model;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("AplicarFiltro", ex.Message);
                _log.Error(ex);
            }
            return true;
        }

        public bool QuitarFiltro(Filtros filtros)
        {
            Filtros model = new Filtros();
            try
            {
                model.Clasificacion = filtros.Clasificacion;
                model.Prestacionesanuales = filtros.Prestacionesanuales;
                model.PrestacionesCosto = filtros.PrestacionesCosto;
                model.Duracion = filtros.Duracion;
                model.PlazoAtencion = filtros.PlazoAtencion;
                model.Requisitos = filtros.Requisitos;
                model.estado = "0";
                System.Web.HttpContext.Current.Session["modelfiltro"] = model;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("AplicarFiltro", ex.Message);
                _log.Error(ex);
            }
            return true;
        }
        public ActionResult Editar(long id, UsuarioInfo user)
        {
            try
            {
                /*Inicio Cargar roles - Acceso*/
                List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 24);
                user.rolmenu = listarolmenu.ToList();
                /*Fin Cargar roles  - Acceso*/

                ViewBag.Usuario = user;
                Entidad entidad = _entidadService.GetOne(user.EntidadId);
                TablaAsme model = _tablaAsmeService.GetOne(id);
                model.Actividad = _actividadService.GetByTablaAsme(id);
                List<UnidadOrganica> listaUndOrg = _unidadOrganicaService.GetAll(user.EntidadId);
                //if (user.Rol == 8 || user.Rol == 7 || user.Rol == 6) {
                listaUndOrg = _unidadOrganicaService.GetAll(model.Procedimiento.Expediente.EntidadId);
                //}
                if (user.Rol != 1)
                {
                    listaUndOrg.Insert(0, new UnidadOrganica() { UnidadOrganicaId = 0, Nombre = " - SELECCIONAR - " });
                }

                Observacion obs = new Observacion();
                obs.ExpedienteId = model.Procedimiento.ExpedienteId;
                obs.ProcedimientoId = model.ProcedimientoId;
                obs.Estado = "1";
                obs.EntidadId = user.EntidadId;
                obs.Pantalla = "Tabla Asme";
                ViewBag.Observacion = _observacionService.GetOneGlobalObservacion(obs);

                ViewBag.estadoexpediente = model.Procedimiento.Expediente.EstadoExpediente;

                /**añadir categorias**///
                ProcedimientoUndOrgResponsable tmppro = new ProcedimientoUndOrgResponsable();
                var eliminarpro = _ProcedimientoUndOrgResponsableService.GetAll(model.ProcedimientoId);
                string nombrecargo = "";
                int cont = 2;
                if (eliminarpro != null)
                {
                    foreach (ProcedimientoUndOrgResponsable l in eliminarpro)
                    {
                        if (cont == 2)
                        {
                            nombrecargo = Convert.ToString(cont) + ".-  " + _unidadOrganicaService.GetOne(l.UndOrgResponsableId2).Nombre;
                        }
                        else
                        {

                            nombrecargo = nombrecargo + "  " + Convert.ToString(cont) + ".-  " + _unidadOrganicaService.GetOne(l.UndOrgResponsableId2).Nombre;
                        }
                        cont = cont + 1;
                    }
                }
                model.Procedimiento.CargoPordependencia = nombrecargo;

                Observacion obsgeneral = new Observacion();
                obsgeneral.ExpedienteId = model.Procedimiento.ExpedienteId;
                obsgeneral.ProcedimientoId = model.ProcedimientoId;
                obsgeneral.CodValidacion = "1";
                obsgeneral.Estado = Convert.ToString(id);
                obsgeneral.EntidadId = user.EntidadId;
                obsgeneral.NombreCampo = "Nota de Ayuda Tabla Asme";
                ViewBag.Observaciongeneral = _observacionService.GetOneGlobal(obsgeneral);


                if (model.Observacion != null)
                {
                    ViewBag.ListaObservacionAsme = JsonConvert.SerializeObject(
                            model.Observacion
                            .Select(x => new
                            {
                                ObservacionId = x.ObservacionId,
                                Descripcion = x.Descripcion,
                                CodValidacion = x.CodValidacion,
                                Estado = x.Estado,
                                ExpedienteId = x.ExpedienteId,
                                ProcedimientoId = x.ProcedimientoId,
                                EntidadId = x.EntidadId,
                                NombreCampo = x.NombreCampo,
                                Pantalla = x.Pantalla,
                                TablaAsmeId = x.TablaAsmeId
                            }).Where(x => x.TablaAsmeId == id).OrderBy(x => x.ObservacionId).ToList()
                        );
                }

                Filtros filorder = new Filtros();



                ViewBag.actividad = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Ninguno", Value = "0", Selected = filorder.actividad == TipoActividad.Ninguno ? true:false},
                    new SelectListItem() { Text = "Operacion", Value = "1" , Selected = filorder.actividad == TipoActividad.Operacion ? true:false},
                    new SelectListItem() { Text = "Revision", Value = "2" , Selected = filorder.actividad == TipoActividad.Revision ? true:false},
                    new SelectListItem() { Text = "Traslado", Value = "3",  Selected = filorder.actividad== TipoActividad.Traslado ? true:false },
                    new SelectListItem() { Text = "Espera", Value = "4" , Selected = filorder.actividad == TipoActividad.Espera ? true:false},
                    new SelectListItem() { Text = "Archivo", Value = "5" , Selected = filorder.actividad == TipoActividad.Archivo ? true:false}
                };
                ViewBag.valor = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Ninguno", Value = "0" , Selected = filorder.valor == TipoValor.Ninguno ? true:false},
                    new SelectListItem() { Text = "VA", Value = "1" , Selected = filorder.valor == TipoValor.VA ? true:false},
                    new SelectListItem() { Text = "Control", Value = "2",  Selected = filorder.valor == TipoValor.Control ? true:false },
                    new SelectListItem() { Text = "SVA", Value = "3" , Selected = filorder.valor == TipoValor.SVA ? true:false}
                };


                ViewBag.ListaUndOrg = JsonConvert.SerializeObject(
                    listaUndOrg.Select(x => new
                    {
                        Numero = x.Numero,
                        UnidadOrganicaId = x.UnidadOrganicaId,
                        Nombre = x.Nombre
                    })
                    .OrderBy(x => x.Numero)
                    .ToList()
                );

                string viewName = string.Empty;

                if (model.Procedimiento.Operacion == OperacionExpediente.Ninguna || model.Procedimiento.Operacion == OperacionExpediente.Eliminacion)
                {
                    viewName = "Ver";
                }
                else
                {

                    if (model.Procedimiento.TablaAsmeTerminado) viewName = "Ver";
                    else
                    {
                        switch (user.Rol)
                        {
                            case (short)Rol.Administrador:
                                if (entidad.TipoTupa == TipoTupa.Estandar && model.Procedimiento.Expediente.EstadoExpediente != EstadoExpediente.Aprobado)
                                    viewName = "Editar";
                                else
                                    viewName = "Ver";
                                break;
                            case (short)Rol.Coordinador: viewName = "Editar"; break;
                            case (short)Rol.Evaluador: viewName = "Ver"; break;
                            case (short)Rol.Ratificador: viewName = "Ver"; break;
                            case (short)Rol.EntidadFiscalizadora: viewName = "Ver"; break;
                            case (short)Rol.RegistradorProcesos: viewName = "Editar"; break; //modificar
                        }
                    }
                }



                return View(viewName, model);
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ModelState.AddModelError("", ex.Message);
                _log.Error(ex);
                return View("_Error");
            }
        }


        [HttpPost]
        public bool Editarorden(long id, int num, UsuarioInfo user)
        {
            TablaAsme model = new TablaAsme();
            try
            {

                //List<TablaAsme> lista = _tablaAsmeService.GetByExpedienteNumero(id);

                //if (lista.Count == 0)
                //{

                //    return false;
                //}
                //else
                //{
                model = _tablaAsmeService.GetOne(id);
                model.EditarNumAsme = num;


                _tablaAsmeService.Save(model);
                //}

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("editarExpediente", ex.Message);
                _log.Error(ex);
            }
            return true;
        }



        [HttpPost]
        public bool guardareditarorden(long id, List<elemento> orden, int tipoorder, UsuarioInfo user)
        {
            Actividad model = new Actividad();
            try
            {
                int totalRows = 0;
                List<Actividad> lista = _actividadService.GetByTablaAsme(id);
                if (lista.Count == 0)
                {
                    return false;
                }
                else
                {
                    foreach (Actividad l in lista)
                    {
                        model.ActividadId = l.ActividadId;
                        model.Orden = Convert.ToInt32(orden[totalRows].orden.ToString());
                        _actividadService.guardarnumeroprocedimiento(model);
                        totalRows = totalRows + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("editarExpediente", ex.Message);
                _log.Error(ex);
            }
            return true;
        }


        public ActionResult VerObs(long id, long entidadid, UsuarioInfo user)
        {
            try
            {

                ViewBag.entidadver = entidadid;

                ViewBag.Usuario = user;
                Entidad entidad = _entidadService.GetOne(user.EntidadId);
                TablaAsme model = _tablaAsmeService.GetOne(id);
                model.Actividad = _actividadService.GetByTablaAsme(id);
                List<UnidadOrganica> listaUndOrg = _unidadOrganicaService.GetAll(user.EntidadId);
                //if (user.Rol == 8 || user.Rol == 7 || user.Rol == 6) {
                listaUndOrg = _unidadOrganicaService.GetAll(model.Procedimiento.Expediente.EntidadId);
                //}
                if (user.Rol != 1)
                {
                    listaUndOrg.Insert(0, new UnidadOrganica() { UnidadOrganicaId = 0, Nombre = " - SELECCIONAR - " });
                }

                Observacion obs = new Observacion();
                obs.ExpedienteId = model.Procedimiento.ExpedienteId;
                obs.ProcedimientoId = model.ProcedimientoId;
                obs.Estado = "0";
                obs.CodValidacion = "5";
                obs.EntidadId = entidadid;
                obs.Pantalla = "Tabla Asme";
                ViewBag.Observacion = _observacionService.GetOneVerObservacion(obs);

                ViewBag.estadoexpediente = model.Procedimiento.Expediente.EstadoExpediente;


                Observacion obsgeneral = new Observacion();
                obsgeneral.ExpedienteId = model.Procedimiento.ExpedienteId;
                obsgeneral.ProcedimientoId = model.ProcedimientoId;
                obsgeneral.CodValidacion = "5";
                obsgeneral.Estado = "0";
                obsgeneral.EntidadId = entidadid;
                obsgeneral.NombreCampo = "Nota de Ayuda Tabla Asme";
                ViewBag.Observaciongeneral = _observacionService.GetOneGlobalobs(obsgeneral);


                if (model.Observacion != null)
                {
                    ViewBag.ListaObservacionAsme = JsonConvert.SerializeObject(
                            model.Observacion
                            .Select(x => new
                            {
                                ObservacionId = x.ObservacionId,
                                Descripcion = x.Descripcion,
                                CodValidacion = x.CodValidacion,
                                Estado = x.Estado,
                                ExpedienteId = x.ExpedienteId,
                                ProcedimientoId = x.ProcedimientoId,
                                EntidadId = x.EntidadId,
                                NombreCampo = x.NombreCampo,
                                Pantalla = x.Pantalla,
                                TablaAsmeId = x.TablaAsmeId,
                                TipoEstado = x.TipoEstado
                            }).Where(x => x.TablaAsmeId == id).OrderBy(x => x.ObservacionId).ToList()
                        );
                }
                ViewBag.ListaUndOrg = JsonConvert.SerializeObject(
                    listaUndOrg.Select(x => new
                    {
                        Numero = x.Numero,
                        UnidadOrganicaId = x.UnidadOrganicaId,
                        Nombre = x.Nombre
                    })
                    .OrderBy(x => x.Numero)
                    .ToList()
                );

                string viewName = string.Empty;
                viewName = "VerObs";

                return View(viewName, model);
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ModelState.AddModelError("", ex.Message);
                _log.Error(ex);
                return View("_Error");
            }
        }
        public ActionResult GetActividades(Filtros filtros, long id, string des)
        {
            try
            {
                List<Actividad> lista = _actividadService.GetByTablaAsmever(filtros, id, des);



                return Json(lista.Select(x => new
                {
                    ActividadId = x.ActividadId,
                    Orden = x.Orden,
                    Descripcion = x.Descripcion,
                    UnidadOrganicaId = x.UnidadOrganicaId,
                    Duracion = x.Duracion,
                    TipoActividad = (int)x.TipoActividad,
                    TipoValor = (int)x.TipoValor
                }).ToList()
                    , JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ModelState.AddModelError("", ex.Message);
                _log.Error(ex);
                return PartialView("_Error");
            }
        }

        public ActionResult GetRecursos(long id, string Nro, string Actividad, string UnidadOrganizacion, string Duracion)
        {
            try
            {
                List<ActividadRecurso> lista = _actividadService.GetByActividad(id);

                ViewBag.Nro = Nro;
                ViewBag.Actividad = Actividad;
                ViewBag.UnidadOrganizacion = UnidadOrganizacion;
                ViewBag.Duracion = Duracion;

                return Json(lista.Select(x => new
                {
                    RecursoId = x.RecursoId,
                    Cantidad = x.Cantidad,
                    Nombre = x.Recurso.Nombre,
                    TipoId = (int)x.Recurso.TipoRecurso
                }).ToList()
                    , JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ModelState.AddModelError("", ex.Message);
                _log.Error(ex);
                return PartialView("_Error");
            }
        }

        [HttpPost]
        public ActionResult GuardarTablaAsme(TablaAsme model, UsuarioInfo user)
        {
            try
            {
                if (model.Actividad != null)
                {
                    int valor = 1;
                    foreach (Actividad item in model.Actividad)
                    {

                        item.TablaAsmeId = model.TablaAsmeId;
                        item.UnidadOrganica = null;
                        item.TablaAsme = null;
                        item.ActividadRecurso = null;
                        item.Orden = valor;

                        item.FecCreacion = DateTime.Now;
                        item.UserCreacion = user.NroDocumento;
                        item.FecModificacion = DateTime.Now;
                        item.UserModificacion = user.NroDocumento;

                        valor = valor + 1;
                    }
                    /*auditoria agregar*/
                    objauditoria.EntidadId = user.EntidadId;
                    objauditoria.SectorId = user.Sector;
                    objauditoria.ProvinciaId = user.Provincia;
                    objauditoria.Usuario = user.NombreCompleto;
                    objauditoria.Actividad = "Agregar Tabla Asme";
                    objauditoria.Pantalla = "Tabla Asme";
                    objauditoria.UserCreacion = user.NroDocumento;
                    objauditoria.FecCreacion = DateTime.Now;
                    /*auditoria*/
                    _tablaAsmeService.Guardar(model);
                    /*auditoria Grabar*/
                    _AuditoriaService.Save(objauditoria);
                    /*auditoria Grabar*/
                }
                else
                {
                    _tablaAsmeService.Guardar(model);
                }

                _AuditoriaService.VerificarRecursoEspera(model.TablaAsmeId);
                //else
                //{
                //    List<Actividad> objactivoada = _actividadService.GetByTablaAsme(model.TablaAsmeId);


                //    foreach (Actividad item in objactivoada)
                //    {
                //        item.TablaAsmeId = model.TablaAsmeId;
                //        item.UnidadOrganica = null;
                //        item.TablaAsme = null;
                //        item.ActividadRecurso = null;

                //        item.FecCreacion = DateTime.Now;
                //        item.UserCreacion = user.NroDocumento;
                //        item.FecModificacion = DateTime.Now;
                //        item.UserModificacion = user.NroDocumento;
                //    }

                //    model.Actividad = objactivoada;
                //    _tablaAsmeService.Guardardelete(model);


                //    /*auditoria agregar*/
                //    objauditoria.EntidadId = user.EntidadId;
                //    objauditoria.SectorId = user.Sector;
                //    objauditoria.ProvinciaId = user.Provincia;
                //    objauditoria.Usuario = user.NombreCompleto;
                //    objauditoria.Actividad = "Eliminar Actividades";
                //    objauditoria.Pantalla = "Tabla Asme";
                //    objauditoria.UserCreacion = user.NroDocumento;
                //    objauditoria.FecCreacion = DateTime.Now; 
                //    /*auditoria Grabar*/
                //    _AuditoriaService.Save(objauditoria);
                //    /*auditoria Grabar*/
                //}
                return Json(new
                {
                    valid = true
                });


            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.Clear();
                ModelState.AddModelError("", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }

        [HttpPost]
        public ActionResult GuardarDerecho(int id, int Prestaciones, UsuarioInfo user)
        {
            try
            {
                TablaAsme model = new TablaAsme();

                model = _tablaAsmeService.GetOne(id);
                model.Prestaciones = Prestaciones;

                _tablaAsmeService.Guardar(model);



                return Json(new
                {
                    valid = true
                });


            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.Clear();
                ModelState.AddModelError("", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }


        [HttpPost]
        public ActionResult ElimiarActividadBlanco(TablaAsme model, UsuarioInfo user)
        {
            try
            {

                _AuditoriaService.Eliminar_ActividadBlanco(model.TablaAsmeId);

                return Json(new
                {
                    valid = true
                });


            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.Clear();
                ModelState.AddModelError("", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }


        [HttpPost]
        public ActionResult eliminarGuardarTablaAsme(TablaAsme model, UsuarioInfo user)
        {
            try
            {

                _AuditoriaService.VerificarRecursoEspera(model.TablaAsmeId);



                return Json(new
                {
                    valid = true
                });


            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.Clear();
                ModelState.AddModelError("", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }

        [HttpPost]
        public ActionResult EliminarTablaAsme(long id, long idasme, UsuarioInfo user)
        {
            try
            {


                Actividad actividad = _actividadService.GetOne(id);


                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Eliminar Actividad";
                objauditoria.Pantalla = "Tabla Asme - Actividad";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                _actividadService.GuardarRecursosEliminar(actividad);
                /*auditoria Grabar*/


                List<Actividad> lista = _actividadService.GetByTablaAsme(idasme);

                Actividad modelact = new Actividad();
                int totalRows = 1;
                foreach (Actividad l in lista)
                {
                    modelact = _actividadService.GetOne(l.ActividadId);
                    modelact.Orden = totalRows;
                    _actividadService.ActualizarActividad(modelact);
                    totalRows = totalRows + 1;
                }



                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/

                return Json(new
                {
                    valid = true
                });


            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.Clear();
                ModelState.AddModelError("", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }

        [HttpPost]
        public ActionResult GuardarRecurso(Actividad model, UsuarioInfo user)
        {
            try
            {
                foreach (ActividadRecurso item in model.ActividadRecurso)
                {
                    item.ActividadId = model.ActividadId;
                    item.Actividad = null;
                    item.Recurso = null;

                    item.FecCreacion = DateTime.Now;
                    item.UserCreacion = user.NroDocumento;
                    item.FecModificacion = DateTime.Now;
                    item.UserModificacion = user.NroDocumento;
                }
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Recurso";
                objauditoria.Pantalla = "Tabla Asme";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                _actividadService.GuardarRecursos(model);

                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
                return Json(new
                {
                    valid = true
                });
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.Clear();
                ModelState.AddModelError("", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }

        public ActionResult ImportarActividades(Int32 tablaAsmeId)
        {
            try
            {
                ViewBag.TablaAsmeId = tablaAsmeId;
                System.Web.HttpContext.Current.Session["tablaAsmeId"] = tablaAsmeId;

                return PartialView("_ImportarActividades");
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        [HttpPost]
        public ActionResult ImportarActividadesTablaNueva(HttpPostedFileBase fileExcel, UsuarioInfo user)
        {
            try
            {

                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Importar Actividades";
                objauditoria.Pantalla = "Actividades";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                List<MetaDato> tipos_act = new List<MetaDato>();
                foreach (var val in Enum.GetValues(typeof(TipoActividad)))
                    tipos_act.Add(new MetaDato()
                    {
                        MetaDatoId = (short)val,
                        Nombre = Enum.GetName(typeof(TipoActividad), val)
                    });
                List<MetaDato> tipos_valor = new List<MetaDato>();
                foreach (var val in Enum.GetValues(typeof(TipoValor)))
                    tipos_valor.Add(new MetaDato()
                    {
                        MetaDatoId = (short)val,
                        Nombre = Enum.GetName(typeof(TipoValor), val)
                    });

                FileInfo info = new FileInfo(fileExcel.FileName);
                string path = ConfigurationManager.AppSettings["Sut.PathUpload"];
                string filename = string.Format("excelASME_{0}_{1}.{2}", user.NroDocumento, DateTime.Now.ToString("yyyMddHHmmss"), info.Extension);
                filename = Path.Combine(path, filename);

                fileExcel.SaveAs(filename);

                var excel = new ExcelQueryFactory(filename);
                excel.TrimSpaces = TrimSpacesType.Both;
                excel.AddMapping<Actividad>(x => x.Descripcion, "Actividad");
                excel.AddMapping<Actividad>(x => x.NomUnidadOrganica, "Unidad_Organica");
                excel.AddMapping<Actividad>(x => x.Duracion, "Duracion");
                excel.AddMapping<Actividad>(x => x.NomTipoActividad, "Tipo_Actividad");
                excel.AddMapping<Actividad>(x => x.NomTipoValor, "Tipo_Valor");

                var data = excel.Worksheet<Actividad>("Actividades").ToList();
                excel.Dispose();
                System.IO.File.Delete(filename);
                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/

                foreach (var act in data)
                {
                    if (tipos_act.Any(x => x.Nombre.ToUpper() == act.NomTipoActividad.ToUpper()))
                        act.TipoActividad = (TipoActividad)(tipos_act.First(x => x.Nombre.ToUpper() == act.NomTipoActividad.ToUpper()).MetaDatoId);
                    if (tipos_valor.Any(x => x.Nombre.ToUpper() == act.NomTipoValor.ToUpper()))
                        act.TipoValor = (TipoValor)(tipos_valor.First(x => x.Nombre.ToUpper() == act.NomTipoValor.ToUpper()).MetaDatoId);
                }

                return Json(new
                {
                    valid = true,
                    data = data.Select(x => new
                    {
                        Descripcion = x.Descripcion,
                        NomUnidadOrganica = x.NomUnidadOrganica,
                        UnidadOrganicaId = x.UnidadOrganicaId,
                        Duracion = x.Duracion,
                        TipoActividad = (int)x.TipoActividad,
                        TipoValor = (int)x.TipoValor
                    }
                    ).ToList()
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.Clear();
                ModelState.AddModelError("", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }

        public bool EsNumeroEntero(string texto)
        {
            int numero;
            bool esNumero = int.TryParse(texto, out numero);

            return esNumero;
        }

        [HttpPost]
        public ActionResult ImportarActividades(HttpPostedFileBase fileExcel, Int32 tablaAsmeId, UsuarioInfo user)
        {

            List<string> lstCabecera = new List<string>();
            List<string> lista = new List<string>();
           // try
            //{

                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Importar Actividades";
                objauditoria.Pantalla = "Actividades";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                //Int32 tablaAsmeId = Convert.ToInt32(System.Web.HttpContext.Current.Session["tablaAsmeId"]);
               
                var dataAsmeunidad = _unidadOrganicaService.GetAll(user.EntidadId);
                List<UnidadOrganica> unidad_organica = new List<UnidadOrganica>();
                foreach (var val in dataAsmeunidad)
                    unidad_organica.Add(new UnidadOrganica()
                    {
                        UnidadOrganicaId = val.UnidadOrganicaId,
                        Nombre = val.Nombre.Trim(),
                        Numero = val.Numero

                    });

                var dataAsmeRecursos = _actividadService.GetDataByTablaAsmeRecursos(user.EntidadId);
                List<Recurso> tipos_recursos = new List<Recurso>();
                foreach (var val in dataAsmeRecursos)
                    tipos_recursos.Add(new Recurso()
                    {
                        RecursoId = val.RecursoId,
                        Nombre = val.Nombre.Trim(),
                        TipoRecurso = val.TipoRecurso

                    });

                List<MetaDato> tipos_act = new List<MetaDato>();
                foreach (var val in Enum.GetValues(typeof(TipoActividad)))
                    tipos_act.Add(new MetaDato()
                    {
                        MetaDatoId = (short)val,
                        Nombre = Enum.GetName(typeof(TipoActividad), val)
                    });

                List<MetaDato> tipos_valor = new List<MetaDato>();
                foreach (var val in Enum.GetValues(typeof(TipoValor)))
                    tipos_valor.Add(new MetaDato()
                    {
                        MetaDatoId = (short)val,
                        Nombre = Enum.GetName(typeof(TipoValor), val)
                    });

                FileInfo info = new FileInfo(fileExcel.FileName);
                string path = ConfigurationManager.AppSettings["Sut.PathUpload"];
                string filename = string.Format("excelASME_{0}_{1}.{2}", user.NroDocumento, DateTime.Now.ToString("yyyMddHHmmss"), info.Extension);
                filename = Path.Combine(path, filename);

                fileExcel.SaveAs(filename);




                //List<string> lstCabecera = new List<string>();

                var excel = new ExcelQueryFactory(filename);

                List<Actividad> lstact = new List<Actividad>();
                //List<Actividad> lstactrecursosCompleto = new List<Actividad>();

                var worksheetNames = excel.GetWorksheetNames().ToArray();
                //
                //Mapping
                //
                for (int i = 0; i < worksheetNames.Length; i++)
                {
                    int contador = 0;
                    int contvalores = 0;
                    var codeIndex = 0;
                    var IndexTipo = 0;
                    var Indexvalor = 0;

                    foreach (var product in excel.WorksheetNoHeader(worksheetNames[i])
                        .Where(x => x[codeIndex] != ""))
                    {
                        contador = contador + 1;
                        contvalores = product.Count() - 8;
                        //if (contador == 5)
                        //{
                        //      tablaAsmeId = Convert.ToInt32(product[0].ToString().Trim());
                        //}


                        if (contador == 5)
                        {
                            for (var j = 0; j < product.Count(); j++)
                            {
                                if (product[j] != "")
                                {
                                    lstCabecera.Add(product[j].ToString().Trim());
                                }
                            }
                        }
                        if (contador > 5)
                        {
                            Actividad act = new Actividad();
                            //Actividad actrecurso = new Actividad();


                            List<ActividadRecurso> lstactrecursos = new List<ActividadRecurso>();
                            act.TablaAsmeId = tablaAsmeId;
                            if (product[0] != "")
                            {

                                if (product[0].ToString().Trim() != "TOTAL :")
                                {


                                    act.Orden = Convert.ToInt32(product[0].ToString().Trim());
                                    if (act.Orden == 0)
                                    {
                                        lista.Add(string.Format("Debe asignar número de orden a la fila. {0}", string.Join(",", act.Orden.ToString())));
                                    }
                                    act.Descripcion = product[1].ToString().Trim();

                                    if (act.Descripcion == "")
                                    {
                                        lista.Add(string.Format("Debe asignar una actividad en la fila. {0}", string.Join(",", act.Orden.ToString())));
                                    }


                                    if (product[2].ToString().Trim() != "")
                                    {
                                        if (unidad_organica.Any(x => x.Numero.ToString().Trim() == product[2].ToString().Trim()))
                                        {
                                            act.UnidadOrganicaId = unidad_organica.First(x => x.Numero.ToString().Trim() == product[2].ToString().Trim()).UnidadOrganicaId;
                                        }
                                        else
                                        {
                                            lista.Add(string.Format("No existe la unidad organica {0} ", string.Join(",", "'" + product[2].ToString().Trim() + "'" + "en la fila." + act.Orden.ToString())));
                                        }
                                    }
                                    else
                                    {
                                        lista.Add(string.Format("Debe asignar una unidad organica en la fila. {0}", string.Join(",", act.Orden.ToString())));
                                    }

                                    if (!EsNumeroEntero(product[3].ToString().Trim())) // product[3].ToString().Trim() == "" || product[3].ToString() == "0")
                                    {
                                        lista.Add(string.Format("Debe asignar una duracion mayor a 0 en la fila. {0} de la Cabecera <b>" + lstCabecera[3] + "</b>", string.Join(",", act.Orden.ToString())));
                                    }
                                    else
                                    {
                                        act.Duracion = Convert.ToDecimal(product[3].ToString().Trim());
                                    }

                                    //validando que sea numero 
                                    for (var h = 4; h <= 23; h++)
                                    {
                                        if (!EsNumeroEntero(product[h].ToString().Trim()))
                                        {
                                            lista.Add(string.Format("Debe asignar una duracion mayor a 0 en la fila. {0} de la Cabecera <b>"+ lstCabecera[h]+"</b>", string.Join(",", act.Orden.ToString())));

                                        }
                                    }
                                    //validando que sea numero
                                    for (var l = 24; l <= 45; l++)
                                    {
                                        var input = product[l].ToString().Trim();
                                        if (string.IsNullOrEmpty(input) || input.ToLower() == "x")
                                        {
                                            // El string es válido (es 'X', 'x' o un string vacío)
                                        }
                                        else
                                        {
                                            lista.Add(string.Format("Debe contener un <b>X</b>. en la fila {0} de la Cabecera <b>" + lstCabecera[l] + "</b>", string.Join(",", act.Orden.ToString())));
                                        }
                                    }

                                    //validando que sea numero
                                    var column = -1;
                                    var activado=false;
                                    for (var l = 46; l <= 48; l++)
                                    {
                                        var input = product[l].ToString().Trim();
                                        if (input.ToLower() == "x")
                                        {
                                            if (!activado)
                                            {

                                            
                                            column = l;
                                            activado = true;
                                            }
                                            else
                                            {
                                                if (column != l)
                                                {
                                                    lista.Add(string.Format("No debe contener un <b>X</b> en la columna. {0} de la Cabecera <b>" + lstCabecera[l] + "</b>", string.Join(",", act.Orden.ToString())));
                                                }
                                            }
                                        }
                                      
                                    }



                                    for (var r = 4; r < lstCabecera.Count() - 8; r++)
                                    {
                                        if (product[r].ToString().Replace("0", "").Trim() != "")
                                        {
                                            ActividadRecurso actrecursos = new ActividadRecurso();
                                            if (tipos_recursos.Any(x => x.Nombre.ToUpper() == lstCabecera[r].ToUpper()))
                                                actrecursos.RecursoId = tipos_recursos.First(x => x.Nombre.ToUpper().Trim() == lstCabecera[r].ToUpper().Trim()).RecursoId;

                                            var tiporecurso = tipos_recursos.First(x => x.Nombre.ToUpper().Trim() == lstCabecera[r].ToUpper().Trim()).TipoRecurso;


                                            if (tiporecurso == TipoRecurso.MaterialNoFungible || tiporecurso == TipoRecurso.ServicioTerceros || tiporecurso == TipoRecurso.Depreciacion || tiporecurso == TipoRecurso.Fijos)
                                            {
                                                actrecursos.Cantidad = Convert.ToDecimal(0);
                                            }
                                            else
                                            {
                                                actrecursos.Cantidad = Convert.ToDecimal(product[r].ToString().Trim());
                                            }
                                            actrecursos.UserCreacion = user.NroDocumento.ToString().Trim();
                                            actrecursos.UserModificacion = user.NroDocumento.ToString().Trim();
                                            actrecursos.FecCreacion = DateTime.Now;
                                            actrecursos.FecModificacion = DateTime.Now;

                                            lstactrecursos.Add(actrecursos);
                                        }

                                        IndexTipo = r;
                                    }

                                    act.ActividadRecurso = lstactrecursos.Select(x => new ActividadRecurso()
                                    {
                                        ActividadId = x.ActividadId,
                                        Cantidad = x.Cantidad,
                                        RecursoId = x.RecursoId,
                                        UserCreacion = x.UserCreacion,
                                        UserModificacion = x.UserModificacion,
                                        FecCreacion = x.FecCreacion,
                                        FecModificacion = x.FecModificacion

                                    }).ToList();

                                    for (var ar = IndexTipo + 1; ar < lstCabecera.Count() - 3; ar++)
                                    {
                                        string ver = product[ar].ToString().Trim();
                                        if (product[ar].ToString().Trim() != "")
                                        {
                                            if (tipos_act.Any(x => x.Nombre.ToUpper().Trim() == lstCabecera[ar].Replace('ó', 'o').ToUpper().Trim()))
                                                act.TipoActividad = (TipoActividad)(tipos_act.First(x => x.Nombre.ToUpper().Trim() == lstCabecera[ar].Replace('ó', 'o').ToUpper().Trim()).MetaDatoId);
                                        }
                                        Indexvalor = ar;

                                    }

                                    for (var valor = Indexvalor + 1; valor < lstCabecera.Count(); valor++)
                                    {
                                        string ver = product[valor].ToString().Trim();
                                        if (product[valor].ToString().Trim() != "")
                                        {
                                            if (tipos_valor.Any(x => x.Nombre.ToUpper().Trim() == lstCabecera[valor].ToUpper().Trim()))
                                                act.TipoValor = (TipoValor)(tipos_valor.First(x => x.Nombre.ToUpper().Trim() == lstCabecera[valor].ToUpper().Trim()).MetaDatoId);
                                        }
                                    }
                                    act.UserCreacion = user.NroDocumento;
                                    act.UserModificacion = user.NroDocumento;
                                    act.FecCreacion = DateTime.Now;
                                    act.FecModificacion = DateTime.Now;


                                    lstact.Add(act);

                                }
                            }
                            //lstactrecursosCompleto.Add(actrecurso);
                        }
                    }
                }



                if (lista.Count() > 0)
                {
                    return Json(new
                    {
                        valid = false,
                        mensaje = lista
                    });
                }
                else
                {
                    TablaAsme asme = new TablaAsme();
                    asme.Actividad = lstact;
                    asme.TablaAsmeId = tablaAsmeId;
                    _tablaAsmeService.Guardarexcel(asme);
                    _AuditoriaService.Save(objauditoria);

                }



                //Actividad Activi = new Actividad();
                //Activi.ActividadRecurso = lstactrecursosCompleto[0].ActividadRecurso;
                //Activi.ActividadId = 1323;
                //_actividadService.GuardarRecursos(Activi);



                return Json(new
                {  
                    text = lista,
                    valid = true,
                }, JsonRequestBehavior.AllowGet);
            //}
           /* catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.Clear();
                ModelState.AddModelError("", ex.Message);

                _log.Error(ex);
                return Json(new
                {
                    error = ex.Message,
                    lista= lista

                }, JsonRequestBehavior.AllowGet);
                //return PartialView("_Error");
            }*/
        }
    }
}