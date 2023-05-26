using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class Distrito
    {
        [Key]
        public long DistritoId { get; set; }
        //[Required]
        public string Nombre { get; set; }

        [Required, ForeignKey("Provincia")]
        public long ProvinciaId { get; set; }
        public Provincia Provincia { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }


        [NotMapped]
        public string Ruta {
            get {
                return string.Format("{0} - {1} - {2}", Nombre, Provincia.Nombre, Provincia.Departamento.Nombre);
            }
        }
    }
}
