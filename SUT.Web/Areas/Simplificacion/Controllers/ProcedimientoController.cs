using Newtonsoft.Json;
using Sut.Entities;
using Sut.Extensions;
using Sut.IApplicationServices;
using Sut.Log;
using Sut.Security;
using Sut.Web.Areas.Simplificacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Sut.Web.Areas.Simplificacion.Controllers
{
    [Authorize]
    public class ProcedimientoController : Controller
    {
        private readonly ILogService<ProcedimientoController> _log;
        private readonly IProcedimientoService _procedimientoService;
        private readonly IRequisitoService _requisitoService;
        private readonly IMetaDatoService _metaDatoService;
        private readonly IBaseLegalService _baseLegalService;
        private readonly IExpedienteService _expedienteService;
        private readonly IArchivoAdjuntoService _archivoAdjuntoService;
        private readonly IUsuarioService _usuarioService;
        private readonly IDatoService _datoService;
        private readonly IMaestroFijoService _maestroFijoService;
        private readonly IUnidadOrganicaService _unidadOrganicaService;
        private readonly ISedeService _sedeService;
        private readonly IUITService _UITService;
        private readonly IObservacionService _observacionService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly IProcedimientoCategoriaService _ProcedimientoCategoriaService;
        private readonly ITablaAsmeService _tablaAsmeService;
        private readonly ITablaAsmeReproduccionService _tablaAsmeReproduccionService;
        private readonly IPlazoAtencionService _plazoAtencionService;
        private readonly IRolMenuService _rolMenuService;
        private readonly IProcedimientoCargosService _procedimientoCargosService;
        private readonly IProcedimientoCargosApeService _ProcedimientoCargosApeService;
        private readonly IProcedimientoCargosOtrosService _ProcedimientoCargosOtrosService;
        private readonly IProcedimientoUndOrgResponsableService _ProcedimientoUndOrgResponsableService;

        Auditoria objauditoria = new Auditoria();
        private string textodenominacion;
        public ProcedimientoController(IProcedimientoService procedimientoService,
                                        IRequisitoService requisitoService,
                                        IMetaDatoService metaDatoService,
                                        IBaseLegalService baseLegalService,
                                        IExpedienteService expedienteService,
                                        IArchivoAdjuntoService archivoAdjuntoService,
                                        IUsuarioService usuarioService,
                                        IDatoService datoService,
                                        IMaestroFijoService maestroFijoService,
                                        IUnidadOrganicaService unidadOrganicaService,
                                        ISedeService sedeService,
                                        IUITService UITService, IAuditoriaService AuditoriaService,
                                        IObservacionService observacionService,
                                        IProcedimientoCategoriaService procedimientoCategoriaService,
                                        ITablaAsmeService tablaAsmeService,
                                        ITablaAsmeReproduccionService tablaAsmeReproduccionService,
                                        IPlazoAtencionService plazoAtencionService,
                                        IRolMenuService rolMenuService,
                                        IProcedimientoCargosService procedimientoCargosService,
                                        IProcedimientoCargosApeService procedimientoCargosApeService,
                                        IProcedimientoCargosOtrosService procedimientoCargosOtrosService,
                                        IProcedimientoUndOrgResponsableService procedimientoUndOrgResponsableService)
        {
            _log = new LogService<ProcedimientoController>();
            _procedimientoService = procedimientoService;
            _requisitoService = requisitoService;
            _metaDatoService = metaDatoService;
            _baseLegalService = baseLegalService;
            _expedienteService = expedienteService;
            _archivoAdjuntoService = archivoAdjuntoService;
            _usuarioService = usuarioService;
            _datoService = datoService;
            _maestroFijoService = maestroFijoService;
            _unidadOrganicaService = unidadOrganicaService;
            _sedeService = sedeService;
            _UITService = UITService;
            _observacionService = observacionService;
            _AuditoriaService = AuditoriaService;
            _ProcedimientoCategoriaService = procedimientoCategoriaService;
            _tablaAsmeService = tablaAsmeService;
            _tablaAsmeReproduccionService = tablaAsmeReproduccionService;
            _plazoAtencionService = plazoAtencionService;
            _rolMenuService = rolMenuService;
            _procedimientoCargosService = procedimientoCargosService;
            _ProcedimientoCargosApeService = procedimientoCargosApeService;
            _ProcedimientoUndOrgResponsableService = procedimientoUndOrgResponsableService;
            _ProcedimientoCargosOtrosService = procedimientoCargosOtrosService;
        }

        public ActionResult GetAllLikePagin(Procedimiento filtro, int pageIndex, int pageSize, Expediente exp, UsuarioInfo user)
        {
            try
            {

                /*Inicio Cargar roles - Acceso*/
                List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 20);
                user.rolmenu = listarolmenu.ToList();
                /*Fin Cargar roles  - Acceso*/
                //Expediente model = new Expediente();
                Expediente model = _expedienteService.GetOne(filtro.ExpedienteId);
                ViewBag.Usuario = user;

                filtro.Expediente = model;

                /*Inicio Cargar roles - Acceso*/
                UsuarioInfo userInf = new UsuarioInfo();
                List<RolMenu> listarolmenuInf = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 21);
                userInf.rolmenu = listarolmenuInf.ToList();
                ViewBag.UsuarioInf = userInf;
                /*Fin Cargar roles  - Acceso*/


                /*Inicio Cargar roles - Acceso*/
                UsuarioInfo userDatos = new UsuarioInfo();
                List<RolMenu> listarolmenuDatos = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 22);
                userDatos.rolmenu = listarolmenuDatos.ToList();
                ViewBag.UsuarioDatos = userDatos;
                /*Fin Cargar roles  - Acceso*/

                /*Inicio Cargar roles - Acceso*/
                UsuarioInfo userSTL = new UsuarioInfo();
                List<RolMenu> listarolmenuSTL = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 23);
                userSTL.rolmenu = listarolmenuSTL.ToList();
                ViewBag.UsuarioSTL = userSTL;
                /*Fin Cargar roles  - Acceso*/


                /*Inicio Cargar roles - Acceso*/
                UsuarioInfo userAsme = new UsuarioInfo();
                List<RolMenu> listarolmenuAsme = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 24);
                userAsme.rolmenu = listarolmenuAsme.ToList();
                ViewBag.UsuarioAsme = userAsme;
                /*Fin Cargar roles  - Acceso*/

                /*Inicio Cargar roles - Acceso*/
                UsuarioInfo userrptTupa = new UsuarioInfo();
                List<RolMenu> listarolmenurptTupa = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 25);
                userrptTupa.rolmenu = listarolmenurptTupa.ToList();
                ViewBag.UsuariorptTupa = userrptTupa;
                /*Fin Cargar roles  - Acceso*/

                /*Inicio Cargar roles - Acceso*/
                UsuarioInfo userrptSTL = new UsuarioInfo();
                List<RolMenu> listarolmenurptSTL = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 26);
                userrptSTL.rolmenu = listarolmenurptSTL.ToList();
                ViewBag.UsuariorptSTL = userrptSTL;
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


                int totalRows = 0;
                List<Procedimiento> lista = _procedimientoService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

                lista.ForEach(x => { if (x.UndOrgResponsable == null) x.UndOrgResponsable = new UnidadOrganica(); });

                var categorias = _datoService.GetByTipo(TipoDato.CategoriaProcedimiento);

                //lista.ForEach(x => x.NombreCategoria = categorias.Where(o => (int)o.Tipo == x.CategoriaProcedimientoId).Select(n => n.Nombre).FirstOrDefault());


                lista.ForEach(x => x.NombreCategoria = categorias.Where(o => (int)o.DatoId == x.CategoriaProcedimientoId).Select(n => n.Nombre).FirstOrDefault());






                //foreach (Procedimiento l in lista)
                //{

                //    foreach (Dato d in categorias)
                //    {
                //        if ((int)d.DatoId == l.CategoriaProcedimientoId)
                //        {
                //            l.NombreCategoria = d.Nombre;
                //        }
                //    }
                //}
                exp = _expedienteService.GetOne(filtro.ExpedienteId);
                ViewBag.Expediente = exp;
                ViewBag.TotalRows = totalRows;
                ViewBag.Row = (pageSize * pageIndex) - pageSize;

                try
                {
                    //model = _expedienteService.GetOne(filtro.ExpedienteId);

                    //ViewBag.User = user;
                    ViewBag.Estadisticas = _expedienteService.GetEstadisticasconteo(filtro);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("editarExpediente", ex.Message);
                    _log.Error(ex);
                }

                var explo = lista.Where(x => x.ExplorarNum == 0 && x.Operacion != OperacionExpediente.Eliminacion && x.Estado != 3).Count();
                if (explo != 0)
                {
                    _AuditoriaService.GenerarNumeracion(lista[0].ExpedienteId);
                }

                return PartialView("_ListaProcedimiento", lista);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("ListaProcedimiento", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }


        [HttpPost]
        public string ExisteRequisitos(long RequisitoId, long TipoRequisito, EstadoExpediente estadoExpediente, UsuarioInfo user)
        {

            var res = false;


            /*auditoria agregar*/
            objauditoria.EntidadId = user.EntidadId;
            objauditoria.SectorId = user.Sector;
            objauditoria.ProvinciaId = user.Provincia;
            objauditoria.Usuario = user.NombreCompleto;
            objauditoria.Actividad = "Eliminar Requisitos";
            objauditoria.Pantalla = "Requisitos";
            objauditoria.UserCreacion = user.NroDocumento;
            objauditoria.FecCreacion = DateTime.Now;
            /*auditoria*/

            var VerificarEliminar = _AuditoriaService.VerificarEliminarReq(RequisitoId, TipoRequisito, (short)estadoExpediente);
            _AuditoriaService.Save(objauditoria);
            return JsonConvert.SerializeObject(new StatusResponse()
            {
                Value = res,
                Success = true,
                Message = VerificarEliminar
            });

        }


        public ActionResult EditarCondicion(long id, int Tipoficha, UsuarioInfo user)
        {
            try
            {
                ViewBag.User = user;

                Procedimiento item;

                item = _procedimientoService.GetOne(id);
                ViewBag.Title = "Editar Condición";
                item.TipoFicha = Tipoficha;

                ViewBag.publicarConcion = new List<SelectListItem>()
                {
                    //new SelectListItem() { Text = "Sin Acción", Value = "0" },
                    new SelectListItem() { Text = "En Revisión", Value = "1" },
                    new SelectListItem() { Text = "Observado", Value = "4" },
                    new SelectListItem() { Text = "Validado", Value = "6" },
                };


                return PartialView("_OpcionCondicion", item);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        public ActionResult condicionvolver(long id, int Tipoficha, UsuarioInfo user)
        {
            try
            {
                ViewBag.User = user;

                Procedimiento item;

                item = _procedimientoService.GetOne(id);
                Int64 condicion = 0;

                if (Tipoficha == 1)
                {
                    condicion = item.EstadoInfoEva;
                }
                else if (Tipoficha == 2)
                {
                    condicion = item.EstadodatosEva;
                }
                else if (Tipoficha == 3)
                {
                    condicion = item.EstadostlEva;
                }
                else if (Tipoficha == 4)
                {
                    condicion = item.EstadotablaasmeEva;
                }
                return Json(new { valid = true, estado = condicion }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        public ActionResult AtrasPA(long pExpediente, int pExplorarNum, UsuarioInfo user)
        {
            try
            {
                long condicion = 0;

                pExplorarNum = pExplorarNum - 1;
                if (pExplorarNum != 0)
                {
                    condicion = _AuditoriaService.BotonSiguienteAtras_INDEX(pExpediente, pExplorarNum);
                }


                return Json(new { valid = true, estado = condicion }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        public ActionResult SiguientePA(long pExpediente, int pExplorarNum, UsuarioInfo user)
        {
            try
            {

                pExplorarNum = pExplorarNum + 1;

                long condicion = _AuditoriaService.BotonSiguienteAtras_INDEX(pExpediente, pExplorarNum);

                return Json(new { valid = true, estado = condicion }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }


        public ActionResult condicionvolverFichas(long id, int Tipoficha, UsuarioInfo user)
        {
            try
            {
                ViewBag.User = user;

                List<Procedimiento> item;

                item = _expedienteService.GetByEstados(id);
                Int64 condicion = 0;
                if (Tipoficha == 1)
                {
                    condicion = item.Where(x => x.EstadoInfoEva == 0 || x.EstadoInfoEva == 1 || x.EstadoInfoEva == 5).Count();
                }
                else if (Tipoficha == 2)
                {
                    condicion = item.Where(x => x.EstadodatosEva == 0 || x.EstadodatosEva == 1 || x.EstadodatosEva == 5).Count();
                }
                else if (Tipoficha == 3)
                {
                    condicion = item.Where(x => x.EstadostlEva == 0 || x.EstadostlEva == 1 || x.EstadostlEva == 5).Count();
                }
                else if (Tipoficha == 4)
                {
                    condicion = item.Where(x => x.EstadotablaasmeEva == 0 || x.EstadotablaasmeEva == 1 || x.EstadotablaasmeEva == 5).Count();
                }


                return Json(new { valid = true, estado = condicion }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }
        [HttpPost]
        public ActionResult activarcondicion(Procedimiento model, UsuarioInfo user)
        {
            try
            {
                _procedimientoService.activarCondicion(model);
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
        public string GetAllLikePagincategoria(Dato filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {


            var categorias = _datoService.GetByTipo(TipoDato.CategoriaProcedimiento);

            int totalRows = 0;


            List<Dato> lista = new List<Dato>();



            List<ProcedimientoCategoria> lista2 = _ProcedimientoCategoriaService.Lsitaprocedimientocategoria(filtro.idex, filtro.idpro);



            lista = _datoService.GetByTipo(TipoDato.CategoriaProcedimiento);


            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    Codigo = x.Codigo,
                    Nombre = x.Nombre
                }),
                lista2 = lista2.Select(x => new
                {
                    CategoriaProcedimientoId = x.CategoriaProcedimientoId.Trim().ToString()
                }),
                totalRows = totalRows
            });
        }



        [HttpPost]
        public bool deleteunidadorg(long tipo, long id, UsuarioInfo user)
        {

            try
            {

                Procedimiento model = _procedimientoService.GetOne(id);
                if (tipo == 1)
                {
                    model.UndOrgReconsideracionId = null;
                }
                else if (tipo == 2)
                {

                    model.UndOrgApelacionId = null;
                }
                else
                {

                    model.UndOrgOtros = null;
                }

                _procedimientoService.Eliminacionorg(model);


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("editarExpediente", ex.Message);
                _log.Error(ex);
            }
            return true;
        }





        [HttpPost]
        public bool GenerarNumeroPA(long id, UsuarioInfo user)
        {
            Procedimiento model = new Procedimiento();
            try
            {



                int totalRows = 1;
                List<Procedimiento> lista = _procedimientoService.GetByExpedienteNumero(id);

                if (lista.Count == 0)
                {
                    return false;
                }
                else
                {

                    foreach (Procedimiento l in lista)
                    {

                        model.ProcedimientoId = l.ProcedimientoId;
                        model.Numero = totalRows;
                        _procedimientoService.guardarnumeroprocedimiento(model);

                        totalRows = totalRows + 1;
                    }
                }



                List<Procedimiento> listaEliminados = _procedimientoService.GetByExpedienteNumeroEli(id);

                if (listaEliminados.Count == 0)
                {
                    return false;
                }
                else
                {

                    foreach (Procedimiento l in listaEliminados)
                    {

                        model.ProcedimientoId = l.ProcedimientoId;
                        model.Numero = 0;
                        _procedimientoService.guardarnumeroprocedimiento(model);

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



        [HttpPost]
        public bool guardareditarorden(long id, List<elemento> orden, int tipoorder, UsuarioInfo user)
        {
            Procedimiento model = new Procedimiento();
            try
            {


                int totalRows = 0;


                List<Procedimiento> lista = _procedimientoService.GetByExpedienteNumerotipoordersinEliminar(id, tipoorder);


                if (lista.Count == 0)
                {

                    return false;
                }
                else
                {
                    foreach (Procedimiento l in lista)
                    {

                        model.ProcedimientoId = l.ProcedimientoId;
                        model.Numero = Convert.ToInt32(orden[totalRows].orden.ToString());
                        _procedimientoService.guardarnumeroprocedimiento(model);

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


        //definir infor/*m*/acion
        [HttpPost]
        public bool guardarcategoria(long idexp, long idpro, List<elemento2> orden, UsuarioInfo user)
        {
            ProcedimientoCategoria model = new ProcedimientoCategoria();
            try
            {
                System.Web.HttpContext.Current.Session["orden"] = orden;


                if (idpro != 0)
                {
                    model.ProcedimientoId = idpro;
                    model.ExpedienteId = idexp;
                    _ProcedimientoCategoriaService.Eliminar(model);

                    foreach (elemento2 l in orden)
                    {
                        model.CategoriaProcedimientoId = l.codigo;

                        _ProcedimientoCategoriaService.Save(model);
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


        //definir infor/*m*/acion
        [HttpPost]
        public bool EliminarCategorias0(long idexp, UsuarioInfo user)
        {
            ProcedimientoCategoria model = new ProcedimientoCategoria();
            try
            {

                model.ProcedimientoId = 0;
                model.ExpedienteId = idexp;
                _ProcedimientoCategoriaService.Eliminar(model);



            }
            catch (Exception ex)
            {
                ModelState.AddModelError("editarExpediente", ex.Message);
                _log.Error(ex);
            }
            return true;
        }




        public ActionResult GetAllLikePaginObservacion(Procedimiento filtro, int pageIndex, int pageSize, Expediente exp, UsuarioInfo user)
        {
            try
            {
                ViewBag.Usuario = user;

                Filtros filData = System.Web.HttpContext.Current.Session["modelfiltro"] as Filtros;

                ViewBag.filtro = filData;
                int totalRows = 0;
                List<Procedimiento> lista;
                if (filData == null)
                {
                    lista = _procedimientoService.GetAllLikePaginObservado(filtro, pageIndex, pageSize, ref totalRows);
                }
                else
                {
                    //filtro.Calificacion = filData.Clasificacion;                    
                    //filtro.filtroPprestacionesanuales = filData.Prestacionesanuales;
                    //filtro.filtroPprestacioncosto = filData.PrestacionesCosto;
                    //filtro.filtroPduracion = filData.Duracion;
                    //filtro.filtroPplazoatencion = filData.PlazoAtencion;
                    //filtro.filtroPrequisitos = filData.Requisitos;
                    lista = _procedimientoService.GetAllLikePaginFiltro(filtro, pageIndex, pageSize, ref totalRows, filData);
                }


                Expediente model = new Expediente();
                ViewBag.Usuario = user;

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


                exp = _expedienteService.GetOne(filtro.ExpedienteId);




                lista.ForEach(x => { if (x.UndOrgResponsable == null) x.UndOrgResponsable = new UnidadOrganica(); });

                var categorias = _datoService.GetByTipo(TipoDato.CategoriaProcedimiento);

                //lista.ForEach(x => x.NombreCategoria = categorias.Where(o => (int)o.Tipo == x.CategoriaProcedimientoId).Select(n => n.Nombre).FirstOrDefault());
                lista.ForEach(x => x.NombreCategoria = categorias.Where(o => (int)o.DatoId == x.CategoriaProcedimientoId).Select(n => n.Nombre).FirstOrDefault());

                //foreach (Procedimiento l in lista)
                //{

                //    foreach (Dato d in categorias)
                //    {
                //        if ((int)d.DatoId == l.CategoriaProcedimientoId)
                //        {
                //            l.NombreCategoria = d.Nombre;
                //        }
                //    }
                //}


                ViewBag.Expediente = exp;
                ViewBag.TotalRows = totalRows;
                ViewBag.Row = (pageSize * pageIndex) - pageSize;

                try
                {
                    //model = _expedienteService.GetOne(filtro.ExpedienteId);

                    //ViewBag.User = user;
                    ViewBag.Estadisticas = _expedienteService.GetEstadisticasconteo(filtro);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("editarExpediente", ex.Message);
                    _log.Error(ex);
                }

                var explo = lista.Where(x => x.ExplorarNum == 0 && x.Operacion != OperacionExpediente.Eliminacion && x.Estado != 3).Count();
                if (explo != 0)
                {
                    _AuditoriaService.GenerarNumeracion(lista[0].ExpedienteId);
                }

                return PartialView("_ListaProcedimientoObservacion", lista);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("ListaProcedimientoObservacion", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }

        public ActionResult GetAllLikePaginVerObservacion(Procedimiento filtro, int pageIndex, int pageSize, Expediente exp, UsuarioInfo user)
        {
            try
            {
                ViewBag.Usuario = user;

                Filtros filData = System.Web.HttpContext.Current.Session["modelfiltro"] as Filtros;

                ViewBag.filtro = filData;
                int totalRows = 0;
                List<Procedimiento> lista;
                if (filData == null)
                {
                    lista = _procedimientoService.GetAllLikePaginObservado(filtro, pageIndex, pageSize, ref totalRows);
                }
                else
                {
                    //filtro.Calificacion = filData.Clasificacion;                    
                    //filtro.filtroPprestacionesanuales = filData.Prestacionesanuales;
                    //filtro.filtroPprestacioncosto = filData.PrestacionesCosto;
                    //filtro.filtroPduracion = filData.Duracion;
                    //filtro.filtroPplazoatencion = filData.PlazoAtencion;
                    //filtro.filtroPrequisitos = filData.Requisitos;
                    lista = _procedimientoService.GetAllLikePaginFiltro(filtro, pageIndex, pageSize, ref totalRows, filData);
                }




                lista.ForEach(x => { if (x.UndOrgResponsable == null) x.UndOrgResponsable = new UnidadOrganica(); });

                var categorias = _datoService.GetByTipo(TipoDato.CategoriaProcedimiento);

                //lista.ForEach(x => x.NombreCategoria = categorias.Where(o => (int)o.Tipo == x.CategoriaProcedimientoId).Select(n => n.Nombre).FirstOrDefault());
                lista.ForEach(x => x.NombreCategoria = categorias.Where(o => (int)o.DatoId == x.CategoriaProcedimientoId).Select(n => n.Nombre).FirstOrDefault());

                //foreach (Procedimiento l in lista)
                //{

                //    foreach (Dato d in categorias)
                //    {
                //        if ((int)d.DatoId == l.CategoriaProcedimientoId)
                //        {
                //            l.NombreCategoria = d.Nombre;
                //        }
                //    }
                //}


                ViewBag.Expediente = exp;
                ViewBag.TotalRows = totalRows;
                ViewBag.Row = (pageSize * pageIndex) - pageSize;


                var explo = lista.Where(x => x.ExplorarNum == 0 && x.Operacion != OperacionExpediente.Eliminacion && x.Estado != 3).Count();
                if (explo != 0)
                {
                    _AuditoriaService.GenerarNumeracion(lista[0].ExpedienteId);
                }

                return PartialView("_ListaProcedimientoVerObservacion", lista);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("ListaProcedimientoVerObservacion", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }

        public ActionResult SelectMasivo(bool multi, string expedienteId, int datocampo, string fnAdd, string texto1, string texto2)
        {
            try
            {
                ViewBag.Multi = multi;
                ViewBag.expedienteId = expedienteId;
                ViewBag.datocampo = datocampo;
                ViewBag.FnAdd = fnAdd;
                ViewBag.Texto1 = texto1;
                ViewBag.Texto2 = texto2;

                //MaestroFijo modelMaestroFijo = _maestroFijoService.GetOneByEntidad(user.EntidadId);

                return PartialView("_SelectMasivo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult SelectCopiarAsme(bool multi, long ExpedienteId, long TablaAsmeId, long ProcedimientoId)
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



        public string GetAllLikePaginMasivoAsme(TablaAsme filtro, int pageIndex, int pageSize, long tablaAsmeId, Expediente exp, UsuarioInfo user)
        {
            ViewBag.Usuario = user;

            int totalRows = 0;
            List<TablaAsme> lista;
            lista = _procedimientoService.GetAllLikePaginFiltroAsme(filtro, pageIndex, pageSize, tablaAsmeId, ref totalRows);

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    TablaAsmeId = x.TablaAsmeId,
                    CodigoCorto = x.Procedimiento.CodigoCorto,
                    Denominacion = x.Procedimiento.Denominacion,
                    Descripcion = x.Descripcion,
                    ProcedimientoId = x.ProcedimientoId,

                }),
                totalRows = totalRows
            });
        }





        public string GetAllLikePaginMasivoObservacion(Procedimiento filtro, int pageIndex, int pageSize, Expediente exp, UsuarioInfo user)
        {
            ViewBag.Usuario = user;
            Filtros filData = System.Web.HttpContext.Current.Session["modelfiltro"] as Filtros;

            ViewBag.filtro = filData;
            int totalRows = 0;
            List<Procedimiento> lista;
            if (filData == null || filData.Clasificacion == 0)
            {
                lista = _procedimientoService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);
            }
            else
            {
                filtro.Calificacion = filData.Clasificacion;


                lista = _procedimientoService.GetAllLikePaginFiltro(filtro, pageIndex, pageSize, ref totalRows, filData);
            }

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    ProcedimientoId = x.ProcedimientoId,
                    Codigo = x.Codigo,
                    Denominacion = x.Denominacion,
                    TipoProcedimiento = x.TipoProcedimiento,
                    CategoriaProcedimientoId = x.CategoriaProcedimientoId,

                }),
                totalRows = totalRows
            });
        }


        public string GetAllLikePaginPAACR(Procedimiento filtro, int pageIndex, int pageSize, Expediente exp, UsuarioInfo user)
        {
            ViewBag.Usuario = user;
            Filtros filData = System.Web.HttpContext.Current.Session["modelfiltro"] as Filtros;

            ViewBag.filtro = filData;
            int totalRows = 0;
            List<Procedimiento> lista = _procedimientoService.GetAllLikePaginACR(filtro, pageIndex, pageSize, ref totalRows);


            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    ProcedimientoId = x.ProcedimientoId,
                    CodigoCorto = x.CodigoCorto,
                    Denominacion = x.Denominacion,
                    TipoProcedimiento = x.TipoProcedimiento,
                    CategoriaProcedimientoId = x.CategoriaProcedimientoId,

                }),
                totalRows = totalRows
            });
        }


        public ActionResult InformacionAdicionalACR(long id, long cod, UsuarioInfo user)
        {
            try
            {
                Procedimiento model = new Procedimiento();

                model = _procedimientoService.GetOne(id);

                ViewBag.User = user;

                ViewBag.codigo = cod;

                return PartialView("_InformacionAdicionalACR", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        public ActionResult GetAllLikePaginByExpediente(Procedimiento filtro, int pageIndex, int pageSize, Expediente exp, UsuarioInfo user)
        {
            try
            {
                ViewBag.Usuario = user;

                int totalRows = 0;

                var Proc = _procedimientoService.GetOne(filtro.ProcedimientoId);

                filtro.FiltroTipoProcedimiento = (short)Proc.TipoProcedimientoId;

                List<Procedimiento> lista = _procedimientoService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

                ViewBag.Expediente = exp;
                ViewBag.TotalRows = totalRows;
                ViewBag.Row = (pageSize * pageIndex) - pageSize;
                return Json(new
                {
                    data = lista.Select(x => new
                    {
                        ProcedimientoId = x.ProcedimientoId,
                        Codigo = x.Codigo,
                        CodigoCorto = x.CodigoCorto,
                        Denominacion = x.Denominacion,
                        Tipo = (int)x.TipoProcedimiento
                    }).ToList(),
                    total = totalRows
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }

        public ActionResult Nuevo(long id, UsuarioInfo user)
        {
            Expediente exp = _expedienteService.GetOne(id);
            ViewBag.Entidad = exp.Entidad;

            Procedimiento model = new Procedimiento();
            model.ExpedienteId = id;

            var categorias = _datoService.GetByTipo(TipoDato.CategoriaProcedimiento);
            categorias.Insert(0, new Dato() { MetaDatoId = 0, Nombre = "- SELECCIONAR -" });
            ViewBag.ListaCategoria = categorias.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.MetaDatoId.ToString() }).ToList();

            var subtipo = _datoService.GetByTipo(TipoDato.TipoProcedimiento);
            subtipo.Insert(0, new Dato() { MetaDatoId = 0, Nombre = "- SELECCIONAR -" });
            ViewBag.ListaSubTipo = subtipo.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.MetaDatoId.ToString() }).ToList();

            var tipos = new List<SelectListItem>() {
                new SelectListItem() { Value = "1", Text = "PROCEDIMIENTO ADMINISTRATIVO" },
                new SelectListItem() { Value = "2", Text = "SERVICIO EXCLUSIVO" }
            };
            ViewBag.ListTipo = tipos;
            ViewBag.Usuario = user;



            return View(model);
        }

        public ActionResult EditarNuevo(long id, UsuarioInfo user)
        {

            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 20);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/

            ViewBag.User = user;

            Procedimiento model = _procedimientoService.GetOne(id);

            List<ProcedimientoCategoria> procat = _ProcedimientoCategoriaService.Lsitaprocedimientocategoria(model.ExpedienteId, id);

            Expediente exp = _expedienteService.GetOne(model.ExpedienteId);
            ViewBag.Entidad = exp.Entidad;

            if (model.UndOrgResponsableId2 != 0) model.UndOrgResponsable.Nombre2 = _unidadOrganicaService.GetOne(model.UndOrgResponsableId2).Nombre;
            if (model.UndOrgResponsableId3 != 0) model.UndOrgResponsable.Nombre3 = _unidadOrganicaService.GetOne(model.UndOrgResponsableId3).Nombre;
            if (model.UndOrgResponsableId4 != 0) model.UndOrgResponsable.Nombre4 = _unidadOrganicaService.GetOne(model.UndOrgResponsableId4).Nombre;
            if (model.UndOrgResponsableId5 != 0) model.UndOrgResponsable.Nombre5 = _unidadOrganicaService.GetOne(model.UndOrgResponsableId5).Nombre;



            var categorias = _datoService.GetByTipo(TipoDato.CategoriaProcedimiento);
            categorias.Insert(0, new Dato() { MetaDatoId = 0, Nombre = "- SELECCIONAR -" });
            ViewBag.ListaCategoria = categorias.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.MetaDatoId.ToString() }).ToList();

            var subtipo = _datoService.GetByTipo(TipoDato.TipoProcedimiento);
            subtipo.Insert(0, new Dato() { MetaDatoId = 0, Nombre = "- SELECCIONAR -" });
            ViewBag.ListaSubTipo = subtipo.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.MetaDatoId.ToString() }).ToList();

            if (model.TipoProcedimiento == TipoProcedimiento.EstandarServicio)
            {

                var tipos = new List<SelectListItem>() {
                new SelectListItem() { Value = "1", Text = "PROCEDIMIENTO ADMINISTRATIVO" },
                new SelectListItem() { Value = "5", Text = "SERVICIO EXCLUSIVO" }
            };
                ViewBag.ListTipo = tipos;

            }
            else
            {
                var tipos = new List<SelectListItem>() {
                new SelectListItem() { Value = "1", Text = "PROCEDIMIENTO ADMINISTRATIVO" },
                new SelectListItem() { Value = "2", Text = "SERVICIO EXCLUSIVO" }
                };
                ViewBag.ListTipo = tipos;
            }


            ViewBag.Expediente = exp;

            if (model.ProcedimientoId != 0)
            {
                List<ProcedimientoUndOrgResponsable> ProcedimientoUndOrgResponsable = _ProcedimientoUndOrgResponsableService.GetAll(model.ProcedimientoId);

                ViewBag.ProcedimientoUndOrgResponsable = JsonConvert.SerializeObject(
                   ProcedimientoUndOrgResponsable
                   .Select(x => new
                   {
                       ProcedimientoUndOrgResponsableID = x.ProcedimientoUndOrgResponsableID,
                       ProcedimientoId = x.ProcedimientoId,
                       UndOrgResponsableId2 = x.UndOrgResponsableId2,
                       NombreUndOrg = _unidadOrganicaService.GetOne(x.UndOrgResponsableId2).Nombre,
                   }).ToList()
               );
            }
            return View(model);
        }

        public ActionResult NuevoCodigo(long id, UsuarioInfo user)
        {

            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 20);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/

            ViewBag.User = user;

            Procedimiento model = _procedimientoService.GetOne(id);

            Expediente exp = _expedienteService.GetOne(model.ExpedienteId);
            ViewBag.Entidad = exp.Entidad;

            var categorias = _datoService.GetByTipo(TipoDato.CategoriaProcedimiento);
            categorias.Insert(0, new Dato() { MetaDatoId = 0, Nombre = "- SELECCIONAR -" });
            ViewBag.ListaCategoria = categorias.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.MetaDatoId.ToString() }).ToList();

            var subtipo = _datoService.GetByTipo(TipoDato.TipoProcedimiento);
            subtipo.Insert(0, new Dato() { MetaDatoId = 0, Nombre = "- SELECCIONAR -" });
            ViewBag.ListaSubTipo = subtipo.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.MetaDatoId.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(Procedimiento model, UsuarioInfo user)
        {
            try
            {


                List<elemento2> orden = System.Web.HttpContext.Current.Session["orden"] as List<elemento2>;

                Expediente exp = _expedienteService.GetOne(model.ExpedienteId);
                ViewBag.Entidad = exp.Entidad;

                if (exp.Entidad.TipoTupa == TipoTupa.Estandar)
                {
                    if (model.TipoProcedimiento == TipoProcedimiento.Servicio)
                    {
                        model.TipoProcedimiento = TipoProcedimiento.EstandarServicio;
                    }
                    else
                    {
                        model.TipoProcedimiento = TipoProcedimiento.Estandar;
                    }

                }

                string codigoCorto = "";


                List<Procedimiento> lista = _procedimientoService.GetByExpedienteNumero(model.ExpedienteId);

                model.Numero = lista.Count() + 1;

                for (var cod = 0; cod < 1; cod++)
                {
                    var codvalor = NewCodigo(model, user, ref codigoCorto);
                    var codBuscar = _procedimientoService.codvalor(Convert.ToString(codvalor));

                    if (codBuscar.Count() == 0)
                    {
                        model.Codigo = codvalor;
                    }
                    else
                    {
                        cod = 0;
                    }
                }

                //model.Codigo = NewCodigo(model, user, ref codigoCorto);
                model.Operacion = OperacionExpediente.Nuevo;
                model.Calificacion = CalificacionProcedimiento.Automatica;
                model.TipoAtencionId = 10;
                model.CodigoACR = "0";
                model.DenominacionAnterior = model.Denominacion;

                //FVN Inicio
                model.FlagCopiarProcedimiento = 1;
                //FVN Fin

                model.UserCreacion = user.NroDocumento;
                model.FecCreacion = DateTime.Now;
                model.UserModificacion = user.NroDocumento;
                model.FecModificacion = DateTime.Now;
                UIT objuit = _UITService.LsitaGetOne();
                model.TablaAsme = new List<TablaAsme>()
                {
                    new TablaAsme() {
                        Codigo = model.Codigo + "-01",
                        Descripcion = "",
                        UserCreacion = user.NroDocumento,
                        FecCreacion = DateTime.Now,
                        UserModificacion = user.NroDocumento,
                        FecModificacion = DateTime.Now,
                        UITID=objuit.UITID

                    }
                };

                model.BaseLegal = new BaseLegal();
                model.ProcedimientoDatoAdicional = new List<ProcedimientoDatoAdicional>();
                model.ProcedimientoSede = new List<ProcedimientoSede>();
                model.NotaCiudadano = new List<NotaCiudadano>();
                model.PasoSeguir = new List<PasoSeguir>();

                model.Expediente = null;
                model.UndOrgResponsable = null;

                MaestroFijo fijo = _maestroFijoService.GetOneByEntidad(exp.EntidadId);
                if (fijo != null)
                {
                    if (!string.IsNullOrEmpty(fijo.Telefono)) model.Telefono = fijo.Telefono;
                    if (!string.IsNullOrEmpty(fijo.Anexo)) model.Anexo = fijo.Anexo;
                    if (!string.IsNullOrEmpty(fijo.Correo)) model.Correo = fijo.Correo;

                    if (fijo.MaestroFijoSede != null)
                        if (fijo.MaestroFijoSede.Count() > 0)
                            fijo.MaestroFijoSede.Where(x => (x.Mostrar ?? false) == true).ToList().ForEach(sede => model.ProcedimientoSede.Add(new ProcedimientoSede()
                            {
                                SedeId = sede.SedeId
                            }));

                    if (fijo.MaestroFijoSede != null)
                        if (fijo.MaestroFijoSede.Count() > 0)
                        {
                            foreach (MaestroFijoSede mf in fijo.MaestroFijoSede)
                            {
                                if ((mf.Mostrar ?? false) == true)
                                    if (mf.MaestroFijoUndOrgRecepcionDocum != null)
                                    {
                                        if (mf.MaestroFijoUndOrgRecepcionDocum.Count() > 0)
                                            foreach (MaestroFijoUndOrgRecepcionDocum mfu in mf.MaestroFijoUndOrgRecepcionDocum)
                                            {
                                                ProcedimientoSede ps = model.ProcedimientoSede.Where(x => x.SedeId == mfu.SedeId).FirstOrDefault();

                                                if (ps != null)
                                                {
                                                    if (ps.UndOrgRecepcionDocumentos == null) ps.UndOrgRecepcionDocumentos = new List<UndOrgRecepcionDocumentos>();

                                                    ps.UndOrgRecepcionDocumentos.Add(new UndOrgRecepcionDocumentos()
                                                    {
                                                        UnidadOrganicaId = mfu.UnidadOrganicaId,
                                                        SedeId = mfu.SedeId,
                                                    });
                                                }
                                            }
                                    }
                            }
                        }

                    if (fijo.MaestroFijoDatoAdicional != null)
                        if (fijo.MaestroFijoDatoAdicional.Count() > 0)
                            fijo.MaestroFijoDatoAdicional.ForEach(da => model.ProcedimientoDatoAdicional.Add(new ProcedimientoDatoAdicional()
                            {
                                MetaDatoId = da.MetaDatoId,
                                Comentario = da.Comentario
                            }));

                    if (fijo.MaestroFijoPasoSeguir != null)
                        if (fijo.MaestroFijoPasoSeguir.Count() > 0)
                            fijo.MaestroFijoPasoSeguir.ForEach(p => model.PasoSeguir.Add(new PasoSeguir()
                            {
                                PasoSeguirId = p.PasoSeguirId,
                                Descripcion = p.Descripcion
                            }));
                    //if (fijo.MaestroFijoNotaCiudadano != null)
                    //    if (fijo.MaestroFijoNotaCiudadano.Count() > 0)
                    //        fijo.MaestroFijoNotaCiudadano.ForEach(p => model.NotaCiudadano.Add(new NotaCiudadano()
                    //        {
                    //            Nota = p.Comentario,
                    //            Orden = p.Orden,
                    //            NotaCiudadanoId = p.NotaCiudadanoId,
                    //            TipoNotaId = _metaDatoService.GetOne(p.MetaDatoTipoNotaId).MetaDatoId
                    //        }));
                }

                /*Johan Dongo*/

                for (var cod2 = 0; cod2 < 1; cod2++)
                {
                    var codigovalor = codigoCorto + exp.Entidad.EntidadId.ToString().PadRight(4, '0') + Guid.NewGuid().ToString().Substring(1, 4).ToUpper();
                    var codigoBuscar = _procedimientoService.codigocorto(codigovalor);

                    if (codigoBuscar.Count() == 0)
                    {
                        model.CodigoCorto = codigovalor;
                    }
                    else
                    {
                        cod2 = 0;
                    }
                }



                _procedimientoService.Save(model);


                if (orden != null)
                {
                    /**añadir categorias**///
                    ProcedimientoCategoria tmp = new ProcedimientoCategoria();
                    tmp.ProcedimientoId = model.ProcedimientoId;
                    tmp.ExpedienteId = model.ExpedienteId;

                    foreach (elemento2 l in orden)
                    {
                        tmp.CategoriaProcedimientoId = l.codigo;
                        _ProcedimientoCategoriaService.Save(tmp);
                    }
                }

                //if (model.ProcedimientoUndOrgResponsable != null)
                //{
                //    /**añadir categorias**///
                //    ProcedimientoUndOrgResponsable tmppro = new ProcedimientoUndOrgResponsable();
                //    tmppro.ProcedimientoId = model.ProcedimientoId; 

                //    foreach (ProcedimientoUndOrgResponsable l in model.ProcedimientoUndOrgResponsable)
                //    {
                //        tmppro.ProcedimientoId = model.ProcedimientoId;
                //        tmppro.UndOrgResponsableId2 = l.UndOrgResponsableId2;
                //        //tmppro.ProcedimientoUndOrgResponsableID = l.ProcedimientoUndOrgResponsableID;
                //        _ProcedimientoUndOrgResponsableService.Save(tmppro);
                //    }
                //}

                //List<ProcedimientoCategoria> pro = _ProcedimientoCategoriaService.Lsitaprocedimientocategoriavalor0(model.ExpedienteId);

                //ProcedimientoCategoria tmp = new ProcedimientoCategoria();

                //tmp.ProcedimientoId = 0;
                //tmp.ExpedienteId = model.ExpedienteId;
                //_ProcedimientoCategoriaService.Eliminar(tmp);


                //foreach (ProcedimientoCategoria l in pro)
                //{
                //    tmp.ProcedimientoId = model.ProcedimientoId;
                //    tmp.CategoriaProcedimientoId = l.CategoriaProcedimientoId;
                //    tmp.ExpedienteId = model.ExpedienteId;

                //    _ProcedimientoCategoriaService.Save(tmp);
                //}  

                return Json(new { exito = true, ExpedienteId = model.ExpedienteId.ToString() }, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Editar", "Expediente", new { area = "Simplificacion", id = model.ExpedienteId.ToString() });
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ModelState.AddModelError("NuevoProcedimiento", ex.Message);
                _log.Error(ex);
            }


            var categorias = _datoService.GetByTipo(TipoDato.CategoriaProcedimiento);
            categorias.Insert(0, new Dato() { MetaDatoId = 0, Nombre = "- SELECCIONAR -" });
            ViewBag.ListaCategoria = categorias.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.MetaDatoId.ToString() }).ToList();

            var subtipo = _datoService.GetByTipo(TipoDato.TipoProcedimiento);
            subtipo.Insert(0, new Dato() { MetaDatoId = 0, Nombre = "- SELECCIONAR -" });
            ViewBag.ListaSubTipo = subtipo.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.MetaDatoId.ToString() }).ToList();

            var tipos = new List<SelectListItem>() {
                new SelectListItem() { Value = "1", Text = "PROCEDIMIENTO ADMINISTRATIVO" },
                new SelectListItem() { Value = "2", Text = "SERVICIO EXCLUSIVO" }
            };
            ViewBag.ListTipo = tipos;




            return View(model);
        }



        [HttpPost]
        public ActionResult EditarNuevo(Procedimiento model, UsuarioInfo user)
        {
            try
            {
                Expediente exp = _expedienteService.GetOne(model.ExpedienteId);
                ViewBag.Entidad = exp.Entidad;

                if (model.ProcedimientoUndOrgResponsable != null)
                {
                    /**añadir categorias**///
                    ProcedimientoUndOrgResponsable tmppro = new ProcedimientoUndOrgResponsable();
                    var eliminarpro = _ProcedimientoUndOrgResponsableService.GetAll(model.ProcedimientoId);

                    foreach (ProcedimientoUndOrgResponsable l in eliminarpro)
                    {
                        _ProcedimientoUndOrgResponsableService.Eliminar(l.ProcedimientoUndOrgResponsableID);
                    }
                }

                _procedimientoService.modificarprocedimiento(model);

                return RedirectToAction("Editar", "Expediente", new { area = "Simplificacion", id = model.ExpedienteId.ToString() });
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ModelState.AddModelError("NuevoProcedimiento", ex.Message);
                _log.Error(ex);
            }


            var categorias = _datoService.GetByTipo(TipoDato.CategoriaProcedimiento);
            categorias.Insert(0, new Dato() { MetaDatoId = 0, Nombre = "- SELECCIONAR -" });
            ViewBag.ListaCategoria = categorias.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.MetaDatoId.ToString() }).ToList();

            var subtipo = _datoService.GetByTipo(TipoDato.TipoProcedimiento);
            subtipo.Insert(0, new Dato() { MetaDatoId = 0, Nombre = "- SELECCIONAR -" });
            ViewBag.ListaSubTipo = subtipo.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.MetaDatoId.ToString() }).ToList();

            var tipos = new List<SelectListItem>() {
                new SelectListItem() { Value = "1", Text = "PROCEDIMIENTO ADMINISTRATIVO" },
                new SelectListItem() { Value = "2", Text = "SERVICIO EXCLUSIVO" }
            };
            ViewBag.ListTipo = tipos;

            return View(model);
        }

        private Procedimiento NuevoCopia(Procedimiento model, UsuarioInfo user)
        {
            try
            {

                Expediente exp = _expedienteService.GetOne(model.ExpedienteId);
                ViewBag.Entidad = exp.Entidad;

                if (exp.Entidad.TipoTupa == TipoTupa.Estandar)
                {
                    model.TipoProcedimiento = TipoProcedimiento.Estandar;
                }

                string codigoCorto = "";

                model.Codigo = NewCodigo(model, user, ref codigoCorto);
                model.Operacion = OperacionExpediente.Nuevo;
                model.Calificacion = CalificacionProcedimiento.Automatica;
                model.TipoAtencionId = 10;

                model.UserCreacion = user.NroDocumento;
                model.FecCreacion = DateTime.Now;
                model.UserModificacion = user.NroDocumento;
                model.FecModificacion = DateTime.Now;
                model.DenominacionAnterior = model.Denominacion;


                UIT objuit = _UITService.LsitaGetOne();
                //model.TablaAsme = new List<TablaAsme>()
                //{
                //    new TablaAsme() {
                //        Codigo = model.Codigo + "-01",
                //        Descripcion = "-",
                //        UserCreacion = user.NroDocumento,
                //        FecCreacion = DateTime.Now,
                //        UserModificacion = user.NroDocumento,
                //        FecModificacion = DateTime.Now,
                //        UITID=objuit.UITID,

                //    }
                //};




                Procedimiento tmp = new Procedimiento();

                model.CopyPropertiesTo(tmp);


                tmp.BaseLegal = new BaseLegal();
                tmp.ProcedimientoDatoAdicional = new List<ProcedimientoDatoAdicional>();
                tmp.ProcedimientoSede = new List<ProcedimientoSede>();
                tmp.PasoSeguir = new List<PasoSeguir>();
                tmp.NotaCiudadano = new List<NotaCiudadano>();

                tmp.Expediente = null;
                tmp.UndOrgResponsable = null;

                tmp.Editado = false;


                tmp.Requisito = new List<Requisito>();
                tmp.TablaAsme = new List<TablaAsme>();




                tmp.CodigoCorto = codigoCorto + exp.Entidad.EntidadId.ToString().PadRight(4, '0') + Guid.NewGuid().ToString().Substring(1, 4).ToUpper();
                tmp.Es_Copia = true;
                _procedimientoService.Save(tmp);

                //model.ProcedimientoId = tmp.ProcedimientoId;
                //model.BaseLegal = tmp.BaseLegal;
                //model.BaseLegalId = tmp.BaseLegalId;


                //tmp.Es_Copia = true;
                //tmp.ProcedimientoSede = new List<ProcedimientoSede>();

                //foreach (ProcedimientoSede p in model.ProcedimientoSede)
                //{
                //    tmp.ProcedimientoSede.Add(new ProcedimientoSede()
                //    {
                //        ProcedimientoId = tmp.ProcedimientoId,
                //        SedeId = p.SedeId
                //    });
                //}

                //foreach (ProcedimientoSede s in tmp.ProcedimientoSede)
                //{

                //    ProcedimientoSede obj = model.ProcedimientoSede.Where(p => p.SedeId == s.SedeId).FirstOrDefault();

                //    if (obj != null)
                //    {
                //        s.UndOrgRecepcionDocumentos = obj.UndOrgRecepcionDocumentos;
                //    }
                //}

                //tmp.ProcedimientoSede.ForEach(r => r.UndOrgRecepcionDocumentos.ForEach(p => p.ProcedimientoId = 0));


                //tmp.ProcedimientoDatoAdicional = new List<ProcedimientoDatoAdicional>();
                //foreach (ProcedimientoDatoAdicional p in model.ProcedimientoDatoAdicional)
                //{
                //    tmp.ProcedimientoDatoAdicional.Add(new ProcedimientoDatoAdicional()
                //    {
                //        MetaDatoId = p.MetaDatoId,
                //        Comentario = p.Comentario,
                //        ProcedimientoId = tmp.ProcedimientoId
                //    });
                //}


                //tmp.PasoSeguir = new List<PasoSeguir>();
                //foreach (PasoSeguir p in model.PasoSeguir)
                //{
                //    tmp.PasoSeguir.Add(new PasoSeguir()
                //    {
                //        PasoSeguirId = p.PasoSeguirId,
                //        Descripcion = p.Descripcion
                //    });
                //}



                //tmp.NotaCiudadano = new List<NotaCiudadano>();
                //foreach (NotaCiudadano p in model.NotaCiudadano)
                //{
                //    tmp.NotaCiudadano.Add(new NotaCiudadano()
                //    {
                //        Nota = p.Nota,
                //        Orden = p.Orden,
                //        NotaCiudadanoId = p.NotaCiudadanoId,
                //        TipoNotaId = p.TipoNotaId
                //    });
                //}


                /*Johan Dongo*/



                _procedimientoService.SaveCopia(tmp);

                return tmp;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("NuevoProcedimiento", ex.Message);
                _log.Error(ex);

                return null;
            }
        }



        [HttpPost]
        public ActionResult NuevoCodigo(Procedimiento model, UsuarioInfo user)
        {
            Expediente exp = _expedienteService.GetOne(model.ExpedienteId);
            try
            {
                string codigoCorto = "";
                //int codig= _procedimientoService.GetByExpedienteNumero(model.ExpedienteId).Max(x=>x.Numero);
                //model.Numero = codig + 1;
                model.Codigo = NewCodigo(model, user, ref codigoCorto);
                model.CodigoCorto = codigoCorto + exp.Entidad.EntidadId.ToString().PadRight(4, '0') + Guid.NewGuid().ToString().Substring(1, 4).ToUpper();


                _procedimientoService.GuardarNuevoCodigo(model);
                List<elemento2> orden = System.Web.HttpContext.Current.Session["orden"] as List<elemento2>;
                if (orden != null)
                {   /**añadir categorias**///
                    ProcedimientoCategoria tmp = new ProcedimientoCategoria();
                    tmp.ProcedimientoId = model.ProcedimientoId;
                    tmp.ExpedienteId = model.ExpedienteId;
                    foreach (elemento2 l in orden)
                    {
                        tmp.CategoriaProcedimientoId = l.codigo;
                        _ProcedimientoCategoriaService.Save(tmp);
                    }
                }
                return RedirectToAction("DatosGenerales", "Procedimiento", new { area = "Simplificacion", id = model.ProcedimientoId.ToString() });
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ModelState.AddModelError("", ex.Message);
                _log.Error(ex);
            }
            ViewBag.Entidad = exp.Entidad;
            var categorias = _datoService.GetByTipo(TipoDato.CategoriaProcedimiento);
            categorias.Insert(0, new Dato() { MetaDatoId = 0, Nombre = "- SELECCIONAR -" });
            ViewBag.ListaCategoria = categorias.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.MetaDatoId.ToString() }).ToList();
            var subtipo = _datoService.GetByTipo(TipoDato.TipoProcedimiento);
            subtipo.Insert(0, new Dato() { MetaDatoId = 0, Nombre = "- SELECCIONAR -" });
            ViewBag.ListaSubTipo = subtipo.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.MetaDatoId.ToString() }).ToList();
            return View(model);
        }

        public string NewCodigo(Procedimiento model, UsuarioInfo user, ref string codigoCorto)
        {
            try
            {
                var categoria = _datoService.GetOne(model.CategoriaProcedimientoId.Value, TipoDato.CategoriaProcedimiento);
                var tipo = _datoService.GetOne(model.TipoProcedimientoId.Value, TipoDato.TipoProcedimiento);

                int orden = _procedimientoService.GetOrdenNewProcByEntidad(user.EntidadId);
                string prefijo = "";
                switch (model.TipoProcedimiento)
                {
                    case TipoProcedimiento.Procedimiento: prefijo = "PA"; break;
                    case TipoProcedimiento.Servicio: prefijo = "SE"; break;
                    case TipoProcedimiento.Estandar: prefijo = "PE"; break;
                    case TipoProcedimiento.EstandarServicio: prefijo = "ES"; break;
                }
                var cod = Guid.NewGuid().ToString().Substring(1, 4).ToUpper();
                string codigo = string.Format("{0}{1}{2}{3}{4}{5:D4}", prefijo, user.CodEntidad, categoria.Codigo, tipo.Codigo, cod, orden);
                codigo = codigo.Replace(".", "");

                codigoCorto = prefijo;

                return codigo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Copia(long id, long ExpedienteId, UsuarioInfo user)
        {
            Expediente modelex = new Expediente();
            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Copia Procedimiento";
                objauditoria.Pantalla = "Procedimiento";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/

                modelex = _expedienteService.GetOne(ExpedienteId);
                CopiaProcedimientoViewModel model = new CopiaProcedimientoViewModel();
                model.ProcedimientoOrigenId = id;
                model.ExpedienteId = ExpedienteId;
                model.TipoExpediente = modelex.TipoExpediente;

                Procedimiento org = _procedimientoService.GetOne(id);
                ViewBag.NomProcOrigen = string.Format("{0} - {1}", org.CodigoCorto, org.Denominacion);
                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
                return PartialView("_Copia", model);


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _log.Error(ex);
                return PartialView("_Error");
            }
        }

        [HttpPost]
        public ActionResult CopiarProcedimiento(CopiaProcedimientoViewModel model, UsuarioInfo user)
        {
            try
            {

                long idexp = 0;
                List<DatoCopia> lstCopia = new List<DatoCopia>();
                if (model.Todo) lstCopia.Add(DatoCopia.Todo);
                if (model.InfoAlCiudadano) lstCopia.Add(DatoCopia.InfoAlCiudadano);
                if (model.InfoBasica) lstCopia.Add(DatoCopia.InfoBasica);
                if (model.TipoAtencion) lstCopia.Add(DatoCopia.TipoAtencion);
                if (model.Requisito) lstCopia.Add(DatoCopia.Requisito);
                if (model.BaseLegal) lstCopia.Add(DatoCopia.BaseLegal);
                if (model.DatosGeneralesYSTL) lstCopia.Add(DatoCopia.DatosGeneralesYSTL);
                if (model.SustentoCalificacion) lstCopia.Add(DatoCopia.SustentoCalificacion);
                if (model.SustentoRequisito) lstCopia.Add(DatoCopia.SustentoRequisito);
                if (model.TablaASME) lstCopia.Add(DatoCopia.TablaASME);
                if (model.Nuevo) lstCopia.Add(DatoCopia.Nuevo);

                if (!model.Nuevo)
                {
                    Procedimiento proc = _procedimientoService.GetOne(model.ProcedimientoOrigenId);


                    Procedimiento pCopia = new Procedimiento();

                    proc.CopyPropertiesTo(pCopia);
                    idexp = proc.ExpedienteId;
                    Expediente exp = _expedienteService.GetOne(proc.ExpedienteId);

                    //FVN Inicio
                    if (pCopia.FlagCopiarProcedimiento == 0)
                    {
                        //return Json(new { valid = false, mensaje = "Debe modificar la denominacion del procediiento antes de copiar." });
                        ModelState.AddModelError("", "Antes de Copiar debe modificar la denominacion : " + pCopia.Denominacion + " en la seccion de Datos Generales");
                        return PartialView("_Error");
                    }

                    pCopia.FlagCopiarProcedimiento = 0;
                    //FVN Fin

                    List<Procedimiento> lista = _procedimientoService.GetByExpedienteNumero(proc.ExpedienteId);

                    pCopia.Numero = lista.Count() + 1;
                    pCopia.ProcedimientoId = 0;
                    pCopia.Denominacion = "Copia desde el procedimiento con codigo " + pCopia.CodigoCorto;
                    pCopia.CodigoCorto = null;
                    pCopia.Codigo = null;

                    Procedimiento result = NuevoCopia(pCopia, user);
                    string cod = "";

                    model.ProcedimientoDestinoId = result.ProcedimientoId;
                    model.CodigoCopia = NewCodigo(result, user, ref cod);
                    model.CodigoCortoCopia = cod + exp.Entidad.EntidadId.ToString().PadRight(4, '0') + Guid.NewGuid().ToString().Substring(1, 4).ToUpper();

                    //pCopia.DenominacionAnterior = pCopia.Denominacion;
                }

                if (model.ProcedimientoDestinoId > 0)
                {
                    List<Procedimiento> lista = _procedimientoService.GetByExpedienteNumero(idexp);

                    model.Numero = lista.Count() + 1;

                    List<string> mensajes = _procedimientoService.CopiarDatosProcedimiento(model.ProcedimientoOrigenId, model.ProcedimientoDestinoId, lstCopia, model.CodigoCopia, model.CodigoCortoCopia);
                    //VERIFICAR

                    //if (mensajes.Count() > 0)
                    if (mensajes.Count() == 2)
                    {

                        _procedimientoService.Eliminar(model.ProcedimientoDestinoId);
                        if (!model.Nuevo)
                            CambiarOperacion(OperacionExpediente.Eliminacion, model.ProcedimientoDestinoId);

                        if (model.ProcedimientoDestinoId == 0)
                            ModelState.AddModelError("", "Debe de seleccionar un procedimiento de Destino.");
                        else
                            mensajes.ForEach(m => ModelState.AddModelError("", m));
                        //Response.StatusCode = (int)HttpStatusCode.BadRequest;

                        return PartialView("_Error");
                    }
                    else
                    {
                        //    Procedimiento proce = new Procedimiento();
                        //int totalRows = 1;
                        //List<Procedimiento> lista = _procedimientoService.GetByExpedienteNumero(idexp);
                        //foreach (Procedimiento l in lista)
                        //{

                        //        proce.ProcedimientoId = l.ProcedimientoId;
                        //        proce.Numero = totalRows;
                        //    _procedimientoService.guardarnumeroprocedimiento(proce);

                        //    totalRows = totalRows + 1;
                        //}
                        return Json(new { valid = true, mensaje = "Los datos se copiaron correctamente." });
                    }
                }
                else
                {
                    return Json(new { valid = true, mensaje = "Ocurrio un error al crear el procedimiento de destino en el sistema." });
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _log.Error(ex);
                return PartialView("_Error");
            }
        }
        [HttpPost]
        public ActionResult CopiarProcedimientoEli(CopiaProcedimientoViewModel model, UsuarioInfo user)
        {
            try
            {

                long idexp = 0;

                List<DatoCopia> lstCopia = new List<DatoCopia>();
                if (model.Todo) lstCopia.Add(DatoCopia.Todo);
                if (model.InfoAlCiudadano) lstCopia.Add(DatoCopia.InfoAlCiudadano);
                if (model.InfoBasica) lstCopia.Add(DatoCopia.InfoBasica);
                if (model.TipoAtencion) lstCopia.Add(DatoCopia.TipoAtencion);
                if (model.Requisito) lstCopia.Add(DatoCopia.Requisito);
                if (model.BaseLegal) lstCopia.Add(DatoCopia.BaseLegal);
                if (model.DatosGeneralesYSTL) lstCopia.Add(DatoCopia.DatosGeneralesYSTL);
                if (model.SustentoCalificacion) lstCopia.Add(DatoCopia.SustentoCalificacion);
                if (model.SustentoRequisito) lstCopia.Add(DatoCopia.SustentoRequisito);
                if (model.TablaASME) lstCopia.Add(DatoCopia.TablaASME);
                if (model.Nuevo) lstCopia.Add(DatoCopia.Nuevo);

                if (!model.Nuevo)
                {
                    Procedimiento proc = _procedimientoService.GetOne(model.ProcedimientoOrigenId);


                    Procedimiento pCopia = new Procedimiento();

                    proc.CopyPropertiesTo(pCopia);
                    //idexp = (long)Session["idexped"];
                    idexp = proc.ExpedienteId;
                    Expediente exp = _expedienteService.GetOne(proc.ExpedienteId);
                    //Expediente exp = _expedienteService.GetOne(idexp);

                    //FVN Inicio
                    if (pCopia.FlagCopiarProcedimiento == 0)
                    {
                        //return Json(new { valid = false, mensaje = "Debe modificar la denominacion del procediiento antes de copiar." });
                        ModelState.AddModelError("", "Antes de Copiar debe modificar la denominacion : " + pCopia.Denominacion + " en la seccion de Datos Generales");
                        return PartialView("_Error");
                    }

                    pCopia.FlagCopiarProcedimiento = 0;
                    //FVN Fin

                    //List<Procedimiento> lista = _procedimientoService.GetByExpedienteNumero(proc.ExpedienteId);
                    List<Procedimiento> lista = _procedimientoService.GetByExpedienteNumero(idexp);

                    pCopia.Numero = lista.Count() + 1;
                    pCopia.ProcedimientoId = 0;
                    pCopia.Denominacion = "Copia desde el procedimiento con codigo " + pCopia.CodigoCorto;
                    pCopia.CodigoCorto = null;
                    pCopia.Codigo = null;

                    pCopia.ExpedienteId = (long)Session["idexped"];
                    Procedimiento result = NuevoCopia(pCopia, user);
                    string cod = "";

                    model.ProcedimientoDestinoId = result.ProcedimientoId;
                    model.CodigoCopia = NewCodigo(result, user, ref cod);
                    model.CodigoCortoCopia = cod + exp.Entidad.EntidadId.ToString().PadRight(4, '0') + Guid.NewGuid().ToString().Substring(1, 4).ToUpper();

                    //pCopia.DenominacionAnterior = pCopia.Denominacion;
                }

                if (model.ProcedimientoDestinoId > 0)
                {
                    List<Procedimiento> lista = _procedimientoService.GetByExpedienteNumero(idexp);

                    model.Numero = lista.Count() + 1;

                    List<string> mensajes = _procedimientoService.CopiarDatosProcedimientoEli(model.ProcedimientoOrigenId, model.ProcedimientoDestinoId, lstCopia, model.CodigoCopia, model.CodigoCortoCopia);
                    //VERIFICAR

                    //if (mensajes.Count() > 0)
                    if (mensajes.Count() == 2)
                    {

                        _procedimientoService.Eliminar(model.ProcedimientoDestinoId);
                        if (!model.Nuevo)
                            CambiarOperacion(OperacionExpediente.Eliminacion, model.ProcedimientoDestinoId);

                        if (model.ProcedimientoDestinoId == 0)
                            ModelState.AddModelError("", "Debe de seleccionar un procedimiento de Destino.");
                        else
                            mensajes.ForEach(m => ModelState.AddModelError("", m));
                        //Response.StatusCode = (int)HttpStatusCode.BadRequest;

                        return PartialView("_Error");
                    }
                    else
                    {
                        //    Procedimiento proce = new Procedimiento();
                        //int totalRows = 1;
                        //List<Procedimiento> lista = _procedimientoService.GetByExpedienteNumero(idexp);
                        //foreach (Procedimiento l in lista)
                        //{

                        //        proce.ProcedimientoId = l.ProcedimientoId;
                        //        proce.Numero = totalRows;
                        //    _procedimientoService.guardarnumeroprocedimiento(proce);

                        //    totalRows = totalRows + 1;
                        //}
                        return Json(new { valid = true, mensaje = "Los datos se copiaron correctamente." });
                    }
                }
                else
                {
                    return Json(new { valid = true, mensaje = "Ocurrio un error al crear el procedimiento de destino en el sistema." });
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _log.Error(ex);
                return PartialView("_Error");
            }
        }



        [HttpPost]
        public ActionResult CopiarProcedimientoTablaASME(long ExpedienteId, long TablaAsmeIdSeleccionado, long TablaAsmeIdCopiar, long ProcedimientoIdDestino, long ProcedimientoIdOrigen, UsuarioInfo user)
        {
            try
            {
                if (ProcedimientoIdDestino > 0)
                {

                    TablaAsme model = _tablaAsmeService.GetOne(TablaAsmeIdSeleccionado);
                    model.Actividad = null;
                    _tablaAsmeService.Guardar(model);
                    List<string> mensajes = _procedimientoService.CopiarDatosProcedimientoTablaASME(ProcedimientoIdOrigen, ProcedimientoIdDestino, TablaAsmeIdSeleccionado, TablaAsmeIdCopiar);
                    //VERIFICAR

                    return Json(new { valid = true, mensaje = "Los datos se copiaron correctamente." });
                }
                else
                {
                    return Json(new { valid = true, mensaje = "Ocurrio un error al crear el procedimiento de destino en el sistema." });
                }

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


        public ActionResult AddEstandar(long ExpedienteId, List<long> ids, UsuarioInfo user)
        {
            try
            {
                string mensajes = "";

                int CodigoCanalOficinaEntidad = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/Areas/Simplificacion/Views/Web.config").AppSettings.Settings["AutoCompletarOficinaEntidad"].Value);

                for (int i = 0; i < ids.Count(); i++)
                {
                    long proceso = ids[i];
                    Procedimiento codigo = _procedimientoService.GetOne(proceso);
                    Procedimiento valorcodigo = _procedimientoService.GetOneCount(codigo.Codigo, ExpedienteId);
                    if (valorcodigo != null)
                    {
                        if (codigo.Codigo == valorcodigo.Codigo)
                        {
                            mensajes = "Duplicado";
                        }
                    }
                    if (mensajes != "Duplicado")
                    {
                        List<long> idunico = new List<long>();
                        idunico.Add(ids[i]);

                        _procedimientoService.CopiarProcedimiento(ExpedienteId, idunico, user.UsuarioId, user.EntidadId, CodigoCanalOficinaEntidad);
                    }



                }


                return Json(new { valid = true, mensaje = mensajes }, JsonRequestBehavior.AllowGet);
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
        public bool Editarrequerimiento(long id, long tipo, int num, UsuarioInfo user)
        {

            try
            {
                Procedimiento model = _procedimientoService.GetOne(id);
                List<Requisito> lista = _requisitoService.GetByProcedimiento(id);

                if (lista.Count == 0)
                {

                    return false;
                }
                else
                {

                    if (tipo == 1)
                    {
                        model.EditableG = num;
                    }
                    else if (tipo == 2)
                    {
                        model.EditableN = num;
                    }


                    _procedimientoService.Updateprocedimiento(model);
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
        public bool Editartitulo(long id, int tipo, UsuarioInfo user)
        {

            try
            {

                Requisito requi = _requisitoService.GetOne(id);


                if (tipo == 1)
                {
                    requi.EditableTitulo = tipo;
                }
                else if (tipo == 0)
                {
                    requi.EditableTitulo = tipo;
                    requi.Titulo = "";
                }

                _requisitoService.Save(requi);



            }
            catch (Exception ex)
            {
                ModelState.AddModelError("editarExpediente", ex.Message);
                _log.Error(ex);
            }
            return true;
        }


        public ActionResult VerObsDatosGenerales(long id, long entidadid, UsuarioInfo userData)
        {
            try
            {

                int CodigoCanalOficinaEntidad = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/Areas/Simplificacion/Views/Web.config").AppSettings.Settings["AutoCompletarOficinaEntidad"].Value);

                ViewBag.Usuario = userData;
                ViewBag.entidadver = entidadid;
                Usuario user = _usuarioService.GetOne(userData.UsuarioId);
                Procedimiento model = _procedimientoService.GetOne(id);
                List<PlazoAtencion> lstplazo = _plazoAtencionService.GetAll(id);
                model.listplazoatencion = lstplazo;
                ViewBag.PlazoAtencionEstandar = model.PlazoAtencionEstandar;
                if (model.FlagCopiarProcedimiento == 0)
                {
                    model.DenominacionAnterior = model.Denominacion;
                }

                if (model.Codigo == "S/C" && !string.IsNullOrEmpty(model.CodigoACR)) return RedirectToAction("NuevoCodigo", "Procedimiento", new { area = "Simplificacion", id = id });

                Expediente exp = _expedienteService.GetOne(model.ExpedienteId);

                /*listar expediente observacion*/
                ViewBag.Expediente = exp;

                Observacion obs = new Observacion();
                obs.ExpedienteId = exp.ExpedienteId;
                obs.ProcedimientoId = id;
                obs.Estado = "0";
                obs.CodValidacion = "5";
                obs.EntidadId = entidadid;
                obs.Pantalla = "Datos Generales";
                ViewBag.Observacion = _observacionService.GetOneVerObservacion(obs);

                /*listar expediente observacion*/

                MaestroFijo modelMaestroFijo = _maestroFijoService.GetOneByEntidad(user.EntidadId);

                if (modelMaestroFijo == null)
                    modelMaestroFijo = new MaestroFijo();

                if (modelMaestroFijo.MaestroFijoDatoAdicional == null)
                    modelMaestroFijo.MaestroFijoDatoAdicional = new List<MaestroFijoDatoAdicional>();

                if (modelMaestroFijo.MaestroFijoSede == null)
                    modelMaestroFijo.MaestroFijoSede = new List<MaestroFijoSede>();

                if (modelMaestroFijo.MaestroFijoPasoSeguir == null)
                    modelMaestroFijo.MaestroFijoPasoSeguir = new List<MaestroFijoPasoSeguir>();

                if (modelMaestroFijo.MaestroFijoNotaCiudadano == null)
                    modelMaestroFijo.MaestroFijoNotaCiudadano = new List<MaestroFijoNotaCiudadano>();


                if (string.IsNullOrEmpty(model.Anexo))
                {
                    model.Anexo = modelMaestroFijo.Anexo;
                }

                if (string.IsNullOrEmpty(model.Correo))
                {
                    model.Correo = modelMaestroFijo.Correo;
                }

                if (string.IsNullOrEmpty(model.Telefono))
                {
                    model.Telefono = modelMaestroFijo.Telefono;
                }


                ViewBag.Calificacion = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Aprobación Automática", Value = "1" , Selected = model.Calificacion == CalificacionProcedimiento.Automatica ? true:false},
                    new SelectListItem() { Text = "Evaluación Previa con Silencio Positivo", Value = "2",  Selected = model.Calificacion == CalificacionProcedimiento.SilencioPositivo ? true:false },
                    new SelectListItem() { Text = "Evaluación Previa con Silencio Negativo", Value = "3" , Selected = model.Calificacion == CalificacionProcedimiento.SilencioNegativo ? true:false},
                    new SelectListItem() { Text = "", Value = "4" , Selected = model.Calificacion == CalificacionProcedimiento.Enblanco ? true:false}
                };

                ViewBag.renovacion = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Si", Value = "1" , Selected = model.Renovacio == Renovacio.Si ? true:false},
                    new SelectListItem() { Text = "No", Value = "2",  Selected = model.Renovacio == Renovacio.No ? true:false },
                     };

                ViewBag.plazorenovacion1 = new List<SelectListItem>()
                {

                    new SelectListItem() { Text = "Seleccione", Value = "0",  Selected = model.Plazorenovacion == Plazorenovacion.seleccione ? true:false },
                    new SelectListItem() { Text = "Indeterminado", Value = "1",  Selected = model.Plazorenovacion == Plazorenovacion.Indeterminado ? true:false },
                    new SelectListItem() { Text = "1 mes", Value = "2" , Selected = model.Plazorenovacion == Plazorenovacion.mes1 ? true:false},
                    new SelectListItem() { Text = "2 mes", Value = "3",  Selected = model.Plazorenovacion == Plazorenovacion.mes2 ? true:false },
                    new SelectListItem() { Text = "3 mes", Value = "4" , Selected = model.Plazorenovacion == Plazorenovacion.mes3 ? true:false},
                    new SelectListItem() { Text = "4 mes", Value = "5",  Selected = model.Plazorenovacion == Plazorenovacion.mes4 ? true:false },
                    new SelectListItem() { Text = "5 mes", Value = "8" , Selected = model.Plazorenovacion == Plazorenovacion.mes5 ? true:false},
                    new SelectListItem() { Text = "6 mes", Value = "7",  Selected = model.Plazorenovacion == Plazorenovacion.mes6 ? true:false },
                    new SelectListItem() { Text = "7 mes", Value = "8" , Selected = model.Plazorenovacion == Plazorenovacion.mes7 ? true:false},
                    new SelectListItem() { Text = "8 mes", Value = "9",  Selected = model.Plazorenovacion == Plazorenovacion.mes8 ? true:false },
                    new SelectListItem() { Text = "9 mes", Value = "10" , Selected = model.Plazorenovacion == Plazorenovacion.mes9 ? true:false},
                    new SelectListItem() { Text = "10 mes", Value = "11",  Selected = model.Plazorenovacion == Plazorenovacion.mes10 ? true:false },
                    new SelectListItem() { Text = "11 mes", Value = "12" , Selected = model.Plazorenovacion == Plazorenovacion.mes11 ? true:false},
                    new SelectListItem() { Text = "1 año", Value = "13",  Selected = model.Plazorenovacion == Plazorenovacion.anio1 ? true:false },
                    new SelectListItem() { Text = "2 año", Value = "14" , Selected = model.Plazorenovacion == Plazorenovacion.anio2 ? true:false},
                    new SelectListItem() { Text = "3 año", Value = "15",  Selected = model.Plazorenovacion == Plazorenovacion.anio3 ? true:false },
                    new SelectListItem() { Text = "4 año", Value = "16" , Selected = model.Plazorenovacion == Plazorenovacion.anio4 ? true:false},
                    new SelectListItem() { Text = "5 año", Value = "17",  Selected = model.Plazorenovacion == Plazorenovacion.anio5 ? true:false },
                    new SelectListItem() { Text = "6 año", Value = "18" , Selected = model.Plazorenovacion == Plazorenovacion.anio6 ? true:false},
                    new SelectListItem() { Text = "7 año", Value = "19",  Selected = model.Plazorenovacion == Plazorenovacion.anio7 ? true:false },
                    new SelectListItem() { Text = "8 año", Value = "20" , Selected = model.Plazorenovacion == Plazorenovacion.anio8 ? true:false},
                    new SelectListItem() { Text = "9 año", Value = "21",  Selected = model.Plazorenovacion == Plazorenovacion.anio9 ? true:false },
                    new SelectListItem() { Text = "10 año", Value = "22" , Selected = model.Plazorenovacion == Plazorenovacion.anio10 ? true:false},
                    new SelectListItem() { Text = "20 año", Value = "23",  Selected = model.Plazorenovacion == Plazorenovacion.anio20 ? true:false },

                };


                List<MetaDato> listaTipoNota = _metaDatoService.GetByParent(15);
                listaTipoNota.Insert(0, new MetaDato() { MetaDatoId = 0, Nombre = "- SELECCIONAR -" });

                ViewBag.TipoNota = JsonConvert.SerializeObject(
                    listaTipoNota.Select(x => new
                    {
                        MetaDatoId = x.MetaDatoId,
                        Nombre = x.Nombre
                    }).ToList()
                );



                ViewBag.tipoplazo = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "- SELECCIONAR -", Value = "0"},
                    new SelectListItem() { Text = "Días Hábiles", Value = "154" , Selected = model.TipoPlazo == TipoPlazo.habiles ? true:false},
                    new SelectListItem() { Text = "Días Calendarios", Value = "155", Selected = model.TipoPlazo == TipoPlazo.calendarios ? true:false},
                    new SelectListItem() { Text = "Meses", Value = "156", Selected = model.TipoPlazo == TipoPlazo.meses ? true:false},
                };



                ViewBag.ListaSede = JsonConvert.SerializeObject(
                        model.ProcedimientoSede
                        .Select(x => new
                        {
                            SedeId = x.SedeId,
                            Nombre = x.Sede.Nombre,
                            Direccion = x.Sede.Direccion + " - " + x.Sede.Distrito.Ruta,
                            SedeUnidadOrganica = x.Sede.SedeUnidadOrganica.Where(p => p.SedeId == x.SedeId).Select(i => i.UnidadOrganica.Nombre),
                            UndOrgRecepcionDocumentos = x.UndOrgRecepcionDocumentos == null ? "" : JsonConvert.SerializeObject(x.UndOrgRecepcionDocumentos.Select(r => new { r.UnidadOrganicaId, r.SedeId, nombre = _unidadOrganicaService.GetOne(r.UnidadOrganicaId).Nombre })),
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
                        }).ToList()
                    );


                List<PasoSeguir> lstPasoSeguir = new List<PasoSeguir>();


                if (model.PasoSeguir.Count == 0 && (model.Editado == null || false))
                {
                    foreach (var obj in modelMaestroFijo.MaestroFijoPasoSeguir)
                    {
                        lstPasoSeguir.Add(new PasoSeguir()
                        {
                            PasoSeguirId = obj.PasoSeguirId,
                            Descripcion = obj.Descripcion
                        });
                    }
                }
                else
                {
                    foreach (PasoSeguir obj in model.PasoSeguir)
                    {
                        lstPasoSeguir.Add(new PasoSeguir()
                        {
                            PasoSeguirId = obj.PasoSeguirId,
                            Descripcion = obj.Descripcion
                        });
                    }
                }


                model.PasoSeguir = lstPasoSeguir;

                ViewBag.ListaPasoSeguir = JsonConvert.SerializeObject(
                        model.PasoSeguir
                        .Select(x => new
                        {
                            PasoSeguirId = x.PasoSeguirId,
                            Descripcion = x.Descripcion
                        }).ToList()
                    );

                // Datos Adicionales
                List<Dato> datos = _datoService.GetByTipo(TipoDato.TipoDatoAdicionalProc);
                List<Dato> datosAdicionales = new List<Dato>();
                List<Dato> canales = datos.Where(x => x.Entero01 == 1).ToList();

                canales.FirstOrDefault(x => x.DatoId == CodigoCanalOficinaEntidad).OficinasAutocompletar = 1;


                datosAdicionales.AddRange(canales);
                List<Dato> dataFormaPago = datos.Where(x => x.Entero01 == 2).ToList();
                foreach (Dato dfp in dataFormaPago)
                {
                    List<Dato> formasPago = datos.Where(x => x.Entero01 == dfp.MetaDatoId).ToList();
                    formasPago.ForEach(x => x.Padre = dfp);
                    datosAdicionales.AddRange(formasPago);
                }

                List<ProcedimientoDatoAdicional> lst = new List<ProcedimientoDatoAdicional>();


                foreach (Dato data in datosAdicionales)
                {
                    if ((model.Editado ?? false) == true)
                    {
                        ProcedimientoDatoAdicional da = model.ProcedimientoDatoAdicional.SingleOrDefault(x => x.MetaDatoId == data.MetaDatoId);
                        if (da != null)
                        {
                            da = new ProcedimientoDatoAdicional()
                            {
                                ProcedimientoId = model.ProcedimientoId,
                                MetaDatoId = data.MetaDatoId,
                                Comentario = da.Comentario,
                                Checked = da.Checked,
                                MetaDato = data,
                                Tipo = data.Padre == null ? 1 : 2
                            };
                            lst.Add(da);
                        }
                    }
                    else
                    {
                        if (modelMaestroFijo.MaestroFijoDatoAdicional != null)
                        {

                            MaestroFijoDatoAdicional obj = modelMaestroFijo.MaestroFijoDatoAdicional.SingleOrDefault(x => x.MetaDatoId == data.MetaDatoId);

                            if (obj != null)
                            {
                                ProcedimientoDatoAdicional objx = new ProcedimientoDatoAdicional()
                                {
                                    ProcedimientoId = model.ProcedimientoId,
                                    MetaDatoId = data.MetaDatoId,
                                    Comentario = obj.Comentario,
                                    Checked = true,
                                    MetaDato = data,
                                    Tipo = data.Padre == null ? 1 : 2
                                };
                                lst.Add(objx);
                            }
                            else
                            {

                                ProcedimientoDatoAdicional da = new ProcedimientoDatoAdicional();
                                da = new ProcedimientoDatoAdicional()
                                {
                                    ProcedimientoId = model.ProcedimientoId,
                                    MetaDatoId = data.MetaDatoId,
                                    Comentario = "",
                                    Checked = false,
                                    MetaDato = data,
                                    Tipo = data.Padre == null ? 1 : 2
                                };
                                lst.Add(da);
                            }
                        }
                    }



                }


                model.ProcedimientoDatoAdicional = lst;

                model.ProcedimientoDatoAdicional = model.ProcedimientoDatoAdicional
                                                    .OrderBy(x => x.Tipo)
                                                    .ThenBy(x => x.MetaDatoId)
                                                    .ToList();


                UIT objuit = _UITService.LsitaGetOne();

                ViewBag.ListaModalidad = JsonConvert.SerializeObject(
                        model.TablaAsme
                        .Select(x => new
                        {
                            TablaAsmeId = x.TablaAsmeId,
                            Codigo = x.Codigo,
                            Descripcion = x.Descripcion,
                            EsGratuito = x.EsGratuito,
                            Prestaciones = x.Prestaciones,
                            CostoUnitario = x.CostoUnitario,
                            Calificacion = x.Calificacion,

                            Personal = x.Personal,
                            MaterialFungible = x.MaterialFungible,
                            ServicioIdentificable = x.ServicioIdentificable,
                            MaterialNoFungible = x.MaterialNoFungible,
                            ServicioTerceros = x.ServicioTerceros,
                            Depreciacion = x.Depreciacion,
                            Fijos = x.Fijos,
                            DerechoTramitacion = x.DerechoTramitacion,


                            UIT = objuit.UITID,
                            TablaAsmeReproduccion = x.TablaAsmeReproduccion.Select(f => new
                            {
                                ReproduccionId = f.ReproduccionId,
                                Descripcion = f.Descripcion,
                                Costo = f.Costo,
                                TablaAsmeId = x.TablaAsmeId,
                                Sustento = f.Sustento
                            })
                        }).OrderBy(x => x.TablaAsmeId).ToList()
                    );


                if (model.Observacion != null)
                {
                    ViewBag.ListaObservacion = JsonConvert.SerializeObject(
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
                            }).Where(x => x.NombreCampo.Contains("NombredelaModalidad")).OrderBy(x => x.ObservacionId).ToList()
                        );
                }
                List<Requisito> listrequi = new List<Requisito>();

                foreach (Requisito reqllenar in model.Requisito.Where(z => z.TipoRequisito == TipoRequisito.General && z.Eliminado != 3).OrderBy(x => x.RecNum).ToList())
                {
                    if (reqllenar.TipoRequisito == TipoRequisito.General)
                    {
                        listrequi.Add(reqllenar);
                    }


                }


                foreach (Requisito reqllenar in model.Requisito.Where(z => z.TipoRequisito == TipoRequisito.Especifico && z.Eliminado != 3).OrderBy(x => x.RecNum).ToList())
                {

                    if (reqllenar.TipoRequisito == TipoRequisito.Especifico)
                    {
                        listrequi.Add(reqllenar);
                    }

                }

                ViewBag.ListaRequisito = JsonConvert.SerializeObject(
                        listrequi
                        .Select(x => new
                        {
                            RequisitoId = x.RequisitoId,
                            Nombre = x.Nombre,
                            Descripcion = x.Descripcion,
                            BaseLegalId = x.BaseLegalId,
                            SustentoTecnico = x.SustentoTecnico,
                            CadenaTramite = x.CadenaTramite,
                            BaseLegalTexto = x.BaseLegalTexto,
                            TipoRequisito = x.TipoRequisito,
                            UserCreacion = x.UserCreacion,
                            RecNum = x.RecNum,
                            Activar = x.Activar,
                            Titulo = x.Titulo,
                            EditableTitulo = x.EditableTitulo,
                            RequisitoFormulario = x.RequisitoFormulario.Select(f => new
                            {
                                RequisitoId = f.RequisitoId,
                                FormularioId = f.FormularioId,
                                Nombre = f.Nombre,
                                Url = f.Url,
                                ArchivoAdjuntoId = f.ArchivoAdjuntoId
                            })
                        }).ToList()
                    );

                if (model.Observacion != null)
                {
                    ViewBag.ListaObservacionRequisitos = JsonConvert.SerializeObject(
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
                       }).Where(x => x.NombreCampo.Contains("NombredelosRequisitos")).OrderBy(x => x.ObservacionId).ToList()
                   );

                    ViewBag.ListaObservacionBaseLegal = JsonConvert.SerializeObject(
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
                        }).Where(x => x.NombreCampo.Contains("NombredelosBaseLegal")).OrderBy(x => x.ObservacionId).ToList()
                    );
                }
                List<BaseLegalNorma> listaBL = new List<BaseLegalNorma>();
                long idBL = model.BaseLegalId != null ? model.BaseLegalId.Value : 0;
                if (idBL > 0) listaBL = _baseLegalService.GetDetails(idBL);

                ViewBag.ListaBaseLegal = JsonConvert.SerializeObject(
                    listaBL.Select(x => new
                    {
                        BaseLegalId = x.BaseLegalId,
                        BaseLegalNormaId = x.BaseLegalNormaId,
                        TipoBaseLegal = x.TipoBaseLegal,
                        NormaId = x.Norma != null ? x.NormaId.Value : 0,
                        Descripcion = x.Norma != null ? x.Norma.Descripcion : x.Descripcion,
                        TipoNormaId = x.Norma != null ? x.Norma.TipoNorma.MetaDatoId : x.TipoNorma.MetaDatoId,
                        NormaTipo = x.Norma != null ? x.Norma.TipoNorma.Nombre : x.TipoNorma.Nombre,
                        Numero = x.Norma != null ? x.Norma.Numero : x.Numero,
                        FechaPublicacion = x.Norma != null ? (x.Norma.FechaPublicacion == null ? "" : x.Norma.FechaPublicacion.Value.ToString("dd/MM/yyyy")) : (x.FechaPublicacion == null ? "" : x.FechaPublicacion.Value.ToString("dd/MM/yyyy")),
                        Sector = x.Norma != null ? x.Norma.Sector : x.Sector,
                        Articulo = x.Articulo,
                        Url = x.Url,
                        ArchivoAdjuntoId = x.TipoBaseLegal != TipoBaseLegal.RutaInternet ? (x.Norma != null ? 0 : x.ArchivoAdjuntoId) : 0,
                        EstadoACR = x.EstadoACR,
                        DescripcionACR = x.DescripcionACR,
                    }).ToList()
                );

                List<NotaCiudadano> ListaNota = model.NotaCiudadano.ToList();
                List<NotaCiudadano> ListaNC = new List<NotaCiudadano>();

                if (ListaNota.Count == 0 && (model.Editado == null || false))
                {
                    MaestroFijo obj = _maestroFijoService.GetOneByEntidad(user.EntidadId);


                    if (obj == null)
                    {
                        ListaNC = new List<NotaCiudadano>();
                    }
                    else
                    {
                        if (obj.MaestroFijoNotaCiudadano != null)
                            foreach (MaestroFijoNotaCiudadano x in obj.MaestroFijoNotaCiudadano)
                            {
                                ListaNC.Add(new NotaCiudadano
                                {
                                    NotaCiudadanoId = x.NotaCiudadanoId,
                                    TipoNotaId = x.MetaDatoTipoNotaId,
                                    Nota = x.Comentario
                                });
                            }
                    }
                }
                else
                {
                    ListaNC = model.NotaCiudadano;
                }

                ViewBag.ListaNota = JsonConvert.SerializeObject(
                      ListaNC.Select(x => new
                      {
                          x.NotaCiudadanoId,
                          x.TipoNotaId,
                          x.Nota
                      })
                   );


                ViewBag.EsCargaInicial = model.Expediente.TipoExpediente == TipoExpediente.CargaInicial;


                if (user.Rol != Rol.Administrador)
                {
                    ViewBag.EsTupaEstandar = user.Entidad.TipoTupa == TipoTupa.Estandar;
                }
                else
                {
                    ViewBag.EsTupaEstandar = userData.TipoTupa == 2;
                }


                ViewBag.User = userData;

                string viewName = string.Empty;
                viewName = "VerObsDatosGenerales";

                return View(viewName, model);
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ModelState.AddModelError("VerObsDatosGenerales", ex.Message);
                _log.Error(ex);
                return View("_Error");
            }
        }

        public ActionResult DatosGenerales(long id, UsuarioInfo userData)
        {
            try
            {

                /*Inicio Cargar roles - Acceso*/
                List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(userData.Rol), 22);
                userData.rolmenu = listarolmenu.ToList();
                /*Fin Cargar roles  - Acceso*/




                int CodigoCanalOficinaEntidad = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/Areas/Simplificacion/Views/Web.config").AppSettings.Settings["AutoCompletarOficinaEntidad"].Value);

                ViewBag.Usuario = userData;

                Usuario user = _usuarioService.GetOne(userData.UsuarioId);
                Procedimiento model = _procedimientoService.GetOne(id);

                List<PlazoAtencion> lstplazo = _plazoAtencionService.GetAll(id);

                //   ViewBag.ListaPlazoAtencion = JsonConvert.SerializeObject(
                //    lstplazo.Select(x => new
                //    {
                //        PlazoAtencionId = x.PlazoAtencionId,
                //        Plazo = x.Plazo,
                //        Descripcion = x.Descripcion,
                //        TipoPlazo = x.TipoPlazo
                //    }).ToList()
                //);

                //model.Posicion = 1;

                //_AuditoriaService.AlimpiarPosicion(model.ExpedienteId,model.ProcedimientoId);
                //_procedimientoService.GuardarPosicion(model);


                model.listplazoatencion = lstplazo;

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
                model.CargoPordependencia = nombrecargo;



                if (model.PlazoAtencionEstandar == null)
                {
                    if (model.PlazoAtencion == null)
                    {
                        model.PlazoAtencion = 0;
                    }
                    ViewBag.PlazoAtencionEstandar = model.PlazoAtencion;
                    ViewBag.PlazoAtenciongrabado = model.PlazoAtencion;
                }
                else
                {
                    ViewBag.PlazoAtencionEstandar = model.PlazoAtencionEstandar;
                    if (model.PlazoAtencion == null)
                    {
                        model.PlazoAtencion = 0;
                    }
                    ViewBag.PlazoAtenciongrabado = model.PlazoAtencion;
                }



                if (model.FlagCopiarProcedimiento == 0)
                {

                    model.DenominacionAnterior = model.Denominacion;
                }

                if (model.Codigo == "S/C" && !string.IsNullOrEmpty(model.CodigoACR)) return RedirectToAction("NuevoCodigo", "Procedimiento", new { area = "Simplificacion", id = id });

                Expediente exp = _expedienteService.GetOne(model.ExpedienteId);

                /*listar expediente observacion*/
                ViewBag.Expediente = exp;

                Observacion obs = new Observacion();
                obs.ExpedienteId = exp.ExpedienteId;
                obs.ProcedimientoId = id;
                obs.Estado = "1";
                obs.EntidadId = userData.EntidadId;
                obs.Pantalla = "Datos Generales";
                ViewBag.Observacion = _observacionService.GetOneGlobalObservacion(obs);

                /*listar expediente observacion*/

                MaestroFijo modelMaestroFijo = _maestroFijoService.GetOneByEntidad(user.EntidadId);

                if (modelMaestroFijo == null)
                    modelMaestroFijo = new MaestroFijo();

                if (modelMaestroFijo.MaestroFijoDatoAdicional == null)
                    modelMaestroFijo.MaestroFijoDatoAdicional = new List<MaestroFijoDatoAdicional>();

                if (modelMaestroFijo.MaestroFijoSede == null)
                    modelMaestroFijo.MaestroFijoSede = new List<MaestroFijoSede>();

                if (modelMaestroFijo.MaestroFijoPasoSeguir == null)
                    modelMaestroFijo.MaestroFijoPasoSeguir = new List<MaestroFijoPasoSeguir>();

                if (modelMaestroFijo.MaestroFijoNotaCiudadano == null)
                    modelMaestroFijo.MaestroFijoNotaCiudadano = new List<MaestroFijoNotaCiudadano>();


                if (string.IsNullOrEmpty(model.Anexo))
                {
                    model.Anexo = modelMaestroFijo.Anexo;
                }

                if (string.IsNullOrEmpty(model.Correo))
                {
                    model.Correo = modelMaestroFijo.Correo;
                }

                if (string.IsNullOrEmpty(model.Telefono))
                {
                    model.Telefono = modelMaestroFijo.Telefono;
                }


                ViewBag.Calificacion = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Aprobación Automática", Value = "1" , Selected = model.Calificacion == CalificacionProcedimiento.Automatica ? true:false},
                    new SelectListItem() { Text = "Evaluación Previa con Silencio Positivo", Value = "2",  Selected = model.Calificacion == CalificacionProcedimiento.SilencioPositivo ? true:false },
                    new SelectListItem() { Text = "Evaluación Previa con Silencio Negativo", Value = "3" , Selected = model.Calificacion == CalificacionProcedimiento.SilencioNegativo ? true:false},
                    new SelectListItem() { Text = "", Value = "4" , Selected = model.Calificacion == CalificacionProcedimiento.Enblanco ? true:false}
                };

                ViewBag.renovacion = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Si", Value = "1" , Selected = model.Renovacio == Renovacio.Si ? true:false},
                    new SelectListItem() { Text = "No", Value = "2",  Selected = model.Renovacio == Renovacio.No ? true:false },
                     };

                ViewBag.plazorenovacion1 = new List<SelectListItem>()
                {

                    new SelectListItem() { Text = "Seleccione", Value = "0",  Selected = model.Plazorenovacion == Plazorenovacion.seleccione ? true:false },
                    new SelectListItem() { Text = "Indeterminado", Value = "1",  Selected = model.Plazorenovacion == Plazorenovacion.Indeterminado ? true:false },
                    new SelectListItem() { Text = "1 mes", Value = "2" , Selected = model.Plazorenovacion == Plazorenovacion.mes1 ? true:false},
                    new SelectListItem() { Text = "2 mes", Value = "3",  Selected = model.Plazorenovacion == Plazorenovacion.mes2 ? true:false },
                    new SelectListItem() { Text = "3 mes", Value = "4" , Selected = model.Plazorenovacion == Plazorenovacion.mes3 ? true:false},
                    new SelectListItem() { Text = "4 mes", Value = "5",  Selected = model.Plazorenovacion == Plazorenovacion.mes4 ? true:false },
                    new SelectListItem() { Text = "5 mes", Value = "8" , Selected = model.Plazorenovacion == Plazorenovacion.mes5 ? true:false},
                    new SelectListItem() { Text = "6 mes", Value = "7",  Selected = model.Plazorenovacion == Plazorenovacion.mes6 ? true:false },
                    new SelectListItem() { Text = "7 mes", Value = "8" , Selected = model.Plazorenovacion == Plazorenovacion.mes7 ? true:false},
                    new SelectListItem() { Text = "8 mes", Value = "9",  Selected = model.Plazorenovacion == Plazorenovacion.mes8 ? true:false },
                    new SelectListItem() { Text = "9 mes", Value = "10" , Selected = model.Plazorenovacion == Plazorenovacion.mes9 ? true:false},
                    new SelectListItem() { Text = "10 mes", Value = "11",  Selected = model.Plazorenovacion == Plazorenovacion.mes10 ? true:false },
                    new SelectListItem() { Text = "11 mes", Value = "12" , Selected = model.Plazorenovacion == Plazorenovacion.mes11 ? true:false},
                    new SelectListItem() { Text = "1 año", Value = "13",  Selected = model.Plazorenovacion == Plazorenovacion.anio1 ? true:false },
                    new SelectListItem() { Text = "2 año", Value = "14" , Selected = model.Plazorenovacion == Plazorenovacion.anio2 ? true:false},
                    new SelectListItem() { Text = "3 año", Value = "15",  Selected = model.Plazorenovacion == Plazorenovacion.anio3 ? true:false },
                    new SelectListItem() { Text = "4 año", Value = "16" , Selected = model.Plazorenovacion == Plazorenovacion.anio4 ? true:false},
                    new SelectListItem() { Text = "5 año", Value = "17",  Selected = model.Plazorenovacion == Plazorenovacion.anio5 ? true:false },
                    new SelectListItem() { Text = "6 año", Value = "18" , Selected = model.Plazorenovacion == Plazorenovacion.anio6 ? true:false},
                    new SelectListItem() { Text = "7 año", Value = "19",  Selected = model.Plazorenovacion == Plazorenovacion.anio7 ? true:false },
                    new SelectListItem() { Text = "8 año", Value = "20" , Selected = model.Plazorenovacion == Plazorenovacion.anio8 ? true:false},
                    new SelectListItem() { Text = "9 año", Value = "21",  Selected = model.Plazorenovacion == Plazorenovacion.anio9 ? true:false },
                    new SelectListItem() { Text = "10 año", Value = "22" , Selected = model.Plazorenovacion == Plazorenovacion.anio10 ? true:false},
                    new SelectListItem() { Text = "20 año", Value = "23",  Selected = model.Plazorenovacion == Plazorenovacion.anio20 ? true:false },

                };


                List<MetaDato> listaTipoNota = _metaDatoService.GetByParent(15);
                listaTipoNota.Insert(0, new MetaDato() { MetaDatoId = 0, Nombre = "- SELECCIONAR -" });

                ViewBag.TipoNota = JsonConvert.SerializeObject(
                    listaTipoNota.Select(x => new
                    {
                        MetaDatoId = x.MetaDatoId,
                        Nombre = x.Nombre
                    }).ToList()
                );



                var listatipo = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "- SELECCIONAR -", Value = "0"},
                    new SelectListItem() { Text = "Días Hábiles", Value = "154" , Selected = model.TipoPlazo == TipoPlazo.habiles ? true:false},
                    new SelectListItem() { Text = "Días Calendarios", Value = "155", Selected = model.TipoPlazo == TipoPlazo.calendarios ? true:false},
                    new SelectListItem() { Text = "Meses", Value = "156", Selected = model.TipoPlazo == TipoPlazo.meses ? true:false},
                };

                ViewBag.tipoplazo = listatipo;
                ViewBag.listatipoplazo = JsonConvert.SerializeObject(
                      listatipo.Select(x => new
                      {
                          Text = x.Text,
                          Value = x.Value,
                      })
                      .OrderBy(x => x.Value)
                      .ToList()
                  );



                var calificaciones = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "- SELECCIONAR -", Value = "0"},
                    new SelectListItem() { Text = "Aprobación Automática", Value = "1" },
                    new SelectListItem() { Text = "Evaluación Previa con Silencio Positivo", Value = "2" },
                    new SelectListItem() { Text = "Evaluación Previa con Silencio Negativo", Value = "3" },
                    new SelectListItem() { Text = "", Value = "4" }
                };



                ViewBag.calificaciones = calificaciones;
                ViewBag.calificaciones = JsonConvert.SerializeObject(
                      calificaciones.Select(x => new
                      {
                          Text = x.Text,
                          Value = x.Value,
                      })
                      .OrderBy(x => x.Value)
                      .ToList()
                  );




                ViewBag.ListaSede = JsonConvert.SerializeObject(
                        model.ProcedimientoSede
                        .Select(x => new
                        {
                            SedeId = x.SedeId,
                            Nombre = x.Sede.Nombre,
                            Direccion = x.Sede.Direccion + " - " + x.Sede.Distrito.Ruta,
                            SedeUnidadOrganica = x.Sede.SedeUnidadOrganica.Where(p => p.SedeId == x.SedeId).Select(i => i.UnidadOrganica.Nombre),
                            UndOrgRecepcionDocumentos = x.UndOrgRecepcionDocumentos == null ? "" : JsonConvert.SerializeObject(x.UndOrgRecepcionDocumentos.Select(r => new { r.UnidadOrganicaId, r.SedeId, nombre = _unidadOrganicaService.GetOne(r.UnidadOrganicaId).Nombre })),
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
                        }).ToList()
                    );


                List<PasoSeguir> lstPasoSeguir = new List<PasoSeguir>();


                if (model.PasoSeguir.Count == 0 && (model.Editado == null || false))
                {
                    foreach (var obj in modelMaestroFijo.MaestroFijoPasoSeguir)
                    {
                        lstPasoSeguir.Add(new PasoSeguir()
                        {
                            PasoSeguirId = obj.PasoSeguirId,
                            Descripcion = obj.Descripcion
                        });
                    }
                }
                else
                {
                    foreach (PasoSeguir obj in model.PasoSeguir)
                    //foreach (var temp in modelMaestroFijo.MaestroFijoPasoSeguir)
                    //{
                    {
                        //if (temp.PasoSeguirId == obj.PasoSeguirId)
                        //{
                        lstPasoSeguir.Add(new PasoSeguir()
                        {
                            PasoSeguirId = obj.PasoSeguirId,
                            Descripcion = obj.Descripcion
                        });
                        //}
                    }
                    //}
                }


                model.PasoSeguir = lstPasoSeguir;

                ViewBag.ListaPasoSeguir = JsonConvert.SerializeObject(
                        model.PasoSeguir
                        .Select(x => new
                        {
                            PasoSeguirId = x.PasoSeguirId,
                            Descripcion = x.Descripcion
                        }).ToList()
                    );

                // Datos Adicionales
                List<Dato> datos = _datoService.GetByTipo(TipoDato.TipoDatoAdicionalProc);
                List<Dato> datosAdicionales = new List<Dato>();
                List<Dato> canales = datos.Where(x => x.Entero01 == 1).ToList();

                canales.FirstOrDefault(x => x.DatoId == CodigoCanalOficinaEntidad).OficinasAutocompletar = 1;


                datosAdicionales.AddRange(canales);
                List<Dato> dataFormaPago = datos.Where(x => x.Entero01 == 2).ToList();
                foreach (Dato dfp in dataFormaPago)
                {
                    List<Dato> formasPago = datos.Where(x => x.Entero01 == dfp.MetaDatoId).ToList();
                    formasPago.ForEach(x => x.Padre = dfp);
                    datosAdicionales.AddRange(formasPago);
                }

                List<ProcedimientoDatoAdicional> lst = new List<ProcedimientoDatoAdicional>();


                foreach (Dato data in datosAdicionales)
                {
                    if ((model.Editado ?? false) == true)
                    {
                        ProcedimientoDatoAdicional da = model.ProcedimientoDatoAdicional.SingleOrDefault(x => x.MetaDatoId == data.MetaDatoId);
                        if (da != null)
                        {
                            da = new ProcedimientoDatoAdicional()
                            {
                                ProcedimientoId = model.ProcedimientoId,
                                MetaDatoId = data.MetaDatoId,
                                Comentario = da.Comentario,
                                Checked = da.Checked,
                                MetaDato = data,
                                Tipo = data.Padre == null ? 1 : 2
                            };
                            lst.Add(da);
                        }
                    }
                    else
                    {
                        if (modelMaestroFijo.MaestroFijoDatoAdicional != null)
                        {

                            MaestroFijoDatoAdicional obj = modelMaestroFijo.MaestroFijoDatoAdicional.SingleOrDefault(x => x.MetaDatoId == data.MetaDatoId);

                            if (obj != null)
                            {
                                ProcedimientoDatoAdicional objx = new ProcedimientoDatoAdicional()
                                {
                                    ProcedimientoId = model.ProcedimientoId,
                                    MetaDatoId = data.MetaDatoId,
                                    Comentario = obj.Comentario,
                                    Checked = true,
                                    MetaDato = data,
                                    Tipo = data.Padre == null ? 1 : 2
                                };
                                lst.Add(objx);
                            }
                            else
                            {

                                ProcedimientoDatoAdicional da = new ProcedimientoDatoAdicional();
                                //da.MetaDato = data;
                                //da.Checked = true;
                                //da.Tipo = data.Padre == null ? 1 : 2;
                                da = new ProcedimientoDatoAdicional()
                                {
                                    ProcedimientoId = model.ProcedimientoId,
                                    MetaDatoId = data.MetaDatoId,
                                    Comentario = "",
                                    Checked = false,
                                    MetaDato = data,
                                    Tipo = data.Padre == null ? 1 : 2
                                };
                                lst.Add(da);
                            }
                        }
                    }



                }


                model.ProcedimientoDatoAdicional = lst;

                model.ProcedimientoDatoAdicional = model.ProcedimientoDatoAdicional
                                                    .OrderBy(x => x.Tipo)
                                                    .ThenBy(x => x.MetaDatoId)
                                                    .ToList();



                //List<MetaDato> datosAdicionales = new List<MetaDato>();
                //List<MetaDato> canales = _metaDatoService.GetByParent(23);
                //datosAdicionales.AddRange(canales);
                //List<MetaDato> dataFormaPago = _metaDatoService.GetByParent(27);
                //foreach (MetaDato dfp in dataFormaPago)
                //{
                //    List<MetaDato> formasPago = _metaDatoService.GetByParent(dfp.MetaDatoId);
                //    formasPago.ForEach(x => x.Padre = dfp);
                //    datosAdicionales.AddRange(formasPago);
                //}

                //foreach (MetaDato data in datosAdicionales)
                //{
                //    ProcedimientoDatoAdicional da = model.ProcedimientoDatoAdicional.SingleOrDefault(x => x.MetaDatoId == data.MetaDatoId);
                //    if(da == null)
                //    {
                //        da = new ProcedimientoDatoAdicional()
                //        {
                //            ProcedimientoId = model.ProcedimientoId,
                //            MetaDatoId = data.MetaDatoId,
                //            Comentario = "",
                //            Checked = false,
                //            MetaDato = data,
                //            Tipo = data.Padre == null ? 1 : 2
                //        };
                //        model.ProcedimientoDatoAdicional.Add(da);
                //    }
                //    else
                //    {
                //        da.MetaDato = data;
                //        da.Checked = true;
                //        da.Tipo = data.Padre == null ? 1 : 2;
                //    }
                //}

                //model.ProcedimientoDatoAdicional = model.ProcedimientoDatoAdicional
                //                                    .OrderBy(x => x.Tipo)
                //                                    .ThenBy(x => x.MetaDatoId)
                //                                    .ToList();

                //////////////////////


                UIT objuit = _UITService.LsitaGetOne();

                ViewBag.ListaModalidad = JsonConvert.SerializeObject(
                        model.TablaAsme
                        .Select(x => new
                        {
                            TablaAsmeId = x.TablaAsmeId,
                            Codigo = x.Codigo,
                            Descripcion = x.Descripcion,
                            EsGratuito = x.EsGratuito,
                            Prestaciones = x.Prestaciones,
                            CostoUnitario = x.CostoUnitario,
                            Calificacion = x.Calificacion,

                            Personal = x.Personal,
                            MaterialFungible = x.MaterialFungible,
                            ServicioIdentificable = x.ServicioIdentificable,
                            MaterialNoFungible = x.MaterialNoFungible,
                            ServicioTerceros = x.ServicioTerceros,
                            Depreciacion = x.Depreciacion,
                            Fijos = x.Fijos,
                            DerechoTramitacion = x.DerechoTramitacion,
                            UIT = objuit.UITID,

                            TablaAsmeReproduccion = x.TablaAsmeReproduccion.Select(f => new
                            {
                                ReproduccionId = f.ReproduccionId,
                                Descripcion = f.Descripcion,
                                Costo = f.Costo,
                                TablaAsmeId = x.TablaAsmeId,
                                Sustento = f.Sustento
                            })
                        }).OrderBy(x => x.TablaAsmeId).ToList()
                    );


                if (model.Observacion != null)
                {
                    ViewBag.ListaObservacion = JsonConvert.SerializeObject(
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
                            }).Where(x => x.NombreCampo.Contains("NombredelaModalidad")).OrderBy(x => x.ObservacionId).ToList()
                        );
                }




                ViewBag.ListaProcedimientoCargos = JsonConvert.SerializeObject(
                   model.ProcedimientoCargos
                   .Select(x => new
                   {
                       ProcedimientoCargosID = x.ProcedimientoCargosID,
                       ProcedimientoId = x.ProcedimientoId,
                       Cargo = x.Cargo,
                       UndOrgId = x.UndOrgId,
                       PzoPresent = x.PzoPresent,
                       PzoResol = x.PzoResol,
                       NombreUndOrg = _unidadOrganicaService.GetOne(x.UndOrgId).Nombre,
                       orden = x.orden,
                       TipoReconPresent = x.TipoReconPresent,
                       TipoReconResol = x.TipoReconResol,
                   }).ToList()
               );

                if (model.Observacion != null)
                {
                    ViewBag.ListaObservacionCargos = JsonConvert.SerializeObject(
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
                            }).Where(x => x.NombreCampo.Contains("NombreProcedimientoCargos")).OrderBy(x => x.ObservacionId).ToList()
                        );
                }



                ViewBag.ListaProcedimientoCargosApe = JsonConvert.SerializeObject(
                model.ProcedimientoCargosApe
                .Select(x => new
                {
                    ProcedimientoCargosApeID = x.ProcedimientoCargosApeID,
                    ProcedimientoId = x.ProcedimientoId,
                    CargoApe = x.CargoApe,
                    UndOrgIdApe = x.UndOrgIdApe,
                    PzoPresentApe = x.PzoPresentApe,
                    PzoResolApe = x.PzoResolApe,
                    NombreUndOrgApe = _unidadOrganicaService.GetOne(x.UndOrgIdApe).Nombre,
                    ordenape = x.ordenape,
                    TipoApelPresent = x.TipoApelPresent,
                    TipoApelResol = x.TipoApelResol,
                }).ToList()
                );

                if (model.Observacion != null)
                {
                    ViewBag.ListaObservacionCargosApe = JsonConvert.SerializeObject(
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
                            }).Where(x => x.NombreCampo.Contains("NombreProcedimientoCargosApe")).OrderBy(x => x.ObservacionId).ToList()
                        );
                }


                ViewBag.ListaProcedimientoCargosOtros = JsonConvert.SerializeObject(
               model.ProcedimientoCargosOtros
               .Select(x => new
               {
                   ProcedimientoCargosOtrosID = x.ProcedimientoCargosOtrosID,
                   ProcedimientoId = x.ProcedimientoId,
                   CargoOtros = x.CargoOtros,
                   UndOrgIdOtros = x.UndOrgIdOtros,
                   PzoPresentOtros = x.PzoPresentOtros,
                   PzoResolOtros = x.PzoResolOtros,
                   NombreUndOrgOtros = _unidadOrganicaService.GetOne(x.UndOrgIdOtros).Nombre,
                   ordenotros = x.ordenotros,
                   TipoOtrosPresent = x.TipoOtrosPresent,
                   TipoOtrosResol = x.TipoOtrosResol,
               }).ToList()
               );

                //ViewBag.ListaRequisito = JsonConvert.SerializeObject(
                //        model.Requisito
                //        .Select(x => new
                //        {
                //            RequisitoId = x.RequisitoId,
                //            Nombre = x.Nombre,
                //            Descripcion = x.Descripcion,
                //            BaseLegalId = x.BaseLegalId,
                //            SustentoTecnico = x.SustentoTecnico,
                //            CadenaTramite = x.CadenaTramite,
                //            BaseLegalTexto = x.BaseLegalTexto,
                //            TipoRequisito = x.TipoRequisito,
                //            UserCreacion = x.UserCreacion,
                //            RecNum = x.RecNum,
                //            Activar = x.Activar,
                //            RequisitoFormulario = x.RequisitoFormulario.Select(f => new
                //            {
                //                RequisitoId = f.RequisitoId,
                //                FormularioId = f.FormularioId,
                //                Nombre = f.Nombre,
                //                Url = f.Url,
                //                ArchivoAdjuntoId = f.ArchivoAdjuntoId
                //            })
                //        }).OrderBy(x => x.RecNum).ToList()
                //    //}).ToList()
                //    );



                List<Requisito> listrequi = new List<Requisito>();

                foreach (Requisito reqllenar in model.Requisito.Where(z => z.TipoRequisito == TipoRequisito.General && z.Eliminado != 3).OrderBy(x => x.RecNum).ToList())
                {
                    if (reqllenar.TipoRequisito == TipoRequisito.General)
                    {
                        listrequi.Add(reqllenar);
                    }


                }


                foreach (Requisito reqllenar in model.Requisito.Where(z => z.TipoRequisito == TipoRequisito.Especifico && z.Eliminado != 3).OrderBy(x => x.RecNum).ToList())
                {

                    if (reqllenar.TipoRequisito == TipoRequisito.Especifico)
                    {
                        listrequi.Add(reqllenar);
                    }

                }


                ViewBag.ListaRequisito = JsonConvert.SerializeObject(
                        listrequi
                        .Select(x => new
                        {
                            RequisitoId = x.RequisitoId,
                            Nombre = x.Nombre,
                            Descripcion = x.Descripcion,
                            BaseLegalId = x.BaseLegalId,
                            SustentoTecnico = x.SustentoTecnico,
                            CadenaTramite = x.CadenaTramite,
                            BaseLegalTexto = x.BaseLegalTexto,
                            TipoRequisito = x.TipoRequisito,
                            UserCreacion = x.UserCreacion,
                            RecNum = x.RecNum,
                            Activar = x.Activar,
                            Titulo = x.Titulo,
                            EditableTitulo = x.EditableTitulo,
                            RequisitoFormulario = x.RequisitoFormulario.Select(f => new
                            {
                                RequisitoId = f.RequisitoId,
                                FormularioId = f.FormularioId,
                                Nombre = f.Nombre,
                                Url = f.Url,
                                ArchivoAdjuntoId = f.ArchivoAdjuntoId
                            })
                            //}).OrderBy(x => x.RecNum).ToList()
                        }).ToList()
                    );


                //ViewBag.ListaRequisito = listrequi;

                if (model.Observacion != null)
                {
                    ViewBag.ListaObservacionRequisitos = JsonConvert.SerializeObject(
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
                       }).Where(x => x.NombreCampo.Contains("NombredelosRequisitos")).OrderBy(x => x.ObservacionId).ToList()
                   );

                    ViewBag.ListaObservacionBaseLegal = JsonConvert.SerializeObject(
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
                        }).Where(x => x.NombreCampo.Contains("NombredelosBaseLegal")).OrderBy(x => x.ObservacionId).ToList()
                    );
                }
                List<BaseLegalNorma> listaBL = new List<BaseLegalNorma>();
                long idBL = model.BaseLegalId != null ? model.BaseLegalId.Value : 0;
                if (idBL > 0) listaBL = _baseLegalService.GetDetails(idBL);

                ViewBag.ListaBaseLegal = JsonConvert.SerializeObject(
                    listaBL.Select(x => new
                    {
                        BaseLegalId = x.BaseLegalId,
                        BaseLegalNormaId = x.BaseLegalNormaId,
                        TipoBaseLegal = x.TipoBaseLegal,
                        NormaId = x.Norma != null ? x.NormaId.Value : 0,
                        Descripcion = x.Norma != null ? x.Norma.Descripcion : x.Descripcion,
                        TipoNormaId = x.Norma != null ? x.Norma.TipoNorma.MetaDatoId : x.TipoNorma.MetaDatoId,
                        NormaTipo = x.Norma != null ? x.Norma.TipoNorma.Nombre : x.TipoNorma.Nombre,
                        Numero = x.Norma != null ? x.Norma.Numero : x.Numero,
                        FechaPublicacion = x.Norma != null ? (x.Norma.FechaPublicacion == null ? "" : x.Norma.FechaPublicacion.Value.ToString("dd/MM/yyyy")) : (x.FechaPublicacion == null ? "" : x.FechaPublicacion.Value.ToString("dd/MM/yyyy")),
                        Sector = x.Norma != null ? x.Norma.Sector : x.Sector,
                        Articulo = x.Articulo,
                        Url = x.Url,
                        ArchivoAdjuntoId = x.TipoBaseLegal != TipoBaseLegal.RutaInternet ? (x.Norma != null ? 0 : x.ArchivoAdjuntoId) : 0,
                        EstadoACR = x.EstadoACR,
                        DescripcionACR = x.DescripcionACR,
                    }).ToList()
                );

                List<NotaCiudadano> ListaNota = model.NotaCiudadano.ToList();
                List<NotaCiudadano> ListaNC = new List<NotaCiudadano>();

                if (ListaNota.Count == 0 && (model.Editado == null || false))
                {
                    MaestroFijo obj = _maestroFijoService.GetOneByEntidad(user.EntidadId);


                    if (obj == null)
                    {
                        ListaNC = new List<NotaCiudadano>();
                    }
                    else
                    {
                        if (obj.MaestroFijoNotaCiudadano != null)
                            foreach (MaestroFijoNotaCiudadano x in obj.MaestroFijoNotaCiudadano)
                            {
                                ListaNC.Add(new NotaCiudadano
                                {
                                    NotaCiudadanoId = x.NotaCiudadanoId,
                                    TipoNotaId = x.MetaDatoTipoNotaId,
                                    Nota = x.Comentario
                                });
                            }
                    }
                }
                else
                {
                    ListaNC = model.NotaCiudadano;
                }

                ViewBag.ListaNota = JsonConvert.SerializeObject(
                      ListaNC.Select(x => new
                      {
                          x.NotaCiudadanoId,
                          x.TipoNotaId,
                          x.Nota
                      })
                   );


                ViewBag.EsCargaInicial = model.Expediente.TipoExpediente == TipoExpediente.CargaInicial;


                if (userData.Rol != (short)Rol.Administrador && userData.Rol != (short)Rol.AccesoEstandar)
                {
                    ViewBag.EsTupaEstandar = user.Entidad.TipoTupa == TipoTupa.Estandar;
                }
                else
                {
                    ViewBag.EsTupaEstandar = userData.TipoTupa == 2;
                }


                ViewBag.User = userData;

                string viewName = string.Empty;



                if (model.Operacion == OperacionExpediente.Ninguna || model.Operacion == OperacionExpediente.Eliminacion || userData.Rol == (short)Rol.Administrador)
                {

                    if (userData.TipoTupa != 2)
                    {

                        viewName = "VerDatosGenerales";
                    }
                    else
                    {
                        if (exp.EstadoExpediente == EstadoExpediente.Aprobado)
                        {
                            viewName = "VerDatosGenerales";
                        }
                        else
                        {
                            if (model.Operacion == OperacionExpediente.Ninguna || model.Operacion == OperacionExpediente.Eliminacion)
                            {
                                viewName = "VerDatosGenerales";
                            }
                            else
                            {
                                viewName = "DatosGenerales";
                            }
                        }
                    }

                }
                else

                if (model.DatosGeneralesTerminado) viewName = "VerDatosGenerales";
                else
                {
                    switch (userData.Rol)
                    {
                        case (short)Rol.Administrador: viewName = model.TipoProcedimiento == TipoProcedimiento.Estandar ? "DatosGenerales" : "VerDatosGenerales"; break;
                        case (short)Rol.Coordinador: viewName = "DatosGenerales"; break;
                        case (short)Rol.RegistradorProcesos: viewName = "DatosGenerales"; break; //modificar
                        case (short)Rol.Ratificador: viewName = "VerDatosGenerales"; break;
                        case (short)Rol.EntidadFiscalizadora: viewName = "VerDatosGenerales"; break;
                        case (short)Rol.Evaluador: viewName = "VerDatosGenerales"; break;

                        default: viewName = "DatosGenerales"; break; //modificar
                    }
                }

                if (model.Posicion == 0)
                {
                    _AuditoriaService.AlimpiarPosicion(model.ExpedienteId, model.ProcedimientoId);
                }

                //_AuditoriaService.GenerarNumeracion(model.ExpedienteId);
                return View(viewName, model);
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ModelState.AddModelError("DatosGenerales", ex.Message);
                _log.Error(ex);
                return View("_Error");
            }
        }
        public ActionResult InformacionBasica(long id, UsuarioInfo userData)
        {
            try
            {
                int CodigoCanalOficinaEntidad = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/Areas/Simplificacion/Views/Web.config").AppSettings.Settings["AutoCompletarOficinaEntidad"].Value);

                Usuario user = _usuarioService.GetOne(userData.UsuarioId);
                Procedimiento model = _procedimientoService.GetOne(id);

                MaestroFijo modelMaestroFijo = _maestroFijoService.GetOneByEntidad(user.EntidadId);


                if (string.IsNullOrEmpty(model.Anexo))
                {
                    model.Anexo = modelMaestroFijo.Anexo;
                }

                if (string.IsNullOrEmpty(model.Correo))
                {
                    model.Correo = modelMaestroFijo.Correo;
                }

                if (string.IsNullOrEmpty(model.Telefono))
                {
                    model.Telefono = modelMaestroFijo.Telefono;
                }


                ViewBag.Calificacion = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Aprobación Automática", Value = "1" },
                    new SelectListItem() { Text = "Evaluación Previa con Silencio Positivo", Value = "2" },
                    new SelectListItem() { Text = "Evaluación Previa con Silencio Negativo", Value = "3" },
                    new SelectListItem() { Text = "", Value = "4" }
                };

                ViewBag.lrenova = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Aprobación Automática", Value = "1" },
                    new SelectListItem() { Text = "Evaluación Previa con Silencio Positivo", Value = "2" },
                    new SelectListItem() { Text = "Evaluación Previa con Silencio Negativo", Value = "3" },
                    new SelectListItem() { Text = "", Value = "4" }
                };

                List<MetaDato> listaTipoNota = _metaDatoService.GetByParent(15);
                listaTipoNota.Insert(0, new MetaDato() { MetaDatoId = 0, Nombre = "- SELECCIONAR -" });

                ViewBag.TipoNota = JsonConvert.SerializeObject(
                    listaTipoNota.Select(x => new
                    {
                        MetaDatoId = x.MetaDatoId,
                        Nombre = x.Nombre
                    }).ToList()
                );




                ViewBag.ListaSede = JsonConvert.SerializeObject(
                    model.ProcedimientoSede
                    .Select(x => new
                    {
                        SedeId = x.SedeId,
                        Nombre = x.Sede.Nombre,
                        Direccion = x.Sede.Direccion + " - " + x.Sede.Distrito.Ruta,
                        SedeUnidadOrganica = x.Sede.SedeUnidadOrganica.Where(p => p.SedeId == x.SedeId).Select(i => i.UnidadOrganica.Nombre),
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
                    }).ToList()
                );


                List<PasoSeguir> lstPasoSeguir = new List<PasoSeguir>();


                if (model.PasoSeguir.Count == 0 && (model.Editado == null || false))
                {
                    foreach (var obj in modelMaestroFijo.MaestroFijoPasoSeguir)
                    {
                        lstPasoSeguir.Add(new PasoSeguir()
                        {
                            PasoSeguirId = obj.PasoSeguirId,
                            Descripcion = obj.Descripcion
                        });
                    }
                }
                else
                {
                    foreach (PasoSeguir obj in model.PasoSeguir)
                        foreach (var temp in modelMaestroFijo.MaestroFijoPasoSeguir)
                        {

                            {
                                if (temp.PasoSeguirId == obj.PasoSeguirId)
                                {
                                    lstPasoSeguir.Add(new PasoSeguir()
                                    {
                                        PasoSeguirId = obj.PasoSeguirId,
                                        Descripcion = obj.Descripcion
                                    });
                                }

                            }
                        }
                }


                model.PasoSeguir = lstPasoSeguir;

                ViewBag.ListaPasoSeguir = JsonConvert.SerializeObject(
                model.PasoSeguir
                .Select(x => new
                {
                    PasoSeguirId = x.PasoSeguirId,
                    Descripcion = x.Descripcion
                }).ToList()
                    );

                // Datos Adicionales
                List<Dato> datos = _datoService.GetByTipo(TipoDato.TipoDatoAdicionalProc);
                List<Dato> datosAdicionales = new List<Dato>();
                List<Dato> canales = datos.Where(x => x.Entero01 == 1).ToList();

                canales.FirstOrDefault(x => x.DatoId == CodigoCanalOficinaEntidad).OficinasAutocompletar = 1;


                datosAdicionales.AddRange(canales);
                List<Dato> dataFormaPago = datos.Where(x => x.Entero01 == 2).ToList();
                foreach (Dato dfp in dataFormaPago)
                {
                    List<Dato> formasPago = datos.Where(x => x.Entero01 == dfp.MetaDatoId).ToList();
                    formasPago.ForEach(x => x.Padre = dfp);
                    datosAdicionales.AddRange(formasPago);
                }

                List<ProcedimientoDatoAdicional> lst = new List<ProcedimientoDatoAdicional>();


                foreach (Dato data in datosAdicionales)
                {
                    ProcedimientoDatoAdicional da = model.ProcedimientoDatoAdicional.SingleOrDefault(x => x.MetaDatoId == data.MetaDatoId);
                    if (da != null)
                    {
                        da = new ProcedimientoDatoAdicional()
                        {
                            ProcedimientoId = model.ProcedimientoId,
                            MetaDatoId = data.MetaDatoId,
                            Comentario = da.Comentario,
                            Checked = true,
                            MetaDato = data,
                            Tipo = data.Padre == null ? 1 : 2
                        };
                        lst.Add(da);
                    }
                    else
                    {
                        MaestroFijoDatoAdicional obj = modelMaestroFijo.MaestroFijoDatoAdicional.SingleOrDefault(x => x.MetaDatoId == data.MetaDatoId);

                        if (obj != null)
                        {
                            ProcedimientoDatoAdicional objx = new ProcedimientoDatoAdicional()
                            {
                                ProcedimientoId = model.ProcedimientoId,
                                MetaDatoId = data.MetaDatoId,
                                Comentario = obj.Comentario,
                                Checked = true,
                                MetaDato = data,
                                Tipo = data.Padre == null ? 1 : 2
                            };
                            lst.Add(objx);
                        }
                        else
                        {
                            //da.MetaDato = data;
                            //da.Checked = true;
                            //da.Tipo = data.Padre == null ? 1 : 2;
                            da = new ProcedimientoDatoAdicional()
                            {
                                ProcedimientoId = model.ProcedimientoId,
                                MetaDatoId = data.MetaDatoId,
                                Comentario = "",
                                Checked = false,
                                MetaDato = data,
                                Tipo = data.Padre == null ? 1 : 2
                            };
                            lst.Add(da);
                        }

                    }
                }


                model.ProcedimientoDatoAdicional = lst;

                model.ProcedimientoDatoAdicional = model.ProcedimientoDatoAdicional
                                                    .OrderBy(x => x.Tipo)
                                                    .ThenBy(x => x.MetaDatoId)
                                                    .ToList();

                UIT objuit = _UITService.LsitaGetOne();
                ViewBag.ListaModalidad = JsonConvert.SerializeObject(
                        model.TablaAsme
                        .Select(x => new
                        {
                            TablaAsmeId = x.TablaAsmeId,
                            Codigo = x.Codigo,
                            Descripcion = x.Descripcion,
                            EsGratuito = x.EsGratuito,
                            Prestaciones = x.Prestaciones,
                            CostoUnitario = x.CostoUnitario,
                            Calificacion = x.Calificacion,


                            Personal = x.Personal,
                            MaterialFungible = x.MaterialFungible,
                            ServicioIdentificable = x.ServicioIdentificable,
                            MaterialNoFungible = x.MaterialNoFungible,
                            ServicioTerceros = x.ServicioTerceros,
                            Depreciacion = x.Depreciacion,
                            DerechoTramitacion = x.DerechoTramitacion,
                            Fijos = x.Fijos,

                            UIT = objuit.UITID
                        }).ToList()
                    );

                ViewBag.ListaRequisito = JsonConvert.SerializeObject(
                        model.Requisito
                        .Select(x => new
                        {
                            RequisitoId = x.RequisitoId,
                            Nombre = x.Nombre,
                            Descripcion = x.Descripcion,
                            BaseLegalId = x.BaseLegalId,
                            SustentoTecnico = x.SustentoTecnico,
                            BaseLegalTexto = x.BaseLegalTexto,
                            CadenaTramite = x.CadenaTramite,
                            TipoRequisito = x.TipoRequisito,
                            UserCreacion = x.UserCreacion,
                            Activar = x.Activar,
                            Titulo = x.Titulo,
                            EditableTitulo = x.EditableTitulo,
                            RequisitoFormulario = x.RequisitoFormulario.Select(f => new
                            {
                                RequisitoId = f.RequisitoId,
                                FormularioId = f.FormularioId,
                                Nombre = f.Nombre,
                                Url = f.Url,
                                ArchivoAdjuntoId = f.ArchivoAdjuntoId
                            })
                        }).ToList()
                    );

                List<BaseLegalNorma> listaBL = new List<BaseLegalNorma>();
                long idBL = model.BaseLegalId != null ? model.BaseLegalId.Value : 0;
                if (idBL > 0) listaBL = _baseLegalService.GetDetails(idBL);

                ViewBag.ListaBaseLegal = JsonConvert.SerializeObject(
                    listaBL.Select(x => new
                    {
                        BaseLegalId = x.BaseLegalId,
                        BaseLegalNormaId = x.BaseLegalNormaId,
                        TipoBaseLegal = x.TipoBaseLegal,
                        NormaId = x.Norma != null ? x.NormaId.Value : 0,
                        Descripcion = x.Norma != null ? x.Norma.Descripcion : x.Descripcion,
                        TipoNormaId = x.Norma != null ? x.Norma.TipoNorma.MetaDatoId : x.TipoNorma.MetaDatoId,
                        NormaTipo = x.Norma != null ? x.Norma.TipoNorma.Nombre : x.TipoNorma.Nombre,
                        Numero = x.Norma != null ? x.Norma.Numero : x.Numero,
                        FechaPublicacion = x.Norma != null ? (x.Norma.FechaPublicacion == null ? "" : x.Norma.FechaPublicacion.Value.ToString("dd/MM/yyyy")) : (x.FechaPublicacion == null ? "" : x.FechaPublicacion.Value.ToString("dd/MM/yyyy")),
                        Sector = x.Norma != null ? x.Norma.Sector : x.Sector,
                        Articulo = x.Articulo,
                        Url = x.Url,
                        ArchivoAdjuntoId = x.TipoBaseLegal != TipoBaseLegal.RutaInternet ? (x.Norma != null ? 0 : x.ArchivoAdjuntoId) : 0
                    }).ToList()
                );

                List<NotaCiudadano> ListaNota = model.NotaCiudadano.ToList();
                List<NotaCiudadano> ListaNC = new List<NotaCiudadano>();

                if (ListaNota.Count == 0 && (model.Editado == null || false))
                {
                    var obj = _maestroFijoService.GetOneByEntidad(user.EntidadId).MaestroFijoNotaCiudadano;


                    if (obj == null)
                    {
                        ListaNC = new List<NotaCiudadano>();
                    }
                    else
                    {

                        foreach (MaestroFijoNotaCiudadano x in obj)
                        {
                            ListaNC.Add(new NotaCiudadano
                            {
                                NotaCiudadanoId = x.NotaCiudadanoId,
                                TipoNotaId = x.MetaDatoTipoNotaId,
                                Nota = x.Comentario
                            });
                        }
                    }
                }
                else
                {
                    ListaNC = model.NotaCiudadano;
                }

                ViewBag.ListaNota = JsonConvert.SerializeObject(
                      ListaNC.Select(x => new
                      {
                          x.NotaCiudadanoId,
                          x.TipoNotaId,
                          x.Nota
                      })
                   );


                ViewBag.EsCargaInicial = model.Expediente.TipoExpediente == TipoExpediente.CargaInicial;
                ViewBag.EsTupaEstandar = user.Entidad.TipoTupa == TipoTupa.Estandar;
                ViewBag.User = userData;
                //List<SelectListItem> Plazorenovacion = _metaDatoService.GetByParent(14)
                //                     .Select(x => new SelectListItem()
                //                     {
                //                         Value = x.MetaDatoId.ToString(),
                //                         Text = x.Nombre
                //                     })
                //                     .ToList();

                //Plazorenovacion.Insert(0, new SelectListItem() { Value = "0", Text = "- SELECCIONAR -" });

                //ViewBag.Plazorenovacion = Plazorenovacion;

                string viewName = string.Empty;
                if (model.DatosGeneralesTerminado) viewName = "VerDatosGenerales";
                else
                {
                    switch (user.Rol)
                    {
                        case Rol.Administrador: viewName = model.TipoProcedimiento == TipoProcedimiento.Estandar ? "InformacionBasica" : "VerDatosGenerales"; break;
                        case Rol.Coordinador: viewName = "InformacionBasica"; break;
                        case Rol.RegistradorProcesos: viewName = "InformacionBasica"; break; //modificar
                        default: viewName = "InformacionBasica"; break; //modificar
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

        public ActionResult InformacionCiudadano(long id, UsuarioInfo userData)
        {
            try
            {
                /*Inicio Cargar roles - Acceso*/
                List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(userData.Rol), 21);
                userData.rolmenu = listarolmenu.ToList();
                /*Fin Cargar roles  - Acceso*/

                ViewBag.Usuario = userData;
                int CodigoCanalOficinaEntidad = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/Areas/Simplificacion/Views/Web.config").AppSettings.Settings["AutoCompletarOficinaEntidad"].Value);

                Usuario user = _usuarioService.GetOne(userData.UsuarioId);
                Procedimiento model = _procedimientoService.GetOne(id);

                //model.Posicion = 1;
                //_AuditoriaService.AlimpiarPosicion(model.ExpedienteId);
                //_procedimientoService.GuardarPosicion(model);


                if (model.Codigo == "S/C" && !string.IsNullOrEmpty(model.CodigoACR)) return RedirectToAction("NuevoCodigo", "Procedimiento", new { area = "Simplificacion", id = id });

                MaestroFijo modelMaestroFijo = _maestroFijoService.GetOneByEntidad(user.EntidadId);

                Expediente exp = _expedienteService.GetOne(model.ExpedienteId);

                if (modelMaestroFijo == null)
                    modelMaestroFijo = new MaestroFijo();

                if (modelMaestroFijo.MaestroFijoDatoAdicional == null)
                    modelMaestroFijo.MaestroFijoDatoAdicional = new List<MaestroFijoDatoAdicional>();

                if (modelMaestroFijo.MaestroFijoSede == null)
                    modelMaestroFijo.MaestroFijoSede = new List<MaestroFijoSede>();

                if (modelMaestroFijo.MaestroFijoPasoSeguir == null)
                    modelMaestroFijo.MaestroFijoPasoSeguir = new List<MaestroFijoPasoSeguir>();

                if (modelMaestroFijo.MaestroFijoNotaCiudadano == null)
                    modelMaestroFijo.MaestroFijoNotaCiudadano = new List<MaestroFijoNotaCiudadano>();

                if (string.IsNullOrEmpty(model.Anexo))
                {
                    model.Anexo = modelMaestroFijo.Anexo;
                }

                if (string.IsNullOrEmpty(model.Correo))
                {
                    model.Correo = modelMaestroFijo.Correo;
                }

                if (string.IsNullOrEmpty(model.Telefono))
                {
                    model.Telefono = modelMaestroFijo.Telefono;
                }


                ViewBag.Calificacion = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Aprobación Automática", Value = "1" },
                    new SelectListItem() { Text = "Evaluación Previa con Silencio Positivo", Value = "2" },
                    new SelectListItem() { Text = "Evaluación Previa con Silencio Negativo", Value = "3" },
                    new SelectListItem() { Text = "", Value = "4" }
                };

                List<MetaDato> listaTipoNota = _metaDatoService.GetByParent(15);
                listaTipoNota.Insert(0, new MetaDato() { MetaDatoId = 0, Nombre = "- SELECCIONAR -" });

                ViewBag.TipoNota = JsonConvert.SerializeObject(
                    listaTipoNota.Select(x => new
                    {
                        MetaDatoId = x.MetaDatoId,
                        Nombre = x.Nombre
                    }).ToList()
                );

                //List<MaestroFijoSede> sedes = modelMaestroFijo.MaestroFijoSede;

                //for (int i = 0; i < model.ProcedimientoSede.Count(); i++)
                //{

                //    MaestroFijoSede s = sedes.Where(p => p.SedeId == model.ProcedimientoSede[i].SedeId).FirstOrDefault();

                //    if (model.Editado ?? false)
                //        if (s.Mostrar ?? false)
                //        {
                //            model.ProcedimientoSede.Remove(model.ProcedimientoSede[i]);
                //        }
                //}

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
                model.CargoPordependencia = nombrecargo;


                ViewBag.ListaSede = JsonConvert.SerializeObject(
                    model.ProcedimientoSede
                    .Select(x => new
                    {
                        SedeId = x.SedeId,
                        Nombre = x.Sede.Nombre,
                        x.ProcedimientoId,
                        Direccion = x.Sede.Direccion + " - " + x.Sede.Distrito.Ruta,
                        SedeUnidadOrganica = x.Sede.SedeUnidadOrganica != null ? x.Sede.SedeUnidadOrganica.Where(p => p.SedeId == x.SedeId).Select(i => i.UnidadOrganica.Nombre) : new List<string>(),
                        UndOrgRecepcionDocumentos = x.UndOrgRecepcionDocumentos == null ? "" : JsonConvert.SerializeObject(x.UndOrgRecepcionDocumentos.Select(r => new { r.UnidadOrganicaId, r.SedeId, nombre = _unidadOrganicaService.GetOne(r.UnidadOrganicaId).Nombre })),
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
                    }).ToList()
                );


                List<PasoSeguir> lstPasoSeguir = new List<PasoSeguir>();


                if (model.PasoSeguir.Count == 0 && (model.Editado == null || false))
                {
                    foreach (var obj in modelMaestroFijo.MaestroFijoPasoSeguir == null ? new List<MaestroFijoPasoSeguir>() : modelMaestroFijo.MaestroFijoPasoSeguir)
                    {
                        lstPasoSeguir.Add(new PasoSeguir()
                        {
                            PasoSeguirId = obj.PasoSeguirId,
                            Descripcion = obj.Descripcion
                        });
                    }
                }
                else
                {
                    foreach (PasoSeguir obj in model.PasoSeguir)
                    {
                        lstPasoSeguir.Add(new PasoSeguir()
                        {
                            PasoSeguirId = obj.PasoSeguirId,
                            Descripcion = obj.Descripcion
                        });
                    }
                }


                model.PasoSeguir = lstPasoSeguir;

                ViewBag.ListaPasoSeguir = JsonConvert.SerializeObject(
                model.PasoSeguir
                .Select(x => new
                {
                    PasoSeguirId = x.PasoSeguirId,
                    Descripcion = x.Descripcion
                }).ToList()
                    );

                // Datos Adicionales
                List<Dato> datos = _datoService.GetByTipo(TipoDato.TipoDatoAdicionalProc);
                List<Dato> datosAdicionales = new List<Dato>();
                List<Dato> canales = datos.Where(x => x.Entero01 == 1).ToList();

                canales.FirstOrDefault(x => x.DatoId == CodigoCanalOficinaEntidad).OficinasAutocompletar = 1;


                datosAdicionales.AddRange(canales);
                List<Dato> dataFormaPago = datos.Where(x => x.Entero01 == 2).ToList();
                foreach (Dato dfp in dataFormaPago)
                {
                    List<Dato> formasPago = datos.Where(x => x.Entero01 == dfp.MetaDatoId).ToList();
                    formasPago.ForEach(x => x.Padre = dfp);
                    datosAdicionales.AddRange(formasPago);
                }

                List<ProcedimientoDatoAdicional> lst = new List<ProcedimientoDatoAdicional>();


                foreach (Dato data in datosAdicionales)
                {
                    if ((model.Editado ?? false) == true)
                    {
                        ProcedimientoDatoAdicional da = model.ProcedimientoDatoAdicional.SingleOrDefault(x => x.MetaDatoId == data.MetaDatoId);
                        if (da != null)
                        {
                            da = new ProcedimientoDatoAdicional()
                            {
                                ProcedimientoId = model.ProcedimientoId,
                                MetaDatoId = data.MetaDatoId,
                                Comentario = da.Comentario,
                                Checked = da.Checked,
                                MetaDato = data,
                                Tipo = data.Padre == null ? 1 : 2
                            };
                            lst.Add(da);
                        }
                    }
                    else
                    {
                        if (modelMaestroFijo.MaestroFijoDatoAdicional != null)
                        {

                            //MaestroFijoDatoAdicional obj = modelMaestroFijo.MaestroFijoDatoAdicional.SingleOrDefault(x => x.MetaDatoId == data.MetaDatoId);
                            ProcedimientoDatoAdicional obj = model.ProcedimientoDatoAdicional.SingleOrDefault(x => x.MetaDatoId == data.MetaDatoId);
                            if (obj != null)
                            {
                                ProcedimientoDatoAdicional objx = new ProcedimientoDatoAdicional()
                                {
                                    ProcedimientoId = model.ProcedimientoId,
                                    MetaDatoId = data.MetaDatoId,
                                    Comentario = obj.Comentario,
                                    Checked = true,
                                    MetaDato = data,
                                    Tipo = data.Padre == null ? 1 : 2
                                };
                                lst.Add(objx);
                            }
                            else
                            {

                                ProcedimientoDatoAdicional da = new ProcedimientoDatoAdicional();
                                //da.MetaDato = data;
                                //da.Checked = true;
                                //da.Tipo = data.Padre == null ? 1 : 2;
                                da = new ProcedimientoDatoAdicional()
                                {
                                    ProcedimientoId = model.ProcedimientoId,
                                    MetaDatoId = data.MetaDatoId,
                                    Comentario = "",
                                    Checked = false,
                                    MetaDato = data,
                                    Tipo = data.Padre == null ? 1 : 2
                                };
                                lst.Add(da);
                            }
                        }
                    }



                }


                model.ProcedimientoDatoAdicional = lst;

                model.ProcedimientoDatoAdicional = model.ProcedimientoDatoAdicional
                                                    .OrderBy(x => x.Tipo)
                                                    .ThenBy(x => x.MetaDatoId)
                                                    .ToList();




                UIT objuit = _UITService.LsitaGetOne();
                ViewBag.ListaModalidad = JsonConvert.SerializeObject(
                        model.TablaAsme
                        .Select(x => new
                        {
                            TablaAsmeId = x.TablaAsmeId,
                            Codigo = x.Codigo,
                            Descripcion = x.Descripcion,
                            EsGratuito = x.EsGratuito,
                            Prestaciones = x.Prestaciones,
                            CostoUnitario = x.CostoUnitario,
                            Calificacion = x.Calificacion,


                            Personal = x.Personal,
                            MaterialFungible = x.MaterialFungible,
                            ServicioIdentificable = x.ServicioIdentificable,
                            MaterialNoFungible = x.MaterialNoFungible,
                            ServicioTerceros = x.ServicioTerceros,
                            Depreciacion = x.Depreciacion,
                            Fijos = x.Fijos,
                            DerechoTramitacion = x.DerechoTramitacion,


                            UIT = objuit.UITID
                        }).ToList()
                    );

                ViewBag.ListaRequisito = JsonConvert.SerializeObject(
                        model.Requisito
                        .Select(x => new
                        {
                            RequisitoId = x.RequisitoId,
                            Nombre = x.Nombre,
                            Descripcion = x.Descripcion,
                            BaseLegalId = x.BaseLegalId,
                            SustentoTecnico = x.SustentoTecnico,
                            CadenaTramite = x.CadenaTramite,
                            BaseLegalTexto = x.BaseLegalTexto,
                            TipoRequisito = x.TipoRequisito,
                            UserCreacion = x.UserCreacion,
                            Activar = x.Activar,
                            Titulo = x.Titulo,
                            EditableTitulo = x.EditableTitulo,
                            RequisitoFormulario = x.RequisitoFormulario.Select(f => new
                            {
                                RequisitoId = f.RequisitoId,
                                FormularioId = f.FormularioId,
                                Nombre = f.Nombre,
                                Url = f.Url,
                                ArchivoAdjuntoId = f.ArchivoAdjuntoId
                            })
                        }).ToList()
                    );

                List<BaseLegalNorma> listaBL = new List<BaseLegalNorma>();
                long idBL = model.BaseLegalId != null ? model.BaseLegalId.Value : 0;
                if (idBL > 0) listaBL = _baseLegalService.GetDetails(idBL);

                ViewBag.ListaBaseLegal = JsonConvert.SerializeObject(
                    listaBL.Select(x => new
                    {
                        BaseLegalId = x.BaseLegalId,
                        BaseLegalNormaId = x.BaseLegalNormaId,
                        TipoBaseLegal = x.TipoBaseLegal,
                        NormaId = x.Norma != null ? x.NormaId.Value : 0,
                        Descripcion = x.Norma != null ? x.Norma.Descripcion : x.Descripcion,
                        TipoNormaId = x.Norma != null ? x.Norma.TipoNorma.MetaDatoId : x.TipoNorma.MetaDatoId,
                        NormaTipo = x.Norma != null ? x.Norma.TipoNorma.Nombre : x.TipoNorma.Nombre,
                        Numero = x.Norma != null ? x.Norma.Numero : x.Numero,
                        FechaPublicacion = x.Norma != null ? (x.Norma.FechaPublicacion == null ? "" : x.Norma.FechaPublicacion.Value.ToString("dd/MM/yyyy")) : (x.FechaPublicacion == null ? "" : x.FechaPublicacion.Value.ToString("dd/MM/yyyy")),
                        Sector = x.Norma != null ? x.Norma.Sector : x.Sector,
                        Articulo = x.Articulo,
                        Url = x.Url,
                        ArchivoAdjuntoId = x.TipoBaseLegal != TipoBaseLegal.RutaInternet ? (x.Norma != null ? 0 : x.ArchivoAdjuntoId) : 0
                    }).ToList()
                );

                List<NotaCiudadano> ListaNota = model.NotaCiudadano.ToList();
                List<NotaCiudadano> ListaNC = new List<NotaCiudadano>();

                if (ListaNota.Count == 0 && (model.Editado == null || false))
                {
                    var result = _maestroFijoService.GetOneByEntidad(user.EntidadId);

                    var obj = result == null ? null : result.MaestroFijoNotaCiudadano; ;


                    if (obj == null)
                    {
                        ListaNC = new List<NotaCiudadano>();
                    }
                    else
                    {

                        foreach (MaestroFijoNotaCiudadano x in obj)
                        {
                            ListaNC.Add(new NotaCiudadano
                            {
                                NotaCiudadanoId = x.NotaCiudadanoId,
                                TipoNotaId = x.MetaDatoTipoNotaId,
                                Nota = x.Comentario
                            });
                        }
                    }
                }
                else
                {
                    ListaNC = model.NotaCiudadano;
                }

                ViewBag.ListaNota = JsonConvert.SerializeObject(
                      ListaNC.Select(x => new
                      {
                          x.NotaCiudadanoId,
                          x.TipoNotaId,
                          x.Nota
                      })
                   );


                ViewBag.EsCargaInicial = model.Expediente.TipoExpediente == TipoExpediente.CargaInicial;
                ViewBag.EsTupaEstandar = user.Entidad.TipoTupa == TipoTupa.Estandar;
                ViewBag.User = userData;

                string viewName = string.Empty;
                if (model.DatosGeneralesTerminado) viewName = "InformacionCiudadano";
                else
                {
                    switch (user.Rol)
                    {
                        case Rol.Administrador: viewName = model.TipoProcedimiento == TipoProcedimiento.Estandar ? "InformacionCiudadano" : "InformacionCiudadano"; break;
                        case Rol.Coordinador: viewName = "InformacionCiudadano"; break;
                        case Rol.RegistradorProcesos: viewName = "InformacionCiudadano"; break; //modificar
                        default: viewName = "InformacionCiudadano"; break; //modificar
                    }
                }


                if (model.Posicion == 0)
                {
                    _AuditoriaService.AlimpiarPosicion(model.ExpedienteId, model.ProcedimientoId);
                }
                //_AuditoriaService.GenerarNumeracion(model.ExpedienteId);

                return View(viewName, model);
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ModelState.AddModelError("InformacionCiudadano", ex.Message);
                _log.Error(ex);
                return View("_Error");
            }
        }
        public ActionResult VerObsInformacionCiudadano(long id, long entidadid, UsuarioInfo userData)
        {
            try
            {
                ViewBag.entidadver = entidadid;
                ViewBag.Usuario = userData;
                int CodigoCanalOficinaEntidad = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/Areas/Simplificacion/Views/Web.config").AppSettings.Settings["AutoCompletarOficinaEntidad"].Value);

                Usuario user = _usuarioService.GetOne(userData.UsuarioId);
                Procedimiento model = _procedimientoService.GetOne(id);
                //model.Posicion = 1;
                //_AuditoriaService.AlimpiarPosicion(model.ExpedienteId);
                //_procedimientoService.GuardarPosicion(model);
                //_AuditoriaService.AlimpiarPosicion(model.ExpedienteId, model.ProcedimientoId);

                if (model.Codigo == "S/C" && !string.IsNullOrEmpty(model.CodigoACR)) return RedirectToAction("NuevoCodigo", "Procedimiento", new { area = "Simplificacion", id = id });

                MaestroFijo modelMaestroFijo = _maestroFijoService.GetOneByEntidad(user.EntidadId);

                Expediente exp = _expedienteService.GetOne(model.ExpedienteId);

                if (modelMaestroFijo == null)
                    modelMaestroFijo = new MaestroFijo();

                if (modelMaestroFijo.MaestroFijoDatoAdicional == null)
                    modelMaestroFijo.MaestroFijoDatoAdicional = new List<MaestroFijoDatoAdicional>();

                if (modelMaestroFijo.MaestroFijoSede == null)
                    modelMaestroFijo.MaestroFijoSede = new List<MaestroFijoSede>();

                if (modelMaestroFijo.MaestroFijoPasoSeguir == null)
                    modelMaestroFijo.MaestroFijoPasoSeguir = new List<MaestroFijoPasoSeguir>();

                if (modelMaestroFijo.MaestroFijoNotaCiudadano == null)
                    modelMaestroFijo.MaestroFijoNotaCiudadano = new List<MaestroFijoNotaCiudadano>();

                if (string.IsNullOrEmpty(model.Anexo))
                {
                    model.Anexo = modelMaestroFijo.Anexo;
                }

                if (string.IsNullOrEmpty(model.Correo))
                {
                    model.Correo = modelMaestroFijo.Correo;
                }

                if (string.IsNullOrEmpty(model.Telefono))
                {
                    model.Telefono = modelMaestroFijo.Telefono;
                }


                ViewBag.Calificacion = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Aprobación Automática", Value = "1" },
                    new SelectListItem() { Text = "Evaluación Previa con Silencio Positivo", Value = "2" },
                    new SelectListItem() { Text = "Evaluación Previa con Silencio Negativo", Value = "3" },
                    new SelectListItem() { Text = "", Value = "4" }
                };

                List<MetaDato> listaTipoNota = _metaDatoService.GetByParent(15);
                listaTipoNota.Insert(0, new MetaDato() { MetaDatoId = 0, Nombre = "- SELECCIONAR -" });

                ViewBag.TipoNota = JsonConvert.SerializeObject(
                    listaTipoNota.Select(x => new
                    {
                        MetaDatoId = x.MetaDatoId,
                        Nombre = x.Nombre
                    }).ToList()
                );

                ViewBag.ListaSede = JsonConvert.SerializeObject(
                    model.ProcedimientoSede
                    .Select(x => new
                    {
                        SedeId = x.SedeId,
                        Nombre = x.Sede.Nombre,
                        x.ProcedimientoId,
                        Direccion = x.Sede.Direccion + " - " + x.Sede.Distrito.Ruta,
                        SedeUnidadOrganica = x.Sede.SedeUnidadOrganica != null ? x.Sede.SedeUnidadOrganica.Where(p => p.SedeId == x.SedeId).Select(i => i.UnidadOrganica.Nombre) : new List<string>(),
                        UndOrgRecepcionDocumentos = x.UndOrgRecepcionDocumentos == null ? "" : JsonConvert.SerializeObject(x.UndOrgRecepcionDocumentos.Select(r => new { r.UnidadOrganicaId, r.SedeId, nombre = _unidadOrganicaService.GetOne(r.UnidadOrganicaId).Nombre })),
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
                    }).ToList()
                );


                List<PasoSeguir> lstPasoSeguir = new List<PasoSeguir>();


                if (model.PasoSeguir.Count == 0 && (model.Editado == null || false))
                {
                    foreach (var obj in modelMaestroFijo.MaestroFijoPasoSeguir == null ? new List<MaestroFijoPasoSeguir>() : modelMaestroFijo.MaestroFijoPasoSeguir)
                    {
                        lstPasoSeguir.Add(new PasoSeguir()
                        {
                            PasoSeguirId = obj.PasoSeguirId,
                            Descripcion = obj.Descripcion
                        });
                    }
                }
                else
                {
                    foreach (PasoSeguir obj in model.PasoSeguir)
                    {
                        lstPasoSeguir.Add(new PasoSeguir()
                        {
                            PasoSeguirId = obj.PasoSeguirId,
                            Descripcion = obj.Descripcion
                        });
                    }
                }


                model.PasoSeguir = lstPasoSeguir;

                ViewBag.ListaPasoSeguir = JsonConvert.SerializeObject(
                model.PasoSeguir
                .Select(x => new
                {
                    PasoSeguirId = x.PasoSeguirId,
                    Descripcion = x.Descripcion
                }).ToList()
                    );

                // Datos Adicionales
                List<Dato> datos = _datoService.GetByTipo(TipoDato.TipoDatoAdicionalProc);
                List<Dato> datosAdicionales = new List<Dato>();
                List<Dato> canales = datos.Where(x => x.Entero01 == 1).ToList();

                canales.FirstOrDefault(x => x.DatoId == CodigoCanalOficinaEntidad).OficinasAutocompletar = 1;


                datosAdicionales.AddRange(canales);
                List<Dato> dataFormaPago = datos.Where(x => x.Entero01 == 2).ToList();
                foreach (Dato dfp in dataFormaPago)
                {
                    List<Dato> formasPago = datos.Where(x => x.Entero01 == dfp.MetaDatoId).ToList();
                    formasPago.ForEach(x => x.Padre = dfp);
                    datosAdicionales.AddRange(formasPago);
                }

                List<ProcedimientoDatoAdicional> lst = new List<ProcedimientoDatoAdicional>();


                foreach (Dato data in datosAdicionales)
                {
                    if ((model.Editado ?? false) == true)
                    {
                        ProcedimientoDatoAdicional da = model.ProcedimientoDatoAdicional.SingleOrDefault(x => x.MetaDatoId == data.MetaDatoId);
                        if (da != null)
                        {
                            da = new ProcedimientoDatoAdicional()
                            {
                                ProcedimientoId = model.ProcedimientoId,
                                MetaDatoId = data.MetaDatoId,
                                Comentario = da.Comentario,
                                Checked = da.Checked,
                                MetaDato = data,
                                Tipo = data.Padre == null ? 1 : 2
                            };
                            lst.Add(da);
                        }
                    }
                    else
                    {
                        if (modelMaestroFijo.MaestroFijoDatoAdicional != null)
                        {
                            ProcedimientoDatoAdicional obj = model.ProcedimientoDatoAdicional.SingleOrDefault(x => x.MetaDatoId == data.MetaDatoId);
                            if (obj != null)
                            {
                                ProcedimientoDatoAdicional objx = new ProcedimientoDatoAdicional()
                                {
                                    ProcedimientoId = model.ProcedimientoId,
                                    MetaDatoId = data.MetaDatoId,
                                    Comentario = obj.Comentario,
                                    Checked = true,
                                    MetaDato = data,
                                    Tipo = data.Padre == null ? 1 : 2
                                };
                                lst.Add(objx);
                            }
                            else
                            {

                                ProcedimientoDatoAdicional da = new ProcedimientoDatoAdicional();
                                da = new ProcedimientoDatoAdicional()
                                {
                                    ProcedimientoId = model.ProcedimientoId,
                                    MetaDatoId = data.MetaDatoId,
                                    Comentario = "",
                                    Checked = false,
                                    MetaDato = data,
                                    Tipo = data.Padre == null ? 1 : 2
                                };
                                lst.Add(da);
                            }
                        }
                    }

                }

                model.ProcedimientoDatoAdicional = lst;

                model.ProcedimientoDatoAdicional = model.ProcedimientoDatoAdicional
                                                    .OrderBy(x => x.Tipo)
                                                    .ThenBy(x => x.MetaDatoId)
                                                    .ToList();
                UIT objuit = _UITService.LsitaGetOne();
                ViewBag.ListaModalidad = JsonConvert.SerializeObject(
                        model.TablaAsme
                        .Select(x => new
                        {
                            TablaAsmeId = x.TablaAsmeId,
                            Codigo = x.Codigo,
                            Descripcion = x.Descripcion,
                            EsGratuito = x.EsGratuito,
                            Prestaciones = x.Prestaciones,
                            CostoUnitario = x.CostoUnitario,
                            Calificacion = x.Calificacion,


                            Personal = x.Personal,
                            MaterialFungible = x.MaterialFungible,
                            ServicioIdentificable = x.ServicioIdentificable,
                            MaterialNoFungible = x.MaterialNoFungible,
                            ServicioTerceros = x.ServicioTerceros,
                            Depreciacion = x.Depreciacion,
                            Fijos = x.Fijos,
                            DerechoTramitacion = x.DerechoTramitacion,

                            UIT = objuit.UITID
                        }).ToList()
                    );

                ViewBag.ListaRequisito = JsonConvert.SerializeObject(
                        model.Requisito
                        .Select(x => new
                        {
                            RequisitoId = x.RequisitoId,
                            Nombre = x.Nombre,
                            Descripcion = x.Descripcion,
                            BaseLegalId = x.BaseLegalId,
                            SustentoTecnico = x.SustentoTecnico,
                            CadenaTramite = x.CadenaTramite,
                            BaseLegalTexto = x.BaseLegalTexto,
                            TipoRequisito = x.TipoRequisito,
                            UserCreacion = x.UserCreacion,
                            Activar = x.Activar,
                            Titulo = x.Titulo,
                            EditableTitulo = x.EditableTitulo,
                            RequisitoFormulario = x.RequisitoFormulario.Select(f => new
                            {
                                RequisitoId = f.RequisitoId,
                                FormularioId = f.FormularioId,
                                Nombre = f.Nombre,
                                Url = f.Url,
                                ArchivoAdjuntoId = f.ArchivoAdjuntoId
                            })
                        }).ToList()
                    );

                List<BaseLegalNorma> listaBL = new List<BaseLegalNorma>();
                long idBL = model.BaseLegalId != null ? model.BaseLegalId.Value : 0;
                if (idBL > 0) listaBL = _baseLegalService.GetDetails(idBL);

                ViewBag.ListaBaseLegal = JsonConvert.SerializeObject(
                    listaBL.Select(x => new
                    {
                        BaseLegalId = x.BaseLegalId,
                        BaseLegalNormaId = x.BaseLegalNormaId,
                        TipoBaseLegal = x.TipoBaseLegal,
                        NormaId = x.Norma != null ? x.NormaId.Value : 0,
                        Descripcion = x.Norma != null ? x.Norma.Descripcion : x.Descripcion,
                        TipoNormaId = x.Norma != null ? x.Norma.TipoNorma.MetaDatoId : x.TipoNorma.MetaDatoId,
                        NormaTipo = x.Norma != null ? x.Norma.TipoNorma.Nombre : x.TipoNorma.Nombre,
                        Numero = x.Norma != null ? x.Norma.Numero : x.Numero,
                        FechaPublicacion = x.Norma != null ? (x.Norma.FechaPublicacion == null ? "" : x.Norma.FechaPublicacion.Value.ToString("dd/MM/yyyy")) : (x.FechaPublicacion == null ? "" : x.FechaPublicacion.Value.ToString("dd/MM/yyyy")),
                        Sector = x.Norma != null ? x.Norma.Sector : x.Sector,
                        Articulo = x.Articulo,
                        Url = x.Url,
                        ArchivoAdjuntoId = x.TipoBaseLegal != TipoBaseLegal.RutaInternet ? (x.Norma != null ? 0 : x.ArchivoAdjuntoId) : 0
                    }).ToList()
                );

                List<NotaCiudadano> ListaNota = model.NotaCiudadano.ToList();
                List<NotaCiudadano> ListaNC = new List<NotaCiudadano>();

                if (ListaNota.Count == 0 && (model.Editado == null || false))
                {
                    var result = _maestroFijoService.GetOneByEntidad(user.EntidadId);

                    var obj = result == null ? null : result.MaestroFijoNotaCiudadano; ;


                    if (obj == null)
                    {
                        ListaNC = new List<NotaCiudadano>();
                    }
                    else
                    {

                        foreach (MaestroFijoNotaCiudadano x in obj)
                        {
                            ListaNC.Add(new NotaCiudadano
                            {
                                NotaCiudadanoId = x.NotaCiudadanoId,
                                TipoNotaId = x.MetaDatoTipoNotaId,
                                Nota = x.Comentario
                            });
                        }
                    }
                }
                else
                {
                    ListaNC = model.NotaCiudadano;
                }

                ViewBag.ListaNota = JsonConvert.SerializeObject(
                      ListaNC.Select(x => new
                      {
                          x.NotaCiudadanoId,
                          x.TipoNotaId,
                          x.Nota
                      })
                   );


                ViewBag.EsCargaInicial = model.Expediente.TipoExpediente == TipoExpediente.CargaInicial;
                ViewBag.EsTupaEstandar = user.Entidad.TipoTupa == TipoTupa.Estandar;
                ViewBag.User = userData;

                string viewName = string.Empty;
                viewName = "VerObsInformacionCiudadano";

                if (model.Posicion == 0)
                {
                    _AuditoriaService.AlimpiarPosicion(model.ExpedienteId, model.ProcedimientoId);
                }
                //_AuditoriaService.GenerarNumeracion(model.ExpedienteId);
                return View(viewName, model);
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ModelState.AddModelError("VerObsInformacionCiudadano", ex.Message);
                _log.Error(ex);
                return View("_Error");
            }
        }
        public ActionResult TablaAsme(long id, UsuarioInfo user)
        {
            try
            {

                /*Inicio Cargar roles - Acceso*/
                List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 24);
                user.rolmenu = listarolmenu.ToList();
                /*Fin Cargar roles  - Acceso*/

                ViewBag.Usuario = user;
                Procedimiento model = _procedimientoService.GetOne(id);

                //model.Posicion = 1;
                //_AuditoriaService.AlimpiarPosicion(model.ExpedienteId);
                //_procedimientoService.GuardarPosicion(model);
                //_AuditoriaService.AlimpiarPosicion(model.ExpedienteId, model.ProcedimientoId);

                if (model.Codigo == "S/C" && !string.IsNullOrEmpty(model.CodigoACR)) return RedirectToAction("NuevoCodigo", "Procedimiento", new { area = "Simplificacion", id = id });

                switch (model.Calificacion)
                {
                    case CalificacionProcedimiento.Automatica: ViewBag.Calificacion = "Aprobación Automática"; break;
                    case CalificacionProcedimiento.SilencioPositivo: ViewBag.Calificacion = "Evaluación Previa con Silencio Positivo"; break;
                    case CalificacionProcedimiento.SilencioNegativo: ViewBag.Calificacion = "Evaluación Previa con Silencio Negativo"; break;
                }

                List<object> lista = new List<object>();
                for (int i = 0; i < model.Requisito.Count(); i++)
                {
                    List<BaseLegalNorma> blList = _baseLegalService.GetDetails(model.Requisito[i].BaseLegalId.Value);
                    lista.Add(blList.Select(x => new
                    {
                        BaseLegalId = x.BaseLegalId,
                        BaseLegalNormaId = x.BaseLegalNormaId,
                        TipoBaseLegal = x.TipoBaseLegal,
                        NormaId = x.Norma != null ? x.NormaId.Value : 0,
                        Descripcion = x.Norma != null ? x.Norma.Descripcion : x.Descripcion,
                        TipoNormaId = x.Norma != null ? x.Norma.TipoNorma.MetaDatoId : x.TipoNorma.MetaDatoId,
                        NormaTipo = x.Norma != null ? x.Norma.TipoNorma.Nombre : x.TipoNorma.Nombre,
                        Numero = x.Norma != null ? x.Norma.Numero : x.Numero,
                        FechaPublicacion = x.Norma != null ? (x.Norma.FechaPublicacion == null ? "" : x.Norma.FechaPublicacion.Value.ToString("dd/MM/yyyy")) : (x.FechaPublicacion == null ? "" : x.FechaPublicacion.Value.ToString("dd/MM/yyyy")),
                        Sector = x.Norma != null ? x.Norma.Sector : x.Sector,
                        Articulo = x.Articulo,
                        Url = x.Url,
                        ArchivoAdjuntoId = x.TipoBaseLegal != TipoBaseLegal.RutaInternet ? (x.Norma != null ? 0 : x.ArchivoAdjuntoId) : 0
                    }).ToList()
                    );
                }

                ViewBag.ListaBaseLegal = JsonConvert.SerializeObject(lista);
                ViewBag.User = user;

                string viewName = string.Empty;

                if (model.Operacion == OperacionExpediente.Ninguna || model.Operacion == OperacionExpediente.Eliminacion || user.Rol == (short)Rol.Administrador)
                {
                    if (user.TipoTupa != 2)
                    {
                        viewName = "VerTablaAsme";
                    }
                    else
                    {
                        if (model.Expediente.EstadoExpediente == EstadoExpediente.Aprobado)
                        {
                            viewName = "VerTablaAsme";
                        }
                        else
                        {

                            if (model.Operacion == OperacionExpediente.Ninguna || model.Operacion == OperacionExpediente.Eliminacion)
                            {
                                viewName = "VerTablaAsme";
                            }
                            else
                            {
                                viewName = "TablaAsme";
                            }
                        }
                    }
                }
                else
                    if (model.TablaAsmeTerminado) viewName = "VerTablaAsme";
                else
                {
                    switch (user.Rol)
                    {
                        case (short)Rol.Administrador: viewName = model.TipoProcedimiento == TipoProcedimiento.Estandar ? "TablaAsme" : "VerTablaAsme"; break;
                        case (short)Rol.Coordinador: viewName = "TablaAsme"; break;
                        case (short)Rol.RegistradorProcesos: viewName = "TablaAsme"; break; //modificar
                        case (short)Rol.Ratificador: viewName = "VerTablaAsme"; break; //modificar
                        case (short)Rol.Evaluador: viewName = "VerTablaAsme"; break; //modificar
                        case (short)Rol.EntidadFiscalizadora: viewName = "VerTablaAsme"; break; //modificar
                    }
                }


                if (model.Posicion == 0)
                {
                    _AuditoriaService.AlimpiarPosicion(model.ExpedienteId, model.ProcedimientoId);
                }
                //_AuditoriaService.AlimpiarPosicion(model.ExpedienteId, model.ProcedimientoId);
                //_AuditoriaService.GenerarNumeracion(model.ExpedienteId);

                return View(viewName, model);
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ModelState.AddModelError("TablaAsme", ex.Message);
                _log.Error(ex);
                return View("_Error");
            }
        }
        public ActionResult VerObsTablaAsme(long id, long entidadid, UsuarioInfo user)
        {
            try
            {

                ViewBag.entidadver = entidadid;
                ViewBag.Usuario = user;
                Procedimiento model = _procedimientoService.GetOne(id);

                //model.Posicion = 1;
                //_AuditoriaService.AlimpiarPosicion(model.ExpedienteId);
                //_procedimientoService.GuardarPosicion(model);
                //_AuditoriaService.AlimpiarPosicion(model.ExpedienteId, model.ProcedimientoId);

                if (model.Codigo == "S/C" && !string.IsNullOrEmpty(model.CodigoACR)) return RedirectToAction("NuevoCodigo", "Procedimiento", new { area = "Simplificacion", id = id });

                switch (model.Calificacion)
                {
                    case CalificacionProcedimiento.Automatica: ViewBag.Calificacion = "Aprobación Automática"; break;
                    case CalificacionProcedimiento.SilencioPositivo: ViewBag.Calificacion = "Evaluación Previa con Silencio Positivo"; break;
                    case CalificacionProcedimiento.SilencioNegativo: ViewBag.Calificacion = "Evaluación Previa con Silencio Negativo"; break;
                }

                List<object> lista = new List<object>();
                for (int i = 0; i < model.Requisito.Count(); i++)
                {
                    List<BaseLegalNorma> blList = _baseLegalService.GetDetails(model.Requisito[i].BaseLegalId.Value);
                    lista.Add(blList.Select(x => new
                    {
                        BaseLegalId = x.BaseLegalId,
                        BaseLegalNormaId = x.BaseLegalNormaId,
                        TipoBaseLegal = x.TipoBaseLegal,
                        NormaId = x.Norma != null ? x.NormaId.Value : 0,
                        Descripcion = x.Norma != null ? x.Norma.Descripcion : x.Descripcion,
                        TipoNormaId = x.Norma != null ? x.Norma.TipoNorma.MetaDatoId : x.TipoNorma.MetaDatoId,
                        NormaTipo = x.Norma != null ? x.Norma.TipoNorma.Nombre : x.TipoNorma.Nombre,
                        Numero = x.Norma != null ? x.Norma.Numero : x.Numero,
                        FechaPublicacion = x.Norma != null ? (x.Norma.FechaPublicacion == null ? "" : x.Norma.FechaPublicacion.Value.ToString("dd/MM/yyyy")) : (x.FechaPublicacion == null ? "" : x.FechaPublicacion.Value.ToString("dd/MM/yyyy")),
                        Sector = x.Norma != null ? x.Norma.Sector : x.Sector,
                        Articulo = x.Articulo,
                        Url = x.Url,
                        ArchivoAdjuntoId = x.TipoBaseLegal != TipoBaseLegal.RutaInternet ? (x.Norma != null ? 0 : x.ArchivoAdjuntoId) : 0
                    }).ToList()
                    );
                }

                ViewBag.ListaBaseLegal = JsonConvert.SerializeObject(lista);
                ViewBag.User = user;

                string viewName = string.Empty;
                viewName = "VerObsTablaAsme";

                if (model.Posicion == 0)
                {
                    _AuditoriaService.AlimpiarPosicion(model.ExpedienteId, model.ProcedimientoId);
                }
                //_AuditoriaService.AlimpiarPosicion(model.ExpedienteId, model.ProcedimientoId);
                //_AuditoriaService.GenerarNumeracion(model.ExpedienteId);
                return View(viewName, model);
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ModelState.AddModelError("VerObsTablaAsme", ex.Message);
                _log.Error(ex);
                return View("_Error");
            }
        }
        [HttpPost]
        public ActionResult DatosGenerales(Procedimiento model, UsuarioInfo user)
        {
            try
            {
                //FVN Inicio
                //Validar que se haya realizado modificacion en la denominacion
                List<Requisito> lista = new List<Requisito>();

                // cambiar el estado
                model.EstadoNinguno = 0;

                //model.Posicion = 1;
                ////_procedimientoService.GuardarPosicion(model);
                ///
                //_AuditoriaService.AlimpiarPosicion(model.ExpedienteId, model.ProcedimientoId);

                var RE = _requisitoService.GetByProcedimientoELI(model.ProcedimientoId);

                if (model.Requisito != null)
                {
                    foreach (Requisito req in model.Requisito)
                    {

                        if (req.Nombre == null)
                        {
                            lista.Add(req);
                        }
                        else if (req.TipoRequisito == 0)
                        {
                            lista.Add(req);
                        }
                    }
                    if (lista.ToString() != "")
                    {
                        foreach (Requisito listar in lista)
                        {
                            model.Requisito.Remove(listar);
                        }
                    }
                }

                if (RE != null)
                {
                    foreach (Requisito req in RE)
                    {
                        model.Requisito.Add(req);
                    }

                }


                if (model.TablaAsme != null)
                {
                    foreach (TablaAsme req in model.TablaAsme)
                    {
                        if (req.EsGratuito == true)
                        {
                            List<TablaAsmeReproduccion> repro = _tablaAsmeReproduccionService.GetAll(req.TablaAsmeId);

                            foreach (TablaAsmeReproduccion tablrep in repro)
                            {
                                _tablaAsmeReproduccionService.Eliminar(tablrep.ReproduccionId);

                            }
                        }
                    }
                }
                model.TablaAsme.ForEach(x =>
                {
                    model.EsGratuito = x.EsGratuito;
                });

                if (model.DenominacionAnterior != model.Denominacion)
                {
                    model.FlagCopiarProcedimiento = 2;
                }
                //else {
                //    model.FlagCopiarProcedimiento = 1;
                //}
                //FVN Fin


                if (model.ProcedimientoSede == null) model.ProcedimientoSede = new List<ProcedimientoSede>();
                else model.ProcedimientoSede.ForEach(x => x.ProcedimientoId = model.ProcedimientoId);

                if (model.NotaCiudadano == null) model.NotaCiudadano = new List<NotaCiudadano>();
                else model.NotaCiudadano.ForEach(x => x.ProcedimientoId = model.ProcedimientoId);

                if (model.PasoSeguir == null) model.PasoSeguir = new List<PasoSeguir>();
                else model.PasoSeguir.ForEach(x => x.ProcedimientoId = model.ProcedimientoId);

                int conteo = 0;
                if (model.Requisito == null) model.Requisito = new List<Requisito>();
                else
                {
                    foreach (Requisito req in model.Requisito)
                    {


                        req.ProcedimientoId = model.ProcedimientoId;

                        if (model.Requisito[conteo].UserCreacion == "ACR")
                        {
                            if (model.Requisito[conteo].Activar == 0)
                            {
                                req.Nombre = model.Requisito[conteo].Nombre + "" + model.Requisito[conteo].Descripcion;
                                req.UserModificacion = user.NroDocumento;
                                req.FecModificacion = DateTime.Now;
                            }
                        }
                        else if (model.Requisito[conteo].UserCreacion == "ACRExante")
                        {
                            if (model.Requisito[conteo].Activar == 0)
                            {
                                req.Nombre = model.Requisito[conteo].Nombre + "" + model.Requisito[conteo].Descripcion;
                                req.UserModificacion = user.NroDocumento;
                                req.FecModificacion = DateTime.Now;
                            }
                        }
                        else
                        {
                            req.UserModificacion = user.NroDocumento;
                            req.FecModificacion = DateTime.Now;
                        }

                        req.Activar = 0;


                        if (req.RequisitoFormulario == null) req.RequisitoFormulario = new List<RequisitoFormulario>();
                        else
                        {
                            for (int i = 0; i < req.RequisitoFormulario.Count; i++)
                            {
                                req.RequisitoFormulario[i].FormularioId = i + 1;
                                req.RequisitoFormulario[i].RequisitoId = req.RequisitoId;
                                req.RequisitoFormulario[i].ArchivoAdjunto = null;
                                if (req.RequisitoFormulario[i].ArchivoAdjuntoId == 0) req.RequisitoFormulario[i].ArchivoAdjuntoId = null;
                            }
                        }
                        conteo = conteo + 1;

                    }
                }

                int reccontu = 0;
                int reccontu2 = 0;
                foreach (Requisito req in model.Requisito)
                {
                    if (req.TipoRequisito == TipoRequisito.General)
                    {
                        reccontu = reccontu + 1;
                        model.Prequisitos = reccontu;
                    }
                    {
                        reccontu2 = reccontu2 + 1;
                        model.Prequisitos = reccontu2;
                    }

                }


                if (model.BaseLegal == null)
                {
                    model.BaseLegal = new BaseLegal();
                    model.BaseLegal.BaseLegalNorma = new List<BaseLegalNorma>();
                }
                else if (model.BaseLegal.BaseLegalNorma == null) model.BaseLegal.BaseLegalNorma = new List<BaseLegalNorma>();
                else model.BaseLegal.BaseLegalNorma.ForEach(x => x.BaseLegalId = model.BaseLegalId.Value);

                for (int i = 0; i < model.BaseLegal.BaseLegalNorma.Count(); i++)
                {
                    model.BaseLegal.BaseLegalNorma[i].BaseLegalNormaId = i + 1;
                    model.BaseLegal.BaseLegalNorma[i].Norma = null;
                    model.BaseLegal.BaseLegalNorma[i].ArchivoAdjunto = null;
                    if (model.BaseLegal.BaseLegalNorma[i].EstadoACR == "2")
                    {
                        model.BaseLegal.BaseLegalNorma[i].TipoNormaId = 141;
                    }
                    if (model.BaseLegal.BaseLegalNorma[i].NormaId == 0) model.BaseLegal.BaseLegalNorma[i].NormaId = null;
                    if (model.BaseLegal.BaseLegalNorma[i].ArchivoAdjuntoId == 0) model.BaseLegal.BaseLegalNorma[i].ArchivoAdjuntoId = null;
                }
                UIT objuit = _UITService.LsitaGetOne();
                model.TablaAsme.ForEach(x =>
                {
                    x.ProcedimientoId = model.ProcedimientoId;
                    x.UITID = objuit.UITID;
                    //_log.Info(x.EsGratuito.ToString());
                    model.Pprestacionesanuales = model.TablaAsme[0].Prestaciones;
                });


                //model.ProcedimientoDatoAdicional.RemoveAll(x => !x.Checked);
                if (model.TipoProcedimiento == TipoProcedimiento.Estandar)
                {
                    if (user.Rol == 1)
                    {
                        model.PlazoAtencionEstandar = model.PlazoAtencion;
                    }
                    else { model.PlazoAtencionEstandar = model.PlazoAtencionEstandar; }
                }
                model.Expediente = null;
                model.UndOrgResponsable = null;
                model.UndOrgOtros = null;
                model.UndOrgReconsideracion = null;
                model.UndOrgApelacion = null;
                model.UndOrgPresentDocumentacion = null;
                model.TipoAtencion = null;
                model.Editado = true;
                model.FecModificacion = DateTime.Now;

                _procedimientoService.Save(model);

                Expediente objexp = _expedienteService.GetOne(model.ExpedienteId);
                //objexp.SustCostosTerminado = false; 
                _expedienteService.Save(objexp);
                //_AuditoriaService.AlimpiarPosicion(model.ExpedienteId, model.ProcedimientoId);
                //_AuditoriaService.GenerarNumeracion(model.ExpedienteId);
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
        public ActionResult DatosGeneralesResolver(Procedimiento model, UsuarioInfo user)
        {
            try
            {

                _AuditoriaService.ActualizarResolver(model.ProcedimientoId, model.PzoReconResol);

                if (model.ProcedimientoCargos != null)
                {
                    foreach (ProcedimientoCargos c in model.ProcedimientoCargos)
                    {
                        _AuditoriaService.ActualizarResolverDet(c.ProcedimientoCargosID, c.PzoResol);
                    }
                }

                //_procedimientoService.Save(model);

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
        public ActionResult GurdarFormulario(int RequisitoId, int FormularioId, string Url, string Nombre, int ArchivoAdjuntoId, int tipo, UsuarioInfo user)
        {
            try
            {

                int valorform = 1;

                List<RequisitoFormulario> listafor = new List<RequisitoFormulario>();

                List<RequisitoFormulario> reqlist = new List<RequisitoFormulario>();
                reqlist = _requisitoService.listProcedimiento(RequisitoId).ToList();



                if (tipo == 1)
                {
                    foreach (RequisitoFormulario reqfor in reqlist)
                    {

                        listafor.Add(reqfor);

                        valorform = valorform + 1;
                    }


                    RequisitoFormulario formulario = new RequisitoFormulario();
                    formulario.RequisitoId = RequisitoId;
                    formulario.FormularioId = valorform;
                    formulario.Url = Url;
                    formulario.Nombre = Nombre;
                    if (ArchivoAdjuntoId != 0)
                    {
                        formulario.ArchivoAdjuntoId = ArchivoAdjuntoId;
                    }
                    listafor.Add(formulario);
                }
                else
                {

                    foreach (RequisitoFormulario reqfor in reqlist)
                    {

                        if (reqfor.FormularioId == FormularioId)
                        {
                            reqfor.Url = Url;
                            reqfor.Nombre = Nombre;

                            if (ArchivoAdjuntoId != 0)
                            {
                                reqfor.ArchivoAdjuntoId = ArchivoAdjuntoId;
                            }


                        }
                        listafor.Add(reqfor);
                    }

                }

                Requisito req = new Requisito();
                req = _requisitoService.GetOne(RequisitoId);
                req.RequisitoFormulario = listafor;


                _requisitoService.Save(req);

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
        public ActionResult EliminarFormulario(int RequisitoId, int FormularioId, UsuarioInfo user)
        {
            try
            {

                int valorform = 1;

                List<RequisitoFormulario> listafor = new List<RequisitoFormulario>();

                List<RequisitoFormulario> reqlist = new List<RequisitoFormulario>();
                reqlist = _requisitoService.listProcedimiento(RequisitoId).ToList();


                if (FormularioId != 1)
                {
                    foreach (RequisitoFormulario reqfor in reqlist)
                    {

                        if (reqfor.FormularioId != FormularioId)
                        {
                            listafor.Add(reqfor);

                        }

                    }
                }

                Requisito req = new Requisito();
                req = _requisitoService.GetOne(RequisitoId);
                req.RequisitoFormulario = listafor;


                _requisitoService.Save(req);

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
        public ActionResult DatosGeneralesModalidad(Procedimiento model, UsuarioInfo user)
        {
            try
            {
                //FVN Inicio
                //Validar que se haya realizado modificacion en la denominacion
                List<Requisito> lista = new List<Requisito>();
                List<RequisitoFormulario> listafor = new List<RequisitoFormulario>();



                if (model.Requisito != null)
                {
                    foreach (Requisito req in model.Requisito)
                    {
                        //string nombrefor = "";
                        if (req.Nombre == null)
                        {
                            lista.Add(req);
                        }
                        else if (req.TipoRequisito == 0)
                        {
                            lista.Add(req);
                        }
                        //if (req.RequisitoFormulario != null) { 
                        //    foreach (RequisitoFormulario reqfor in req.RequisitoFormulario)
                        //    {

                        //        if (reqfor.Nombre == nombrefor)
                        //        {
                        //            listafor.Add(reqfor);
                        //        }
                        //        nombrefor = reqfor.Nombre; 
                        //    }

                        //    if (listafor.ToString() != "")
                        //    {
                        //        foreach (RequisitoFormulario listarfor in listafor)
                        //        {
                        //            req.RequisitoFormulario.Remove(listarfor);
                        //        }
                        //    }
                        //}

                    }
                    if (lista.ToString() != "")
                    {
                        foreach (Requisito listar in lista)
                        {
                            model.Requisito.Remove(listar);
                        }
                    }

                }





                if (model.DenominacionAnterior != model.Denominacion)
                {
                    model.FlagCopiarProcedimiento = 2;
                }
                //else {
                //    model.FlagCopiarProcedimiento = 1;
                //}
                //FVN Fin


                if (model.ProcedimientoSede == null) model.ProcedimientoSede = new List<ProcedimientoSede>();
                else model.ProcedimientoSede.ForEach(x => x.ProcedimientoId = model.ProcedimientoId);

                if (model.NotaCiudadano == null) model.NotaCiudadano = new List<NotaCiudadano>();
                else model.NotaCiudadano.ForEach(x => x.ProcedimientoId = model.ProcedimientoId);

                if (model.PasoSeguir == null) model.PasoSeguir = new List<PasoSeguir>();
                else model.PasoSeguir.ForEach(x => x.ProcedimientoId = model.ProcedimientoId);

                int conteo = 0;
                if (model.Requisito == null) model.Requisito = new List<Requisito>();
                else
                {
                    foreach (Requisito req in model.Requisito)
                    {


                        req.ProcedimientoId = model.ProcedimientoId;

                        if (model.Requisito[conteo].UserCreacion == "ACR" || model.Requisito[conteo].UserCreacion == "ACRExante")
                        {
                            req.Nombre = model.Requisito[conteo].Nombre + "" + model.Requisito[conteo].Descripcion;
                            req.UserModificacion = user.NroDocumento;
                            req.FecModificacion = DateTime.Now;
                        }
                        else
                        {
                            req.UserModificacion = user.NroDocumento;
                            req.FecModificacion = DateTime.Now;
                        }

                        if (req.RequisitoFormulario == null) req.RequisitoFormulario = new List<RequisitoFormulario>();
                        else
                        {
                            for (int i = 0; i < req.RequisitoFormulario.Count; i++)
                            {
                                req.RequisitoFormulario[i].FormularioId = i + 1;
                                req.RequisitoFormulario[i].RequisitoId = req.RequisitoId;
                                req.RequisitoFormulario[i].ArchivoAdjunto = null;
                                if (req.RequisitoFormulario[i].ArchivoAdjuntoId == 0) req.RequisitoFormulario[i].ArchivoAdjuntoId = null;
                            }
                        }
                        conteo = conteo + 1;

                    }
                }

                int reccontu = 0;
                int reccontu2 = 0;

                foreach (Requisito req in model.Requisito)
                {
                    if (req.TipoRequisito == TipoRequisito.General)
                    {
                        reccontu = reccontu + 1;
                        model.Prequisitos = reccontu;
                    }
                    if (req.TipoRequisito == TipoRequisito.Especifico)
                    {
                        reccontu2 = reccontu2 + 1;
                        model.Prequisitos = reccontu2;
                    }



                }


                if (model.BaseLegal == null)
                {
                    model.BaseLegal = new BaseLegal();
                    model.BaseLegal.BaseLegalNorma = new List<BaseLegalNorma>();
                }
                else if (model.BaseLegal.BaseLegalNorma == null) model.BaseLegal.BaseLegalNorma = new List<BaseLegalNorma>();
                else model.BaseLegal.BaseLegalNorma.ForEach(x => x.BaseLegalId = model.BaseLegalId.Value);

                for (int i = 0; i < model.BaseLegal.BaseLegalNorma.Count(); i++)
                {
                    model.BaseLegal.BaseLegalNorma[i].BaseLegalNormaId = i + 1;
                    model.BaseLegal.BaseLegalNorma[i].Norma = null;
                    model.BaseLegal.BaseLegalNorma[i].ArchivoAdjunto = null;
                    if (model.BaseLegal.BaseLegalNorma[i].EstadoACR == "2")
                    {
                        model.BaseLegal.BaseLegalNorma[i].TipoNormaId = 141;
                    }
                    if (model.BaseLegal.BaseLegalNorma[i].NormaId == 0) model.BaseLegal.BaseLegalNorma[i].NormaId = null;
                    if (model.BaseLegal.BaseLegalNorma[i].ArchivoAdjuntoId == 0) model.BaseLegal.BaseLegalNorma[i].ArchivoAdjuntoId = null;
                }
                UIT objuit = _UITService.LsitaGetOne();
                model.TablaAsme.ForEach(x =>
                {
                    x.ProcedimientoId = model.ProcedimientoId;
                    x.UITID = objuit.UITID;
                    //_log.Info(x.EsGratuito.ToString());
                    model.Pprestacionesanuales = model.TablaAsme[0].Prestaciones;
                });


                //model.ProcedimientoDatoAdicional.RemoveAll(x => !x.Checked);
                if (model.TipoProcedimiento == TipoProcedimiento.Estandar)
                {
                    if (user.Rol == 1)
                    {
                        model.PlazoAtencionEstandar = model.PlazoAtencion;
                    }
                    else { model.PlazoAtencionEstandar = model.PlazoAtencionEstandar; }
                }
                model.Expediente = null;
                model.UndOrgResponsable = null;
                model.UndOrgReconsideracion = null;
                model.UndOrgApelacion = null;
                model.UndOrgPresentDocumentacion = null;
                model.TipoAtencion = null;
                model.Editado = true;


                _procedimientoService.Save(model);
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
        public ActionResult DatosGeneralesInformacionCiudadano(Procedimiento model)
        {
            try
            {
                Procedimiento obj = _procedimientoService.GetOne(model.ProcedimientoId);
                obj.ProcedimientoSede = new List<ProcedimientoSede>();
                obj.NotaCiudadano = new List<NotaCiudadano>();
                obj.PasoSeguir = new List<PasoSeguir>();
                obj.ProcedimientoDatoAdicional = new List<ProcedimientoDatoAdicional>();

                // bloquera ninguno
                obj.EstadoNinguno = 0;

                if (model.ProcedimientoSede == null) model.ProcedimientoSede = new List<ProcedimientoSede>();
                if (model.ProcedimientoSede.Count > 0)
                {
                    model.ProcedimientoSede.ForEach(x =>
                    {
                        x.ProcedimientoId = model.ProcedimientoId;
                        if (x.UndOrgRecepcionDocumentos != null)
                            x.UndOrgRecepcionDocumentos.ForEach(p =>
                            {
                                p.ProcedimientoId = model.ProcedimientoId;
                                p.SedeId = x.SedeId;
                            });
                    });
                }

                obj.ProcedimientoSede = model.ProcedimientoSede;
                obj.ProcedimientoSede.ForEach(x => { if (x.UndOrgRecepcionDocumentos == null) x.UndOrgRecepcionDocumentos = new List<UndOrgRecepcionDocumentos>(); });




                if (model.NotaCiudadano == null) model.NotaCiudadano = new List<NotaCiudadano>();
                model.NotaCiudadano.ForEach(x => x.ProcedimientoId = model.ProcedimientoId);
                obj.NotaCiudadano = model.NotaCiudadano;

                if (model.PasoSeguir == null) model.PasoSeguir = new List<PasoSeguir>();
                model.PasoSeguir.ForEach(x => x.ProcedimientoId = obj.ProcedimientoId);
                obj.PasoSeguir = model.PasoSeguir;

                obj.Correo = model.Correo;
                obj.Telefono = model.Telefono;
                obj.Anexo = model.Anexo;



                //model.ProcedimientoDatoAdicional.RemoveAll(x => !x.Checked);
                obj.ProcedimientoDatoAdicional = model.ProcedimientoDatoAdicional;

                obj.Editado = true;

                obj.Es_Seccion = true;

                _procedimientoService.SaveInformacionCiudadano(obj);
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
        public ActionResult DatosGeneralesTablaAsme(Procedimiento model)
        {
            try
            {
                if (model.ProcedimientoSede == null) model.ProcedimientoSede = new List<ProcedimientoSede>();
                else model.ProcedimientoSede.ForEach(x => x.ProcedimientoId = model.ProcedimientoId);

                if (model.NotaCiudadano == null) model.NotaCiudadano = new List<NotaCiudadano>();
                else model.NotaCiudadano.ForEach(x => x.ProcedimientoId = model.ProcedimientoId);

                if (model.PasoSeguir == null) model.PasoSeguir = new List<PasoSeguir>();
                else model.PasoSeguir.ForEach(x => x.ProcedimientoId = model.ProcedimientoId);

                if (model.Requisito == null) model.Requisito = new List<Requisito>();
                else
                {
                    foreach (Requisito req in model.Requisito)
                    {
                        req.ProcedimientoId = model.ProcedimientoId;
                        if (req.RequisitoFormulario == null) req.RequisitoFormulario = new List<RequisitoFormulario>();
                        else
                        {
                            for (int i = 0; i < req.RequisitoFormulario.Count; i++)
                            {
                                req.RequisitoFormulario[i].FormularioId = i + 1;

                                req.RequisitoFormulario[i].ArchivoAdjunto = null;
                                if (req.RequisitoFormulario[i].ArchivoAdjuntoId == 0) req.RequisitoFormulario[i].ArchivoAdjuntoId = null;
                            }
                        }
                    }
                }

                if (model.BaseLegal == null)
                {
                    model.BaseLegal = new BaseLegal();
                    model.BaseLegal.BaseLegalNorma = new List<BaseLegalNorma>();
                }
                else if (model.BaseLegal.BaseLegalNorma == null) model.BaseLegal.BaseLegalNorma = new List<BaseLegalNorma>();
                else model.BaseLegal.BaseLegalNorma.ForEach(x => x.BaseLegalId = model.BaseLegalId.Value);

                for (int i = 0; i < model.BaseLegal.BaseLegalNorma.Count(); i++)
                {
                    model.BaseLegal.BaseLegalNorma[i].BaseLegalNormaId = i + 1;
                    model.BaseLegal.BaseLegalNorma[i].Norma = null;
                    model.BaseLegal.BaseLegalNorma[i].ArchivoAdjunto = null;
                    if (model.BaseLegal.BaseLegalNorma[i].NormaId == 0) model.BaseLegal.BaseLegalNorma[i].NormaId = null;
                    if (model.BaseLegal.BaseLegalNorma[i].ArchivoAdjuntoId == 0) model.BaseLegal.BaseLegalNorma[i].ArchivoAdjuntoId = null;
                }


                UIT objuit = _UITService.LsitaGetOne();
                model.TablaAsme.ForEach(x =>
                {
                    x.ProcedimientoId = model.ProcedimientoId;
                    x.UITID = objuit.UITID;
                    //_log.Info(x.EsGratuito.ToString());
                });


                //model.ProcedimientoDatoAdicional.RemoveAll(x => !x.Checked);

                model.Expediente = null;
                model.UndOrgResponsable = null;
                model.UndOrgReconsideracion = null;
                model.UndOrgApelacion = null;
                model.UndOrgPresentDocumentacion = null;
                model.TipoAtencion = null;
                model.Editado = true;

                _procedimientoService.Save(model);
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
        public ActionResult DatosGeneralesInfoBasica(Procedimiento model)
        {
            try
            {
                Procedimiento obj = _procedimientoService.GetOne(model.ProcedimientoId);

                if (obj.ProcedimientoSede == null) obj.ProcedimientoSede = new List<ProcedimientoSede>();
                if (obj.NotaCiudadano == null) obj.NotaCiudadano = new List<NotaCiudadano>();
                if (obj.ProcedimientoDatoAdicional == null) obj.ProcedimientoDatoAdicional = new List<ProcedimientoDatoAdicional>();
                if (obj.PasoSeguir == null) obj.PasoSeguir = new List<PasoSeguir>();


                model.ProcedimientoSede = new List<ProcedimientoSede>();
                model.ProcedimientoSede = obj.ProcedimientoSede;
                model.NotaCiudadano = new List<NotaCiudadano>();
                model.NotaCiudadano = obj.NotaCiudadano;
                model.ProcedimientoDatoAdicional = new List<ProcedimientoDatoAdicional>();
                model.ProcedimientoDatoAdicional = obj.ProcedimientoDatoAdicional;
                model.PasoSeguir = new List<PasoSeguir>();
                model.PasoSeguir = obj.PasoSeguir;

                if (model.Requisito == null) model.Requisito = new List<Requisito>();
                else
                {
                    foreach (Requisito req in model.Requisito)
                    {
                        req.ProcedimientoId = model.ProcedimientoId;
                        if (req.RequisitoFormulario == null) req.RequisitoFormulario = new List<RequisitoFormulario>();
                        else
                        {
                            for (int i = 0; i < req.RequisitoFormulario.Count; i++)
                            {
                                req.RequisitoFormulario[i].FormularioId = i + 1;

                                req.RequisitoFormulario[i].ArchivoAdjunto = null;
                                if (req.RequisitoFormulario[i].ArchivoAdjuntoId == 0) req.RequisitoFormulario[i].ArchivoAdjuntoId = null;
                            }
                        }
                    }
                }

                if (model.BaseLegal == null)
                {
                    model.BaseLegal = new BaseLegal();
                    model.BaseLegal.BaseLegalNorma = new List<BaseLegalNorma>();
                }
                else if (model.BaseLegal.BaseLegalNorma == null) model.BaseLegal.BaseLegalNorma = new List<BaseLegalNorma>();
                else model.BaseLegal.BaseLegalNorma.ForEach(x => x.BaseLegalId = model.BaseLegalId.Value);

                for (int i = 0; i < model.BaseLegal.BaseLegalNorma.Count(); i++)
                {
                    model.BaseLegal.BaseLegalNorma[i].BaseLegalNormaId = i + 1;
                    model.BaseLegal.BaseLegalNorma[i].Norma = null;
                    model.BaseLegal.BaseLegalNorma[i].ArchivoAdjunto = null;
                    if (model.BaseLegal.BaseLegalNorma[i].NormaId == 0) model.BaseLegal.BaseLegalNorma[i].NormaId = null;
                    if (model.BaseLegal.BaseLegalNorma[i].ArchivoAdjuntoId == 0) model.BaseLegal.BaseLegalNorma[i].ArchivoAdjuntoId = null;
                }


                UIT objuit = _UITService.LsitaGetOne();
                model.TablaAsme.ForEach(x =>
                {
                    x.ProcedimientoId = model.ProcedimientoId;
                    x.UITID = objuit.UITID;
                    //_log.Info(x.EsGratuito.ToString());
                });

                model.Editado = true;

                _procedimientoService.SaveInformacionBasica(model);
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


        public ActionResult EditarFormulario(RequisitoFormulario model, short indexReq, short indexForm, short nuevo, string fnAdd, int TipoRequisito, int RequisitoId, int FormularioId)
        {
            try
            {
                ViewBag.indexReq = indexReq;
                ViewBag.indexForm = indexForm;
                ViewBag.FnAdd = fnAdd;
                ViewBag.Nuevo = nuevo;
                ViewBag.TipoRequisito = TipoRequisito;

                ViewBag.RequisitoId = RequisitoId;
                ViewBag.FormularioId = FormularioId;

                ViewBag.Tipo = model.ArchivoAdjuntoId != null || nuevo == 1 ? 1 : 2;




                if (model.ArchivoAdjuntoId != null)
                {
                    model.ArchivoAdjunto = _archivoAdjuntoService.GetOne(model.ArchivoAdjuntoId.Value);
                    model.ArchivoAdjunto.Ruta = "/General/Adjunto/Descargar/" + model.ArchivoAdjuntoId;

                }
                if (nuevo != 1)
                {
                    RequisitoFormulario forreq = new RequisitoFormulario();
                    model = _requisitoService.GetOneForm(RequisitoId, FormularioId);
                    if (model.ArchivoAdjuntoId != null)
                    {
                        model.ArchivoAdjunto = _archivoAdjuntoService.GetOne(model.ArchivoAdjuntoId.Value);
                        model.ArchivoAdjunto.Ruta = "/General/Adjunto/Descargar/" + model.ArchivoAdjuntoId;

                    }
                    ViewBag.cantiadreq = model.FormularioId;
                }
                else
                {
                    ViewBag.cantiadreq = _requisitoService.listProcedimiento(RequisitoId).Count() + 1;
                }


                return PartialView("_editFormulario", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _log.Error(ex);
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        public ActionResult EditarTablaASMEReproduccion(short indexForm, short nuevo, int indexTablaAsmeId, long indexProcedimientoId)
        {
            try
            {
                ViewBag.indexForm = indexForm;
                ViewBag.Nuevo = nuevo;
                ViewBag.indexTablaAsmeId = indexTablaAsmeId;
                ViewBag.indexProcedimientoId = indexProcedimientoId;

                TablaAsmeReproduccion model = new TablaAsmeReproduccion();
                if (indexForm != 0)
                {
                    model = _tablaAsmeReproduccionService.GetOne(indexForm);
                }
                else
                {

                    model.TablaAsmeId = indexTablaAsmeId;
                    model.ReproduccionId = indexForm;
                }



                return PartialView("_editTablaAsmeReproduccion", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _log.Error(ex);
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        public ActionResult EditarPlazoAtencion(int pantalla, int indexTablaAsmeId, long indexProcedimientoId)
        {
            try
            {
                //ViewBag.indexForm = indexForm;
                //ViewBag.Nuevo = nuevo;
                ViewBag.indexTablaAsmeId = indexTablaAsmeId;
                ViewBag.indexProcedimientoId = indexProcedimientoId;
                ViewBag.pantalla = pantalla;

                PlazoAtencion model = new PlazoAtencion();
                if (indexTablaAsmeId != 0)
                {
                    model = _plazoAtencionService.GetOne(indexTablaAsmeId);
                }
                else
                {

                    model.ProcedimientoId = indexProcedimientoId;
                    model.PlazoAtencionId = indexTablaAsmeId;
                }

                if (model.TipoPlazo == TipoPlazo.habiles)
                {

                    ViewBag.tipoplazo = new List<SelectListItem>()
                        {
                            new SelectListItem() { Text = "Días Hábiles", Value = "154" , Selected = model.TipoPlazo == TipoPlazo.habiles ? true:false},
                            new SelectListItem() { Text = "Días Calendarios", Value = "155", Selected = model.TipoPlazo == TipoPlazo.calendarios ? true:false},
                            new SelectListItem() { Text = "Meses", Value = "156", Selected = model.TipoPlazo == TipoPlazo.meses ? true:false},
                        };
                }
                else if (model.TipoPlazo == TipoPlazo.calendarios)
                {
                    ViewBag.tipoplazo = new List<SelectListItem>()
                        {
                          new SelectListItem() { Text = "Días Calendarios", Value = "155", Selected = model.TipoPlazo == TipoPlazo.calendarios ? true:false},
                          new SelectListItem() { Text = "Días Hábiles", Value = "154" , Selected = model.TipoPlazo == TipoPlazo.habiles ? true:false},
                          new SelectListItem() { Text = "Meses", Value = "156", Selected = model.TipoPlazo == TipoPlazo.meses ? true:false},

                        };
                }
                else if (model.TipoPlazo == TipoPlazo.meses)
                {
                    ViewBag.tipoplazo = new List<SelectListItem>()
                        {
                          new SelectListItem() { Text = "Meses", Value = "156", Selected = model.TipoPlazo == TipoPlazo.meses ? true:false},
                          new SelectListItem() { Text = "Días Hábiles", Value = "154" , Selected = model.TipoPlazo == TipoPlazo.habiles ? true:false},
                          new SelectListItem() { Text = "Días Calendarios", Value = "155", Selected = model.TipoPlazo == TipoPlazo.calendarios ? true:false},


                        };
                }
                else
                {
                    ViewBag.tipoplazo = new List<SelectListItem>()
                        {
                            new SelectListItem() { Text = "Días Hábiles", Value = "154" , Selected = model.TipoPlazo == TipoPlazo.habiles ? true:false},
                            new SelectListItem() { Text = "Días Calendarios", Value = "155", Selected = model.TipoPlazo == TipoPlazo.calendarios ? true:false},
                            new SelectListItem() { Text = "Meses", Value = "156", Selected = model.TipoPlazo == TipoPlazo.meses ? true:false},
                        };
                }


                return PartialView("_editPlazoAtencion", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _log.Error(ex);
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }



        [HttpPost]
        public ActionResult EliminarTablaASMEReproduccion(int ReproduccionId, UsuarioInfo user)
        {

            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Eliminar  Reproduccion";
                objauditoria.Pantalla = "Reproduccion";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                _tablaAsmeReproduccionService.Eliminar(ReproduccionId);
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
        public ActionResult EliminarApelacion(int ProcedimientoCargosApeID, UsuarioInfo user)
        {

            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Eliminar  Apelación";
                objauditoria.Pantalla = "Apelación";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                _ProcedimientoCargosApeService.Eliminar(ProcedimientoCargosApeID, 0);
                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
                return Json(new
                {
                    mensaje = "La Apelación fue eliminada satisfactoriamente."
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
        public ActionResult EliminarOtros(int ProcedimientoCargosOtrosID, UsuarioInfo user)
        {

            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Eliminar  Otros";
                objauditoria.Pantalla = "Apelación";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                _ProcedimientoCargosOtrosService.Eliminar(ProcedimientoCargosOtrosID);
                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
                return Json(new
                {
                    mensaje = "La Apelación fue eliminada satisfactoriamente."
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
        public ActionResult EliminarReconsideracion(int ProcedimientoCargosID, int ProcedimientoID, int orden, UsuarioInfo user)
        {

            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Eliminar  Reconsideración";
                objauditoria.Pantalla = "Reconsideración";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                _procedimientoCargosService.Eliminar(ProcedimientoCargosID);
                //_ProcedimientoCargosApeService.Eliminar(ProcedimientoID, orden);
                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
                return Json(new
                {
                    mensaje = "La Reconsideración fue eliminada satisfactoriamente."
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
        public ActionResult EliminarReconsideracionProc(int ProcedimientoUndOrgResponsableID, int ProcedimientoID, int orden, UsuarioInfo user)
        {

            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Eliminar  Reconsideración";
                objauditoria.Pantalla = "Reconsideración";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                _ProcedimientoUndOrgResponsableService.Eliminar(ProcedimientoUndOrgResponsableID);
                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
                return Json(new
                {
                    mensaje = "La Reconsideración fue eliminada satisfactoriamente."
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
        public ActionResult EliminarPlazoAtencion(int PlazoAtencionId, UsuarioInfo user)
        {

            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Eliminar Plazo Atención";
                objauditoria.Pantalla = "Plazo Atención";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                _plazoAtencionService.Eliminar(PlazoAtencionId);

                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
                return Json(new
                {
                    mensaje = "El Plazo Atención fue eliminada satisfactoriamente."
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
        public ActionResult EliminarTablaASME(int TablaAsmeId, UsuarioInfo user)
        {

            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Eliminar TablaAsme";
                objauditoria.Pantalla = "Modalidad";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/


                /*TablaAsmeReproduccion*/
                List<TablaAsmeReproduccion> repro = _tablaAsmeReproduccionService.GetAll(TablaAsmeId);

                foreach (TablaAsmeReproduccion tablrep in repro)
                {
                    _tablaAsmeReproduccionService.Eliminar(tablrep.ReproduccionId);

                }
                /*AnexoIdentificable*/
                List<AnexoIdentificable> reproAnexoIdentificable = _tablaAsmeReproduccionService.GetAllAnexoIdentificable(TablaAsmeId);

                foreach (AnexoIdentificable tablrep2 in reproAnexoIdentificable)
                {
                    _tablaAsmeReproduccionService.EliminarAnexoIdentificable(tablrep2.AnexoIdentificableId);

                }
                /*AnexoNoIdentificable*/
                List<AnexoNoIdentificable> reproAnexoNoIdentificable = _tablaAsmeReproduccionService.GetAllAnexoNoIdentificable(TablaAsmeId);

                foreach (AnexoNoIdentificable tablrep2 in reproAnexoNoIdentificable)
                {
                    _tablaAsmeReproduccionService.EliminarAnexoNoIdentificable(tablrep2.AnexoNoIdentificableId);

                }

                /*AnexoPersonal*/
                List<AnexoPersonal> reproAnexoPersonal = _tablaAsmeReproduccionService.GetAllAnexoPersonal(TablaAsmeId);

                foreach (AnexoPersonal tablrep2 in reproAnexoPersonal)
                {
                    _tablaAsmeReproduccionService.EliminarAnexoPersonal(tablrep2.AnexoPersonalId);

                }

                /*Actividad*/
                List<Actividad> reproActividad = _tablaAsmeReproduccionService.GetAllActividad(TablaAsmeId);

                foreach (Actividad tablrep2 in reproActividad)
                {
                    _tablaAsmeReproduccionService.EliminarActividad(tablrep2.ActividadId);

                }


                TablaAsme tablaasme = _tablaAsmeService.GetOne(TablaAsmeId);


                _tablaAsmeService.delete(tablaasme);




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
        public ActionResult GuardarReproduccion(TablaAsmeReproduccion model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.ReproduccionId == 0 ? "El Reproduccion fue registrado satisfactoriamente."
                                                : "El Reproduccion fue modificado satisfactoriamente.";

                    TablaAsmeReproduccion obj;

                    if (model.ReproduccionId == 0)
                    {

                        obj = new TablaAsmeReproduccion();
                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;

                        obj.Descripcion = model.Descripcion;
                        obj.Costo = model.Costo;
                        obj.Sustento = model.Sustento;
                        obj.TablaAsmeId = model.TablaAsmeId;


                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Reproduccion";
                        objauditoria.Pantalla = "Reproduccion";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;


                        obj.UserModificacion = user.NroDocumento;
                        obj.FecModificacion = DateTime.Now;
                        _tablaAsmeReproduccionService.Save(obj);
                        /*auditoria Grabar*/
                        _AuditoriaService.Save(objauditoria);
                        /*auditoria Grabar*/


                        /*auditoria*/
                    }
                    else
                    {
                        obj = _tablaAsmeReproduccionService.GetOne(model.ReproduccionId);
                        obj.Descripcion = model.Descripcion;
                        obj.Costo = model.Costo;
                        obj.Sustento = model.Sustento;

                        obj.UserModificacion = user.NroDocumento;
                        obj.FecModificacion = DateTime.Now;


                        _tablaAsmeReproduccionService.Save(obj);

                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Reproduccion";
                        objauditoria.Pantalla = "Reproduccion";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/

                        /*auditoria Grabar*/
                        _AuditoriaService.Save(objauditoria);
                        /*auditoria Grabar*/

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
                ModelState.AddModelError("editarRecurso", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }



        [HttpPost]
        public ActionResult GuardarPlazoAtencio(PlazoAtencion model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.PlazoAtencionId == 0 ? "El Plazo de Atención fue registrado satisfactoriamente."
                                                : "El Plazo de Atención fue modificado satisfactoriamente.";

                    PlazoAtencion obj;

                    if (model.PlazoAtencionId == 0)
                    {

                        obj = new PlazoAtencion();
                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;

                        obj.Descripcion = model.Descripcion;
                        obj.Plazo = model.Plazo;
                        obj.TipoPlazo = model.TipoPlazo;
                        //obj.Sustento = model.Sustento;
                        obj.ProcedimientoId = model.ProcedimientoId;


                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Plazo de Atención";
                        objauditoria.Pantalla = "Plazo de Atención";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;


                        obj.UserModificacion = user.NroDocumento;
                        obj.FecModificacion = DateTime.Now;
                        _plazoAtencionService.Save(obj);
                        /*auditoria Grabar*/
                        _AuditoriaService.Save(objauditoria);
                        /*auditoria Grabar*/


                        /*auditoria*/
                    }
                    else
                    {
                        obj = _plazoAtencionService.GetOne(model.PlazoAtencionId);
                        obj.Descripcion = model.Descripcion;
                        obj.Plazo = model.Plazo;
                        obj.TipoPlazo = model.TipoPlazo;

                        obj.UserModificacion = user.NroDocumento;
                        obj.FecModificacion = DateTime.Now;


                        _plazoAtencionService.Save(obj);

                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Plazo de Atención";
                        objauditoria.Pantalla = "Plazo de Atención";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/

                        /*auditoria Grabar*/
                        _AuditoriaService.Save(objauditoria);
                        /*auditoria Grabar*/

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
                ModelState.AddModelError("editarRecurso", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }

        [HttpPost]
        public ActionResult GuardarPlazoAtencioSustento(PlazoAtencion model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = "El Plazo de Atención fue modificado satisfactoriamente.";

                    PlazoAtencion obj;

                    obj = _plazoAtencionService.GetOne(model.PlazoAtencionId);
                    obj.Sustento = model.Sustento;

                    obj.UserModificacion = user.NroDocumento;
                    obj.FecModificacion = DateTime.Now;

                    _plazoAtencionService.Save(obj);

                    /*auditoria actualizar*/
                    objauditoria.EntidadId = user.EntidadId;
                    objauditoria.SectorId = user.Sector;
                    objauditoria.ProvinciaId = user.Provincia;
                    objauditoria.Usuario = user.NombreCompleto;
                    objauditoria.Actividad = "Modificar Sustento";
                    objauditoria.Pantalla = "Plazo de Atención";
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
                ModelState.AddModelError("editarRecurso", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }






        [HttpPost]
        public ActionResult TerminarSustento(TipoSustento tipo, long ProcedimientoId, EstadoExpediente estadoExpediente, long ExpedienteId, UsuarioInfo user)
        {
            string mensaje = "";
            try
            {
                string Actividades = "";
                string Pantalla = "";


                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                if (tipo == TipoSustento.DatosGenerales)
                {
                    Actividades = "Activar Datos Generales";
                    Pantalla = "Datos Generales";
                }
                else if (tipo == TipoSustento.InfCdno)
                {
                    Actividades = "Activar Inf. Cdno.";
                    Pantalla = "Inf. Cdno.";
                }
                else if (tipo == TipoSustento.SustentoCostos)
                {
                    Actividades = "Activar Sustento Costos";
                    Pantalla = "Sustento Costos";
                }
                else if (tipo == TipoSustento.SustentoTecnico)
                {
                    Actividades = "Activar Sustento Tecnico";
                    Pantalla = "Sustento Tecnico";
                }
                else if (tipo == TipoSustento.TablaAsme)
                {
                    Actividades = "Activar Tabla Asme";
                    Pantalla = "Tabla Asme";
                }
                objauditoria.Actividad = Actividades;
                objauditoria.Pantalla = Pantalla;
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/

                if (tipo == TipoSustento.TablaAsme)
                {
                    List<string> validaciones = _procedimientoService.ValidarSustTecnico(ProcedimientoId);

                    if (validaciones.Count() > 0)
                    {
                        return Json(new
                        {
                            valid = false,
                            mensaje = validaciones
                        });
                    }
                }

                if (estadoExpediente == EstadoExpediente.Observado)
                {
                    _AuditoriaService.CambiarEstadoSustento(tipo, ProcedimientoId, 1, ExpedienteId);
                }
                else
                {
                    //_procedimientoService.CambiarEstadoSustento(tipo, ProcedimientoId, true);

                    _AuditoriaService.CambiarEstadoProceso(tipo, ProcedimientoId, 1, ExpedienteId);
                }


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
                mensaje = ex.Message;
                ModelState.AddModelError("", ex.Message);
                _log.Error(ex);
                return PartialView("_Error");
            }
        }

        [HttpPost]
        public ActionResult ActivarSustento(TipoSustento tipo, long ProcedimientoId, EstadoExpediente estadoExpediente, long ExpedienteId, UsuarioInfo user)
        {
            try
            {
                string Actividades = "";
                string Pantalla = "";
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                if (tipo == TipoSustento.DatosGenerales)
                {
                    Actividades = "Activar Datos Generales";
                    Pantalla = "Datos Generales";
                }
                else if (tipo == TipoSustento.InfCdno)
                {
                    Actividades = "Activar Inf. Cdno.";
                    Pantalla = "Inf. Cdno.";
                }
                else if (tipo == TipoSustento.SustentoCostos)
                {
                    Actividades = "Activar Sustento Costos";
                    Pantalla = "Sustento Costos";
                }
                else if (tipo == TipoSustento.SustentoTecnico)
                {
                    Actividades = "Activar Sustento Tecnico";
                    Pantalla = "Sustento Tecnico";
                }
                else if (tipo == TipoSustento.TablaAsme)
                {
                    Actividades = "Activar Tabla Asme";
                    Pantalla = "Tabla Asme";
                }

                objauditoria.Actividad = Actividades;
                objauditoria.Pantalla = Pantalla;
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/

                var proce_estado = _procedimientoService.GetOne(ProcedimientoId);
                if (tipo == TipoSustento.TablaAsme)
                {
                    if (proce_estado.TablaAsmeTerminado != true)
                    {
                        List<string> validaciones = _procedimientoService.ValidarSustTecnico(ProcedimientoId);

                        if (validaciones.Count() > 0)
                        {
                            return Json(new
                            {
                                valid = false,
                                mensaje = validaciones
                            });
                        }

                    }
                }


                if (estadoExpediente == EstadoExpediente.Observado)
                {
                    _AuditoriaService.CambiarEstadoSustento(tipo, ProcedimientoId, 0, ExpedienteId);
                }
                else
                {
                    //_procedimientoService.CambiarEstadoSustento(tipo, ProcedimientoId, false);

                    _AuditoriaService.CambiarEstadoProceso(tipo, ProcedimientoId, 0, ExpedienteId);
                }


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
        public ActionResult ActivarSolicitudWord(long expedienteId, UsuarioInfo user)
        {
            try
            {
                string Actividades = "Solicitar Formato Word";
                string Pantalla = "Expediente";
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = Actividades;
                objauditoria.Pantalla = Pantalla;
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/


                _expedienteService.ActivarSolicitudWord(expedienteId);


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
        public ActionResult SustentoTecnico(long id, UsuarioInfo user)
        {
            try
            {

                /*Inicio Cargar roles - Acceso*/
                List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 23);
                user.rolmenu = listarolmenu.ToList();
                /*Fin Cargar roles  - Acceso*/
                ViewBag.Usuario = user;
                Procedimiento model = _procedimientoService.GetOne(id);

                //model.Posicion = 1;
                //_AuditoriaService.AlimpiarPosicion(model.ExpedienteId);
                //_procedimientoService.GuardarPosicion(model);
                //_AuditoriaService.AlimpiarPosicion(model.ExpedienteId, model.ProcedimientoId);

                List<PlazoAtencion> lstplazo = _plazoAtencionService.GetAll(id).Where(x => x.Plazo > 30).ToList();

                model.listplazoatencion = lstplazo;


                ViewBag.tipoplazo = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Días Hábiles", Value = "154" , Selected = model.TipoPlazo == TipoPlazo.habiles ? true:false},
                    new SelectListItem() { Text = "Días Calendarios", Value = "155", Selected = model.TipoPlazo == TipoPlazo.calendarios ? true:false},
                    new SelectListItem() { Text = "Meses", Value = "156", Selected = model.TipoPlazo == TipoPlazo.meses ? true:false},
                };


                Expediente exp = _expedienteService.GetOne(model.Expediente.ExpedienteId);
                /*listar expediente observacion*/
                ViewBag.Expediente = exp;

                model.Requisito = model.Requisito.Where(z => z.Eliminado != 3).OrderBy(x => x.RecNum).OrderBy(y => y.TipoRequisito)
                                                   .ToList();
                Observacion obs = new Observacion();
                obs.ExpedienteId = exp.ExpedienteId;
                obs.ProcedimientoId = id;
                obs.CodValidacion = "1";
                obs.Estado = "1";
                obs.EntidadId = user.EntidadId;
                obs.NombreCampo = "Nota de Ayuda Sustento Tecnico";
                ViewBag.Observacion = _observacionService.GetOneGlobal(obs);
                /*listar expediente observacion*/

                if (model.Codigo == "S/C" && !string.IsNullOrEmpty(model.CodigoACR)) return RedirectToAction("NuevoCodigo", "Procedimiento", new { area = "Simplificacion", id = id });

                switch (model.Calificacion)
                {
                    case CalificacionProcedimiento.Automatica: ViewBag.Calificacion = "Aprobación Automática"; break;
                    case CalificacionProcedimiento.SilencioPositivo: ViewBag.Calificacion = "Evaluación Previa con Silencio Positivo"; break;
                    case CalificacionProcedimiento.SilencioNegativo: ViewBag.Calificacion = "Evaluación Previa con Silencio Negativo"; break;
                }


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
                model.CargoPordependencia = nombrecargo;

                List<object> lista = new List<object>();
                List<object> lstvalidar = new List<object>();
                for (int i = 0; i < model.Requisito.Count(); i++)
                {
                    List<BaseLegalNorma> blList = _baseLegalService.GetDetails(model.Requisito[i].BaseLegalId.Value);
                    if ((int)model.Requisito[i].TipoRequisito == 1)
                    {
                        lstvalidar.Add(blList.Select(x => new
                        {
                            BaseLegalId = x.BaseLegalId,
                            BaseLegalNormaId = x.BaseLegalNormaId,
                            TipoBaseLegal = x.TipoBaseLegal,
                            NormaId = x.Norma != null ? x.NormaId.Value : 0,
                            Descripcion = x.Norma != null ? x.Norma.Descripcion : x.Descripcion,
                            TipoNormaId = x.Norma != null ? x.Norma.TipoNorma.MetaDatoId : x.TipoNorma.MetaDatoId,
                            NormaTipo = x.Norma != null ? x.Norma.TipoNorma.Nombre : x.TipoNorma.Nombre,
                            Numero = x.Norma != null ? x.Norma.Numero : x.Numero,
                            FechaPublicacion = x.Norma != null ? (x.Norma.FechaPublicacion == null ? "" : x.Norma.FechaPublicacion.Value.ToShortDateString()) : (x.FechaPublicacion == null ? "" : x.FechaPublicacion.Value.ToShortDateString()),
                            Sector = x.Norma != null ? x.Norma.Sector : x.Sector,
                            Articulo = x.Articulo,
                            Url = x.Url,
                            ArchivoAdjuntoId = x.TipoBaseLegal != TipoBaseLegal.RutaInternet ? (x.Norma != null ? 0 : x.ArchivoAdjuntoId) : 0
                        }).ToList()
                    );
                    }
                    lista.Add(blList.Select(x => new
                    {
                        BaseLegalId = x.BaseLegalId,
                        BaseLegalNormaId = x.BaseLegalNormaId,
                        TipoBaseLegal = x.TipoBaseLegal,
                        NormaId = x.Norma != null ? x.NormaId.Value : 0,
                        Descripcion = x.Norma != null ? x.Norma.Descripcion : x.Descripcion,
                        TipoNormaId = x.Norma != null ? x.Norma.TipoNorma.MetaDatoId : x.TipoNorma.MetaDatoId,
                        NormaTipo = x.Norma != null ? x.Norma.TipoNorma.Nombre : x.TipoNorma.Nombre,
                        Numero = x.Norma != null ? x.Norma.Numero : x.Numero,
                        FechaPublicacion = x.Norma != null ? (x.Norma.FechaPublicacion == null ? "" : x.Norma.FechaPublicacion.Value.ToString("dd/MM/yyyy")) : (x.FechaPublicacion == null ? "" : x.FechaPublicacion.Value.ToString("dd/MM/yyyy")),
                        Sector = x.Norma != null ? x.Norma.Sector : x.Sector,
                        Articulo = x.Articulo,
                        Url = x.Url,
                        ArchivoAdjuntoId = x.TipoBaseLegal != TipoBaseLegal.RutaInternet ? (x.Norma != null ? 0 : x.ArchivoAdjuntoId) : 0,
                        EstadoACR = x.EstadoACR,
                        DescripcionACR = x.DescripcionACR,
                    }).ToList()
                    );


                }

                ViewBag.ListaBaseLegal = JsonConvert.SerializeObject(lista);
                ViewBag.ListaBaseLegalvalidar = JsonConvert.SerializeObject(lstvalidar);
                ViewBag.User = user;


                Observacion obstecnico = new Observacion();
                obstecnico.ExpedienteId = exp.ExpedienteId;
                obstecnico.ProcedimientoId = id;
                obstecnico.Estado = "1";
                obstecnico.EntidadId = user.EntidadId;
                obstecnico.Pantalla = "Sustento Tecnico";
                ViewBag.Observaciontecnico = _observacionService.GetOneGlobalObservacion(obstecnico);

                string viewName = string.Empty;

                if (model.Operacion == OperacionExpediente.Ninguna || model.Operacion == OperacionExpediente.Eliminacion || user.Rol == (short)Rol.Administrador)
                {
                    if (user.TipoTupa != 2)
                    {
                        viewName = "VerSustentoTecnico";
                    }
                    else
                    {
                        if (exp.EstadoExpediente == EstadoExpediente.Aprobado)
                        {
                            viewName = "VerSustentoTecnico";
                        }
                        else
                        {

                            if (model.Operacion == OperacionExpediente.Ninguna || model.Operacion == OperacionExpediente.Eliminacion)
                            {
                                viewName = "VerSustentoTecnico";
                            }
                            else
                            {
                                viewName = "SustentoTecnico";
                            }

                        }

                    }

                }
                else
                {
                    if (model.SustTecnicoTerminado) viewName = "VerSustentoTecnico";
                    else
                    {
                        switch (user.Rol)
                        {
                            case (short)Rol.Administrador: viewName = model.TipoProcedimiento == TipoProcedimiento.Estandar ? "SustentoTecnico" : "VerSustentoTecnico"; break;
                            case (short)Rol.Coordinador: viewName = "SustentoTecnico"; break;
                            case (short)Rol.RegistradorProcesos: viewName = "SustentoTecnico"; break; //modificar
                            case (short)Rol.Ratificador: viewName = "VerSustentoTecnico"; break; //modificar
                            case (short)Rol.EntidadFiscalizadora: viewName = "VerSustentoTecnico"; break; //modificar
                            case (short)Rol.Evaluador: viewName = "VerSustentoTecnico"; break; //modificar
                        }
                    }
                }
                if (model.Posicion == 0)
                {
                    _AuditoriaService.AlimpiarPosicion(model.ExpedienteId, model.ProcedimientoId);
                }
                //_AuditoriaService.AlimpiarPosicion(model.ExpedienteId, model.ProcedimientoId);
                //_AuditoriaService.GenerarNumeracion(model.ExpedienteId);
                return View(viewName, model);
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ModelState.AddModelError("SustentoTecnico", ex.Message);
                _log.Error(ex);
                return View("_Error");
            }
        }


        public ActionResult VerObsSustentoTecnico(long id, long entidadid, UsuarioInfo user)
        {
            try
            {
                ViewBag.entidadver = entidadid;
                ViewBag.Usuario = user;
                Procedimiento model = _procedimientoService.GetOne(id);

                List<PlazoAtencion> lstplazo = _plazoAtencionService.GetAll(id).Where(x => x.Plazo > 30).ToList();

                model.listplazoatencion = lstplazo;


                ViewBag.tipoplazo = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Días Hábiles", Value = "154" , Selected = model.TipoPlazo == TipoPlazo.habiles ? true:false},
                    new SelectListItem() { Text = "Días Calendarios", Value = "155", Selected = model.TipoPlazo == TipoPlazo.calendarios ? true:false},
                    new SelectListItem() { Text = "Meses", Value = "156", Selected = model.TipoPlazo == TipoPlazo.meses ? true:false},
                };


                Expediente exp = _expedienteService.GetOne(model.Expediente.ExpedienteId);
                /*listar expediente observacion*/
                ViewBag.Expediente = exp;

                model.Requisito = model.Requisito.Where(z => z.Eliminado != 3).OrderBy(x => x.RecNum).OrderBy(y => y.TipoRequisito)
                                                   .ToList();
                Observacion obs = new Observacion();
                obs.ExpedienteId = exp.ExpedienteId;
                obs.ProcedimientoId = id;
                obs.Estado = "0";
                obs.CodValidacion = "5";
                obs.EntidadId = entidadid;
                //obs.Pantalla = "Sustento Tecnico";
                obs.NombreCampo = "Nota de Ayuda Sustento Tecnico";
                ViewBag.Observacion = _observacionService.GetOneGlobalobs(obs);
                /*listar expediente observacion*/




                if (model.Codigo == "S/C" && !string.IsNullOrEmpty(model.CodigoACR)) return RedirectToAction("NuevoCodigo", "Procedimiento", new { area = "Simplificacion", id = id });

                switch (model.Calificacion)
                {
                    case CalificacionProcedimiento.Automatica: ViewBag.Calificacion = "Aprobación Automática"; break;
                    case CalificacionProcedimiento.SilencioPositivo: ViewBag.Calificacion = "Evaluación Previa con Silencio Positivo"; break;
                    case CalificacionProcedimiento.SilencioNegativo: ViewBag.Calificacion = "Evaluación Previa con Silencio Negativo"; break;
                }

                List<object> lista = new List<object>();
                List<object> lstvalidar = new List<object>();
                for (int i = 0; i < model.Requisito.Count(); i++)
                {
                    List<BaseLegalNorma> blList = _baseLegalService.GetDetails(model.Requisito[i].BaseLegalId.Value);
                    if ((int)model.Requisito[i].TipoRequisito == 1)
                    {
                        lstvalidar.Add(blList.Select(x => new
                        {
                            BaseLegalId = x.BaseLegalId,
                            BaseLegalNormaId = x.BaseLegalNormaId,
                            TipoBaseLegal = x.TipoBaseLegal,
                            NormaId = x.Norma != null ? x.NormaId.Value : 0,
                            Descripcion = x.Norma != null ? x.Norma.Descripcion : x.Descripcion,
                            TipoNormaId = x.Norma != null ? x.Norma.TipoNorma.MetaDatoId : x.TipoNorma.MetaDatoId,
                            NormaTipo = x.Norma != null ? x.Norma.TipoNorma.Nombre : x.TipoNorma.Nombre,
                            Numero = x.Norma != null ? x.Norma.Numero : x.Numero,
                            FechaPublicacion = x.Norma != null ? (x.Norma.FechaPublicacion == null ? "" : x.Norma.FechaPublicacion.Value.ToShortDateString()) : (x.FechaPublicacion == null ? "" : x.FechaPublicacion.Value.ToShortDateString()),
                            Sector = x.Norma != null ? x.Norma.Sector : x.Sector,
                            Articulo = x.Articulo,
                            Url = x.Url,
                            ArchivoAdjuntoId = x.TipoBaseLegal != TipoBaseLegal.RutaInternet ? (x.Norma != null ? 0 : x.ArchivoAdjuntoId) : 0
                        }).ToList()
                    );
                    }
                    lista.Add(blList.Select(x => new
                    {
                        BaseLegalId = x.BaseLegalId,
                        BaseLegalNormaId = x.BaseLegalNormaId,
                        TipoBaseLegal = x.TipoBaseLegal,
                        NormaId = x.Norma != null ? x.NormaId.Value : 0,
                        Descripcion = x.Norma != null ? x.Norma.Descripcion : x.Descripcion,
                        TipoNormaId = x.Norma != null ? x.Norma.TipoNorma.MetaDatoId : x.TipoNorma.MetaDatoId,
                        NormaTipo = x.Norma != null ? x.Norma.TipoNorma.Nombre : x.TipoNorma.Nombre,
                        Numero = x.Norma != null ? x.Norma.Numero : x.Numero,
                        FechaPublicacion = x.Norma != null ? (x.Norma.FechaPublicacion == null ? "" : x.Norma.FechaPublicacion.Value.ToString("dd/MM/yyyy")) : (x.FechaPublicacion == null ? "" : x.FechaPublicacion.Value.ToString("dd/MM/yyyy")),
                        Sector = x.Norma != null ? x.Norma.Sector : x.Sector,
                        Articulo = x.Articulo,
                        Url = x.Url,
                        ArchivoAdjuntoId = x.TipoBaseLegal != TipoBaseLegal.RutaInternet ? (x.Norma != null ? 0 : x.ArchivoAdjuntoId) : 0,
                        EstadoACR = x.EstadoACR,
                        DescripcionACR = x.DescripcionACR,
                    }).ToList()
                    );


                }

                ViewBag.ListaBaseLegal = JsonConvert.SerializeObject(lista);
                ViewBag.ListaBaseLegalvalidar = JsonConvert.SerializeObject(lstvalidar);
                ViewBag.User = user;


                Observacion obstecnico = new Observacion();
                obstecnico.ExpedienteId = exp.ExpedienteId;
                obstecnico.ProcedimientoId = id;
                obstecnico.Estado = "0";
                obstecnico.CodValidacion = "5";
                obstecnico.EntidadId = entidadid;
                obstecnico.Pantalla = "Sustento Tecnico";
                ViewBag.Observaciontecnico = _observacionService.GetOneVerObservacion(obstecnico);


                string viewName = string.Empty;
                viewName = "VerObsSustentoTecnico";

                return View(viewName, model);
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ModelState.AddModelError("VerObsSustentoTecnico", ex.Message);
                _log.Error(ex);
                return View("_Error");
            }
        }
        [HttpPost]
        public ActionResult SustentoTecnico(Procedimiento model)
        {
            try
            {


                List<PlazoAtencion> lstplazo = _plazoAtencionService.GetAll(model.ProcedimientoId).Where(x => x.Sustento == null && x.Plazo > 30).ToList();

                // borrar ninguno
                model.EstadoNinguno = 0;


                if (model.Operacion == OperacionExpediente.Eliminacion)
                {
                    _procedimientoService.GuardarSustEliminacion(model);
                }
                else if (lstplazo.Count() != 0)
                {

                    return Json(new
                    {
                        valid = false,
                        mensaje = 0
                    });
                }
                else
                {
                    if (model.Requisito != null)
                    {
                        foreach (Requisito req in model.Requisito)
                        {
                            //FVN Se cambia validacion para notas 17-01-2019
                            //se agrega If
                            if (req.BaseLegal.BaseLegalNorma != null)
                            {
                                for (int i = 0; i < req.BaseLegal.BaseLegalNorma.Count(); i++)
                                {
                                    req.BaseLegal.BaseLegalNorma[i].BaseLegalId = req.BaseLegal.BaseLegalId;
                                    req.BaseLegal.BaseLegalNorma[i].BaseLegalNormaId = i + 1;
                                    req.BaseLegal.BaseLegalNorma[i].Norma = null;
                                    req.BaseLegal.BaseLegalNorma[i].ArchivoAdjunto = null;
                                    if (req.BaseLegal.BaseLegalNorma[i].EstadoACR == "2")
                                    {
                                        req.BaseLegal.BaseLegalNorma[i].TipoNormaId = 141;
                                    }
                                    if (req.BaseLegal.BaseLegalNorma[i].NormaId == 0) req.BaseLegal.BaseLegalNorma[i].NormaId = null;
                                    if (req.BaseLegal.BaseLegalNorma[i].ArchivoAdjuntoId == 0) req.BaseLegal.BaseLegalNorma[i].ArchivoAdjuntoId = null;
                                }
                            }

                        }
                    }
                    _requisitoService.GuardarSustentoTecnico(model);

                }

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

        public ActionResult SustentoLegal(long id, UsuarioInfo user)
        {
            try
            {
                ViewBag.Usuario = user;
                Procedimiento model = _procedimientoService.GetOne(id);

                if (model.Codigo == "S/C" && !string.IsNullOrEmpty(model.CodigoACR)) return RedirectToAction("NuevoCodigo", "Procedimiento", new { area = "Simplificacion", id = id });

                List<object> lista = new List<object>();
                for (int i = 0; i < model.Requisito.Count(); i++)
                {
                    List<BaseLegalNorma> blList = _baseLegalService.GetDetails(model.Requisito[i].BaseLegalId.Value);
                    lista.Add(blList.Select(x => new
                    {
                        BaseLegalId = x.BaseLegalId,
                        BaseLegalNormaId = x.BaseLegalNormaId,
                        TipoBaseLegal = x.TipoBaseLegal,
                        NormaId = x.Norma != null ? x.NormaId.Value : 0,
                        Descripcion = x.Norma != null ? x.Norma.Descripcion : x.Descripcion,
                        TipoNormaId = x.Norma != null ? x.Norma.TipoNorma.MetaDatoId : x.TipoNorma.MetaDatoId,
                        NormaTipo = x.Norma != null ? x.Norma.TipoNorma.Nombre : x.TipoNorma.Nombre,
                        Numero = x.Norma != null ? x.Norma.Numero : x.Numero,
                        FechaPublicacion = x.Norma != null ? (x.Norma.FechaPublicacion == null ? "" : x.Norma.FechaPublicacion.Value.ToString("dd/MM/yyyy")) : (x.FechaPublicacion == null ? "" : x.FechaPublicacion.Value.ToString("dd/MM/yyyy")),
                        Sector = x.Norma != null ? x.Norma.Sector : x.Sector,
                        Articulo = x.Articulo,
                        Url = x.Url,
                        ArchivoAdjuntoId = x.TipoBaseLegal != TipoBaseLegal.RutaInternet ? (x.Norma != null ? 0 : x.ArchivoAdjuntoId) : 0
                    }).ToList()
                        );
                }

                ViewBag.ListaBaseLegal = JsonConvert.SerializeObject(lista);


                string viewName = string.Empty;
                if (model.SustLegalTerminado) viewName = "VerSustentoLegal";
                else
                {
                    switch (user.Rol)
                    {
                        case (short)Rol.Administrador: viewName = model.TipoProcedimiento == TipoProcedimiento.Estandar ? "SustentoLegal" : "VerSustentoLegal"; break;
                        case (short)Rol.Coordinador: viewName = "SustentoLegal"; break;
                        case (short)Rol.RegistradorProcesos: viewName = "SustentoLegal"; break; //modificar
                        case (short)Rol.Evaluador: viewName = "VerSustentoLegal"; break; //modificar
                        case (short)Rol.Ratificador: viewName = "VerSustentoLegal"; break; //modificar
                        case (short)Rol.EntidadFiscalizadora: viewName = "VerSustentoLegal"; break; //modificar
                    }
                }

                return View(viewName, model);
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ModelState.AddModelError("SustentoLegal", ex.Message);
                _log.Error(ex);
                return View("_Error");
            }
        }

        [HttpPost]
        public ActionResult SustentoLegal(Procedimiento model)
        {
            try
            {
                _requisitoService.GuardarSustentoLegal(model);

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
        public ActionResult CambiarOperacion(OperacionExpediente operacion, long ProcedimientoId)
        {
            try
            {
                _procedimientoService.CambiarOperacion(operacion, ProcedimientoId);

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
        public ActionResult EliminarProcedimientoNuevo(long ProcedimientoId, UsuarioInfo user)
        {
            try
            {


                Procedimiento model3 = new Procedimiento();

                model3 = _procedimientoService.GetOne(ProcedimientoId);

                if (model3.CodigoACR != "0")
                {
                    OperacionExpediente tipo = OperacionExpediente.Eliminacion;
                    _procedimientoService.CambiarOperacion(tipo, ProcedimientoId);
                }
                else
                {
                    //model3.Estado = 3;
                    //model3.ProcedimientoId = ProcedimientoId;
                    //_procedimientoService.Eliminacionprocedimiento(model3);
                    OperacionExpediente tipo = OperacionExpediente.Eliminacion;
                    _procedimientoService.CambiarOperacion(tipo, ProcedimientoId);
                }

                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Eliminar Procedimiento Nuevo";
                objauditoria.Pantalla = "Procedimiento";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/


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

        #region Select Procedimiento
        public ActionResult Select(TipoTupa tipo, bool multi, string fnAdd)
        {
            try
            {
                ViewBag.Multi = multi;
                ViewBag.FnAdd = fnAdd;

                ViewBag.TipoTupa = (short)tipo;

                return PartialView("_Select");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult SelectByExpediente(long ExpedienteId, long ProcedimientoOrigenId, bool multi, string fnAdd)
        {
            try
            {
                ViewBag.Multi = multi;
                ViewBag.FnAdd = fnAdd;

                ViewBag.ExpedienteId = ExpedienteId;
                ViewBag.ProcedimientoOrigenId = ProcedimientoOrigenId;

                return PartialView("_SelectByExpediente");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult GetAllLikePaginByTupaxidDelete(long expedienteId, Procedimiento filtro, int pageIndex, int pageSize)
        {
            try
            {
                int totalRows = 0;
                long entityid = (long)Session["entityid"];
                List<Procedimiento> lista = _procedimientoService.GetAllLikePaginByTupaxidDelete(expedienteId, entityid, filtro, pageIndex, pageSize, ref totalRows);

                ViewBag.TotalRows = totalRows;
                ViewBag.Row = (pageSize * pageIndex) - pageSize;

                return Json(new
                {
                    lista = lista.Select(x => new
                    {
                        ProcedimientoId = x.ProcedimientoId,
                        CodigoCorto = x.CodigoCorto,
                        Denominacion = x.Denominacion,
                        Tipo = (short)x.TipoProcedimiento,
                        ExpedienteId = x.ExpedienteId,
                        Entidad = x.Expediente.Entidad.Nombre
                    }),
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

        public ActionResult GetAllLikePaginByTupaxidFiltroDelete(long expedienteId, int pageIndex, int pageSize)
        {
            try
            {
                int totalRows = 0;
                long entityid = (long)Session["entityid"];

                List<Procedimiento> lista = _procedimientoService.GetAllLikePaginByTupaEstandarDelete(expedienteId, entityid, pageIndex, pageSize, ref totalRows);

                ViewBag.TotalRows = totalRows;
                ViewBag.Row = (pageSize * pageIndex) - pageSize;

                return Json(new
                {
                    lista = lista.Select(x => new
                    {
                        ExpedienteId = x.ExpedienteId,
                        EstadoExp = x.Expediente.EstadoExpediente,
                        Codigo1 = x.Expediente.Codigo,
                        //Tipo = (short)x.TipoProcedimiento,
                        Entidad = x.Expediente.Entidad.Nombre
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


        public ActionResult SelectDelete(long expedienteId, TipoTupa tipo, bool multi, string fnAdd)
        {
            try
            {
                ViewBag.Multi = multi;
                ViewBag.FnAdd = fnAdd;
                ViewBag.expedienteId = expedienteId;

                ViewBag.TipoTupa = (short)tipo;

                return PartialView("_SelectDelete");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult AddEstandarDelete(long ExpedienteId, List<long> ids, UsuarioInfo user)
        {
            try
            {
                string mensajes = "";

                int CodigoCanalOficinaEntidad = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/Areas/Simplificacion/Views/Web.config").AppSettings.Settings["AutoCompletarOficinaEntidad"].Value);

                for (int i = 0; i < ids.Count(); i++)
                {
                    long proceso = ids[i];
                    Procedimiento codigo = _procedimientoService.GetOne(proceso);
                    Procedimiento valorcodigo = _procedimientoService.GetOneCount(codigo.Codigo, ExpedienteId);
                    //if (valorcodigo != null)
                    //{
                    //if (codigo.Codigo == valorcodigo.Codigo)
                    //{
                    //mensajes = "Duplicado";
                    //}
                    //}
                    //if (mensajes != "Duplicado")
                    //{
                    List<long> idunico = new List<long>();
                    idunico.Add(ids[i]);

                    //_procedimientoService.CopiarProcedimiento(ExpedienteId, idunico, user.UsuarioId, user.EntidadId, CodigoCanalOficinaEntidad);

                    CopiaProcedimientoViewModel modelproc = new CopiaProcedimientoViewModel();
                    modelproc.InfoAlCiudadano = true;
                    modelproc.InfoBasica = true;
                    modelproc.TipoAtencion = true;
                    modelproc.Todo = true;
                    modelproc.Requisito = true;
                    modelproc.TablaASME = true;
                    modelproc.SustentoRequisito = true;
                    modelproc.BaseLegal = true;
                    modelproc.ProcedimientoDestinoId = 0;
                    modelproc.ProcedimientoOrigenId = proceso;
                    modelproc.ExpedienteId = 0;
                    modelproc.DatosGeneralesYSTL = false;
                    modelproc.Nuevo = false;
                    modelproc.Numero = 0;
                    modelproc.SustentoCalificacion = false;
                    modelproc.TipoExpediente = TipoExpediente.ExpedienteRegular;

                    CopiarProcedimientoEli(modelproc, user);
                    //}



                }


                return Json(new { valid = true, mensaje = mensajes }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ModelState.AddModelError("", ex.Message);
                _log.Error(ex);
                return PartialView("_Error");
            }
        }


        public ActionResult GetAllLikePaginByTupa(TipoTupa tipo, Procedimiento filtro, int pageIndex, int pageSize)
        {
            try
            {
                int totalRows = 0;
                List<Procedimiento> lista = _procedimientoService.GetAllLikePaginByTupa(tipo, filtro, pageIndex, pageSize, ref totalRows);

                ViewBag.TotalRows = totalRows;
                ViewBag.Row = (pageSize * pageIndex) - pageSize;

                return Json(new
                {
                    lista = lista.Select(x => new
                    {
                        ProcedimientoId = x.ProcedimientoId,
                        CodigoCorto = x.CodigoCorto,
                        Denominacion = x.Denominacion,
                        Tipo = (short)x.TipoProcedimiento,
                        Entidad = x.Expediente.Entidad.Nombre
                    }),
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


        public ActionResult GetAllLikePaginByTupaxid(TipoTupa tipo, Procedimiento filtro, int pageIndex, int pageSize)
        {
            try
            {
                int totalRows = 0;
                List<Procedimiento> lista = _procedimientoService.GetAllLikePaginByTupaxid(tipo, filtro, pageIndex, pageSize, ref totalRows);

                ViewBag.TotalRows = totalRows;
                ViewBag.Row = (pageSize * pageIndex) - pageSize;

                return Json(new
                {
                    lista = lista.Select(x => new
                    {
                        ProcedimientoId = x.ProcedimientoId,
                        CodigoCorto = x.CodigoCorto,
                        Denominacion = x.Denominacion,
                        Tipo = (short)x.TipoProcedimiento,
                        ExpedienteId = x.ExpedienteId,
                        Entidad = x.Expediente.Entidad.Nombre
                    }),
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


        public ActionResult GetAllLikePaginByTupaxidFiltro(TipoTupa tipo, Procedimiento filtro, int pageIndex, int pageSize)
        {
            try
            {
                int totalRows = 0;
                List<Procedimiento> lista = _procedimientoService.GetAllLikePaginByTupaEstandar(tipo, filtro, pageIndex, pageSize, ref totalRows);

                ViewBag.TotalRows = totalRows;
                ViewBag.Row = (pageSize * pageIndex) - pageSize;

                return Json(new
                {
                    lista = lista.Select(x => new
                    {
                        ExpedienteId = x.ExpedienteId,
                        Tipo = (short)x.TipoProcedimiento,
                        Entidad = x.Expediente.Entidad.Nombre
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

        #endregion
    }
}

public class elemento
{

    public string orden { get; set; }

    public string id { get; set; }

}

public class elemento2
{

    public string codigo { get; set; }

    public string nombre { get; set; }

}