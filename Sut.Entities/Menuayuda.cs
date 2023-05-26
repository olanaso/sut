using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class Menuayuda
    {
        [Key]
        public long MenuayudaId { get; set; }
 
        public long UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

    
        public Rol Rol { get; set; }

        public int Ruta { get; set; }

        public int Estado { get; set; }

        public long EntidadId { get; set; }

        public long MenuId { get; set; }
         

        [NotMapped]
        public MiembroEquipo MiembroEquipo { get; set; }



    }
}
