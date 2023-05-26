using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class ExpedienteNorma
    {
        [Key]
        public long NormaId { get; set; }

        [ForeignKey("Expediente")]
        public long ExpedienteId { get; set; }
        public Expediente Expediente { get; set; }

        //[ForeignKey("ArchivoAdjunto")]
        public long? ArchivoAdjuntoId { get; set; }
        public ArchivoAdjunto ArchivoAdjunto { get; set; }

        [ForeignKey("ArMotivoAdjunto")]
        public long? ArMotivoAdjuntoId { get; set; }
        public ArMotivoAdjunto ArMotivoAdjunto { get; set; }


        [ForeignKey("TipoNorma")]
        public long TipoNormaId { get; set; }
        public MetaDato TipoNorma { get; set; }

        [MaxLength(300)]
        public string Descripcion { get; set; }
        public DateTime? Fecha { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }
        public int ENUM_TIPO_NORMA_APROBACION { get; set; }
        public int EstadoPublicado { get; set; }

        
    }
}
