using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class NotaCiudadano
    {
        [Key, Column(Order = 0), ForeignKey("Procedimiento")]
        public long ProcedimientoId { get; set; }
        public Procedimiento Procedimiento { get; set; }

        [Key, Column(Order = 1)]
        public long NotaCiudadanoId { get; set; }

        [ForeignKey("TipoNota")]
        public long TipoNotaId { get; set; }
        public MetaDato TipoNota { get; set; }

        public int? Orden { get; set; }
        public string Nota { get; set; }
    }
}
