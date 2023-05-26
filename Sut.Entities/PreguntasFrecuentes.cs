using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class PreguntasFrecuentes
    {
        [Key]
        public long PreguntasID { get; set; }
        public long EntidadId { get; set; }
        public Entidad Entidad { get; set; }


        public string NombreCompleto { get; set; }
        public string Descripcion { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public string Estado { get; set; }
        public Respuesta RespuestaTelefono { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Anexo { get; set; }
        public Respuesta EstadoPublicar { get; set; }

        public string UserCreacion { get; set; }

        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }

        [NotMapped]
        public int activarayuda { get; set; }

    }
}
