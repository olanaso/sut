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
    public class IncentivosCorteController : Controller
    {
        private readonly ILogService<IncentivosCorteController> _log;
        private readonly IEntidadService _entidadService;
        private readonly IMiembroEquipoService _miembroEquipoService;
        private readonly IMetaDatoService _metaDatoService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly IIncentivosCorteService _IncentivosCorteService;
        private readonly IRolMenuService _RolMenuService;
        private readonly IIncentivosFormulasCorteService _IncentivosFormulasCorteService;
        private readonly IArchivoAdjuntoService _ArchivoAdjuntoService;
        private readonly IDepartamentoService _departamentoService;

        public IncentivosCorteController(IEntidadService entidadService,
                                IMiembroEquipoService miembroEquipoService,
                                IAuditoriaService AuditoriaService,
                                IMetaDatoService metaDatoService,
                                IIncentivosCorteService IncentivosCorteService,
                                IDepartamentoService departamentoService,
                                IRolMenuService rolMenuService,
                                IIncentivosFormulasCorteService IncentivosFormulasCorteService,
                                IArchivoAdjuntoService ArchivoAdjuntoService)
        {
            _log = new LogService<IncentivosCorteController>();
            _entidadService = entidadService;
            _miembroEquipoService = miembroEquipoService;
            _metaDatoService = metaDatoService;
            _AuditoriaService = AuditoriaService;
            _IncentivosCorteService = IncentivosCorteService;
            _departamentoService = departamentoService;
            _RolMenuService = rolMenuService;
            _IncentivosFormulasCorteService = IncentivosFormulasCorteService;
            _ArchivoAdjuntoService = ArchivoAdjuntoService;
        }

        public ActionResult Lista(UsuarioInfo user)
        {
            try
            {
                ViewBag.Usuario = user;
                Filtros filorder = new Filtros();
                ViewBag.periodos = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "-TODOS -", Value = "0"},
                    new SelectListItem() { Text = "Marzo – Junio", Value = "1" },
                    new SelectListItem() { Text = "Agosto – Diciembre", Value = "2"},
                };


                List<Departamento> listaDepartamento = _departamentoService.GetAll();
                listaDepartamento = listaDepartamento.OrderBy(x => x.Nombre).ToList();
                listaDepartamento.Insert(0, new Departamento() { DepartamentoId = 0, Nombre = " - SELECCIONAR - " });
                ViewBag.ListaDepartamento = listaDepartamento.Select(x => new SelectListItem() { Value = x.DepartamentoId.ToString(), Text = x.Nombre }).ToList();


                List<Provincia> listaProvincia = new List<Provincia>();
                listaProvincia = listaProvincia.OrderBy(x => x.Nombre).ToList();
                listaProvincia.Insert(0, new Provincia() { ProvinciaId = 0, Nombre = " - SELECCIONAR - " });
                ViewBag.ListaProvincia = listaProvincia.Select(x => new SelectListItem()
                {
                    Value = x.ProvinciaId.ToString(),
                    Text = x.Nombre
                })
              .ToList();




                var listaEntidad = _entidadService.GetAll()
                                                .Where(x => x.Estado == Respuesta.Si)
                                                .OrderBy(x => x.Nombre)
                                                .ToList();
                listaEntidad.Insert(0, new Entidad()
                {
                    EntidadId = 0,
                    Nombre = " - TODOS - "
                });

                ViewBag.ListaEntidad = listaEntidad.Select(x => new SelectListItem()
                {
                    Value = x.EntidadId.ToString(),
                    Text = x.Nombre
                })
                                                    .ToList();



                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                _log.Error(ex);
                return View("_Error");
            }
        }


        public ActionResult GetAllLikePagin(IncentivosCorte filtro, int pageIndex, int pageSize, Int32 tipoperiodo, long EntidadId, UsuarioInfo user)
        {
            try
            {

                List<IncentivosCorte> lista = new List<IncentivosCorte>();

                int totalRows = 0;

                if (user.EntidadId != 4105)
                {
                    lista = _IncentivosCorteService.GetByIncentivosCorte(filtro, user.EntidadId, pageIndex, pageSize, tipoperiodo, ref totalRows);
                }
                else
                {
                    lista = _IncentivosCorteService.GetByIncentivosCorte(filtro, EntidadId, pageIndex, pageSize, tipoperiodo, ref totalRows);
                }

                var json = Json(new
                {
                    lista = lista.Select(x => new
                    {
                        IncentivosCorteId = x.IncentivosCorteId,
                        NumExpediente = x.NumExpediente,
                        Fec_Ing_Sol = x.Fec_Ing_Sol == null ? "-" : x.Fec_Ing_Sol.Value.ToString("dd/MM/yyyy"),
                        NivelRiesgo = x.NivelRiesgo,
                        Fec_Emision_Cert_ITSE = x.Fec_Emision_Cert_ITSE == null ? "-" : x.Fec_Emision_Cert_ITSE.Value.ToString("dd/MM/yyyy"),
                        Fec_Notificacion_Cert_ITSE = x.Fec_Notificacion_Cert_ITSE == null ? "-" : x.Fec_Notificacion_Cert_ITSE.Value.ToString("dd/MM/yyyy"),
                        Fec_Emision_Licencia_Funcionamiento = x.Fec_Emision_Licencia_Funcionamiento == null ? "-" : x.Fec_Emision_Licencia_Funcionamiento.Value.ToString("dd/MM/yyyy"),
                        Fec_Notificacion_Licencia_Funcionamiento = x.Fec_Notificacion_Licencia_Funcionamiento == null ? "-" : x.Fec_Notificacion_Licencia_Funcionamiento.Value.ToString("dd/MM/yyyy"),
                        Fec_Revocacion_Licencia_Funcionamiento = x.Fec_Revocacion_Licencia_Funcionamiento == null ? "-" : x.Fec_Revocacion_Licencia_Funcionamiento.Value.ToString("dd/MM/yyyy"),
                        Fec_Suspencion_ITSE_Subsanacion = x.Fec_Suspencion_ITSE_Subsanacion == null ? "-" : x.Fec_Suspencion_ITSE_Subsanacion.Value.ToString("dd/MM/yyyy"),

                        Cod_Acreditacion_Inspector_ITSE = x.Cod_Acreditacion_Inspector_ITSE,
                        Renovacion_ITSE = x.Renovacion_ITSE == null ? "-" : x.Renovacion_ITSE.Value.ToString("dd/MM/yyyy"),
                        Estado = x.Estado,
                        EstadoIngreso = x.EstadoIngreso,
                        Entidad = x.Entidad.Nombre,
                        FecCorte = x.FecCorte == null ? "-" : x.FecCorte.Value.ToString("dd/MM/yyyy"),

                        EvaFecdoc = x.Eva_Fec_doc,
                        EvaDoccorresponde = x.Eva_Doc_corresponde,
                        EvaComentario = x.Eva_Comentario == null ? "" : x.Eva_Comentario,
                        EvaObservacion = x.Eva_Observacion == null ? "" : x.Eva_Comentario,
                        EvaResultado = x.Eva_Resultado,
                    }),
                    totalRows = totalRows

                }, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = 500000000;
                return json;

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                _log.Error(ex);
                return PartialView("_Error");
            }
        }

        public ActionResult Editar(long id, UsuarioInfo user)
        {
            try
            {
                ViewBag.User = user;

                ArchivoAdjunto2 obj2 = new ArchivoAdjunto2();
                ArchivoAdjunto3 obj3 = new ArchivoAdjunto3();
                ArchivoAdjunto4 obj4 = new ArchivoAdjunto4();
                ArchivoAdjunto5 obj5 = new ArchivoAdjunto5();
                ArchivoAdjunto6 obj6 = new ArchivoAdjunto6();
                ArchivoAdjunto7 obj7 = new ArchivoAdjunto7();
                ArchivoAdjunto10 obj10 = new ArchivoAdjunto10();
                ArchivoAdjunto11 obj11 = new ArchivoAdjunto11();

                ArchivoAdjunto obj = new ArchivoAdjunto();


                IncentivosCorte item;
                if (id == 0)
                {
                    item = new IncentivosCorte();
                    ViewBag.Title = "Nueva Licencia de Funcionamiento";

                }
                else
                {
                    item = _IncentivosCorteService.GetByone(id);
                    ViewBag.Title = "Editar Licencia de Funcionamiento";



                    if (item.Archivo_Fec_Emision_Cert_ITSE != null && item.Archivo_Fec_Emision_Cert_ITSE != 0)
                    {

                        obj = _ArchivoAdjuntoService.GetOne(Convert.ToInt32(item.Archivo_Fec_Emision_Cert_ITSE));
                        item.ArchivoAdjunto2 = obj2;

                        item.ArchivoAdjunto2.Archivo_Fec_Emision_Cert_ITSE = Convert.ToInt32(obj.ArchivoAdjuntoId);
                        item.ArchivoAdjunto2.NombreArchivo = obj.NombreArchivo;
                        item.ArchivoAdjunto2.Ruta = obj.Ruta;
                        item.ArchivoAdjunto2.Tipo = obj.Tipo;
                        item.ArchivoAdjunto2.Tamano = obj.Tamano;
                        item.ArchivoAdjunto2.Extension = obj.Extension;
                        item.ArchivoAdjunto2.Descripcion = obj.Descripcion;

                        item.ArchivoAdjunto2.FecCreacion = obj.FecCreacion;
                        item.ArchivoAdjunto2.FecModificacion = obj.FecModificacion;
                        item.ArchivoAdjunto2.UserCreacion = obj.UserCreacion;
                        item.ArchivoAdjunto2.UserModificacion = obj.UserModificacion;
                    }


                    if (item.Archivo_Fec_Suspencion_ITSE != null && item.Archivo_Fec_Suspencion_ITSE != 0)
                    {

                        obj = _ArchivoAdjuntoService.GetOne(Convert.ToInt32(item.Archivo_Fec_Suspencion_ITSE));
                        item.ArchivoAdjunto11 = obj11;
                        item.ArchivoAdjunto11.Archivo_Fec_Suspencion_ITSE = Convert.ToInt32(obj.ArchivoAdjuntoId);
                        item.ArchivoAdjunto11.NombreArchivo = obj.NombreArchivo;
                        item.ArchivoAdjunto11.Ruta = obj.Ruta;
                        item.ArchivoAdjunto11.Tipo = obj.Tipo;
                        item.ArchivoAdjunto11.Tamano = obj.Tamano;
                        item.ArchivoAdjunto11.Extension = obj.Extension;
                        item.ArchivoAdjunto11.Descripcion = obj.Descripcion;
                        item.ArchivoAdjunto11.FecCreacion = obj.FecCreacion;
                        item.ArchivoAdjunto11.FecModificacion = obj.FecModificacion;
                        item.ArchivoAdjunto11.UserCreacion = obj.UserCreacion;
                        item.ArchivoAdjunto11.UserModificacion = obj.UserModificacion;
                    }

                    if (item.Archivo_Fec_Notificacion_Cert_ITSE != null && item.Archivo_Fec_Notificacion_Cert_ITSE != 0)
                    {

                        obj = _ArchivoAdjuntoService.GetOne(Convert.ToInt32(item.Archivo_Fec_Notificacion_Cert_ITSE));
                        item.ArchivoAdjunto3 = obj3;
                        item.ArchivoAdjunto3.Archivo_Fec_Notificacion_Cert_ITSE = Convert.ToInt32(obj.ArchivoAdjuntoId);
                        item.ArchivoAdjunto3.NombreArchivo = obj.NombreArchivo;
                        item.ArchivoAdjunto3.Ruta = obj.Ruta;
                        item.ArchivoAdjunto3.Tipo = obj.Tipo;
                        item.ArchivoAdjunto3.Tamano = obj.Tamano;
                        item.ArchivoAdjunto3.Extension = obj.Extension;
                        item.ArchivoAdjunto3.Descripcion = obj.Descripcion;
                        item.ArchivoAdjunto3.FecCreacion = obj.FecCreacion;
                        item.ArchivoAdjunto3.FecModificacion = obj.FecModificacion;
                        item.ArchivoAdjunto3.UserCreacion = obj.UserCreacion;
                        item.ArchivoAdjunto3.UserModificacion = obj.UserModificacion;
                    }

                    if (item.Archivo_Fec_Emision_Licencia_Funcionamiento != null && item.Archivo_Fec_Emision_Licencia_Funcionamiento != 0)
                    {

                        obj = _ArchivoAdjuntoService.GetOne(Convert.ToInt32(item.Archivo_Fec_Emision_Licencia_Funcionamiento));
                        item.ArchivoAdjunto4 = obj4;
                        item.ArchivoAdjunto4.Archivo_Fec_Emision_Licencia_Funcionamiento = Convert.ToInt32(obj.ArchivoAdjuntoId);
                        item.ArchivoAdjunto4.NombreArchivo = obj.NombreArchivo;
                        item.ArchivoAdjunto4.Ruta = obj.Ruta;
                        item.ArchivoAdjunto4.Tipo = obj.Tipo;
                        item.ArchivoAdjunto4.Tamano = obj.Tamano;
                        item.ArchivoAdjunto4.Extension = obj.Extension;
                        item.ArchivoAdjunto4.Descripcion = obj.Descripcion;
                        item.ArchivoAdjunto4.FecCreacion = obj.FecCreacion;
                        item.ArchivoAdjunto4.FecModificacion = obj.FecModificacion;
                        item.ArchivoAdjunto4.UserCreacion = obj.UserCreacion;
                        item.ArchivoAdjunto4.UserModificacion = obj.UserModificacion;
                    }

                    if (item.Archivo_Fec_Notificacion_Licencia_Funcionamiento != null && item.Archivo_Fec_Notificacion_Licencia_Funcionamiento != 0)
                    {

                        obj = _ArchivoAdjuntoService.GetOne(Convert.ToInt32(item.Archivo_Fec_Notificacion_Licencia_Funcionamiento));
                        item.ArchivoAdjunto5 = obj5;
                        item.ArchivoAdjunto5.Archivo_Fec_Notificacion_Licencia_Funcionamiento = Convert.ToInt32(obj.ArchivoAdjuntoId);
                        item.ArchivoAdjunto5.NombreArchivo = obj.NombreArchivo;
                        item.ArchivoAdjunto5.Ruta = obj.Ruta;
                        item.ArchivoAdjunto5.Tipo = obj.Tipo;
                        item.ArchivoAdjunto5.Tamano = obj.Tamano;
                        item.ArchivoAdjunto5.Extension = obj.Extension;
                        item.ArchivoAdjunto5.Descripcion = obj.Descripcion;
                        item.ArchivoAdjunto5.FecCreacion = obj.FecCreacion;
                        item.ArchivoAdjunto5.FecModificacion = obj.FecModificacion;
                        item.ArchivoAdjunto5.UserCreacion = obj.UserCreacion;
                        item.ArchivoAdjunto5.UserModificacion = obj.UserModificacion;
                    }

                    if (item.Archivo_Fec_Suspencion_ITSE_Subsanacion != null && item.Archivo_Fec_Suspencion_ITSE_Subsanacion != 0)
                    {

                        obj = _ArchivoAdjuntoService.GetOne(Convert.ToInt32(item.Archivo_Fec_Suspencion_ITSE_Subsanacion));
                        item.ArchivoAdjunto6 = obj6;
                        item.ArchivoAdjunto6.Archivo_Fec_Suspencion_ITSE_Subsanacion = Convert.ToInt32(obj.ArchivoAdjuntoId);
                        item.ArchivoAdjunto6.NombreArchivo = obj.NombreArchivo;
                        item.ArchivoAdjunto6.Ruta = obj.Ruta;
                        item.ArchivoAdjunto6.Tipo = obj.Tipo;
                        item.ArchivoAdjunto6.Tamano = obj.Tamano;
                        item.ArchivoAdjunto6.Extension = obj.Extension;
                        item.ArchivoAdjunto6.Descripcion = obj.Descripcion;
                        item.ArchivoAdjunto6.FecCreacion = obj.FecCreacion;
                        item.ArchivoAdjunto6.FecModificacion = obj.FecModificacion;
                        item.ArchivoAdjunto6.UserCreacion = obj.UserCreacion;
                        item.ArchivoAdjunto6.UserModificacion = obj.UserModificacion;
                    }

                    if (item.Archivo_Fec_Resolucion_denegatoria != null && item.Archivo_Fec_Resolucion_denegatoria != 0)
                    {

                        obj = _ArchivoAdjuntoService.GetOne(Convert.ToInt32(item.Archivo_Fec_Resolucion_denegatoria));
                        item.ArchivoAdjunto7 = obj7;
                        item.ArchivoAdjunto7.Archivo_Fec_Resolucion_denegatoria = Convert.ToInt32(obj.ArchivoAdjuntoId);
                        item.ArchivoAdjunto7.NombreArchivo = obj.NombreArchivo;
                        item.ArchivoAdjunto7.Ruta = obj.Ruta;
                        item.ArchivoAdjunto7.Tipo = obj.Tipo;
                        item.ArchivoAdjunto7.Tamano = obj.Tamano;
                        item.ArchivoAdjunto7.Extension = obj.Extension;
                        item.ArchivoAdjunto7.Descripcion = obj.Descripcion;
                        item.ArchivoAdjunto7.FecCreacion = obj.FecCreacion;
                        item.ArchivoAdjunto7.FecModificacion = obj.FecModificacion;
                        item.ArchivoAdjunto7.UserCreacion = obj.UserCreacion;
                        item.ArchivoAdjunto7.UserModificacion = obj.UserModificacion;
                    }

                    if (item.Archivo_Fec_Revocacion_Licencia_Funcionamiento != null && item.Archivo_Fec_Revocacion_Licencia_Funcionamiento != 0)
                    {

                        obj = _ArchivoAdjuntoService.GetOne(Convert.ToInt32(item.Archivo_Fec_Revocacion_Licencia_Funcionamiento));
                        item.ArchivoAdjunto10 = obj10;
                        item.ArchivoAdjunto10.Archivo_Fec_Revocacion_Licencia_Funcionamiento = Convert.ToInt32(obj.ArchivoAdjuntoId);
                        item.ArchivoAdjunto10.NombreArchivo = obj.NombreArchivo;
                        item.ArchivoAdjunto10.Ruta = obj.Ruta;
                        item.ArchivoAdjunto10.Tipo = obj.Tipo;
                        item.ArchivoAdjunto10.Tamano = obj.Tamano;
                        item.ArchivoAdjunto10.Extension = obj.Extension;
                        item.ArchivoAdjunto10.Descripcion = obj.Descripcion;
                        item.ArchivoAdjunto10.FecCreacion = obj.FecCreacion;
                        item.ArchivoAdjunto10.FecModificacion = obj.FecModificacion;
                        item.ArchivoAdjunto10.UserCreacion = obj.UserCreacion;
                        item.ArchivoAdjunto10.UserModificacion = obj.UserModificacion;
                    }

                }


                ViewBag.Nivel = new List<SelectListItem>()
                {
                     new SelectListItem() { Text = "- Seleccionar -", Value = "0"},
                    new SelectListItem() { Text = "Bajo", Value = "1" , Selected = item.NivelRiesgo == 1 ? true:false},
                    new SelectListItem() { Text = "Medio", Value = "2",  Selected = item.NivelRiesgo == 2? true:false },
                    new SelectListItem() { Text = "Alto", Value = "3",  Selected = item.NivelRiesgo == 3? true:false },
                    new SelectListItem() { Text = "Muy Alto", Value = "4",  Selected = item.NivelRiesgo == 4? true:false },
                };
                ViewBag.publicar = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Activo", Value = "1" , Selected = item.Estado == 1 ? true:false},
                    new SelectListItem() { Text = "Inactivo", Value = "2",  Selected = item.Estado == 0? true:false },
                };

                ViewBag.EvaFecdoc = new List<SelectListItem>()
                {
                     new SelectListItem() { Text = "- Seleccionar -", Value = "0" , Selected = item.Estado == 0 ? true:false},
                    new SelectListItem() { Text = "SI", Value = "2" , Selected = item.Estado == 2 ? true:false},
                    new SelectListItem() { Text = "NO", Value = "1",  Selected = item.Estado == 1 ? true:false },
                };



                ViewBag.EvaDoccorresponde = new List<SelectListItem>()
                {
                     new SelectListItem() { Text = "- Seleccionar -", Value = "0" , Selected = item.Estado == 0 ? true:false},
                    new SelectListItem() { Text = "SI", Value = "2" , Selected = item.Estado == 2 ? true:false},
                    new SelectListItem() { Text = "NO", Value = "1",  Selected = item.Estado == 1 ? true:false },
                };

                ViewBag.EvaResultado = new List<SelectListItem>()
                {
                     new SelectListItem() { Text = "- Seleccionar -", Value = "0" , Selected = item.Estado == 0 ? true:false},
                    new SelectListItem() { Text = "SI califica", Value = "2" , Selected = item.Estado == 2 ? true:false},
                    new SelectListItem() { Text = "NO califica", Value = "1",  Selected = item.Estado == 1 ? true:false },
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



        [HttpPost]
        public ActionResult actualizar(int estado, UsuarioInfo user)
        {
            try
            {

                _AuditoriaService.ActualizarLicencias(user.UsuarioId);
                //VERIFICAR

                return Json(new { valid = true, mensaje = "Los datos se copiaron correctamente. Con fecha: " + DateTime.Now.ToString() });


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



        public ActionResult Procesar(long EntidadId, int tipoperiodo, UsuarioInfo user)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    string mensaje = "Se Proceso correctamente";

                    Auditoria objauditoria = new Auditoria();



                    /*Inicio del calculo de proceso*/

                    IncentivosFormulasCorte objIncentivosFormulasCorte = new IncentivosFormulasCorte();


                    objIncentivosFormulasCorte = _IncentivosFormulasCorteService.GetByone(EntidadId);

                    if (objIncentivosFormulasCorte == null)
                    {
                        objIncentivosFormulasCorte = new IncentivosFormulasCorte();
                    }

                    IncentivosCorte objIncentivosCorte = new IncentivosCorte();

                    List<IncentivosCorte> listaBajoMedio = _IncentivosCorteService.GetByIncentivosCorteBajoMedio(EntidadId, tipoperiodo);

                    int mesbajomedio;
                    //TimeSpan tiempobajomedio1;

                    int cantidadbajo = 0;
                    int cantidadmedio = 0;

                    int numerobajo = 0;
                    int numeromedio = 0;


                    int Cant;
                    DateTime Ini;
                    DateTime Fin;

                    Cant = 0;

                    //Inicio de lista FECHAS DE FERIADOS

                    List<DateTime> listaDiasFestivos = new List<DateTime>();
                    listaDiasFestivos.Add(Convert.ToDateTime("01/04/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("02/04/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("01/05/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("29/06/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("28/07/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("29/07/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("30/08/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("08/10/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("11/10/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("01/11/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("02/11/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("08/12/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("25/12/2021"));
                    //Fin de lista FECHAS DE FERIADOS

                    foreach (var procBajoMedio in listaBajoMedio)
                    {
                        int diasbajomedio1 = 0;
                        int diasbajomedio2 = 0;

                        mesbajomedio = procBajoMedio.Fec_Ing_Sol.Value.Month;
                        if (mesbajomedio == 3 || mesbajomedio == 4 || mesbajomedio == 5 || mesbajomedio == 6 || mesbajomedio == 8 || mesbajomedio == 9 || mesbajomedio == 10 || mesbajomedio == 11 || mesbajomedio == 12)
                        //if (mesbajomedio == 4 || mesbajomedio == 5 || mesbajomedio == 6 || mesbajomedio == 7 || mesbajomedio == 8 || mesbajomedio == 9 || mesbajomedio == 10 || mesbajomedio == 11)
                        {
                            //tiempobajomedio1 = procBajoMedio.Fec_Notificacion_Licencia_Funcionamiento.Value.Date - procBajoMedio.Fec_Ing_Sol.Value.Date;
                            //diasbajomedio1 = tiempobajomedio1.Days;

                            if (procBajoMedio.Fec_Notificacion_Licencia_Funcionamiento != null)
                            {




                                Ini = procBajoMedio.Fec_Ing_Sol.Value.Date;
                                Fin = procBajoMedio.Fec_Notificacion_Licencia_Funcionamiento.Value.Date;
                                while (Ini != Fin)
                                {
                                    if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                                    {

                                        diasbajomedio1 = diasbajomedio1 + 1;
                                        foreach (var fecha in listaDiasFestivos)
                                        {
                                            if (Ini == fecha)
                                            {
                                                diasbajomedio1 = diasbajomedio1 - 1;
                                            }
                                        }
                                    }
                                    Ini = Ini.AddDays(1);
                                }


                                if (diasbajomedio1 <= 2 || procBajoMedio.Fec_Suspencion_ITSE_Subsanacion != null || procBajoMedio.Fec_Suspencion_ITSE != null)
                                {
                                    //tiempobajomedio2 = procBajoMedio.Fec_Notificacion_Cert_ITSE.Value.Date - procBajoMedio.Fec_Ing_Sol.Value.Date;
                                    //diasbajomedio2 = tiempobajomedio2.Days;
                                    if (procBajoMedio.Fec_Suspencion_ITSE_Subsanacion == null)
                                    {
                                        Ini = procBajoMedio.Fec_Ing_Sol.Value.Date;
                                    }
                                    else
                                    {
                                        Ini = procBajoMedio.Fec_Suspencion_ITSE_Subsanacion.Value.Date;
                                    }

                                    if (procBajoMedio.Fec_Notificacion_Cert_ITSE != null)
                                    {
                                        Fin = procBajoMedio.Fec_Notificacion_Cert_ITSE.Value.Date;
                                        while (Ini != Fin)
                                        {
                                            //if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                                            //    diasbajomedio2 = diasbajomedio2 + 1;
                                            //Ini = Ini.AddDays(1);

                                            if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                                            {

                                                diasbajomedio2 = diasbajomedio2 + 1;
                                                foreach (var fecha in listaDiasFestivos)
                                                {
                                                    if (Ini == fecha)
                                                    {
                                                        diasbajomedio2 = diasbajomedio2 - 1;
                                                    }
                                                }
                                            }
                                            Ini = Ini.AddDays(1);
                                        }
                                        if (procBajoMedio.Fec_Suspencion_ITSE_Subsanacion == null)
                                        {

                                            if (procBajoMedio.Fec_Suspencion_ITSE != null)
                                            {
                                                if (diasbajomedio2 <= 9)
                                                {
                                                    if (procBajoMedio.NivelRiesgo == 1)
                                                    {
                                                        cantidadbajo = cantidadbajo + 1;
                                                    }
                                                    else if (procBajoMedio.NivelRiesgo == 2)
                                                    {
                                                        cantidadmedio = cantidadmedio + 1;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (diasbajomedio2 <= 9)
                                                {
                                                    if (procBajoMedio.NivelRiesgo == 1)
                                                    {
                                                        cantidadbajo = cantidadbajo + 1;
                                                    }
                                                    else if (procBajoMedio.NivelRiesgo == 2)
                                                    {
                                                        cantidadmedio = cantidadmedio + 1;
                                                    }
                                                }
                                            }


                                        }
                                        else
                                        {
                                            if (procBajoMedio.Fec_Suspencion_ITSE != null)
                                            {

                                                if (diasbajomedio2 <= 5)
                                                {
                                                    if (procBajoMedio.NivelRiesgo == 1)
                                                    {
                                                        cantidadbajo = cantidadbajo + 1;
                                                    }
                                                    else if (procBajoMedio.NivelRiesgo == 2)
                                                    {
                                                        cantidadmedio = cantidadmedio + 1;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (diasbajomedio2 <= 5)
                                                {
                                                    if (procBajoMedio.NivelRiesgo == 1)
                                                    {
                                                        cantidadbajo = cantidadbajo + 1;
                                                    }
                                                    else if (procBajoMedio.NivelRiesgo == 2)
                                                    {
                                                        cantidadmedio = cantidadmedio + 1;
                                                    }
                                                }

                                            }


                                        }

                                    }

                                }

                                if (procBajoMedio.NivelRiesgo == 1)
                                {
                                    numerobajo = numerobajo + 1;
                                }
                                else if (procBajoMedio.NivelRiesgo == 2)
                                {
                                    numeromedio = numeromedio + 1;
                                }
                            }
                            else
                            {
                                if (procBajoMedio.NivelRiesgo == 1)
                                {
                                    numerobajo = numerobajo + 1;
                                }
                                else if (procBajoMedio.NivelRiesgo == 2)
                                {
                                    numeromedio = numeromedio + 1;
                                }
                            }
                        }
                    }

                    objIncentivosFormulasCorte.Numero_Licencias_Bajo = numerobajo;
                    objIncentivosFormulasCorte.Numero_Licencias_medio = numeromedio;
                    objIncentivosFormulasCorte.Licencias_Notificaciones_Bajo = cantidadbajo;
                    objIncentivosFormulasCorte.Licencias_Notificaciones_Medio = cantidadmedio;




                    List<IncentivosCorte> listaAltoMuyAlto = _IncentivosCorteService.GetByIncentivosCorteAltoMuyAlto(EntidadId, tipoperiodo);


                    int mesaltomuyalto;
                    //TimeSpan tiempoaltomuyalto1;

                    int cantidadAlto = 0;
                    int cantidadMuyAlto = 0;

                    int numeroAlto = 0;
                    int numeroMuyAlto = 0;

                    foreach (var procAltoMuyAlto in listaAltoMuyAlto)
                    {
                        int diasaltomuyalto1 = 0;
                        //TimeSpan tiempoaltomuyalto2;
                        int diasaltomuyalto2 = 0;
                        mesaltomuyalto = procAltoMuyAlto.Fec_Ing_Sol.Value.Month;


                        if (mesaltomuyalto == 3 || mesaltomuyalto == 4 || mesaltomuyalto == 5 || mesaltomuyalto == 6 || mesaltomuyalto == 8 || mesaltomuyalto == 9 || mesaltomuyalto == 10 || mesaltomuyalto == 11 || mesaltomuyalto == 12)

                        {
                            //tiempoaltomuyalto1 = procAltoMuyAlto.Fec_Emision_Cert_ITSE.Value.Date - procAltoMuyAlto.Fec_Ing_Sol.Value.Date;
                            //diasaltomuyalto1 = tiempoaltomuyalto1.Days;

                            if (procAltoMuyAlto.Fec_Emision_Cert_ITSE != null)
                            {


                                Ini = procAltoMuyAlto.Fec_Ing_Sol.Value.Date;
                                Fin = procAltoMuyAlto.Fec_Emision_Cert_ITSE.Value.Date;
                                while (Ini != Fin)
                                {
                                    //if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                                    //    diasaltomuyalto1 = diasaltomuyalto1 + 1;
                                    //Ini = Ini.AddDays(1);

                                    if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                                    {

                                        diasaltomuyalto1 = diasaltomuyalto1 + 1;
                                        foreach (var fecha in listaDiasFestivos)
                                        {
                                            if (Ini == fecha)
                                            {
                                                diasaltomuyalto1 = diasaltomuyalto1 - 1;
                                            }
                                        }
                                    }
                                    Ini = Ini.AddDays(1);
                                }

                                if (diasaltomuyalto1 <= 7 || procAltoMuyAlto.Fec_Suspencion_ITSE_Subsanacion != null || procAltoMuyAlto.Fec_Suspencion_ITSE != null)
                                {
                                    //tiempoaltomuyalto2 = procAltoMuyAlto.Fec_Notificacion_Licencia_Funcionamiento.Value.Date - procAltoMuyAlto.Fec_Ing_Sol.Value.Date;
                                    //diasaltomuyalto2 = tiempoaltomuyalto2.Days;
                                    if (procAltoMuyAlto.Fec_Suspencion_ITSE_Subsanacion == null)
                                    {
                                        Ini = procAltoMuyAlto.Fec_Ing_Sol.Value.Date;
                                    }
                                    else
                                    {
                                        Ini = procAltoMuyAlto.Fec_Suspencion_ITSE_Subsanacion.Value.Date;
                                    }


                                    if (procAltoMuyAlto.Fec_Notificacion_Licencia_Funcionamiento != null)
                                    {
                                        Fin = procAltoMuyAlto.Fec_Notificacion_Licencia_Funcionamiento.Value.Date;
                                        while (Ini != Fin)
                                        {
                                            //if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                                            //    diasaltomuyalto2 = diasaltomuyalto2 + 1;
                                            //Ini = Ini.AddDays(1);


                                            if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                                            {

                                                diasaltomuyalto2 = diasaltomuyalto2 + 1;
                                                foreach (var fecha in listaDiasFestivos)
                                                {
                                                    if (Ini == fecha)
                                                    {
                                                        diasaltomuyalto2 = diasaltomuyalto2 - 1;
                                                    }
                                                }
                                            }
                                            Ini = Ini.AddDays(1);

                                        }


                                        if (procAltoMuyAlto.Fec_Suspencion_ITSE_Subsanacion == null)
                                        {

                                            if (procAltoMuyAlto.Fec_Suspencion_ITSE == null)
                                            {
                                                if (diasaltomuyalto2 <= 9)
                                                {
                                                    if (procAltoMuyAlto.NivelRiesgo == 3)
                                                    {
                                                        cantidadAlto = cantidadAlto + 1;
                                                    }
                                                    else if (procAltoMuyAlto.NivelRiesgo == 4)
                                                    {
                                                        cantidadMuyAlto = cantidadMuyAlto + 1;
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                if (diasaltomuyalto2 <= 9)
                                                {
                                                    if (procAltoMuyAlto.NivelRiesgo == 3)
                                                    {
                                                        cantidadAlto = cantidadAlto + 1;
                                                    }
                                                    else if (procAltoMuyAlto.NivelRiesgo == 4)
                                                    {
                                                        cantidadMuyAlto = cantidadMuyAlto + 1;
                                                    }
                                                }

                                            }


                                        }
                                        else
                                        {
                                            if (procAltoMuyAlto.Fec_Suspencion_ITSE == null)
                                            {
                                                if (diasaltomuyalto2 <= 22)
                                                {
                                                    if (procAltoMuyAlto.NivelRiesgo == 3)
                                                    {
                                                        cantidadAlto = cantidadAlto + 1;
                                                    }
                                                    else if (procAltoMuyAlto.NivelRiesgo == 4)
                                                    {
                                                        cantidadMuyAlto = cantidadMuyAlto + 1;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (diasaltomuyalto2 <= 22)
                                                {
                                                    if (procAltoMuyAlto.NivelRiesgo == 3)
                                                    {
                                                        cantidadAlto = cantidadAlto + 1;
                                                    }
                                                    else if (procAltoMuyAlto.NivelRiesgo == 4)
                                                    {
                                                        cantidadMuyAlto = cantidadMuyAlto + 1;
                                                    }
                                                }

                                            }

                                        }

                                    }
                                }

                                if (procAltoMuyAlto.NivelRiesgo == 3)
                                {
                                    numeroAlto = numeroAlto + 1;
                                }
                                else if (procAltoMuyAlto.NivelRiesgo == 4)
                                {
                                    numeroMuyAlto = numeroMuyAlto + 1;
                                }
                            }
                            else
                            {
                                if (procAltoMuyAlto.NivelRiesgo == 3)
                                {
                                    numeroAlto = numeroAlto + 1;
                                }
                                else if (procAltoMuyAlto.NivelRiesgo == 4)
                                {
                                    numeroMuyAlto = numeroMuyAlto + 1;
                                }
                            }
                        }
                    }

                    double multiple = 0.5;
                    double valor1 = 0;
                    double valor2 = 0;
                    decimal Meta_Total = 0;
                    objIncentivosFormulasCorte.Numero_Licencias_Alto = numeroAlto;
                    objIncentivosFormulasCorte.Numero_Licencias_MuyAlto = numeroMuyAlto;

                    objIncentivosFormulasCorte.Licencias_Emitida_Alto = cantidadAlto;
                    objIncentivosFormulasCorte.Licencias_Emitida_MuyAlto = cantidadMuyAlto;

                    if (objIncentivosFormulasCorte.Numero_Licencias_Alto == 0 && objIncentivosFormulasCorte.Numero_Licencias_MuyAlto == 0)
                    {
                        objIncentivosFormulasCorte.Total_Alto_MuyAlto = 0;
                        valor2 = 0;
                    }
                    else
                    {
                        objIncentivosFormulasCorte.Total_Alto_MuyAlto = (objIncentivosFormulasCorte.Licencias_Emitida_Alto + objIncentivosFormulasCorte.Licencias_Emitida_MuyAlto) / (objIncentivosFormulasCorte.Numero_Licencias_Alto + objIncentivosFormulasCorte.Numero_Licencias_MuyAlto);
                        valor2 = ((double)objIncentivosFormulasCorte.Total_Alto_MuyAlto * multiple);
                    }

                    if (objIncentivosFormulasCorte.Numero_Licencias_Bajo == 0 && objIncentivosFormulasCorte.Numero_Licencias_medio == 0)
                    {
                        objIncentivosFormulasCorte.Total_Bajo_Medio = 0;
                        valor1 = 0;
                    }
                    else
                    {
                        objIncentivosFormulasCorte.Total_Bajo_Medio = (objIncentivosFormulasCorte.Licencias_Notificaciones_Bajo + objIncentivosFormulasCorte.Licencias_Notificaciones_Medio) / (objIncentivosFormulasCorte.Numero_Licencias_Bajo + objIncentivosFormulasCorte.Numero_Licencias_medio);
                        valor1 = ((double)objIncentivosFormulasCorte.Total_Bajo_Medio * multiple);
                    }


                    //objIncentivosFormulasCorte.Total_Bajo_Medio = (objIncentivosFormulasCorte.Licencias_Notificaciones_Bajo + objIncentivosFormulasCorte.Licencias_Notificaciones_Medio)/(objIncentivosFormulasCorte.Numero_Licencias_Bajo + objIncentivosFormulasCorte.Numero_Licencias_medio);
                    //objIncentivosFormulasCorte.Total_Alto_MuyAlto = (objIncentivosFormulasCorte.Licencias_Emitida_Alto + objIncentivosFormulasCorte.Licencias_Emitida_MuyAlto) /(objIncentivosFormulasCorte.Numero_Licencias_Alto + objIncentivosFormulasCorte.Numero_Licencias_MuyAlto);

                    //double valor1 = ((double)objIncentivosFormulasCorte.Total_Bajo_Medio * multiple);
                    //double valor2 = ((double)objIncentivosFormulasCorte.Total_Alto_MuyAlto * multiple);
                    Meta_Total = Convert.ToDecimal(valor1 + valor2) * 100;
                    objIncentivosFormulasCorte.Meta_Total = Convert.ToDecimal(valor1 + valor2) * 100;


                    objIncentivosFormulasCorte.UserCreacion = user.NroDocumento;
                    objIncentivosFormulasCorte.UserModificacion = user.NroDocumento;
                    objIncentivosFormulasCorte.FecCreacion = DateTime.Now;
                    objIncentivosFormulasCorte.FecModificacion = DateTime.Now;
                    objIncentivosFormulasCorte.Estado = 1;
                    objIncentivosFormulasCorte.EntidadId = EntidadId;

                    _IncentivosFormulasCorteService.Save(objIncentivosFormulasCorte);




                    /*fin del calculo de proceso*/


                    /*auditoria agregar*/
                    objauditoria.EntidadId = user.EntidadId;
                    objauditoria.SectorId = user.Sector;
                    objauditoria.ProvinciaId = user.Provincia;
                    objauditoria.Usuario = user.NombreCompleto;
                    objauditoria.Actividad = "Procesar IncentivosCorte";
                    objauditoria.Pantalla = "IncentivosCorte Formulas";
                    objauditoria.UserCreacion = user.NroDocumento;
                    objauditoria.FecCreacion = DateTime.Now;
                    /*auditoria*/

                    /*auditoria Grabar*/
                    _AuditoriaService.Save(objauditoria);
                    /*auditoria Grabar*/

                    return Json(new
                    {
                        mensaje = mensaje,
                        numerobajo = numerobajo,
                        numeromedio = numeromedio,
                        cantidadbajo = cantidadbajo,
                        cantidadmedio = cantidadmedio,
                        valor1 = Math.Truncate((valor1) * 100) / 100,
                        valor2 = Math.Truncate((valor2) * 100) / 100,
                        Meta_Total = Math.Truncate((Meta_Total) * 100) / 100,
                        numeroAlto = numeroAlto,
                        numeroMuyAlto = numeroMuyAlto,
                        cantidadAlto = cantidadAlto,
                        cantidadMuyAlto = cantidadMuyAlto,
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
                return PartialView("_Error");
            }
        }


        [HttpPost]
        public ActionResult Guardar(IncentivosCorte model, UsuarioInfo user)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    string mensaje = model.IncentivosCorteId == 0 ? "Se registraron los datos correctamente."
                                                : "Se modificaron los datos correctamente.";

                    Auditoria objauditoria = new Auditoria();
                    IncentivosCorte obj;

                    if (model.IncentivosCorteId == 0)
                    {
                        obj = new IncentivosCorte();

                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;



                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar IncentivosCorte";
                        objauditoria.Pantalla = "IncentivosCorte";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/


                    }
                    else
                    {
                        obj = _IncentivosCorteService.GetByone(model.IncentivosCorteId);

                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar IncentivosCorte";
                        objauditoria.Pantalla = "IncentivosCorte";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/

                        obj.UserModificacion = user.NroDocumento;
                        obj.FecModificacion = DateTime.Now;
                    }


                    obj.Eva_Fec_doc = model.Eva_Fec_doc;
                    obj.Eva_Doc_corresponde = model.Eva_Doc_corresponde;
                    obj.Eva_Comentario = model.Eva_Comentario;
                    obj.Eva_Observacion = model.Eva_Observacion;
                    obj.Eva_Resultado = model.Eva_Resultado;

                    _IncentivosCorteService.Save(obj);

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
                ModelState.AddModelError("", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }


        [HttpPost]
        public ActionResult Guardar2(IncentivosCorte model, UsuarioInfo user)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    List<IncentivosCorte> listamodel = _IncentivosCorteService.listaByIncentivosCorte();

                    foreach (var fecha in listamodel)
                    {

                        model = _IncentivosCorteService.GetByone(fecha.IncentivosCorteId);


                    }





                    Auditoria objauditoria = new Auditoria();
                    IncentivosCorte obj;


                    obj = _IncentivosCorteService.GetByone(model.IncentivosCorteId);

                    obj.NumExpediente = model.NumExpediente;
                    obj.Fec_Ing_Sol = model.Fec_Ing_Sol;
                    obj.NivelRiesgo = model.NivelRiesgo;
                    obj.Fec_Emision_Cert_ITSE = model.Fec_Emision_Cert_ITSE;
                    obj.Fec_Notificacion_Cert_ITSE = model.Fec_Notificacion_Cert_ITSE;
                    obj.Fec_Emision_Licencia_Funcionamiento = model.Fec_Emision_Licencia_Funcionamiento;
                    obj.Fec_Notificacion_Licencia_Funcionamiento = model.Fec_Notificacion_Licencia_Funcionamiento;
                    obj.Fec_Revocacion_Licencia_Funcionamiento = model.Fec_Revocacion_Licencia_Funcionamiento;
                    obj.Cod_Acreditacion_Inspector_ITSE = model.Cod_Acreditacion_Inspector_ITSE;

                    obj.Archivo_Fec_Suspencion_ITSE_Subsanacion = model.Archivo_Fec_Suspencion_ITSE_Subsanacion;
                    obj.Archivo_Fec_Suspencion_ITSE = model.Archivo_Fec_Suspencion_ITSE;
                    obj.Archivo_Fec_Resolucion_denegatoria = model.Archivo_Fec_Resolucion_denegatoria;
                    obj.Fec_Suspencion_ITSE_Subsanacion = model.Fec_Suspencion_ITSE_Subsanacion;
                    obj.Fec_Suspencion_ITSE = model.Fec_Suspencion_ITSE;
                    obj.Fec_Resolucion_denegatoria = model.Fec_Resolucion_denegatoria;
                    obj.Numero_Resolucion_Revocatoria = model.Numero_Resolucion_Revocatoria;
                    //obj.Renovacion_ITSE = model.Renovacion_ITSE;

                    obj.Archivo_Fec_Ing_Sol = model.Archivo_Fec_Ing_Sol;
                    obj.Archivo_Fec_Emision_Cert_ITSE = model.Archivo_Fec_Emision_Cert_ITSE;
                    obj.Archivo_Fec_Emision_Licencia_Funcionamiento = model.Archivo_Fec_Emision_Licencia_Funcionamiento;
                    obj.Archivo_Fec_Notificacion_Cert_ITSE = model.Archivo_Fec_Notificacion_Cert_ITSE;
                    obj.Archivo_Fec_Notificacion_Licencia_Funcionamiento = model.Archivo_Fec_Notificacion_Licencia_Funcionamiento;
                    obj.Archivo_Fec_Revocacion_Licencia_Funcionamiento = model.Archivo_Fec_Revocacion_Licencia_Funcionamiento;


                    obj.NumRUC = model.NumRUC;
                    obj.Actividadogironegocio = model.Actividadogironegocio;
                    obj.Girocomplementario = model.Girocomplementario;


                    obj.Funcion = model.Funcion;
                    obj.Numeral = model.Numeral;
                    obj.NivelInicial = model.NivelInicial;
                    obj.NivelFinal = model.NivelFinal;


                    obj.Numero_Revocacion_Licencia_Funcionamiento = model.Numero_Revocacion_Licencia_Funcionamiento;

                    obj.Estado = model.Estado;
                    obj.EntidadId = user.EntidadId;





                    int mesbajomedio;
                    //TimeSpan tiempobajomedio1;


                    int mesaltomuyalto;
                    //TimeSpan tiempoaltomuyalto1;


                    DateTime Ini;
                    DateTime Fin;
                    //Inicio de lista FECHAS DE FERIADOS


                    List<DateTime> listaDiasFestivos = new List<DateTime>();
                    listaDiasFestivos.Add(Convert.ToDateTime("01/04/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("02/04/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("01/05/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("29/06/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("28/07/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("29/07/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("30/08/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("08/10/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("11/10/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("01/11/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("02/11/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("08/12/2021"));
                    listaDiasFestivos.Add(Convert.ToDateTime("25/12/2021"));
                    //Fin de lista FECHAS DE FERIADOS

                    if (model.NivelRiesgo == 1 || model.NivelRiesgo == 2)
                    {
                        int diasbajomedio1 = 0;
                        int diasbajomedio2 = 0;

                        mesbajomedio = model.Fec_Ing_Sol.Value.Month;
                        if (mesbajomedio == 3 || mesbajomedio == 4 || mesbajomedio == 5 || mesbajomedio == 6 || mesbajomedio == 8 || mesbajomedio == 9 || mesbajomedio == 10 || mesbajomedio == 11 || mesbajomedio == 12)
                        {
                            //tiempobajomedio1 = model.Fec_Notificacion_Licencia_Funcionamiento.Value.Date - model.Fec_Ing_Sol.Value.Date;
                            //diasbajomedio1 = tiempobajomedio1.Days;



                            if (model.Fec_Notificacion_Licencia_Funcionamiento != null)
                            {
                                Ini = model.Fec_Ing_Sol.Value.Date;
                                Fin = model.Fec_Notificacion_Licencia_Funcionamiento.Value.Date;

                                while (Ini != Fin)
                                {
                                    //if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                                    //    diasbajomedio1 = diasbajomedio1 + 1;
                                    //Ini = Ini.AddDays(1);

                                    if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                                    {

                                        diasbajomedio1 = diasbajomedio1 + 1;
                                        foreach (var fecha in listaDiasFestivos)
                                        {
                                            if (Ini == fecha)
                                            {
                                                diasbajomedio1 = diasbajomedio1 - 1;
                                            }
                                        }
                                    }
                                    Ini = Ini.AddDays(1);

                                }


                                if (diasbajomedio1 <= 2 || model.Fec_Suspencion_ITSE_Subsanacion != null || model.Fec_Suspencion_ITSE != null)
                                {
                                    //tiempobajomedio2 = model.Fec_Notificacion_Cert_ITSE.Value.Date - model.Fec_Ing_Sol.Value.Date;
                                    //diasbajomedio2 = tiempobajomedio2.Days;

                                    if (model.Fec_Suspencion_ITSE_Subsanacion == null)
                                    {
                                        Ini = model.Fec_Ing_Sol.Value.Date;
                                    }
                                    else
                                    {
                                        Ini = model.Fec_Suspencion_ITSE_Subsanacion.Value.Date;
                                    }


                                    if (model.Fec_Notificacion_Cert_ITSE != null)
                                    {
                                        Fin = model.Fec_Notificacion_Cert_ITSE.Value.Date;
                                        while (Ini != Fin)
                                        {
                                            //if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                                            //    diasbajomedio2 = diasbajomedio2 + 1;
                                            //Ini = Ini.AddDays(1);

                                            if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                                            {

                                                diasbajomedio2 = diasbajomedio2 + 1;
                                                foreach (var fecha in listaDiasFestivos)
                                                {
                                                    if (Ini == fecha)
                                                    {
                                                        diasbajomedio2 = diasbajomedio2 - 1;
                                                    }
                                                }
                                            }
                                            Ini = Ini.AddDays(1);
                                        }

                                        if (model.Fec_Suspencion_ITSE_Subsanacion == null)
                                        {
                                            if (model.Fec_Suspencion_ITSE != null)
                                            {
                                                if (diasbajomedio2 <= 9)
                                                {
                                                    obj.EstadoIngreso = 1;
                                                }
                                                else
                                                {
                                                    obj.EstadoIngreso = 0;
                                                }
                                            }
                                            else
                                            {
                                                if (diasbajomedio2 <= 9)
                                                {
                                                    obj.EstadoIngreso = 1;
                                                }
                                                else
                                                {
                                                    obj.EstadoIngreso = 0;
                                                }
                                            }


                                        }
                                        else
                                        {

                                            if (model.Fec_Suspencion_ITSE != null)
                                            {

                                                if (diasbajomedio2 <= 5)
                                                {
                                                    obj.EstadoIngreso = 1;
                                                }
                                                else
                                                {
                                                    obj.EstadoIngreso = 0;
                                                }
                                            }
                                            else
                                            {
                                                if (diasbajomedio2 <= 5)
                                                {
                                                    obj.EstadoIngreso = 1;
                                                }
                                                else
                                                {
                                                    obj.EstadoIngreso = 0;
                                                }

                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    obj.EstadoIngreso = 0;
                                }
                            }
                        }
                        else
                        {
                            obj.EstadoIngreso = 0;
                        }

                    }
                    else if (model.NivelRiesgo == 3 || model.NivelRiesgo == 4)
                    {

                        int diasaltomuyalto1 = 0;
                        int diasaltomuyalto2 = 0;

                        mesaltomuyalto = model.Fec_Ing_Sol.Value.Month;

                        if (mesaltomuyalto == 3 || mesaltomuyalto == 4 || mesaltomuyalto == 5 || mesaltomuyalto == 6 || mesaltomuyalto == 8 || mesaltomuyalto == 9 || mesaltomuyalto == 10 || mesaltomuyalto == 11 || mesaltomuyalto == 12)
                        {



                            //tiempoaltomuyalto1 = model.Fec_Emision_Cert_ITSE.Value.Date - model.Fec_Ing_Sol.Value.Date;
                            //diasaltomuyalto1 = tiempoaltomuyalto1.Days;



                            if (model.Fec_Emision_Cert_ITSE != null)
                            {
                                Ini = model.Fec_Ing_Sol.Value.Date;

                                Fin = model.Fec_Emision_Cert_ITSE.Value.Date;


                                while (Ini != Fin)
                                {
                                    //if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                                    //    diasaltomuyalto1 = diasaltomuyalto1 + 1;
                                    //Ini = Ini.AddDays(1);
                                    if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                                    {

                                        diasaltomuyalto1 = diasaltomuyalto1 + 1;
                                        foreach (var fecha in listaDiasFestivos)
                                        {
                                            if (Ini == fecha)
                                            {
                                                diasaltomuyalto1 = diasaltomuyalto1 - 1;
                                            }
                                        }
                                    }
                                    Ini = Ini.AddDays(1);
                                }

                                if (diasaltomuyalto1 <= 7 || model.Fec_Suspencion_ITSE_Subsanacion != null || model.Fec_Suspencion_ITSE != null)
                                {
                                    //tiempoaltomuyalto2 = model.Fec_Notificacion_Licencia_Funcionamiento.Value.Date - model.Fec_Ing_Sol.Value.Date;
                                    //diasaltomuyalto2 = tiempoaltomuyalto2.Days;

                                    if (model.Fec_Suspencion_ITSE_Subsanacion == null)
                                    {
                                        Ini = model.Fec_Ing_Sol.Value.Date;
                                    }
                                    else
                                    {
                                        Ini = model.Fec_Suspencion_ITSE_Subsanacion.Value.Date;
                                    }


                                    if (model.Fec_Notificacion_Licencia_Funcionamiento != null)
                                    {
                                        Fin = model.Fec_Notificacion_Licencia_Funcionamiento.Value.Date;
                                        while (Ini != Fin)
                                        {
                                            //if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                                            //    diasaltomuyalto2 = diasaltomuyalto2 + 1;
                                            //Ini = Ini.AddDays(1);
                                            if (!(Ini.DayOfWeek == DayOfWeek.Sunday | Ini.DayOfWeek == DayOfWeek.Saturday))
                                            {

                                                diasaltomuyalto2 = diasaltomuyalto2 + 1;
                                                foreach (var fecha in listaDiasFestivos)
                                                {
                                                    if (Ini == fecha)
                                                    {
                                                        diasaltomuyalto2 = diasaltomuyalto2 - 1;
                                                    }
                                                }
                                            }
                                            Ini = Ini.AddDays(1);

                                        }

                                        if (model.Fec_Suspencion_ITSE_Subsanacion == null)
                                        {

                                            if (model.Fec_Suspencion_ITSE == null)
                                            {
                                                if (diasaltomuyalto2 <= 9)
                                                {
                                                    obj.EstadoIngreso = 1;
                                                }
                                                else
                                                {
                                                    obj.EstadoIngreso = 0;
                                                }

                                            }
                                            else
                                            {
                                                if (diasaltomuyalto2 <= 9)
                                                {
                                                    obj.EstadoIngreso = 1;
                                                }
                                                else
                                                {
                                                    obj.EstadoIngreso = 0;
                                                }

                                            }



                                        }
                                        else
                                        {

                                            if (model.Fec_Suspencion_ITSE == null)
                                            {
                                                if (diasaltomuyalto2 <= 22)
                                                {
                                                    obj.EstadoIngreso = 1;
                                                }
                                                else
                                                {
                                                    obj.EstadoIngreso = 0;
                                                }
                                            }
                                            else
                                            {
                                                if (diasaltomuyalto2 <= 22)
                                                {
                                                    obj.EstadoIngreso = 1;
                                                }
                                                else
                                                {
                                                    obj.EstadoIngreso = 0;
                                                }

                                            }

                                        }


                                    }
                                }
                                else
                                {
                                    obj.EstadoIngreso = 0;
                                }
                            }
                        }
                        else
                        {
                            obj.EstadoIngreso = 0;
                        }

                    }

                    _IncentivosCorteService.Save(obj);


                    return Json(new
                    {
                        mensaje = 1
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


    }


}
