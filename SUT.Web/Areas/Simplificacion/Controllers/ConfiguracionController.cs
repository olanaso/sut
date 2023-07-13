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
    public class ConfiguracionController : Controller
    {
        private readonly ILogService<ConfiguracionController> _log;
        private readonly ITablaAsmeService _tablaAsmeService;
        private readonly IExpedienteService _expedienteService;
        private readonly IRecursoCostoService _recursoCostoService;
        private readonly IInductorService _inductorService;
        private readonly IUnidadOrganicaService _unidadOrganicaService;
        private readonly IFactorDedicacionService _factorDedicacionService;
        private readonly IUITService _UITService;
        private readonly IObservacionService _observacionService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly IRolMenuService _rolMenuService;

        Auditoria objauditoria = new Auditoria();
        public ConfiguracionController(ITablaAsmeService tablaAsmeService,
                                        IExpedienteService expedienteService,
                                        IRecursoCostoService recursoCostoService,
                                        IInductorService inductorService,
                                        IUnidadOrganicaService unidadOrganicaService,
                                        IFactorDedicacionService factorDedicacionService,
                                        IUITService UITService, IAuditoriaService AuditoriaService,
                                        IObservacionService observacionService,
                                        IRolMenuService rolMenuService)
        {
            _log = new LogService<ConfiguracionController>();
            _tablaAsmeService = tablaAsmeService;
            _expedienteService = expedienteService;
            _recursoCostoService = recursoCostoService;
            _inductorService = inductorService;
            _unidadOrganicaService = unidadOrganicaService;
            _factorDedicacionService = factorDedicacionService;
            _UITService = UITService;
            _observacionService = observacionService;
            _AuditoriaService = AuditoriaService;
            _rolMenuService = rolMenuService;
        }

        public ActionResult Resumen(long id, UsuarioInfo user)
        {

            /*Inicio Cargar roles - Acceso*/
            //List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 27);
            //user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/
            /***** Inicio ACTIVAR DERECHO DE TRAMITE ****/
            List<RolMenu> listarolmenu = new List<RolMenu>();
            listarolmenu.AddRange(_rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 27));
            listarolmenu.AddRange(_rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 58));
            user.rolmenu = listarolmenu;
            /***** FIN ACTIVAR DERECHO DE TRAMITE ****/

            ViewBag.Usuario = user;
            Expediente model = _expedienteService.GetOne(id);

            Observacion obs = new Observacion();
            obs.ExpedienteId = id;
            obs.Estado = "1";
            obs.EntidadId = user.EntidadId;
            obs.Pantalla = "Costo Resumen";
            ViewBag.Observacion = _observacionService.GetOneGlobalObservacion(obs);


            if (model.Observacion != null)
            {
                ViewBag.ListaCostoResumen = JsonConvert.SerializeObject(
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
                            Pantalla = x.Pantalla
                        }).Where(x => x.NombreCampo.Contains("Resumen")).OrderBy(x => x.ObservacionId).ToList()
                    );
            }


            return View(model);
        }

        public ActionResult VerObsResumen(long id, long entidadid, UsuarioInfo user)
        {


            ViewBag.entidadver = entidadid;
            ViewBag.Usuario = user;
            Expediente model = _expedienteService.GetOne(id);

            Observacion obs = new Observacion();
            obs.ExpedienteId = id;
            obs.Estado = "0";
            obs.CodValidacion = "5";
            obs.EntidadId = entidadid;
            obs.Pantalla = "Costo Resumen";
            ViewBag.Observacion = _observacionService.GetOneVerObservacion(obs);


            if (model.Observacion != null)
            {
                ViewBag.ListaCostoResumen = JsonConvert.SerializeObject(
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
                            TipoEstado = x.TipoEstado
                        }).Where(x => x.NombreCampo.Contains("Resumen")).OrderBy(x => x.ObservacionId).ToList()
                    );
            }


            return View(model);
        }
        public string ResumenGetAllLikePagin(TablaAsme filtro, int pageIndex, int pageSize)
        {
            try
            {
                int totalRows = 0;


                List<TablaAsme> lista = _tablaAsmeService.ResumenGetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

                UIT montonuite = _UITService.LsitaGetOne();


                return JsonConvert.SerializeObject(new
                {
                    lista = lista.Select(x => new
                    {

                        TablaAsmeId = x.TablaAsmeId,
                        Codigo = x.Codigo,
                        UndOrganicaResponsableNombre = x.Procedimiento.UndOrgResponsable.Nombre,
                        Descripcion = string.Format("{0} {1}", x.Procedimiento.Denominacion,
                                x.Descripcion != "-" || string.IsNullOrEmpty(x.Descripcion) ? "- " + x.Descripcion : ""),
                        Prestaciones = x.Prestaciones,
                        Personal = x.Personal,
                        MaterialFungible = x.MaterialFungible,
                        ServicioIdentificable = x.ServicioIdentificable,
                        MaterialNoFungible = x.MaterialNoFungible,
                        ServicioTerceros = x.ServicioTerceros,
                        Depreciacion = x.Depreciacion,
                        Fijos = x.Fijos,
                        CostoUnitario = x.CostoUnitario,
                        EsGratuito = x.EsGratuito,
                        DerechoTramitacion = x.DerechoTramitacion,
                        x.EsSubvencion,
                        //montouit = montonuite.Monto,
                        montouit = x.UIT.Monto,
                        autorizacionmef = x.AutorizacionMEF
                    }),
                    totalRows = totalRows
                });
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _log.Error(ex);
                return ex.Message;
            }
        }

        public ActionResult CostoPersonal(long id, UsuarioInfo user)
        {


            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 28);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/

            Expediente exp = _expedienteService.GetOne(id);
            ViewBag.Expediente = exp;

            ViewBag.Usuario = user;

            ViewBag.TipoRecursos = 1;

            Observacion obscostopersonal = new Observacion();
            obscostopersonal.ExpedienteId = exp.ExpedienteId;
            obscostopersonal.Estado = "1";
            obscostopersonal.EntidadId = user.EntidadId;
            obscostopersonal.Pantalla = "Costo Personal";
            ViewBag.ObservacionCostoPersonal = _observacionService.GetOneGlobalObservacion(obscostopersonal);



            List<RecursoCosto> model = _recursoCostoService.GetCostoPersonal(id);
            foreach (RecursoCosto item in model) item.ExpedienteId = id;

            string viewName = string.Empty;
            if (exp.SustCostosTerminado) viewName = "VerCostoPersonal";
            else
            {
                switch (user.Rol)
                {
                    case (short)Rol.Administrador: viewName = exp.Entidad.TipoTupa == TipoTupa.Estandar ? "CostoPersonal" : "VerCostoPersonal"; break;
                    case (short)Rol.Coordinador: viewName = "CostoPersonal"; break;
                    case (short)Rol.Evaluador: viewName = "VerCostoPersonal"; break;
                    case (short)Rol.EntidadFiscalizadora: viewName = "VerCostoPersonal"; break;
                    case (short)Rol.Ratificador: viewName = "VerCostoPersonal"; break;
                    case (short)Rol.RegistradorProcesos: viewName = "CostoPersonal"; break; //modificar
                }
            }

            return View(viewName, model);
        }

        public ActionResult VerObsCostoPersonal(long id, long entidadid, UsuarioInfo user)
        {
            Expediente exp = _expedienteService.GetOne(id);
            ViewBag.entidadver = entidadid;
            ViewBag.Expediente = exp;
            ViewBag.Usuario = user;
            ViewBag.TipoRecursos = 1;
            Observacion obscostopersonal = new Observacion();
            obscostopersonal.ExpedienteId = exp.ExpedienteId;
            obscostopersonal.Estado = "0";
            obscostopersonal.CodValidacion = "5";
            obscostopersonal.EntidadId = entidadid;
            obscostopersonal.Pantalla = "Costo Personal";
            ViewBag.ObservacionCostoPersonal = _observacionService.GetOneVerObservacion(obscostopersonal);

            List<RecursoCosto> model = _recursoCostoService.GetCostoPersonal(id);
            foreach (RecursoCosto item in model) item.ExpedienteId = id;

            string viewName = string.Empty;
            viewName = "VerObsCostoPersonal";

            return View(viewName, model);
        }


        public ActionResult AutorizacionMef(long id, UsuarioInfo user)
        {
            try
            {
                ViewBag.User = user;
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Autorizacion Mef";
                objauditoria.Pantalla = "Autorizacion Mef";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                TablaAsme item;

                item = _tablaAsmeService.GetOne(id);

                ViewBag.publicar = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Autorizar", Value = "2" , Selected = item.AutorizacionMEF == Autorizacionmef.Autorizar ? true:false},
                    new SelectListItem() { Text = "No Autorizar", Value = "3",  Selected = item.AutorizacionMEF == Autorizacionmef.NoAutorizar ? true:false },
                };

                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
                return PartialView("_AutorizarMef", item);

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        public ActionResult CostoMatFungible(long id, UsuarioInfo user)
        {
            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 29);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/

            ViewBag.Usuario = user;

            ViewBag.TipoRecursos = 2;
            return CostoRecurso(id, TipoRecurso.MaterialFungible, user);
        }
        public ActionResult VerObsCostoMatFungible(long id, long entidadid, UsuarioInfo user)
        {
            ViewBag.Usuario = user;
            ViewBag.TipoRecursos = 2;
            return VerObsCostoRecurso(id, TipoRecurso.MaterialFungible, entidadid, user);
        }
        public ActionResult CostoServIdentificable(long id, UsuarioInfo user)
        {
            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 30);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/

            ViewBag.Usuario = user;

            ViewBag.TipoRecursos = 3;
            return CostoRecurso(id, TipoRecurso.ServicioIdentificable, user);
        }
        public ActionResult VerObsCostoServIdentificable(long id, long entidadid, UsuarioInfo user)
        {
            ViewBag.Usuario = user;
            ViewBag.TipoRecursos = 3;
            return VerObsCostoRecurso(id, TipoRecurso.ServicioIdentificable, entidadid, user);
        }
        public ActionResult CostoMatNoFungible(long id, UsuarioInfo user)
        {
            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 31);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/

            ViewBag.Usuario = user;

            ViewBag.TipoRecursos = 4;
            return CostoRecurso(id, TipoRecurso.MaterialNoFungible, user);
        }
        public ActionResult VerObsCostoMatNoFungible(long id, long entidadid, UsuarioInfo user)
        {

            ViewBag.Usuario = user;
            ViewBag.TipoRecursos = 4;
            return VerObsCostoRecurso(id, TipoRecurso.MaterialNoFungible, entidadid, user);
        }
        public ActionResult CostoServTerceros(long id, UsuarioInfo user)
        {
            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 32);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/

            ViewBag.Usuario = user;

            ViewBag.TipoRecursos = 5;
            return CostoRecurso(id, TipoRecurso.ServicioTerceros, user);
        }
        public ActionResult VerObsCostoServTerceros(long id, long entidadid, UsuarioInfo user)
        {
            ViewBag.Usuario = user;

            ViewBag.TipoRecursos = 5;
            return VerObsCostoRecurso(id, TipoRecurso.ServicioTerceros, entidadid, user);
        }
        public ActionResult CostoDepreciacion(long id, UsuarioInfo user)
        {
            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 33);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/

            ViewBag.Usuario = user;

            ViewBag.TipoRecursos = 6;
            return CostoRecurso(id, TipoRecurso.Depreciacion, user);
        }
        public ActionResult VerObsCostoDepreciacion(long id, long entidadid, UsuarioInfo user)
        {
            ViewBag.Usuario = user;
            ViewBag.TipoRecursos = 6;
            return VerObsCostoRecurso(id, TipoRecurso.Depreciacion, entidadid, user);
        }
        public ActionResult CostoFijo(long id, UsuarioInfo user)
        {
            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 34);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/

            ViewBag.Usuario = user;

            ViewBag.TipoRecursos = 7;
            return CostoRecurso(id, TipoRecurso.Fijos, user);
        }
        public ActionResult VerObsCostoFijo(long id, long entidadid, UsuarioInfo user)
        {
            ViewBag.Usuario = user;
            ViewBag.TipoRecursos = 7;
            return VerObsCostoRecurso(id, TipoRecurso.Fijos, entidadid, user);
        }

        public ActionResult CostoRecurso(long id, TipoRecurso tipo, UsuarioInfo user)
        {

            ViewBag.Usuario = user;
            Expediente exp = _expedienteService.GetOne(id);
            ViewBag.Expediente = exp;
            string viewName = "";

            if (tipo == TipoRecurso.MaterialFungible || tipo == TipoRecurso.ServicioIdentificable)
                viewName = "CostoRecIdent";
            else
            {
                viewName = "CostoRecNoIdent";

                var listaInductor = _inductorService.GetAll(exp.EntidadId)
                                        .Select(x => new SelectListItem()
                                        {
                                            Text = x.Nombre,
                                            Value = x.InductorId.ToString()
                                        }).ToList();
                listaInductor.Add(new SelectListItem() { Text = "- SELECCIONAR -", Value = "0" });
                listaInductor = listaInductor.OrderBy(x => int.Parse(x.Value)).ToList();

                ViewBag.ListaInductor = listaInductor;

                ViewBag.Depreciacion = tipo == TipoRecurso.Depreciacion;
            }

            List<RecursoCosto> model = _recursoCostoService.GetRecursoCosto(id, tipo);
            foreach (RecursoCosto item in model) item.ExpedienteId = id;

            switch (tipo)
            {
                case TipoRecurso.MaterialFungible: ViewBag.TipoRecurso = "Costos de Material Fungible"; break;
                case TipoRecurso.ServicioIdentificable: ViewBag.TipoRecurso = "Costos de Servicios Identificables"; break;
                case TipoRecurso.MaterialNoFungible: ViewBag.TipoRecurso = "Costos de Material No Fungible"; break;
                case TipoRecurso.ServicioTerceros: ViewBag.TipoRecurso = "Costos de Servicios de Terceros"; break;
                case TipoRecurso.Depreciacion: ViewBag.TipoRecurso = "Costos de Depreciacion y Amortizacion"; break;
                case TipoRecurso.Fijos: ViewBag.TipoRecurso = "Costos Fijos"; break;
            }
            Observacion obsmultiple = new Observacion();
            obsmultiple.ExpedienteId = exp.ExpedienteId;
            obsmultiple.Estado = "1";
            obsmultiple.EntidadId = user.EntidadId;
            string nombremul = ViewBag.TipoRecurso;
            obsmultiple.Pantalla = nombremul.Replace(" ", "");

            ViewBag.nomoficial = obsmultiple.Pantalla;

            ViewBag.ObservacionCostoPersonal = _observacionService.GetOneGlobalObservacion(obsmultiple);

            if (exp.SustCostosTerminado) viewName = "Ver" + viewName;
            else
            {
                switch (user.Rol)
                {
                    case (short)Rol.Administrador: viewName = exp.Entidad.TipoTupa == TipoTupa.Estandar ? viewName : "Ver" + viewName; break;
                    case (short)Rol.Evaluador: viewName = "Ver" + viewName; break;
                    case (short)Rol.Ratificador: viewName = "Ver" + viewName; break;
                    case (short)Rol.EntidadFiscalizadora: viewName = "Ver" + viewName; break;
                        //case Rol.Coordinador: viewName = "CostoPersonal"; break;
                        //case Rol.Registrador: viewName = "CostoPersonal"; break; //modificar
                }
            }

            return View(viewName, model);
        }

        public ActionResult VerObsCostoRecurso(long id, TipoRecurso tipo, long entidadid, UsuarioInfo user)
        {

            ViewBag.Usuario = user;
            Expediente exp = _expedienteService.GetOne(id);
            ViewBag.Expediente = exp;
            ViewBag.entidadver = entidadid;
            string viewName = "";

            if (tipo == TipoRecurso.MaterialFungible || tipo == TipoRecurso.ServicioIdentificable)
                viewName = "VerObsCostoRecIdent";
            else
            {
                viewName = "VerObsCostoRecNoIdent";

                var listaInductor = _inductorService.GetAll(exp.EntidadId)
                                        .Select(x => new SelectListItem()
                                        {
                                            Text = x.Nombre,
                                            Value = x.InductorId.ToString()
                                        }).ToList();
                listaInductor.Add(new SelectListItem() { Text = "- SELECCIONAR -", Value = "0" });
                listaInductor = listaInductor.OrderBy(x => int.Parse(x.Value)).ToList();

                ViewBag.ListaInductor = listaInductor;

                ViewBag.Depreciacion = tipo == TipoRecurso.Depreciacion;
            }

            List<RecursoCosto> model = _recursoCostoService.GetRecursoCosto(id, tipo);
            foreach (RecursoCosto item in model) item.ExpedienteId = id;

            switch (tipo)
            {
                case TipoRecurso.MaterialFungible: ViewBag.TipoRecurso = "Costos de Material Fungible"; break;
                case TipoRecurso.ServicioIdentificable: ViewBag.TipoRecurso = "Costos de Servicios Identificables"; break;
                case TipoRecurso.MaterialNoFungible: ViewBag.TipoRecurso = "Costos de Material No Fungible"; break;
                case TipoRecurso.ServicioTerceros: ViewBag.TipoRecurso = "Costos de Servicios de Terceros"; break;
                case TipoRecurso.Depreciacion: ViewBag.TipoRecurso = "Costos de Depreciacion y Amortizacion"; break;
                case TipoRecurso.Fijos: ViewBag.TipoRecurso = "Costos Fijos"; break;
            }
            Observacion obsmultiple = new Observacion();
            obsmultiple.ExpedienteId = exp.ExpedienteId;
            obsmultiple.Estado = "0";
            obsmultiple.CodValidacion = "5";
            obsmultiple.EntidadId = entidadid;
            string nombremul = ViewBag.TipoRecurso;
            obsmultiple.Pantalla = nombremul.Replace(" ", "");

            ViewBag.nomoficial = obsmultiple.Pantalla;

            ViewBag.ObservacionCostoPersonal = _observacionService.GetOneVerObservacion(obsmultiple);



            return View(viewName, model);
        }
        [HttpPost]
        public ActionResult GuardarRecursoCosto(List<RecursoCosto> model, UsuarioInfo user)
        {
            try
            {
                foreach (RecursoCosto item in model)
                {
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
                objauditoria.Actividad = "Agregar Recurso Costo";
                objauditoria.Pantalla = "Recurso Costo";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                _recursoCostoService.Guardar(model);

                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/

                _AuditoriaService.ResetearFactor(model[0].ExpedienteId);

                return Json(new
                {
                    valid = true,
                    mensaje = "Datos guardados correctamente."
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

        public ActionResult ConfigInductor(long id, UsuarioInfo user)
        {
            try
            {
                /*Inicio Cargar roles - Acceso*/
                List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 35);
                user.rolmenu = listarolmenu.ToList();
                /*Fin Cargar roles  - Acceso*/


                Expediente exp = _expedienteService.GetOne(id);
                ViewBag.Expediente = exp;

                ViewBag.TipoRecursos = 8;


                ViewBag.Usuario = user;


                Observacion obscostopersonal = new Observacion();
                obscostopersonal.ExpedienteId = exp.ExpedienteId;
                obscostopersonal.Estado = "1";
                obscostopersonal.EntidadId = user.EntidadId;
                obscostopersonal.Pantalla = "Configuracion del Valor de Inductor";
                ViewBag.ObservacionCostoPersonal = _observacionService.GetOneGlobalObservacion(obscostopersonal);


                List<Inductor> listaInductor = _inductorService.GetByExpediente(id);

                for (int i = 0; i < listaInductor.Count; i++)
                {
                    if (listaInductor[i] == null)
                    {
                        listaInductor.RemoveAt(i);
                    }
                }


                ViewBag.ListaInductor = listaInductor ?? new List<Inductor>();

                string viewName = string.Empty;
                if (exp.SustCostosTerminado) viewName = "VerConfigInductor";
                else
                {
                    switch (user.Rol)
                    {
                        case (short)Rol.Administrador: viewName = exp.Entidad.TipoTupa == TipoTupa.Estandar ? "ConfigInductor" : "VerConfigInductor"; break;
                        case (short)Rol.Coordinador: viewName = "ConfigInductor"; break;
                        case (short)Rol.Evaluador: viewName = "VerConfigInductor"; break;
                        case (short)Rol.EntidadFiscalizadora: viewName = "VerConfigInductor"; break;
                        case (short)Rol.Ratificador: viewName = "VerConfigInductor"; break;
                        case (short)Rol.RegistradorProcesos: viewName = "ConfigInductor"; break; //modificar
                    }
                }

                return View(viewName);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                ModelState.AddModelError("", ex.Message);
                return View("_Error");
            }
        }

        public ActionResult VerObsConfigInductor(long id, long entidadid, UsuarioInfo user)
        {
            try
            {
                Expediente exp = _expedienteService.GetOne(id);
                ViewBag.Expediente = exp;
                ViewBag.entidadver = entidadid;
                ViewBag.TipoRecursos = 8;
                ViewBag.Usuario = user;


                Observacion obscostopersonal = new Observacion();
                obscostopersonal.ExpedienteId = exp.ExpedienteId;
                obscostopersonal.Estado = "0";
                obscostopersonal.CodValidacion = "5";
                obscostopersonal.EntidadId = entidadid;
                obscostopersonal.Pantalla = "Configuracion del Valor de Inductor";
                ViewBag.ObservacionCostoPersonal = _observacionService.GetOneVerObservacion(obscostopersonal);


                List<Inductor> listaInductor = _inductorService.GetByExpediente(id);

                for (int i = 0; i < listaInductor.Count; i++)
                {
                    if (listaInductor[i] == null)
                    {
                        listaInductor.RemoveAt(i);
                    }
                }


                ViewBag.ListaInductor = listaInductor ?? new List<Inductor>();

                string viewName = string.Empty;
                viewName = "VerObsConfigInductor";


                return View(viewName);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                ModelState.AddModelError("", ex.Message);
                return View("_Error");
            }
        }
        public string InductorValorGetAllLikePagin(InductorValor filtro, UsuarioInfo user)
        {
            try
            {
                Expediente exp = _expedienteService.GetOne(filtro.ExpedienteId);

                List<InductorValor> lista = _inductorService.GetValoresByInductor(exp.EntidadId, filtro.ExpedienteId, filtro.InductorId);

                return JsonConvert.SerializeObject(new
                {
                    lista = lista.Select(x => new
                    {
                        ExpedienteId = filtro.ExpedienteId,
                        InductorValorId = x.InductorValorId,
                        InductorId = x.InductorId,
                        UnidadOrganicaId = x.UnidadOrganicaId,
                        Nombre = x.UnidadOrganica.Nombre,
                        Valor = x.Valor
                    })
                });
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _log.Error(ex);
                return ex.Message;
            }
        }
        [HttpPost]
        public ActionResult GuardarInductorValor(List<InductorValor> model, UsuarioInfo user)
        {
            try
            {
                foreach (InductorValor item in model)
                {
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
                objauditoria.Actividad = "Agregar Inductor Valor";
                objauditoria.Pantalla = "Inductor Valor";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                _inductorService.GuardarInductorValor(model);


                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/

                _AuditoriaService.ResetearFactor(model[0].ExpedienteId);
                return Json(new
                {
                    valid = true,
                    mensaje = "Datos guardados correctamente."
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

        public ActionResult FactorTupa(long id, UsuarioInfo user)
        {
            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 36);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/


            Expediente exp = _expedienteService.GetOne(id);
            ViewBag.Expediente = exp;

            ViewBag.Usuario = user;

            ViewBag.TipoRecursos = 9;
            Observacion obscostopersonal = new Observacion();
            obscostopersonal.ExpedienteId = exp.ExpedienteId;
            obscostopersonal.Estado = "1";
            obscostopersonal.EntidadId = user.EntidadId;
            obscostopersonal.Pantalla = "Configuracion del Inductor de Asignacion de Costos";
            ViewBag.ObservacionCostoPersonal = _observacionService.GetOneGlobalObservacion(obscostopersonal);

            ViewBag.ListaUnidadOrganica = _unidadOrganicaService.GetByExpediente(id);

            string viewName = string.Empty;
            if (exp.SustCostosTerminado) viewName = "VerFactorTupa";
            else
            {
                switch (user.Rol)
                {
                    case (short)Rol.Administrador: viewName = exp.Entidad.TipoTupa == TipoTupa.Estandar ? "FactorTupa" : "VerFactorTupa"; break;
                    case (short)Rol.Coordinador: viewName = "FactorTupa"; break;
                    case (short)Rol.EntidadFiscalizadora: viewName = "VerFactorTupa"; break;
                    case (short)Rol.Evaluador: viewName = "VerFactorTupa"; break;
                    case (short)Rol.Ratificador: viewName = "VerFactorTupa"; break;
                    case (short)Rol.RegistradorProcesos: viewName = "FactorTupa"; break; //modificar
                }
            }

            return View(viewName);
        }

        public ActionResult VerObsFactorTupa(long id, long entidadid, UsuarioInfo user)
        {

            Expediente exp = _expedienteService.GetOne(id);
            ViewBag.Expediente = exp;
            ViewBag.entidadver = entidadid;
            ViewBag.Usuario = user;

            ViewBag.TipoRecursos = 9;
            Observacion obscostopersonal = new Observacion();
            obscostopersonal.ExpedienteId = exp.ExpedienteId;
            obscostopersonal.Estado = "0";
            obscostopersonal.CodValidacion = "5";
            obscostopersonal.EntidadId = entidadid;
            obscostopersonal.Pantalla = "Configuracion del Inductor de Asignacion de Costos";
            ViewBag.ObservacionCostoPersonal = _observacionService.GetOneVerObservacion(obscostopersonal);

            ViewBag.ListaUnidadOrganica = _unidadOrganicaService.GetByExpediente(id);

            string viewName = string.Empty;
            viewName = "VerObsFactorTupa";

            return View(viewName);
        }

        public string FactorTupaGetAllLikePagin(FactorDedicacion filtro)
        {
            try
            {
                string[] tipos = { "",
                    "PERSONAL DIRECTO",
                    "MATERIAL FUNGIBLE",
                    "SERVICIO IDENTIFICABLE",
                    "MATERIAL NO FUNGIBLE",
                    "SERVICIO DE TERCERO",
                    "DEPRECIACIÓN",
                    "COSTO FIJO"
                };

                List<FactorDedicacion> lista = _factorDedicacionService.GetValoresByUndOrganica(filtro.ExpedienteId, filtro.UnidadOrganicaId);

                return JsonConvert.SerializeObject(new
                {
                    lista = lista.Select(x => new
                    {
                        FactorDedicacionId = x.FactorDedicacionId,
                        ExpedienteId = filtro.ExpedienteId,
                        RecursoId = x.RecursoId,
                        UnidadOrganicaId = x.UnidadOrganicaId,
                        Nombre = x.Recurso.Nombre,
                        TipoId = x.Recurso.TipoRecurso,
                        Tipo = tipos[(int)x.Recurso.TipoRecurso],
                        ValorTupa = x.ValorTupa,
                        ValorNoTupa = x.ValorNoTupa,
                        AutoCalculo = x.AutoCalculo
                    })
                });
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _log.Error(ex);
                return ex.Message;
            }
        }
        public string FactorTupaGetAutoCalculo(FactorDedicacion filtro, UsuarioInfo user)
        {
            try
            {
                string[] tipos = { "",
                    "PERSONAL DIRECTO",
                    "MATERIAL FUNGIBLE",
                    "SERVICIO IDENTIFICABLE",
                    "MATERIAL NO FUNGIBLE",
                    "SERVICIO DE TERCERO",
                    "DEPRECIACIÓN",
                    "COSTO FIJO"
                };

                List<string> validaciones = new List<string>();
                string mensaje = "";

                List<FactorDedicacion> lista = _factorDedicacionService.GetAutoCalculoByUndOrganica(user.EntidadId, user.ActivarAlgoritmo, filtro.ExpedienteId, filtro.UnidadOrganicaId);



                foreach (FactorDedicacion item in lista)
                {
                    item.FecCreacion = DateTime.Now;
                    item.UserCreacion = user.NroDocumento;
                    item.FecModificacion = DateTime.Now;
                    item.UserModificacion = user.NroDocumento;
                    if (item.ValorTupa > 100)
                    {
                        validaciones.Add(item.UserModificacion);
                    }
                }
                if (validaciones.Count() > 0)
                {
                    mensaje = "En uno o más recursos el cálculo del factor TUPA es mayor al 100%";

                }
                else
                {
                    /*auditoria agregar*/
                    objauditoria.EntidadId = user.EntidadId;
                    objauditoria.SectorId = user.Sector;
                    objauditoria.ProvinciaId = user.Provincia;
                    objauditoria.Usuario = user.NombreCompleto;
                    objauditoria.Actividad = "Agregar Factor Tupa";
                    objauditoria.Pantalla = "Factor Tupa";
                    objauditoria.UserCreacion = user.NroDocumento;
                    objauditoria.FecCreacion = DateTime.Now;
                    /*auditoria*/

                    _factorDedicacionService.Guardar(lista);

                    /*auditoria Grabar*/
                    _AuditoriaService.Save(objauditoria);
                }


                return JsonConvert.SerializeObject(new
                {
                    lista = lista.Select(x => new
                    {
                        FactorDedicacionId = x.FactorDedicacionId,
                        ExpedienteId = filtro.ExpedienteId,
                        RecursoId = x.RecursoId,
                        UnidadOrganicaId = x.UnidadOrganicaId,
                        Nombre = x.Recurso.Nombre,
                        TipoId = x.Recurso.TipoRecurso,
                        Tipo = tipos[(int)x.Recurso.TipoRecurso],
                        ValorTupa = x.ValorTupa,
                        ValorNoTupa = x.ValorNoTupa,
                        AutoCalculo = x.AutoCalculo,
                        mensaje = mensaje
                    }),
                    mensaje = mensaje
                });
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _log.Error(ex);
                return ex.Message;
            }
        }
        [HttpPost]
        public ActionResult GuardarFactorTupa(List<FactorDedicacion> model, UsuarioInfo user)
        {
            try
            {



                //List<string> validaciones = _factorDedicacionService.Validarfactor(model[0].ExpedienteId, model[0].UnidadOrganicaId);

                //if (validaciones.Count() > 0)
                //{
                //    return Json(new
                //    {
                //        valid = false,
                //        mensaje = validaciones
                //    });
                //}




                foreach (FactorDedicacion item in model)
                {
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
                objauditoria.Actividad = "Agregar Factor Tupa";
                objauditoria.Pantalla = "Factor Tupa";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/

                _factorDedicacionService.Guardar(model);

                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
                return Json(new
                {
                    valid = true,
                    mensaje = "Datos guardados correctamente."
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
        public ActionResult ProcesarCostos(long id, UsuarioInfo user)
        {
            try
            {

                List<string> validaciones = _expedienteService.ValidarCostos(id, user.procesogratuito);



                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Procesar Costos";
                objauditoria.Pantalla = "Procesar Costos";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/

                if (validaciones.Count() > 0)
                {
                    return Json(new
                    {
                        valid = false,
                        mensaje = validaciones
                    });
                }
                else
                {

                    ViewBag.ListaUnidadOrganica = _unidadOrganicaService.GetByExpediente(id);

                    foreach (UnidadOrganica item in ViewBag.ListaUnidadOrganica)
                    {
                        List<FactorDedicacion> lista = _factorDedicacionService.GetAutoCalculoByUndOrganicacantidad(user.EntidadId, id, item.UnidadOrganicaId);

                        if (lista.Count() == 0)
                        {
                            return Json(new
                            {
                                valid = false,
                                mensaje4 = "Generar Inductor de Asignación de Costos a la Unidad Orgánica - " + item.Nombre
                            });
                        }

                    }

                    _expedienteService.EliminarProcesarCostos(id);
                    _expedienteService.ProcesarCostos(id);

                    Expediente objexp = _expedienteService.GetOne(id);
                    objexp.ProcesarCosto = 1;
                    _expedienteService.Save(objexp);
                    /*auditoria Grabar*/
                    _AuditoriaService.Save(objauditoria);
                    /*auditoria Grabar*/
                    return Json(new
                    {
                        valid = true,
                        mensaje = "Cálculo realizado correctamente."
                    });

                }


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
        public ActionResult TerminarSustento(long id, UsuarioInfo user)
        {
            try
            {
                List<string> validaciones = _expedienteService.ValidarSubvencion(id, user.Sector);
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Terminar Sustento";
                objauditoria.Pantalla = "Sustento de Costo";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/

                if (validaciones.Count() > 0)
                {
                    return Json(new
                    {
                        valid = false,
                        mensaje = validaciones
                    });
                }
                else
                {


                    ViewBag.ListaUnidadOrganica = _unidadOrganicaService.GetByExpediente(id);

                    foreach (UnidadOrganica item in ViewBag.ListaUnidadOrganica)
                    {
                        List<FactorDedicacion> lista = _factorDedicacionService.GetAutoCalculoByUndOrganicacantidad(user.EntidadId, id, item.UnidadOrganicaId);

                        if (lista.Count() == 0)
                        {
                            return Json(new
                            {
                                valid = false,
                                mensaje4 = "Generar Inductor de Asignación de Costos a la Unidad Orgánica - " + item.Nombre
                            });
                        }

                    }


                    _expedienteService.CambiarEstadoSustento(id, true);
                    /*auditoria Grabar*/
                    _AuditoriaService.Save(objauditoria);
                    /*auditoria Grabar*/
                    return Json(new
                    {
                        valid = true
                    });
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.Clear();
                ModelState.AddModelError("", ex.Message);
                _log.Error(ex);

                return Json(new
                {
                    ex.Message
                });

                //return PartialView("_Error");
            }
        }


        [HttpPost]
        public ActionResult Activar(long id, UsuarioInfo user)
        {
            try
            {    /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Activar Sustento";
                objauditoria.Pantalla = "Activar Sustento";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                _expedienteService.CambiarEstadoSustento(id, false);

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

        [HttpPost]
        public ActionResult GuardarSubvencion(List<TablaAsme> model, UsuarioInfo user)
        {
            try
            {

                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Agregar Subvencion";
                objauditoria.Pantalla = "Subvencion";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/



                //if (model != null)
                //{
                //    List<TablaAsme> objTablaAsme = new List<TablaAsme>();

                //    foreach (var mode in model)
                //    {

                //        _tablaAsmeService.Save(objAtributo);
                //    }



                //}

                _tablaAsmeService.GuardarSubvencion(model);

                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
                return Json(new
                {
                    valid = true,
                    mensaje = ""
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
        public ActionResult EnviarAutorizacion(TablaAsme model)
        {
            try
            {
                TablaAsme obj;
                UIT objuit = _UITService.LsitaGetOne();
                obj = _tablaAsmeService.GetOne(model.TablaAsmeId);

                obj.AutorizacionMEF = Autorizacionmef.Enviar;
                obj.UITID = objuit.UITID;
                obj.FecEnvioMef = DateTime.Now;
                obj.DescripcionSusustento = model.DescripcionSusustento;

                obj.DescripcionRespuesa = model.DescripcionRespuesa;


                obj.ArMotivoAdjuntoId = model.ArMotivoAdjuntoId;

                _tablaAsmeService.Save(obj);

                return Json(new
                {
                    valid = true,
                    mensaje = ""
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