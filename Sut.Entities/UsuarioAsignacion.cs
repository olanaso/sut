using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class UsuarioAsignacion
    {
        [Key]
        public long UsuarioAsignacionId { get; set; }       
        public long UsuarioId { get; set; }
        public long EntidadId { get; set; } 
        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }
       


    }
}
