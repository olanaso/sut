using Sut.Entities;
using Sut.IApplicationServices;
using Sut.Log;
using Sut.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sut.Web.Areas.Simplificacion.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ILogService<DashboardController> _log;
        private readonly IExpedienteService _expedienteService;
        private readonly ITupaService _tupaService;
        private readonly IExpedienteNormaService _expedienteNormaService;
        private readonly IMetaDatoService _metaDatoService;
        private readonly IEnumeradoService _enumeradoService;
        private readonly IObservacionService _observacionService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly IArMotivoAdjuntoService _ArMotivoAdjuntoService;
        private readonly IRequisitoService _RequisitoService;
        private readonly IRolMenuService _rolMenuService;
        private readonly ICorreosService _correosService;
        private readonly IInformeService _InformeService;
        private readonly IInformeDocumentosService _IInformeDocumentosService;

        private readonly INotificacionExpedientesService _NotificacionExpedientesService;

        private readonly IProcedimientoService _procedimientoService;
        private readonly IProcedimientoCategoriaService _ProcedimientoCategoriaService;
        private readonly INotificacionService _notificacionService;
        private readonly IEntidadService _entidadService;

        private readonly IUsuarioService _usuarioService;
        private readonly IReporteService reporteService;
        private readonly IDepartamentoService _departamentoService;

        private readonly IDashboardService _dashboardService;

        Dashboard objdashboard = new Dashboard();

        public DashboardController(IExpedienteService expedienteService,
                                    ITupaService tupaService,
                                    IExpedienteNormaService expedienteNormaService,
                                    IMetaDatoService metaDatoService,
                                    IEnumeradoService enumeradoService, IAuditoriaService AuditoriaService,
                                    IObservacionService observacionService,
                                    IProcedimientoService procedimientoService,
                                    IArMotivoAdjuntoService arMotivoAdjuntoService,
                                    IRequisitoService requisitoService,
                                    IProcedimientoCategoriaService procedimientoCategoriaService,
                                    IRolMenuService rolMenuService,
                                    INotificacionExpedientesService notificacionExpedientesService,
                                    INotificacionService notificacionService,
                                    IEntidadService entidadService,
                                    IUsuarioService usuarioService,
                                    IReporteService _reporteService,
                                    ICorreosService correosService,
                                    IInformeService informeService,
                                    IInformeDocumentosService informeDocumentosService,
                                     IDepartamentoService departamentoService,
                                     IDashboardService dashboardService

            )
        {
            _log = new LogService<DashboardController>();
            _expedienteService = expedienteService;
            _tupaService = tupaService;
            _expedienteNormaService = expedienteNormaService;
            _metaDatoService = metaDatoService;
            _enumeradoService = enumeradoService;
            _observacionService = observacionService;
            _procedimientoService = procedimientoService;
            _AuditoriaService = AuditoriaService;
            _ArMotivoAdjuntoService = arMotivoAdjuntoService;
            _ProcedimientoCategoriaService = procedimientoCategoriaService;
            _RequisitoService = requisitoService;
            _rolMenuService = rolMenuService;
            _NotificacionExpedientesService = notificacionExpedientesService;
            _notificacionService = notificacionService;
            _entidadService = entidadService;
            _usuarioService = usuarioService;
            _reporteService = reporteService;
            _correosService = correosService;
            _InformeService = informeService;
            _IInformeDocumentosService = informeDocumentosService;
            _departamentoService = departamentoService;
            _dashboardService = dashboardService;
        }

        public ActionResult Lista(UsuarioInfo user)
        {



            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 20);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/


            /*Inicio Cargar roles - Acceso*/
            UsuarioInfo userCostos = new UsuarioInfo();
            List<RolMenu> listarolmenuCostos = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 27);
            userCostos.rolmenu = listarolmenuCostos.ToList();
            ViewBag.UsuarioCostos = userCostos;
            /*Fin Cargar roles  - Acceso*/


            /*Inicio Cargar roles - Acceso*/
            UsuarioInfo userrptTupaCompleto = new UsuarioInfo();
            List<RolMenu> listarolmenurptTupaCompleto = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 38);
            userrptTupaCompleto.rolmenu = listarolmenurptTupaCompleto.ToList();
            ViewBag.UsuariorptTupaCompleto = userrptTupaCompleto;
            /*Fin Cargar roles  - Acceso*/

            /*Inicio Cargar roles - Acceso*/
            UsuarioInfo userrptSTLCompleto = new UsuarioInfo();
            List<RolMenu> listarolmenurptSTLCompleto = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 39);
            userrptSTLCompleto.rolmenu = listarolmenurptSTLCompleto.ToList();
            ViewBag.UsuariorptSTLCompleto = userrptSTLCompleto;
            /*Fin Cargar roles  - Acceso*/


            /*Tipo de los expedientes*/

            ViewBag.publicarEstadoTipo = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = " - TODOS - ", Value = "0" },
                    new SelectListItem() { Text = "EXPEDIENTE REGULAR", Value = "1" },
                    new SelectListItem() { Text = "CARGA INICIAL", Value = "2" },
                };

            /*Estado de los expedientes*/

            ViewBag.publicarEstado = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = " - TODOS - ", Value = "0" },
                    new SelectListItem() { Text = "En Proceso", Value = "1" },
                    new SelectListItem() { Text = "Presentado", Value = "2" },
                    new SelectListItem() { Text = "Aprobado", Value = "3" },
                    new SelectListItem() { Text = "Observado", Value = "4" },
                    new SelectListItem() { Text = "Anulado", Value = "5" },
                    new SelectListItem() { Text = "Subsanado", Value = "6" },
                    new SelectListItem() { Text = "Publicado", Value = "7" },
                };

            /*Entidades*/


            List<Entidad> listaEntidades = _entidadService.GetAll();
            listaEntidades = listaEntidades.OrderBy(x => x.Acronimo).ToList();
            listaEntidades.Insert(0, new Entidad() { EntidadId = 0, Nombre = " - TODOS - " });
            ViewBag.ListaEntidades = listaEntidades.Select(x => new SelectListItem() { Value = x.EntidadId.ToString(), Text = x.Nombre }).ToList();

            List<Departamento> listaDepartamento = _departamentoService.GetAll();
            listaDepartamento = listaDepartamento.OrderBy(x => x.Nombre).ToList();
            listaDepartamento.Insert(0, new Departamento() { DepartamentoId = 0, Nombre = " - TODOS - " });
            ViewBag.ListaDepartamento = listaDepartamento.Select(x => new SelectListItem() { Value = x.DepartamentoId.ToString(), Text = x.Nombre }).ToList();


            List<Provincia> listaProvincia = new List<Provincia>();
            listaProvincia = listaProvincia.OrderBy(x => x.Nombre).ToList();
            listaProvincia.Insert(0, new Provincia() { ProvinciaId = 0, Nombre = " - TODOS - " });
            ViewBag.ListaProvincia = listaProvincia.Select(x => new SelectListItem() { Value = x.ProvinciaId.ToString(), Text = x.Nombre }).ToList();




            UsuarioInfo model = new UsuarioInfo();
            model = user;

            List<MetaDato> listaGobierno = _metaDatoService.GetByParent(43);
            listaGobierno.Insert(0, new MetaDato() { MetaDatoId = 0, Nombre = " - SELECCIONAR - " });
            ViewBag.ListaGobierno = listaGobierno.Select(x => new SelectListItem() { Value = x.MetaDatoId.ToString(), Text = x.Nombre }).ToList();

            List<MetaDato> listaSector = _metaDatoService.GetByParent(47);
            listaSector = listaSector.OrderBy(x => x.Nombre).ToList();
            listaSector.Insert(0, new MetaDato() { MetaDatoId = 0, Nombre = " - SELECCIONAR - " });
            ViewBag.ListaSector = listaSector.Select(x => new SelectListItem() { Value = x.MetaDatoId.ToString(), Text = x.Nombre }).ToList();


            List<Expediente> lstex = _expedienteService.GetByEntidadCompleto();

            ViewBag.totalexp = lstex.ToList().Count();
            ViewBag.cargainicial = lstex.Where(x => x.TipoExpediente == TipoExpediente.CargaInicial).Count();
            ViewBag.Expedienteregular = lstex.Where(x => x.TipoExpediente == TipoExpediente.ExpedienteRegular).Count();
            ViewBag.enproceso = lstex.Where(x => x.EstadoExpediente == EstadoExpediente.EnProceso).Count();
            ViewBag.presentado = lstex.Where(x => x.EstadoExpediente == EstadoExpediente.Presentado).Count();
            ViewBag.aprobado = lstex.Where(x => x.EstadoExpediente == EstadoExpediente.Aprobado).Count();
            ViewBag.observado = lstex.Where(x => x.EstadoExpediente == EstadoExpediente.Observado).Count();
            ViewBag.anulado = lstex.Where(x => x.EstadoExpediente == EstadoExpediente.Anulado).Count();
            ViewBag.subsanado = lstex.Where(x => x.EstadoExpediente == EstadoExpediente.Subsanado).Count();
            ViewBag.publicado = lstex.Where(x => x.EstadoExpediente == EstadoExpediente.Publicado).Count();


            ViewBag.publicarEstadoTipo = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = " - TODOS - ", Value = "0" },
                    new SelectListItem() { Text = "EXPEDIENTE REGULAR", Value = "1" },
                    new SelectListItem() { Text = "CARGA INICIAL", Value = "2" },
                };
            return View();
        }

        [HttpGet]
        public ActionResult ObtenerDatosJson(string iopsp)
        {
            // Construir el objeto que se va a devolver
            Dashboard odashboard = new Dashboard();
            odashboard.iOpSp = int.Parse(iopsp);
            Dashboard result = _dashboardService.getDashboard(odashboard);
            int longitud = result.result.Length;


            // Devolver el objeto en formato JSON utilizando la clase JsonResult
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult ObtenerDatosCaldarioJson(string iopsp)
        {
            // Construir el objeto que se va a devolver
            Dashboard odashboard = new Dashboard();
            odashboard.iOpSp = int.Parse(iopsp);
            Dashboard result = _dashboardService.getDashboardCalendario(odashboard);
            int longitud = result.result.Length;


            // Devolver el objeto en formato JSON utilizando la clase JsonResult
            return Json(result, JsonRequestBehavior.AllowGet);
        }






    }
}