using Newtonsoft.Json;
using Sut.Entities;
using Sut.IApplicationServices;
using Sut.Log;
using Sut.Security;
using Sut.Security.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Sut.Web.Areas.Seguridad.Controllers
{
    [Autorizacion]
    public class PortadaController : Controller
    {

        private readonly ILogService<PortadaController> _log;
        private readonly IPreguntasFrecuentesService _PreguntasFrecuentesService;
        private readonly IComunicadoService _ComunicadoService;
        private readonly IDocumentosNormaService _DocumentosNormaService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly ITablaAsmeService _TablaAsmeService;
        private readonly IUsuarioService _usuarioService;
        private readonly IMenuService _menuService;
        private readonly IMenuayudaService _menuayudaService;
        private readonly INotificacionExpedientesService _notificacionExpedientesService;
        private readonly IFeriadosAnualesService _feriadosAnualesService;
        private readonly IMetaDatoService _metaDatoService;
        private readonly IBandejaService _bandejaService;
        private readonly IUsuarioRolService _usuarioRolService;
        Auditoria objauditoria = new Auditoria();
        public PortadaController(IUsuarioService usuarioService, IPreguntasFrecuentesService PreguntasFrecuentesService, IComunicadoService ComunicadoService,IDocumentosNormaService DocumentosNormaService,IAuditoriaService AuditoriaService, ITablaAsmeService TablaAsmeService, IMenuService MenuService, IMenuayudaService menuayudaService, INotificacionExpedientesService notificacionExpedientesService, IMetaDatoService metaDatoService, IBandejaService bandejaService, IUsuarioRolService usuarioRolService, IFeriadosAnualesService feriadosAnualesService)
        {
            _log = new LogService<PortadaController>();
            _PreguntasFrecuentesService = PreguntasFrecuentesService;
            _ComunicadoService = ComunicadoService;
            _DocumentosNormaService = DocumentosNormaService;
            _AuditoriaService = AuditoriaService;
            _TablaAsmeService = TablaAsmeService;
            _usuarioService = usuarioService;
            _menuService = MenuService;
            _menuayudaService = menuayudaService;
            _notificacionExpedientesService = notificacionExpedientesService;
            _metaDatoService = metaDatoService;
            _bandejaService = bandejaService;
            _usuarioRolService = usuarioRolService;
            _feriadosAnualesService = feriadosAnualesService; 
        }

        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Lista(UsuarioInfo user)
        {
            ViewBag.rolmultiples = System.Web.HttpContext.Current.Session["roles"];
            ViewBag.Usuario = user;

            //if (user.validrol == 0)
            //{
            //    var asd = 0;
            //    foreach (var mode in ViewBag.rolmultiples)
            //    {
            //        asd = asd + 1;
            //    }
            //    if (asd > 1)
            //    {
            //        user.MultipleRol = asd;
            //    }
            //}




            ViewBag.lstbandeja = _bandejaService.GetAllusuario(user.UsuarioId).ToList();
            return View();
        }




        [HttpPost]
        public ActionResult GenerarListamenulista(UsuarioInfo user)
        {
            try
            {

                ViewBag.menu = _menuService.GetByMenu();
                List<Menu> menu = _menuService.GetByMenu();
                List<Menuayuda> menuestado = _menuayudaService.GetByEntidad(user.EntidadId);
                //Menuayuda maximomenu = _menuayudaService.GetByEntidad(user.EntidadId).Max();


                int count = 0;
                int countayuda = 1;
                int valor = 0;
                string nombremenu = "";
                int cantidad = menu.Count;
                if (user.Rol != 2)
                {
                    valor = 4;
                    //for (var j = 0; j < valor; j++) { 
                    //    menu.Remove(menu[j]);
                    //    }
                }
                if (menuestado.Count == 0)
                {
                    foreach (Menu lmenu in menu)
                    {

                        if (count == valor)
                        {
                            lmenu.Estilo = lmenu.Estilo;
                            nombremenu = lmenu.Descripcion;
                        }
                        else
                        {
                            if (lmenu.IdPadreMenu == 0 || (lmenu.MenuId == 17 || lmenu.MenuId == 7 || lmenu.MenuId == 20 || lmenu.MenuId == 22))
                            {
                                lmenu.Estilo = "bg-light";
                            }
                            //lmenu.url = "#";
                        }
                        count++;
                    }
                }
                else
                {
                    long maxId = menuestado.Max(x => x.MenuId);
                    foreach (Menu lmenu in menu)
                    {

                        var resultadoayuda = _menuayudaService.GetByone(countayuda, user.EntidadId);
                        if (resultadoayuda != null)
                        {
                            lmenu.url = lmenu.url;

                            if (lmenu.MenuId == 7)
                            {
                                lmenu.Estilo = lmenu.Estilo;
                            }
                            else if (lmenu.MenuId == 17)
                            {
                                lmenu.Estilo = lmenu.Estilo;
                            }
                            else if (lmenu.MenuId == 20)
                            {
                                lmenu.Estilo = lmenu.Estilo;
                            }
                            else if (lmenu.MenuId == 22)
                            {
                                lmenu.Estilo = lmenu.Estilo;
                            }
                            else if (lmenu.IdPadreMenu != 0)
                            {
                                lmenu.Estilo = "";
                            }
                            else
                            {
                                lmenu.Estilo = lmenu.Estilo;
                            }
                            nombremenu = lmenu.Descripcion;
                        }
                        else
                        {
                            if (maxId + 1 == lmenu.MenuId)
                            {
                                lmenu.url = lmenu.url;
                                //if (lmenu.IdPadreMenu != 0 || (lmenu.MenuId == 17 || lmenu.MenuId == 7 || lmenu.MenuId == 20 || lmenu.MenuId == 22))
                                //{
                                //    lmenu.Estilo = "";
                                //}
                                //else
                                //{
                                lmenu.Estilo = lmenu.Estilo;
                                //}                                
                                nombremenu = lmenu.Descripcion;
                            }
                            else
                            {
                                if (lmenu.IdPadreMenu == 0 || (lmenu.MenuId == 17 || lmenu.MenuId == 7 || lmenu.MenuId == 20 || lmenu.MenuId == 22))
                                {
                                    lmenu.Estilo = "bg-light";
                                }
                                //lmenu.url = "#"; 
                            }
                        }
                        countayuda++;
                    }
                }

                return Json(new
                {
                    menu = menu,
                    cantidad = cantidad,
                    nombremenu = nombremenu
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


        public string GetAllLikePagincomunicado(Comunicado filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;



            List<Comunicado> lista = _ComunicadoService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    comunicadoID = x.ComunicadoID,
                    descripcion = x.Descripcion,
                    estado = x.Estado,
                    fecCreacion = x.FecCreacion.Value.ToString("dd/MM/yyyy")
                }),
                totalRows = totalRows
            });
        }

        public string GetAllLikePagincomunicadobaner(Comunicado filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;

            List<Comunicado> lista = _ComunicadoService.GetAllLikePaginBaner(filtro, pageIndex, pageSize, ref totalRows);

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    comunicadoID = x.ComunicadoID,
                    descripcion = x.Descripcion.ToUpper(),
                    estado = x.Estado,
                    documentosNorma = x.DocumentosNorma

                }),
                totalRows = totalRows
            });
        }

        public string GetAllLikePaginnorma(DocumentosNorma filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;
            List<DocumentosNorma> lista;
            if (user.Rol == 1)
            {
                lista = _DocumentosNormaService.GetAllLikePaginAdmin(filtro, pageIndex, pageSize, ref totalRows);
            }
            else
            {
                lista = _DocumentosNormaService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);
            }

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    documentosNormaID = x.DocumentosNormaID,
                    descripcion = x.Descripcion,
                    fechaPublicacion = x.FechaPublicacion.ToString("dd/MM/yyyy"),
                    ArchivoAdjuntoId = x.ArchivoAdjuntoId
                }),
                totalRows = totalRows
            });
        }


        public string GetAllLikePaginentidadevaluacion(NotificacionExpedientes filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;
             filtro.EntidadId = user.EntidadId;
            int rol = 0;

            DateTime Ini;
            DateTime Fin;

            MetaDato objmeta = _metaDatoService.GetOne(user.Sector);

            if ((user.Sector == 80 || user.Sector == 79) && user.Rol == 6)
            {


                filtro.ProvinciaId = user.Provincia;
                List<NotificacionExpedientes> lista = _notificacionExpedientesService.GetAllLikePaginRatificadorEval(filtro, pageIndex, pageSize, ref totalRows);

                //for (var j = 0; j < lista.Count(); j++)
                //{
                //    int dias = 0;

                //    Ini = lista[j].FecEnvio.Value.Date;
                //    Fin = DateTime.Now.Date;
                //    while (Ini != Fin)
                //    {
                //        if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                //        {
                //            dias = dias + 1;
                //        }
                //        Ini = Ini.AddDays(1);
                //    }

                //    lista[j].DiasTranscurrido = dias;
                //}


                List<DateTime> FestivosDias = new List<DateTime>();
                //listaDiasFestivos.Add(Convert.ToDateTime("24/04/2023"));


                //filtro.EstadoEvaluadorMinisterio = EstadoExpediente.Aprobado;
                List<FeriadosAnuales> listaDiasFestivos = _feriadosAnualesService.GetByLista();

                for (var j = 0; j < listaDiasFestivos.Count(); j++)
                {

                    FestivosDias.Add(Convert.ToDateTime(listaDiasFestivos[j].Fecha.Value.Date));
                }


                //Fin de lista FECHAS DE FERIADOS


                for (var j = 0; j < lista.Count(); j++)
                {
                    int dias = 0;

                    Ini = lista[j].FecEnvio.Value.Date;
                    Fin = DateTime.Now.Date;
                    while (Ini != Fin)
                    {
                        if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                        {
                            dias = dias + 1;

                            foreach (var fecha in FestivosDias)
                            {
                                if (Ini == fecha)
                                {
                                    dias = dias - 1;
                                }
                            }

                        }
                        Ini = Ini.AddDays(1);
                    }

                    lista[j].DiasTranscurrido = dias;
                }


                return JsonConvert.SerializeObject(new
                {
                    lista = lista.Select(x => new
                    {
                        NotificacionExpedientesId = x.NotificacionExpedientesId,
                        ExpedienteId = x.ExpedienteId,
                        Codigo = x.Expediente.Codigo,
                        NomEntidad = x.Entidad.Nombre,
                        SectorId = x.SectorId,
                        ProvinciaId = x.ProvinciaId,
                        FecEnvio = x.FecEnvio.Value.ToString("dd/MM/yyyy hh:mm:ss tt"),
                        DiasTranscurrido = x.DiasTranscurrido,
                        FecUltimaRevision = x.FecUltimaRevision == null ? "" : x.FecUltimaRevision.Value.ToString("dd/MM/yyyy hh:mm:ss tt"),
                        EstadoExpediente = x.EstadoExpediente,
                        Tomar = x.Tomar,
                        RevEstado = x.RevEstado,
                        RevInfCdno = x.RevInfCdno,
                        RevDatosGenerales = x.RevDatosGenerales,
                        RevSutentoTecnico = x.RevSutentoTecnico,
                        RevTablaAsme = x.RevTablaAsme,
                        RevSustentoCosto = x.RevSustentoCosto,
                        Estado = x.Estado,

                    }),
                    rol = 6,
                    totalRows = totalRows
                });

            }
            else if (user.CodPadre == "2" && user.Rol == 7 || (user.EntidadId == 4105 && user.Rol == 7) || (user.EntidadId == 18497 && user.Rol == 7) || user.Rol == 12)
            {
                if (user.EntidadId == 4105 || user.EntidadId == 18497)
                {
                    //filtro.EstadoEvaluadorMinisterio = EstadoExpediente.Aprobado;
                    List<NotificacionExpedientes> lista = _notificacionExpedientesService.GetAllLikePaginEvaluadorMEFPCMEval(filtro, pageIndex, pageSize, ref totalRows);


                    //Inicio de lista FECHAS DE FERIADOS

                    List<DateTime> FestivosDias = new List<DateTime>();
                    //listaDiasFestivos.Add(Convert.ToDateTime("24/04/2023"));


                    //filtro.EstadoEvaluadorMinisterio = EstadoExpediente.Aprobado;
                    List<FeriadosAnuales> listaDiasFestivos = _feriadosAnualesService.GetByLista();

                    for (var j = 0; j < listaDiasFestivos.Count(); j++)
                    { 
                         
                        FestivosDias.Add(Convert.ToDateTime(listaDiasFestivos[j].Fecha.Value.Date));
                    }
                     

                    //Fin de lista FECHAS DE FERIADOS


                    for (var j = 0; j < lista.Count(); j++)
                    {
                        int dias = 0;

                        Ini = lista[j].FecEnvio.Value.Date;
                        Fin = DateTime.Now.Date;
                        while (Ini != Fin)
                        {
                            if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                            {
                                dias = dias + 1;

                                foreach (var fecha in FestivosDias)
                                {
                                    if (Ini == fecha)
                                    {
                                        dias = dias - 1;
                                    }
                                }

                            }
                            Ini = Ini.AddDays(1);
                        }

                        lista[j].DiasTranscurrido = dias;
                    }

                    


                    return JsonConvert.SerializeObject(new
                    {
                        lista = lista.Select(x => new
                        {
                            NotificacionExpedientesId = x.NotificacionExpedientesId,
                            ExpedienteId = x.ExpedienteId,
                            Codigo = x.Expediente.Codigo,
                            NomEntidad = x.Entidad.Nombre,
                            SectorId = x.SectorId,
                            ProvinciaId = x.ProvinciaId,
                            FecEnvio = x.FecEnvio.Value.ToString("dd/MM/yyyy hh:mm:ss tt"),
                            DiasTranscurrido = x.DiasTranscurrido,
                            FecUltimaRevision = x.FecUltimaRevision == null ? "" : x.FecUltimaRevision.Value.ToString("dd/MM/yyyy hh:mm:ss tt"),
                            EstadoExpediente = x.EstadoExpediente,
                            Tomar = x.Tomar,
                            RevEstado = x.RevEstado,
                            RevInfCdno = x.RevInfCdno,
                            RevDatosGenerales = x.RevDatosGenerales,
                            RevSutentoTecnico = x.RevSutentoTecnico,
                            RevTablaAsme = x.RevTablaAsme,
                            RevSustentoCosto = x.RevSustentoCosto,
                            Estado = x.Estado,


                        }),
                        rol = 7,
                        totalRows = totalRows
                    });
                }
                else
                {
                    filtro.SectorId = user.Sector;
                    List<NotificacionExpedientes> lista = _notificacionExpedientesService.GetAllLikePaginEvaluadorEval(filtro, pageIndex, pageSize, ref totalRows);
                    //for (var j = 0; j < lista.Count(); j++)
                    //{
                    //    int dias = 0;

                    //    Ini = lista[j].FecEnvio.Value.Date;
                    //    Fin = DateTime.Now.Date;
                    //    while (Ini != Fin)
                    //    {
                    //        if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                    //        {
                    //            dias = dias + 1;
                    //        }
                    //        Ini = Ini.AddDays(1);
                    //    }

                    //    lista[j].DiasTranscurrido = dias;
                    //}


                    List<DateTime> FestivosDias = new List<DateTime>();
                    //listaDiasFestivos.Add(Convert.ToDateTime("24/04/2023"));


                    //filtro.EstadoEvaluadorMinisterio = EstadoExpediente.Aprobado;
                    List<FeriadosAnuales> listaDiasFestivos = _feriadosAnualesService.GetByLista();

                    for (var j = 0; j < listaDiasFestivos.Count(); j++)
                    {

                        FestivosDias.Add(Convert.ToDateTime(listaDiasFestivos[j].Fecha.Value.Date));
                    }


                    //Fin de lista FECHAS DE FERIADOS


                    for (var j = 0; j < lista.Count(); j++)
                    {
                        int dias = 0;

                        Ini = lista[j].FecEnvio.Value.Date;
                        Fin = DateTime.Now.Date;
                        while (Ini != Fin)
                        {
                            if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                            {
                                dias = dias + 1;

                                foreach (var fecha in FestivosDias)
                                {
                                    if (Ini == fecha)
                                    {
                                        dias = dias - 1;
                                    }
                                }

                            }
                            Ini = Ini.AddDays(1);
                        }

                        lista[j].DiasTranscurrido = dias;
                    }


                    return JsonConvert.SerializeObject(new
                    {
                        lista = lista.Select(x => new
                        {
                            NotificacionExpedientesId = x.NotificacionExpedientesId,
                            ExpedienteId = x.ExpedienteId,
                            Codigo = x.Expediente.Codigo,
                            NomEntidad = x.Entidad.Nombre,
                            SectorId = x.SectorId,
                            ProvinciaId = x.ProvinciaId,
                            FecEnvio = x.FecEnvio.Value.ToString("dd/MM/yyyy hh:mm:ss tt"),
                            DiasTranscurrido = x.DiasTranscurrido,
                            FecUltimaRevision = x.FecUltimaRevision == null ? "" : x.FecUltimaRevision.Value.ToString("dd/MM/yyyy hh:mm:ss tt"),
                            EstadoExpediente = x.EstadoExpediente,
                            Tomar = x.Tomar,
                            RevEstado = x.RevEstado,
                            RevInfCdno = x.RevInfCdno,
                            RevDatosGenerales = x.RevDatosGenerales,
                            RevSutentoTecnico = x.RevSutentoTecnico,
                            RevTablaAsme = x.RevTablaAsme,
                            RevSustentoCosto = x.RevSustentoCosto,
                            Estado = x.Estado,

                        }),
                        rol = 7,
                        totalRows = totalRows
                    });
                }

            }
            else
            {
                //if (user.Rol == 8)
                    //filtro.EstadoExpediente = EstadoExpediente.Publicado;
                    List<NotificacionExpedientes> lista = _notificacionExpedientesService.GetAllLikePaginFiscalizadorEval(filtro, pageIndex, pageSize, ref totalRows);
                //for (var j = 0; j < lista.Count(); j++)
                //{
                //    int dias = 0;

                //    Ini = lista[j].FecEnvio.Value.Date;
                //    Fin = DateTime.Now.Date;
                //    while (Ini != Fin)
                //    {
                //        if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                //        {
                //            dias = dias + 1;
                //        }
                //        Ini = Ini.AddDays(1);
                //    }

                //    lista[j].DiasTranscurrido = dias;
                //}


                List<DateTime> FestivosDias = new List<DateTime>();
                //listaDiasFestivos.Add(Convert.ToDateTime("24/04/2023"));


                //filtro.EstadoEvaluadorMinisterio = EstadoExpediente.Aprobado;
                List<FeriadosAnuales> listaDiasFestivos = _feriadosAnualesService.GetByLista();

                for (var j = 0; j < listaDiasFestivos.Count(); j++)
                {

                    FestivosDias.Add(Convert.ToDateTime(listaDiasFestivos[j].Fecha.Value.Date));
                }


                //Fin de lista FECHAS DE FERIADOS


                for (var j = 0; j < lista.Count(); j++)
                {
                    int dias = 0;

                    Ini = lista[j].FecEnvio.Value.Date;
                    Fin = DateTime.Now.Date;
                    while (Ini != Fin)
                    {
                        if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                        {
                            dias = dias + 1;

                            foreach (var fecha in FestivosDias)
                            {
                                if (Ini == fecha)
                                {
                                    dias = dias - 1;
                                }
                            }

                        }
                        Ini = Ini.AddDays(1);
                    }

                    lista[j].DiasTranscurrido = dias;
                }

                return JsonConvert.SerializeObject(new
                {
                    lista = lista.Select(x => new
                    {
                        NotificacionExpedientesId = x.NotificacionExpedientesId,
                        ExpedienteId = x.ExpedienteId,
                        Codigo = x.Expediente.Codigo,
                        NomEntidad = x.Entidad.Nombre,
                        SectorId = x.SectorId,
                        ProvinciaId = x.ProvinciaId,
                        FecEnvio = x.FecEnvio.Value.ToString("dd/MM/yyyy"),
                        DiasTranscurrido = x.DiasTranscurrido,
                        FecUltimaRevision = x.FecUltimaRevision == null ? "" : x.FecUltimaRevision.Value.ToString("dd/MM/yyyy"),
                        EstadoExpediente = x.EstadoExpediente,
                        Tomar = x.Tomar,
                        RevEstado = x.RevEstado,
                        RevInfCdno = x.RevInfCdno,
                        RevDatosGenerales = x.RevDatosGenerales,
                        RevSutentoTecnico = x.RevSutentoTecnico,
                        RevTablaAsme = x.RevTablaAsme,
                        RevSustentoCosto = x.RevSustentoCosto,
                        Estado = x.Estado,

                    }),
                    rol = 8,
                    totalRows = totalRows
                });
            }

        }


        public string GetAllLikePaginentidadevaluacionUsuario(NotificacionExpedientes filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;
            filtro.EntidadId = user.EntidadId;
            int rol = 0;

            DateTime Ini;
            DateTime Fin;

            MetaDato objmeta = _metaDatoService.GetOne(user.Sector);

            if ((user.Sector == 80 || user.Sector == 79) && user.Rol == 6)
            {


                List<NotificacionExpedientes> lstNotifiexp = new List<NotificacionExpedientes>();

                filtro.ProvinciaId = user.Provincia;
                List<NotificacionExpedientes> lista = _notificacionExpedientesService.GetAllLikePaginRatificadorEval(filtro, pageIndex, pageSize, ref totalRows);

                var lstbandeja = _bandejaService.GetAllusuario(user.UsuarioId);

                if (user.Bandeja == "1" && lstbandeja != null)
                {
                    foreach (Bandeja ban in lstbandeja)
                    {
                        foreach (NotificacionExpedientes lstnotexp in lista)
                        {
                            if (ban.ExpedienteId == lstnotexp.ExpedienteId)
                            {
                                lstNotifiexp.Add(lstnotexp);

                            }
                        }
                    }
                }
                else
                {

                    lstNotifiexp = lista;
                }

                for (var j = 0; j < lstNotifiexp.Count(); j++)
                {
                    int dias = 0;

                    Ini = lstNotifiexp[j].FecEnvio.Value.Date;
                    Fin = DateTime.Now.Date;
                    while (Ini != Fin)
                    {
                        if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                        {
                            dias = dias + 1;
                        }
                        Ini = Ini.AddDays(1);
                    }

                    lstNotifiexp[j].DiasTranscurrido = dias;
                }

                return JsonConvert.SerializeObject(new
                {
                    lista = lstNotifiexp.Select(x => new
                    {
                        NotificacionExpedientesId = x.NotificacionExpedientesId,
                        ExpedienteId = x.ExpedienteId,
                        Codigo = x.Expediente.Codigo,
                        NomEntidad = x.Entidad.Nombre,
                        SectorId = x.SectorId,
                        ProvinciaId = x.ProvinciaId,
                        FecEnvio = x.FecEnvio.Value.ToString("dd/MM/yyyy hh:mm:ss tt"),
                        DiasTranscurrido = x.DiasTranscurrido,
                        FecUltimaRevision = x.FecUltimaRevision == null ? "" : x.FecUltimaRevision.Value.ToString("dd/MM/yyyy hh:mm:ss tt"),
                        EstadoExpediente = x.EstadoExpediente,
                        Tomar = x.Tomar,
                        RevEstado = x.RevEstado,
                        RevInfCdno = x.RevInfCdno,
                        RevDatosGenerales = x.RevDatosGenerales,
                        RevSutentoTecnico = x.RevSutentoTecnico,
                        RevTablaAsme = x.RevTablaAsme,
                        RevSustentoCosto = x.RevSustentoCosto,
                        Estado = x.Estado,

                    }),
                    rol = 6,
                    totalRows = totalRows
                });

            }
            else if (user.CodPadre == "2" && user.Rol == 7 || (user.EntidadId == 4105 && user.Rol == 7) || (user.EntidadId == 18497 && user.Rol == 7) || user.Rol == 12)
            {
                if (user.EntidadId == 4105 || user.EntidadId == 18497)
                {
                    //filtro.EstadoEvaluadorMinisterio = EstadoExpediente.Aprobado;

                    List<NotificacionExpedientes> lstNotifiexp = new List<NotificacionExpedientes>();

                    List<NotificacionExpedientes> lista = _notificacionExpedientesService.GetAllLikePaginEvaluadorMEFPCMEval(filtro, pageIndex, pageSize, ref totalRows);

                    var lstbandeja = _bandejaService.GetAllusuario(user.UsuarioId);

                    if (user.Bandeja == "1" && lstbandeja != null)
                    {
                        foreach (Bandeja ban in lstbandeja)
                        {
                            foreach (NotificacionExpedientes lstnotexp in lista)
                            {
                                if (ban.ExpedienteId == lstnotexp.ExpedienteId)
                                {
                                    lstNotifiexp.Add(lstnotexp);

                                }
                            }
                        }
                    }
                    else
                    {

                        lstNotifiexp = lista;
                    }


                    for (var j = 0; j < lstNotifiexp.Count(); j++)
                    {
                        int dias = 0;

                        Ini = lstNotifiexp[j].FecEnvio.Value.Date;
                        Fin = DateTime.Now.Date;
                        while (Ini != Fin)
                        {
                            if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                            {
                                dias = dias + 1;
                            }
                            Ini = Ini.AddDays(1);
                        }

                        lstNotifiexp[j].DiasTranscurrido = dias;
                    }
                    return JsonConvert.SerializeObject(new
                    {
                        lista = lstNotifiexp.Select(x => new
                        {
                            NotificacionExpedientesId = x.NotificacionExpedientesId,
                            ExpedienteId = x.ExpedienteId,
                            Codigo = x.Expediente.Codigo,
                            NomEntidad = x.Entidad.Nombre,
                            SectorId = x.SectorId,
                            ProvinciaId = x.ProvinciaId,
                            FecEnvio = x.FecEnvio.Value.ToString("dd/MM/yyyy hh:mm:ss tt"),
                            DiasTranscurrido = x.DiasTranscurrido,
                            FecUltimaRevision = x.FecUltimaRevision == null ? "" : x.FecUltimaRevision.Value.ToString("dd/MM/yyyy hh:mm:ss tt"),
                            EstadoExpediente = x.EstadoExpediente,
                            Tomar = x.Tomar,
                            RevEstado = x.RevEstado,
                            RevInfCdno = x.RevInfCdno,
                            RevDatosGenerales = x.RevDatosGenerales,
                            RevSutentoTecnico = x.RevSutentoTecnico,
                            RevTablaAsme = x.RevTablaAsme,
                            RevSustentoCosto = x.RevSustentoCosto,
                            Estado = x.Estado,


                        }),
                        rol = 7,
                        totalRows = totalRows
                    });
                }
                else
                {
                    List<NotificacionExpedientes> lstNotifiexp = new List<NotificacionExpedientes>();

                    filtro.SectorId = user.Sector;

                    var lstbandeja = _bandejaService.GetAllusuario(user.UsuarioId);
                    List<NotificacionExpedientes> lista = _notificacionExpedientesService.GetAllLikePaginEvaluadorEval(filtro, pageIndex, pageSize, ref totalRows);


                    if (user.Bandeja == "1" && lstbandeja != null)
                    {
                        foreach (Bandeja ban in lstbandeja)
                        {
                            foreach (NotificacionExpedientes lstnotexp in lista)
                            {
                                if (ban.ExpedienteId == lstnotexp.ExpedienteId)
                                {
                                    lstNotifiexp.Add(lstnotexp);

                                }
                            }
                        }
                    }
                    else
                    {

                        lstNotifiexp = lista;
                    }

                    for (var j = 0; j < lstNotifiexp.Count(); j++)
                    {
                        int dias = 0;

                        Ini = lstNotifiexp[j].FecEnvio.Value.Date;
                        Fin = DateTime.Now.Date;
                        while (Ini != Fin)
                        {
                            if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                            {
                                dias = dias + 1;
                            }
                            Ini = Ini.AddDays(1);
                        }

                        lstNotifiexp[j].DiasTranscurrido = dias;
                    }
                    return JsonConvert.SerializeObject(new
                    {
                        lista = lstNotifiexp.Select(x => new
                        {
                            NotificacionExpedientesId = x.NotificacionExpedientesId,
                            ExpedienteId = x.ExpedienteId,
                            Codigo = x.Expediente.Codigo,
                            NomEntidad = x.Entidad.Nombre,
                            SectorId = x.SectorId,
                            ProvinciaId = x.ProvinciaId,
                            FecEnvio = x.FecEnvio.Value.ToString("dd/MM/yyyy hh:mm:ss tt"),
                            DiasTranscurrido = x.DiasTranscurrido,
                            FecUltimaRevision = x.FecUltimaRevision == null ? "" : x.FecUltimaRevision.Value.ToString("dd/MM/yyyy hh:mm:ss tt"),
                            EstadoExpediente = x.EstadoExpediente,
                            Tomar = x.Tomar,
                            RevEstado = x.RevEstado,
                            RevInfCdno = x.RevInfCdno,
                            RevDatosGenerales = x.RevDatosGenerales,
                            RevSutentoTecnico = x.RevSutentoTecnico,
                            RevTablaAsme = x.RevTablaAsme,
                            RevSustentoCosto = x.RevSustentoCosto,
                            Estado = x.Estado,

                        }),
                        rol = 7,
                        totalRows = totalRows
                    });
                }

            }
            else
            {
                //if (user.Rol == 8)
                //filtro.EstadoExpediente = EstadoExpediente.Publicado; 

                List<NotificacionExpedientes> lstNotifiexp = new List<NotificacionExpedientes>();

                List<NotificacionExpedientes> lista = _notificacionExpedientesService.GetAllLikePaginFiscalizadorEval(filtro, pageIndex, pageSize, ref totalRows);

                var lstbandeja = _bandejaService.GetAllusuario(user.UsuarioId);

                if (user.Bandeja == "1" && lstbandeja != null)
                {
                    foreach (Bandeja ban in lstbandeja)
                    {
                        foreach (NotificacionExpedientes lstnotexp in lista)
                        {
                            if (ban.ExpedienteId == lstnotexp.ExpedienteId)
                            {
                                lstNotifiexp.Add(lstnotexp);

                            }
                        }
                    }
                }
                else
                {

                    lstNotifiexp = lista;
                }


                for (var j = 0; j < lstNotifiexp.Count(); j++)
                {
                    int dias = 0;

                    Ini = lstNotifiexp[j].FecEnvio.Value.Date;
                    Fin = DateTime.Now.Date;
                    while (Ini != Fin)
                    {
                        if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                        {
                            dias = dias + 1;
                        }
                        Ini = Ini.AddDays(1);
                    }

                    lstNotifiexp[j].DiasTranscurrido = dias;
                }
                return JsonConvert.SerializeObject(new
                {
                    lista = lstNotifiexp.Select(x => new
                    {
                        NotificacionExpedientesId = x.NotificacionExpedientesId,
                        ExpedienteId = x.ExpedienteId,
                        Codigo = x.Expediente.Codigo,
                        NomEntidad = x.Entidad.Nombre,
                        SectorId = x.SectorId,
                        ProvinciaId = x.ProvinciaId,
                        FecEnvio = x.FecEnvio.Value.ToString("dd/MM/yyyy"),
                        DiasTranscurrido = x.DiasTranscurrido,
                        FecUltimaRevision = x.FecUltimaRevision == null ? "" : x.FecUltimaRevision.Value.ToString("dd/MM/yyyy"),
                        EstadoExpediente = x.EstadoExpediente,
                        Tomar = x.Tomar,
                        RevEstado = x.RevEstado,
                        RevInfCdno = x.RevInfCdno,
                        RevDatosGenerales = x.RevDatosGenerales,
                        RevSutentoTecnico = x.RevSutentoTecnico,
                        RevTablaAsme = x.RevTablaAsme,
                        RevSustentoCosto = x.RevSustentoCosto,
                        Estado = x.Estado,

                    }),
                    rol = 8,
                    totalRows = totalRows
                });
            }

        }


        public string GetAllLikePaginactividades(Auditoria filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {


            Usuario TUsuario = _usuarioService.GetOne(user.UsuarioId);

            int totalRows = 0;
            List<Auditoria> lista;
            if (user.EntidadId == 0)
            {
                filtro.EntidadId = TUsuario.EntidadId;
            }
            else { filtro.EntidadId = user.EntidadId; }


            lista = _AuditoriaService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);


            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    auditoriaID = x.AuditoriaID,
                    actividad = x.Actividad,
                    usuario = x.Usuario,
                    fecCreacion = x.FecCreacion.Value.ToString("dd/MM/yyyy"),
                    pantalla = x.Pantalla
                }),
                totalRows = totalRows
            });
        }
        public string GetAllLikePaginMef(TablaAsme filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;
            List<TablaAsme> lista;

            lista = _TablaAsmeService.GetByMef(filtro, pageIndex, pageSize, ref totalRows);

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    tablaAsmeId = x.TablaAsmeId,
                    expediente = x.Procedimiento.Expediente.Codigo,
                    derechoTramitacion = x.DerechoTramitacion,
                    descripcionSusustento = x.DescripcionSusustento,
                    fecEnvioMef = x.FecEnvioMef.Value.ToString("dd/MM/yyyy"),
                    autorizacionMEF = x.AutorizacionMEF,
                    ArMotivoAdjuntoId = x.ArMotivoAdjuntoId,
                    UIT = x.UIT.Monto
                }),
                totalRows = totalRows
            });
        }

        public ActionResult guardaraccion(long Id, int Accion, int Estado, int Expediente, UsuarioInfo user)
        {
            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                if (Estado == 1)
                {
                    objauditoria.Actividad = "Iniciar Proceso";
                }
                else
                {
                    objauditoria.Actividad = "Detener Proceso";
                }
                objauditoria.Pantalla = "Expediente: " + Expediente;
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/

                NotificacionExpedientes objnot = _notificacionExpedientesService.GetByone(Id);
                objnot.Tomar = Accion;
                objnot.RevEstado = Estado;
                if (objnot.RevInfCdno != 2)
                {
                    objnot.RevInfCdno = Estado;
                }
                if (objnot.RevDatosGenerales != 2)
                {
                    objnot.RevDatosGenerales = Estado;
                }
                if (objnot.RevSutentoTecnico != 2)
                {
                    objnot.RevSutentoTecnico = Estado;
                }
                if (objnot.RevTablaAsme != 2)
                {
                    objnot.RevTablaAsme = Estado;
                }
                if (objnot.RevEstado != 2)
                {
                    objnot.RevEstado = Estado;
                }
                if (objnot.RevSustentoCosto != 2)
                {
                    objnot.RevSustentoCosto = Estado;
                }

                _notificacionExpedientesService.Save(objnot);
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



        public ActionResult ActualizarRevision(long Id, int Expediente, UsuarioInfo user)
        {
            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;  
                objauditoria.Pantalla = "Expediente: " + Expediente;
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                objauditoria.Actividad = "Fecha Ultima Revisión";
                /*auditoria*/

                NotificacionExpedientes objnot = _notificacionExpedientesService.GetByone(Id);

                objnot.FecUltimaRevision= DateTime.Now;

                _notificacionExpedientesService.Save(objnot);
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




        public ActionResult Editar(long id, UsuarioInfo user)
        {
            try
            {


                ViewBag.Usuario = user;
                Comunicado item;
                if (id == 0)
                {
                    item = new Comunicado();
                    ViewBag.Title = "Nuevo Comunicado";
                }
                else
                {
                    item = _ComunicadoService.GetOne(id);
                    ViewBag.Title = "Editar Comunicado";
                }
                var listaDocumentosNorma = _DocumentosNormaService.GetAll()
                                               .OrderBy(x => x.DocumentosNormaID)
                                               .ToList();
                listaDocumentosNorma.Insert(0, new DocumentosNorma()
                {
                    DocumentosNormaID = 0,
                    Descripcion = " - Ninguno - "
                });
                ViewBag.ListaDocumentosNorma = listaDocumentosNorma.Select(x => new SelectListItem()
                {
                    Value = x.DocumentosNormaID.ToString(),
                    Text = x.Descripcion
                }).ToList();

                ViewBag.publicar = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Si", Value = "1" , Selected = item.Estado == Respuesta.Si ? true:false},
                    new SelectListItem() { Text = "No", Value = "0",  Selected = item.Estado == Respuesta.No ? true:false },
                };

                return PartialView("_Editar", item);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }


        public ActionResult EditarMef(long id, UsuarioInfo user)
        {
            try
            {
                ViewBag.Usuario = user;
                TablaAsme item;
                if (id == 0)
                {
                    item = new TablaAsme();
                    ViewBag.Title = "Nuevo Comunicado";
                }
                else
                {
                    item = _TablaAsmeService.GetOne(id);
                    ViewBag.Title = "Editar Autorización";
                }


                ViewBag.publicar = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Autorizar", Value = "2" , Selected = item.AutorizacionMEF == Autorizacionmef.Autorizar ? true:false},
                    new SelectListItem() { Text = "No Autorizar", Value = "3",  Selected = item.AutorizacionMEF == Autorizacionmef.NoAutorizar ? true:false },
                };

                return PartialView("_EditarMef", item);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        public ActionResult EditarAsignar(long id, UsuarioInfo user)
        {
            try
            {
                ViewBag.Usuario = user;
                Bandeja item;
                if (id == 0)
                {
                    item = new Bandeja();
                    ViewBag.Title = "Nuevo Comunicado";
                }
                else
                {
                    item = _bandejaService.GetOne(id);
                    ViewBag.Title = "Asignar Usuario";
                }

                var lstbandeja = _bandejaService.GetAll(id);
                long usuid;
                ViewBag.DatosUsuario = _usuarioService.GetByEntidadROL(user.EntidadId).Where(x => x.EntidadId == user.EntidadId && (x.Rol == Rol.Evaluador || x.Rol == Rol.Ratificador)).ToList();




                if (lstbandeja != null)
                {
                    foreach (var mode1 in lstbandeja)
                    {

                        foreach (var mode in ViewBag.DatosUsuario)
                        {
                            if (mode1.UsuarioId == mode.UsuarioId)
                            {

                                mode.chekActivar = 1;
                            }
                        }
                    }

                }


                ViewBag.expedienteID = id;

                return PartialView("_EditarAsignacion", item);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        [HttpPost]
        public ActionResult AsignarBandeja(int Expediente, List<Bandeja> opcionbandeja, UsuarioInfo user)
        {
            try
            {
                //_sedeGrupoService.Eliminar(SedeId);

                _bandejaService.Eliminar(Expediente);

                if (opcionbandeja != null)
                {
                    List<Bandeja> objAtributo = new List<Bandeja>();

                    foreach (var mode in opcionbandeja)
                    {
                        objAtributo.Add(new Bandeja()
                        {
                            ExpedienteId = Expediente,
                            UsuarioId = mode.UsuarioId,
                            UserCreacion = user.NroDocumento,
                            FecCreacion = DateTime.Now,
                            Estado = 1,
                        });
                    }

                    _bandejaService.Save2(objAtributo);

                }

                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Asignar Usuario Bandeja";
                objauditoria.Pantalla = "Asignar Evaluador";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/

                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
                return Json(new
                {
                    mensaje = "Se asignó correctamente",
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
        public ActionResult EnviarAutorizacionMef(TablaAsme model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.TablaAsmeId == 0 ? "El Autorizar Mef fue registrada satisfactoriamente."
                                                : "El Autorizar Mef fue modificada satisfactoriamente.";

                    TablaAsme obj = _TablaAsmeService.GetOne(model.TablaAsmeId);
                    Auditoria objauditoria = new Auditoria();

                    obj.DescripcionRespuesa = model.DescripcionRespuesa;
                    obj.AutorizacionMEF = model.AutorizacionMEF;
                    obj.FecRespuestaMef = DateTime.Now;
                    obj.ArchivoAdjuntoId = model.ArchivoAdjuntoId;
                    _TablaAsmeService.Save(obj);

                    /*auditoria*/
                    objauditoria.EntidadId = user.EntidadId;
                    objauditoria.SectorId = user.Sector;
                    objauditoria.ProvinciaId = user.Provincia;
                    objauditoria.Usuario = user.NombreCompleto;
                    objauditoria.Actividad = "Autorizar Mef";
                    objauditoria.Pantalla = "Autorización Mef";
                    objauditoria.UserCreacion = user.NroDocumento;
                    objauditoria.FecCreacion = DateTime.Now;
                    /*auditoria*/
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
                ModelState.Clear();
                ModelState.AddModelError("", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }


        public ActionResult EditarNorma(long id, UsuarioInfo user)
        {
            try
            {
                ViewBag.Usuario = user;
                DocumentosNorma item;
                if (id == 0)
                {
                    item = new DocumentosNorma();
                    ViewBag.Title = "Nuevo Marco Normativo";
                }
                else
                {
                    item = _DocumentosNormaService.GetOne(id);
                    ViewBag.Title = "Editar  Marco Normativo";
                }


                ViewBag.publicar = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Si", Value = "1" , Selected = item.Estado == Respuesta.Si ? true:false},
                    new SelectListItem() { Text = "No", Value = "0",  Selected = item.Estado == Respuesta.No ? true:false },
                };

                return PartialView("_EditarNorma", item);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        [HttpPost]
        public ActionResult GuardarComunicado(Comunicado model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.ComunicadoID == 0 ? "El Comunicado fue registrada satisfactoriamente."
                                                : "El Comunicado fue modificada satisfactoriamente.";

                    Comunicado obj;
                    Auditoria objauditoria = new Auditoria();
                    if (model.ComunicadoID == 0)
                    {
                        obj = new Comunicado();
                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;
                        obj.EntidadId = user.EntidadId;
                        obj.SectorId = user.Sector;
                        obj.ProvinciaId = user.Provincia;

                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Noticia";
                        objauditoria.Pantalla = "Comunicado";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/


                    }
                    else
                    {
                        obj = _ComunicadoService.GetOne(model.ComunicadoID);

                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Noticia";
                        objauditoria.Pantalla = "Comunicado";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/

                    }


                    obj.Descripcion = model.Descripcion;
                    obj.Estado = model.Estado;
                    obj.UserModificacion = user.NroDocumento;
                    obj.FecModificacion = DateTime.Now;
                    if (model.DocumentosNormaID == 0)
                    {
                        obj.DocumentosNormaID = null;
                    }
                    else
                    {
                        obj.DocumentosNormaID = model.DocumentosNormaID;

                    }

                    _ComunicadoService.Save(obj);

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
                ModelState.AddModelError("editarComunicado", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }

        [HttpPost]
        public ActionResult GuardarNorma(DocumentosNorma model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.DocumentosNormaID == 0 ? "El Comunicado fue registrada satisfactoriamente."
                                                : "El Comunicado fue modificada satisfactoriamente.";

                    DocumentosNorma obj;
                    Auditoria objauditoria = new Auditoria();
                    if (model.DocumentosNormaID == 0)
                    {
                        obj = new DocumentosNorma();
                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;
                        obj.EntidadId = user.EntidadId;

                        /*auditoria nuevo*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Marco Normativo y Documentos SUT";
                        objauditoria.Pantalla = "Documentos Norma";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/

                    }
                    else
                    {
                        obj = _DocumentosNormaService.GetOne(model.DocumentosNormaID);

                        /*auditoria Modificar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Marco Normativo y Documentos SUT";
                        objauditoria.Pantalla = "Documentos Norma";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }


                    obj.Descripcion = model.Descripcion;
                    obj.Estado = model.Estado;
                    obj.FechaPublicacion = model.FechaPublicacion;

                    if (model.ArchivoAdjuntoId == 0)
                    {
                        obj.ArchivoAdjuntoId = null;
                    }
                    else
                    {
                        obj.ArchivoAdjuntoId = model.ArchivoAdjuntoId;
                    }
                    obj.UserModificacion = user.NroDocumento;
                    obj.FecModificacion = DateTime.Now;
                    _DocumentosNormaService.Save(obj);


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
                ModelState.AddModelError("editarNorma", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }

    }
}