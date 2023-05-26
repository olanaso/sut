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
    public class InductorController : Controller
    {
        private readonly ILogService<InductorController> _log;
        private readonly IInductorService _inductorService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly IDetalleMaestroService _DetalleMaestroService;
        private readonly IExpedienteService _expedienteService;
        private readonly IInductorValorService _inductorValorService;
        private readonly IRolMenuService _rolMenuService;
        Auditoria objauditoria = new Auditoria();
        public InductorController(IAuditoriaService AuditoriaService, IInductorValorService inductorValorService, IInductorService inductorService, IDetalleMaestroService detalleMaestroService,
                                        IExpedienteService expedienteService, IRolMenuService rolMenuService)
        {
            _log = new LogService<InductorController>();
            _inductorService = inductorService;
            _AuditoriaService = AuditoriaService;
            _DetalleMaestroService = detalleMaestroService;
            _expedienteService = expedienteService;
            _inductorValorService = inductorValorService;
            _rolMenuService = rolMenuService;
        }

        public ActionResult Lista(UsuarioInfo user)
        {
            /*Inicio Cargar roles - Acceso*/
            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 17);
            user.rolmenu = listarolmenu.ToList();
            /*Fin Cargar roles  - Acceso*/
            ViewBag.Usuario = user;
            return View();
        }

        public string GetAllLikePagin(Inductor filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;
            filtro.EntidadId = user.EntidadId;

            List<Inductor> lista = _inductorService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);

            System.Web.HttpContext.Current.Session["ListInductor"] = lista;

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Select(x => new
                {
                    InductorId = x.InductorId,
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
                List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenuid(Convert.ToInt32(user.Rol), 17);
                user.rolmenu = listarolmenu.ToList();
                /*Fin Cargar roles  - Acceso*/

                ViewBag.User = user;

                Inductor item;
                if (id == 0)
                {
                    item = new Inductor();
                    item.EntidadId = user.EntidadId;
                    ViewBag.Title = "Nuevo Inductor";
                }
                else
                {
                    item = _inductorService.GetOne(id);
                    ViewBag.Title = "Editar Inductor";
                }
                ViewBag.Id = id;

                //Combo para cargar la data maestra de inicio 
                ViewBag.ListInductor = System.Web.HttpContext.Current.Session["ListInductor"];

                List<DetalleMaestro> listDM = _DetalleMaestroService.GetAllXTIPO(TipoDetalleMaestro.Inductores);
                List<Inductor> lista = ViewBag.ListInductor;


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
        public string ExisteEnProcedimiento(long InductorId, UsuarioInfo user)
        {

            var res = false;


            //var Idasme = _inductorValorService.listGetOne(InductorId).Count();

            //if (Idasme != 0)
            //{
            //    res = true;
            //}
            //else
            //{
            //    var Id = _inductorValorService.GetOne(InductorId);

            //    if (Id != null)
            //    {
            //        res = true;
            //    }
            //} 

            /*auditoria agregar*/
            objauditoria.EntidadId = user.EntidadId;
            objauditoria.SectorId = user.Sector;
            objauditoria.ProvinciaId = user.Provincia;
            objauditoria.Usuario = user.NombreCompleto;
            objauditoria.Actividad = "Eliminar Inductor";
            objauditoria.Pantalla = "Inductor";
            objauditoria.UserCreacion = user.NroDocumento;
            objauditoria.FecCreacion = DateTime.Now;


            var VerificarEliminar = _AuditoriaService.VerificarEliminarInductorId(InductorId);
            _AuditoriaService.Save(objauditoria);

            return JsonConvert.SerializeObject(new StatusResponse()
            {
                Value = res,
                Success = true,
                Message = VerificarEliminar
            });

        }


        [HttpPost]
        public string ExisteEnProcedimientoEditar(long InductorId, UsuarioInfo user)
        {

            var res = false;


            /*auditoria agregar*/
            objauditoria.EntidadId = user.EntidadId;
            objauditoria.SectorId = user.Sector;
            objauditoria.ProvinciaId = user.Provincia;
            objauditoria.Usuario = user.NombreCompleto;
            objauditoria.Actividad = "Editar Inductor";
            objauditoria.Pantalla = "Inductor";
            objauditoria.UserCreacion = user.NroDocumento;
            objauditoria.FecCreacion = DateTime.Now;


            var VerificarEliminar = _AuditoriaService.VerificarEliminarInductorIdEditar(InductorId);
            _AuditoriaService.Save(objauditoria);

            return JsonConvert.SerializeObject(new StatusResponse()
            {
                Value = res,
                Success = true,
                Message = VerificarEliminar
            });

        }


        [HttpPost]
        public ActionResult Eliminar(int InductorId, UsuarioInfo user)
        {

            try
            {
                /*auditoria agregar*/
                objauditoria.EntidadId = user.EntidadId;
                objauditoria.SectorId = user.Sector;
                objauditoria.ProvinciaId = user.Provincia;
                objauditoria.Usuario = user.NombreCompleto;
                objauditoria.Actividad = "Eliminar Inductor";
                objauditoria.Pantalla = "Inductor";
                objauditoria.UserCreacion = user.NroDocumento;
                objauditoria.FecCreacion = DateTime.Now;
                /*auditoria*/
                _inductorService.Eliminar(InductorId);
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
        public ActionResult Guardar(Inductor model, UsuarioInfo user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = model.InductorId == 0 ? "El Inductor fue registrado satisfactoriamente."
                                                : "El Inductor fue modificado satisfactoriamente.";

                    if (model.listaNombreCargado != null)
                    {
                        if (model.listaNombreCargado.Count() == 4)
                        {

                            if (model.chkbuscarUndOrg == null)
                            {
                                String[] array = new string[1];
                                array[0] = "0";
                                model.chkbuscarUndOrg = array;
                            }
                            else
                            {
                                int cantidad = model.chkbuscarUndOrg.Count() + 1;
                                String[] array = new string[cantidad];
                                array[0] = "0";
                                int a = 1;
                                for (int i = 0; i < model.chkbuscarUndOrg.Count(); i++)
                                {

                                    array[a] = model.chkbuscarUndOrg[i];
                                    a = a + 1;
                                }


                                //array = model.chkbuscarUndOrg;
                                //array[cantidad] = "0"; 
                                //model.chkbuscarUndOrg = new string[cantidad];
                                model.chkbuscarUndOrg = array;

                            }

                        }
                    }

                    Inductor obj;
                    int valordecargar = 0;
                    if (model.InductorId == 0)
                    {

                        if (model.chkbuscarUndOrg != null)
                        {
                            if (model.chkbuscarUndOrg.Count() != 0)
                            {
                                foreach (var item in model.chkbuscarUndOrg)
                                {

                                    if (model.listaNombreCargado.Count() == 4 && item.ToString() != "0" && valordecargar == 0)
                                    {
                                        obj = new Inductor();
                                        obj.EntidadId = model.EntidadId;
                                        obj.UserCreacion = user.NroDocumento;
                                        obj.FecCreacion = DateTime.Now;

                                        //obj.Nombre = model.Nombre;


                                        obj.Nombre = model.listaNombreCargado[Convert.ToInt32(item.ToString())];


                                        /*auditoria agregar*/
                                        objauditoria.EntidadId = user.EntidadId;
                                        objauditoria.SectorId = user.Sector;
                                        objauditoria.ProvinciaId = user.Provincia;
                                        objauditoria.Usuario = user.NombreCompleto;
                                        objauditoria.Actividad = "Agregar Inductor";
                                        objauditoria.Pantalla = "Inductor";
                                        objauditoria.UserCreacion = user.NroDocumento;
                                        objauditoria.FecCreacion = DateTime.Now;

                                        obj.Activo = true;

                                        obj.UserModificacion = user.NroDocumento;
                                        obj.FecModificacion = DateTime.Now;
                                        _inductorService.Save(obj);
                                        /*auditoria Grabar*/
                                        _AuditoriaService.Save(objauditoria);
                                        /*auditoria Grabar*/
                                        valordecargar = 1;
                                    }
                                    else
                                    if (valordecargar == 1)
                                    {
                                        obj = new Inductor();
                                        obj.EntidadId = model.EntidadId;
                                        obj.UserCreacion = user.NroDocumento;
                                        obj.FecCreacion = DateTime.Now;

                                        //obj.Nombre = model.Nombre;

                                        string valor = item.ToString();
                                        obj.Nombre = model.listaNombreCargado[Convert.ToInt32(valor)];


                                        /*auditoria agregar*/
                                        objauditoria.EntidadId = user.EntidadId;
                                        objauditoria.SectorId = user.Sector;
                                        objauditoria.ProvinciaId = user.Provincia;
                                        objauditoria.Usuario = user.NombreCompleto;
                                        objauditoria.Actividad = "Agregar Inductor";
                                        objauditoria.Pantalla = "Inductor";
                                        objauditoria.UserCreacion = user.NroDocumento;
                                        objauditoria.FecCreacion = DateTime.Now;

                                        obj.Activo = true;

                                        obj.UserModificacion = user.NroDocumento;
                                        obj.FecModificacion = DateTime.Now;
                                        _inductorService.Save(obj);
                                        /*auditoria Grabar*/
                                        _AuditoriaService.Save(objauditoria);
                                        /*auditoria Grabar*/
                                    }
                                    else if (valordecargar == 0)
                                    {
                                        obj = new Inductor();
                                        obj.EntidadId = model.EntidadId;
                                        obj.UserCreacion = user.NroDocumento;
                                        obj.FecCreacion = DateTime.Now;

                                        //obj.Nombre = model.Nombre;

                                        string valor = item.ToString();
                                        obj.Nombre = model.listaNombreCargado[Convert.ToInt32(valor)];



                                        /*auditoria agregar*/
                                        objauditoria.EntidadId = user.EntidadId;
                                        objauditoria.SectorId = user.Sector;
                                        objauditoria.ProvinciaId = user.Provincia;
                                        objauditoria.Usuario = user.NombreCompleto;
                                        objauditoria.Actividad = "Agregar Inductor";
                                        objauditoria.Pantalla = "Inductor";
                                        objauditoria.UserCreacion = user.NroDocumento;
                                        objauditoria.FecCreacion = DateTime.Now;

                                        obj.Activo = true;

                                        obj.UserModificacion = user.NroDocumento;
                                        obj.FecModificacion = DateTime.Now;
                                        _inductorService.Save(obj);
                                        /*auditoria Grabar*/
                                        _AuditoriaService.Save(objauditoria);
                                        //_AuditoriaService.registrarAuditoria(objauditoria);
                                    }
                                }
                            }
                        }
                        else if (model.listaNombreCargado != null)
                        {
                            if (model.listaNombreCargado.Count() == 4)
                            {
                                obj = new Inductor();
                                obj.EntidadId = model.EntidadId;
                                obj.UserCreacion = user.NroDocumento;
                                obj.FecCreacion = DateTime.Now;

                                //obj.Nombre = model.Nombre;


                                obj.Nombre = model.listaNombreCargado[Convert.ToInt32(0)];


                                /*auditoria agregar*/
                                objauditoria.EntidadId = user.EntidadId;
                                objauditoria.SectorId = user.Sector;
                                objauditoria.ProvinciaId = user.Provincia;
                                objauditoria.Usuario = user.NombreCompleto;
                                objauditoria.Actividad = "Agregar Inductor";
                                objauditoria.Pantalla = "Inductor";
                                objauditoria.UserCreacion = user.NroDocumento;
                                objauditoria.FecCreacion = DateTime.Now;

                                obj.Activo = true;

                                obj.UserModificacion = user.NroDocumento;
                                obj.FecModificacion = DateTime.Now;
                                _inductorService.Save(obj);
                                /*auditoria Grabar*/
                                _AuditoriaService.Save(objauditoria);
                                /*auditoria Grabar*/
                            }
                        }



                        for (int i = 0; i < model.listaNombre.Count(); i++)
                        {
                            if (model.listaNombre[i] != "")
                            {

                                var resultado = _AuditoriaService.ValidarInductorId(0, user.EntidadId, model.listaNombre[i]);

                                if (resultado > 0)
                                {

                                    return Json(new
                                    {
                                        mensaje = "La Inductor " + model.listaNombre[i] + " ya fue registrado ",
                                        Estado = "2"
                                    });
                                }


                                obj = new Inductor();
                                obj.EntidadId = model.EntidadId;
                                obj.UserCreacion = user.NroDocumento;
                                obj.FecCreacion = DateTime.Now;

                                obj.Nombre = model.listaNombre[i];


                                /*auditoria agregar*/
                                objauditoria.EntidadId = user.EntidadId;
                                objauditoria.SectorId = user.Sector;
                                objauditoria.ProvinciaId = user.Provincia;
                                objauditoria.Usuario = user.NombreCompleto;
                                objauditoria.Actividad = "Agregar Inductor";
                                objauditoria.Pantalla = "Inductor";
                                objauditoria.UserCreacion = user.NroDocumento;
                                objauditoria.FecCreacion = DateTime.Now;

                                obj.Activo = true;

                                obj.UserModificacion = user.NroDocumento;
                                obj.FecModificacion = DateTime.Now;
                                _inductorService.Save(obj);
                                /*auditoria Grabar*/
                                _AuditoriaService.Save(objauditoria);
                                /*auditoria Grabar*/

                            }
                        }






                        /*auditoria*/
                    }
                    else
                    {

                        var resultado = _AuditoriaService.ValidarUnidadOrganicaId(model.InductorId, user.EntidadId, model.Nombre);

                        if (resultado > 0)
                        {

                            return Json(new
                            {
                                mensaje = "La Inductor " + model.Nombre + " ya fue registrado ",
                                Estado = "2"
                            });
                        }

                        obj = _inductorService.GetOne(model.InductorId);
                        obj.Nombre = model.Nombre;
                        obj.Activo = true;

                        obj.UserModificacion = user.NroDocumento;
                        obj.FecModificacion = DateTime.Now;
                        _inductorService.Save(obj);

                        /*auditoria actualizar*/
                        objauditoria.EntidadId = user.EntidadId;
                        objauditoria.SectorId = user.Sector;
                        objauditoria.ProvinciaId = user.Provincia;
                        objauditoria.Usuario = user.NombreCompleto;
                        objauditoria.Actividad = "Modificar Inductor";
                        objauditoria.Pantalla = "Inductor";
                        objauditoria.UserCreacion = user.NroDocumento;
                        objauditoria.FecCreacion = DateTime.Now;
                        /*auditoria*/

                        /*auditoria Grabar*/
                        _AuditoriaService.Save(objauditoria);
                        //_AuditoriaService.registrarAuditoria(objauditoria);
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
        //public ActionResult Select(bool multi, string fnAdd)
        //{
        //    try
        //    {
        //        ViewBag.Multi = multi;
        //        ViewBag.FnAdd = fnAdd;

        //        return PartialView("_SelectServicio");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}