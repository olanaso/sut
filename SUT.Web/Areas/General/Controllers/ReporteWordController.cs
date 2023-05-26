
using iTextSharp.text;
using Microsoft.Reporting.WebForms;
using Sut.IApplicationServices;
using Sut.Log;
using System;
using System.Configuration;
using System.Web.Mvc;



namespace Sut.Web.Areas.General.Controllers
{

    public class ReporteWordController : Controller
    {

        string path = ConfigurationManager.AppSettings["Sut.PathUpload"].ToString();
        string pathdocumentos = ConfigurationManager.AppSettings["Sut.PathUploadDocumentos"].ToString();
        string pathdocumentosCiudadano = ConfigurationManager.AppSettings["Sut.PathUploadDocumentosCiudadano"].ToString();
        string pathLinkarchivos = ConfigurationManager.AppSettings["Sut.PathLinkarchivos"].ToString();
        string estadoformulario = "";
        Font fontsTituloNormal = FontFactory.GetFont("Arial", 8, Font.NORMAL);
        Font fontsTituloNegrita = FontFactory.GetFont("Arial", 10, Font.BOLD);
        Font fontsSubTituloNormal = FontFactory.GetFont("Arial", 9, Font.BOLD);
        Font fontsLetraNormal = FontFactory.GetFont("Arial", 7, Font.NORMAL);
        Font fontssubLetraNormal = FontFactory.GetFont("Arial", 6, Font.NORMAL);
        Font fontsNormal = FontFactory.GetFont("Arial", 8, Font.NORMAL);
        Font fontsNormalUnderline = FontFactory.GetFont("Arial", 5, Font.ITALIC + Font.UNDERLINE);
        Font fontsNegrita = FontFactory.GetFont("Arial", 7, Font.BOLD);
        Font fontsUnderline = FontFactory.GetFont("Arial", 5, Font.BOLDITALIC + Font.UNDERLINE);
        Font fontsTransparente = FontFactory.GetFont(FontFactory.TIMES, 5, Font.NORMAL, iTextSharp.text.Color.WHITE);
        Font fontsCabFooter = FontFactory.GetFont("Arial", 8, Font.BOLD);
        Font fontsDetalle = FontFactory.GetFont("Arial", 4, Font.NORMAL);

        ReportDataSource sub_rds1;
        ReportDataSource sub_rds2;
        ReportDataSource sub_rds3;
        ReportDataSource sub_rds4;
        ReportDataSource sub_rds5;
        ReportDataSource sub_rds8;
        int pagcab;
        int pagcab2;
        int pagcabCompleto;
        int pagcabhoja;
        ReportDataSource sub_rds6;


        public string[] tipo = new string[8];

        private readonly ILogService<ReporteWordController> _log;
        private readonly IReporteService _reporteService;
        private readonly ITablaAsmeService _tablaAsmeService;
        private readonly IActividadService _actividadService;
        private readonly IProcedimientoService _procedimientoService;
        private readonly IRequisitoService _requisitoService;
        private readonly IBaseLegalService _baseLegalService;
        private readonly IDatoService _datoService;
        private readonly IEntidadService _entidadService;
        private readonly IExpedienteService _expedienteService;
        private readonly ISedeService _sedeService;
        private readonly IExpedienteNormaService _expedienteNormaService;
        private readonly IEnumeradoService _enumeradoService;
        private readonly IArchivoAdjuntoService _archivoAdjuntoService;
        private readonly IUnidadOrganicaService _unidadOrganicaService;
        private readonly ITablaAsmeReproduccionService _tablaAsmeReproduccionService;
        private readonly IPlazoAtencionService _plazoAtencionService;
        private readonly IIncentivosFormulasService _IncentivosFormulasService;
        private readonly IIncentivosFormulasCorteService _IncentivosFormulasCorteService;
        private readonly IIncentivosService _IncentivosService;
        private readonly IIncentivosCorteService _IncentivosCorteService;
        private readonly IAuditoriaService _AuditoriaService;
        private readonly IRecursoCostoService _recursoCostoService;
        private readonly IInductorService _inductorService;
        private readonly IFactorDedicacionService _factorDedicacionService;


        private readonly IInformeService _InformeService;

        private readonly IProcedimientoCargosService _procedimientoCargosService;
        private readonly IProcedimientoCargosApeService _procedimientoCargosApeService;
        private readonly IProcedimientoCargosOtrosService _procedimientoCargosOtrosService;

