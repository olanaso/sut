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
    public class ACRController : Controller
    {
        private readonly ILogService<ACRController> _log;
        private readonly IVCalidadService _VCalidadService;
        private readonly IVCalidadRequisitosService _VCalidadRequisitosService;
        private readonly IVListaEntidadACRService _VListaEntidadACRService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly IMaestroFijoService _maestroFijoService;
        private readonly IProcedimientoService _ProcedimientoService;
        private readonly IEntidadService _entidadService;


        private readonly IVCalidadRequisitos_4_2Service _vCalidadRequisitos_4_2Service;
        private readonly IVCalidadRequisitos_5_4Service _vCalidadRequisitos_5_4Service;
        private readonly IVCalidadRequisitos_CABECERAService _vCalidadRequisitos_CABECERAService;
        private readonly IRolMenuService _rolMenuService;

        Auditoria objauditoria = new Auditoria();
        public ACRController(IAuditoriaService AuditoriaService, IVCalidadService VCalidadService, IVCalidadRequisitosService VCalidadRequisitosService, IVListaEntidadACRService VListaEntidadACRService, IProcedimientoService ProcedimientoService, IMaestroFijoService MaestroFijoService, IEntidadService EntidadService, IVCalidadRequisitos_4_2Service VCalidadRequisitos_4_2Service, IVCalidadRequisitos_5_4Service VCalidadRequisitos_5_4Service, IVCalidadRequisitos_CABECERAService VCalidadRequisitos_CABECERAService, IRolMenuService rolMenuService)
        {
            _log = new LogService<ACRController>();
            _VCalidadService = VCalidadService;
            _VCalidadRequisitosService = VCalidadRequisitosService;
            _VListaEntidadACRService = VListaEntidadACRService;
            _AuditoriaService = AuditoriaService;
            _ProcedimientoService = ProcedimientoService;
            _maestroFijoService = MaestroFijoService;
            _entidadService = EntidadService;
            _vCalidadRequisitos_4_2Service = VCalidadRequisitos_4_2Service;
            _vCalidadRequisitos_5_4Service = VCalidadRequisitos_5_4Service;
            _vCalidadRequisitos_CABECERAService = VCalidadRequisitos_CABECERAService;
            _rolMenuService = rolMenuService;
        }

        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Lista(UsuarioInfo user)
        {

            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 42);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/

            var listaEntidadACR = _VListaEntidadACRService.GetAll();
            if (user.EntidadId == 4105 || user.EntidadId == 0)
            {
                listaEntidadACR = _VListaEntidadACRService.GetAll()
                                         //.Where(x => x.Estado == Respuesta.Si)
                                         .OrderBy(x => x.Codigo)
                                         .ToList();
            }
            else
            {
                listaEntidadACR = _VListaEntidadACRService.GetAll()
                                  .Where(x => x.Codigo == user.CodEntidad)
                                  .OrderBy(x => x.Codigo)
                                  .ToList();
            }

            listaEntidadACR.Insert(0, new VListaEntidadACR()
            {
                Codigo = "0",
                nombre = " - SELECCIONAR - "
            });

            ViewBag.ListaEntidadACR = listaEntidadACR.Select(x => new SelectListItem()
            {
                Value = x.Codigo.ToString(),
                Text = x.nombre
            }).ToList();

            ViewBag.Usuario = user;
            ViewBag.TipoRecurso = 4;
            return View();
        }

        public string GetAllLikePagin(VListaEntidadACR filtro, int pageIndex, int pageSize)
        {
            int totalRows = 0;

            List<VListaEntidadACR> lista = _VListaEntidadACRService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

            return JsonConvert.SerializeObject(new
            {
                data = lista.Select(x => new
                {
                    EntidadId = x.EntidadId,
                    Nombre = x.nombre,
                    Codigo = x.Codigo,
                    Acronimo = x.Acronimo,
                    ActivarACR = x.ActivarACR == 1 ? "SI" : "NO",
                    Ejecucion = x.Ejecucion,

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
        public ActionResult Generar(string codigo, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = "";
                    Entidad entidadcodigo = _entidadService.GetOne(codigo);
                    if (entidadcodigo != null)
                    {


                        Resultado result = new Resultado(true);
                        List<VCalidad> listaEntidadACR = _VCalidadService.GetAll(codigo);



                        foreach (var procacr in listaEntidadACR)
                        {
                            List<VCalidadRequisitos_4_2> listaCalidadRequisitos_4_2 = _vCalidadRequisitos_4_2Service.GetAll(procacr.ICODCALIDAD);

                            List<VCalidadRequisitos_5_4> listaCalidadRequisitos_5_4 = _vCalidadRequisitos_5_4Service.GetAll(procacr.ICODCALIDAD);

                            List<VCalidadRequisitos_CABECERA> listaCalidadRequisitos_CABECERA = _vCalidadRequisitos_CABECERAService.GetAll(procacr.ICODCALIDAD);

                            Procedimiento proc = new Procedimiento();
                            proc.TipoProcedimiento = (TipoProcedimiento)Enum.Parse(typeof(TipoProcedimiento), procacr.TIPOPROCEDIMIENTO.ToString());
                            proc.CodigoACR = procacr.ICODCALIDAD.ToString().Trim();
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
                            proc.UserCreacion = "ACR";
                            proc.UserModificacion = user.NroDocumento;
                            proc.FecCreacion = DateTime.Now;
                            proc.FecModificacion = DateTime.Now;
                            proc.SustTecCalificacion = procacr.sustento.Trim();
                            proc.Organo = procacr.ORGANO.Trim();
                            proc.TipoACR = 1;
                            proc.TipoPlazo = TipoPlazo.habiles;
                            proc.TablaAsme = new List<TablaAsme>()
                            {
                                new TablaAsme() {
                                    Codigo = "0000-01",
                                    Descripcion = "-",
                                    UserCreacion = "ACR",
                                    UserModificacion = user.NroDocumento,
                                    FecCreacion = DateTime.Now,
                                    FecModificacion = DateTime.Now
                                }
                            };

                            proc.BaseLegal = new BaseLegal();
                            proc.ProcedimientoDatoAdicional = new List<ProcedimientoDatoAdicional>();
                            proc.ProcedimientoSede = new List<ProcedimientoSede>();
                            proc.PasoSeguir = new List<PasoSeguir>();

                            proc.Expediente = null;
                            proc.UndOrgResponsable = null;

                            int numreq = 1;
                            if (listaCalidadRequisitos_CABECERA.Count != 0)
                            {
                                if (listaCalidadRequisitos_5_4.Count != 0)
                                {
                                    foreach (var req in listaCalidadRequisitos_5_4)
                                    {
                                        proc.Requisito.Add(new Requisito()
                                        {
                                            Nombre = req.VREQUISITO,
                                            //BaseLegalTexto = req.VNORMA.Trim(),
                                            BaseLegalTexto = "null",
                                            TipoRequisito = TipoRequisito.General,
                                            UserCreacion = "ACR",
                                            RecNum = numreq,
                                            BaseLegal = new BaseLegal()
                                        });

                                        numreq = numreq + 1;
                                    }
                                    var textosustento = "";
                                    foreach (var req in listaCalidadRequisitos_4_2)
                                    {
                                        textosustento = textosustento + '\n' + req.VNORMA.Trim();
                                    }
                                    proc.Sustentolegalrequisitotexto = textosustento;

                                }

                            }
                            else
                            {
                                foreach (var req in listaCalidadRequisitos_4_2)
                                {
                                    proc.Requisito.Add(new Requisito()
                                    {
                                        Nombre = req.VREQUISITO,
                                        BaseLegalTexto = req.VNORMA.Trim(),
                                        TipoRequisito = TipoRequisito.General,
                                        UserCreacion = "ACR",
                                        RecNum = numreq,
                                        BaseLegal = new BaseLegal()
                                    });

                                    numreq = numreq + 1;
                                }
                            }

                            //if (procacr.CalidadRequisitos != null) { 
                            //foreach (var req in listarequisitos)
                            //{
                            //    proc.Requisito.Add(new Requisito()
                            //    {
                            //        Nombre = req.VREQUISITO,
                            //        BaseLegalTexto = req.VNORMA,
                            //        TipoRequisito = TipoRequisito.General,
                            //        UserCreacion = "ACR", 
                            //        BaseLegal = new BaseLegal()
                            //    });
                            //}
                            //}

                            MaestroFijo fijo = _maestroFijoService.GetOneByEntidad(entidadcodigo.EntidadId);
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
                            List<string> mensajes = _ProcedimientoService.ImportarProcedimientoACR(proc, entidadcodigo.EntidadId);
                            if (mensajes.Count() > 0) result.Mensajes.AddRange(mensajes);
                        }
                        result.Exito = result.Mensajes.Count == 0;
                        if (result.Exito == true)
                        {

                            mensaje = "Se Importo la información del Sistema de ACR";

                        }
                        else
                        {
                            mensaje = result.Mensajes[0].ToString();
                        }


                    }
                    else
                    {
                        mensaje = "no Genero";
                    }

                    if (mensaje != "0")
                    {
                        Entidad obj = _entidadService.GetOne(entidadcodigo.EntidadId);

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

    }
    public class Resultado
    {
        public Resultado()
        {
            this.Exito = false;
            this.Mensajes = new List<string>();
        }

        public Resultado(bool Exito)
        {
            this.Exito = Exito;
            this.Mensajes = new List<string>();
        }

        public bool Exito { get; set; }
        public List<string> Mensajes { get; set; }
    }
}