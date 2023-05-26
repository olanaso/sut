using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class Asignacion
    {
        [Key]
        public long AsignacionId { get; set; }

        public TipoSustento TipoSustento { get; set; }

        [ForeignKey("Procedimiento")]
        public long ProcedimientoId { get; set; }
        public Procedimiento Procedimiento { get; set; }

        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public bool Vigente { get; set; }

        public Asignacion()
        {
            Vigente = true;
        }
    }
}
