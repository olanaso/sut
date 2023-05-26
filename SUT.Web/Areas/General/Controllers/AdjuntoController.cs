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
    public class AdjuntoController : Controller
    {
        private readonly ILogService<AdjuntoController> _log;
        private readonly IArchivoAdjuntoService _archivoAdjuntoService;

        public AdjuntoController(IArchivoAdjuntoService archivoAdjuntoService)
        {
            _log = new LogService<AdjuntoController>();
            _archivoAdjuntoService = archivoAdjuntoService;
        }

        public ActionResult Upload(ArchivoAdjuntoModel model, UsuarioInfo user)
        {
            try
            {
                string path = ConfigurationManager.AppSettings["Sut.PathUpload"].ToString();
                string filename = string.Format("file_{0}_{1}", user.EntidadId, DateTime.Now.ToString("yyyyMMdd_HHmmss"));

                if (path.Substring(0, 1) == "~") path = Server.MapPath(path);
                ArchivoAdjunto obj = new ArchivoAdjunto();
                obj.Extension = new FileInfo(model.PostedFile.FileName).Extension;
                //revisar
                //if (obj.Extension != ".pdf" && obj.Extension != ".xlsx" && obj.Extension != ".xlsx" && obj.Extension != ".doc" && obj.Extension != ".docx" && obj.Extension != ".ppt" && obj.Extension != ".pptx")
                //{
                //    return PartialView("_Error");
                //}
                if (obj.Extension.ToLower() != ".pdf")
                {
                    return PartialView("_Error");
                }
                model.PostedFile.SaveAs(path + filename + obj.Extension);


                if (model.ArchivoAdjuntoId > 0)
                {
                    obj = _archivoAdjuntoService.GetOne(model.ArchivoAdjuntoId);
                    if (System.IO.File.Exists(path + obj.Ruta)) System.IO.File.Delete(path + obj.Ruta);
                }

                obj.Ruta = filename;
                obj.NombreArchivo = model.PostedFile.FileName;
                obj.Extension = new FileInfo(model.PostedFile.FileName).Extension;
                obj.Tipo = model.PostedFile.ContentType;
                obj.Tamano = model.PostedFile.ContentLength;
                obj.Descripcion = model.PostedFile.FileName;

                obj.UserModificacion = user.NroDocumento;
                obj.FecModificacion = DateTime.Now;
                if (model.ArchivoAdjuntoId == 0)
                {
                    obj.UserCreacion = user.NroDocumento;
                    obj.FecCreacion = DateTime.Now;
                }

                _archivoAdjuntoService.Save(obj);

                return Json(new
                {
                    ArchivoAdjuntoId = obj.ArchivoAdjuntoId,
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

        public ActionResult UploadTodo(ArchivoAdjuntoModel model, UsuarioInfo user)
        {
            try
            {
                string path = ConfigurationManager.AppSettings["Sut.PathUpload"].ToString();
                string filename = string.Format("file_{0}_{1}", user.EntidadId, DateTime.Now.ToString("yyyyMMdd_HHmmss"));

                if (path.Substring(0, 1) == "~") path = Server.MapPath(path);
                ArchivoAdjunto obj = new ArchivoAdjunto();
                obj.Extension = new FileInfo(model.PostedFile.FileName).Extension;
                //revisar
                if (obj.Extension.ToLower() != ".pdf" && obj.Extension.ToLower() != ".xls" && obj.Extension.ToLower() != ".xlsx" && obj.Extension.ToLower() != ".doc" && obj.Extension.ToLower() != ".docx" && obj.Extension.ToLower() != ".ppt" && obj.Extension.ToLower() != ".pptx")
                {
                    return PartialView("_Error");
                }
                //if (obj.Extension != ".pdf")
                //{
                //    return PartialView("_Error");
                //}
                model.PostedFile.SaveAs(path + filename + obj.Extension);


                if (model.ArchivoAdjuntoId > 0)
                {
                    obj = _archivoAdjuntoService.GetOne(model.ArchivoAdjuntoId);
                    if (System.IO.File.Exists(path + obj.Ruta)) System.IO.File.Delete(path + obj.Ruta);
                }

                obj.Ruta = filename;
                obj.NombreArchivo = model.PostedFile.FileName;
                obj.Extension = new FileInfo(model.PostedFile.FileName).Extension;
                obj.Tipo = model.PostedFile.ContentType;
                obj.Tamano = model.PostedFile.ContentLength;
                obj.Descripcion = model.PostedFile.FileName;

                obj.UserModificacion = user.NroDocumento;
                obj.FecModificacion = DateTime.Now;
                if (model.ArchivoAdjuntoId == 0)
                {
                    obj.UserCreacion = user.NroDocumento;
                    obj.FecCreacion = DateTime.Now;
                }

                _archivoAdjuntoService.Save(obj);

                return Json(new
                {
                    ArchivoAdjuntoId = obj.ArchivoAdjuntoId,
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

        public ActionResult Uploadword(ArchivoAdjuntoModel model, UsuarioInfo user)
        {
            try
            {
                string path = ConfigurationManager.AppSettings["Sut.PathUpload"].ToString();
                string filename = string.Format("file_{0}_{1}", user.EntidadId, DateTime.Now.ToString("yyyyMMdd_HHmmss"));

                if (path.Substring(0, 1) == "~") path = Server.MapPath(path);
                ArchivoAdjunto obj = new ArchivoAdjunto();
                obj.Extension = new FileInfo(model.PostedFile.FileName).Extension;
                //revisar
                //if (obj.Extension != ".pdf" && obj.Extension != ".xlsx" && obj.Extension != ".xlsx" && obj.Extension != ".doc" && obj.Extension != ".docx" && obj.Extension != ".ppt" && obj.Extension != ".pptx")
                //{
                //    return PartialView("_Error");
                //}
                if (obj.Extension.ToLower() != ".pdf" && obj.Extension.ToLower() != ".doc" && obj.Extension.ToLower() != ".docx" && obj.Extension.ToLower() != ".xls" && obj.Extension.ToLower() != ".xlsx")
                {
                    return PartialView("_Error");
                }
                //if (obj.Extension != ".pdf")
                //{
                //    return PartialView("_Error");
                //}
                model.PostedFile.SaveAs(path + filename + obj.Extension);


                if (model.ArchivoAdjuntoId > 0)
                {
                    obj = _archivoAdjuntoService.GetOne(model.ArchivoAdjuntoId);
                    if (System.IO.File.Exists(path + obj.Ruta)) System.IO.File.Delete(path + obj.Ruta);
                }

                obj.Ruta = filename;
                obj.NombreArchivo = model.PostedFile.FileName;
                obj.Extension = new FileInfo(model.PostedFile.FileName).Extension;
                obj.Tipo = model.PostedFile.ContentType;
                obj.Tamano = model.PostedFile.ContentLength;
                obj.Descripcion = model.PostedFile.FileName;

                obj.UserModificacion = user.NroDocumento;
                obj.FecModificacion = DateTime.Now;
                if (model.ArchivoAdjuntoId == 0)
                {
                    obj.UserCreacion = user.NroDocumento;
                    obj.FecCreacion = DateTime.Now;
                }

                _archivoAdjuntoService.Save(obj);

                return Json(new
                {
                    ArchivoAdjuntoId = obj.ArchivoAdjuntoId,
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

                ArchivoAdjunto obj = _archivoAdjuntoService.GetOne(id);
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