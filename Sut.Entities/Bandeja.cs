using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class Bandeja
    {
        [Key]
        public long BandejaId { get; set; }

        [Required, ForeignKey("Expediente")]
        public long ExpedienteId { get; set; }
        public Expediente Expediente { get; set; }

        [Required, ForeignKey("Usuario")]
        public long UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

         
        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }
         
        public int Estado { get; set; }
         
        [NotMapped] 
        public string[] chkbuscarUsuario { get; set; } 
        

    }
}
