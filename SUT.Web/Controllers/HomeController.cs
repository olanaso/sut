using iTextSharp.text;
using iTextSharp.text.pdf;
using Sut.Security;
using System;
using System.Web.Mvc;

namespace Sut.Web.Controllers
{

    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index(UsuarioInfo user)
        {
            //if (user.Rol == (short)Entities.Rol.Administrador)
            //    return RedirectToAction("Lista", "Tupa", new { area = "Seguridad" });
            //else
            return RedirectToAction("Lista", "Portada", new { area = "Seguridad" });

            //return Json(Cryptography.Encrypt("1234"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PruebaPDF()
        {
            string[] files = System.IO.Directory.GetFiles("D:\\temp");

            bool exito = Merge("D:\\temp.pdf", files);

            //return Json(new { exito }, JsonRequestBehavior.AllowGet);

            var filestream = new System.IO.FileStream("D:\\temp.pdf", System.IO.FileMode.Open, System.IO.FileAccess.Read);
            return new FileStreamResult(filestream, "application/pdf");
        }

        private bool Merge(string strFileTarget, string[] arrStrFilesSource)
        {
            bool blnMerged = false;

            // Crea el PDF de salida
            try
            {
                using (System.IO.FileStream stmFile = new System.IO.FileStream(strFileTarget, System.IO.FileMode.Create))
                {
                    Document objDocument = null;
                    PdfWriter objWriter = null;

                    // Recorre los archivos
                    for (int intIndexFile = 0; intIndexFile < arrStrFilesSource.Length; intIndexFile++)
                    {
                        PdfReader objReader = new PdfReader(arrStrFilesSource[intIndexFile]);
                        int intNumberOfPages = objReader.NumberOfPages;

                        // La primera vez, inicializa el documento y el escritor
                        if (intIndexFile == 0)
                        { // Asigna el documento y el generador
                            objDocument = new Document(objReader.GetPageSizeWithRotation(1));
                            objWriter = PdfWriter.GetInstance(objDocument, stmFile);
                            // Abre el documento
                            objDocument.Open();
                        }
                        // Añade las páginas
                        for (int intPage = 0; intPage < intNumberOfPages; intPage++)
                        {
                            int intRotation = objReader.GetPageRotation(intPage + 1);
                            PdfImportedPage objPage = objWriter.GetImportedPage(objReader, intPage + 1);

                            // Asigna el tamaño de la página
                            objDocument.SetPageSize(objReader.GetPageSizeWithRotation(intPage + 1));
                            // Crea una nueva página
                            objDocument.NewPage();
                            // Añade la página leída
                            if (intRotation == 90 || intRotation == 270)
                                objWriter.DirectContent.AddTemplate(objPage, 0, -1f, 1f, 0, 0,
                                                                    objReader.GetPageSizeWithRotation(intPage + 1).Height);
                            else
                                objWriter.DirectContent.AddTemplate(objPage, 1f, 0, 0, 1f, 0, 0);
                        }
                    }
                    // Cierra el documento
                    if (objDocument != null)
                        objDocument.Close();
                    // Cierra el stream del archivo
                    stmFile.Close();
                }
                // Indica que se ha creado el documento
                blnMerged = true;
            }
            catch (Exception objException)
            {
                System.Diagnostics.Debug.WriteLine(objException.Message);
            }
            // Devuelve el valor que indica si se han mezclado los archivos
            return blnMerged;
        }
    }
}