        private readonly IProcedimientoUndOrgResponsableService _ProcedimientoUndOrgResponsableService;
        public ReporteWordController(IReporteService reporteService,
                                ITablaAsmeService tablaAsmeService,
                                IActividadService actividadService,
                                IProcedimientoService procedimientoService,
                                IRequisitoService requisitoService,
                                IBaseLegalService baseLegalService,
                                IDatoService datoService,
                                IEntidadService entidadService,
                                IExpedienteService expedienteService,
                                ISedeService sedeService,
                                IExpedienteNormaService expedienteNormaService,
                                IEnumeradoService enumeradoService,
                                IUnidadOrganicaService unidadOrganicaService,
                                IArchivoAdjuntoService archivoAdjuntoService,
                                ITablaAsmeReproduccionService tablaAsmeReproduccionService,
                                IPlazoAtencionService plazoAtencionService,
                                IIncentivosFormulasService IncentivosFormulasService,
                                IIncentivosFormulasCorteService IncentivosFormulasCorteService,
                                IIncentivosService incentivosService,
                                IIncentivosCorteService incentivosCorteService,
                                IAuditoriaService AuditoriaService,
                                IRecursoCostoService recursoCostoService,
                                IInductorService inductorService,
                                IFactorDedicacionService factorDedicacionService,
                                IProcedimientoCargosService procedimientoCargosService,
                                IProcedimientoCargosApeService procedimientoCargosApeService,
                                        IProcedimientoUndOrgResponsableService procedimientoUndOrgResponsableService,
                                        IProcedimientoCargosOtrosService procedimientoCargosOtrosService,
                                        IInformeService informeService)
        {
            _log = new LogService<ReporteWordController>();
            _reporteService = reporteService;
            _tablaAsmeService = tablaAsmeService;
            _actividadService = actividadService;
            _procedimientoService = procedimientoService;
            _requisitoService = requisitoService;
            _baseLegalService = baseLegalService;
            _datoService = datoService;
            _entidadService = entidadService;
            _expedienteService = expedienteService;
            _sedeService = sedeService;
            _expedienteNormaService = expedienteNormaService;
            _enumeradoService = enumeradoService;
            _archivoAdjuntoService = archivoAdjuntoService;
            _unidadOrganicaService = unidadOrganicaService;
            _tablaAsmeReproduccionService = tablaAsmeReproduccionService;
            _plazoAtencionService = plazoAtencionService;
            _IncentivosFormulasService = IncentivosFormulasService;
            _IncentivosFormulasCorteService = IncentivosFormulasCorteService;
            _IncentivosService = incentivosService;
            _IncentivosCorteService = incentivosCorteService;
            _AuditoriaService = AuditoriaService;
            _recursoCostoService = recursoCostoService;
            _inductorService = inductorService;
            _factorDedicacionService = factorDedicacionService;
            _InformeService = informeService;

            _procedimientoCargosService = procedimientoCargosService;
            _procedimientoCargosApeService = procedimientoCargosApeService;

            _ProcedimientoUndOrgResponsableService = procedimientoUndOrgResponsableService;
            _procedimientoCargosOtrosService = procedimientoCargosOtrosService;
        }



