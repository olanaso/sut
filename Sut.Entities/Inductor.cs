using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class Inductor
    {
        [Key]
        public long InductorId { get; set; }

        [Required, ForeignKey("Entidad")]
        public long EntidadId { get; set; }
        public Entidad Entidad { get; set; }

        [MaxLength(10)]
        public string Codigo { get; set; }
        [MaxLength(50)]
        public string Nombre { get; set; }

        public bool Activo { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }

        public Inductor()
        {
            Activo = true;
        }

        [NotMapped]
        public decimal ValorTotal { get; set; }

        [NotMapped]
        public string[] listaNombre { get; set; }
        public string[] chkbuscarUndOrg { get; set; }
        public string[] listaNombreCargado { get; set; } 
        

    }
}
