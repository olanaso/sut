using Newtonsoft.Json;
using Sut.ApplicationServices;
using Sut.Entities;
using Sut.IApplicationServices;
using Sut.Log;
using Sut.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Threading;
using System.Web;
using System.Web.Security;

namespace Sut.Web.Areas.Simplificacion.Controllers
{
    [Authorize]
    public class ExpedienteController : Controller
    {
        private readonly ILogService<ExpedienteController> _log;
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
        private readonly IReporteService _reporteService;
        private readonly IMiembroEquipoService _miembroEquipoService;
        private readonly IDepartamentoService _departamentoService;
        private readonly IProvinciaService _provinciaService;


        Auditoria objauditoria = new Auditoria();
        public ExpedienteController(IExpedienteService expedienteService,
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
                                    IReporteService reporteService,
                                    ICorreosService correosService,
                                    IInformeService informeService,
                                    IInformeDocumentosService informeDocumentosService,
                                IDepartamentoService departamentoService,
                                    IMiembroEquipoService miembroEquipoService, IProvinciaService provinciaService 
                                   )
        {
            _log = new LogService<ExpedienteController>();
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
            _miembroEquipoService = miembroEquipoService;
            _departamentoService = departamentoService;
            _provinciaService = provinciaService;
        }



        public string GetAllLikePaginRptActividad(Expediente filtro, int pageIndex, int pageSize, long usuarioId, UsuarioInfo user)
        {
            int totalRows = 0;
            List<Expediente> lista = _expedienteService.GetAllLikePaginRptActividad(filtro, pageIndex, pageSize, ref totalRows);
            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    campo1 = x.campo1,
                    campo2 = x.campo2,
                    campo3 = x.campo3,
                    campo4 = x.campo4,
                    campo5 = x.campo5,
                    campo6 = x.campo6,
                    campo7 = x.campo7,
                    campo8 = x.campo8,
                    campo9 = x.campo9,
                    campo10 = x.campo10,
                    campo11 = x.campo11,
                    campo12 = x.campo12,
                }),
                totalRows = totalRows,
            });
        }
        /*JJJMSP2*/
        public ActionResult ListaComparar(UsuarioInfo user)
        {
            List<Expediente> listaexpedientesCompara = _expedienteService.GetByEntidad(user.EntidadId);
            listaexpedientesCompara = listaexpedientesCompara.OrderByDescending(x => x.Codigo).ToList();
            listaexpedientesCompara.Insert(0, new Expediente() { ExpedienteId = 0, Codigo = " Seleccionar Expediente " });
            ViewBag.listaexpedientesCompara = listaexpedientesCompara.Select((x, i) => new SelectListItem()
            {
                Value = x.ExpedienteId.ToString(),
                Text = i.ToString("00") + "  -  " + x.Codigo + "  -  " + GetTipoExpedienteText((int)x.TipoExpediente) + "  -  " + GetEstadoExpedienteText((int)x.EstadoExpediente)
            })
           .ToList();

            List<Entidad> listaentidades = _entidadService.GetAll();
            listaentidades = listaentidades.OrderByDescending(x => x.Codigo).ToList();
            listaentidades.Insert(0, new Entidad() { EntidadId = 0, Nombre = " Seleccionar Entidad - " });
            ViewBag.listaentidades = listaentidades.Select((x, i) => new SelectListItem()
            {
                Value = x.EntidadId.ToString(),
                Text = i.ToString("00") + "  -  " + x.Nombre
            })
           .ToList();

            UsuarioInfo model = new UsuarioInfo();
            model = user;
            return View(model);
        }

        private string GetTipoExpedienteText(int tipoexpediente)
        {
            switch (tipoexpediente)
            {
                case 1:
                    return "C. INICIAL ";
                case 2:
                    return "E. REGULAR";
                default:
                    return "Tipo Expediente";
            }
        }

        private string GetEstadoExpedienteText(int estadoexpediente)
        {
            switch (estadoexpediente)
            {
                case 1:
                    return "EN PROCESO";
                case 2:
                    return "PRESENTADO";
                case 3:
                    return "APROBADO";
                case 4:
                    return "OBSERVADO";
                case 5:
                    return "ANULADO";
                case 6:
                    return "SUBSANADO";
                case 7:
                    return "PUBLICADO";
                default:
                    return "Estado Expediente";
            }
        }

        public string GetProcedimientoCompararAll(CompararPA filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            List<CompararPA> lista = _AuditoriaService.getProcedimientoComparar(filtro.entidadid, filtro.procedimientoidb, filtro.procedimientoidc);
            //return Json(lista, JsonRequestBehavior.AllowGet);
            int totalRows = 0;
            totalRows = lista.Count;
            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    entidadid = x.entidadid,
                    procedimientoidb = x.procedimientoidb,
                    Camp1_Base = x.Camp1_Base,
                    Camp2_Base = x.Camp2_Base,
                    Camp3_Base = x.Camp3_Base,
                    Camp4_Base = x.Camp4_Base,
                    Camp5_Base = x.Camp5_Base,
                    Camp6_Base = x.Camp6_Base,
                    Camp7_Base = x.Camp7_Base,
                    c1 = x.c1,
                    c2 = x.c2,
                    c3 = x.c3,
                    c4 = x.c4,
                    c5 = x.c5,
                    c6 = x.c6,
                    c7 = x.c7,
                    procedimientoidc = x.procedimientoidc,
                    Campo1_Comp = x.Campo1_Comp,
                    Campo2_Comp = x.Campo2_Comp,
                    Campo3_Comp = x.Campo3_Comp,
                    Campo4_Comp = x.Campo4_Comp,
                    Campo5_Comp = x.Campo5_Comp,
                    Campo6_Comp = x.Campo6_Comp,
                    Campo7_Comp = x.Campo7_Comp,
                }),
                totalRows = totalRows,
            });
        }
        public ActionResult GetExpedientesEntidadGrid(long id)
        {
            try
            {
                List<Expediente> listaexpedientesCompara = _expedienteService.GetByEntidad(id);
                listaexpedientesCompara = listaexpedientesCompara.OrderByDescending(x => x.Codigo).ToList();
                listaexpedientesCompara.Insert(0, new Expediente() { ExpedienteId = 0, Codigo = " Seleccionar Expediente " });
                return Json(new { data = listaexpedientesCompara.Select((x, i) => new { ExpedienteId = x.ExpedienteId, Codigo = i.ToString("00") + "  -  " + x.Codigo + "  -  " + GetTipoExpedienteText((int)x.TipoExpediente) + "  -  " + GetEstadoExpedienteText((int)x.EstadoExpediente) }).ToList() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                _log.Error(ex);
                return PartialView("_Error");
            }
        }
        /*JJJMSP2 INI*/
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

            List<Departamento> listaDepartamento = _departamentoService.GetAll();
            listaDepartamento = listaDepartamento.OrderBy(x => x.Nombre).ToList();
            listaDepartamento.Insert(0, new Departamento() { DepartamentoId = 0, Nombre = " - TODOS - " });
            ViewBag.ListaDepartamento = listaDepartamento.Select(x => new SelectListItem()
            {
                Value = x.DepartamentoId.ToString(),
                Text = x.Nombre
            })
           .ToList();

            List<Provincia> listaProvincia = new List<Provincia>();
            listaProvincia = listaProvincia.OrderBy(x => x.Nombre).ToList();
            listaProvincia.Insert(0, new Provincia() { ProvinciaId = 0, Nombre = " - TODOS - " });
            ViewBag.ListaProvincia = listaProvincia.Select(x => new SelectListItem()
            {
                Value = x.ProvinciaId.ToString(),
                Text = x.Nombre
            })
           .ToList();

            return View(model);
        }
        public ActionResult ListaConfigurar(UsuarioInfo user)
        {
            UsuarioInfo model = new UsuarioInfo();
            model = user;
            return View(model);
        }
        
        public ActionResult ListadetalleReporte(long id,  UsuarioInfo user)
        {
            try
            {
                ViewBag.Usuario = user;

                ViewBag.UsuarioID = id;

                Expediente model = _expedienteService.GetOne(id);

                ViewBag.NOMBRE = model.Entidad.Nombre;

                string viewName = string.Empty;

                List<MetaDato> listaGobierno = _metaDatoService.GetByParent(43);
                listaGobierno = listaGobierno.OrderBy(x => x.Nombre).ToList();
                listaGobierno.Insert(0, new MetaDato() { MetaDatoId = 0, Nombre = " - TODOS - " });

                ViewBag.ListaGobierno = listaGobierno.Select(x => new SelectListItem()
                {
                    Value = x.MetaDatoId.ToString(),
                    Text = x.Nombre
                })
               .ToList();


                List<MetaDato> listaSector = _metaDatoService.GetByParent(47);
                listaSector = listaSector.OrderBy(x => x.Nombre).ToList();
                listaSector.Insert(0, new MetaDato() { MetaDatoId = 0, Nombre = " - TODOS - " });

                ViewBag.ListaSector = listaSector.Select(x => new SelectListItem()
                {
                    Value = x.MetaDatoId.ToString(),
                    Text = x.Nombre
                })
               .ToList();


                List<Departamento> listaDepartamento = _departamentoService.GetAll();
                listaDepartamento = listaDepartamento.OrderBy(x => x.Nombre).ToList();
                listaDepartamento.Insert(0, new Departamento() { DepartamentoId = 0, Nombre = " - TODOS - " });

                ViewBag.ListaDepartamento = listaDepartamento.Select(x => new SelectListItem()
                {
                    Value = x.DepartamentoId.ToString(),
                    Text = x.Nombre
                })
               .ToList();

                ViewBag.publicarTipo = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = " - Seleccionar - ", Value = "0" },
                    new SelectListItem() { Text = "Reporte De Duración De Actividades", Value = "1" },
                    new SelectListItem() { Text = "Reporte De Recursos Asignados A Actividades", Value = "2" }, 
                };

                List<Provincia> listaProvincia = new List<Provincia>();
                listaProvincia = listaProvincia.OrderBy(x => x.Nombre).ToList();
                listaProvincia.Insert(0, new Provincia() { ProvinciaId = 0, Nombre = " - TODOS - " });

                ViewBag.ListaProvincia = listaProvincia.Select(x => new SelectListItem()
                {
                    Value = x.ProvinciaId.ToString(),
                    Text = x.Nombre
                })
               .ToList();

                viewName = "_ListaReporte";

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

        public ActionResult ListaConfigurarProcedimientos(UsuarioInfo user)
        {

            UsuarioInfo model = new UsuarioInfo();
            model = user;
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
        public string GetAllLikePaginArchivos(ArMotivoAdjunto filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;


            List<ArMotivoAdjunto> lista = _ArMotivoAdjuntoService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);



            System.Web.HttpContext.Current.Session["ArMotivoAdjunto"] = lista;

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    ArMotivoAdjuntoId = x.ArMotivoAdjuntoId,
                    NombreArchivo = x.NombreArchivo,
                    UserCreacion = x.UserCreacion,
                    NroDocumento = user.NroDocumento
                }),
                totalRows = totalRows
            });
        }

        [HttpPost]
        public ActionResult Eliminar(int ArMotivoAdjuntoId, UsuarioInfo user)
        {

            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Eliminar ArMotivoAdjuntoId";
                objauditoria.Pantalla = "ArMotivoAdjunto";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                _ArMotivoAdjuntoService.Eliminar(ArMotivoAdjuntoId);
                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
                return Json(new
                {
                    mensaje = "El Inductor fue eliminada satisfactoriamente."
                });

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
        public ActionResult EliminarMasivo(int ExpedienteId, UsuarioInfo user)
        {

            try
            {
                List<ArMotivoAdjunto> lista = _ArMotivoAdjuntoService.GetAllLikePaginEliminar(ExpedienteId);


                foreach (ArMotivoAdjunto l in lista)
                {
                    _ArMotivoAdjuntoService.Eliminar(l.ArMotivoAdjuntoId);
                }


                return Json(new
                {
                    mensaje = "El Inductor fue eliminada satisfactoriamente."
                });

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }

        }


        public string GetAllLikePagin(Expediente filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;
            filtro.EntidadId = user.EntidadId;
            int rol = 0;


            if (user.Rol == 1 && filtro.EntidadId == 4105)
            {
                List<Expediente> lista = _expedienteService.GetAllLikePaginTodo(filtro, pageIndex, pageSize, ref totalRows);
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
                        EstadoRactificador = x.EstadoRactificador,
                        EstadoEvaluadorMinisterio = x.EstadoEvaluadorMinisterio,
                        EstadoEvaluadorPCM = x.EstadoEvaluadorPCM,
                        EstadoEvaluadorMEF = x.EstadoEvaluadorMEF,
                        EstadoFiscalizadorPCM = x.EstadoFiscalizadorPCM,
                        EstadoFiscalizadorMEF = x.EstadoFiscalizadorMEF,
                        EstadoFiscalizadorINDECOPI = x.EstadoFiscalizadorINDECOPI,
                        NomEntidad = x.Entidad.Nombre,
                        GenerarPDF = x.GenerarPDF,
                        publicarCIU = x.publicarCIU,
                        PublicEstandar = x.PublicEstandar,
                        Importar = x.Importar,
                        Departamento = x.Entidad.Provincia == null ? "" : x.Entidad.Provincia.Departamento.Nombre,
                        Provincia = x.Entidad.Provincia == null ? "" : x.Entidad.Provincia.Nombre,

                    }),
                    rol = 1,
                    totalRows = totalRows
                });

            }
            else if (user.Rol == 10 && filtro.EntidadId == 4105)
            {
                List<Expediente> lista = _expedienteService.GetAllLikePaginAsigando(filtro, pageIndex, pageSize, ref totalRows, user.UsuarioId);


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
                        EstadoRactificador = x.EstadoRactificador,
                        EstadoEvaluadorMinisterio = x.EstadoEvaluadorMinisterio,
                        EstadoEvaluadorPCM = x.EstadoEvaluadorPCM,
                        EstadoEvaluadorMEF = x.EstadoEvaluadorMEF,
                        EstadoFiscalizadorPCM = x.EstadoFiscalizadorPCM,
                        EstadoFiscalizadorMEF = x.EstadoFiscalizadorMEF,
                        EstadoFiscalizadorINDECOPI = x.EstadoFiscalizadorINDECOPI,
                        NomEntidad = x.Entidad.Nombre,
                        GenerarPDF = x.GenerarPDF,
                        publicarCIU = x.publicarCIU,
                        PublicEstandar = x.PublicEstandar,
                        Importar = x.Importar,
                        Departamento = x.Entidad.Provincia == null ? "" : x.Entidad.Provincia.Departamento.Nombre,
                        Provincia = x.Entidad.Provincia == null ? "" : x.Entidad.Provincia.Nombre,

                    }),
                    rol = 1,
                    totalRows = totalRows
                });

            }
            //separar no olvidar por sector
            else if ((user.Sector == 80 || user.Sector == 79) && user.Rol == 6)
            {

                filtro.ProvinciaId = user.Provincia;
                List<Expediente> lista = _expedienteService.GetAllLikePaginRatificador(filtro, pageIndex, pageSize, ref totalRows);
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
                        EstadoRactificador = x.EstadoRactificador,
                        EstadoFiscalizadorPCM = x.EstadoFiscalizadorPCM,
                        EstadoFiscalizadorMEF = x.EstadoFiscalizadorMEF,
                        EstadoFiscalizadorINDECOPI = x.EstadoFiscalizadorINDECOPI,
                        NomEntidad = x.Entidad.Nombre,
                        Departamento = x.Entidad.Provincia == null ? "" : x.Entidad.Provincia.Departamento.Nombre,
                        Provincia = x.Entidad.Provincia == null ? "" : x.Entidad.Provincia.Nombre,

                    }),
                    rol = 6,
                    totalRows = totalRows
                });

            }
            else if (user.CodPadre == "2" && user.Rol == 7 || (user.EntidadId == 4105 && user.Rol == 7) || (user.EntidadId == 18497 && user.Rol == 7))
            {
                if (user.EntidadId == 4105 || user.EntidadId == 18497)
                {
                    filtro.EstadoEvaluadorMinisterio = EstadoExpediente.Aprobado;
                    List<Expediente> lista = _expedienteService.GetAllLikePaginEvaluadorMEFPCM(filtro, pageIndex, pageSize, ref totalRows);
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
                            EstadoEvaluadorMinisterio = x.EstadoEvaluadorMinisterio,
                            EstadoEvaluadorPCM = x.EstadoEvaluadorPCM,
                            EstadoEvaluadorMEF = x.EstadoEvaluadorMEF,
                            EstadoFiscalizadorPCM = x.EstadoFiscalizadorPCM,
                            EstadoFiscalizadorMEF = x.EstadoFiscalizadorMEF,
                            EstadoFiscalizadorINDECOPI = x.EstadoFiscalizadorINDECOPI,
                            NomEntidad = x.Entidad.Nombre,
                            Departamento = x.Entidad.Provincia == null ? "" : x.Entidad.Provincia.Departamento.Nombre,
                            Provincia = x.Entidad.Provincia == null ? "" : x.Entidad.Provincia.Nombre,

                        }),
                        rol = 7,
                        totalRows = totalRows
                    });
                }
                else
                {
                    filtro.SectorId = user.Sector;
                    List<Expediente> lista = _expedienteService.GetAllLikePaginEvaluador(filtro, pageIndex, pageSize, ref totalRows);
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
                            EstadoEvaluadorMinisterio = x.EstadoEvaluadorMinisterio,
                            EstadoEvaluadorPCM = x.EstadoEvaluadorPCM,
                            EstadoEvaluadorMEF = x.EstadoEvaluadorMEF,
                            EstadoFiscalizadorPCM = x.EstadoFiscalizadorPCM,
                            EstadoFiscalizadorMEF = x.EstadoFiscalizadorMEF,
                            EstadoFiscalizadorINDECOPI = x.EstadoFiscalizadorINDECOPI,
                            NomEntidad = x.Entidad.Nombre,
                            Departamento = x.Entidad.Provincia == null ? "" : x.Entidad.Provincia.Departamento.Nombre,
                            Provincia = x.Entidad.Provincia == null ? "" : x.Entidad.Provincia.Nombre,

                        }),
                        rol = 7,
                        totalRows = totalRows
                    });
                }

            }
            else if (user.Rol == 8)
            {
                filtro.EstadoExpediente = EstadoExpediente.Publicado;
                List<Expediente> lista = _expedienteService.GetAllLikePaginFiscalizador(filtro, pageIndex, pageSize, ref totalRows);
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
                        EstadoFiscalizadorPCM = x.EstadoFiscalizadorPCM,
                        EstadoFiscalizadorMEF = x.EstadoFiscalizadorMEF,
                        EstadoFiscalizadorINDECOPI = x.EstadoFiscalizadorINDECOPI,
                        NomEntidad = x.NombreEntidad,
                        Departamento = x.Entidad.Provincia == null ? "" : x.Entidad.Provincia.Departamento.Nombre,
                        Provincia = x.Entidad.Provincia == null ? "" : x.Entidad.Provincia.Nombre,

                    }),
                    rol = 8,
                    totalRows = totalRows
                });
            }
            else
            {
                List<Expediente> lista = _expedienteService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

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
                        EstadoFiscalizadorPCM = x.EstadoFiscalizadorPCM,
                        EstadoFiscalizadorMEF = x.EstadoFiscalizadorMEF,
                        EstadoFiscalizadorINDECOPI = x.EstadoFiscalizadorINDECOPI,
                        EstadoRactificador = x.EstadoRactificador,
                        EstadoEvaluadorMinisterio = x.EstadoEvaluadorMinisterio,
                        EstadoEvaluadorPCM = x.EstadoEvaluadorPCM,
                        EstadoEvaluadorMEF = x.EstadoEvaluadorMEF,
                    }),
                    totalRows = totalRows
                });
            }

        }

        public string GetAllLikePaginConf(Procedimiento filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;
            //filtro.EntidadId = user.EntidadId;
            int rol = 0;

            List<Procedimiento> lista = _expedienteService.GetAllLikePaginTodoConfigurar(filtro, pageIndex, pageSize, ref totalRows);
            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    ExpedienteId = x.ExpedienteId,
                    Codigo = x.Expediente.Codigo,
                    FechaCreacion = x.Expediente.FecCreacion.Value.ToShortDateString(),
                    FecModificacion = x.Expediente.FecModificacion.Value.ToShortDateString(),
                    TipoExpediente = (short)x.Expediente.TipoExpediente,
                    EstadoExpediente = (short)x.Expediente.EstadoExpediente,
                    CodigoCorto = x.CodigoCorto,
                    Denominacion = x.Denominacion,
                    NomEntidad = x.Expediente.Entidad.Nombre,
                    ProcedimientoId = x.ProcedimientoId,
                    //Departamento = x.Entidad.Provincia == null ? "" : x.Entidad.Provincia.Departamento.Nombre,
                    //Provincia = x.Entidad.Provincia == null ? "" : x.Entidad.Provincia.Nombre,

                }),
                rol = 1,
                totalRows = totalRows
            });



        }

        public string GetAllLikePaginConfProcedimiento(Procedimiento filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;
            //filtro.EntidadId = user.EntidadId;
            int rol = 0;

            List<Procedimiento> lista = _expedienteService.GetAllLikePaginTodoConfigurarProce(filtro, pageIndex, pageSize, ref totalRows);
            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    ExpedienteId = x.ExpedienteId,
                    Codigo = x.Expediente.Codigo,
                    FechaCreacion = x.Expediente.FecCreacion.Value.ToShortDateString(),
                    FecModificacion = x.Expediente.FecModificacion.Value.ToShortDateString(),
                    TipoExpediente = (short)x.Expediente.TipoExpediente,
                    EstadoExpediente = (short)x.Expediente.EstadoExpediente,
                    CodigoCorto = x.CodigoCorto,
                    Denominacion = x.Denominacion,
                    NomEntidad = x.Expediente.Entidad.Nombre,
                    ProcedimientoId = x.ProcedimientoId,
                    Activar = x.Activar,
                    Activar2 = x.Activar2,
                    Activar3 = x.Activar3,
                    sinnotas = x.sinnotas,
                    reclamacion = x.reclamacion,
                    revision = x.revision,
                    Calificaciones = x.Calificaciones,
                    ActivarTlf = x.ChkAtencionTlf
                }),
                rol = 1,
                totalRows = totalRows
            });



        }



        public string GetAllLikePaginRequisitos(Requisito filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;
            //filtro.EntidadId = user.EntidadId;
            int rol = 0;

            List<Requisito> lista = _expedienteService.GetAllLikePaginTodorequisitos(filtro, pageIndex, pageSize, ref totalRows);
            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    RequisitoId = x.RequisitoId,
                    Nombre = x.Nombre,
                    RecNum = x.RecNum,
                    Activar = x.Activar,
                    ProcedimientoId = x.ProcedimientoId,

                }),
                rol = 1,
                totalRows = totalRows
            });
        }

        [HttpPost]
        public ActionResult GuardarRequisitos(long RequisitoId, UsuarioInfo user)
        {
            try
            {

                Requisito model = _RequisitoService.GetOne(RequisitoId);
                model.Activar = 1;
                _RequisitoService.Save(model);
                //VERIFICAR

                return Json(new { valid = true, mensaje = "Los datos se copiaron correctamente." });


            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                _log.Error(ex);
                var error = "Ya Existe Información en la tabla Asme favor de eliminar las actividades para poder copiar";
                return PartialView("_Error");
            }
        }

        [HttpPost]
        public ActionResult Guardaractivarproce(long procedimientoId, int estado, int tipo, UsuarioInfo user)
        {
            try
            {

                Procedimiento model = _procedimientoService.GetOne(procedimientoId);

                if (tipo == 1)
                {
                    model.Activar = estado;
                }
                else if (tipo == 2)
                {
                    model.Activar2 = estado;
                }
                else if (tipo == 3)
                {
                    model.Activar3 = estado;
                }
                else if (tipo == 4)
                {
                    model.sinnotas = estado;
                }
                else if (tipo == 5)
                {
                    model.reclamacion = estado;
                }
                else if (tipo == 6)
                {
                    model.revision = estado;
                }
                else if (tipo == 7)
                {
                    model.Calificaciones = estado;
                }
                else if (tipo == 8)
                {
                    bool lb_chk = true;
                    if (estado == 0) { lb_chk = false; }
                    model.ChkAtencionTlf = lb_chk;
                }
                _procedimientoService.Updateprocedimiento(model);
                //VERIFICAR

                return Json(new { valid = true, mensaje = "Los datos se copiaron correctamente." });


            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                _log.Error(ex);
                var error = "Ya Existe Información en la tabla Asme favor de eliminar las actividades para poder copiar";
                return PartialView("_Error");
            }
        }

        [HttpPost]
        public ActionResult activarpublicado(long expedienteId, int estado, UsuarioInfo user)
        {
            try
            {
                _expedienteService.activarpublicado(expedienteId, estado);
                //VERIFICAR

                return Json(new { valid = true, mensaje = "Los datos se copiaron correctamente." });


            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                _log.Error(ex);
                var error = "Ya Existe Información en la tabla Asme favor de eliminar las actividades para poder copiar";
                return PartialView("_Error");
            }
        }

        public ActionResult Editarpublicestandar(long id, UsuarioInfo user)
        {
            try
            {
                ViewBag.User = user;

                Expediente item;

                item = _expedienteService.GetOne(id);
                ViewBag.Title = "Editar Publicestandar";

                ViewBag.publicarEstandar = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Sin Acción", Value = "0" },
                    new SelectListItem() { Text = "Finalizar y Publicar", Value = "1" },
                    new SelectListItem() { Text = "Presentar y Ratificación", Value = "2" },
                };


                return PartialView("_OpcionPublic", item);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }


        public ActionResult ImportarPAEliminados(long id, UsuarioInfo user)
        {
            try
            {
                ViewBag.User = user;

                Expediente item;

                item = _expedienteService.GetOne(id);
                ViewBag.Title = "Editar Importacion";

                ViewBag.publicarImportarPAEliminados = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "No Importar", Value = "0" },
                    new SelectListItem() { Text = "Importar", Value = "1" },
                };


                return PartialView("_ImportarPAEliminados", item);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }



        [HttpPost]
        public ActionResult activarpublicadoexp(Expediente model, UsuarioInfo user)
        {
            try
            {


                _expedienteService.activarpublicadoexp(model.ExpedienteId, model.PublicEstandar);
                //VERIFICAR

                return Json(new { valid = true, mensaje = "Los datos se actuálizaron correctamente." });


            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                _log.Error(ex);
                var error = "Ya Existe Información en la tabla Asme favor de eliminar las actividades para poder copiar";
                return PartialView("_Error");
            }
        }




        [HttpPost]
        public ActionResult activaImportarPAEliminados(Expediente model, UsuarioInfo user)
        {
            try
            {


                _expedienteService.activaImportarPAEliminados(model.ExpedienteId, model.Importar);
                //VERIFICAR

                return Json(new { valid = true, mensaje = "Los datos se actuálizaron correctamente." });


            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                _log.Error(ex);
                var error = "Ya Existe Información en la tabla Asme favor de eliminar las actividades para poder copiar";
                return PartialView("_Error");
            }
        }


        [HttpPost]
        public ActionResult duplicado(long expedienteId, UsuarioInfo user)
        {
            try
            {
                _procedimientoService.quitarduplocado();
                //VERIFICAR

                return Json(new { valid = true, mensaje = "Los datos se copiaron correctamente." });


            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                _log.Error(ex);
                var error = "Ya Existe Información en la tabla Asme favor de eliminar las actividades para poder copiar";
                return PartialView("_Error");
            }
        }



        public ActionResult SelectProcACR(bool multi, long ProcedimientoId)
        {
            try
            {
                ViewBag.Multi = false;
                ViewBag.ProcedimientoId = ProcedimientoId;

                return PartialView("_SelectACR");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult ActivarProc(bool multi, long ProcedimientoId)
        {
            try
            {
                ViewBag.Multi = false;
                ViewBag.ProcedimientoId = ProcedimientoId;


                ViewBag.Activar = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Desactivar", Value = "0"},
                    new SelectListItem() { Text = "Activar", Value = "1"}
                };

                return PartialView("_ActivarProc");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public ActionResult NormaGetAllLikePagin(long ExpedienteId, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;

            ExpedienteNorma filtro = new ExpedienteNorma() { ExpedienteId = ExpedienteId };

            List<ExpedienteNorma> lista = _expedienteNormaService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

            List<Enumerado> lstEnum = _enumeradoService.GetByTipo(TipoEnumerado.ENUM_TIPO_NORMA_APROBACION);

            return Json(new
            {
                total = 6,
                data = lista.Select(x => new
                {
                    x.NormaId,
                    x.Descripcion,
                    TipoNorma = x.TipoNorma.Nombre,

                    //verjorget
                    Fechas = (x.Fecha == null ? "" : x.Fecha.Value.ToShortDateString()),

                    x.ArchivoAdjuntoId,
                    x.ArMotivoAdjuntoId,
                    tipoNormaAprobacion = lstEnum.Where(r => r.CODIGO == x.ENUM_TIPO_NORMA_APROBACION.ToString()).FirstOrDefault().VALOR
                })
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult EliminarNorma(int id, long expedi, UsuarioInfo user)
        {

            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Eliminar norma";
                objauditoria.Pantalla = "ArMotivoAdjunto";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;

                Expediente exp = _expedienteService.GetOne(expedi);
                if (exp.EstadoExpediente == EstadoExpediente.EnProceso || exp.EstadoExpediente == EstadoExpediente.Observado)
                {
                    /*auditoria*/
                    _expedienteNormaService.eliminar(id);
                    /*auditoria Grabar*/
                    _AuditoriaService.Save(objauditoria);
                }
                /*auditoria Grabar*/
                return Json(new
                {
                    mensaje = "El Inductor fue eliminada satisfactoriamente."
                });

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }

        }
        public ActionResult EditarNorma(long id, long ExpedienteId, EstadoExpediente Estado, UsuarioInfo user)
        {
            try
            {

                /*Inicio Cargar roles - Acceso*/
                List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 20);
                user.rolmenu = listarolmenu.ToList();
                /*Fin Cargar roles  - Acceso*/
                ViewBag.User = user;

                var NroDocumento = user.NroDocumento;
                Expediente itemexp = new Expediente();

                ExpedienteNorma item;

                if (id == 0)
                {
                    item = new ExpedienteNorma();
                    ViewBag.Title = "Nueva Norma de Aprobación";
                    item.ExpedienteId = ExpedienteId;
                    item.Fecha = DateTime.Now;
                    //if (user.Rol == (short)Rol.Evaluador || user.Rol == (short)Rol.Ratificador)
                    //{
                    //    itemexp = _expedienteService.GetOne(ExpedienteId);
                    //    NroDocumento = itemexp.UserCreacion;
                    //}

                }
                else
                {
                    item = _expedienteNormaService.GetOne(id);
                    ViewBag.Title = "Editar Norma de Aprobación";
                    NroDocumento = item.UserCreacion;
                    //if (user.Rol == (short)Rol.Evaluador || user.Rol == (short)Rol.Ratificador)
                    //{
                    //    itemexp = _expedienteService.GetOne(ExpedienteId);
                    //    NroDocumento = itemexp.UserCreacion;
                    //}

                    if (item.UserCreacion == user.NroDocumento)
                    {

                        ViewBag.estadoUSER = 1;
                    }
                    else { ViewBag.estadoUSER = 0; }

                }


                ViewBag.expetado = (short)Estado;






                List<MetaDato> listMD = _metaDatoService.GetByParent(12);
                ViewBag.TipoNorma = listMD.Select(x => new SelectListItem()
                {
                    Value = x.MetaDatoId.ToString(),
                    Text = x.Nombre
                })
                .ToList();

                List<MetaDato> listMDnorma = _metaDatoService.GetByParent(13);
                ViewBag.TipoNormaLista = listMDnorma.Select(x => new SelectListItem()
                {
                    Value = x.MetaDatoId.ToString(),
                    Text = x.Nombre
                })
                .ToList();

                List<Enumerado> lstEnumerado = _enumeradoService.GetByTipo(TipoEnumerado.ENUM_TIPO_NORMA_APROBACION);

                ViewBag.TipoNormaAprobacion = lstEnumerado.Select(x => new SelectListItem()
                {
                    Value = x.CODIGO,
                    Text = x.VALOR
                }).ToList();


                //ViewBag.TipoNormasid = new List<SelectListItem>()
                //    {
                //        new SelectListItem() { Text = "Decreto Supremo", Value = "1" , Selected = item.TipoNormaId == TipoNormasid. ? true:false},
                //        new SelectListItem() { Text = "Resolución Ministerial", Value = "2",  Selected = model.Renovacio == Renovacio.No ? true:false },
                //        new SelectListItem() { Text = "Consejo Directivo del Organismo Regulador", Value = "1" , Selected = model.Renovacio == Renovacio.Si ? true:false},
                //        new SelectListItem() { Text = "Resolución del Órgano de Dirección del Organismo Técnico Especializado", Value = "2",  Selected = model.Renovacio == Renovacio.No ? true:false },
                //        new SelectListItem() { Text = "Resolución del Titular del Organismo Técnico Especializado", Value = "1" , Selected = model.Renovacio == Renovacio.Si ? true:false},
                //        new SelectListItem() { Text = "Resolución del Titular del Organismo Constitucionalmente Autónomo", Value = "2",  Selected = model.Renovacio == Renovacio.No ? true:false },
                //        new SelectListItem() { Text = "Ordenanza Regional", Value = "1" , Selected = model.Renovacio == Renovacio.Si ? true:false},
                //        new SelectListItem() { Text = "Decreto Regional", Value = "2",  Selected = model.Renovacio == Renovacio.No ? true:false },

                //      };

                //if (user.Rol == 6 || Estado = (2,3,5,7) )


                if (EstadoExpediente.Presentado == Estado || EstadoExpediente.Aprobado == Estado || EstadoExpediente.Subsanado == Estado || EstadoExpediente.Publicado == Estado)
                {
                    //if ((user.Rol == (short)Rol.Evaluador || user.Rol == (short)Rol.Ratificador) && NroDocumento == user.NroDocumento)
                    if ((user.Rol == (short)Rol.Coordinador && EstadoExpediente.Aprobado == Estado))
                    {
                        return PartialView("_EditarNorma", item);
                    }
                    else
                    if ((user.Rol == (short)Rol.Evaluador || user.Rol == (short)Rol.Ratificador))
                    {
                        return PartialView("_EditarNorma", item);
                    }
                    else
                    {
                        return PartialView("_EditarNormaObservacion", item);
                    }
                }
                else
                {
                    return PartialView("_EditarNorma", item);
                }

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }



        public ActionResult EditarNormaInforme(long id, long ExpedienteId, EstadoExpediente Estado, UsuarioInfo user)
        {
            try
            {

                /*Inicio Cargar roles - Acceso*/
                List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 20);
                user.rolmenu = listarolmenu.ToList();
                /*Fin Cargar roles  - Acceso*/
                ViewBag.User = user;

                var NroDocumento = user.NroDocumento;
                Expediente itemexp = new Expediente();
                itemexp = _expedienteService.GetOne(ExpedienteId);
                Informe item;

                List<Informe> itemInforme = new List<Informe>();
                itemInforme = _InformeService.listaGetOne(user.UsuarioId);

                if (id == 0)
                {
                    item = new Informe();
                    ViewBag.Title = "Nueva Norma de Aprobación";
                    item.ExpedienteId = ExpedienteId;
                    item.Fecha = DateTime.Now;
                }
                else
                {
                    item = _InformeService.GetOne(id);
                    ViewBag.Title = "Editar Norma de Aprobación";
                    NroDocumento = item.UserCreacion;

                }

                int numero = itemInforme.Count() + 1;


                ViewBag.Entidad = itemexp.Entidad.Nombre;
                ViewBag.revisor = user.NombreCompleto;
                ViewBag.InformeNo = numero + "-" + DateTime.Now.Year.ToString() + "-PCM/SGP/SSSAR";

                //List<MetaDato> listMD = _metaDatoService.GetByParent(12);
                //ViewBag.TipoNorma = listMD.Select(x => new SelectListItem()
                //{
                //    Value = x.MetaDatoId.ToString(),
                //    Text = x.Nombre
                //})
                //.ToList();

                //List<MetaDato> listMDnorma = _metaDatoService.GetByParent(13);
                //ViewBag.TipoNormaLista = listMDnorma.Select(x => new SelectListItem()
                //{
                //    Value = x.MetaDatoId.ToString(),
                //    Text = x.Nombre
                //})
                //.ToList();

                //List<Enumerado> lstEnumerado = _enumeradoService.GetByTipo(TipoEnumerado.ENUM_TIPO_NORMA_APROBACION);

                //ViewBag.TipoNormaAprobacion = lstEnumerado.Select(x => new SelectListItem()
                //{
                //    Value = x.CODIGO,
                //    Text = x.VALOR
                //}).ToList();

                if (EstadoExpediente.Presentado == Estado || EstadoExpediente.Aprobado == Estado || EstadoExpediente.Subsanado == Estado || EstadoExpediente.Publicado == Estado)
                {
                    //if ((user.Rol == (short)Rol.Evaluador || user.Rol == (short)Rol.Ratificador) && NroDocumento == user.NroDocumento)
                    if ((user.Rol == (short)Rol.Evaluador || user.Rol == (short)Rol.Ratificador))
                    {
                        return PartialView("_EditarNormaInforme", item);
                    }
                    else
                    {
                        return PartialView("_EditarNormaInforme", item);
                    }
                }
                else
                {
                    return PartialView("EditarNormaInforme", item);
                }

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        public ActionResult EditarPublicado(long id, long ExpedienteId, UsuarioInfo user)
        {
            try
            {
                ViewBag.User = user;

                ExpedienteNorma item;

                if (id == 0)
                {
                    item = new ExpedienteNorma();
                    ViewBag.Title = "Nueva Sustento Publicación del TUPA";
                    item.ExpedienteId = ExpedienteId;
                    item.Fecha = DateTime.Now;

                }
                else
                {
                    item = _expedienteNormaService.GetOne(id);
                    ViewBag.Title = "Editar Sustento Publicación del TUPA";
                }


                List<MetaDato> listMD = _metaDatoService.GetByParent(12);
                ViewBag.TipoNorma = listMD.Select(x => new SelectListItem()
                {
                    Value = x.MetaDatoId.ToString(),
                    Text = x.Nombre
                })
                .ToList();

                List<MetaDato> listMDnorma = _metaDatoService.GetByParent(13);
                ViewBag.TipoNormaLista = listMDnorma.Select(x => new SelectListItem()
                {
                    Value = x.MetaDatoId.ToString(),
                    Text = x.Nombre
                })
                .ToList();

                List<Enumerado> lstEnumerado = _enumeradoService.GetByTipo(TipoEnumerado.ENUM_TIPO_NORMA_APROBACION);

                ViewBag.TipoNormaAprobacion = lstEnumerado.Select(x => new SelectListItem()
                {
                    Value = x.CODIGO,
                    Text = x.VALOR
                }).ToList();


                //ViewBag.TipoNormasid = new List<SelectListItem>()
                //    {
                //        new SelectListItem() { Text = "Decreto Supremo", Value = "1" , Selected = item.TipoNormaId == TipoNormasid. ? true:false},
                //        new SelectListItem() { Text = "Resolución Ministerial", Value = "2",  Selected = model.Renovacio == Renovacio.No ? true:false },
                //        new SelectListItem() { Text = "Consejo Directivo del Organismo Regulador", Value = "1" , Selected = model.Renovacio == Renovacio.Si ? true:false},
                //        new SelectListItem() { Text = "Resolución del Órgano de Dirección del Organismo Técnico Especializado", Value = "2",  Selected = model.Renovacio == Renovacio.No ? true:false },
                //        new SelectListItem() { Text = "Resolución del Titular del Organismo Técnico Especializado", Value = "1" , Selected = model.Renovacio == Renovacio.Si ? true:false},
                //        new SelectListItem() { Text = "Resolución del Titular del Organismo Constitucionalmente Autónomo", Value = "2",  Selected = model.Renovacio == Renovacio.No ? true:false },
                //        new SelectListItem() { Text = "Ordenanza Regional", Value = "1" , Selected = model.Renovacio == Renovacio.Si ? true:false},
                //        new SelectListItem() { Text = "Decreto Regional", Value = "2",  Selected = model.Renovacio == Renovacio.No ? true:false },

                //      };

                return PartialView("_EditarPublicado", item);

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }


        public ActionResult EditarPublicadoEnabled(long id, long ExpedienteId, UsuarioInfo user)
        {
            try
            {
                ViewBag.User = user;

                ExpedienteNorma item;

                if (id == 0)
                {
                    item = new ExpedienteNorma();
                    ViewBag.Title = "Nueva Sustento Publicación del TUPA";
                    item.ExpedienteId = ExpedienteId;
                    item.Fecha = DateTime.Now;

                }
                else
                {
                    item = _expedienteNormaService.GetOne(id);
                    ViewBag.Title = "Editar Sustento Publicación del TUPA";
                }


                List<MetaDato> listMD = _metaDatoService.GetByParent(12);
                ViewBag.TipoNorma = listMD.Select(x => new SelectListItem()
                {
                    Value = x.MetaDatoId.ToString(),
                    Text = x.Nombre
                })
                .ToList();

                List<MetaDato> listMDnorma = _metaDatoService.GetByParent(13);
                ViewBag.TipoNormaLista = listMDnorma.Select(x => new SelectListItem()
                {
                    Value = x.MetaDatoId.ToString(),
                    Text = x.Nombre
                })
                .ToList();

                List<Enumerado> lstEnumerado = _enumeradoService.GetByTipo(TipoEnumerado.ENUM_TIPO_NORMA_APROBACION);

                ViewBag.TipoNormaAprobacion = lstEnumerado.Select(x => new SelectListItem()
                {
                    Value = x.CODIGO,
                    Text = x.VALOR
                }).ToList();


                //ViewBag.TipoNormasid = new List<SelectListItem>()
                //    {
                //        new SelectListItem() { Text = "Decreto Supremo", Value = "1" , Selected = item.TipoNormaId == TipoNormasid. ? true:false},
                //        new SelectListItem() { Text = "Resolución Ministerial", Value = "2",  Selected = model.Renovacio == Renovacio.No ? true:false },
                //        new SelectListItem() { Text = "Consejo Directivo del Organismo Regulador", Value = "1" , Selected = model.Renovacio == Renovacio.Si ? true:false},
                //        new SelectListItem() { Text = "Resolución del Órgano de Dirección del Organismo Técnico Especializado", Value = "2",  Selected = model.Renovacio == Renovacio.No ? true:false },
                //        new SelectListItem() { Text = "Resolución del Titular del Organismo Técnico Especializado", Value = "1" , Selected = model.Renovacio == Renovacio.Si ? true:false},
                //        new SelectListItem() { Text = "Resolución del Titular del Organismo Constitucionalmente Autónomo", Value = "2",  Selected = model.Renovacio == Renovacio.No ? true:false },
                //        new SelectListItem() { Text = "Ordenanza Regional", Value = "1" , Selected = model.Renovacio == Renovacio.Si ? true:false},
                //        new SelectListItem() { Text = "Decreto Regional", Value = "2",  Selected = model.Renovacio == Renovacio.No ? true:false },

                //      };

                return PartialView("_EditarPublicadoGrilla", item);

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }


        public ActionResult EditarPublicadoGrilla(long ExpedienteId, UsuarioInfo user)
        {
            try
            {
                ViewBag.User = user;

                ExpedienteNorma exp = _expedienteNormaService.GetOneexpediente(ExpedienteId);

                ExpedienteNorma item = _expedienteNormaService.GetOne(exp.NormaId);
                ViewBag.Title = "Sustento Publicación del TUPA";



                List<MetaDato> listMD = _metaDatoService.GetByParent(12);
                ViewBag.TipoNorma = listMD.Select(x => new SelectListItem()
                {
                    Value = x.MetaDatoId.ToString(),
                    Text = x.Nombre
                })
                .ToList();

                List<MetaDato> listMDnorma = _metaDatoService.GetByParent(13);
                ViewBag.TipoNormaLista = listMDnorma.Select(x => new SelectListItem()
                {
                    Value = x.MetaDatoId.ToString(),
                    Text = x.Nombre
                })
                .ToList();

                List<Enumerado> lstEnumerado = _enumeradoService.GetByTipo(TipoEnumerado.ENUM_TIPO_NORMA_APROBACION);

                ViewBag.TipoNormaAprobacion = lstEnumerado.Select(x => new SelectListItem()
                {
                    Value = x.CODIGO,
                    Text = x.VALOR
                }).ToList();



                return PartialView("_EditarPublicadoGrilla", item);

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        [HttpPost]
        public ActionResult GuardarNorma(ExpedienteNorma model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.NormaId == 0 ? "La Norma fue registrada satisfactoriamente."
                                                : "La Norma fue modificada satisfactoriamente.";

                    ExpedienteNorma obj;

                    if (model.NormaId == 0)
                    {
                        obj = new ExpedienteNorma();
                        obj.ExpedienteId = model.ExpedienteId;

                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;
                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar  Norma";
                        objauditoria.Pantalla = "Norma";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }
                    else
                    {
                        obj = _expedienteNormaService.GetOne(model.NormaId);

                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar  Norma";
                        objauditoria.Pantalla = "Norma";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }

                    obj.Fecha = model.Fecha;
                    obj.TipoNormaId = model.TipoNormaId;
                    obj.Descripcion = model.Descripcion;
                    if (model.ArchivoAdjuntoId == 0)
                    {
                        obj.ArchivoAdjuntoId = null;
                    }
                    else
                    {
                        obj.ArchivoAdjuntoId = model.ArchivoAdjuntoId;
                    }

                    if (model.ArMotivoAdjuntoId == 0)
                    {
                        obj.ArMotivoAdjuntoId = null;
                    }
                    else
                    {
                        obj.ArMotivoAdjuntoId = model.ArMotivoAdjuntoId;
                    }

                    obj.UserModificacion = user.NroDocumento;
                    obj.FecModificacion = DateTime.Now;
                    obj.ENUM_TIPO_NORMA_APROBACION = model.ENUM_TIPO_NORMA_APROBACION;
                    _expedienteNormaService.Save(obj);
                    /*auditoria Grabar*/
                    _AuditoriaService.Save(objauditoria);
                    /*auditoria Grabar*/



                    List<ArMotivoAdjunto> lista = _ArMotivoAdjuntoService.GetAllLikePaginEliminar(model.ExpedienteId);


                    ArMotivoAdjunto ar;
                    foreach (ArMotivoAdjunto l in lista)
                    {
                        ar = _ArMotivoAdjuntoService.GetOne(l.ArMotivoAdjuntoId);
                        ar.Activo = 1;
                        ar.NormaId = obj.NormaId;
                        _ArMotivoAdjuntoService.modificar(ar);
                    }

                    return Json(new
                    {
                        mensaje = mensaje
                    });
                    //return View(model);
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
        public ActionResult GuardarInforme(Informe model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.InformeID == 0 ? "El informe fue generado satisfactoriamente."
                                                : "El informe fue modificada satisfactoriamente.";

                    Informe obj;

                    if (model.InformeID == 0)
                    {
                        obj = new Informe();
                        obj.ExpedienteId = model.ExpedienteId;

                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;
                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar  Norma";
                        objauditoria.Pantalla = "Norma";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }
                    else
                    {
                        obj = _InformeService.GetOne(model.InformeID);

                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar  Norma";
                        objauditoria.Pantalla = "Norma";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }

                    obj.Fecha = model.Fecha;
                    obj.Codigo = model.Codigo;
                    obj.Conclusiones = model.Conclusiones;
                    obj.RevisorId = user.UsuarioId;
                    Expediente itemexp = new Expediente();
                    itemexp = _expedienteService.GetOne(model.ExpedienteId);
                    obj.EntidadId = itemexp.EntidadId;

                    obj.Recomendacion = model.Recomendacion;
                    obj.Estado = "1";
                    obj.Anio = Convert.ToInt32(DateTime.Now.Year.ToString());
                    obj.Titulo = model.Titulo;

                    obj.valor = model.valor;
                    obj.valor2 = model.valor2;
                    obj.valor3 = model.valor3;
                    obj.valor4 = model.valor4;
                    obj.valor5 = model.valor5;



                    obj.UserModificacion = user.NroDocumento;
                    obj.FecModificacion = DateTime.Now;
                    _InformeService.Save(obj);
                    /*auditoria Grabar*/
                    _AuditoriaService.Save(objauditoria);
                    /*auditoria Grabar*/


                    return Json(new
                    {
                        mensaje = mensaje,
                        codigo = obj.InformeID
                    });
                    //return View(model);
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
        /// <summary>
        /// 
        /// 
        /// 
        /// 



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


        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        /// <returns></returns>


        [HttpPost]
        public ActionResult GuardarPublicado(ExpedienteNorma model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.NormaId == 0 ? "La Publicación fue registrada satisfactoriamente."
                                                : "La Publicación fue modificada satisfactoriamente.";

                    ExpedienteNorma obj;
                    string estado;

                    if (model.NormaId == 0)
                    {
                        obj = new ExpedienteNorma();
                        obj.ExpedienteId = model.ExpedienteId;

                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;
                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Publicación";
                        objauditoria.Pantalla = "Publicación";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        estado = "Publicado";
                        /*auditoria*/
                    }
                    else
                    {
                        obj = _expedienteNormaService.GetOne(model.NormaId);


                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Publicación";
                        objauditoria.Pantalla = "Publicación";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        estado = "Modificado";
                        /*auditoria*/
                    }

                    obj.Fecha = model.Fecha;
                    obj.TipoNormaId = model.TipoNormaId;
                    obj.Descripcion = model.Descripcion;
                    obj.ArchivoAdjuntoId = model.ArchivoAdjuntoId;
                    obj.ArMotivoAdjuntoId = model.ArMotivoAdjuntoId;
                    obj.EstadoPublicado = 1;
                    obj.UserModificacion = user.NroDocumento;
                    obj.FecModificacion = DateTime.Now;
                    obj.ENUM_TIPO_NORMA_APROBACION = model.ENUM_TIPO_NORMA_APROBACION;

                    //_expedienteService.CambiarEstado(obj.ExpedienteId, EstadoExpediente.Publicado);

                    _AuditoriaService.CambiarEstado(obj.ExpedienteId, (short)EstadoExpediente.Publicado, user.EntidadId);
                    _expedienteNormaService.Save(obj);
                    /*auditoria Grabar*/
                    _AuditoriaService.Save(objauditoria);
                    /*auditoria Grabar*/
                    /*auditoria Grabar*/
                    string destinatario = "";
                    string NombreEntidadDestinatario = "";
                    string NombreEntidad = "DIRECCIÓN GENERAL DE POLÍTICA DE INGRESOS PÚBLICOS";
                    Expediente expe = _expedienteService.GetOne(model.ExpedienteId);


                    string Expediente = expe.Codigo;

                    Entidad listminis = new Entidad();

                    listminis = _entidadService.GetOne(user.EntidadId);


                    if (listminis != null)
                    {

                        List<Usuario> objent = _usuarioService.GetByEntidad(user.EntidadId).Where(x => x.Estado == EstadoUsuario.Activo && x.ActivarCorreo == 1).ToList();
                        NombreEntidadDestinatario = listminis.Nombre;
                        if (objent.Count() != 0)
                        {
                            foreach (var j in objent)
                            {
                                if (destinatario == "")
                                {
                                    destinatario = j.MiembroEquipo.Correo;
                                }
                                else
                                {
                                    destinatario = destinatario + "," + j.MiembroEquipo.Correo;
                                }
                            }

                            Notificacion presentarEnvio = _notificacionService.GetByone((short)Correo.PublicarEnvio);
                            EnviarCorreo(user.Correo, destinatario, presentarEnvio.CCO, presentarEnvio.Asunto, presentarEnvio.Descripcion.ToString().Replace("[NombreEntidad]", NombreEntidad).Replace("\n", "<br />").Replace("[Expediente]", Expediente).Replace("[NombreEntidadDestinatario]", NombreEntidadDestinatario).Replace("[EstadoExpediente]", estado));

                            //Notificacion presentaRecibido = _notificacionService.GetByone((short)Correo.PublicarRecibido);
                            //EnviarCorreo(user.Correo, user.Correo, presentaRecibido.CCO, presentaRecibido.Asunto, presentaRecibido.Descripcion.ToString().Replace("[NombreEntidad]", NombreEntidad).Replace("\n", "<br />").Replace("[Expediente]", Expediente).Replace("[NombreEntidadDestinatario]", NombreEntidadDestinatario).Replace("[EstadoExpediente]", estado));

                        }
                    }


                    return Json(new
                    {
                        mensaje = mensaje
                    });
                    //return View(model);
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

        public ActionResult GeneraNuevo(TipoExpediente tipo, UsuarioInfo user)
        {
            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Genera Nuevo Expediente";
                objauditoria.Pantalla = "Expediente";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                Expediente obj = new Expediente();
                obj.TipoExpediente = tipo;
                obj.EditarNum = "0";
                obj.Estado = 1;
                obj.OrdenPa = TipoOrdenPa.Sistema;
                obj.SustCostosTerminado = true;

                bool cargainicial = _expedienteService.EsPosibleCargaInicial(user.EntidadId);
                //if (cargainicial != false) { 
                _expedienteService.GenerarNuevo(obj, user.UsuarioId, user.Sector, user.Provincia, user.EntidadId, user.Rol);
                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);


                Procedimiento model = new Procedimiento();
                int totalRows = 1;
                List<Procedimiento> lista = _procedimientoService.GetByExpedienteNumero(obj.ExpedienteId);
                foreach (Procedimiento l in lista)
                {

                    model.ProcedimientoId = l.ProcedimientoId;
                    model.Numero = totalRows;
                    _procedimientoService.guardarnumeroprocedimiento(model);

                    totalRows = totalRows + 1;
                }
                //}


                /*auditoria Grabar*/
                return Json(new { exito = true, ExpedienteId = obj.ExpedienteId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("nuevoExpediente", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }


        public ActionResult ValidarNuevoExpediente(TipoExpediente tipo, UsuarioInfo user)
        {
            try
            {
                Expediente obj = new Expediente();
                obj.TipoExpediente = tipo;
                string mensaje = _expedienteService.ValidarNuevoExpediente(obj, user.UsuarioId);

                Entidad objentidad;

                objentidad = _entidadService.GetOne(user.EntidadId);
                objentidad.ActivarExpediente = 0;
                _entidadService.Save(objentidad);


                return Json(new { exito = true, ExpedienteId = obj.ExpedienteId, resultado = mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("nuevoExpediente", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }


        public ActionResult Nuevo(UsuarioInfo user)
        {
            try
            {
                bool cargainicial = _expedienteService.EsPosibleCargaInicial(user.EntidadId);

                return Json(new { cargainicial = cargainicial }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("nuevoExpediente", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }

        public ActionResult DeclaracionJurada(UsuarioInfo user)
        {

            if (user.DJ == 1)
            {
                return PartialView("_DeclaracionJuradaOpcion");
            }
            else
            {
                return PartialView("_DeclaracionJurada");
            }
        }
        [HttpPost]
        public ActionResult activarinfcondicion(Expediente model, UsuarioInfo user)
        {
            try
            {


                _expedienteService.activarinfcondicion(model.ExpedienteId, model.OrdenPa);
                //VERIFICAR

                return Json(new { valid = true, mensaje = "Los datos se actuálizaron correctamente." });


            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                _log.Error(ex);
                var error = "Ya Existe Información en la tabla Asme favor de eliminar las actividades para poder copiar";
                return PartialView("_Error");
            }
        }
        public ActionResult InformacionAdicional(long id, UsuarioInfo user)
        {
            try
            {
                Expediente model = new Expediente();

                model = _expedienteService.GetOne(id);


                ViewBag.ordenpas = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Sistema", Value = "1" },
                    new SelectListItem() { Text = "Unidad Organica", Value = "2" },
                    new SelectListItem() { Text = "Bloque", Value = "3" },
                    new SelectListItem() { Text = "Unidad Organica y Bloque", Value = "4" }
                };



                ViewBag.User = user;
                ViewBag.Estadisticas = _expedienteService.GetEstadisticas(id);
                ViewBag.EstadoRepWord = model.EstadoReporteWord;

                return PartialView("_InformacionAdicional", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        public ActionResult Editar(long id, UsuarioInfo user)
        {

            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 20);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/

            Session["idexped"] = id;
            long entityid = user.EntidadId;
            Session["entityid"] = entityid;

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



            Observacion obs = new Observacion();
            ViewBag.Usuario = user;
            Expediente model = new Expediente();

            ExpedienteNorma obj = new ExpedienteNorma();
            obj = _expedienteNormaService.GetOneexpediente(id);
            ViewBag.expedientenorma = obj;

            obs.ExpedienteId = id;
            //obs.EntidadId = user.EntidadId;
            obs.CodValidacion = "1";
            obs.Estado = "1";
            obs.NombreCampo = "Nota de Ayuda General Expediente";
            ViewBag.Observacion = _observacionService.GetOneGlobal(obs);
            try
            {
                model = _expedienteService.GetOne(id);

                ViewBag.User = user;
                ViewBag.Estadisticas = _expedienteService.GetEstadisticas(id);

                ViewBag.EstadoRepWord = model.EstadoReporteWord;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("editarExpediente", ex.Message);
                _log.Error(ex);
            }
            return View(model);
        }

        public ActionResult EditarObservacion(long id, UsuarioInfo user)
        {

            ViewBag.Usuario = user;
            int nivel = 1;
            if (user.EntidadId == 4105)
            {
                nivel = 2;
            }
            else if (user.EntidadId == 18497)
            {
                nivel = 3;
            }
            NotificacionExpedientes objnot = _NotificacionExpedientesService.GetByoneExpedinte(id, nivel);
            if (objnot != null)
            {
                ViewBag.RevInfCdno = objnot.RevInfCdno;
                ViewBag.RevDatosGenerales = objnot.RevDatosGenerales;
                ViewBag.RevSutentoTecnico = objnot.RevSutentoTecnico;
                ViewBag.RevTablaAsme = objnot.RevTablaAsme;
                ViewBag.RevSustentoCosto = objnot.RevSustentoCosto;
                ViewBag.desactivar = 1;
            }
            else
            {
                ViewBag.desactivar = 0;
                ViewBag.RevInfCdno = 0;
                ViewBag.RevDatosGenerales = 0;
                ViewBag.RevSutentoTecnico = 0;
                ViewBag.RevTablaAsme = 0;
                ViewBag.RevSustentoCosto = 0;
            }

            Expediente model = new Expediente();
            Observacion obs = new Observacion();
            Filtros filorder = new Filtros();

            Filtros filData = System.Web.HttpContext.Current.Session["modelfiltro"] as Filtros;

            ViewBag.filtro = filData;

            if (filData != null)
            {
                filorder.Clasificacion = filData.Clasificacion;
                filorder.Prestacionesanuales = filData.Prestacionesanuales;
                filorder.PrestacionesCosto = filData.PrestacionesCosto;
                filorder.Duracion = filData.Duracion;
                filorder.PlazoAtencion = filData.PlazoAtencion;
                filorder.Requisitos = filData.Requisitos;
            }
            try
            {
                model = _expedienteService.GetOne(id);
                obs.ExpedienteId = id;
                obs.EntidadId = user.EntidadId;
                obs.CodValidacion = "1";
                obs.Estado = "1";
                obs.NombreCampo = "Nota de Ayuda General Expediente";

                ViewBag.Observacion = _observacionService.GetOneGlobal(obs);
                ViewBag.User = user;
                ViewBag.Estadisticas = _expedienteService.GetEstadisticas(id);
                ViewBag.EstadoRepWord = model.EstadoReporteWord;

                ViewBag.Calificacion = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Ninguno", Value = "0" , Selected = filorder.Clasificacion == CalificacionProcedimiento.Ninguno ? true:false},
                    new SelectListItem() { Text = "Aprobación Automática", Value = "1" , Selected = filorder.Clasificacion == CalificacionProcedimiento.Automatica ? true:false},
                    new SelectListItem() { Text = "Evaluación Previa con Silencio Positivo", Value = "2",  Selected = filorder.Clasificacion== CalificacionProcedimiento.SilencioPositivo ? true:false },
                    new SelectListItem() { Text = "Evaluación Previa con Silencio Negativo", Value = "3" , Selected = filorder.Clasificacion == CalificacionProcedimiento.SilencioNegativo ? true:false},
                    new SelectListItem() { Text = "", Value = "4" , Selected = filorder.Clasificacion == CalificacionProcedimiento.Enblanco ? true:false}
                };

                ViewBag.OrdenarPrestacionesanuales = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Ninguno", Value = "0" , Selected = filorder.Prestacionesanuales == FiltrosOrdenar.Ninguno ? true:false},
                    new SelectListItem() { Text = "Mayor a Menor", Value = "1",  Selected = filorder.Prestacionesanuales == FiltrosOrdenar.Ascendente ? true:false },
                    new SelectListItem() { Text = "Menor a Mayor", Value = "2" , Selected = filorder.Prestacionesanuales == FiltrosOrdenar.Descendente ? true:false}
                };
                ViewBag.OrdenarPrestacionesCosto = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Ninguno", Value = "0" , Selected = filorder.PrestacionesCosto == FiltrosOrdenar.Ninguno ? true:false},
                    new SelectListItem() { Text = "Mayor a Menor", Value = "1",  Selected = filorder.PrestacionesCosto == FiltrosOrdenar.Ascendente ? true:false },
                    new SelectListItem() { Text = "Menor a Mayor", Value = "2" , Selected = filorder.PrestacionesCosto == FiltrosOrdenar.Descendente ? true:false}
                };
                ViewBag.OrdenarDuracion = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Ninguno", Value = "0" , Selected = filorder.Duracion == FiltrosOrdenar.Ninguno ? true:false},
                    new SelectListItem() { Text = "Mayor a Menor", Value = "1",  Selected = filorder.Duracion == FiltrosOrdenar.Ascendente ? true:false },
                    new SelectListItem() { Text = "Menor a Mayor", Value = "2" , Selected = filorder.Duracion == FiltrosOrdenar.Descendente ? true:false}
                };
                ViewBag.OrdenarPlazoAtencion = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Ninguno", Value = "0" , Selected = filorder.PlazoAtencion == FiltrosOrdenar.Ninguno ? true:false},
                    new SelectListItem() { Text = "Mayor a Menor", Value = "1",  Selected = filorder.PlazoAtencion == FiltrosOrdenar.Ascendente ? true:false },
                    new SelectListItem() { Text = "Menor a Mayor", Value = "2" , Selected = filorder.PlazoAtencion == FiltrosOrdenar.Descendente ? true:false}
                };
                ViewBag.OrdenarRequisitos = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Ninguno", Value = "0" , Selected = filorder.Requisitos == FiltrosOrdenar.Ninguno ? true:false},
                    new SelectListItem() { Text = "Mayor a Menor", Value = "1",  Selected = filorder.Requisitos == FiltrosOrdenar.Ascendente ? true:false },
                    new SelectListItem() { Text = "Menor a Mayor", Value = "2" , Selected = filorder.Requisitos == FiltrosOrdenar.Descendente ? true:false}
                };


                ViewBag.seccion = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Seleccionar", Value = "0" },
                    new SelectListItem() { Text = "Informacion Ciudadano", Value = "1"},
                    new SelectListItem() { Text = "Datos Generales", Value = "2"},
                    new SelectListItem() { Text = "Sutento Tecnico", Value = "3"},
                    new SelectListItem() { Text = "Tabla ASME", Value = "4"},
                    new SelectListItem() { Text = "Sustento Costo", Value = "5"}
                };


                ViewBag.estadoseccion = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Seleccionar", Value = "0"},
                    //new SelectListItem() { Text = "En Proceso", Value = "1" },
                    new SelectListItem() { Text = "Terminar", Value = "2" }
                };
                ExpedienteNorma obj = new ExpedienteNorma();
                obj = _expedienteNormaService.GetOneexpediente(id);
                ViewBag.expedientenorma = obj;
                var entidadDatos = _expedienteService.GetOne(id);
                ViewBag.DatosUsuario = _usuarioService.GetByEntidad(entidadDatos.EntidadId).Where(x => x.Estado == EstadoUsuario.Activo).ToList();


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("editarExpediente", ex.Message);
                _log.Error(ex);
            }
            return View(model);
        }

        public ActionResult VerObservacion(long id, long entidadid, UsuarioInfo user)
        {

            ViewBag.Usuario = user;
            ViewBag.entidadver = entidadid;
            int nivel = 1;
            if (entidadid == 4105)
            {
                nivel = 2;
            }
            else if (entidadid == 18497)
            {
                nivel = 3;
            }
            NotificacionExpedientes objnot = _NotificacionExpedientesService.GetByoneExpedinte(id, nivel);
            if (objnot != null)
            {
                ViewBag.RevInfCdno = objnot.RevInfCdno;
                ViewBag.RevDatosGenerales = objnot.RevDatosGenerales;
                ViewBag.RevSutentoTecnico = objnot.RevSutentoTecnico;
                ViewBag.RevTablaAsme = objnot.RevTablaAsme;
                ViewBag.RevSustentoCosto = objnot.RevSustentoCosto;
                ViewBag.desactivar = 1;
            }
            else
            {
                ViewBag.desactivar = 0;
                ViewBag.RevInfCdno = 0;
                ViewBag.RevDatosGenerales = 0;
                ViewBag.RevSutentoTecnico = 0;
                ViewBag.RevTablaAsme = 0;
                ViewBag.RevSustentoCosto = 0;
            }

            Expediente model = new Expediente();
            Observacion obs = new Observacion();
            Filtros filorder = new Filtros();

            Filtros filData = System.Web.HttpContext.Current.Session["modelfiltro"] as Filtros;

            ViewBag.filtro = filData;

            if (filData != null)
            {
                filorder.Clasificacion = filData.Clasificacion;
                filorder.Prestacionesanuales = filData.Prestacionesanuales;
                filorder.PrestacionesCosto = filData.PrestacionesCosto;
                filorder.Duracion = filData.Duracion;
                filorder.PlazoAtencion = filData.PlazoAtencion;
                filorder.Requisitos = filData.Requisitos;
            }
            try
            {
                model = _expedienteService.GetOne(id);
                obs.ExpedienteId = id;
                obs.EntidadId = entidadid;
                obs.TipoEstado = "1";
                obs.CodValidacion = "5";
                obs.Estado = "0";
                obs.NombreCampo = "Nota de Ayuda General Expediente";

                ViewBag.Observacion = _observacionService.GetOneGlobal(obs);
                ViewBag.User = user;
                ViewBag.Estadisticas = _expedienteService.GetEstadisticas(id);
                ViewBag.EstadoRepWord = model.EstadoReporteWord;

                ViewBag.Calificacion = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Ninguno", Value = "0" , Selected = filorder.Clasificacion == CalificacionProcedimiento.Ninguno ? true:false},
                    new SelectListItem() { Text = "Aprobación Automática", Value = "1" , Selected = filorder.Clasificacion == CalificacionProcedimiento.Automatica ? true:false},
                    new SelectListItem() { Text = "Evaluación Previa con Silencio Positivo", Value = "2",  Selected = filorder.Clasificacion== CalificacionProcedimiento.SilencioPositivo ? true:false },
                    new SelectListItem() { Text = "Evaluación Previa con Silencio Negativo", Value = "3" , Selected = filorder.Clasificacion == CalificacionProcedimiento.SilencioNegativo ? true:false},
                    new SelectListItem() { Text = "", Value = "4" , Selected = filorder.Clasificacion == CalificacionProcedimiento.Enblanco ? true:false}
                };

                ViewBag.OrdenarPrestacionesanuales = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Ninguno", Value = "0" , Selected = filorder.Prestacionesanuales == FiltrosOrdenar.Ninguno ? true:false},
                    new SelectListItem() { Text = "Mayor a Menor", Value = "1",  Selected = filorder.Prestacionesanuales == FiltrosOrdenar.Ascendente ? true:false },
                    new SelectListItem() { Text = "Menor a Mayor", Value = "2" , Selected = filorder.Prestacionesanuales == FiltrosOrdenar.Descendente ? true:false}
                };
                ViewBag.OrdenarPrestacionesCosto = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Ninguno", Value = "0" , Selected = filorder.PrestacionesCosto == FiltrosOrdenar.Ninguno ? true:false},
                    new SelectListItem() { Text = "Mayor a Menor", Value = "1",  Selected = filorder.PrestacionesCosto == FiltrosOrdenar.Ascendente ? true:false },
                    new SelectListItem() { Text = "Menor a Mayor", Value = "2" , Selected = filorder.PrestacionesCosto == FiltrosOrdenar.Descendente ? true:false}
                };
                ViewBag.OrdenarDuracion = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Ninguno", Value = "0" , Selected = filorder.Duracion == FiltrosOrdenar.Ninguno ? true:false},
                    new SelectListItem() { Text = "Mayor a Menor", Value = "1",  Selected = filorder.Duracion == FiltrosOrdenar.Ascendente ? true:false },
                    new SelectListItem() { Text = "Menor a Mayor", Value = "2" , Selected = filorder.Duracion == FiltrosOrdenar.Descendente ? true:false}
                };
                ViewBag.OrdenarPlazoAtencion = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Ninguno", Value = "0" , Selected = filorder.PlazoAtencion == FiltrosOrdenar.Ninguno ? true:false},
                    new SelectListItem() { Text = "Mayor a Menor", Value = "1",  Selected = filorder.PlazoAtencion == FiltrosOrdenar.Ascendente ? true:false },
                    new SelectListItem() { Text = "Menor a Mayor", Value = "2" , Selected = filorder.PlazoAtencion == FiltrosOrdenar.Descendente ? true:false}
                };
                ViewBag.OrdenarRequisitos = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Ninguno", Value = "0" , Selected = filorder.Requisitos == FiltrosOrdenar.Ninguno ? true:false},
                    new SelectListItem() { Text = "Mayor a Menor", Value = "1",  Selected = filorder.Requisitos == FiltrosOrdenar.Ascendente ? true:false },
                    new SelectListItem() { Text = "Menor a Mayor", Value = "2" , Selected = filorder.Requisitos == FiltrosOrdenar.Descendente ? true:false}
                };


                ViewBag.seccion = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Seleccionar", Value = "0" },
                    new SelectListItem() { Text = "Informacion Ciudadano", Value = "1"},
                    new SelectListItem() { Text = "Datos Generales", Value = "2"},
                    new SelectListItem() { Text = "Sutento Tecnico", Value = "3"},
                    new SelectListItem() { Text = "Tabla ASME", Value = "4"},
                    new SelectListItem() { Text = "Sustento Costo", Value = "5"}
                };


                ViewBag.estadoseccion = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Seleccionar", Value = "0"},
                    new SelectListItem() { Text = "En Proceso", Value = "1" },
                    new SelectListItem() { Text = "Terminar", Value = "2" }
                };

                var entidadDatos = _expedienteService.GetOne(id);
                ViewBag.DatosUsuario = _usuarioService.GetByEntidad(entidadDatos.EntidadId).Where(x => x.Estado == EstadoUsuario.Activo).ToList();


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("editarExpediente", ex.Message);
                _log.Error(ex);
            }
            return View(model);
        }

        public ActionResult guardarseccion(long Id, int Seccion, int Estadoseccion, UsuarioInfo user)
        {
            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                if (Estadoseccion == 1)
                {
                    objauditoria.Actividad = "Iniciar Proceso";
                }
                else
                {
                    objauditoria.Actividad = "Detener Proceso";
                }
                objauditoria.Pantalla = "Expediente: " + Id;
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                int nivel = 1;
                if (user.EntidadId == 4105)
                {
                    nivel = 2;
                }
                else if (user.EntidadId == 18497)
                {
                    nivel = 3;
                }
                NotificacionExpedientes objnot = _NotificacionExpedientesService.GetByoneExpedinte(Id, nivel);

                if (Seccion == 1)
                {
                    objnot.RevInfCdno = Estadoseccion;
                }
                else if (Seccion == 2)
                {
                    objnot.RevDatosGenerales = Estadoseccion;
                }
                else if (Seccion == 3)
                {
                    objnot.RevSutentoTecnico = Estadoseccion;
                }
                else if (Seccion == 4)
                {
                    objnot.RevTablaAsme = Estadoseccion;
                }
                else if (Seccion == 5)
                {
                    objnot.RevSustentoCosto = Estadoseccion;
                }
                objnot.FecUltimaRevision = DateTime.Now;

                _NotificacionExpedientesService.Save(objnot);
                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/

                _AuditoriaService.ValidarProcedimiento(Id, Seccion);


                return Json(new { valid = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }
        public ActionResult Anular(long id, UsuarioInfo user)
        {
            try
            {  /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Anular";
                objauditoria.Pantalla = "Expediente";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                //_expedienteService.CambiarEstado(id, EstadoExpediente.Anulado);
                _AuditoriaService.AnularExpediente(id);
                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/

                return Json(new { valid = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        public ActionResult FinalizarPresentar(long Id, string Codigo, UsuarioInfo user)
        {
            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Finalizar Presentar";
                objauditoria.Pantalla = "Expediente";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                //_expedienteService.CambiarEstado(Id, EstadoExpediente.Presentado);
                _AuditoriaService.CambiarEstado(Id, (short)EstadoExpediente.Presentado, user.EntidadId);
                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/

                /*auditoria Grabar*/
                NotificacionExpedientes objnot = new NotificacionExpedientes();
                objnot.ExpedienteId = Id;
                objnot.EntidadId = user.EntidadId;
                objnot.SectorId = user.Sector;
                objnot.ProvinciaId = user.Provincia;

                objnot.FecEnvio = DateTime.Now;
                objnot.DiasTranscurrido = 0;
                objnot.EstadoExpediente = (short)EstadoExpediente.Presentado;
                objnot.Tomar = 0;
                objnot.Nivel = 1;
                objnot.RevEstado = 0; // Pendiente
                objnot.RevInfCdno = 0; // Sin Acción
                objnot.RevDatosGenerales = 0; // Sin Acción
                objnot.RevSutentoTecnico = 0; // Sin Acción
                objnot.RevTablaAsme = 0; // Sin Acción
                objnot.RevSustentoCosto = 0; // Sin Acción
                objnot.UserCreacion = user.NroDocumento;
                objnot.UserModificacion = user.NroDocumento;
                objnot.FecCreacion = DateTime.Now;
                objnot.FecModificacion = DateTime.Now;

                _NotificacionExpedientesService.Save(objnot);
                /*auditoria Grabar*/
                string destinatario = "";
                string NombreEntidadDestinatario = "";
                string NombreEntidad = user.NomEntidad;
                string Expediente = Codigo;

                Entidad listminis = new Entidad();
                if (user.Provincia == 0)
                {

                    listminis = _entidadService.GetOneMin(user.Sector);
                }
                else
                {
                    listminis = _entidadService.GetOneProvincia(user.Provincia);
                }


                if (listminis != null)
                {
                    NombreEntidadDestinatario = listminis.Nombre;
                    List<Usuario> objent = _usuarioService.GetByEntidad(listminis.EntidadId).Where(x => x.Estado == EstadoUsuario.Activo && x.ActivarCorreo == 1).ToList();

                    if (objent.Count() != 0)
                    {
                        foreach (var j in objent)
                        {
                            if (destinatario == "")
                            {
                                //destinatario = j.MiembroEquipo.Correo + "," + user.Correo;
                                destinatario = j.MiembroEquipo.Correo ;
                            }
                            else
                            {
                                //destinatario = destinatario + "," + j.MiembroEquipo.Correo + "," + user.Correo;
                                destinatario = destinatario + "," + j.MiembroEquipo.Correo ;
                            }
                        }

                        Notificacion presentarEnvio = _notificacionService.GetByone((short)Correo.PresentarEnvio);
                        EnviarCorreo(user.Correo, destinatario, presentarEnvio.CCO, presentarEnvio.Asunto, presentarEnvio.Descripcion.ToString().Replace("[NombreEntidad]", NombreEntidad).Replace("\n", "<br />").Replace("[Expediente]", Expediente).Replace("[NombreEntidadDestinatario]", NombreEntidadDestinatario));

                        //Notificacion presentaRecibido = _notificacionService.GetByone((short)Correo.PresentarRecibido);
                        //EnviarCorreo(user.Correo, user.Correo, presentaRecibido.CCO, presentaRecibido.Asunto, presentaRecibido.Descripcion.ToString().Replace("[NombreEntidad]", NombreEntidad).Replace("\n", "<br />").Replace("[Expediente]", Expediente).Replace("[NombreEntidadDestinatario]", NombreEntidadDestinatario));

                    }
                }

                /// actualizar estados
                /// 
                _AuditoriaService.PresentarAprobar(Id);

                return Json(new { valid = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        /*metodo de envio de correo*/

        public void EnviarCorreo(string _strCorreos, string _strCorreosCC, string _strCorreosBCC, string Titulo, string Mensaje)
        {
            try
            {
                Correos correolist = _correosService.GetByone(1);

                //var strcuerpo = "<div class=\"m_ - 127107708333239351WordSection1\" > ";
                //strcuerpo += "<div class=\"adM\"></div>";
                //strcuerpo += "<p class=\"MsoNormal\">";
                //strcuerpo += "<span style=\"font-family:&quot;Arial&quot;,sans-serif\">Estimado, " + Mensaje + "<u></u><u></u></span></p>";
                //strcuerpo += "<p class=\"MsoNormal\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\"><u></u>&nbsp;<u></u></span></p>";
                //strcuerpo += "<p class=\"MsoNormal\" style=\"margin-bottom:6.0pt\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\">Se remite el correo para su revicion en la bandeja de la ONP";
                //strcuerpo += "</span><span style=\"font-size:9.0pt;font-family:&quot;Arial&quot;,sans-serif\"><u></u><u></u></span></p>";
                //strcuerpo += "<p class=\"MsoNormal\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\"><u></u>&nbsp;<u></u></span></p>";
                //strcuerpo += "<p class=\"MsoNormal\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\">Por lo que agradeceré ingresar a la plataforma.<u></u><u></u></span></p>";
                //strcuerpo += "<p class=\"MsoNormal\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\"><u></u>&nbsp;<u></u></span></p>";
                //strcuerpo += "<p class=\"MsoNormal\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\">Saludos,<u></u><u></u></span></p>";
                //strcuerpo += "<p class=\"MsoNormal\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\"><u></u>&nbsp;<u></u></span></p>";
                //strcuerpo += "<table border=\"0\" cellspacing =\"0\" cellpadding =\"0\" style =\"border-collapse:collapse\">";
                //strcuerpo += "<tbody>";
                //strcuerpo += "<tr style=\"height:75.5pt\">";
                //strcuerpo += "<td width=\"158\" valign =\"top\" style =\"width:118.8pt;padding:0cm 5.4pt 0cm 5.4pt;height:75.5pt\">";
                //strcuerpo += "<p class=\"MsoNormal\" style =\"line-height:115%\"><span style=\"font-family:&quot;Spranq eco sans&quot;,sans-serif;color:#1f497d\"><img width=\"142\" height =\"70\" style =\"width:1.4791in;height:.7291in\";";
                //strcuerpo += " id =\"m_-127107708333239351Imagen_x0020_4\" src =\"https://mail.google.com/mail/u/0?ui=2&ik=59e0793eb4&attid=0.0.1&permmsgid=msg-f:1683759635597018665&th=175dea77b6e6c629&view=fimg&fur=ip&sz=s0-l75-ft&attbid=ANGjdJ8m5tUDJprH2ZYzyt_AxCHzCpMBAjoXFZUpDzrFKHKesmW2J-C409vGASq7Cy3zn4nqu81z018O5KRho6dSUvYHXZUECCF7S6-bPgcC7vWScQJg94alUBJV4hc&disp=emb \" alt =\"cid:image001.jpg@01D0E495.5F56ABC0\" data - image - whitelisted =\"\" class=\"CToWUd\"></span><span lang=\"ES\" style =\"font-family:&quot;Spranq eco sans&quot;,sans-serif;color:#1f497d\"><u></u><u></u></span></p>";
                //strcuerpo += "</td>";
                //strcuerpo += "<td width=\"550\" valign=\"top\" style =\"width:508.2pt;padding:0cm 5.4pt 0cm 5.4pt;height:75.5pt\">";
                //strcuerpo += "<p class=\"MsoNormal\" style=\"line-height:115%\"><b><span style=\"color:#17365d\">s<u></u><u></u></span></b></p>";
                //strcuerpo += "<p class=\"MsoNormal\" style=\"line-height:115%\"><span style=\"font-size:9.0pt;line-height:115%;color:#4a442a\">Desarrollo de Soluciones TI";
                //strcuerpo += "</span><b><span style=\"font-size:9.0pt;line-height:115%;color:#943634\">|</span></b><span style=\"font-size:9.0pt;line-height:115%;color:#4a442a\"> Oficina de Normalización Previsional – ONP";
                //strcuerpo += "</span><b><span style=\"font-size:9.0pt;line-height:115%;color:#943634\">|</span></b><span style=\"font-size:9.0pt;line-height:115%;color:#1f497d\">";
                //strcuerpo += "</span><span style=\"font-size:9.0pt;line-height:115%;color:#4a442a\">Jr. Bolivia 109, Lima.<u></u><u></u></span></p>";
                //strcuerpo += "<p class=\"MsoNormal\" style=\"line-height:115%\"><span style=\"font-size:9.0pt;line-height:115%;color:#4a442a\">Cel.: 991494436";
                //strcuerpo += "</span><b><span style=\"font-size:9.0pt;line-height:115%;color:#943634\">|</span></b><span style=\"font-size:9.0pt;line-height:115%;color:#1f497d\">";
                //strcuerpo += "</span><span style=\"font-size:9.0pt;line-height:115%;font-family:Wingdings;color:#4a442a\">(</span><span style=\"font-size:9.0pt;line-height:115%;font-family:Symbol;color:#4a442a\">";
                //strcuerpo += "</span><span style=\"font-size:9.0pt;line-height:115%;color:#4a442a\">(+511) 634-2222 anexo: 2891</span><b><span style=\"font-size:9.0pt;line-height:115%;color:#943634\">|</span></b><span style=\"font-size:9.0pt;line-height:115%;color:#1f497d\">";
                //strcuerpo += "</span><span style=\"color:#1f497d\"><a href=\"http://intranet/Plantillas/Plantilla%20Autofirma/www.onp.gob.pe\" target=\"_blank\" data -saferedirecturl=\"https://mail.google.com/mail/u/0?ui=2&ik=59e0793eb4&attid=0.0.2&permmsgid=msg-f:1683759635597018665&th=175dea77b6e6c629&view=fimg&fur=ip&sz=s0-l75-ft&attbid=ANGjdJ-Istglh9ct3uAF2tS2_9xhxXqxBNF-rxqJOoUfaM4bYXZ9SlCnZJQZlZEK7Uu56groGlBM325DGuFa2nso3-pKIzkX30lTIPsKT3UXybsJmW1pkf1fTcNPuQ8&disp=emb \" ><span style=\"font-size:9.0pt;line-height:115%;color:blue\">www.onp.gob.pe</span></a></span><span style=\"font-size:9.0pt;line-height:115%;color:#1f497d\"><u></u><u></u></span></p>";
                //strcuerpo += "<p class=\"MsoNormal\" style=\"line-height:115%\"><span style=\"font-size:9.0pt;line-height:115%;color:#4a442a\"><img border=\"0\" width =\"13\" height =\"13\" style =\"width:.1354in;height:.1354in\" id =\"m_-127107708333239351Imagen_x0020_3\" src =\"https://mail.google.com/mail/u/0?ui=2&ik=59e0793eb4&attid=0.0.2&permmsgid=msg-f:1683759635597018665&th=175dea77b6e6c629&view=fimg&fur=ip&sz=s0-l75-ft&attbid=ANGjdJ-Istglh9ct3uAF2tS2_9xhxXqxBNF-rxqJOoUfaM4bYXZ9SlCnZJQZlZEK7Uu56groGlBM325DGuFa2nso3-pKIzkX30lTIPsKT3UXybsJmW1pkf1fTcNPuQ8&disp=emb\" alt=\"cid:image002.gif@01D0E495.5F56ABC0\" data -image-whitelisted=\"\" class=\"CToWUd\">&nbsp;En";
                //strcuerpo += " la ONP preservamos el medio ambiente, imprima este correo sólo si es necesario.</span><span style=\"color:#4a442a\"><u></u><u></u></span></p>";
                //strcuerpo += "</td>";
                //strcuerpo += "</tr>";
                //strcuerpo += "</tbody>";
                //strcuerpo += "</table>";
                //strcuerpo += "<p class=\"MsoNormal\"><span><u></u>&nbsp;<u></u></span></p>";
                //strcuerpo += "<p class=\"MsoNormal\"><u></u>&nbsp;<u></u></p>";
                //strcuerpo += "</div>";

                MailMessage correo = new MailMessage();
                correo.To.Add(_strCorreosCC);
                correo.From = new MailAddress(correolist.From);
                correo.Subject = Titulo;
                correo.IsBodyHtml = true;
                correo.Body = Mensaje;
                correo.CC.Add(_strCorreos );
                correo.Bcc.Add(_strCorreosBCC);
                try
                {
                    SmtpClient clienteSmtp = new SmtpClient(correolist.Host);
                    clienteSmtp.EnableSsl = true;
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    clienteSmtp.Credentials = new NetworkCredential(correolist.CredentialsUser, correolist.CredentialsPass);
                    clienteSmtp.Send(correo);
                }
                catch (SmtpException error)
                {
                    string smensaje = "";


                    if (error.StatusCode == SmtpStatusCode.MailboxUnavailable)
                    {
                        smensaje = "0No se pudo enviar el correo." + Environment.NewLine + "la direccion de correo no es valida";
                    }
                    else
                    {
                        smensaje = "0" + error.Message;
                    }
                }


                //////System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

                //////mail.From = new MailAddress("jleon.valle1@gmail.com");

                //////mail.To.Add(_strCorreos);
                //////mail.Subject = Titulo;
                //////mail.Body = Mensaje;

                //////SmtpClient smtp = new SmtpClient();

                //////smtp.Host = "smtp.gmail.com";
                //////smtp.Port = 25; //465; //587
                //////smtp.Credentials = new NetworkCredential("jleon.valle1@gmail.com", "L!onV@lle31");
                //////smtp.EnableSsl = true;
                //////try
                //////{
                //////    smtp.Send(mail);
                //////}
                //////catch (Exception ex)
                //////{
                //////    throw new Exception("No se ha podido enviar el email", ex.InnerException);
                //////}
                //////finally
                //////{
                //////    smtp.Dispose();
                //////}



            }
            catch (Exception ex)
            {
                string smensaje = "";
                smensaje = "alert('Error  :" + ex.Message.ToString() + " ');";
                string scriptStr = smensaje;
            }
        }
        /*fin*/



        public ActionResult Publicar(long id, UsuarioInfo user)
        {
            try
            {  /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Publicar";
                objauditoria.Pantalla = "Expediente";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                //_expedienteService.CambiarEstado(id, EstadoExpediente.Publicado);
                _AuditoriaService.CambiarEstado(id, (short)EstadoExpediente.Publicado, user.EntidadId);
                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
                return Json(new { valid = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        public ActionResult DesPublicarCargaInicial(long ExpedienteId, UsuarioInfo user)
        {
            try
            {
                Expediente obj = _expedienteService.GetOne(ExpedienteId);

                List<Expediente> listaexpe = _expedienteService.GetByEntidadMax(obj.EntidadId);

                if (listaexpe[0].ExpedienteId == ExpedienteId)
                {
                    List<Expediente> listaexpeanulado = _expedienteService.GetByEntidadAnulado(obj.EntidadId);
                    if (listaexpeanulado.Count > 1)
                    {

                        return Json(new { valid = false }, JsonRequestBehavior.AllowGet);

                    }
                }
                else
                {
                    return Json(new { valid = false }, JsonRequestBehavior.AllowGet);
                }




                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Despublicar";
                objauditoria.Pantalla = "Expediente";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                _expedienteService.DespublicarCambiarEstado(ExpedienteId, EstadoExpediente.EnProceso);
                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
                return Json(new { valid = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }




        public ActionResult Subsanado(long id, UsuarioInfo user)
        {
            try
            {/*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Subsanar";
                objauditoria.Pantalla = "Comunicado";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/


                _AuditoriaService.CambiarEstado(id, (short)EstadoExpediente.Subsanado, user.EntidadId);
                /////*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /////*auditoria Grabar*/


                List<NotificacionExpedientes> objnot = _NotificacionExpedientesService.GetByListaExpedinte(id);

                foreach (var j in objnot)
                {
                    NotificacionExpedientes objnotExp = _NotificacionExpedientesService.GetByoneExpedinte(id, j.Nivel);
                    objnotExp.FecEnvio = DateTime.Now;
                    objnotExp.FecUltimaRevision = null;
                    objnotExp.DiasTranscurrido = 0;
                    objnotExp.EstadoExpediente = (short)EstadoExpediente.Subsanado;
                    objnotExp.Tomar = 0;
                    objnotExp.RevEstado = 0; // Pendiente
                    objnotExp.RevInfCdno = 0; // Sin Acción
                    objnotExp.RevDatosGenerales = 0; // Sin Acción
                    objnotExp.RevSutentoTecnico = 0; // Sin Acción
                    objnotExp.RevTablaAsme = 0; // Sin Acción
                    objnotExp.RevSustentoCosto = 0; // Sin Acción
                    objnotExp.UserCreacion = user.NroDocumento;
                    objnotExp.UserModificacion = user.NroDocumento;
                    objnotExp.FecCreacion = DateTime.Now;
                    objnotExp.FecModificacion = DateTime.Now;

                    _NotificacionExpedientesService.Save(objnotExp);
                    /*auditoria Grabar*/
                }

                //_AuditoriaService.ActualizarSubsanar(id);
                /*auditoria Grabar*/
                string destinatario = "";
                string NombreEntidadDestinatario = "";
                string NombreEntidad = user.NomEntidad;

                Expediente objexp = _expedienteService.GetOne(id);


                string Expediente = objexp.Codigo;

                Entidad listminis = new Entidad();
                if (user.Provincia == 0 && objexp.EstadoEvaluadorMinisterio != EstadoExpediente.Aprobado)
                {

                    listminis = _entidadService.GetOneMin(user.Sector);
                }
                else if (objexp.EstadoEvaluadorMinisterio == EstadoExpediente.Aprobado && objexp.EstadoEvaluadorPCM != EstadoExpediente.Aprobado)
                {
                    listminis = _entidadService.GetOne(4105);
                }
                else if (objexp.EstadoEvaluadorMinisterio == EstadoExpediente.Aprobado && objexp.EstadoEvaluadorPCM == EstadoExpediente.Aprobado)
                {
                    listminis = _entidadService.GetOne(18497);
                }
                else
                {
                    listminis = _entidadService.GetOneProvincia(user.Provincia);
                }


                if (listminis != null)
                {
                    NombreEntidadDestinatario = listminis.Nombre;
                    List<Usuario> objent = _usuarioService.GetByEntidad(listminis.EntidadId).Where(x => x.Estado == EstadoUsuario.Activo && x.ActivarCorreo == 1).ToList();

                    if (objent.Count() != 0)
                    {
                        foreach (var j in objent)
                        {
                            if (destinatario == "")
                            {
                                //destinatario = j.MiembroEquipo.Correo + "," + user.Correo;
                                destinatario = j.MiembroEquipo.Correo;
                            }
                            else
                            {
                                //destinatario = destinatario + "," + j.MiembroEquipo.Correo + "," + user.Correo;
                                destinatario = destinatario + "," + j.MiembroEquipo.Correo;
                            }
                        }

                        Notificacion presentarEnvio = _notificacionService.GetByone((short)Correo.SustentarEnvio);
                        EnviarCorreo(user.Correo, destinatario, presentarEnvio.CCO, presentarEnvio.Asunto, presentarEnvio.Descripcion.ToString().Replace("[NombreEntidad]", NombreEntidad).Replace("\n", "<br />").Replace("[Expediente]", Expediente).Replace("[NombreEntidadDestinatario]", NombreEntidadDestinatario));

                        //Notificacion presentaRecibido = _notificacionService.GetByone((short)Correo.SustentarRecibido);
                        //EnviarCorreo(user.Correo, user.Correo, presentaRecibido.CCO, presentaRecibido.Asunto, presentaRecibido.Descripcion.ToString().Replace("[NombreEntidad]", NombreEntidad).Replace("\n", "<br />").Replace("[Expediente]", Expediente).Replace("[NombreEntidadDestinatario]", NombreEntidadDestinatario));

                    }
                }




                return Json(new { valid = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }


        public ActionResult Observar(long id, UsuarioInfo user)
        {
            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Observar";
                objauditoria.Pantalla = "Observar";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                /*lista de expedientes*/
                //List<Observacion> lista = _observacionService.GetByExpedienteListapro(id);

                //foreach (Observacion l in lista)
                //{
                //    _procedimientoService.CambiarEstadoSustento(TipoSustento.DatosGenerales, l.ProcedimientoId, false);
                //    if (l.Pantalla == "Sustento Tecnico" || l.Pantalla == "Tabla Asme")
                //    {
                //        //_procedimientoService.CambiarEstadoSustento(TipoSustento.SustentoTecnico, l.ProcedimientoId, false);
                //        //_procedimientoService.CambiarEstadoSustento(TipoSustento.TablaAsme, l.ProcedimientoId, false);
                //    }
                //}
                /*Fin*/
                _expedienteService.CambiarEstadoEvaluadores(id, EstadoExpediente.Observado, user.Rol, user.EntidadId, (short)user.EstadoMinistrio);


                Expediente objexp = _expedienteService.GetOne(id);
                // se comento para que no se abra costos
                //objexp.SustCostosTerminado = false;
                _expedienteService.Save(objexp);

                Observacion filtro = new Observacion();
                filtro.ExpedienteId = id;
                List<Observacion> observa = _observacionService.GetOneGlobalObservacion(filtro);

                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/

                int nivel = 1;
                if (user.EntidadId == 4105)
                {
                    nivel = 2;
                }
                else if (user.EntidadId == 18497)
                {
                    nivel = 3;
                }
                /*auditoria Grabar*/
                NotificacionExpedientes objnot = _NotificacionExpedientesService.GetByoneExpedinte(id, nivel);

                DateTime Ini;
                DateTime Fin;
                int dias = 0;

                Ini = objnot.FecEnvio.Value.Date;
                Fin = DateTime.Now.Date;
                while (Ini != Fin)
                {
                    if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                    {
                        dias = dias + 1;
                    }
                    Ini = Ini.AddDays(1);
                }

                objnot.DiasTranscurrido = dias;

                objnot.EstadoExpediente = (short)EstadoExpediente.Observado;
                objnot.Tomar = 2;
                objnot.RevEstado = 2; // Pendiente 

                _NotificacionExpedientesService.Save(objnot);
                /*auditoria Grabar*/
                _AuditoriaService.ActualizarObservacion(id);
                /*auditoria Grabar*/
                string destinatario = "";
                string NombreEntidadDestinatario = "";
                string NombreEntidad = user.NomEntidad;
                string Expediente = objexp.Codigo;

                Entidad listminis = new Entidad();

                listminis = _entidadService.GetOne(objexp.EntidadId);


                if (listminis != null)
                {

                    List<Usuario> objent = _usuarioService.GetByEntidad(objexp.EntidadId).Where(x => x.Estado == EstadoUsuario.Activo && x.ActivarCorreo == 1).ToList();
                    NombreEntidadDestinatario = listminis.Nombre;
                    if (objent.Count() != 0)
                    {
                        foreach (var j in objent)
                        {
                            if (destinatario == "")
                            {
                                //destinatario = j.MiembroEquipo.Correo + "," + user.Correo;

                                destinatario = j.MiembroEquipo.Correo ;
                            }
                            else
                            {
                                //destinatario = destinatario + "," + j.MiembroEquipo.Correo ;
                                destinatario = destinatario + "," + j.MiembroEquipo.Correo;

                            }
                        }

                        Notificacion presentarEnvio = _notificacionService.GetByone((short)Correo.ObservarEnvio);
                        EnviarCorreo(user.Correo, destinatario, presentarEnvio.CCO, presentarEnvio.Asunto, presentarEnvio.Descripcion.ToString().Replace("[NombreEntidad]", NombreEntidad).Replace("\n", "<br />").Replace("[Expediente]", Expediente).Replace("[NombreEntidadDestinatario]", NombreEntidadDestinatario));

                        //Notificacion presentaRecibido = _notificacionService.GetByone((short)Correo.ObservarRecibido);
                        //EnviarCorreo(user.Correo, user.Correo, presentaRecibido.CCO, presentaRecibido.Asunto, presentaRecibido.Descripcion.ToString().Replace("[NombreEntidad]", NombreEntidad).Replace("\n", "<br />").Replace("[Expediente]", Expediente).Replace("[NombreEntidadDestinatario]", NombreEntidadDestinatario));

                    }
                }


                return Json(new { valid = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        public ActionResult Aprobar(long id, UsuarioInfo user)
        {
            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Aprobar";
                objauditoria.Pantalla = "Expediente";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                //_expedienteService.CambiarEstado(id, EstadoExpediente.Aprobado);
                _expedienteService.CambiarEstadoEvaluadores(id, EstadoExpediente.Aprobado, user.Rol, user.EntidadId, (short)user.EstadoMinistrio);
                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
                int nivel = 1;
                long idpro = 0;

                Expediente objexp = _expedienteService.GetOne(id);

                if (user.EntidadId == 4105)
                {
                    nivel = 2;
                }
                else if (user.EntidadId == 18497)
                {
                    nivel = 3;
                }

                /*reset Inicio*/
                //Expediente objexp = _expedienteService.GetOne(id);
                objexp.ObsCOSTO = 0;
                objexp.ObsCP = 0;
                objexp.ObsCMF = 0;
                objexp.ObsCSI = 0;
                objexp.ObsCMNF = 0;
                objexp.ObsCST = 0;
                objexp.ObsCDA = 0;
                objexp.ObsCF = 0;
                objexp.ObsVI = 0;
                objexp.ObsIAC = 0;
                objexp.ObsCI = 0;
                _expedienteService.Save(objexp);


                if (user.Rol == (short)Rol.Evaluador || user.Rol == (short)Rol.Ratificador || user.Rol == (short)Rol.EntidadFiscalizadora)
                {
                    //List<Observacion> lstobser = _observacionService.GetlstentidadExp(user.EntidadId, id);
                    //foreach (Observacion l in lstobser)
                    //{
                    //    Observacion reset = _observacionService.GetOne(l.ObservacionId);
                    //    reset.Estado = "0";
                    //    reset.CodValidacion = "5";
                    //    reset.UsuarioModifica = user.NombreCompleto;
                    //    reset.UserModificacion = user.NroDocumento;
                    //    reset.FecModificacion = DateTime.Now;
                    //    _observacionService.Save(reset);
                    //    if (l.ProcedimientoId != 0)
                    //    {
                    //        if (idpro != l.ProcedimientoId)
                    //        {
                    //            _procedimientoService.ResetObsEstado(l.ProcedimientoId, 0);
                    //            idpro = l.ProcedimientoId;
                    //        }
                    //    }
                    //}

                    _AuditoriaService.LimpiarObs_Expediente(id, user.NombreCompleto, user.NroDocumento);


                }
                NotificacionExpedientes objnotAct = _NotificacionExpedientesService.GetByoneExpedinte(id, nivel);

                if (objnotAct != null)
                {
                    if (objnotAct.EntidadId == user.EntidadId)
                    {
                        objnotAct.Estado = 1;
                        _NotificacionExpedientesService.Save(objnotAct);
                    }
                }
                else if (nivel == 2)
                {
                    objnotAct.Estado = 1;
                    _NotificacionExpedientesService.Save(objnotAct);
                }
                else if (nivel == 3)
                {
                    objnotAct.Estado = 1;
                    _NotificacionExpedientesService.Save(objnotAct);
                }

                /*reset Fin*/

                if ((short)user.EstadoMinistrio != 1)
                {
                    /*Actualizar Grabar*/
                    NotificacionExpedientes objnot = _NotificacionExpedientesService.GetByoneExpedinte(id, nivel);
                    DateTime Ini;
                    DateTime Fin;
                    int dias = 0;

                    Ini = objnot.FecEnvio.Value.Date;
                    Fin = DateTime.Now.Date;
                    while (Ini != Fin)
                    {
                        if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                        {
                            dias = dias + 1;
                        }
                        Ini = Ini.AddDays(1);
                    }

                    objnot.DiasTranscurrido = dias;
                    objnot.Estado = 3;

                    objnot.EstadoExpediente = (short)EstadoExpediente.Aprobado;
                    objnot.Tomar = 2;// en proceso 0 , detener 1, terminara 2
                    objnot.RevEstado = 2; // terminado 2 , sin accion 0, en proceso 1, detenido 3 

                    _NotificacionExpedientesService.Save(objnot);
                }
                else if (nivel == 1)
                {
                    NotificacionExpedientes objnot = _NotificacionExpedientesService.GetByoneExpedinte(id, nivel);
                    DateTime Ini;
                    DateTime Fin;
                    int dias = 0;
                    if (objnot != null)
                    {
                        Ini = objnot.FecEnvio.Value.Date;
                        Fin = DateTime.Now.Date;
                        while (Ini != Fin)
                        {
                            if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                            {
                                dias = dias + 1;
                            }
                            Ini = Ini.AddDays(1);
                        }

                        objnot.DiasTranscurrido = dias;
                        objnot.Estado = 3;

                        objnot.EstadoExpediente = (short)EstadoExpediente.Aprobado;
                        objnot.Tomar = 2;// en proceso 0 , detener 1, terminara 2
                        objnot.RevEstado = 2; // terminado 2 , sin accion 0, en proceso 1, detenido 3 

                        _NotificacionExpedientesService.Save(objnot);
                    }
                }
                else if (nivel == 2)
                {
                    NotificacionExpedientes objnot = _NotificacionExpedientesService.GetByoneExpedinte(id, nivel);
                    DateTime Ini;
                    DateTime Fin;
                    int dias = 0;
                    if (objnot != null)
                    {
                        Ini = objnot.FecEnvio.Value.Date;
                        Fin = DateTime.Now.Date;
                        while (Ini != Fin)
                        {
                            if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                            {
                                dias = dias + 1;
                            }
                            Ini = Ini.AddDays(1);
                        }

                        objnot.DiasTranscurrido = dias;
                        objnot.Estado = 3;

                        objnot.EstadoExpediente = (short)EstadoExpediente.Aprobado;
                        objnot.Tomar = 2;// en proceso 0 , detener 1, terminara 2
                        objnot.RevEstado = 2; // terminado 2 , sin accion 0, en proceso 1, detenido 3 

                        _NotificacionExpedientesService.Save(objnot);
                    }
                }


                /*auditoria Grabar*/
                if (user.EntidadId != 4105 && user.Rol != (short)Rol.Ratificador && objexp.EstadoEvaluadorPCM != EstadoExpediente.Aprobado)
                {
                    /*Insertar Grabar 1*/
                    NotificacionExpedientes objnotent = new NotificacionExpedientes();
                    objnotent.ExpedienteId = id;
                    objnotent.EntidadId = objexp.EntidadId;
                    objnotent.SectorId = objexp.Entidad.SectorId;
                    objnotent.ProvinciaId = user.Provincia;

                    objnotent.FecEnvio = DateTime.Now;
                    objnotent.DiasTranscurrido = 0;
                    objnotent.EstadoExpediente = (short)EstadoExpediente.Presentado;
                    objnotent.Tomar = 0;
                    objnotent.Nivel = 2;
                    objnotent.RevEstado = 0; // Pendiente
                    objnotent.RevInfCdno = 0; // Sin Acción
                    objnotent.RevDatosGenerales = 0; // Sin Acción
                    objnotent.RevSutentoTecnico = 0; // Sin Acción
                    objnotent.RevTablaAsme = 0; // Sin Acción
                    objnotent.RevSustentoCosto = 0; // Sin Acción
                    objnotent.UserCreacion = user.NroDocumento;
                    objnotent.UserModificacion = user.NroDocumento;
                    objnotent.FecCreacion = DateTime.Now;
                    objnotent.FecModificacion = DateTime.Now;

                    _NotificacionExpedientesService.Save(objnotent);
                    /*auditoria Grabar*/

                    /*auditoria Grabar*/
                }
                else if (user.EntidadId == 4105 && user.Rol != (short)Rol.Ratificador && objexp.EstadoEvaluadorPCM == EstadoExpediente.Aprobado)
                {
                    /*Insertar Grabar 2*/
                    NotificacionExpedientes objnotent2 = new NotificacionExpedientes();
                    objnotent2.ExpedienteId = id;
                    objnotent2.EntidadId = objexp.EntidadId;
                    objnotent2.SectorId = objexp.Entidad.SectorId;
                    objnotent2.ProvinciaId = user.Provincia;

                    objnotent2.FecEnvio = DateTime.Now;
                    objnotent2.DiasTranscurrido = 0;
                    objnotent2.EstadoExpediente = (short)EstadoExpediente.Aprobado;
                    objnotent2.Tomar = 0;
                    objnotent2.Nivel = 3;
                    objnotent2.RevEstado = 0; // Pendiente
                    objnotent2.RevInfCdno = 0; // Sin Acción
                    objnotent2.RevDatosGenerales = 0; // Sin Acción
                    objnotent2.RevSutentoTecnico = 0; // Sin Acción
                    objnotent2.RevTablaAsme = 0; // Sin Acción
                    objnotent2.RevSustentoCosto = 0; // Sin Acción
                    objnotent2.UserCreacion = user.NroDocumento;
                    objnotent2.UserModificacion = user.NroDocumento;
                    objnotent2.FecCreacion = DateTime.Now;
                    objnotent2.FecModificacion = DateTime.Now;

                    _NotificacionExpedientesService.Save(objnotent2);
                    /*auditoria Grabar*/
                }

                else if (user.EntidadId != 18497 && user.Rol != (short)Rol.Ratificador && objexp.EstadoEvaluadorPCM != EstadoExpediente.Aprobado)
                {
                    /*Insertar Grabar 2*/
                    NotificacionExpedientes objnotent2 = new NotificacionExpedientes();
                    objnotent2.ExpedienteId = id;
                    objnotent2.EntidadId = objexp.EntidadId;
                    objnotent2.SectorId = objexp.Entidad.SectorId;
                    objnotent2.ProvinciaId = user.Provincia;

                    objnotent2.FecEnvio = DateTime.Now;
                    objnotent2.DiasTranscurrido = 0;
                    objnotent2.EstadoExpediente = (short)EstadoExpediente.Aprobado;
                    objnotent2.Tomar = 0;
                    objnotent2.Nivel = 3;
                    objnotent2.RevEstado = 0; // Pendiente
                    objnotent2.RevInfCdno = 0; // Sin Acción
                    objnotent2.RevDatosGenerales = 0; // Sin Acción
                    objnotent2.RevSutentoTecnico = 0; // Sin Acción
                    objnotent2.RevTablaAsme = 0; // Sin Acción
                    objnotent2.RevSustentoCosto = 0; // Sin Acción
                    objnotent2.UserCreacion = user.NroDocumento;
                    objnotent2.UserModificacion = user.NroDocumento;
                    objnotent2.FecCreacion = DateTime.Now;
                    objnotent2.FecModificacion = DateTime.Now;

                    _NotificacionExpedientesService.Save(objnotent2);
                    /*auditoria Grabar*/
                }

                /*auditoria Grabar*/
                string destinatario = "";
                string NombreEntidadDestinatario = "";
                string NombreEntidad = user.NomEntidad;
                string Expediente = objexp.Codigo;

                Entidad listminis = new Entidad();

                listminis = _entidadService.GetOne(objexp.EntidadId);


                if (listminis != null)
                {

                    List<Usuario> objent = _usuarioService.GetByEntidad(objexp.EntidadId).Where(x => x.Estado == EstadoUsuario.Activo && x.ActivarCorreo == 1).ToList();
                    NombreEntidadDestinatario = listminis.Nombre;
                    if (objent.Count() != 0)
                    {
                        foreach (var j in objent)
                        {
                            if (destinatario == "")
                            {
                                //destinatario = j.MiembroEquipo.Correo + "," + user.Correo;
                                destinatario = j.MiembroEquipo.Correo;
                            }
                            else
                            {
                                //destinatario = destinatario + "," + j.MiembroEquipo.Correo + "," + user.Correo;
                                destinatario = destinatario + "," + j.MiembroEquipo.Correo;
                            }
                        }

                        Notificacion presentarEnvio = _notificacionService.GetByone((short)Correo.AprobarEnvio);
                        EnviarCorreo(user.Correo, destinatario, presentarEnvio.CCO, presentarEnvio.Asunto, presentarEnvio.Descripcion.ToString().Replace("[NombreEntidad]", NombreEntidad).Replace("\n", "<br />").Replace("[Expediente]", Expediente).Replace("[NombreEntidadDestinatario]", NombreEntidadDestinatario));

                        //Notificacion presentaRecibido = _notificacionService.GetByone((short)Correo.AprobarRecibido);
                        //EnviarCorreo(user.Correo, user.Correo, presentaRecibido.CCO, presentaRecibido.Asunto, presentaRecibido.Descripcion.ToString().Replace("[NombreEntidad]", NombreEntidad).Replace("\n", "<br />").Replace("[Expediente]", Expediente).Replace("[NombreEntidadDestinatario]", NombreEntidadDestinatario));

                        if (user.EntidadId == 4105)
                        {
                            List<Usuario> objentMef = _usuarioService.GetByEntidad(18497).Where(x => x.Estado == EstadoUsuario.Activo && x.ActivarCorreo == 1).ToList();
                            string NombreEntidad2 = objentMef[0].Entidad.Nombre;
                            destinatario = "";
                            foreach (var j in objentMef)
                            {
                                if (destinatario == "")
                                {
                                    //destinatario = j.MiembroEquipo.Correo + "," + user.Correo;
                                    destinatario = j.MiembroEquipo.Correo;
                                }
                                else
                                {
                                    //destinatario = destinatario + "," + j.MiembroEquipo.Correo + "," + user.Correo;
                                    destinatario = destinatario + "," + j.MiembroEquipo.Correo;
                                }
                            }

                            Notificacion presentarEnvio2 = _notificacionService.GetByone((short)Correo.PresentarEnvio);
                            EnviarCorreo(user.Correo, destinatario, presentarEnvio2.CCO, presentarEnvio2.Asunto, presentarEnvio2.Descripcion.ToString().Replace("[NombreEntidad]", NombreEntidad2).Replace("\n", "<br />").Replace("[Expediente]", Expediente).Replace("[NombreEntidadDestinatario]", NombreEntidadDestinatario));

                        }

                    }
                }

                /// actualizar estados
                /// 
                //_AuditoriaService.PresentarAprobar(id);

                return Json(new { valid = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        public ActionResult AprobarEstandar(long id, UsuarioInfo user)
        {
            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Aprobar";
                objauditoria.Pantalla = "Expediente";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                //_expedienteService.CambiarEstado(id, EstadoExpediente.Aprobado);

                _AuditoriaService.CambiarEstado(id, (short)EstadoExpediente.Aprobado, user.EntidadId);
                //_expedienteService.CambiarEstadoEvaluadores(id, EstadoExpediente.Aprobado, user.Rol, user.EntidadId, (short)user.EstadoMinistrio);
                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/

                return Json(new { valid = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        public ActionResult TupaVigente(UsuarioInfo user)
        {
            try
            {
                Tupa vigente = _tupaService.GetTupaVigenteByEntidad(user.EntidadId);

                if (vigente == null)
                {
                    ModelState.AddModelError("", "La entidad no cuenta con un TUPA Vigente.");
                    return View("_Error");
                }

                return RedirectToAction("Editar", "Expediente", new { area = "Simplificacion", id = vigente.ExpedienteId });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                ModelState.AddModelError("", ex.Message);
                return View("_Error");
            }
        }

        [HttpPost]
        public bool EditarExpediente(long id, string comentario, UsuarioInfo user)
        {
            Expediente model = new Expediente();
            try
            {
                model = _expedienteService.GetOne(id);
                model.Descripcion = comentario;

                _expedienteService.Save(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("editarExpediente", ex.Message);
                _log.Error(ex);
            }
            return true;
        }


        [HttpPost]
        public bool ObservarTotal(long id, long Num, UsuarioInfo user)
        {
            Expediente model = new Expediente();
            try
            {
                _AuditoriaService.ObservarTtotal(id, Num);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("editarExpediente", ex.Message);
                _log.Error(ex);
            }
            return true;
        }
        [HttpPost]
        public bool ValidarTotal(long id, long Num, UsuarioInfo user)
        {
            Expediente model = new Expediente();
            try
            {
                _AuditoriaService.ValidarTotal(id, Num);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("editarExpediente", ex.Message);
                _log.Error(ex);
            }
            return true;
        }


        [HttpPost]
        public bool ordenarascdesc(long id, int order, UsuarioInfo user)
        {
            Expediente model = new Expediente();
            try
            {


                List<Procedimiento> lista = _procedimientoService.GetByExpedienteNumero(id);

                if (lista.Count == 0)
                {

                    return false;
                }
                else
                {
                    model = _expedienteService.GetOne(id);
                    model.Ascendente = order;

                    _expedienteService.Save(model);
                }


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("editarExpediente", ex.Message);
                _log.Error(ex);
            }
            return true;
        }


        [HttpPost]
        public bool Editarorden(long id, string num, UsuarioInfo user)
        {
            Expediente model = new Expediente();
            try
            {

                List<Procedimiento> lista = _procedimientoService.GetByExpedienteNumero(id);

                if (lista.Count == 0)
                {

                    return false;
                }
                else
                {
                    model = _expedienteService.GetOne(id);
                    model.EditarNum = num;


                    _expedienteService.Save(model);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("editarExpediente", ex.Message);
                _log.Error(ex);
            }
            return true;
        }


        [HttpPost]
        public bool AgregarObservacion(Observacion filtro, UsuarioInfo user)
        {

            try
            {

                filtro.EntidadId = user.EntidadId;

                Observacion lista = _observacionService.GetOneGlobal(filtro);
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Agregar Nota Expediente";
                objauditoria.Pantalla = "Expediente";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/


                if (lista != null)
                {

                    filtro.ObservacionId = lista.ObservacionId;
                }


                _AuditoriaService.AgregarObservacion(filtro, 1, user.NombreCompleto, user.NroDocumento, user.EntidadId);

                //if (lista != null)
                //{
                //    Observacion reset = _observacionService.GetOne(lista.ObservacionId);
                //    //reset.Estado = "0";
                //    reset.UsuarioModifica = user.NombreCompleto;
                //    reset.UserModificacion = user.NroDocumento;
                //    reset.FecModificacion = DateTime.Now; 
                //    reset.Descripcion = filtro.Descripcion;
                //    _observacionService.Save(reset);

                //}
                //else { 
                //filtro.UsuarioCreacion = user.NombreCompleto;
                //filtro.UserCreacion = user.NroDocumento;
                //filtro.FecCreacion = DateTime.Now;
                //    _observacionService.Save(filtro);
                //}

                //Expediente objexp = _expedienteService.GetOne(filtro.ExpedienteId);


                //if (filtro.Pantalla == "Costo Personal")
                //{
                //    objexp.ObsCP = 1;
                //    objexp.ObsCOSTO = 1;
                //    objexp.ObsCI = 1;

                //    objexp.ObsCPH = 1;
                //    objexp.ObsCOSTOH = 1;
                //    objexp.ObsCIH = 1;
                //    _expedienteService.Save(objexp);
                //}
                //else if (filtro.Pantalla == "CostosdeMaterialFungible")
                //{
                //    objexp.ObsCMF = 1;
                //    objexp.ObsCOSTO = 1;
                //    objexp.ObsCI = 1;


                //    objexp.ObsCMFH = 1;
                //    objexp.ObsCOSTOH = 1;
                //    objexp.ObsCIH = 1;
                //    _expedienteService.Save(objexp);
                //}
                //else if (filtro.Pantalla == "CostosdeServiciosIdentificables")
                //{

                //    objexp.ObsCSI = 1;
                //    objexp.ObsCOSTO = 1;
                //    objexp.ObsCI = 1;


                //    objexp.ObsCSIH = 1;
                //    objexp.ObsCOSTOH = 1;
                //    objexp.ObsCIH = 1;
                //    _expedienteService.Save(objexp);
                //}
                //else if (filtro.Pantalla == "CostosdeMaterialNoFungible")
                //{

                //    objexp.ObsCMNF = 1;
                //    objexp.ObsCOSTO = 1;
                //    objexp.ObsCI = 1;

                //    objexp.ObsCMNFH = 1;
                //    objexp.ObsCOSTOH = 1;
                //    objexp.ObsCIH = 1;
                //    _expedienteService.Save(objexp);
                //}
                //else if (filtro.Pantalla == "CostosdeServiciosdeTerceros")
                //{

                //    objexp.ObsCST = 1;
                //    objexp.ObsCOSTO = 1;
                //    objexp.ObsCI = 1;

                //    objexp.ObsCSTH = 1;
                //    objexp.ObsCOSTOH = 1;
                //    objexp.ObsCIH = 1;
                //    _expedienteService.Save(objexp);
                //}
                //else if (filtro.Pantalla == "CostosdeDepreciacionyAmortizacion")
                //{

                //    objexp.ObsCDA = 1;
                //    objexp.ObsCOSTO = 1;
                //    objexp.ObsCI = 1;

                //    objexp.ObsCDAH = 1;
                //    objexp.ObsCOSTOH = 1;
                //    objexp.ObsCIH = 1;
                //    _expedienteService.Save(objexp);
                //}
                //else if (filtro.Pantalla == "CostosFijos")
                //{

                //    objexp.ObsCF = 1;
                //    objexp.ObsCOSTO = 1;
                //    objexp.ObsCI = 1;

                //    objexp.ObsCFH = 1;
                //    objexp.ObsCOSTOH = 1;
                //    objexp.ObsCIH = 1;
                //    _expedienteService.Save(objexp);
                //}
                //else if (filtro.Pantalla == "Configuracion del Valor de Inductor")
                //{

                //    objexp.ObsVI = 1;
                //    objexp.ObsCOSTO = 1;
                //    objexp.ObsCI = 1;

                //    objexp.ObsVIH = 1;
                //    objexp.ObsCOSTOH = 1;
                //    objexp.ObsCIH = 1;
                //    _expedienteService.Save(objexp);
                //}
                //else if (filtro.Pantalla == "Configuracion del Inductor de Asignacion de Costos")
                //{

                //    objexp.ObsIAC = 1;
                //    objexp.ObsCOSTO = 1;
                //    objexp.ObsCI = 1;

                //    objexp.ObsIACH = 1;
                //    objexp.ObsCOSTOH = 1;
                //    objexp.ObsCIH = 1;
                //    _expedienteService.Save(objexp);
                //} 
                //else if (filtro.Pantalla == "Costo Resumen")
                //{
                //    objexp.ObsCOSTO = 1;
                //    objexp.ObsCOSTOH = 1;
                //    _expedienteService.Save(objexp);
                //}
                //else
                //{
                //    _procedimientoService.CambiarObsEstado(filtro.ProcedimientoId, filtro.Pantalla, 1);
                //}

                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("editarExpediente", ex.Message);
                _log.Error(ex);
            }
            return true;
        }

        [HttpPost]
        public bool AgregarObservacionMasivo(Observacion filtro, int codval, string texto1, string texto2, UsuarioInfo user)
        {

            try
            {


                if (codval == 1)
                {
                    filtro.NombreCampo = "Objetivo";
                }
                if (codval == 2)
                {
                    filtro.NombreCampo = "Calificacion";
                }
                if (codval == 3)
                {
                    filtro.NombreCampo = "PlazoAtencion";
                }
                if (codval == 4)
                {
                    filtro.NombreCampo = "Baselegal";
                }
                if (codval == 5)
                {
                    filtro.NombreCampo = "PlazoAtencion";
                }
                if (codval == 6)
                {
                    filtro.NombreCampo = "Plazorenovacion";
                }
                if (codval == 7)
                {
                    filtro.NombreCampo = "Reconsideracion";
                }
                if (codval == 8)
                {
                    filtro.NombreCampo = "Apelacion";
                }



                if (filtro.CodValidacion == "2")
                {
                    filtro.Descripcion = texto2;
                }
                else
                {
                    Procedimiento procelist = _procedimientoService.GetOne(filtro.ProcedimientoId);
                    if (codval == 1)
                    {
                        filtro.Descripcion = procelist.Objetivo;
                    }
                    if (codval == 2)
                    {

                        if (procelist.Calificacion == CalificacionProcedimiento.Ninguno)
                        {

                            filtro.Descripcion = "Ninguno";
                        }
                        if (procelist.Calificacion == CalificacionProcedimiento.Automatica)
                        {

                            filtro.Descripcion = "Aprobación Automática";
                        }
                        if (procelist.Calificacion == CalificacionProcedimiento.SilencioPositivo)
                        {

                            filtro.Descripcion = "Evaluación Previa con Silencio Positivo";
                        }
                        if (procelist.Calificacion == CalificacionProcedimiento.SilencioNegativo)
                        {
                            filtro.Descripcion = "Evaluación Previa con Silencio Negativo";
                        }

                    }
                    if (codval == 3)
                    {
                        filtro.Descripcion = Convert.ToString(procelist.PlazoAtencion);
                    }
                    if (codval == 4)
                    {
                        filtro.Descripcion = procelist.SustentoPlazo;
                    }
                    if (codval == 5)
                    {
                        filtro.Descripcion = Convert.ToString(procelist.FrecuenciaRenovacion);
                    }
                    if (codval == 6)
                    {
                        filtro.Descripcion = texto1;
                    }
                    if (codval == 7)
                    {
                        string nombre = procelist.UndOrgReconsideracion.Nombre;
                        filtro.Descripcion = procelist.CargoReconsideracion + ";" + nombre + ";" + procelist.PzoReconPresent + ";" + procelist.PzoReconResol;
                    }
                    if (codval == 8)
                    {
                        string nombre = procelist.UndOrgApelacion.Nombre;
                        filtro.Descripcion = procelist.CargoApelacion + ";" + nombre + ";" + procelist.PzoApelPresent + ";" + procelist.PzoApelResol;
                    }
                    //procelist.ObsDG = 1;

                }

                filtro.EntidadId = user.EntidadId;
                Observacion lista = _observacionService.GetOneGlobal(filtro);
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Agregar Observacion Masivo";
                objauditoria.Pantalla = "Observacion";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/

                if (lista != null)
                {
                    Observacion reset = _observacionService.GetOne(lista.ObservacionId);
                    //reset.Estado = "0";
                    filtro.UsuarioModifica = user.NombreCompleto;
                    reset.UserModificacion = user.NroDocumento;
                    reset.FecModificacion = DateTime.Now;
                    reset.Descripcion = filtro.Descripcion;
                    _observacionService.Save(reset);
                }
                else
                {
                    filtro.UsuarioCreacion = user.NombreCompleto;
                    filtro.UserCreacion = user.NroDocumento;
                    filtro.FecCreacion = DateTime.Now;
                    _observacionService.Save(filtro);
                }

                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/




            }
            catch (Exception ex)
            {
                ModelState.AddModelError("editarExpediente", ex.Message);
                _log.Error(ex);
            }
            return true;
        }

        [HttpPost]
        public bool CancelarObservacion(Observacion filtro, UsuarioInfo user)
        {

            try
            {

                filtro.EntidadId = user.EntidadId;
                Observacion lista = _observacionService.GetOneGlobal(filtro);

                if (lista != null)
                {
                    Observacion reset = _observacionService.GetOne(lista.ObservacionId);
                    reset.Estado = "0";
                    reset.CodValidacion = "5";
                    reset.UserModificacion = user.NombreCompleto;
                    reset.UserModificacion = user.NroDocumento;
                    reset.FecModificacion = DateTime.Now;
                    reset.EntidadId = user.EntidadId;
                    reset.Descripcion = filtro.Descripcion;
                    _observacionService.Save(reset);
                }
                //filtro.CodValidacion = "5";
                //filtro.Estado = "0";
                //filtro.UsuarioCreacion = user.NombreCompleto;
                //filtro.UserCreacion = user.NroDocumento;
                //filtro.FecCreacion = DateTime.Now;
                //_observacionService.Save(filtro);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("editarExpediente", ex.Message);
                _log.Error(ex);
            }
            return true;
        }

        [HttpPost]
        public bool CancelarDescripcion1(Observacion filtro, UsuarioInfo user)
        {

            try
            {

                filtro.EntidadId = user.EntidadId;
                Observacion lista = _observacionService.GetOneGlobal(filtro);
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Cancelar  Observacion";
                objauditoria.Pantalla = "Expediente";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                filtro.CodValidacion = "5";
                filtro.Estado = "0";

                if (lista != null)
                {
                    filtro.ObservacionId = lista.ObservacionId;
                }



                _AuditoriaService.CancelarObservacion(filtro, 0, user.NombreCompleto, user.NroDocumento, user.EntidadId);

                //if (lista != null)
                //{
                //    //Observacion reset = _observacionService.GetOne(lista.ObservacionId);
                //    //reset.Estado = "0";
                //    //reset.UserModificacion = user.NroDocumento;
                //    //reset.FecModificacion = DateTime.Now;
                //    //reset.EntidadId = user.EntidadId;
                //    //_observacionService.Save(reset);

                //    Observacion reset = _observacionService.GetOne(lista.ObservacionId);
                //    reset.CodValidacion = "5";
                //    reset.Estado = "0";
                //    reset.UsuarioModifica = user.NombreCompleto;
                //    reset.UserModificacion = user.NroDocumento;
                //    reset.FecModificacion = DateTime.Now;
                //    reset.Descripcion = filtro.Descripcion;
                //    _observacionService.Save(reset);
                //}
                ////filtro.CodValidacion = "5";
                ////filtro.Estado = "0";
                ////filtro.UsuarioCreacion = user.NombreCompleto;
                ////filtro.UserCreacion = user.NroDocumento;
                ////filtro.FecCreacion = DateTime.Now;
                ////_observacionService.Save(filtro);


                //Expediente objexp = _expedienteService.GetOne(filtro.ExpedienteId);

                //List<Observacion> listaConunt = _observacionService.GetlstentidadTotal(filtro.EntidadId, filtro.Pantalla);

                //if (filtro.Pantalla == "Costo Personal")
                //{
                //    if (listaConunt.Count() == 0)
                //    {
                //        objexp.ObsCP = 0;
                //        _expedienteService.Save(objexp);
                //    } 
                //}
                //else if (filtro.Pantalla == "CostosdeMaterialFungible")
                //{

                //    if (listaConunt.Count() == 0)
                //    {
                //        objexp.ObsCMF = 0;
                //        _expedienteService.Save(objexp);
                //    } 
                //}
                //else if (filtro.Pantalla == "CostosdeServiciosIdentificables")
                //{
                //    if (listaConunt.Count() == 0)
                //    {
                //        objexp.ObsCSI = 0;
                //        _expedienteService.Save(objexp);
                //    } 
                //}
                //else if (filtro.Pantalla == "CostosdeMaterialNoFungible")
                //{
                //    if (listaConunt.Count() == 0)
                //    {
                //        objexp.ObsCMNF = 0;
                //        _expedienteService.Save(objexp);
                //    } 
                //}
                //else if (filtro.Pantalla == "CostosdeServiciosdeTerceros")
                //{
                //    if (listaConunt.Count() == 0)
                //    {
                //        objexp.ObsCST = 0;
                //        _expedienteService.Save(objexp);
                //    } 
                //}
                //else if (filtro.Pantalla == "CostosdeDepreciacionyAmortizacion")
                //{
                //    if (listaConunt.Count() == 0)
                //    {
                //        objexp.ObsCDA = 0;
                //        _expedienteService.Save(objexp);
                //    } 
                //}
                //else if (filtro.Pantalla == "CostosFijos")
                //{
                //    if (listaConunt.Count() == 0)
                //    {
                //        objexp.ObsCF = 0;
                //        _expedienteService.Save(objexp);
                //    } 
                //}
                //else if (filtro.Pantalla == "Configuracion del Valor de Inductor")
                //{
                //    if (listaConunt.Count() == 0)
                //    {
                //        objexp.ObsVI = 0;
                //        _expedienteService.Save(objexp);
                //    } 
                //}
                //else if (filtro.Pantalla == "Configuracion del Inductor de Asignacion de Costos")
                //{
                //    if (listaConunt.Count() == 0)
                //    {
                //        objexp.ObsIAC = 0;
                //        _expedienteService.Save(objexp);
                //    } 
                //}
                //else if (filtro.Pantalla == "Costo Resumen" )
                //{
                //    if (objexp.ObsCP == 0 && objexp.ObsCMF == 0 && objexp.ObsCSI == 0 && objexp.ObsCMNF == 0 && objexp.ObsCST == 0 && objexp.ObsCDA == 0 && objexp.ObsCF == 0 && objexp.ObsVI == 0 )
                //    {
                //        if (listaConunt.Count() == 0)
                //        {
                //            objexp.ObsCOSTO = 0;
                //            _expedienteService.Save(objexp);
                //        }
                //    }  
                //}
                //else
                //{
                //    if (listaConunt.Count() == 0)
                //    {
                //        _procedimientoService.CambiarObsEstado(filtro.ProcedimientoId, filtro.Pantalla, 0);
                //    }
                //}

                //if (objexp.ObsCP == 0 && objexp.ObsCMF == 0 && objexp.ObsCSI == 0 && objexp.ObsCMNF == 0 && objexp.ObsCST == 0 && objexp.ObsCDA == 0 && objexp.ObsCF == 0 && objexp.ObsVI == 0)
                //{
                //    objexp.ObsCI = 0;
                //    _expedienteService.Save(objexp);
                //}

                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("editarExpediente", ex.Message);
                _log.Error(ex);
            }
            return true;
        }


        [HttpPost]
        public bool EditarOcultarDes(long id, string OcultarDes, UsuarioInfo user)
        {
            Expediente model = new Expediente();
            try
            {
                model = _expedienteService.GetOne(id);
                model.OcultarDes = OcultarDes;

                _expedienteService.Save(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("EditarOcultarDes", ex.Message);
                _log.Error(ex);
            }
            return true;
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

        [HttpPost]
        public bool EditarOcultarNor(long id, string OcultarNor, UsuarioInfo user)
        {
            Expediente model = new Expediente();
            try
            {
                model = _expedienteService.GetOne(id);
                model.OcultarNor = OcultarNor;

                _expedienteService.Save(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("EditarOcultarNor", ex.Message);
                _log.Error(ex);
            }
            return true;
        }
    }
}