        public ActionResult ReporteTupa(long ExpedienteId, int orden, int ProcedimientoId)
        {
            try
            {








                //Warning[] warn = null;
                //string[] streamids = null;
                //string mime = string.Empty, encoding = string.Empty, extension = string.Empty;
                //byte[] byteViewer;
                //ReportViewer viewer;
                //PdfReader pdfReader;

                //List<ItemIndice> lstIndice = new List<ItemIndice>();
                //var expediente = _expedienteService.GetOne(ExpedienteId);
                //var entidad = _entidadService.GetOne(expediente.EntidadId);
                //var dataProc = _procedimientoService.GetByExpediente(ExpedienteId).ToList().Where(x => x.ProcedimientoId == ProcedimientoId).OrderBy(x => x.Numero);

                //if (ProcedimientoId == 0)
                //{
                //    dataProc = _procedimientoService.GetByExpediente(ExpedienteId).Where(x => x.Operacion != OperacionExpediente.Eliminacion && x.CodigoCorto != null).ToList().OrderBy(x => x.Numero);
                //}
                //else
                //{
                //    dataProc = _procedimientoService.GetByExpediente(ExpedienteId).ToList().Where(x => x.ProcedimientoId == ProcedimientoId).OrderBy(x => x.Numero);
                //}

                //Document Doc = new Document(PageSize.A4, 60, 60, 35, 50);

                //PdfWriter writer = PdfWriter.GetInstance(Doc, new FileStream(@pathdocumentos + "Archivo" + ExpedienteId + "_Inicio.pdf", FileMode.Create));
                ////Phrase obje = new Phrase(fnChunk("pág. ", (int)Fuente.FuenteCabFooter));
                ////HeaderFooter footer = new HeaderFooter(obje, new Phrase(""));
                ////footer.Border = 0;
                //////footer.BorderWidthTop = 1;
                ////footer.Alignment = Element.ALIGN_RIGHT;
                ////Doc.Footer = footer;

                ////inicio
                //string cadena1 = "Texto Único de Procedimientos Administrativos - " + "\"" + entidad.Nombre.ToString() + "\"";
                //Font fontHeaderFooter = FontFactory.GetFont("Arial", 8f, Font.BOLD);
                //Chunk chkHeader = new Chunk(cadena1, fontHeaderFooter);
                //Phrase p1 = new Phrase(chkHeader);
                //HeaderFooter footer2 = new HeaderFooter(p1, false);
                //footer2.Border = Rectangle.NO_BORDER;
                //footer2.Alignment = Element.ALIGN_CENTER;
                //footer2.BorderColor = new Color(242, 242, 242);
                //footer2.BackgroundColor = new Color(242, 242, 242);
                //Doc.Header = footer2;
                ////fin

                ////ocultar informacion cuando es individual
                //if (ProcedimientoId == 0)
                //{
                //    GenerarCaratula(Doc, ExpedienteId, ProcedimientoId);
                //    Doc.Close();

                //    //GenerarIndice(Doc, ExpedienteId, ProcedimientoId); 
                //    GenerarIndiceConteo( ExpedienteId, ProcedimientoId);

                //    //GenerarIndiceCompleto( ExpedienteId, ProcedimientoId);


                //    //TituloProceAdm(Doc, ExpedienteId, ProcedimientoId);

                //    TituloProceAdmCompleto( ExpedienteId, ProcedimientoId);

                //}
                //else
                //{

                //    Doc.Open();

                //}


                //var pageBorderRect = new Rectangle(Doc.PageSize);

                //pageBorderRect.Left += Doc.LeftMargin;
                //pageBorderRect.Right -= Doc.RightMargin;
                //pageBorderRect.Top -= Doc.TopMargin;
                //pageBorderRect.Bottom += Doc.BottomMargin;



                ////PdfContentByte cb = writer.DirectContent;
                ////// los costados, ancho, alto, curva
                ////cb.RoundRectangle(50f, 40f, 495f, 752f, 3f);
                ////cb.Stroke();

                //if (ProcedimientoId != 0)
                //{


                //    foreach (Procedimiento proc2 in dataProc)
                //    {
                //        if (proc2.TipoProcedimiento == TipoProcedimiento.Servicio || proc2.TipoProcedimiento == TipoProcedimiento.EstandarServicio)
                //        {
                //            GenerarPresExc(Doc, ExpedienteId, ProcedimientoId, writer);
                //            if (estadoformulario != "")
                //            {
                //                TituloForm(Doc, ExpedienteId, ProcedimientoId, "FORMULARIOS");
                //                Doc.NewPage();
                //            }
                //            IndiceConteo(ExpedienteId, ProcedimientoId, "S");
                //            Doc.Close();
                //        }
                //        else
                //        {

                //            GenerarProceAdm(Doc, ExpedienteId, ProcedimientoId, writer);
                //            if (estadoformulario != "")
                //            {
                //                TituloForm(Doc, ExpedienteId, ProcedimientoId, "FORMULARIOS");
                //                Doc.NewPage();
                //            }
                //            IndiceConteo(ExpedienteId, ProcedimientoId, "P");
                //            Doc.Close();
                //        }

                //    }



                //}
                //else
                //{

                //    GenerarProceAdmCompleto(  ExpedienteId, ProcedimientoId, writer);
                //    //Doc.NewPage();  


                //    //TituloPresExc(Doc, ExpedienteId, ProcedimientoId);
                //    TituloPresExcCompleto(ExpedienteId, ProcedimientoId);
                //    //Doc.NewPage();


                //    //cb.RoundRectangle(50f, 40f, 495f, 752f, 3f);
                //    //cb.Stroke();

                //    //GenerarPresExc(Doc, ExpedienteId, ProcedimientoId, writer);
                //    GenerarPresExcCompleto(ExpedienteId, ProcedimientoId, writer);


                //     //TituloForm(Doc, ExpedienteId, ProcedimientoId, "SECCIÓN N° 3: FORMULARIOS");
                //    TituloFormCompleto(  ExpedienteId, ProcedimientoId, "SECCIÓN N° 3: FORMULARIOS");
                //    //Doc.NewPage();

                //    //Doc.Close();
                //}


                //if (ProcedimientoId != 0)
                //{
                //Phrase obje = new Phrase(fnChunk("pág. ", (int)Fuente.FuenteCabFooter));
                //HeaderFooter footer = new HeaderFooter(obje, new Phrase(""));
                //Document Doc3 = new Document(PageSize.A4, 60, 60, 30, 50);
                //PdfWriter.GetInstance(Doc3, new FileStream(@pathdocumentos + "Archivo" + ExpedienteId + "_Final.pdf", FileMode.Create));

                //for (int i = 1; i < pagcab; i++)
                //{
                //    Doc3.Open();
                //}


                ////footer.Border = 0;
                //////footer.BorderWidthTop = 1;
                ////footer.Alignment = Element.ALIGN_RIGHT;
                ////Doc.Footer = footer;
                //footer.Border = 0;
                //footer.BorderWidthTop = 1;
                //footer.Alignment = Element.ALIGN_CENTER;
                //Doc3.Footer = footer;
                ////inicio 
                //footer2.Border = Rectangle.NO_BORDER;
                //footer2.Alignment = Element.ALIGN_CENTER;
                //footer2.BorderColor = new Color(242, 242, 242);
                //footer2.BackgroundColor = new Color(242, 242, 242);
                //Doc3.Header = footer2;
                ////fin
                //if (ProcedimientoId != 0)
                //{
                //    TituloSedes(Doc3, ExpedienteId, ProcedimientoId, "SEDES DE ATENCIÓN");
                //}
                //else
                //{
                //    TituloSedes(Doc3, ExpedienteId, ProcedimientoId, "SECCIÓN N° 4: SEDES DE ATENCIÓN");
                //}
                //Doc3.NewPage();

                ////cb.RoundRectangle(50f, 40f, 495f, 752f, 3f);
                ////cb.Stroke();

                //GenerarSedes(Doc3, ExpedienteId, ProcedimientoId);
                //Doc3.NewPage();
                //Doc3.Close();

                //}
                //else
                //{

                //    TituloSedesCompleto( ExpedienteId, ProcedimientoId, "SECCIÓN N° 4: SEDES DE ATENCIÓN");

                //    //cb.RoundRectangle(50f, 40f, 495f, 752f, 3f);
                //    //cb.Stroke();

                //    GenerarSedesCompleto(ExpedienteId, ProcedimientoId); 

                //}
                ////Phrase obje = new Phrase(fnChunk("pág. ", (int)Fuente.FuenteCabFooter));
                //Document Doc2 = new Document(PageSize.A4, 60, 60, 30, 50);
                ////HeaderFooter footer3 = new HeaderFooter(obje, new Phrase(""));
                ////footer3.Border = 0;
                ////footer3.BorderWidthTop = 1;
                ////footer3.Alignment = Element.ALIGN_CENTER;
                ////Doc2.Footer = footer3;


                ////cb.RoundRectangle(50f, 40f, 495f, 752f, 3f);
                ////cb.Stroke();

                //if (ProcedimientoId == 0)
                //{  
                //    GenerarIndiceCompleto(ExpedienteId, ProcedimientoId);

                //} 

                //if (estadoformulario != "")
                //{
                //    GenerarForm(Doc2, ExpedienteId, expediente.EntidadId, ProcedimientoId, 0);
                //    Doc2.Close();
                //}
                //else
                //{

                //    UnirForm(Doc2, ExpedienteId, expediente.EntidadId, ProcedimientoId, 0);
                //    Doc2.Close();

                //}
                //Doc.Close();


                string Nombre = "Expediente_" + ExpedienteId + ".pdf";

                string Ruta = @pathdocumentos + "Archivo" + ExpedienteId + "_EXPORTAR.pdf";
                //Response.ClearContent();
                //Response.ClearHeaders();
                //Response.ContentType = "application/ms-word";
                //Response.AddHeader("Content-Disposition", "attachment;filename=" + Nombre + "");
                //Response.WriteFile(Ruta);
                //Response.Flush();
                //Response.Close();

                return Json(new { valid = true, nombre = Nombre, ruta = Ruta }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                //Document Doc = new Document(PageSize.A4, 60, 60, 35, 50);
                //Doc.Close();
                //_log.Error(ex);
                //throw ex;
                var mensaje = "Error message: " + ex.Message;
                _log.Error(ex);
                //Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", mensaje);
                return PartialView("_Error");
                //return ex.ToString();
            }
        }



        public void descargarArchivos(String Nombre, String Ruta)
        {


            try
            {
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + Nombre + "");
                Response.WriteFile(Ruta);
                Response.Flush();
                Response.Close();

            }
            catch (Exception ex)
            {
                var mensaje = "Error message: " + ex.Message;
                _log.Error(ex);
                throw ex;
                //return ex.ToString(); 
            }
        }






    }



}

