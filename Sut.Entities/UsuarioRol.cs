using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class UsuarioRol
    {
        [Key]
        public long UsuarioRolId { get; set; }

        public long UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public Entidad Entidad { get; set; }
        public long EntidadId { get; set; }


        public Rol Rol { get; set; }
        public int valor { get; set; }

        [NotMapped]
        public MiembroEquipo MiembroEquipo { get; set; }

        [NotMapped]
        public int chekActivar { get; set; }
    }
}