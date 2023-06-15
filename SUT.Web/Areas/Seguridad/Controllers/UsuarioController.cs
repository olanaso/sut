using Microsoft.Ajax.Utilities;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Newtonsoft.Json;
using Sut.ApplicationServices;
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
    public class UsuarioController : Controller
    {
        private readonly ILogService<UsuarioController> _log;
        private readonly IEntidadService _entidadService;
        private readonly IUsuarioService _usuarioService;
        private readonly IMiembroEquipoService _miembroEquipoService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly IUsuarioRolService _usuariorolService;
        private readonly IDepartamentoService _departamentoService;
        private readonly IProvinciaService _provinciaService;
        private readonly IRolesService _rolesService;
        private readonly IRolMenuService _rolMenuService;

        Auditoria objauditoria = new Auditoria();
        private object iosp;

        public UsuarioController(IEntidadService entidadService,
                                IUsuarioService usuarioService, IAuditoriaService AuditoriaService,
                                IMiembroEquipoService miembroEquipoService,
                                IUsuarioRolService usuariorolService,
                                IDepartamentoService departamentoService,
                                IProvinciaService provinciaService,
                                IRolesService rolesService,
                                IRolMenuService rolMenuService)
        {
            _log = new LogService<UsuarioController>();
            _entidadService = entidadService;
            _usuarioService = usuarioService;
            _miembroEquipoService = miembroEquipoService;
            _AuditoriaService = AuditoriaService;
            _usuariorolService = usuariorolService;
            _departamentoService = departamentoService;
            _provinciaService = provinciaService;
            _rolesService = rolesService;
            _rolMenuService = rolMenuService;
        }

        [AutorizacionRol(Roles = "Administrador,Coordinador")]
        public ActionResult Lista(UsuarioInfo user)
        {
            if (user.Rol == (short)Rol.Administrador)
            {

                List<Roles> listaRol = _rolesService.GetAll();

                //List<SelectListItem> listaRol = new List<SelectListItem>();
                //listaRol.Add(new SelectListItem() { Value = "1", Text = "Administrador SGP" });
                //listaRol.Add(new SelectListItem() { Value = "2", Text = "Administrador SUT" });
                //listaRol.Add(new SelectListItem() { Value = "3", Text = "Registrador Procesos" });
                //listaRol.Add(new SelectListItem() { Value = "4", Text = "Registrador Legal" });
                //listaRol.Add(new SelectListItem() { Value = "5", Text = "Registrador Costos" });


                //listaRol.Add(new SelectListItem() { Value = "6", Text = "Ratificador" });
                //listaRol.Add(new SelectListItem() { Value = "7", Text = "Evaluador" });
                //listaRol.Add(new SelectListItem() { Value = "8", Text = "Entidad Fiscalizadora" });

                //listaRol = listaRol.OrderBy(x => x.Text).ToList();
                listaRol.Insert(0, new Roles() { RolId = 0, Descripcion = " - TODOS - " });
                ViewBag.ListaRol = listaRol.Select(x => new SelectListItem()
                {
                    Value = x.RolId.ToString(),
                    Text = x.Descripcion
                })
               .ToList();


                ViewBag.publicarEstado = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = " - TODOS - ", Value = "0" },
                    new SelectListItem() { Text = "Activo", Value = "1" },
                    new SelectListItem() { Text = "Suspendido", Value = "2" },
                    new SelectListItem() { Text = "Baja", Value = "3" },
                };


                //List<Departamento> listaDepartamento = _departamentoService.GetAll();
                //listaDepartamento = listaDepartamento.OrderBy(x => x.Nombre).ToList();
                //listaDepartamento.Insert(0, new Departamento() { DepartamentoId = 0, Nombre = " - TODOS - " });
                //ViewBag.ListaDepartamento = JsonConvert.SerializeObject(listaDepartamento.Select(x => new SelectListItem() { Value = x.DepartamentoId.ToString(), Text = x.Nombre }).ToList());


                //List<Provincia> listaProvincia = new List<Provincia>();
                //listaProvincia = listaProvincia.OrderBy(x => x.Nombre).ToList();
                //listaProvincia.Insert(0, new Provincia() { ProvinciaId = 0, Nombre = " - TODOS - " });
                //ViewBag.ListaProvincia = JsonConvert.SerializeObject(listaProvincia.Select(x => new SelectListItem() { Value = x.ProvinciaId.ToString(), Text = x.Nombre }).ToList());


                List<Departamento> listaDepartamento = _departamentoService.GetAll();
                listaDepartamento = listaDepartamento.OrderBy(x => x.Nombre).ToList();
                listaDepartamento.Insert(0, new Departamento() { DepartamentoId = 0, Nombre = " - TODOS - " });

                ViewBag.ListaDepartamento = listaDepartamento.Select(x => new SelectListItem()
                {
                    Value = x.DepartamentoId.ToString(),
                    Text = x.Nombre
                })
               .ToList();


                ViewBag.asignarentidades = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Seleccionar", Value = "0" },
                    new SelectListItem() { Text = "SI", Value = "1" },
                    new SelectListItem() { Text = "No", Value = "2" },
                };


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



                return View();
            }
            else if (user.Rol == (short)Rol.Coordinador)
            {
                return RedirectToAction("ListaUsr");
            }
            else
            {
                ModelState.AddModelError("", "No tiene acceso");
                return View("_Error");
            }
        }
        //public void GenerarListamenu(UsuarioInfo user)
        //{
        //    ViewBag.menu = _menuService.GetByMenu();
        //    List<Menu> menu = _menuService.GetByMenu();
        //    List<Menuayuda> menuestado = _menuayudaService.GetByEntidad(user.EntidadId);


        //    Menuayuda maximomenu = _menuayudaService.GetByEntidad(user.EntidadId).Max();

        //    int count = 0;
        //    int countayuda = 1;
        //    int valor = 0;

        //    if (user.Rol != 2)
        //    {
        //        valor = 4;
        //    }
        //    if (menuestado.Count == 0)
        //    {
        //        foreach (Menu lmenu in menu)
        //        {

        //            if (count == valor)
        //            {
        //                lmenu.Estilo = lmenu.Estilo;
        //                ViewBag.nombremenu = lmenu.Descripcion;
        //            }
        //            else
        //            {
        //                if (lmenu.IdPadreMenu == 0 || (lmenu.MenuId == 17 || lmenu.MenuId == 7 || lmenu.MenuId == 20 || lmenu.MenuId == 22))
        //                {
        //                    lmenu.Estilo = "bg-light";
        //                }
        //                lmenu.url = "#";
        //            }
        //            count++;
        //        }
        //    }
        //    else
        //    {

        //        foreach (Menu lmenu in menu)
        //        {

        //            var resultadoayuda = _menuayudaService.GetByone(countayuda, user.EntidadId);
        //            if (resultadoayuda != null)
        //            {
        //                lmenu.url = lmenu.url;
        //                lmenu.Estilo = lmenu.Estilo;
        //                ViewBag.nombremenu = lmenu.Descripcion;
        //            }
        //            else
        //            {
        //                if (maximomenu.MenuId + 1 == lmenu.MenuId)
        //                {
        //                    lmenu.url = lmenu.url;
        //                    lmenu.Estilo = lmenu.Estilo;
        //                    ViewBag.nombremenu = lmenu.Descripcion;
        //                }
        //                else
        //                {
        //                    if (lmenu.IdPadreMenu == 0 || (lmenu.MenuId == 17 || lmenu.MenuId == 7 || lmenu.MenuId == 20 || lmenu.MenuId == 22))
        //                    {
        //                        lmenu.Estilo = "bg-light";
        //                    }
        //                    lmenu.url = "#";
        //                }
        //            }
        //            countayuda++;
        //        }
        //    }
        //}

        [AutorizacionRol(Roles = "Coordinador")]
        public ActionResult ListaUsr(UsuarioInfo user)
        {
            try
            {
                /*Inicio Cargar roles - Acceso*/
                List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 3);
                user.rolmenu = listarolmenu.ToList();
                /*Fin Cargar roles  - Acceso*/

                List<UsuarioRol> usuariorol = _usuariorolService.GetByEntidad(user.EntidadId).Where(x => x.valor != 0).ToList();

                List<UsuarioRol> lista = new List<UsuarioRol>();

                var obj1 = usuariorol.Where(x => x.Rol == Rol.RegistradorProcesos);
                if (obj1.Count() != 0)
                {

                    if (user.ActivarMultiUsuario == 1)
                    {
                        foreach (UsuarioRol roles in obj1)
                        {

                            lista.Add(roles);

                        }
                        for (int i = obj1.Count(); i < 20; i++)
                        {
                            lista.Add(new UsuarioRol() { Rol = Rol.RegistradorProcesos, MiembroEquipo = new MiembroEquipo() { RolEquipo = new MetaDato() } });
                        }
                    }
                    else
                    {
                        foreach (UsuarioRol roles in obj1)
                        {

                            lista.Add(roles);

                        }
                    }

                }
                else
                {
                    if (user.ActivarMultiUsuario == 1)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            lista.Add(new UsuarioRol() { Rol = Rol.RegistradorProcesos, MiembroEquipo = new MiembroEquipo() { RolEquipo = new MetaDato() } });
                        }
                    }
                    else
                    {
                        lista.Add(new UsuarioRol() { Rol = Rol.RegistradorProcesos, MiembroEquipo = new MiembroEquipo() { RolEquipo = new MetaDato() } });
                    }
                }

                obj1 = usuariorol.Where(x => x.Rol == Rol.RegistradorLegal);
                if (obj1.Count() != 0)
                {
                    if (user.ActivarMultiUsuario == 1)
                    {
                        foreach (UsuarioRol roles in obj1)
                        {

                            lista.Add(roles);

                        }
                        for (int i = obj1.Count(); i < 20; i++)
                        {
                            lista.Add(new UsuarioRol() { Rol = Rol.RegistradorLegal, MiembroEquipo = new MiembroEquipo() { RolEquipo = new MetaDato() } });
                        }
                    }
                    else
                    {
                        foreach (UsuarioRol roles in obj1)
                        {

                            lista.Add(roles);

                        }

                    }
                }

                else
                {
                    if (user.ActivarMultiUsuario == 1)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            lista.Add(new UsuarioRol() { Rol = Rol.RegistradorLegal, MiembroEquipo = new MiembroEquipo() { RolEquipo = new MetaDato() } });
                        }
                    }
                    else
                    {
                        lista.Add(new UsuarioRol() { Rol = Rol.RegistradorLegal, MiembroEquipo = new MiembroEquipo() { RolEquipo = new MetaDato() } });
                    }
                }


                obj1 = usuariorol.Where(x => x.Rol == Rol.RegistradorCostos);
                if (obj1.Count() != 0)
                {
                    if (user.ActivarMultiUsuario == 1)
                    {
                        foreach (UsuarioRol roles in obj1)
                        {

                            lista.Add(roles);

                        }
                        for (int i = obj1.Count(); i < 20; i++)
                        {

                            lista.Add(new UsuarioRol() { Rol = Rol.RegistradorCostos, MiembroEquipo = new MiembroEquipo() { RolEquipo = new MetaDato() } });
                        }
                    }
                    else
                    {

                        foreach (UsuarioRol roles in obj1)
                        {

                            lista.Add(roles);

                        }
                    }
                }

                else
                {
                    if (user.ActivarMultiUsuario == 1)
                    {
                        for (int i = 0; i < 20; i++)
                        {

                            lista.Add(new UsuarioRol() { Rol = Rol.RegistradorCostos, MiembroEquipo = new MiembroEquipo() { RolEquipo = new MetaDato() } });
                        }
                    }
                    else
                    {

                        lista.Add(new UsuarioRol() { Rol = Rol.RegistradorCostos, MiembroEquipo = new MiembroEquipo() { RolEquipo = new MetaDato() } });
                    }

                }



                //evaluador
                if (user.EstadoMinistrio == 1)
                {
                    var obj = usuariorol.Where(x => x.Rol == Rol.Evaluador).Take(1).Min();
                    if (obj != null)
                    {
                        lista.Add(obj);
                        usuariorol.Remove(obj);
                    }
                    else lista.Add(new UsuarioRol() { Rol = Rol.Evaluador, MiembroEquipo = new MiembroEquipo() { RolEquipo = new MetaDato() } });


                    obj = usuariorol.Where(x => x.Rol == Rol.Evaluador).Take(1).Min();
                    if (obj != null)
                    {
                        lista.Add(obj);
                        usuariorol.Remove(obj);
                    }
                    else lista.Add(new UsuarioRol() { Rol = Rol.Evaluador, MiembroEquipo = new MiembroEquipo() { RolEquipo = new MetaDato() } });

                    obj = usuariorol.Where(x => x.Rol == Rol.Evaluador).Take(1).Min();
                    if (obj != null)
                    {
                        lista.Add(obj);
                        usuariorol.Remove(obj);
                    }
                    else lista.Add(new UsuarioRol() { Rol = Rol.Evaluador, MiembroEquipo = new MiembroEquipo() { RolEquipo = new MetaDato() } });

                    obj = usuariorol.Where(x => x.Rol == Rol.Evaluador).Take(1).Min();
                    if (obj != null)
                    {
                        lista.Add(obj);
                        usuariorol.Remove(obj);
                    }
                    else lista.Add(new UsuarioRol() { Rol = Rol.Evaluador, MiembroEquipo = new MiembroEquipo() { RolEquipo = new MetaDato() } });
                }
                else if (user.Provincia != 0)
                {
                    var obj = usuariorol.Where(x => x.Rol == Rol.Ratificador).Take(1).Min();
                    if (obj != null)
                    {
                        lista.Add(obj);
                        usuariorol.Remove(obj);
                    }
                    else lista.Add(new UsuarioRol() { Rol = Rol.Ratificador, MiembroEquipo = new MiembroEquipo() { RolEquipo = new MetaDato() } });


                    obj = usuariorol.Where(x => x.Rol == Rol.Ratificador).Take(1).Min();
                    if (obj != null)
                    {
                        lista.Add(obj);
                        usuariorol.Remove(obj);
                    }
                    else lista.Add(new UsuarioRol() { Rol = Rol.Ratificador, MiembroEquipo = new MiembroEquipo() { RolEquipo = new MetaDato() } });

                    obj = usuariorol.Where(x => x.Rol == Rol.Ratificador).Take(1).Min();
                    if (obj != null)
                    {
                        lista.Add(obj);
                        usuariorol.Remove(obj);
                    }
                    else lista.Add(new UsuarioRol() { Rol = Rol.Ratificador, MiembroEquipo = new MiembroEquipo() { RolEquipo = new MetaDato() } });

                    obj = usuariorol.Where(x => x.Rol == Rol.Ratificador).Take(1).Min();
                    if (obj != null)
                    {
                        lista.Add(obj);
                        usuariorol.Remove(obj);
                    }
                    else lista.Add(new UsuarioRol() { Rol = Rol.Ratificador, MiembroEquipo = new MiembroEquipo() { RolEquipo = new MetaDato() } });
                }
                //
                ViewBag.Usuario = user;
                ViewBag.Provincia = user.Provincia;
                ViewBag.EstadoMinistrio = user.EstadoMinistrio;

                return View(lista);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("_Error");
            }
        }

        public string GetAllLikePagin(Usuario filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            if (user.Rol == (short)Rol.Coordinador)
            {
                filtro.EntidadId = user.EntidadId;
            }

            int totalRows = 0;
            List<Usuario> lista = _usuarioService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    UsuarioId = x.UsuarioId,
                    NroDocumento = x.NroDocumento,
                    NombreCompleto = x.MiembroEquipo.NombreCompleto,
                    Entidad = x.Entidad != null ? x.Entidad.Nombre : "",
                    Cargo = x.MiembroEquipo.RolEquipo == null ? "" : x.MiembroEquipo.RolEquipo.Nombre,
                    Rol = Enum.GetName(typeof(Rol), x.Rol),
                    FecCreacion = x.FecCreacion.Value.ToString("dd/MM/yyyy"),
                    Estado = x.Estado,
                    EntidadId = x.EntidadId,
                    ActivarCorreo = x.ActivarCorreo,
                    asigEntidad = x.AsigEntidad
                }),
                totalRows = totalRows
            });
        }

        //[AutorizacionRol(Roles = "Administrador")]
        public ActionResult Editar(long id, UsuarioInfo user)
        {
            try
            {
                ViewBag.User = user;



                List<Departamento> listaDepartamento = _departamentoService.GetAll();
                listaDepartamento = listaDepartamento.OrderBy(x => x.Nombre).ToList();
                listaDepartamento.Insert(0, new Departamento() { DepartamentoId = 0, Nombre = " - SELECCIONAR - " });
                ViewBag.ListaDepartamento = listaDepartamento.Select(x => new SelectListItem() { Value = x.DepartamentoId.ToString(), Text = x.Nombre }).ToList();


                List<Provincia> listaProvincia = new List<Provincia>();

                Usuario item;
                if (id == 0)
                {
                    item = new Usuario();
                    ViewBag.Title = "Nuevo Usuario";

                    if (user.Rol == (short)Rol.Coordinador)
                    {
                        item.EntidadId = user.EntidadId;
                        item.Rol = Rol.RegistradorProcesos;
                    }
                }
                else
                {
                    item = _usuarioService.GetOne(id);
                    ViewBag.Title = "Editar Usuario";

                    Entidad itementidad = _entidadService.GetOne(item.EntidadId);

                    if (itementidad.Provincia != null)
                    {
                        item.DepartamentoId = itementidad.Provincia.DepartamentoId;

                        listaProvincia = _provinciaService.GetByDepartamento(item.DepartamentoId);
                        listaProvincia = listaProvincia.OrderBy(x => x.Nombre).ToList();

                        item.ProvinciaId = itementidad.Provincia.ProvinciaId;

                    }
                }


                //List<SelectListItem> listaRol = new List<SelectListItem>();
                //listaRol.Add(new SelectListItem() { Value = "1", Text = "Administrador SGP" });
                //listaRol.Add(new SelectListItem() { Value = "2", Text = "Administrador SUT" });
                //listaRol.Add(new SelectListItem() { Value = "3", Text = "Registrador Procesos" });
                //listaRol.Add(new SelectListItem() { Value = "4", Text = "Registrador Legal" });
                //listaRol.Add(new SelectListItem() { Value = "5", Text = "Registrador Costos" });


                //listaRol.Add(new SelectListItem() { Value = "6", Text = "Ratificador" });
                //listaRol.Add(new SelectListItem() { Value = "7", Text = "Evaluador" });
                //listaRol.Add(new SelectListItem() { Value = "8", Text = "Entidad Fiscalizadora" });
                //listaRol = listaRol.OrderBy(x => x.Text).ToList();
                //listaRol.Insert(0, new SelectListItem() { Value = "0", Text = " - SELECCIONAR -" });
                //ViewBag.ListaRol = listaRol;

                /*Selccion para llamar los roles del usuario seleccionado*/
                //listando los reoles del usuario al cual se esta modificando los datos

                //Obtenienddo el usuario selecionado en la fila
                Usuario ousuario = _usuarioService.GetOne(id);
                ViewBag.usuarioselect = ousuario;

                //obteniendo los roles del usuario seleccionado
                List<UsuarioRol> usurol = _usuariorolService.GetByUsuarioRol(id);
                int valroles = 0;
                List<Roles> lroles = new List<Roles>();
                foreach (UsuarioRol roles in usurol)
                {

                    if ((short)roles.Rol != valroles)
                    {
                        string valor = Convert.ToString((short)roles.Rol);
                        Roles oRoles = new Roles();
                        oRoles.Descripcion = valor;
                        oRoles.RolId = (short)roles.Rol;
                        lroles.Add(oRoles);


                    }


                }
                /**/

                List<Roles> listaRol = _rolesService.GetAll();

                //listaRol.Insert(0, new Roles() { RolId = 0, Descripcion = " - SELECCIONAR - " });
                 var rolesusuario = listaRol.Select(roles => new SelectListItem()
                {
                    Value = roles.RolId.ToString(),
                    Text = roles.Descripcion,
                    Selected = lroles.Distinct().Any(rolUsua => rolUsua.RolId == roles.RolId)

                })
                  .ToList();

                ViewBag.ListaRol = rolesusuario;



                var listaEntidad = _entidadService.GetAll()
                                                .Where(x => x.Estado == Respuesta.Si)
                                                .OrderBy(x => x.Nombre)
                                                .ToList();
                listaEntidad.Insert(0, new Entidad()
                {
                    EntidadId = 0,
                    Nombre = " - SELECCIONAR - "
                });

                ViewBag.ListaEntidad = listaEntidad.Select(x => new SelectListItem()
                {
                    Value = x.EntidadId.ToString(),
                    Text = x.Nombre
                })
                                                    .ToList();


                List<MetaDato> listaMiembros;
                if (id == 0)
                {

                    listaMiembros = _miembroEquipoService.GetByEntidad(item.EntidadId)
                                            .OrderBy(x => x.NombreCompleto)
                                            .Select(x => new MetaDato()
                                            {
                                                MetaDatoId = x.MiembroEquipoId,
                                                Nombre = x.NombreCompleto,
                                                Valor01 = x.NroDocumento
                                            }).ToList();

                    listaMiembros.Insert(0, new MetaDato() { MetaDatoId = 0, Nombre = " - SELECCIONAR - ", Valor01 = "" });
                }
                else
                {
                    listaMiembros = _miembroEquipoService.GetByEntidad(item.EntidadId)
                                  .Where(x => x.MiembroEquipoId == item.MiembroEquipoId)
                                            .OrderBy(x => x.NombreCompleto)
                                            .Select(x => new MetaDato()
                                            {
                                                MetaDatoId = x.MiembroEquipoId,
                                                Nombre = x.NombreCompleto,
                                                Valor01 = x.NroDocumento
                                            }).ToList();
                }

                ViewBag.ListaMiembros = listaMiembros;

                ViewBag.publicar = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Activo", Value = "1" , Selected = item.Estado == EstadoUsuario.Activo ? true:false},
                    new SelectListItem() { Text = "Suspendido", Value = "2",  Selected = item.Estado == EstadoUsuario.Suspendido ? true:false },
                    new SelectListItem() { Text = "Baja", Value = "3",  Selected = item.Estado == EstadoUsuario.Baja ? true:false },
                };


                ViewBag.activarcorreo = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Activo", Value = "1" , Selected = item.Estado == EstadoUsuario.Activo ? true:false},
                    new SelectListItem() { Text = "No Activo", Value = "2",  Selected = item.Estado == EstadoUsuario.Suspendido ? true:false },
                };


                ViewBag.asignarentidades = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Seleccionar", Value = "0" , Selected = item.AsigEntidad == AsignarEntidades.Seleccionar ? true:false},
                    new SelectListItem() { Text = "SI", Value = "1",  Selected = item.AsigEntidad == AsignarEntidades.Si ? true:false },
                    new SelectListItem() { Text = "No", Value = "2",  Selected = item.AsigEntidad == AsignarEntidades.No ? true:false },
                };


                listaProvincia.Insert(0, new Provincia() { ProvinciaId = 0, Nombre = " - SELECCIONAR - " });
                ViewBag.ListaProvincia = listaProvincia.Select(x => new SelectListItem() { Value = x.ProvinciaId.ToString(), Text = x.Nombre }).ToList();
                var clave = Cryptography.Decrypt(item.Clave);

                return PartialView("_Editar", item);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        [AutorizacionRol(Roles = "Coordinador")]
        public ActionResult EditarUsr(long id, Rol rol, UsuarioInfo user)
        {
            try
            {

                /*Inicio Cargar roles - Acceso*/
                List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 3);
                user.rolmenu = listarolmenu.ToList();
                /*Fin Cargar roles  - Acceso*/

                ViewBag.User = user;

                Usuario item;
                if (id == 0)
                {
                    item = new Usuario();
                    item.EntidadId = user.EntidadId;
                    ViewBag.Title = "Nuevo Usuario";
                }
                else
                {
                    item = _usuarioService.GetOne(id);
                    ViewBag.Title = "Editar Usuario";
                }

                item.Rol = rol;

                var listaMiembros = _miembroEquipoService.GetByEntidad(item.EntidadId)
                                                .OrderBy(x => x.NombreCompleto)
                                                .Select(x => new MetaDato()
                                                {
                                                    MetaDatoId = x.MiembroEquipoId,
                                                    Nombre = x.NombreCompleto,
                                                    Valor01 = x.NroDocumento
                                                }).ToList();
                listaMiembros.Insert(0, new MetaDato() { MetaDatoId = 0, Nombre = " - SELECCIONAR - ", Valor01 = "" });
                ViewBag.ListaMiembros = listaMiembros;

                return PartialView("_EditarUsr", item);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }



        public ActionResult Eliminar(long id, long idusurol, UsuarioInfo user)
        {
            try
            {


                UsuarioRol usuariorol = _usuariorolService.GetByone(id);
                if (usuariorol == null)
                {
                    _usuarioService.Delete(id);
                }
                _usuariorolService.Delete(idusurol);

                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Eliminar Usuario";
                objauditoria.Pantalla = "Usuario";
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

        [HttpPost]
        [AutorizacionRol(Roles = "Administrador,Coordinador")]
        public ActionResult Guardar(Usuario model, UsuarioInfo user)
        {
            try
            {
                Usuario rep = _usuarioService.GetOne(model.NroDocumento);
                if (rep != null)
                    if (rep.UsuarioId != model.UsuarioId)
                        ModelState.AddModelError("", string.Format("El usuario \"{0}\" ya se encuentra registrado.", model.NroDocumento));

                if (model.UsuarioId == 0 && string.IsNullOrEmpty(model.Clave))
                    ModelState.AddModelError("", "Debe ingresar una contraseña.");

                if (ModelState.IsValid)
                {
                    string mensaje = model.UsuarioId == 0 ? "El Usuario fue registrado satisfactoriamente."
                                                : "El Usuario fue modificado satisfactoriamente.";

                    Usuario obj;
                    UsuarioRol objrol;
                    if (model.UsuarioId == 0)
                    {
                        obj = new Usuario();
                        var miembro = _miembroEquipoService.GetOne(model.MiembroEquipoId);
                        obj.NroDocumento = miembro.NroDocumento;
                        obj.MiembroEquipoId = model.MiembroEquipoId;

                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;


                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Usuario";
                        objauditoria.Pantalla = "Usuario";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }
                    else
                    {
                        obj = _usuarioService.GetOne(model.UsuarioId);

                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Usuario";
                        objauditoria.Pantalla = "Usuario";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }

                    obj.Rol = model.Rol;

                    obj.Estado = model.Estado;
                    obj.ActivarCorreo = model.ActivarCorreo;
                    obj.FechaSolicitud = model.FechaSolicitud;

                    if (!string.IsNullOrEmpty(model.Clave)) obj.Clave = Cryptography.Encrypt(model.Clave);

                    if (user.Rol == (short)Rol.Coordinador)
                    {
                        obj.EntidadId = user.EntidadId;
                        obj.Estado = EstadoUsuario.Activo;
                    }
                    else if (obj.EntidadId != model.EntidadId && model.EntidadId > 0)
                    {
                        obj.EntidadId = model.EntidadId;
                    }

                    obj.UserModificacion = user.NroDocumento;
                    obj.FecModificacion = DateTime.Now;
                    _usuarioService.Save(obj);

                    objrol = new UsuarioRol();

                    objrol.UsuarioId = obj.UsuarioId;
                    objrol.Rol = obj.Rol;
                    objrol.EntidadId = obj.EntidadId;

                    obj.AsigEntidad = model.AsigEntidad;

                    _usuariorolService.Save(objrol);


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
        [AutorizacionRol(Roles = "Administrador,Coordinador")]
        public ActionResult GuardarInformacionmultirol(Usuario model, UsuarioInfo user)
        {
            try
            {


                string mensaje = model.UsuarioId == 0 ? "El Usuario fue registrado satisfactoriamente."
                                            : "El Usuario fue modificado satisfactoriamente.";
                UsuarioRol objrol;

                Usuario rep = _usuarioService.GetOne(model.NroDocumento);
                if (rep != null)
                {
                    if (rep.UsuarioId != model.UsuarioId)
                    {
                        Usuario repusurol = _usuarioService.GetOnerol(model.NroDocumento, model.Rol);
                        if (repusurol == null)
                        {

                            objrol = new UsuarioRol();
                            objrol.UsuarioId = rep.UsuarioId;
                            objrol.Rol = model.Rol;
                            objrol.valor = 1;
                            objrol.EntidadId = model.EntidadId;
                            _usuariorolService.Save(objrol);

                            return Json(new
                            {
                                mensaje = mensaje
                            });
                        }
                    }
                }

                if (model.UsuarioId == 0 && string.IsNullOrEmpty(model.Clave))
                {
                    ModelState.AddModelError("", "Debe ingresar una contraseña.");
                }

                if (ModelState.IsValid)
                {

                    Usuario obj;

                    if (model.UsuarioId == 0)
                    {
                        obj = new Usuario();
                        var miembro = _miembroEquipoService.GetOne(model.MiembroEquipoId);
                        obj.NroDocumento = miembro.NroDocumento;
                        obj.MiembroEquipoId = model.MiembroEquipoId;

                        obj.UserCreacion = user.NroDocumento;
                        obj.FecCreacion = DateTime.Now;


                        /*auditoria agregar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Agregar Usuario";
                        objauditoria.Pantalla = "Usuario";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }
                    else
                    {
                        obj = _usuarioService.GetOne(model.UsuarioId);

                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Usuario";
                        objauditoria.Pantalla = "Usuario";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/
                    }

                    obj.Rol = model.Rol;

                    obj.Estado = model.Estado;
                    //obj.FechaSolicitud = model.FechaSolicitud;

                    if (!string.IsNullOrEmpty(model.Clave)) obj.Clave = Cryptography.Encrypt(model.Clave);

                    if (user.Rol == (short)Rol.Coordinador)
                    {
                        obj.EntidadId = user.EntidadId;
                        obj.Estado = EstadoUsuario.Activo;
                    }
                    else if (obj.EntidadId != model.EntidadId && model.EntidadId > 0)
                    {
                        obj.EntidadId = model.EntidadId;
                    }

                    obj.UserModificacion = user.NroDocumento;
                    obj.FecModificacion = DateTime.Now;
                    _usuarioService.Save(obj);

                    objrol = new UsuarioRol();
                    objrol.UsuarioId = obj.UsuarioId;
                    objrol.Rol = obj.Rol;
                    objrol.valor = 1;
                    objrol.EntidadId = model.EntidadId;
                    _usuariorolService.Save(objrol);

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


        public ActionResult GetEntidades(long id)
        {
            try
            {

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


        [HttpPost]
        [AutorizacionRol(Roles = "Administrador,Coordinador")]
        public string ValidarDocumento(string NroDocumento, int UsuarioId, UsuarioInfo user)
        {

            string resltado = "";
            Usuario rep = _usuarioService.GetOne(NroDocumento);
            if (rep != null)
                if (rep.UsuarioId != UsuarioId)
                {
                    resltado = "0";
                }

            return resltado;


        }

        public ActionResult Configuracion()
        {
            return View();
        }


        public ActionResult ChangeLanguage(string lang)
        {
            Session["lang"] = lang;
            return RedirectToAction("Index", "Inicio", new { area = "" });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Configuracion(string ClaveActual, string NuevaClave, UsuarioInfo user)
        {
            try
            {
                Usuario usuario = _usuarioService.GetOne(user.UsuarioId);

                if (Cryptography.Encrypt(ClaveActual) == usuario.Clave)
                {
                    usuario.Clave = Cryptography.Encrypt(NuevaClave);
                    usuario.UserModificacion = user.NroDocumento;
                    usuario.FecModificacion = DateTime.Now;

                    _usuarioService.Save(usuario);

                    return RedirectToAction("Index", "Inicio", new { area = "" });
                }
                else
                {
                    ModelState.AddModelError("", "La contraseña ingresada no coincide con la contraseña actual.");
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                ModelState.AddModelError("", ex.Message);
            }
            return View();
        }

        [AutorizacionRol(Roles = "Administrador")]
        [HttpPost]
        public ActionResult SaveRoles(int iosp, int RolId, int UsuarioId , int EntidadId)
        {
            // Construir el objeto que se va a devolver
            Usuario pUsuario=new Usuario();
            pUsuario.iosp = iosp;
            pUsuario.RolId = RolId;
            pUsuario.UsuarioId = UsuarioId;
            pUsuario.EntidadId = EntidadId;

            _usuarioService.SaveRoles(pUsuario);
          

            // Devolver el objeto en formato JSON utilizando la clase JsonResult
            return Json(pUsuario, JsonRequestBehavior.AllowGet);
        }
    }
}