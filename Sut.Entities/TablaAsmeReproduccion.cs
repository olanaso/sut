using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class TablaAsmeReproduccion
    {
        [Key]
        public long ReproduccionId { get; set; }

        [ForeignKey("TablaAsme")]
        public long? TablaAsmeId { get; set; }
        public TablaAsme TablaAsme { get; set; }
         
        public string Descripcion { get; set; }
        public string Sustento { get; set; } 

        public decimal Costo { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }

      
    }
}
