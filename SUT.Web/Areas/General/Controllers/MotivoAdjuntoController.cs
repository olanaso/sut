using Sut.Entities;
using Sut.IApplicationServices;
using Sut.Log;
using Sut.Security;
using Sut.Web.Areas.General.Models;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace Sut.Web.Areas.General.Controllers
{
    public class MotivoAdjuntoController : Controller
    {
        private readonly ILogService<MotivoAdjuntoController> _log;
        private readonly IArMotivoAdjuntoService _ArMotivoAdjuntoService;

        public MotivoAdjuntoController(IArMotivoAdjuntoService ArMotivoAdjuntoService)
        {
            _log = new LogService<MotivoAdjuntoController>();
            _ArMotivoAdjuntoService = ArMotivoAdjuntoService;
        }

        public ActionResult Upload(ArMotivoAdjuntoModel model, UsuarioInfo user)
        {
            try
            {
                string path = ConfigurationManager.AppSettings["Sut.PathUpload"].ToString();
                string filename = string.Format("file_{0}_{1}", user.EntidadId, DateTime.Now.ToString("yyyyMMdd_HHmmss"));

                if (path.Substring(0, 1) == "~") path = Server.MapPath(path);
                ArMotivoAdjunto obj = new ArMotivoAdjunto();
                obj.Extension = new FileInfo(model.PostedFile.FileName).Extension;
                if (obj.Extension.ToLower() != ".pdf")
                {
                    return PartialView("_Error");
                }
                model.PostedFile.SaveAs(path + filename + obj.Extension);


                if (model.ArMotivoAdjuntoId > 0)
                {
                    obj = _ArMotivoAdjuntoService.GetOne(model.ArMotivoAdjuntoId);
                    if (System.IO.File.Exists(path + obj.Ruta)) System.IO.File.Delete(path + obj.Ruta);
                }

                obj.Ruta = filename;
                obj.NombreArchivo = model.PostedFile.FileName;
                //obj.Extension = new FileInfo(model.PostedFile.FileName).Extension;
                obj.Tipo = model.PostedFile.ContentType;
                obj.Tamano = model.PostedFile.ContentLength;
                obj.Descripcion = model.PostedFile.FileName;

                obj.UserModificacion = user.NroDocumento;
                obj.FecModificacion = DateTime.Now;
                if (model.ArMotivoAdjuntoId == 0)
                {
                    obj.UserCreacion = user.NroDocumento;
                    obj.FecCreacion = DateTime.Now;
                }

                _ArMotivoAdjuntoService.Save(obj);

                return Json(new
                {
                    ArMotivoAdjuntoId = obj.ArMotivoAdjuntoId,
                    FileName = obj.NombreArchivo
                });
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _log.Error(ex);
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        public ActionResult Uploadword(ArMotivoAdjuntoModel model, string ExpedienteId, string NormaId, UsuarioInfo user)
        {
            try
            {
                string path = ConfigurationManager.AppSettings["Sut.PathUpload"].ToString();
                string filename = string.Format("file_{0}_{1}", user.EntidadId, DateTime.Now.ToString("yyyyMMdd_HHmmss"));

                if (path.Substring(0, 1) == "~") path = Server.MapPath(path);
                ArMotivoAdjunto obj = new ArMotivoAdjunto();
                obj.Extension = new FileInfo(model.PostedFile.FileName).Extension;
                //if (obj.Extension != ".pdf")
                //{
                //    return PartialView("_Error");
                //}
                if (obj.Extension.ToLower() != ".pdf" && obj.Extension.ToLower() != ".doc" && obj.Extension.ToLower() != ".docx")
                {
                    return PartialView("_Error");
                }
                model.PostedFile.SaveAs(path + filename + obj.Extension);


                if (model.ArMotivoAdjuntoId > 0)
                {
                    obj = _ArMotivoAdjuntoService.GetOne(model.ArMotivoAdjuntoId);
                    if (System.IO.File.Exists(path + obj.Ruta)) System.IO.File.Delete(path + obj.Ruta);
                }

                obj.Ruta = filename;
                obj.NombreArchivo = model.PostedFile.FileName;
                //obj.Extension = new FileInfo(model.PostedFile.FileName).Extension;
                obj.Tipo = model.PostedFile.ContentType;
                obj.Tamano = model.PostedFile.ContentLength;
                obj.Descripcion = model.PostedFile.FileName;
                obj.ExpedienteId = Convert.ToInt64(ExpedienteId);
                obj.NormaId = Convert.ToInt64(NormaId);

                obj.UserModificacion = user.NroDocumento;
                obj.FecModificacion = DateTime.Now;
                if (model.ArMotivoAdjuntoId == 0)
                {
                    obj.UserCreacion = user.NroDocumento;
                    obj.FecCreacion = DateTime.Now;
                }

                _ArMotivoAdjuntoService.Save(obj);

                return Json(new
                {
                    ArMotivoAdjuntoId = obj.ArMotivoAdjuntoId,
                    FileName = obj.NombreArchivo
                });
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _log.Error(ex);
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

        public ActionResult Descargar(long id)
        {
            try
            {
                string path = ConfigurationManager.AppSettings["Sut.PathUpload"].ToString();
                if (path.Substring(0, 1) == "~") path = Server.MapPath(path);

                ArMotivoAdjunto obj = _ArMotivoAdjuntoService.GetOne(id);
                System.IO.FileStream filestream = System.IO.File.Open(path + obj.Ruta + obj.Extension, FileMode.Open);
                return File(filestream, obj.Tipo, obj.NombreArchivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}