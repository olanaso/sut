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
    public class SedeController : Controller
    {
        private readonly ILogService<SedeController> _log;
        private readonly ISedeService _sedeService;
        private readonly IDepartamentoService _departamentoService;
        private readonly IProvinciaService _provinciaService;
        private readonly IDistritoService _distritoService;
        private readonly IDatoService _datoService;
        private readonly IProcedimientoService _procedimientoService;
        private readonly IExpedienteService _expedienteService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly IRolMenuService _rolMenuService;
        private readonly ISedeGrupoService _sedeGrupoService;
        Auditoria objauditoria = new Auditoria();
        public SedeController(ISedeService sedeService,
                                IUbigeoService ubigeoService,
                                IDepartamentoService departamentoService,
                                IProvinciaService provinciaService,
                                IDistritoService distritoService,
                                IDatoService datoService, IAuditoriaService AuditoriaService,
                                IProcedimientoService procedimientoService,
                                IExpedienteService expedienteService,
                                IRolMenuService rolMenuService,
                                ISedeGrupoService sedeGrupoService)
        {
            _log = new LogService<SedeController>();
            _sedeService = sedeService;
            _departamentoService = departamentoService;
            _provinciaService = provinciaService;
            _distritoService = distritoService;
            _datoService = datoService;
            _procedimientoService = procedimientoService;
            _expedienteService = expedienteService;
            _AuditoriaService = AuditoriaService;
            _rolMenuService = rolMenuService;
            _sedeGrupoService = sedeGrupoService;
        }

        public ActionResult Lista(UsuarioInfo user)
        {
            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 8);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/
            ViewBag.TipoSede = 1;
            ViewBag.Usuario = user;
            return View("Lista");
        }

        public ActionResult Listagrupado(UsuarioInfo user)
        {
            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 8);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/
            ViewBag.TipoSede = 2;
            ViewBag.Usuario = user;
            return View("Lista");
        }

        public string GetAllLikePagin(Sede filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;
            filtro.EntidadId = user.EntidadId;

            List<Sede> lista = _sedeService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

            List<Dato> tipos = _datoService.GetByTipo(TipoDato.TipoSede);

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    SedeId = x.SedeId,
                    Nombre = x.Nombre,
                    TipoSede = tipos.First(t => t.MetaDatoId == x.TipoSedeId).Nombre,
                    Ubigeo = x.Distrito.Ruta,
                    Direccion = x.Direccion,
                    Tipogrupo = x.Tipogrupo,
                    ListaUndOrganica = x.SedeUnidadOrganica != null ? string.Join(",", x.SedeUnidadOrganica.Select(uo => uo.UnidadOrganica.Nombre).ToArray()) : "",
                    Horario =
                    (x.EsLunesViernes ? (x.TipoTurno == "C" ?
                        string.Format("Lunes a Viernes de {0} a {1}. ",
                                x.CorridoHorIni == null ? "-" : x.CorridoHorIni.Value.ToString("HH:mm"),
                                x.CorridoHorFin == null ? "-" : x.CorridoHorFin.Value.ToString("HH:mm"))
                        : string.Format("Lunes a Viernes de {0} a {1} y de {2} a {3}. ",
                                x.Turno1HorIni == null ? "-" : x.Turno1HorIni.Value.ToString("HH:mm"),
                                x.Turno1HorFin == null ? "-" : x.Turno1HorFin.Value.ToString("HH:mm"),
                                x.Turno2HorIni == null ? "-" : x.Turno2HorIni.Value.ToString("HH:mm"),
                                x.Turno2HorFin == null ? "-" : x.Turno2HorFin.Value.ToString("HH:mm"))
                                )
                                : (x.TipoTurno == "C" ? 
                        string.Format("{0} {1} {2} {3} {4} de {5} a {6}. ", x.EsLunes == true ? "Lunes, " : "",
                                x.EsMartes == true ? "Martes, " : "", x.EsMiercoles == true ? "Miercoles, " : "",
                                x.EsJueves == true ? "Jueves, " : "", x.EsViernes == true ? "Viernes" : "",
                                x.CorridoHorIni == null ? "-" : x.CorridoHorIni.Value.ToString("HH:mm"),
                                x.CorridoHorFin == null ? "-" : x.CorridoHorFin.Value.ToString("HH:mm"))
                        :string.Format("{0} {1} {2} {3} {4} de {5} a {6} y de {7} a {8}. ", x.EsLunes == true ? "Lunes, " : "",
                                x.EsMartes == true ? "Martes, " : "", x.EsMiercoles == true ? "Miercoles, " : "",
                                x.EsJueves == true ? "Jueves, " : "", x.EsViernes == true ? "Viernes" : "",
                                x.Turno1HorIni == null ? "-" : x.Turno1HorIni.Value.ToString("HH:mm"),
                                x.Turno1HorFin == null ? "-" : x.Turno1HorFin.Value.ToString("HH:mm"),
                                x.Turno2HorIni == null ? "-" : x.Turno2HorIni.Value.ToString("HH:mm"),
                                x.Turno2HorFin == null ? "-" : x.Turno2HorFin.Value.ToString("HH:mm"))
                                )) +
                    (x.EsSabado ? string.Format("Sábados de {0} a {1}. ",
                                x.SabadoHorIni == null ? "-" : x.SabadoHorIni.Value.ToString("HH:mm"),
                                x.SabadoHorFin == null ? "-" : x.SabadoHorFin.Value.ToString("HH:mm"))
                                : "") +
                    (x.EsDomingo ? string.Format("Domingos de {0} a {1}. ",
                                x.DomingoHorIni == null ? "-" : x.DomingoHorIni.Value.ToString("HH:mm"),
                                x.DomingoHorFin == null ? "-" : x.DomingoHorFin.Value.ToString("HH:mm"))
                                : "")
                }),
                totalRows = totalRows
            });
        }

        public string GetAllLikePaginDetalle(SedeGrupo filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;

            Sede sedes = new Sede();
            filtro.Sede = sedes;
            filtro.Sede.EntidadId = user.EntidadId;

            List<SedeGrupo> lista = _sedeGrupoService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

            List<Dato> tipos = _datoService.GetByTipo(TipoDato.TipoSede);

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    SedeGrupoId = x.SedeGrupoId,
                    SedeId = x.SedeId,
                    Nombre = x.Sede.Nombre,
                    TipoSede = tipos.First(t => t.MetaDatoId == x.Sede.TipoSedeId).Nombre,
                    Ubigeo = x.Sede.Distrito.Ruta,
                    Direccion = x.Sede.Direccion,
                    ListaUndOrganica = x.Sede.SedeUnidadOrganica != null ? string.Join(",", x.Sede.SedeUnidadOrganica.Select(uo => uo.UnidadOrganica.Nombre).ToArray()) : "",
                    Horario =
                    (x.Sede.EsLunesViernes ? (x.Sede.TipoTurno == "C" ?
                        string.Format("Lunes a Viernes de {0} a {1}. ",
                                x.Sede.CorridoHorIni == null ? "-" : x.Sede.CorridoHorIni.Value.ToString("HH:mm"),
                                x.Sede.CorridoHorFin == null ? "-" : x.Sede.CorridoHorFin.Value.ToString("HH:mm"))
                        : string.Format("Lunes a Viernes de {0} a {1} y de {2} a {3}. ",
                                x.Sede.Turno1HorIni == null ? "-" : x.Sede.Turno1HorIni.Value.ToString("HH:mm"),
                                x.Sede.Turno1HorFin == null ? "-" : x.Sede.Turno1HorFin.Value.ToString("HH:mm"),
                                x.Sede.Turno2HorIni == null ? "-" : x.Sede.Turno2HorIni.Value.ToString("HH:mm"),
                                x.Sede.Turno2HorFin == null ? "-" : x.Sede.Turno2HorFin.Value.ToString("HH:mm"))
                                )
                                : (x.Sede.TipoTurno == "C" ?
                        string.Format("{0} {1} {2} {3} {4} de {5} a {6}. ", x.Sede.EsLunes == true ? "Lunes, " : "",
                                x.Sede.EsMartes == true ? "Martes, " : "", x.Sede.EsMiercoles == true ? "Miercoles, " : "",
                                x.Sede.EsJueves == true ? "Jueves, " : "", x.Sede.EsViernes == true ? "Viernes" : "",
                                x.Sede.CorridoHorIni == null ? "-" : x.Sede.CorridoHorIni.Value.ToString("HH:mm"),
                                x.Sede.CorridoHorFin == null ? "-" : x.Sede.CorridoHorFin.Value.ToString("HH:mm"))
                        : string.Format("{0} {1} {2} {3} {4} de {5} a {6} y de {7} a {8}. ", x.Sede.EsLunes == true ? "Lunes, " : "",
                                x.Sede.EsMartes == true ? "Martes, " : "", x.Sede.EsMiercoles == true ? "Miercoles, " : "",
                                x.Sede.EsJueves == true ? "Jueves, " : "", x.Sede.EsViernes == true ? "Viernes" : "",
                                x.Sede.Turno1HorIni == null ? "-" : x.Sede.Turno1HorIni.Value.ToString("HH:mm"),
                                x.Sede.Turno1HorFin == null ? "-" : x.Sede.Turno1HorFin.Value.ToString("HH:mm"),
                                x.Sede.Turno2HorIni == null ? "-" : x.Sede.Turno2HorIni.Value.ToString("HH:mm"),
                                x.Sede.Turno2HorFin == null ? "-" : x.Sede.Turno2HorFin.Value.ToString("HH:mm"))
                                )) +
                    (x.Sede.EsSabado ? string.Format("Sábados de {0} a {1}. ",
                                x.Sede.SabadoHorIni == null ? "-" : x.Sede.SabadoHorIni.Value.ToString("HH:mm"),
                                x.Sede.SabadoHorFin == null ? "-" : x.Sede.SabadoHorFin.Value.ToString("HH:mm"))
                                : "") +
                    (x.Sede.EsDomingo ? string.Format("Domingos de {0} a {1}. ",
                                x.Sede.DomingoHorIni == null ? "-" : x.Sede.DomingoHorIni.Value.ToString("HH:mm"),
                                x.Sede.DomingoHorFin == null ? "-" : x.Sede.DomingoHorFin.Value.ToString("HH:mm"))
                                : "")
                }),
                totalRows = totalRows
            });
        }
        public string GetAllLikePaginDetalleSede(Sede filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;
            filtro.EntidadId = user.EntidadId;

            List<Sede> lista = _sedeService.GetAllLikePaginCarga(filtro, pageIndex, pageSize, ref totalRows);



            Sede Sede2 = new Sede();
            SedeGrupo filtro2 = new SedeGrupo();
            filtro2.SedePadreId = filtro.SedeId;
            filtro2.Sede = Sede2;
            filtro2.Sede.EntidadId = filtro.EntidadId;
            List<SedeGrupo> lista2 = _sedeGrupoService.GetAllLikePagin(filtro2, pageIndex, pageSize, ref totalRows);



            for (int i = 0; i < lista2.Count; i++)
            {
                for (int j = 0; j < lista.Count; j++)
                {

                    if (lista[j].SedeId == lista2[i].SedeId)
                    {

                        lista.RemoveAt(j);
                        break;
                    }
                }
            }



            List<Dato> tipos = _datoService.GetByTipo(TipoDato.TipoSede);

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    SedeId = x.SedeId,
                    Nombre = x.Nombre,
                    TipoSede = tipos.First(t => t.MetaDatoId == x.TipoSedeId).Nombre,
                    Ubigeo = x.Distrito.Ruta,
                    Direccion = x.Direccion,
                    ListaUndOrganica = x.SedeUnidadOrganica != null ? string.Join(",", x.SedeUnidadOrganica.Select(uo => uo.UnidadOrganica.Nombre).ToArray()) : "",
                    Horario =
                    (x.EsLunesViernes ? (x.TipoTurno == "C" ?
                        string.Format("Lunes a Viernes de {0} a {1}. ",
                                x.CorridoHorIni == null ? "-" : x.CorridoHorIni.Value.ToString("HH:mm"),
                                x.CorridoHorFin == null ? "-" : x.CorridoHorFin.Value.ToString("HH:mm"))
                        : string.Format("Lunes a Viernes de {0} a {1} y de {2} a {3}. ",
                                x.Turno1HorIni == null ? "-" : x.Turno1HorIni.Value.ToString("HH:mm"),
                                x.Turno1HorFin == null ? "-" : x.Turno1HorFin.Value.ToString("HH:mm"),
                                x.Turno2HorIni == null ? "-" : x.Turno2HorIni.Value.ToString("HH:mm"),
                                x.Turno2HorFin == null ? "-" : x.Turno2HorFin.Value.ToString("HH:mm"))
                                )
                                : (x.TipoTurno == "C" ?
                        string.Format("{0} {1} {2} {3} {4} de {5} a {6}. ", x.EsLunes == true ? "Lunes, " : "",
                                x.EsMartes == true ? "Martes, " : "", x.EsMiercoles == true ? "Miercoles, " : "",
                                x.EsJueves == true ? "Jueves, " : "", x.EsViernes == true ? "Viernes" : "",
                                x.CorridoHorIni == null ? "-" : x.CorridoHorIni.Value.ToString("HH:mm"),
                                x.CorridoHorFin == null ? "-" : x.CorridoHorFin.Value.ToString("HH:mm"))
                        : string.Format("{0} {1} {2} {3} {4} de {5} a {6} y de {7} a {8}. ", x.EsLunes == true ? "Lunes, " : "",
                                x.EsMartes == true ? "Martes, " : "", x.EsMiercoles == true ? "Miercoles, " : "",
                                x.EsJueves == true ? "Jueves, " : "", x.EsViernes == true ? "Viernes" : "",
                                x.Turno1HorIni == null ? "-" : x.Turno1HorIni.Value.ToString("HH:mm"),
                                x.Turno1HorFin == null ? "-" : x.Turno1HorFin.Value.ToString("HH:mm"),
                                x.Turno2HorIni == null ? "-" : x.Turno2HorIni.Value.ToString("HH:mm"),
                                x.Turno2HorFin == null ? "-" : x.Turno2HorFin.Value.ToString("HH:mm"))
                                )) +
                    (x.EsSabado ? string.Format("Sábados de {0} a {1}. ",
                                x.SabadoHorIni == null ? "-" : x.SabadoHorIni.Value.ToString("HH:mm"),
                                x.SabadoHorFin == null ? "-" : x.SabadoHorFin.Value.ToString("HH:mm"))
                                : "") +
                    (x.EsDomingo ? string.Format("Domingos de {0} a {1}. ",
                                x.DomingoHorIni == null ? "-" : x.DomingoHorIni.Value.ToString("HH:mm"),
                                x.DomingoHorFin == null ? "-" : x.DomingoHorFin.Value.ToString("HH:mm"))
                                : "")
                }),
                totalRows = totalRows
            });
        }

        [HttpPost]
        public ActionResult Agregargrupo(int SedeId, List<SedeGrupo> opcionsede, UsuarioInfo user)
        {
            try
            {
                //_sedeGrupoService.Eliminar(SedeId);

                if (opcionsede != null)
                {
                    List<SedeGrupo> objAtributo = new List<SedeGrupo>();

                    foreach (var mode in opcionsede)
                    {
                        objAtributo.Add(new SedeGrupo()
                        {
                            SedeId = mode.SedeId,
                            SedePadreId = SedeId,
                        });
                    }

                    _sedeGrupoService.Save(objAtributo);

                }

                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
                return Json(new
                {
                    mensaje = "Se agrego las sedes correctamente",
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



        public ActionResult GetAllLikePaginByTupaxidFiltro(Sede filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            try
            {
                int totalRows = 0;
                filtro.EntidadId = user.EntidadId;

                List<Sede> lista = _sedeService.GetAllLikePaginGrupo(filtro, pageIndex, pageSize, ref totalRows);
                List<Dato> tipos = _datoService.GetByTipo(TipoDato.TipoSede);

                ViewBag.TotalRows = totalRows;
                ViewBag.Row = (pageSize * pageIndex) - pageSize;

                return Json(new
                {
                    lista = lista.Select(x => new
                    {
                        SedeId = x.SedeId,
                        Nombre = x.Nombre,
                        TipoSede = tipos.First(t => t.MetaDatoId == x.TipoSedeId).Nombre,
                        Ubigeo = x.Distrito.Ruta,
                        Direccion = x.Direccion,
                        ListaUndOrganica = x.SedeUnidadOrganica != null ? string.Join(",", x.SedeUnidadOrganica.Select(uo => uo.UnidadOrganica.Nombre).ToArray()) : "",
                        Horario =
                    (x.EsLunesViernes ? (x.TipoTurno == "C" ?
                        string.Format("Lunes a Viernes de {0} a {1}. ",
                                x.CorridoHorIni == null ? "-" : x.CorridoHorIni.Value.ToString("HH:mm"),
                                x.CorridoHorFin == null ? "-" : x.CorridoHorFin.Value.ToString("HH:mm"))
                        : string.Format("Lunes a Viernes de {0} a {1} y de {2} a {3}. ",
                                x.Turno1HorIni == null ? "-" : x.Turno1HorIni.Value.ToString("HH:mm"),
                                x.Turno1HorFin == null ? "-" : x.Turno1HorFin.Value.ToString("HH:mm"),
                                x.Turno2HorIni == null ? "-" : x.Turno2HorIni.Value.ToString("HH:mm"),
                                x.Turno2HorFin == null ? "-" : x.Turno2HorFin.Value.ToString("HH:mm"))
                                )
                                : (x.TipoTurno == "C" ?
                        string.Format("{0} {1} {2} {3} {4} de {5} a {6}. ", x.EsLunes == true ? "Lunes, " : "",
                                x.EsMartes == true ? "Martes, " : "", x.EsMiercoles == true ? "Miercoles, " : "",
                                x.EsJueves == true ? "Jueves, " : "", x.EsViernes == true ? "Viernes" : "",
                                x.CorridoHorIni == null ? "-" : x.CorridoHorIni.Value.ToString("HH:mm"),
                                x.CorridoHorFin == null ? "-" : x.CorridoHorFin.Value.ToString("HH:mm"))
                        : string.Format("{0} {1} {2} {3} {4} de {5} a {6} y de {7} a {8}. ", x.EsLunes == true ? "Lunes, " : "",
                                x.EsMartes == true ? "Martes, " : "", x.EsMiercoles == true ? "Miercoles, " : "",
                                x.EsJueves == true ? "Jueves, " : "", x.EsViernes == true ? "Viernes" : "",
                                x.Turno1HorIni == null ? "-" : x.Turno1HorIni.Value.ToString("HH:mm"),
                                x.Turno1HorFin == null ? "-" : x.Turno1HorFin.Value.ToString("HH:mm"),
                                x.Turno2HorIni == null ? "-" : x.Turno2HorIni.Value.ToString("HH:mm"),
                                x.Turno2HorFin == null ? "-" : x.Turno2HorFin.Value.ToString("HH:mm"))
                                )) +
                    (x.EsSabado ? string.Format("Sábados de {0} a {1}. ",
                                x.SabadoHorIni == null ? "-" : x.SabadoHorIni.Value.ToString("HH:mm"),
                                x.SabadoHorFin == null ? "-" : x.SabadoHorFin.Value.ToString("HH:mm"))
                                : "") +
                    (x.EsDomingo ? string.Format("Domingos de {0} a {1}. ",
                                x.DomingoHorIni == null ? "-" : x.DomingoHorIni.Value.ToString("HH:mm"),
                                x.DomingoHorFin == null ? "-" : x.DomingoHorFin.Value.ToString("HH:mm"))
                                : "")
                    }).Distinct(),
                    totalRows = totalRows
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
        public ActionResult Editar(long id, UsuarioInfo user)
        {
            try
            {
                /*Inicio Cargar roles - Acceso*/
                List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 8);
                user.rolmenu = listarolmenu.ToList();
                /*Fin Cargar roles  - Acceso*/

                ViewBag.User = user;

                List<SelectListItem> listaTipos = _datoService.GetByTipo(TipoDato.TipoSede)
                                            .Select(x => new SelectListItem()
                                            {
                                                Text = x.Nombre,
                                                Value = x.MetaDatoId.ToString()
                                            }).ToList();
                listaTipos.Insert(0, new SelectListItem() { Text = "- SELECCIONAR -", Value = "0" });

                List<SelectListItem> listaDepartamentos = _departamentoService.GetAll()
                                                        .Select(x => new SelectListItem()
                                                        {
                                                            Text = x.Nombre,
                                                            Value = x.DepartamentoId.ToString()
                                                        }).ToList();
                listaDepartamentos.Insert(0, new SelectListItem() { Text = "- SELECCIONAR -", Value = "0" });

                List<SelectListItem> listaProvincias = new List<SelectListItem>() { new SelectListItem() { Text = "- SELECCIONAR -", Value = "0" } };
                List<SelectListItem> listaDistritos = new List<SelectListItem>() { new SelectListItem() { Text = "- SELECCIONAR -", Value = "0" } };

                Sede item;
                if (id == 0)
                {
                    item = new Sede();
                    item.EntidadId = user.EntidadId;
                    item.SedeUnidadOrganica = new List<SedeUnidadOrganica>();
                    ViewBag.Title = "Nueva Sede";
                    item.EsLunesViernes = true;
                }
                else
                {
                    item = _sedeService.GetOne(id);
                    ViewBag.Title = "Editar Sede";

                    listaDepartamentos.ForEach(x =>
                    {
                        x.Selected = x.Value == item.Distrito.Provincia.DepartamentoId.ToString();
                    });

                    listaProvincias = _provinciaService.GetByDepartamento(item.Distrito.Provincia.DepartamentoId)
                                        .Select(x => new SelectListItem()
                                        {
                                            Text = x.Nombre,
                                            Value = x.ProvinciaId.ToString(),
                                            Selected = x.ProvinciaId == item.Distrito.ProvinciaId
                                        }).ToList();
                    listaProvincias.Insert(0, new SelectListItem() { Text = "- SELECCIONAR -", Value = "0" });

                    listaDistritos = _distritoService.GetByProvincia(item.Distrito.ProvinciaId)
                                        .Select(x => new SelectListItem()
                                        {
                                            Text = x.Nombre,
                                            Value = x.DistritoId.ToString(),
                                            Selected = x.DistritoId == item.DistritoId
                                        }).ToList();
                    listaDistritos.Insert(0, new SelectListItem() { Text = "- SELECCIONAR -", Value = "0" });


                    if (item.SedeUnidadOrganica != null)
                        if (item.SedeUnidadOrganica.Count() > 0)
                        {
                            item.ListaUndOrganica = string.Join(",", item.SedeUnidadOrganica.Select(x => x.UnidadOrganicaId).ToArray());
                            item.TextoUndOrganica = string.Join(",", item.SedeUnidadOrganica.Select(x => x.UnidadOrganica.Nombre).ToArray());
                        }
                }

                ViewBag.ListaTipos = listaTipos;

                ViewBag.TiposGrupo = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Si", Value = "1" , Selected = item.Tipogrupo == 1 ? true:false},
                    new SelectListItem() { Text = "No", Value = "0",  Selected = item.Tipogrupo == 0 ? true:false },
                };



                ViewBag.ListaDepartamentos = listaDepartamentos;
                ViewBag.ListaProvincias = listaProvincias;
                ViewBag.ListaDistritos = listaDistritos;

                ViewBag.TipoTurno = item.TipoTurno;

                return PartialView("_Editar", item);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                _log.Error(ex);
                return PartialView("_Error");
            }
        }

        public ActionResult Agregar(long id, UsuarioInfo user)
        {
            try
            {
                Sede item = new Sede();
                item.SedeId = id;

                ViewBag.SedeId = id;

                return PartialView("_AgregaSede", item);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                _log.Error(ex);
                return PartialView("_Error");
            }
        }


        public ActionResult GetProvincias(int DepartamentoId)
        {
            try
            {
                List<SelectListItem> lista = _provinciaService.GetByDepartamento(DepartamentoId)
                                            .Select(x => new SelectListItem()
                                            {
                                                Text = x.Nombre,
                                                Value = x.ProvinciaId.ToString()
                                            }).ToList();
                lista.Insert(0, new SelectListItem() { Text = "- SELECCIONAR -", Value = "0" });

                return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                _log.Error(ex);
                return PartialView("_Error");
            }
        }
        public ActionResult GetDistritos(int DepartamentoId, int ProvinciaId)
        {
            try
            {
                List<SelectListItem> lista = _distritoService.GetByProvincia(ProvinciaId)
                                            .Select(x => new SelectListItem()
                                            {
                                                Text = x.Nombre,
                                                Value = x.DistritoId.ToString()
                                            }).ToList();
                lista.Insert(0, new SelectListItem() { Text = "- SELECCIONAR -", Value = "0" });

                return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
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
        public string ExisteEnProcedimiento(int SedeId, UsuarioInfo user)
        {

            var res = false;
            //var sede = _sedeService.GetOne(SedeId);

            //var vExpediente = _expedienteService.GetByEntidad(user.EntidadId);

            //foreach (Expediente o in vExpediente == null ? new List<Expediente>() : vExpediente)
            //{

            //    var Proc = _procedimientoService.GetByExpediente(o.ExpedienteId);

            //    foreach (Procedimiento p in Proc == null ? new List<Procedimiento>() : Proc)
            //    {

            //        if (p.ProcedimientoSede != null)
            //        {

            //            var result = p.ProcedimientoSede.Where(x => x.SedeId == sede.SedeId).Count();

            //            if (result > 0)
            //                res = true;
            //        }

            //    }
            //} 

            var VerificarEliminarSede = _AuditoriaService.VerificarEliminarSede(SedeId);

            return JsonConvert.SerializeObject(new StatusResponse()
            {
                Value = res,
                Success = true,
                Message = VerificarEliminarSede
            });

        }


        [HttpPost]
        public ActionResult Guardar(Sede model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.SedeId == 0 ? "La Sede fue registrada satisfactoriamente."
                                                : "La Sede fue modificada satisfactoriamente.";

                    Sede obj;

                    if (model.SedeId == 0)
                    {
                        obj = new Sede();
                        obj.EntidadId = model.EntidadId;
                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;
                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Sede";
                        objauditoria.Pantalla = "Sede";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }
                    else
                    {
                        obj = _sedeService.GetOne(model.SedeId);

                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Sede";
                        objauditoria.Pantalla = "Sede";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }


                    obj.TipoSedeId = model.TipoSedeId;
                    obj.Nombre = model.Nombre;
                    obj.DistritoId = model.DistritoId;
                    obj.Direccion = model.Direccion;
                    obj.EsLunesViernes = model.EsLunesViernes;
                    obj.TipoTurno = model.TipoTurno;
                    obj.CorridoHorIni = model.CorridoHorIni;
                    obj.CorridoHorFin = model.CorridoHorFin;
                    obj.Turno1HorIni = model.Turno1HorIni;
                    obj.Turno1HorFin = model.Turno1HorFin;
                    obj.Turno2HorIni = model.Turno2HorIni;
                    obj.Turno2HorFin = model.Turno2HorFin;
                    obj.EsSabado = model.EsSabado;
                    obj.SabadoHorIni = model.SabadoHorIni;
                    obj.SabadoHorFin = model.SabadoHorFin;
                    obj.EsDomingo = model.EsDomingo;
                    obj.DomingoHorIni = model.DomingoHorIni;
                    obj.DomingoHorFin = model.DomingoHorFin;
                    obj.EsLunes = model.EsLunes; /*JJJMSP2*/
                    obj.EsMartes = model.EsMartes;
                    obj.EsMiercoles = model.EsMiercoles;
                    obj.EsJueves = model.EsJueves;
                    obj.EsViernes = model.EsViernes;
                    obj.Activo = true;
                    obj.Tipogrupo = model.Tipogrupo;


                    if (model.Tipogrupo == 0 && model.SedeId != 0)
                    {
                        _sedeGrupoService.Eliminar(Convert.ToInt32(model.SedeId));
                    }


                    obj.SedeUnidadOrganica = new List<SedeUnidadOrganica>();
                    if (!string.IsNullOrEmpty(model.ListaUndOrganica))
                    {
                        string[] listaUO = model.ListaUndOrganica.Split(new[] { "," }, StringSplitOptions.None);
                        foreach (string id in listaUO)
                        {
                            obj.SedeUnidadOrganica.Add(new SedeUnidadOrganica()
                            {
                                SedeId = obj.SedeId,
                                UnidadOrganicaId = int.Parse(id)
                            });
                        }
                    }

                    obj.UserModificacion = user.NroDocumento;
                    obj.FecModificacion = DateTime.Now;
                    _sedeService.Save(obj);
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
                ModelState.AddModelError("editarRecurso", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }
        [HttpPost]
        public ActionResult Eliminar(int SedeId, UsuarioInfo user)
        {

            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Eliminar Sede";
                objauditoria.Pantalla = "Sede";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                _sedeService.Eliminar(SedeId);
                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
                return Json(new
                {
                    mensaje = "La sede fue eliminada satisfactoriamente."
                });

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("editarRecurso", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }

        }


        [HttpPost]
        public ActionResult EliminarGrupo(int SedeId, UsuarioInfo user)
        {

            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Quitar Sede grupo";
                objauditoria.Pantalla = "Sede grupo";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                _sedeGrupoService.EliminarGrupo(SedeId);
                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
                return Json(new
                {
                    mensaje = "La sede fue eliminada satisfactoriamente."
                });

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("editarRecurso", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }

        }


        public ActionResult Select(bool multi, string fnAdd)
        {
            try
            {
                ViewBag.Multi = multi;
                ViewBag.FnAdd = fnAdd;

                return PartialView("_Select");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}