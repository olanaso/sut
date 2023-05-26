using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class UnidadOrganica
    {
        [Key]
        public long UnidadOrganicaId { get; set; }

        [Required, ForeignKey("Entidad")]
        public long EntidadId { get; set; }
        public Entidad Entidad { get; set; }

        [MaxLength(4000)]
        public string Nombre { get; set; }
        [Required]
        public bool Activo { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }

        public int Numero { get; set; }
        
        [NotMapped]
        public long SedeId { get; set; }
        [NotMapped]
        public string[] listaNombre { get; set; }

        [NotMapped]
        public string Nombre2 { get; set; }
        [NotMapped]
        public string Nombre3 { get; set; }
        [NotMapped]
        public string Nombre4 { get; set; }
        [NotMapped]
        public string Nombre5 { get; set; }



    }
}
