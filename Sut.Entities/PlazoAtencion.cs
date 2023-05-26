using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class PlazoAtencion
    {
        [Key]
        public long PlazoAtencionId { get; set; }

        [ForeignKey("Procedimiento")]
        public long? ProcedimientoId { get; set; }
        public Procedimiento Procedimiento { get; set; }
         
        public string Descripcion { get; set; }
        public string Sustento { get; set; } 

        public int Plazo { get; set; }


        public TipoPlazo TipoPlazo { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }

      
    }
}
