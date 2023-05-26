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
    public class MiembroController : Controller
    {
        private readonly ILogService<MiembroController> _log;
        private readonly IEntidadService _entidadService;
        private readonly IMiembroEquipoService _miembroEquipoService;
        private readonly IMetaDatoService _metaDatoService;
        private readonly IResolucionEquipoService _resolucionEquipoService;
        private readonly IPlanTrabajoService _planTrabajoService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly IMenuayudaService _menuayudaService;
        private readonly IMenuService _menuService;
        private readonly IDepartamentoService _departamentoService;
        private readonly IProvinciaService _provinciaService;
        private readonly IRolMenuService _rolMenuService;

        public MiembroController(IEntidadService entidadService,
                                IMiembroEquipoService miembroEquipoService,
                                IResolucionEquipoService resolucionEquipoService,
                                IPlanTrabajoService planTrabajoService,
                                IAuditoriaService AuditoriaService,
                                IMetaDatoService metaDatoService,
                                IMenuayudaService menuayudaService,
                                IMenuService menuService,
                                IDepartamentoService departamentoService,
                                IProvinciaService provinciaService,
                                IRolMenuService rolMenuService)
        {
            _log = new LogService<MiembroController>();
            _entidadService = entidadService;
            _miembroEquipoService = miembroEquipoService;
            _metaDatoService = metaDatoService;
            _planTrabajoService = planTrabajoService;
            _AuditoriaService = AuditoriaService;
            _resolucionEquipoService = resolucionEquipoService;
            _menuayudaService = menuayudaService;
            _menuService = menuService;
            _departamentoService = departamentoService;
            _provinciaService = provinciaService;
            _rolMenuService = rolMenuService;
        }

        [AutorizacionRol(Roles = "Administrador,Coordinador")]
        public ActionResult Lista(UsuarioInfo user)
        {
            try
            {


                /*Inicio Cargar roles - Acceso*/
                List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 2);
                user.rolmenu = listarolmenu.ToList();
                /*Fin Cargar roles  - Acceso*/


                ResolucionEquipo listaEntidad = _resolucionEquipoService.GetOneEntidad(user.EntidadId);
                if (listaEntidad != null)
                {
                    ViewBag.ResolucionEquipoId = listaEntidad.ArchivoAdjuntoId;
                }
                else
                {
                    ViewBag.ResolucionEquipoId = 0;
                }

                PlanTrabajo item = _planTrabajoService.GetOneEntidad(user.EntidadId);

                if (item != null)
                {
                    ViewBag.PlanTrabajoId = item.ArchivoAdjuntoId;
                }
                else
                {
                    ViewBag.PlanTrabajoId = 0;
                }
                ViewBag.Usuario = user;


                List<MetaDato> listMD = _metaDatoService.GetByParent(36);

                listMD.Insert(0, new MetaDato() { MetaDatoId = 0, Nombre = " - TODOS - " });
                ViewBag.RolEquipo = listMD.Select(x => new SelectListItem()
                {
                    Value = x.MetaDatoId.ToString(),
                    Text = x.Nombre
                })
                .ToList();

                ViewBag.publicarEstado = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = " - Todos - ", Value = "0" },
                    new SelectListItem() { Text = "Activo", Value = "1" },
                    new SelectListItem() { Text = "Baja", Value = "2" },
                };

                //ViewBag.Sexo = new List<SelectListItem>()
                //{
                //    new SelectListItem() { Text = " - Seleccionar - ", Value = "0" },
                //    new SelectListItem() { Text = "Masculino", Value = "1" },
                //    new SelectListItem() { Text = "Femenino", Value = "2" },
                //};

                List<MetaDato> listtipodoc = _metaDatoService.GetByParent(38);

                ViewBag.TipoDoc = listtipodoc.Select(x => new SelectListItem()
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



                //ViewBag.ListaDepartamento = JsonConvert.SerializeObject(listaDepartamento.Select(x => new SelectListItem() { Value = x.DepartamentoId.ToString(), Text = x.Nombre }).ToList());


                List<Provincia> listaProvincia = new List<Provincia>();
                listaProvincia = listaProvincia.OrderBy(x => x.Nombre).ToList();
                listaProvincia.Insert(0, new Provincia() { ProvinciaId = 0, Nombre = " - TODOS - " });
                ViewBag.ListaProvincia = listaProvincia.Select(x => new SelectListItem()
                {
                    Value = x.ProvinciaId.ToString(),
                    Text = x.Nombre
                })
              .ToList();

                //ViewBag.ListaProvincia = JsonConvert.SerializeObject(listaProvincia.Select(x => new SelectListItem() { Value = x.ProvinciaId.ToString(), Text = x.Nombre }).ToList());




                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                _log.Error(ex);
                return View("_Error");
            }
        }

        public ActionResult GetEntidades(long id)
        {
            try
            {
                //List<Provincia> listaProvincia = new List<Provincia>();
                //listaProvincia = _provinciaService.GetByDepartamento(id);
                //listaProvincia = listaProvincia.OrderBy(x => x.Nombre).ToList();
                //listaProvincia.Insert(0, new Provincia() { ProvinciaId = 0, Nombre = " - SELECCIONAR - " });
                List<Entidad> listaEntidad;

                if (id == 0)
                {
                    listaEntidad = _entidadService.GetAll()
                                                   .Where(x => x.Estado == Respuesta.Si && x.ProvinciaId == null)
                                                   .OrderBy(x => x.Nombre)
                                                   .ToList();
                }
                else
                {

                    listaEntidad = _entidadService.GetAll()
                                             .Where(x => x.Estado == Respuesta.Si && x.ProvinciaId == id)
                                             .OrderBy(x => x.Nombre)
                                             .ToList();

                }
                listaEntidad.Insert(0, new Entidad()
                {
                    EntidadId = 0,
                    Nombre = " - SELECCIONAR - "
                });
                ViewBag.ListaEntidad = listaEntidad.Select(x => new SelectListItem()
                {
                    Value = x.EntidadId.ToString(),
                    Text = x.Nombre
                }).ToList();



                return Json(new { data = listaEntidad.Select(x => new { EntidadId = x.EntidadId, Nombre = x.Nombre }).ToList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                _log.Error(ex);
                return PartialView("_Error");
            }
        }

        public ActionResult GetProvincias(long id)
        {
            try
            {
                List<Provincia> listaProvincia = new List<Provincia>();
                listaProvincia = _provinciaService.GetByDepartamento(id);
                listaProvincia = listaProvincia.OrderBy(x => x.Nombre).ToList();
                listaProvincia.Insert(0, new Provincia() { ProvinciaId = 0, Nombre = " - SELECCIONAR - " });



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
        public ActionResult GetAllLikePagin(MiembroEquipo filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            try
            {
                if (user.Rol == (short)Rol.Coordinador)
                {
                    filtro.EntidadId = user.EntidadId;
                }

                int totalRows = 0;
                List<MiembroEquipo> lista = _miembroEquipoService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

                return Json(new
                {
                    lista = lista.Select(x => new
                    {
                        MiembroEquipoId = x.MiembroEquipoId,
                        NroDocumento = x.NroDocumento,
                        NombreCompleto = x.NombreCompleto,
                        EntidadId = x.EntidadId,
                        Entidad = x.Entidad != null ? x.Entidad.Nombre : "",
                        RolEquipo = x.RolEquipo == null ? "" : x.RolEquipo.Nombre,
                        Cargo = x.Cargo ?? "",
                        Correo = x.Correo ?? "",
                        TelefonoFijo = x.TelefonoFijo ?? "",
                        TelefonoMovil = x.TelefonoMovil ?? "",
                        FecCreacion = x.FecCreacion != null ? x.FecCreacion.Value.ToString("dd/MM/yyyy") : "",
                        FecModificacion = x.FecModificacion != null ? x.FecModificacion.Value.ToString("dd/MM/yyyy") : "",
                        ArchivoAdjuntoId = x.ArchivoAdjuntoId,
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

                /*Inicio Cargar roles - Acceso*/
                List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 2);
                user.rolmenu = listarolmenu.ToList();
                /*Fin Cargar roles  - Acceso*/


                ViewBag.User = user;


                List<Departamento> listaDepartamento = _departamentoService.GetAll();
                listaDepartamento = listaDepartamento.OrderBy(x => x.Nombre).ToList();
                listaDepartamento.Insert(0, new Departamento() { DepartamentoId = 0, Nombre = " - SELECCIONAR - " });
                ViewBag.ListaDepartamento = listaDepartamento.Select(x => new SelectListItem() { Value = x.DepartamentoId.ToString(), Text = x.Nombre }).ToList();


                List<Provincia> listaProvincia = new List<Provincia>();

                MiembroEquipo item;
                if (id == 0)
                {
                    item = new MiembroEquipo();
                    ViewBag.Title = "Nuevo Miembro de Equipo";

                    if (user.Rol == (short)Rol.Coordinador)
                    {
                        Entidad itementidad = _entidadService.GetOne(user.EntidadId);

                        if (itementidad.Provincia != null)
                        {
                            item.DepartamentoId = itementidad.Provincia.DepartamentoId;

                            listaProvincia = _provinciaService.GetByDepartamento(item.DepartamentoId);
                            listaProvincia = listaProvincia.OrderBy(x => x.Nombre).ToList();

                            item.ProvinciaId = itementidad.Provincia.ProvinciaId;
                            item.EntidadId = itementidad.EntidadId;
                            item.Entidad = itementidad;
                        }

                    }
                }
                else
                {
                    item = _miembroEquipoService.GetOne(id);
                    ViewBag.Title = "Editar Miembro de Equipo";


                    Entidad itementidad = _entidadService.GetOne(item.EntidadId);

                    if (itementidad.Provincia != null)
                    {
                        item.DepartamentoId = itementidad.Provincia.DepartamentoId;

                        listaProvincia = _provinciaService.GetByDepartamento(item.DepartamentoId);
                        listaProvincia = listaProvincia.OrderBy(x => x.Nombre).ToList();

                        item.ProvinciaId = itementidad.Provincia.ProvinciaId;

                    }

                }

                List<MetaDato> listMD = new List<MetaDato>();
                if (user.Rol == (short)Rol.Administrador)
                {
                    listMD = _metaDatoService.GetByParent(36);
                }
                else
                {
                    listMD = _metaDatoService.GetByParent(36).Where(x => x.MetaDatoId != 137 && x.MetaDatoId != 128 && x.MetaDatoId != 129 && x.MetaDatoId != 130).ToList();

                }

                List<MetaDato> listtipodoc = _metaDatoService.GetByParent(38);

                ViewBag.TipoDoc = listtipodoc.Select(x => new SelectListItem()
                {
                    Value = x.MetaDatoId.ToString(),
                    Text = x.Nombre
                })
                .ToList();

                listMD.Insert(0, new MetaDato() { MetaDatoId = 0, Nombre = " - SELECCIONAR - " });

                ViewBag.RolEquipo = listMD.Select(x => new SelectListItem()
                {
                    Value = x.MetaDatoId.ToString(),
                    Text = x.Nombre
                })
                .ToList();

                if (user.Rol == (short)Rol.Coordinador)
                {
                    ViewBag.RolEquipo.RemoveAt(1);
                    ViewBag.RolEquipo.RemoveAt(1);
                    ViewBag.RolEquipo.RemoveAt(7);
                }


                List<Entidad> listaEntidad;
                if (id == 0 && user.Rol == (short)Rol.Administrador)
                {

                    listaEntidad = _entidadService.GetAll()
                                              .Where(x => x.Estado == Respuesta.Si && x.ProvinciaId == null)
                                              .OrderBy(x => x.Nombre)
                                              .ToList();
                }
                else
                {
                    listaEntidad = _entidadService.GetAll()
                                            .Where(x => x.Estado == Respuesta.Si)
                                            .OrderBy(x => x.Nombre)
                                            .ToList();
                }

                listaEntidad.Insert(0, new Entidad()
                {
                    EntidadId = 0,
                    Nombre = " - SELECCIONAR - "
                });
                ViewBag.ListaEntidad = listaEntidad.Select(x => new SelectListItem()
                {
                    Value = x.EntidadId.ToString(),
                    Text = x.Nombre
                }).ToList();

                ViewBag.publicar = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Activo", Value = "1" , Selected = item.Estado == EstadoMiembroEquipo.Activo ? true:false},
                    new SelectListItem() { Text = "Baja", Value = "2",  Selected = item.Estado == EstadoMiembroEquipo.Baja ? true:false },
                };
                ViewBag.Genero = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = " - Seleccionar - ", Value = "0" },
                    new SelectListItem() { Text = "Masculino", Value = "1", Selected = item.Sexo == 1 ? true:false  },
                    new SelectListItem() { Text = "Femenino", Value = "2", Selected = item.Sexo == 2 ? true:false  },
                };



                listaProvincia.Insert(0, new Provincia() { ProvinciaId = 0, Nombre = " - SELECCIONAR - " });
                ViewBag.ListaProvincia = listaProvincia.Select(x => new SelectListItem() { Value = x.ProvinciaId.ToString(), Text = x.Nombre }).ToList();

                return PartialView("_Editar", item);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        public ActionResult Eliminar(long id, UsuarioInfo user)
        {
            try
            {
                Auditoria objauditoria = new Auditoria();
                _miembroEquipoService.Delete(id);

                /*auditoria actualizar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Eliminar Miembro";
                objauditoria.Pantalla = "Equipo de Trabajo";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/

                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);
                /*auditoria Grabar*/
                return Json(new { exito = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        public ActionResult GuardarMenu(int menuid, int ruta, UsuarioInfo user)
        {
            try
            {

                List<Menuayuda> lstmenuayuda = _menuayudaService.GetByMenuayuda(user.EntidadId, ruta, menuid);

                if (lstmenuayuda.Count == 0)
                {

                    Menuayuda menuayuda = new Menuayuda();
                    menuayuda.EntidadId = user.EntidadId;
                    menuayuda.UsuarioId = user.UsuarioId;
                    menuayuda.Rol = (Rol)user.Rol;
                    menuayuda.Estado = 1;
                    menuayuda.MenuId = menuid;
                    menuayuda.Ruta = ruta;
                    _menuayudaService.Save(menuayuda);

                    List<Menu> lstmenu = _menuService.GetByMenuidpadre(menuid + 1);
                    if (lstmenu.Count == 1)
                    {
                        menuayuda.EntidadId = user.EntidadId;
                        menuayuda.UsuarioId = user.UsuarioId;
                        menuayuda.Rol = (Rol)user.Rol;
                        menuayuda.Estado = 1;
                        menuayuda.MenuId = menuid + 1;
                        menuayuda.Ruta = ruta + 1;
                        _menuayudaService.Save(menuayuda);

                    }

                }


                return Json(new { exito = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }


        [HttpPost]
        [AutorizacionRol(Roles = "Administrador,Coordinador")]
        public ActionResult Guardar(MiembroEquipo model, UsuarioInfo user)
        {
            try
            {
                MiembroEquipo rep = _miembroEquipoService.GetOne(model.NroDocumento);
                string mensaje = "";
                string estad = "1";
                if (rep != null)
                {
                    if (rep.MiembroEquipoId != model.MiembroEquipoId)
                    {
                        ModelState.AddModelError("", string.Format("El DNI \"{0}\" ya se encuentra registrado.", model.NroDocumento));
                        mensaje = "El DNI " + model.NroDocumento + " ya se encuentra registrado.";
                    }
                }

                if (ModelState.IsValid)
                {
                    mensaje = model.MiembroEquipoId == 0 ? "La persona en su Equipo fue registrado satisfactoriamente."
                                              : "La persona en su Equipo fue modificado satisfactoriamente.";

                    Auditoria objauditoria = new Auditoria();
                    MiembroEquipo obj;

                    if (model.MiembroEquipoId == 0)
                    {
                        obj = new MiembroEquipo();
                        obj.NroDocumento = model.NroDocumento;

                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;



                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Miembro";
                        objauditoria.Pantalla = "Equipo de Trabajo";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/

                    }
                    else
                    {
                        obj = _miembroEquipoService.GetOne(model.MiembroEquipoId);

                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Miembro";
                        objauditoria.Pantalla = "Equipo de Trabajo";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/

                    }

                    if (user.Rol == (short)Rol.Coordinador)
                    {
                        obj.EntidadId = user.EntidadId;
                    }
                    else if (obj.EntidadId != model.EntidadId)
                    {
                        obj.Entidad = null;
                        obj.EntidadId = model.EntidadId;
                    }

                    obj.NroDocumento = model.NroDocumento;
                    obj.Cargo = model.Cargo;
                    obj.RolEquipoId = model.RolEquipoId;
                    obj.Correo = model.Correo;
                    obj.TelefonoFijo = model.TelefonoFijo;
                    obj.TelefonoMovil = model.TelefonoMovil;
                    obj.Estado = model.Estado;
                    obj.TipoDoc = model.TipoDoc;
                    obj.Sexo = model.Sexo;

                    if (model.ArchivoAdjuntoId == 0)
                    {
                        obj.ArchivoAdjuntoId = null;
                    }
                    else
                    {
                        obj.ArchivoAdjuntoId = model.ArchivoAdjuntoId;
                    }
                    obj.ApePaterno = model.ApePaterno;
                    obj.ApeMaterno = model.ApeMaterno;
                    obj.Nombres = model.Nombres;

                    obj.UserModificacion = user.NroDocumento;
                    obj.FecModificacion = DateTime.Now;
                    _miembroEquipoService.Save(obj);

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
                    return Json(new
                    {
                        estad = "2",
                        mensaje = mensaje
                    });

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

        public ActionResult GetByEntidad(long EntidadId)
        {
            try
            {
                List<MetaDato> lista = _miembroEquipoService.GetByEntidad(EntidadId)
                                            .Where(x => x.Estado != EstadoMiembroEquipo.Baja)
                                            .Select(x => new MetaDato()
                                            {
                                                MetaDatoId = x.MiembroEquipoId,
                                                Nombre = x.NombreCompleto,
                                                Valor01 = x.NroDocumento
                                            }).ToList();
                lista.Insert(0, new MetaDato() { Nombre = "- SELECCIONAR -", MetaDatoId = 0, Valor01 = "" });

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
    }
}