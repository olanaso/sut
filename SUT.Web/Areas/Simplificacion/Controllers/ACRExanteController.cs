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
    public class ACRExanteController : Controller
    {
        private readonly ILogService<ACRExanteController> _log;
        private readonly IVCalidadEXANTEService _VCalidadEXANTEService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly IMaestroFijoService _maestroFijoService;
        private readonly IProcedimientoService _ProcedimientoService;
        private readonly IEntidadService _entidadService;
        private readonly IMetaDatoService _metaDatoService;
        private readonly IExpedienteService _expedienteService;
        private readonly IRolMenuService _rolMenuService;



        private readonly IVCalidadRequisitos_4_2EXANTEService _vCalidadRequisitos_4_2EXANTEService;
        private readonly IVCalidadRequisitos_5_4EXANTEService _vCalidadRequisitos_5_4EXANTEService;
        private readonly IVCalidadRequisitos_CABECERAEXANTEService _vCalidadRequisitos_CABECERAEXANTEService;

        Auditoria objauditoria = new Auditoria();
        public ACRExanteController(IAuditoriaService AuditoriaService,
                                    IExpedienteService expedienteService,
                                    IMetaDatoService metaDatoService, IVCalidadEXANTEService VCalidadEXANTEService, IProcedimientoService ProcedimientoService, IMaestroFijoService MaestroFijoService, IEntidadService EntidadService, IVCalidadRequisitos_4_2EXANTEService VCalidadRequisitos_4_2EXANTEService, IVCalidadRequisitos_5_4EXANTEService VCalidadRequisitos_5_4EXANTEService, IVCalidadRequisitos_CABECERAEXANTEService VCalidadRequisitos_CABECERAEXANTEService,
                                    IRolMenuService rolMenuService)
        {
            _log = new LogService<ACRExanteController>();
            _VCalidadEXANTEService = VCalidadEXANTEService;
            _AuditoriaService = AuditoriaService;
            _ProcedimientoService = ProcedimientoService;
            _maestroFijoService = MaestroFijoService;
            _entidadService = EntidadService;
            _metaDatoService = metaDatoService;
            _expedienteService = expedienteService;
            _vCalidadRequisitos_4_2EXANTEService = VCalidadRequisitos_4_2EXANTEService;
            _vCalidadRequisitos_5_4EXANTEService = VCalidadRequisitos_5_4EXANTEService;
            _vCalidadRequisitos_CABECERAEXANTEService = VCalidadRequisitos_CABECERAEXANTEService;
            _rolMenuService = rolMenuService;
        }

        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Lista(UsuarioInfo user)
        {
            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 43);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/

            ViewBag.Usuario = user;
            ViewBag.TipoRecurso = 4;
            return View();
        }

        public string GetAllLikePagin(VCalidadEXANTE filtro, int pageIndex, int pageSize)
        {
            int totalRows = 0;


            List<VCalidadEXANTE> lista = _VCalidadEXANTEService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);
            //List<VListaEntidadACREXANTE> lista = _VListaEntidadACREXANTEService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    ICODCALIDADEXANTE = x.ICODCALIDADEXANTE,
                    EntidadId = x.EntidadId,
                    Nombre = x.Nombre,
                    Codigo = x.Codigo,
                    Acronimo = x.Acronimo,
                    nomproyecto = x.nomproyecto,
                    Denominacion = x.Denominacion,
                    NORMASUT = x.NORMASUT,
                    MigrarEntidad = x.MigrarEntidad

                }),
                total = totalRows
            });
        }

        [HttpPost]
        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Guardar(long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = "OK";

                    Entidad obj = _entidadService.GetOne(id);

                    obj.ActivarACR = 1;
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




        [HttpPost]
        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult GenerarNuevo(int ICODCALIDADEXANTE, int tipoNormaId, string numero, string descripcion, string articulo, string fechaPublicacion, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = "";

                    ResultadoEXANTE result = new ResultadoEXANTE(true);
                    List<VCalidadEXANTE> listaEntidadACR = _VCalidadEXANTEService.GetAllexante(ICODCALIDADEXANTE);


                    foreach (var procacr in listaEntidadACR)
                    {
                        List<VCalidadRequisitos_4_2EXANTE> listaCalidadRequisitos_4_2EXANTE = _vCalidadRequisitos_4_2EXANTEService.GetAll(procacr.ICODCALIDADEXANTE);

                        List<VCalidadRequisitos_5_4EXANTE> listaCalidadRequisitos_5_4EXANTE = _vCalidadRequisitos_5_4EXANTEService.GetAll(procacr.ICODCALIDADEXANTE);

                        List<VCalidadRequisitos_CABECERAEXANTE> listaCalidadRequisitos_CABECERAEXANTE = _vCalidadRequisitos_CABECERAEXANTEService.GetAll(procacr.ICODCALIDADEXANTE);

                        Procedimiento proc = new Procedimiento();
                        proc.TipoProcedimiento = (TipoProcedimiento)Enum.Parse(typeof(TipoProcedimiento), procacr.TIPOPROCEDIMIENTO.ToString());
                        //proc.CodigoACREXANTE = procacr.ICODCALIDADEXANTE.ToString().Trim();
                        proc.CodigoACR = procacr.ICODCALIDADEXANTE.ToString().Trim();
                        proc.Codigo = "S/C";
                        proc.Denominacion = procacr.Denominacion.Trim();
                        proc.Operacion = OperacionExpediente.Nuevo;
                        proc.Calificacion = (CalificacionProcedimiento)Enum.Parse(typeof(CalificacionProcedimiento), procacr.CALIFICACION.ToString().Trim());
                        proc.TipoAtencionId = 10;
                        proc.BaseLegalTexto = procacr.BASELEGAL.Trim();
                        proc.Renovacio = procacr.RENOVACION;
                        proc.Plazorenovacion = procacr.Plazorenovacion;
                        proc.Objetivo = procacr.Descripcion.Trim();
                        proc.FrecuenciaRenovacion = procacr.FRECUENCIARENOVACION;
                        proc.Requisito = new List<Requisito>();
                        proc.PlazoAtencion = procacr.PLAZOATENCION;
                        proc.UserCreacion = "ACRExante";
                        proc.UserModificacion = user.NroDocumento;
                        proc.FecCreacion = DateTime.Now;
                        proc.FecModificacion = DateTime.Now;
                        proc.SustTecCalificacion = procacr.sustento.Trim();
                        proc.Organo = procacr.ORGANO.Trim();
                        proc.TipoACR = 2;
                        proc.TipoPlazo = TipoPlazo.habiles;
                        proc.TablaAsme = new List<TablaAsme>()
                            {
                                new TablaAsme() {
                                    Codigo = "0000-01",
                                    Descripcion = "-",
                                    UserCreacion = "ACRExante",
                                    UserModificacion = user.NroDocumento,
                                    FecCreacion = DateTime.Now,
                                    FecModificacion = DateTime.Now
                                }
                            };

                        proc.BaseLegal = new BaseLegal();

                        if (fechaPublicacion != "")
                        {
                            proc.BaseLegal.BaseLegalNorma = new List<BaseLegalNorma>()
                            {
                                new BaseLegalNorma()
                                {
                                    TipoNorma = _metaDatoService.GetOne(tipoNormaId),
                                    Numero = numero,
                                    Descripcion = descripcion,
                                    Articulo = articulo,
                                    FechaPublicacion = Convert.ToDateTime(fechaPublicacion),
                                    TipoBaseLegal= TipoBaseLegal.Adjunto,
                                    BaseLegalNormaId=0,
                                    EstadoACR="3"
                                }
                            };
                        }
                        else
                        {
                            proc.BaseLegal.BaseLegalNorma = new List<BaseLegalNorma>()
                            {
                                new BaseLegalNorma()
                                {
                                    TipoNorma = _metaDatoService.GetOne(tipoNormaId),
                                    Numero = numero,
                                    Descripcion = descripcion,
                                    Articulo = articulo,
                                    TipoBaseLegal= TipoBaseLegal.Adjunto,
                                    BaseLegalNormaId=0,
                                    EstadoACR="3"
                                }
                            };
                        }


                        proc.ProcedimientoDatoAdicional = new List<ProcedimientoDatoAdicional>();
                        proc.ProcedimientoSede = new List<ProcedimientoSede>();
                        proc.PasoSeguir = new List<PasoSeguir>();

                        proc.Expediente = null;
                        proc.UndOrgResponsable = null;

                        int numreq = 1;
                        if (listaCalidadRequisitos_CABECERAEXANTE.Count != 0)
                        {
                            if (listaCalidadRequisitos_5_4EXANTE.Count != 0)
                            {
                                foreach (var req in listaCalidadRequisitos_5_4EXANTE)
                                {
                                    proc.Requisito.Add(new Requisito()
                                    {
                                        Nombre = req.VREQUISITO,
                                        //BaseLegalTexto = req.VNORMA.Trim(),
                                        BaseLegalTexto = "null",
                                        TipoRequisito = TipoRequisito.General,
                                        UserCreacion = "ACRExante",
                                        RecNum = numreq,
                                        BaseLegal = new BaseLegal()
                                    });

                                    numreq = numreq + 1;
                                }
                                var textosustento = "";
                                foreach (var req in listaCalidadRequisitos_4_2EXANTE)
                                {
                                    textosustento = textosustento + '\n' + req.VNORMA.Trim();
                                }
                                proc.Sustentolegalrequisitotexto = textosustento;

                            }

                        }
                        else
                        {
                            foreach (var req in listaCalidadRequisitos_4_2EXANTE)
                            {
                                proc.Requisito.Add(new Requisito()
                                {
                                    Nombre = req.VREQUISITO,
                                    BaseLegalTexto = req.VNORMA.Trim(),
                                    TipoRequisito = TipoRequisito.General,
                                    UserCreacion = "ACRExante",
                                    RecNum = numreq,
                                    BaseLegal = new BaseLegal()
                                });

                                numreq = numreq + 1;
                            }
                        }



                        MaestroFijo fijo = _maestroFijoService.GetOneByEntidad(user.EntidadId);
                        if (fijo != null)
                        {
                            if (!string.IsNullOrEmpty(fijo.Telefono)) proc.Telefono = fijo.Telefono;
                            if (!string.IsNullOrEmpty(fijo.Anexo)) proc.Anexo = fijo.Anexo;
                            if (!string.IsNullOrEmpty(fijo.Correo)) proc.Correo = fijo.Correo;

                            if (fijo.MaestroFijoSede != null)
                                if (fijo.MaestroFijoSede.Count() > 0)
                                    fijo.MaestroFijoSede.ForEach(sede => proc.ProcedimientoSede.Add(new ProcedimientoSede()
                                    {
                                        SedeId = sede.SedeId
                                    }));

                            if (fijo.MaestroFijoDatoAdicional != null)
                                if (fijo.MaestroFijoDatoAdicional.Count() > 0)
                                    fijo.MaestroFijoDatoAdicional.ForEach(da => proc.ProcedimientoDatoAdicional.Add(new ProcedimientoDatoAdicional()
                                    {
                                        MetaDatoId = da.MetaDatoId,
                                        Comentario = da.Comentario
                                    }));

                            if (fijo.MaestroFijoPasoSeguir != null)
                                if (fijo.MaestroFijoPasoSeguir.Count() > 0)
                                    fijo.MaestroFijoPasoSeguir.ForEach(p => proc.PasoSeguir.Add(new PasoSeguir()
                                    {
                                        PasoSeguirId = p.PasoSeguirId,
                                        Descripcion = p.Descripcion
                                    }));
                        }
                        List<string> mensajes = _ProcedimientoService.ImportarProcedimientoACR(proc, user.EntidadId);
                        if (mensajes.Count() > 0) result.Mensajes.AddRange(mensajes);
                    }

                    result.Exito = result.Mensajes.Count == 0;
                    if (result.Exito == true)
                    {

                        mensaje = "Se Importo la información del Sistema de ACR Exante";

                    }
                    else
                    {
                        mensaje = result.Mensajes[0].ToString();
                    }



                    if (mensaje != "0")
                    {
                        Entidad obj = _entidadService.GetOne(user.EntidadId);

                        obj.ActivarACR = 0;
                        obj.Ejecucion = obj.Ejecucion + 1;
                        _entidadService.Save(obj);

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
                ModelState.AddModelError("editarUIT", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }




        [HttpPost]
        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult GenerarNuevoMigrar(int ICODCALIDADEXANTE, int tipoNormaId, string numero, string descripcion, string articulo, string fechaPublicacion, int entidadID, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = "";


                    ResultadoEXANTE result = new ResultadoEXANTE(true);
                    List<VCalidadEXANTE> listaEntidadACR = _VCalidadEXANTEService.GetAllexante(ICODCALIDADEXANTE);


                    foreach (var procacr in listaEntidadACR)
                    {
                        List<VCalidadRequisitos_4_2EXANTE> listaCalidadRequisitos_4_2EXANTE = _vCalidadRequisitos_4_2EXANTEService.GetAll(procacr.ICODCALIDADEXANTE);

                        List<VCalidadRequisitos_5_4EXANTE> listaCalidadRequisitos_5_4EXANTE = _vCalidadRequisitos_5_4EXANTEService.GetAll(procacr.ICODCALIDADEXANTE);

                        List<VCalidadRequisitos_CABECERAEXANTE> listaCalidadRequisitos_CABECERAEXANTE = _vCalidadRequisitos_CABECERAEXANTEService.GetAll(procacr.ICODCALIDADEXANTE);

                        Procedimiento proc = new Procedimiento();
                        proc.TipoProcedimiento = (TipoProcedimiento)Enum.Parse(typeof(TipoProcedimiento), procacr.TIPOPROCEDIMIENTO.ToString());
                        //proc.CodigoACREXANTE = procacr.ICODCALIDADEXANTE.ToString().Trim();
                        proc.CodigoACR = procacr.ICODCALIDADEXANTE.ToString().Trim();
                        proc.Codigo = "S/C";
                        proc.Denominacion = procacr.Denominacion.Trim();
                        proc.Operacion = OperacionExpediente.Nuevo;
                        proc.Calificacion = (CalificacionProcedimiento)Enum.Parse(typeof(CalificacionProcedimiento), procacr.CALIFICACION.ToString().Trim());
                        proc.TipoAtencionId = 10;
                        proc.BaseLegalTexto = procacr.BASELEGAL.Trim();
                        proc.Renovacio = procacr.RENOVACION;
                        proc.Plazorenovacion = procacr.Plazorenovacion;
                        proc.Objetivo = procacr.Descripcion.Trim();
                        proc.FrecuenciaRenovacion = procacr.FRECUENCIARENOVACION;
                        proc.Requisito = new List<Requisito>();
                        proc.PlazoAtencion = procacr.PLAZOATENCION;
                        proc.UserCreacion = "ACRExante";
                        proc.UserModificacion = user.NroDocumento;
                        proc.FecCreacion = DateTime.Now;
                        proc.FecModificacion = DateTime.Now;
                        proc.SustTecCalificacion = procacr.sustento.Trim();
                        proc.Organo = procacr.ORGANO.Trim();
                        proc.TipoACR = 2;
                        proc.MigracionEntidad = entidadID;
                        proc.TipoPlazo = TipoPlazo.habiles;
                        proc.TablaAsme = new List<TablaAsme>()
                            {
                                new TablaAsme() {
                                    Codigo = "0000-01",
                                    Descripcion = "-",
                                    UserCreacion = "ACRExante",
                                    UserModificacion =user.NroDocumento,
                                    FecCreacion = DateTime.Now,
                                    FecModificacion = DateTime.Now
                                }
                            };

                        proc.BaseLegal = new BaseLegal();

                        if (fechaPublicacion != "")
                        {
                            proc.BaseLegal.BaseLegalNorma = new List<BaseLegalNorma>()
                            {
                                new BaseLegalNorma()
                                {
                                    TipoNorma = _metaDatoService.GetOne(tipoNormaId),
                                    Numero = numero,
                                    Descripcion = descripcion,
                                    Articulo = articulo,
                                    FechaPublicacion = Convert.ToDateTime(fechaPublicacion),
                                    TipoBaseLegal= TipoBaseLegal.Adjunto,
                                    BaseLegalNormaId=0,
                                    EstadoACR="3"
                                }
                            };
                        }
                        else
                        {
                            proc.BaseLegal.BaseLegalNorma = new List<BaseLegalNorma>()
                            {
                                new BaseLegalNorma()
                                {
                                    TipoNorma = _metaDatoService.GetOne(tipoNormaId),
                                    Numero = numero,
                                    Descripcion = descripcion,
                                    Articulo = articulo,
                                    TipoBaseLegal= TipoBaseLegal.Adjunto,
                                    BaseLegalNormaId=0,
                                    EstadoACR="3"
                                }
                            };
                        }


                        proc.ProcedimientoDatoAdicional = new List<ProcedimientoDatoAdicional>();
                        proc.ProcedimientoSede = new List<ProcedimientoSede>();
                        proc.PasoSeguir = new List<PasoSeguir>();

                        proc.Expediente = null;
                        proc.UndOrgResponsable = null;

                        int numreq = 1;
                        if (listaCalidadRequisitos_CABECERAEXANTE.Count != 0)
                        {
                            if (listaCalidadRequisitos_5_4EXANTE.Count != 0)
                            {
                                foreach (var req in listaCalidadRequisitos_5_4EXANTE)
                                {
                                    proc.Requisito.Add(new Requisito()
                                    {
                                        Nombre = req.VREQUISITO,
                                        BaseLegalTexto = "null",
                                        TipoRequisito = TipoRequisito.General,
                                        UserCreacion = "ACRExante",
                                        RecNum = numreq,
                                        BaseLegal = new BaseLegal()
                                    });

                                    numreq = numreq + 1;
                                }
                                var textosustento = "";
                                foreach (var req in listaCalidadRequisitos_4_2EXANTE)
                                {
                                    textosustento = textosustento + '\n' + req.VNORMA.Trim();
                                }
                                proc.Sustentolegalrequisitotexto = textosustento;

                            }

                        }
                        else
                        {
                            foreach (var req in listaCalidadRequisitos_4_2EXANTE)
                            {
                                proc.Requisito.Add(new Requisito()
                                {
                                    Nombre = req.VREQUISITO,
                                    BaseLegalTexto = req.VNORMA.Trim(),
                                    TipoRequisito = TipoRequisito.General,
                                    UserCreacion = "ACRExante",
                                    RecNum = numreq,
                                    BaseLegal = new BaseLegal()
                                });

                                numreq = numreq + 1;
                            }
                        }



                        //MaestroFijo fijo = _maestroFijoService.GetOneByEntidad(user.EntidadId);

                        MaestroFijo fijo = _maestroFijoService.GetOneByEntidad(entidadID);
                        if (fijo != null)
                        {
                            if (!string.IsNullOrEmpty(fijo.Telefono)) proc.Telefono = fijo.Telefono;
                            if (!string.IsNullOrEmpty(fijo.Anexo)) proc.Anexo = fijo.Anexo;
                            if (!string.IsNullOrEmpty(fijo.Correo)) proc.Correo = fijo.Correo;

                            if (fijo.MaestroFijoSede != null)
                                if (fijo.MaestroFijoSede.Count() > 0)
                                    fijo.MaestroFijoSede.ForEach(sede => proc.ProcedimientoSede.Add(new ProcedimientoSede()
                                    {
                                        SedeId = sede.SedeId
                                    }));

                            if (fijo.MaestroFijoDatoAdicional != null)
                                if (fijo.MaestroFijoDatoAdicional.Count() > 0)
                                    fijo.MaestroFijoDatoAdicional.ForEach(da => proc.ProcedimientoDatoAdicional.Add(new ProcedimientoDatoAdicional()
                                    {
                                        MetaDatoId = da.MetaDatoId,
                                        Comentario = da.Comentario
                                    }));

                            if (fijo.MaestroFijoPasoSeguir != null)
                                if (fijo.MaestroFijoPasoSeguir.Count() > 0)
                                    fijo.MaestroFijoPasoSeguir.ForEach(p => proc.PasoSeguir.Add(new PasoSeguir()
                                    {
                                        PasoSeguirId = p.PasoSeguirId,
                                        Descripcion = p.Descripcion
                                    }));
                        }

                        //List<string> mensajes = _ProcedimientoService.ImportarProcedimientoACR(proc, user.EntidadId);
                        List<string> mensajes = _ProcedimientoService.ImportarProcedimientoACR(proc, entidadID);
                        if (mensajes.Count() > 0) result.Mensajes.AddRange(mensajes);
                    }

                    result.Exito = result.Mensajes.Count == 0;
                    if (result.Exito == true)
                    {

                        mensaje = "Se Importo la información del Sistema de ACR Exante";

                    }
                    else
                    {
                        mensaje = result.Mensajes[0].ToString();
                    }



                    if (mensaje != "0")
                    {
                        Entidad obj = _entidadService.GetOne(user.EntidadId);
                        //Entidad obj = _entidadService.GetOne(entidadID);

                        obj.ActivarACR = 0;
                        obj.Ejecucion = obj.Ejecucion + 1;
                        _entidadService.Save(obj);

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
                ModelState.AddModelError("editarUIT", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }



        [HttpPost]
        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult GenerarReemplazo(int ICODCALIDADEXANTE, int tipoNormaId, string numero, string descripcion, string articulo, string fechaPublicacion, long procedimientoId, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = "";


                    Procedimiento modelproce = _ProcedimientoService.GetOne(procedimientoId);



                    ResultadoEXANTE result = new ResultadoEXANTE(true);
                    List<VCalidadEXANTE> listaEntidadACR = _VCalidadEXANTEService.GetAllexante(ICODCALIDADEXANTE);


                    foreach (var procacr in listaEntidadACR)
                    {
                        List<VCalidadRequisitos_4_2EXANTE> listaCalidadRequisitos_4_2EXANTE = _vCalidadRequisitos_4_2EXANTEService.GetAll(procacr.ICODCALIDADEXANTE);

                        List<VCalidadRequisitos_5_4EXANTE> listaCalidadRequisitos_5_4EXANTE = _vCalidadRequisitos_5_4EXANTEService.GetAll(procacr.ICODCALIDADEXANTE);

                        List<VCalidadRequisitos_CABECERAEXANTE> listaCalidadRequisitos_CABECERAEXANTE = _vCalidadRequisitos_CABECERAEXANTEService.GetAll(procacr.ICODCALIDADEXANTE);

                        Procedimiento proc = new Procedimiento();
                        proc.TipoProcedimiento = (TipoProcedimiento)Enum.Parse(typeof(TipoProcedimiento), procacr.TIPOPROCEDIMIENTO.ToString());

                        proc.CodigoACR = procacr.ICODCALIDADEXANTE.ToString().Trim();

                        //mantener
                        proc.Codigo = modelproce.Codigo;
                        proc.UndOrgResponsableId = modelproce.UndOrgResponsableId;
                        proc.UndOrgReconsideracionId = modelproce.UndOrgReconsideracionId;
                        proc.UndOrgApelacionId = modelproce.UndOrgApelacionId;
                        proc.Telefono = modelproce.Telefono;
                        proc.Anexo = modelproce.Anexo;
                        proc.Correo = modelproce.Correo;
                        proc.CategoriaProcedimientoId = modelproce.CategoriaProcedimientoId;
                        proc.TipoProcedimientoId = modelproce.TipoProcedimientoId;
                        proc.CodigoCorto = modelproce.CodigoCorto;


                        proc.CargoReconsideracion = modelproce.CargoReconsideracion;
                        proc.CargoApelacion = modelproce.CargoApelacion;
                        proc.CargoResponsable = modelproce.CargoResponsable;

                        proc.PzoApelPresent = modelproce.PzoApelPresent;
                        proc.PzoApelResol = modelproce.PzoApelResol;
                        proc.PzoReconPresent = modelproce.PzoReconPresent;
                        proc.PzoReconResol = modelproce.PzoReconResol;
                        proc.Numero = modelproce.Numero;
                        proc.BaseLegalId = modelproce.BaseLegalId;


                        proc.Denominacion = procacr.Denominacion.Trim();
                        proc.Operacion = OperacionExpediente.Nuevo;
                        proc.Calificacion = (CalificacionProcedimiento)Enum.Parse(typeof(CalificacionProcedimiento), procacr.CALIFICACION.ToString().Trim());
                        proc.TipoAtencionId = 10;
                        proc.BaseLegalTexto = procacr.BASELEGAL.Trim();
                        proc.Renovacio = procacr.RENOVACION;
                        proc.Plazorenovacion = procacr.Plazorenovacion;
                        proc.Objetivo = procacr.Descripcion.Trim();
                        proc.FrecuenciaRenovacion = procacr.FRECUENCIARENOVACION;
                        proc.Requisito = new List<Requisito>();
                        proc.PlazoAtencion = procacr.PLAZOATENCION;
                        proc.UserCreacion = "ACRExante";
                        proc.UserModificacion = user.NroDocumento;
                        proc.FecCreacion = DateTime.Now;
                        proc.FecModificacion = DateTime.Now;
                        proc.SustTecCalificacion = procacr.sustento.Trim();
                        proc.Organo = procacr.ORGANO.Trim();
                        proc.TipoPlazo = TipoPlazo.habiles;
                        //proc.TablaAsme = modelproce.TablaAsme;

                        List<TablaAsme> asme = new List<TablaAsme>();


                        foreach (var reqtabla in modelproce.TablaAsme)
                        {

                            asme.Add(new TablaAsme()
                            {
                                Codigo = reqtabla.Codigo,
                                Descripcion = reqtabla.Descripcion,
                                Prestaciones = reqtabla.Prestaciones,
                                Personal = reqtabla.Personal,
                                MaterialFungible = reqtabla.MaterialFungible,
                                MaterialNoFungible = reqtabla.MaterialNoFungible,
                                ServicioIdentificable = reqtabla.ServicioIdentificable,
                                ServicioTerceros = reqtabla.ServicioTerceros,
                                Depreciacion = reqtabla.Depreciacion,
                                Fijos = reqtabla.Fijos,
                                CostoUnitario = reqtabla.CostoUnitario,
                                Subvencion = reqtabla.Subvencion,
                                PctSubvencion = reqtabla.PctSubvencion,
                                DerechoTramitacion = reqtabla.DerechoTramitacion,
                                DescripcionSusustento = reqtabla.DescripcionSusustento,
                                DescripcionRespuesa = reqtabla.DescripcionRespuesa,
                                FecEnvioMef = reqtabla.FecEnvioMef,
                                FecRespuestaMef = reqtabla.FecRespuestaMef,
                                UITID = reqtabla.UITID,
                                EsGratuito = reqtabla.EsGratuito,
                                AutorizacionMEF = reqtabla.AutorizacionMEF,
                                AsmeActualId = reqtabla.AsmeActualId,
                                UserCreacion = "ACRExante",
                                UserModificacion = user.NroDocumento,
                                FecCreacion = DateTime.Now,
                                FecModificacion = DateTime.Now,


                            });
                        }

                        var i = 0;

                        foreach (var reqtabla in modelproce.TablaAsme)
                        {
                            List<Actividad> Actividadasme = new List<Actividad>();
                            foreach (Actividad a in reqtabla.Actividad)
                            {
                                Actividadasme.Add(new Actividad()
                                {
                                    Descripcion = a.Descripcion,
                                    Duracion = a.Duracion,
                                    Orden = a.Orden,
                                    TipoActividad = a.TipoActividad,
                                    TipoValor = a.TipoValor,
                                    UnidadOrganicaId = a.UnidadOrganicaId,
                                    ActividadRecurso = a.ActividadRecurso == null ? new List<ActividadRecurso>() : a.ActividadRecurso.Select(x => new ActividadRecurso()
                                    {
                                        Cantidad = x.Cantidad,
                                        RecursoId = x.RecursoId
                                    }).ToList()
                                });
                                asme[i].Actividad = Actividadasme;

                            }
                            i = i + 1;
                        }

                        proc.TablaAsme = asme;

                        proc.BaseLegal = new BaseLegal();

                        if (fechaPublicacion != "")
                        {
                            proc.BaseLegal.BaseLegalNorma = new List<BaseLegalNorma>()
                            {
                                new BaseLegalNorma()
                                {
                                    TipoNorma = _metaDatoService.GetOne(tipoNormaId),
                                    Numero = numero,
                                    Descripcion = descripcion,
                                    Articulo = articulo,
                                    FechaPublicacion = Convert.ToDateTime(fechaPublicacion),
                                    TipoBaseLegal= TipoBaseLegal.Adjunto,
                                    BaseLegalNormaId=0,
                                    EstadoACR="3"
                                }
                            };
                        }
                        else
                        {
                            proc.BaseLegal.BaseLegalNorma = new List<BaseLegalNorma>()
                            {
                                new BaseLegalNorma()
                                {
                                    TipoNorma = _metaDatoService.GetOne(tipoNormaId),
                                    Numero = numero,
                                    Descripcion = descripcion,
                                    Articulo = articulo,
                                    TipoBaseLegal= TipoBaseLegal.Adjunto,
                                    BaseLegalNormaId=0,
                                    EstadoACR="3"
                                }
                            };
                        }


                        proc.ProcedimientoDatoAdicional = new List<ProcedimientoDatoAdicional>();
                        proc.ProcedimientoSede = new List<ProcedimientoSede>();
                        proc.PasoSeguir = new List<PasoSeguir>();

                        proc.Expediente = null;
                        //proc.UndOrgResponsable = null;

                        int numreq = 1;
                        if (listaCalidadRequisitos_CABECERAEXANTE.Count != 0)
                        {
                            if (listaCalidadRequisitos_5_4EXANTE.Count != 0)
                            {
                                foreach (var req in listaCalidadRequisitos_5_4EXANTE)
                                {
                                    proc.Requisito.Add(new Requisito()
                                    {
                                        Nombre = req.VREQUISITO,
                                        BaseLegalTexto = "null",
                                        TipoRequisito = TipoRequisito.General,
                                        UserCreacion = "ACRExante",
                                        RecNum = numreq,
                                        BaseLegal = new BaseLegal()
                                    });

                                    numreq = numreq + 1;
                                }
                                var textosustento = "";
                                foreach (var req in listaCalidadRequisitos_4_2EXANTE)
                                {
                                    textosustento = textosustento + '\n' + req.VNORMA.Trim();
                                }
                                proc.Sustentolegalrequisitotexto = textosustento;

                            }

                        }
                        else
                        {
                            foreach (var req in listaCalidadRequisitos_4_2EXANTE)
                            {
                                proc.Requisito.Add(new Requisito()
                                {
                                    Nombre = req.VREQUISITO,
                                    BaseLegalTexto = req.VNORMA.Trim(),
                                    TipoRequisito = TipoRequisito.General,
                                    UserCreacion = "ACRExante",
                                    RecNum = numreq,
                                    BaseLegal = new BaseLegal()
                                });

                                numreq = numreq + 1;
                            }
                        }



                        MaestroFijo fijo = _maestroFijoService.GetOneByEntidad(user.EntidadId);
                        if (fijo != null)
                        {
                            if (!string.IsNullOrEmpty(fijo.Telefono)) proc.Telefono = fijo.Telefono;
                            if (!string.IsNullOrEmpty(fijo.Anexo)) proc.Anexo = fijo.Anexo;
                            if (!string.IsNullOrEmpty(fijo.Correo)) proc.Correo = fijo.Correo;

                            if (fijo.MaestroFijoSede != null)
                                if (fijo.MaestroFijoSede.Count() > 0)
                                    fijo.MaestroFijoSede.ForEach(sede => proc.ProcedimientoSede.Add(new ProcedimientoSede()
                                    {
                                        SedeId = sede.SedeId
                                    }));

                            if (fijo.MaestroFijoDatoAdicional != null)
                                if (fijo.MaestroFijoDatoAdicional.Count() > 0)
                                    fijo.MaestroFijoDatoAdicional.ForEach(da => proc.ProcedimientoDatoAdicional.Add(new ProcedimientoDatoAdicional()
                                    {
                                        MetaDatoId = da.MetaDatoId,
                                        Comentario = da.Comentario
                                    }));

                            if (fijo.MaestroFijoPasoSeguir != null)
                                if (fijo.MaestroFijoPasoSeguir.Count() > 0)
                                    fijo.MaestroFijoPasoSeguir.ForEach(p => proc.PasoSeguir.Add(new PasoSeguir()
                                    {
                                        PasoSeguirId = p.PasoSeguirId,
                                        Descripcion = p.Descripcion
                                    }));
                        }
                        List<string> mensajes = _ProcedimientoService.ImportarProcedimientoACR(proc, user.EntidadId);
                        if (mensajes.Count() > 0) result.Mensajes.AddRange(mensajes);
                    }

                    result.Exito = result.Mensajes.Count == 0;
                    if (result.Exito == true)
                    {

                        mensaje = "Se Importo la información del Sistema de ACR Exante";

                        //Procedimiento model3 = new Procedimiento();


                        modelproce.Estado = 3;
                        //modelproce.ProcedimientoId = modelproce.ProcedimientoId;
                        _ProcedimientoService.Eliminacionprocedimiento(modelproce);



                    }
                    else
                    {
                        mensaje = result.Mensajes[0].ToString();
                    }



                    if (mensaje != "0")
                    {
                        Entidad obj = _entidadService.GetOne(user.EntidadId);

                        obj.ActivarACR = 0;
                        obj.Ejecucion = obj.Ejecucion + 1;
                        _entidadService.Save(obj);

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
                ModelState.AddModelError("editarUIT", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }

        public ActionResult Reemplazar(bool multi, long ExpedienteId, long TablaAsmeId, long ProcedimientoId)
        {
            try
            {
                ViewBag.Multi = false;
                ViewBag.ExpedienteId = ExpedienteId;
                ViewBag.TablaAsmeId = TablaAsmeId;
                ViewBag.ProcedimientoIdDes = ProcedimientoId;

                return PartialView("_SelectCopiarAsme");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Nuevo(int ICODCALIDADEXANTE, long EntidadId, int pantalla)
        {
            try
            {
                ViewBag.ICODCALIDADEXANTE = ICODCALIDADEXANTE;


                List<Expediente> exp = _expedienteService.GetByEntidad(EntidadId).Where(x => (x.EstadoExpediente == EstadoExpediente.EnProceso || x.EstadoExpediente == EstadoExpediente.Observado)).ToList();
                //List<Expediente> exp = _expedienteService.GetByEntidad(EntidadId).Where(x => (x.EstadoExpediente == EstadoExpediente.EnProceso || x.EstadoExpediente == EstadoExpediente.Observado) && x.FecPresentacion == null).ToList();

                if (exp.Count() == 0)
                {

                    Response.StatusCode = (int)HttpStatusCode.BadRequest;

                    return PartialView("_Error");
                }


                ViewBag.ExpedienteId = exp[0].ExpedienteId;


                ViewBag.pantalla = pantalla;

                BaseLegalNorma model = new BaseLegalNorma();


                List<MetaDato> listaTipoNorma1 = _metaDatoService.GetByParent(12).Where(x => x.MetaDatoId == 133 || x.MetaDatoId == 134 || x.MetaDatoId == 151 || x.MetaDatoId == 138).ToList();
                List<SelectListItem> listaTipoNorma = listaTipoNorma1
                                        .Select(x => new SelectListItem()
                                        {
                                            Value = x.MetaDatoId.ToString(),
                                            Text = x.Nombre
                                        })
                                        .ToList();
                listaTipoNorma.Insert(0, new SelectListItem() { Value = "0", Text = "- SELECCIONAR -" });

                /*lista de entidad INICIO*/
                var listaEntidad = _entidadService.GetAll()
                                              .Where(x => x.Estado == Respuesta.Si && x.EntidadId != EntidadId)
                                              .OrderBy(x => x.Nombre)
                                              .ToList();
                listaEntidad.Insert(0, new Entidad()
                {
                    EntidadId = 0,
                    Nombre = " - SELECCIONAR - "
                });
                ViewBag.ListaEntidad = listaEntidad.Select(x => new SelectListItem()
                {
                    Value = x.EntidadId.ToString(),
                    Text = x.Nombre
                }).ToList();
                /*lista de entidad FIN*/



                ViewBag.listTipoNorma = listaTipoNorma;


                return PartialView("_EditarDetalleNorma", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }


    public class ResultadoEXANTE
    {
        public ResultadoEXANTE()
        {
            this.Exito = false;
            this.Mensajes = new List<string>();
        }

        public ResultadoEXANTE(bool Exito)
        {
            this.Exito = Exito;
            this.Mensajes = new List<string>();
        }

        public bool Exito { get; set; }
        public List<string> Mensajes { get; set; }
    }
}