using Sut.Entities;

namespace Sut.Web.Areas.Simplificacion.Models
{
    public class CopiaProcedimientoViewModel
    {
        public long ExpedienteId { get; set; }
        public long ProcedimientoOrigenId { get; set; }
        public long ProcedimientoDestinoId { get; set; }

        public bool Todo { get; set; }
        public bool InfoAlCiudadano { get; set; }
        public bool InfoBasica { get; set; }
        public bool TipoAtencion { get; set; }
        public bool Requisito { get; set; }
        public bool BaseLegal { get; set; }
        public bool DatosGeneralesYSTL { get; set; }
        public bool SustentoCalificacion { get; set; }
        public bool SustentoRequisito { get; set; }
        public bool TablaASME { get; set; }
        public bool Nuevo { get; set; }
        public string CodigoCortoCopia { get; set; }
        public string CodigoCopia { get; set; }
        public TipoExpediente TipoExpediente { get; set; }
        public long Numero { get; set; }
    }
}