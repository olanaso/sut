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
    public class UnidadOrganicaController : Controller
    {
        private readonly ILogService<UnidadOrganicaController> _log;
        private readonly IUnidadOrganicaService _unidadOrganicaService;
        private readonly IExpedienteService _expedienteService;
        private readonly IProcedimientoService _procedimientoService;
        private readonly ISedeService _sedeService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly IDetalleMaestroService _DetalleMaestroService;
        private readonly IRolMenuService _rolMenuService;

        Auditoria objauditoria = new Auditoria();
        public UnidadOrganicaController(IUnidadOrganicaService unidadOrganicaService,
                                        IExpedienteService expedienteService, IAuditoriaService AuditoriaService,
                                        IProcedimientoService procedimientoService,
                                        ISedeService sedeService,
                                        IDetalleMaestroService detalleMaestroService,
                                        IRolMenuService rolMenuService
                                        )
        {
            _log = new LogService<UnidadOrganicaController>();
            _unidadOrganicaService = unidadOrganicaService;
            _expedienteService = expedienteService;
            _procedimientoService = procedimientoService;
            _sedeService = sedeService;
            _AuditoriaService = AuditoriaService;
            _DetalleMaestroService = detalleMaestroService;
            _rolMenuService = rolMenuService;
        }

        public ActionResult Lista(UsuarioInfo user)
        {
            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 7);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/

            ViewBag.Usuario = user;
            return View();
        }

        public string GetAllLikePagin(UnidadOrganica filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;
            filtro.EntidadId = user.EntidadId;
            Sede s = null;

            if (filtro.SedeId > 0)
                s = _sedeService.GetOne(filtro.SedeId);

            List<UnidadOrganica> lista = new List<UnidadOrganica>();



            if (filtro.SedeId == 0)
                lista = _unidadOrganicaService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);
            else
            {

                var l = _unidadOrganicaService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows).ToList();

                foreach (SedeUnidadOrganica o in s.SedeUnidadOrganica)
                {
                    UnidadOrganica x = l.Where(r => r.UnidadOrganicaId == o.UnidadOrganicaId).FirstOrDefault();
                    if (x != null)
                    {
                        lista.Add(x);
                    }
                }
            }

            System.Web.HttpContext.Current.Session["ListUnidadOrganica"] = lista;

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    UnidadOrganicaId = x.UnidadOrganicaId,
                    Nombre = x.Nombre
                }),
                totalRows = totalRows
            });
        }
        public ActionResult Editar(long id, UsuarioInfo user)
        {
            try
            {
                /*Inicio Cargar roles - Acceso*/
                List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 7);
                user.rolmenu = listarolmenu.ToList();
                /*Fin Cargar roles  - Acceso*/

                ViewBag.User = user;

                UnidadOrganica item;
                if (id == 0)
                {
                    item = new UnidadOrganica();
                    item.EntidadId = user.EntidadId;
                    ViewBag.Title = "Nueva Unidad de Organización";
                }
                else
                {
                    item = _unidadOrganicaService.GetOne(id);
                    ViewBag.Title = "Editar Unidad de Organización";
                }

                ViewBag.Id = id;

                //Combo para cargar la data maestra de inicio 
                ViewBag.ListUnidadOrganica = System.Web.HttpContext.Current.Session["ListUnidadOrganica"];

                List<DetalleMaestro> listDM = _DetalleMaestroService.GetAllXTIPO(TipoDetalleMaestro.UnidadOrganica);
                List<UnidadOrganica> lista = ViewBag.ListUnidadOrganica;


                //validar el contenido de información
                var excludedIDs = new HashSet<string>(lista.Select(p => p.Nombre));
                var result = listDM.Where(p => !excludedIDs.Contains(p.Nombre));
                //fin


                ViewBag.DetalleMaestro = result.Select(x => new SelectListItem()
                {
                    Value = x.DetalleMaestroId.ToString(),
                    Text = x.Nombre.Trim()
                }).ToList();



                //fin


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
        public ActionResult Guardar(UnidadOrganica model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.UnidadOrganicaId == 0 ? "La Unidad Orgánica fue registrada satisfactoriamente."
                                                : "La Unidad Orgánica fue modificada satisfactoriamente.";
                    string Estado = "0";

                    UnidadOrganica obj;
                    if (model.UnidadOrganicaId == 0)
                    {
                        for (int i = 0; i < model.listaNombre.Count(); i++)
                        {

                            var resultado = _AuditoriaService.ValidarUnidadOrganicaId(0, user.EntidadId, model.listaNombre[i]);

                            if (resultado > 0)
                            {

                                return Json(new
                                {
                                    mensaje = "La unidad " + model.listaNombre[i] + " ya fue registrado ",
                                    Estado = "2"
                                });
                            }


                            var num = _unidadOrganicaService.GetAll(user.EntidadId).Count();


                            if (model.listaNombre[i] != "")
                            {

                                obj = new UnidadOrganica();
                                obj.EntidadId = model.EntidadId;
                                obj.UserCreacion = user.NroDocumento;
                                obj.FecCreacion = DateTime.Now;
                                obj.Nombre = model.listaNombre[i];
                                obj.Numero = num + 1;

                                /*auditoria agregar*/
                                objauditoria.EntidadId = user.EntidadId;
                                objauditoria.SectorId = user.Sector;
                                objauditoria.ProvinciaId = user.Provincia;
                                objauditoria.Usuario = user.NombreCompleto;
                                objauditoria.Actividad = "Agregar Unidad Orgánica";
                                objauditoria.Pantalla = "Unidad Orgánica";
                                objauditoria.UserCreacion = user.NroDocumento;
                                objauditoria.FecCreacion = DateTime.Now;
                                /*auditoria*/

                                obj.Activo = true;

                                obj.UserModificacion = user.NroDocumento;
                                obj.FecModificacion = DateTime.Now;
                                _unidadOrganicaService.Save(obj);
                                /*auditoria Grabar*/
                                _AuditoriaService.Save(objauditoria);
                                /*auditoria Grabar*/
                            }
                        }
                    }
                    else
                    {
                        var resultado = _AuditoriaService.ValidarUnidadOrganicaId(model.UnidadOrganicaId, user.EntidadId, model.Nombre);

                        if (resultado > 0)
                        {

                            return Json(new
                            {
                                mensaje = "La unidad " + model.Nombre + " ya fue registrado ",
                                Estado = "2"
                            });
                        }

                        obj = _unidadOrganicaService.GetOne(model.UnidadOrganicaId);

                        obj.Nombre = model.Nombre;

                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar  Unidad Orgánica";
                        objauditoria.Pantalla = "Unidad Orgánica";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/

                        obj.Activo = true;

                        obj.UserModificacion = user.NroDocumento;
                        obj.FecModificacion = DateTime.Now;
                        _unidadOrganicaService.Save(obj);
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
                ModelState.AddModelError("editarUnidadOrganica", ex.Message);

                _log.Error(ex);
                return PartialView("_Error");
            }
        }


        [HttpPost]
        public string ExisteEnProcedimiento(long UnidadOrganicaId, UsuarioInfo user)
        {

            var res = false;


            /*auditoria agregar*/
            objauditoria.EntidadId = user.EntidadId;
            objauditoria.SectorId = user.Sector;
            objauditoria.ProvinciaId = user.Provincia;
            objauditoria.Usuario = user.NombreCompleto;
            objauditoria.Actividad = "Eliminar Unidad Orgánica";
            objauditoria.Pantalla = "Unidad Orgánica";
            objauditoria.UserCreacion = user.NroDocumento;
            objauditoria.FecCreacion = DateTime.Now;
            /*auditoria*/

            //var vExpediente = _expedienteService.GetByEntidad(user.EntidadId);

            //foreach (Expediente o in vExpediente == null ? new List<Expediente>() : vExpediente)
            //{
            //    var Proc = _procedimientoService.GetByExpediente(o.ExpedienteId);

            //    foreach (Procedimiento p in Proc == null ? new List<Procedimiento>() : Proc)
            //    {
            //        if (p.ProcedimientoSede != null)
            //        {
            //            foreach (ProcedimientoSede s in p.ProcedimientoSede)
            //            {

            //                if (s.UndOrgRecepcionDocumentos != null)
            //                {

            //                    foreach (UndOrgRecepcionDocumentos u in s.UndOrgRecepcionDocumentos)
            //                    {
            //                        if (u.UnidadOrganicaId == UnidadOrganicaId)
            //                        {
            //                            res = true;
            //                        }
            //                    }

            //                }
            //            }
            //        }
            //    }
            //}

            var VerificarEliminar = _AuditoriaService.VerificarEliminar(UnidadOrganicaId);
            _AuditoriaService.Save(objauditoria);
            return JsonConvert.SerializeObject(new StatusResponse()
            {
                Value = res,
                Success = true,
                Message = VerificarEliminar
            });

        }

        [HttpPost]
        public string EditarExisteEnProcedimiento(long UnidadOrganicaId, UsuarioInfo user)
        {

            var res = false;


            /*auditoria agregar*/
            objauditoria.EntidadId = user.EntidadId;
            objauditoria.SectorId = user.Sector;
            objauditoria.ProvinciaId = user.Provincia;
            objauditoria.Usuario = user.NombreCompleto;
            objauditoria.Actividad = "Editar Unidad Orgánica";
            objauditoria.Pantalla = "Unidad Orgánica";
            objauditoria.UserCreacion = user.NroDocumento;
            objauditoria.FecCreacion = DateTime.Now;
            /*auditoria*/

            var VerificarEliminar = _AuditoriaService.VerificarEliminareditar(UnidadOrganicaId);
            _AuditoriaService.Save(objauditoria);
            return JsonConvert.SerializeObject(new StatusResponse()
            {
                Value = res,
                Success = true,
                Message = VerificarEliminar
            });

        }

        [HttpPost]
        public ActionResult Eliminar(int UnidadOrganicaId, UsuarioInfo user)
        {

            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Eliminar Unidad Orgánica";
                objauditoria.Pantalla = " Unidad Orgánica";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                _unidadOrganicaService.Eliminar(UnidadOrganicaId);
                /*auditoria Grabar*/
                _AuditoriaService.Save(objauditoria);

                var num = _unidadOrganicaService.GetAll(user.EntidadId);
                for (int i = 0; i < _unidadOrganicaService.GetAll(user.EntidadId).Count(); i++)
                {

                    var obj = _unidadOrganicaService.GetOne(num[i].UnidadOrganicaId);

                    obj.Numero = i + 1;

                    _unidadOrganicaService.Save(obj);

                }

                /*auditoria Grabar*/
                return Json(new
                {
                    mensaje = "La Unidad Orgánica fue eliminada satisfactoriamente."
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

        public ActionResult Select(bool multi, string fnAdd, string texto, int? SedeId, int? tipo, int? num)
        {
            try
            {
                ViewBag.Multi = multi;
                ViewBag.FnAdd = fnAdd;
                ViewBag.Texto = texto;
                ViewBag.SedeId = SedeId ?? 0;
                ViewBag.tipo = tipo ?? 0;
                ViewBag.num = num ?? 0;

                //MaestroFijo modelMaestroFijo = _maestroFijoService.GetOneByEntidad(user.EntidadId);

                return PartialView("_Select");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult SelectDelete(bool multi, string fnAdd, string lstUndOrg)
        {
            try
            {
                ViewBag.Multi = multi;
                ViewBag.FnAdd = fnAdd;
                ViewBag.lstUndOrg = lstUndOrg;


                return PartialView("_SelectDelete");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}