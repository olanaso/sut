using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class DetalleMaestro
    {
        [Key]
        public long DetalleMaestroId { get; set; }
 

        public TipoDetalleMaestro TipoDetalleMaestro { get; set; }
        [MaxLength(80)]
        public string Nombre { get; set; }
         
        public TipoRecurso TipoRecurso { get; set; }

        [ForeignKey("TipoDepreciacion")]
        public long? TipoDepreciacionId { get; set; }
        public MetaDato TipoDepreciacion { get; set; }

        [ForeignKey("UnidadMedida")]
        public long? UnidadMedidaId { get; set; }
        public UnidadMedida UnidadMedida { get; set; }

        public bool Activo { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }
          
        [NotMapped]
        public decimal DuracionTotal { get; set; }

        [NotMapped]
        public string[] listaNombre { get; set; }
        public string[] listaUnidadMedidaId { get; set; }
        public string[] listaTipoDepreciacionId { get; set; } 

    }
}
