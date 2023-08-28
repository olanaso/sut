using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    class UsuarioEntidad
    {
        public Entidad Entidad { get; set; }
        public Usuario Usuario { get; set; }
        public short Rol { get; set; }
        public string LogoEntidad { get; set; }
        public long EntidadId { get; set; }
        public long UsuarioId { get; set; }

    }
}
