using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class Tupa
    {
        [Key]
        public long TupaId { get; set; }
        public TipoTupa TipoTupa { get; set; }
        public EstadoTupa EstadoTupa { get; set; }

        //[ForeignKey("Expediente")]
        public long ExpedienteId { get; set; }
        //public Expediente Expediente { get; set; }

        //[ForeignKey("Entidad")]
        public long EntidadId { get; set; }
        //public Entidad Entidad { get; set; }

        public DateTime? FecPublicacion { get; set; }

        public List<Procedimiento> Procedimiento { get; set; }

        public Tupa()
        {
            this.Procedimiento = new List<Procedimiento>();
        }
    }
}
