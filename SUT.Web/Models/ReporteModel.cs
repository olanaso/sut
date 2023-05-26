using Microsoft.Reporting.WebForms;
using System.Collections.Generic;

namespace Sut.Web.Models
{
    public class ReporteModel
    {
        public string mimeType { get; set; }
        public string extension { get; set; }
        public string File { get; set; }
        public List<ReportParameter> Parametros { get; set; }
        public List<ReportDataSource> Source { get; set; }

        public ReporteModel()
        {
            this.Parametros = new List<ReportParameter>();
            this.Source = new List<ReportDataSource>();
        }

        public byte[] DescargarPDF()
        {
            return Descargar("pdf");
        }

        public byte[] DescargarExcel()
        {
            return Descargar("EXCELOPENXML");
        }

        public byte[] Descargar(string tipo)
        {
            Warning[] warn = null;
            string[] streamids = null;
            string mime = string.Empty, encoding = string.Empty, ext = string.Empty;
            byte[] byteViewer;
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = this.File;
            viewer.LocalReport.SetParameters(this.Parametros);

            foreach (ReportDataSource source in this.Source)
                viewer.LocalReport.DataSources.Add(source);

            byteViewer = viewer.LocalReport.Render(tipo, null, out mime, out encoding, out ext, out streamids, out warn);
            this.mimeType = mime;
            this.extension = ext;
            return byteViewer;
        }
    }
}