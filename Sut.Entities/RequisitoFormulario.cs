using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class RequisitoFormulario
    {
        [Key, Column(Order = 0), ForeignKey("Requisito")]
        public long RequisitoId { get; set; }
        public Requisito Requisito { get; set; }

        [Key, Column(Order = 1)]
        public long FormularioId { get; set; }

        public string Nombre { get; set; }
        public string Url { get; set; }

        [ForeignKey("ArchivoAdjunto")]
        public long? ArchivoAdjuntoId { get; set; }
        public ArchivoAdjunto ArchivoAdjunto { get; set; }

        public int Eliminado { get; set; }

        

        [NotMapped]
        public long ProcedimientoId { get; set; }

        [NotMapped]
        public long ExpedienteId { get; set; }

    }
}
