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
    public class SolicitudReporteController : Controller
    {
        private readonly ILogService<SolicitudReporteController> _log;
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



        Auditoria objauditoria = new Auditoria();
        public SolicitudReporteController(IExpedienteService expedienteService,
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
                                    IInformeDocumentosService informeDocumentosService)
        {
            _log = new LogService<SolicitudReporteController>();
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

            UsuarioInfo model = new UsuarioInfo();
            model = user;





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

            ViewBag.publicarCIU = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "TODOS", Value = "0" },
                    new SelectListItem() { Text = "SI", Value = "1" },
                    new SelectListItem() { Text = "NO", Value = "2" },
                };

            ViewBag.publicarEstandar = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Sin Acción", Value = "0" },
                    new SelectListItem() { Text = "Publicar", Value = "1" },
                    new SelectListItem() { Text = "Presentar", Value = "2" },
                };

            ViewBag.publicarEstadoTipo = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = " - TODOS - ", Value = "0" },
                    new SelectListItem() { Text = "EXPEDIENTE REGULAR", Value = "1" },
                    new SelectListItem() { Text = "CARGA INICIAL", Value = "2" },
                };



            return View(model);
        }


        /// <summary>
        /// nueva informacion para cargar archivos
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="user"></param>
        /// <returns></returns>

        public string GetAllLikePaginSOLICITUDWORD(Expediente filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;
            //filtro.EntidadId = user.EntidadId;
            int rol = 0;

            List<Expediente> lista = _expedienteService.GetAllLikePaginSOLICITUDWORD(filtro, pageIndex, pageSize, ref totalRows);
            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    ExpedienteId = x.ExpedienteId,
                    Codigo = x.Codigo,
                    FechaCreacion = x.FecCreacion.Value.ToShortDateString(),
                    FecModificacion = x.FecModificacion.Value.ToShortDateString(),
                    TipoExpediente = (short)x.TipoExpediente,
                    EstadoExpediente = (short)x.EstadoExpediente,
                    NomEntidad = x.Entidad.Nombre,

                }),
                rol = 1,
                totalRows = totalRows
            });



        }


        [HttpPost]
        public void Upload()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                var fileName = System.IO.Path.GetFileName(file.FileName);
                var path = System.IO.Path.Combine("C:\\inetpub\\wwwroot\\SUT\\Documentos\\", fileName);

                file.SaveAs(path);
            }
        }





        public ActionResult Editar(long id, UsuarioInfo user)
        {

            try
            {
                Expediente item;

                item = _expedienteService.GetOne(id);
                if (item.ArchivoAdjuntoId == 0) item.ArchivoAdjuntoId = 0;
                ViewBag.Title = "Asignar Word";

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
        public ActionResult Guardar(Expediente model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.ExpedienteId == 0 ? "La Norma fue registrada satisfactoriamente."
                                                : "La solicitud fue modificada satisfactoriamente.";

                    Expediente obj;

                    obj = _expedienteService.GetOne(model.ExpedienteId);



                    obj.ArchivoAdjuntoId = model.ArchivoAdjuntoId;
                    obj.EstadoReporteWord = 2;
                    //obj.ArchivoAdjunto = null;
                    if (model.ArchivoAdjuntoId == 0) obj.ArchivoAdjuntoId = 0;


                    _expedienteService.Save(obj);

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
                ModelState.AddModelError("editararchivo", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }

    }
}