using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class Comentario
    {
        [Key]
        public long ComentarioId { get; set; }

        [ForeignKey("ComentarioSeccion")]
        public long ComentarioSeccionId { get; set; }
        public ComentarioSeccion ComentarioSeccion { get; set; }

        [Required]
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        [MaxLength(500), Required]
        public string Mensaje { get; set; }
    }
}
