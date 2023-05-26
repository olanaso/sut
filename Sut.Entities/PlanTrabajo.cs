using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class PlanTrabajo
    {
        [Key]
        public long PlanTrabajoId { get; set; }
        [ForeignKey("Entidad")]
        public long EntidadId { get; set; }
        public Entidad Entidad { get; set; }

        [ForeignKey("ArchivoAdjunto")]
        public long? ArchivoAdjuntoId { get; set; }
        public ArchivoAdjunto ArchivoAdjunto { get; set; }

         

        [MaxLength(200)]
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }


        public string Principal { get; set; }
    }
}
