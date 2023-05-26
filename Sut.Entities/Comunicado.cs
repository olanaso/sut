using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class Comunicado
    {
        [Key]
        public long ComunicadoID { get; set; }

        [ForeignKey("DocumentosNorma")]
        public long? DocumentosNormaID { get; set; }
        public DocumentosNorma DocumentosNorma { get; set; }

        public long EntidadId { get; set; }
        public long SectorId { get; set; }
        public long ProvinciaId { get; set; }
        public string Descripcion { get; set; }  
        public Respuesta Estado { get; set; }  
        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }


        

    }
}
