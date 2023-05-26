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
    public class MaestroFijoController : Controller
    {
        private readonly ILogService<MaestroFijoController> _log;
        private readonly IDatoService _datoService;
        private readonly IMaestroFijoService _maestroFijoService;
        private readonly ISedeService _sedeService;
        private readonly IMetaDatoService _metaDatoService;
        private readonly IUnidadOrganicaService _unidadOrganicaService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly IRolMenuService _rolMenuService;
        Auditoria objauditoria = new Auditoria();
        public MaestroFijoController(IDatoService datoService,
                                    IMaestroFijoService maestroFijoService,
                                    ISedeService sedeService,
                                    IMetaDatoService metaDatoService, IAuditoriaService AuditoriaService,
                                    IUnidadOrganicaService unidadOrganicaService, IRolMenuService rolMenuService)
        {
            _log = new LogService<MaestroFijoController>();
            _datoService = datoService;
            _maestroFijoService = maestroFijoService;
            _sedeService = sedeService;
            _metaDatoService = metaDatoService;
            _unidadOrganicaService = unidadOrganicaService;
            _AuditoriaService = AuditoriaService;
            _rolMenuService = rolMenuService;
        }

        public ActionResult Editar(UsuarioInfo user)
        {

            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 19);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/
            ViewBag.Usuario = user;

            MaestroFijo model = null;
            try
            {
                model = _maestroFijoService.GetOneByEntidad(user.EntidadId);
                if (model == null)
                    model = new MaestroFijo()
                    {
                        EntidadId = user.EntidadId,
                        MaestroFijoDatoAdicional = new List<MaestroFijoDatoAdicional>(),
                        MaestroFijoSede = new List<MaestroFijoSede>(),
                        MaestroFijoPasoSeguir = new List<MaestroFijoPasoSeguir>(),
                        MaestroFijoNotaCiudadano = new List<MaestroFijoNotaCiudadano>()
                    };

                // Datos Adicionales
                List<Dato> datos = _datoService.GetByTipo(TipoDato.TipoDatoAdicionalProc);
                List<Dato> datosAdicionales = new List<Dato>();
                List<Dato> canales = datos.Where(x => x.Entero01 == 1).ToList();
                datosAdicionales.AddRange(canales);
                List<Dato> dataFormaPago = datos.Where(x => x.Entero01 == 2).ToList();
                foreach (Dato dfp in dataFormaPago)
                {
                    List<Dato> formasPago = datos.Where(x => x.Entero01 == dfp.MetaDatoId).ToList();
                    formasPago.ForEach(x => x.Padre = dfp);
                    datosAdicionales.AddRange(formasPago);
                }

                foreach (Dato data in datosAdicionales)
                {
                    MaestroFijoDatoAdicional da = model.MaestroFijoDatoAdicional.SingleOrDefault(x => x.MetaDatoId == data.MetaDatoId);
                    if (da == null)
                    {
                        da = new MaestroFijoDatoAdicional()
                        {
                            MaestroFijoId = model.MaestroFijoId,
                            MetaDatoId = data.MetaDatoId,
                            Comentario = "",
                            Checked = false,
                            MetaDato = data,
                            Tipo = data.Padre == null ? 1 : 2
                        };
                        model.MaestroFijoDatoAdicional.Add(da);
                    }
                    else
                    {
                        da.MetaDato = data;
                        da.Checked = true;
                        da.Tipo = data.Padre == null ? 1 : 2;
                    }
                }

                model.MaestroFijoDatoAdicional = model.MaestroFijoDatoAdicional
                                                    .OrderBy(x => x.Tipo)
                                                    .ThenBy(x => x.MetaDatoId)
                                                    .ToList();

                var sedes = _sedeService.GetAll(user.EntidadId);

                ViewBag.valorsede = sedes.Count();
                List<MaestroFijoSede> lstSedes = new List<MaestroFijoSede>();
                foreach (var sede in sedes)
                {
                    lstSedes.Add(new MaestroFijoSede()
                    {
                        MaestroFijoId = model.MaestroFijoId,
                        Sede = sede,
                        SedeId = sede.SedeId,
                        Checked = model.MaestroFijoSede.Count(x => x.SedeId == sede.SedeId) > 0
                    });
                }
                //model.MaestroFijoSede = lstSedes;
                //model.MaestroFijoSede = lstSedes;

                ViewBag.MaestroFijoSede = JsonConvert.SerializeObject(sedes.Select(p => new
                {
                    MaestroFijoId = model.MaestroFijoId,
                    SedeId = p.SedeId,
                    p.Direccion,
                    p.Distrito.Ruta,
                    p.Nombre,
                    Checked = model.MaestroFijoSede.Count(x => x.SedeId == p.SedeId) > 0,
                    Mostrar = model.MaestroFijoSede.Count(o => o.SedeId == p.SedeId) == 0 ? true : model.MaestroFijoSede.Where(o => o.SedeId == p.SedeId).FirstOrDefault().Mostrar
                }));




                List<MaestroFijoUndOrgRecepcionDocum> lstUndOrgSede = new List<MaestroFijoUndOrgRecepcionDocum>();

                foreach (MaestroFijoSede o in model.MaestroFijoSede)
                {
                    if (o.MaestroFijoUndOrgRecepcionDocum != null)
                    {
                        lstUndOrgSede.AddRange(o.MaestroFijoUndOrgRecepcionDocum);
                    }
                }

                ViewBag.MaestroUndOrgSede = JsonConvert.SerializeObject(lstUndOrgSede.Select(x => new
                {
                    x.SedeId
                    ,
                    x.MaestroFijoId
                    ,
                    x.UnidadOrganicaId
                    ,
                    nombre = _unidadOrganicaService.GetOne(x.UnidadOrganicaId).Nombre
                }));



                List<MetaDato> listaTipoNota = _metaDatoService.GetByParent(15);
                listaTipoNota.Insert(0, new MetaDato() { MetaDatoId = 0, Nombre = "- SELECCIONAR -" });

                ViewBag.TipoNota = JsonConvert.SerializeObject(
                    listaTipoNota.Select(x => new
                    {
                        MetaDatoId = x.MetaDatoId,
                        Nombre = x.Nombre
                    }).ToList()
                );


                ViewBag.ListaNota = JsonConvert.SerializeObject(model.MaestroFijoNotaCiudadano.Select(x => new
                {
                    x.NotaCiudadanoId,
                    x.Orden,
                    x.Comentario,
                    x.MetaDatoTipoNotaId
                }));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult ActivarInfPredeterminada(int tipo, long EntidadId, UsuarioInfo user)
        {
            try
            {
                bool estado;
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;

                if (tipo == 1)
                {
                    objauditoria.Actividad = "Finalizar Inf. Predeterminada";
                    objauditoria.Pantalla = "Inf. Predeterminada";
                    estado = true;
                }
                else
                {
                    objauditoria.Actividad = "Activar Inf. Predeterminada";
                    objauditoria.Pantalla = "Inf. Predeterminada";
                    estado = false;
                }
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/


                _maestroFijoService.CambiarEstadoInfCdo(estado, EntidadId);
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

        public string GetOneByEntidad(long EntidadId)
        {
            MaestroFijo model = null;

            string json = "";
            try
            {
                model = _maestroFijoService.GetOneByEntidad(EntidadId);
                if (model == null)
                    model = new MaestroFijo()
                    {
                        MaestroFijoDatoAdicional = new List<MaestroFijoDatoAdicional>(),
                        MaestroFijoSede = new List<MaestroFijoSede>(),
                        MaestroFijoPasoSeguir = new List<MaestroFijoPasoSeguir>(),
                        MaestroFijoNotaCiudadano = new List<MaestroFijoNotaCiudadano>()
                    };


                List<MaestroFijoUndOrgRecepcionDocum> uo = new List<MaestroFijoUndOrgRecepcionDocum>();

                foreach (MaestroFijoSede p in model.MaestroFijoSede)
                {
                    foreach (MaestroFijoUndOrgRecepcionDocum r in p.MaestroFijoUndOrgRecepcionDocum)
                    {
                        uo.Add(r);
                    }
                }


                json = JsonConvert.SerializeObject(uo.Select(x => new
                {
                    x.SedeId,
                    x.UnidadOrganicaId,
                    nombre = _unidadOrganicaService.GetOne(x.UnidadOrganicaId).Nombre
                }));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return json;
        }

        [HttpPost]
        public ActionResult Guardar(MaestroFijo model, UsuarioInfo user)
        {
            try
            {

                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Agregar Maestros Fijo";
                objauditoria.Pantalla = "Maestros Fijo";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/

                /*validacion de multiples ingresos*/
                if (model.MaestroFijoId == 0)
                {
                    MaestroFijo maestro = _maestroFijoService.GetOneByEntidadMaestro(user.EntidadId);
                    if (maestro != null)
                    {
                        return Json(new
                        {
                            valid = false
                        });
                    }
                }

                if (model.MaestroFijoDatoAdicional == null) model.MaestroFijoDatoAdicional = new List<MaestroFijoDatoAdicional>();
                if (model.MaestroFijoSede == null) model.MaestroFijoSede = new List<MaestroFijoSede>();
                if (model.MaestroFijoPasoSeguir == null) model.MaestroFijoPasoSeguir = new List<MaestroFijoPasoSeguir>();
                if (model.MaestroFijoNotaCiudadano == null) model.MaestroFijoNotaCiudadano = new List<MaestroFijoNotaCiudadano>();


                if (model.MaestroFijoId > 0)
                {
                    model.MaestroFijoDatoAdicional.ForEach(x => x.MaestroFijoId = model.MaestroFijoId);
                    model.MaestroFijoSede.ForEach(x => x.MaestroFijoId = model.MaestroFijoId);
                    model.MaestroFijoPasoSeguir.ForEach(x => x.MaestroFijoId = model.MaestroFijoId);
                    model.MaestroFijoNotaCiudadano.ForEach(x => x.MaestroFijoId = model.MaestroFijoId);
                }

                model.MaestroFijoDatoAdicional.RemoveAll(x => !x.Checked);
                //model.MaestroFijoSede.RemoveAll(x => !x.Checked);
                model.MaestroFijoPasoSeguir.RemoveAll(x => string.IsNullOrEmpty(x.Descripcion));

                _maestroFijoService.Save(model);

                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
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

        public ActionResult FijarDatoAdicional(MaestroFijoDatoAdicional model, UsuarioInfo user)
        {
            try
            {
                _maestroFijoService.FijarDatoAdicional(user.EntidadId, model);

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

        public ActionResult FijarDatoConsultaTramite(MaestroFijo model, UsuarioInfo user)
        {
            try
            {
                _maestroFijoService.FijarDatoConsultaTramite(user.EntidadId, model);

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

        public ActionResult FijarSede(MaestroFijoSede model, UsuarioInfo user)
        {
            try
            {
                _maestroFijoService.FijarSede(user.EntidadId, model);

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

        public ActionResult FijarPasoSeguir(MaestroFijoPasoSeguir model, UsuarioInfo user)
        {
            try
            {
                _maestroFijoService.FijarPasoSeguir(user.EntidadId, model);

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


        public ActionResult FijarNotaCiudadano(MaestroFijoNotaCiudadano model, UsuarioInfo user)
        {
            try
            {



                _maestroFijoService.FijarNotaCiudadano(user.EntidadId, model);

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

    }
}