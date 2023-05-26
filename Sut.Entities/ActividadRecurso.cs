using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class ActividadRecurso
    {
        [Key, Column(Order = 0), ForeignKey("Actividad")]
        public long ActividadId { get; set; }
        public Actividad Actividad { get; set; }

        [Key, Column(Order = 1), ForeignKey("Recurso")]
        public long RecursoId { get; set; }
        public Recurso Recurso { get; set; }

        public decimal Cantidad { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }
         
    }
}
