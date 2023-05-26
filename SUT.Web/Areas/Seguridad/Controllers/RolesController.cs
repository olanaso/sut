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
    public class RolesController : Controller
    {
        private readonly ILogService<RolesController> _log;
        private readonly IEntidadService _entidadService;
        private readonly IMiembroEquipoService _miembroEquipoService;
        private readonly IMetaDatoService _metaDatoService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly IRolesService _RolesService;
        private readonly IRolMenuService _RolMenuService;
        private readonly IMenuService _MenuService;
        Auditoria objauditoria = new Auditoria();
        public RolesController(IEntidadService entidadService,
                                IMiembroEquipoService miembroEquipoService,
                                IAuditoriaService AuditoriaService,
                                IMetaDatoService metaDatoService,
                                IRolesService rolesService,
                                IRolMenuService rolMenuService,
                                IMenuService menuService)
        {
            _log = new LogService<RolesController>();
            _entidadService = entidadService;
            _miembroEquipoService = miembroEquipoService;
            _metaDatoService = metaDatoService;
            _AuditoriaService = AuditoriaService;
            _RolesService = rolesService;
            _RolMenuService = rolMenuService;
            _MenuService = menuService;
        }

        [AutorizacionRol(Roles = "Administrador,Coordinador")]
        public ActionResult Lista(UsuarioInfo user)
        {
            try
            {
                ViewBag.Usuario = user;

                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                _log.Error(ex);
                return View("_Error");
            }
        }


        public ActionResult GetAllLikePagin(Roles filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            try
            {

                int totalRows = 0;
                List<Roles> lista = _RolesService.GetByRoles(filtro, pageIndex, pageSize, ref totalRows);

                return Json(new
                {
                    lista = lista.Select(x => new
                    {
                        RolId = x.RolId,
                        Descripcion = x.Descripcion,
                        Estado = x.Estado,
                    }),
                    totalRows = totalRows
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

        [AutorizacionRol(Roles = "Administrador,Coordinador")]
        public ActionResult Editar(long id, UsuarioInfo user)
        {
            try
            {
                ViewBag.User = user;

                Roles item;
                if (id == 0)
                {
                    item = new Roles();
                    ViewBag.Title = "Nuevo Rol";

                }
                else
                {
                    item = _RolesService.GetByone(id);
                    ViewBag.Title = "Editar Roles";
                }
                ViewBag.publicar = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Activo", Value = "1" , Selected = item.Estado == 1 ? true:false},
                    new SelectListItem() { Text = "Baja", Value = "2",  Selected = item.Estado == 0? true:false },
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


        public ActionResult EliminarRol(long id, UsuarioInfo user)
        {

            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Eliminar Rol";
                objauditoria.Pantalla = "Roles";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/

                _RolMenuService.Eliminar(id);
                _RolesService.Eliminar(id);
                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
                return Json(new
                {
                    mensaje = "El registro fue eliminado satisfactoriamente."
                });
                //return PartialView("_Select");

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                string msj = "El recurso se encuentra en uso.";
                ModelState.AddModelError("", msj.ToString());


                _log.Error(ex);
                return PartialView("_Error");
            }

        }

        public ActionResult ListaMenuConfEditar(long id, UsuarioInfo user)
        {
            try
            {

                ViewBag.Usuario = user;
                Roles model = _RolesService.GetByone(id);

                string viewName = string.Empty;

                viewName = "ListaMenuConf";


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



        public ActionResult ListaMenuConf(long idrol, int pageIndex, int pageSize, UsuarioInfo user)
        {
            try
            {

                int totalRows = 0;
                List<Menu> lista = _MenuService.GetByMenuConfig(1, pageIndex, pageSize, ref totalRows);


                List<RolMenu> listarolmenu = _RolMenuService.GetByRolMenu(idrol);


                for (int i = 0; i < listarolmenu.Count(); i++)
                {
                    lista[i].Ver = listarolmenu[i].Ver;
                    lista[i].Nuevo = listarolmenu[i].Nuevo;
                    lista[i].Editar = listarolmenu[i].Editar;
                    lista[i].Guardar = listarolmenu[i].Guardar;
                    lista[i].Eliminar = listarolmenu[i].Eliminar;
                    lista[i].Descargar = listarolmenu[i].Descargar;
                    lista[i].Agregar = listarolmenu[i].Agregar;
                    lista[i].Copiar = listarolmenu[i].Copiar;
                    lista[i].Terminar = listarolmenu[i].Terminar;
                    lista[i].Importar = listarolmenu[i].Importar;
                    lista[i].Procesar = listarolmenu[i].Procesar;
                    lista[i].Anular = listarolmenu[i].Anular;
                    lista[i].Activar = listarolmenu[i].Activar;
                    lista[i].Generar = listarolmenu[i].Generar;
                    lista[i].Reemplazar = listarolmenu[i].Reemplazar;
                    lista[i].Presentar = listarolmenu[i].Presentar;
                    lista[i].Sustentar = listarolmenu[i].Sustentar;
                    lista[i].Observar = listarolmenu[i].Observar;
                    lista[i].Publicar = listarolmenu[i].Publicar;
                }


                return Json(new
                {
                    lista = lista.Select(x => new
                    {

                        MenuId = x.MenuId,
                        Descripcion = x.Descripcion,
                        IdPadreMenu = x.IdPadreMenu,
                        Ver_Ac = x.Ver_Ac,
                        Nuevo_Ac = x.Nuevo_Ac,
                        Editar_Ac = x.Editar_Ac,
                        Guardar_Ac = x.Guardar_Ac,
                        Eliminar_Ac = x.Eliminar_Ac,
                        Descargar_Ac = x.Descargar_Ac,
                        Agregar_Ac = x.Agregar_Ac,
                        Copiar_Ac = x.Copiar_Ac,
                        Terminar_Ac = x.Terminar_Ac,
                        Importar_Ac = x.Importar_Ac,
                        Procesar_Ac = x.Procesar_Ac,
                        Anular_Ac = x.Anular_Ac,
                        Activar_Ac = x.Activar_Ac,
                        Generar_Ac = x.Generar_Ac,
                        Reemplazar_Ac = x.Reemplazar_Ac,
                        Presentar_Ac = x.Presentar_Ac,
                        Sustentar_Ac = x.Sustentar_Ac,
                        Observar_Ac = x.Observar_Ac,
                        Publicar_Ac = x.Publicar_Ac,
                        Ver = x.Ver,
                        Nuevo = x.Nuevo,
                        Editar = x.Editar,
                        Guardar = x.Guardar,
                        Eliminar = x.Eliminar,
                        Descargar = x.Descargar,
                        Agregar = x.Agregar,
                        Copiar = x.Copiar,
                        Terminar = x.Terminar,
                        Importar = x.Importar,
                        Procesar = x.Procesar,
                        Anular = x.Anular,
                        Activar = x.Activar,
                        Generar = x.Generar,
                        Reemplazar = x.Reemplazar,
                        Presentar = x.Presentar,
                        Sustentar = x.Sustentar,
                        Observar = x.Observar,
                        Publicar = x.Publicar,
                    }),
                    totalRows = totalRows
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

        [HttpPost]
        public ActionResult GuardarrolesMenu(List<RolMenu> model, UsuarioInfo user)
        {
            try
            {

                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Agregar menu a los roles";
                objauditoria.Pantalla = "Roles";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                ///*auditoria*/
                ///


                _RolMenuService.Eliminar(model[0].RolId);

                RolMenu obj = new RolMenu();
                foreach (var asme in model)
                {

                    obj.MenuId = asme.MenuId;
                    obj.RolId = asme.RolId;
                    obj.Ver = asme.Ver;
                    obj.Nuevo = asme.Nuevo;
                    obj.Editar = asme.Editar;
                    obj.Guardar = asme.Guardar;
                    obj.Eliminar = asme.Eliminar;
                    obj.Descargar = asme.Descargar;
                    obj.Agregar = asme.Agregar;
                    obj.Copiar = asme.Copiar;
                    obj.Terminar = asme.Terminar;
                    obj.Importar = asme.Importar;
                    obj.Procesar = asme.Procesar;
                    obj.Anular = asme.Anular;
                    obj.Activar = asme.Activar;
                    obj.Generar = asme.Generar;
                    obj.Reemplazar = asme.Reemplazar;
                    obj.Presentar = asme.Presentar;
                    obj.Sustentar = asme.Sustentar;
                    obj.Observar = asme.Observar;
                    obj.Publicar = asme.Publicar;

                    _RolMenuService.Save(obj);
                }


                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
                return Json(new
                {
                    valid = true,
                    mensaje = "La configuración fue asignado satisfactoriamente."
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



        [AutorizacionRol(Roles = "Administrador,Coordinador")]
        public ActionResult Menu(long RolId, UsuarioInfo user)
        {
            try
            {
                ViewBag.User = user;

                List<RolMenu> item;
                if (RolId == 0)
                {
                    item = new List<RolMenu>();
                    ViewBag.Title = "Asignar Roles ";

                }
                else
                {
                    item = _RolMenuService.GetByRolMenu(RolId);
                    ViewBag.Title = "Editar Roles";
                }



                return PartialView("_AsignarRol", item);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        public string GetAllListaMenuRol(int RolId, UsuarioInfo user)
        {


            //var categorias = _datoService.GetByTipo(TipoDato.CategoriaProcedimiento);

            int totalRows = 1;


            //List<Dato> lista = new List<Dato>();



            List<Menu> lista1 = _MenuService.GetByMenu();


            List<RolMenu> lista2 = _RolMenuService.GetByRolMenu(RolId);

            //lista = _datoService.GetByTipo(TipoDato.CategoriaProcedimiento);


            return JsonConvert.SerializeObject(new
            {
                lista = lista1.Select(x => new
                {
                    MenuId = x.MenuId,
                    Descripcion = x.Descripcion,
                    IdPadreMenu = x.IdPadreMenu,
                }),
                lista2 = lista2.Select(x => new
                {
                    MenuId = x.MenuId
                }),
                totalRows = totalRows
            });
        }



        [HttpPost]
        [AutorizacionRol(Roles = "Administrador,Coordinador")]
        public ActionResult Guardar(Roles model, UsuarioInfo user)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    string mensaje = model.RolId == 0 ? "El Rol de Equipo fue registrado satisfactoriamente."
                                                : "El Rol de Equipo fue modificado satisfactoriamente.";

                    Auditoria objauditoria = new Auditoria();
                    Roles obj;

                    if (model.RolId == 0)
                    {
                        obj = new Roles();
                        obj.Descripcion = model.Descripcion;

                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;



                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Roles";
                        objauditoria.Pantalla = "Roles";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/

                    }
                    else
                    {
                        obj = _RolesService.GetByone(model.RolId);

                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Roles";
                        objauditoria.Pantalla = "Roles";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/

                    }

                    obj.Descripcion = model.Descripcion;
                    obj.Estado = model.Estado;


                    obj.UserModificacion = user.NroDocumento;
                    obj.FecModificacion = DateTime.Now;
                    _RolesService.Save(obj);

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

    }
}